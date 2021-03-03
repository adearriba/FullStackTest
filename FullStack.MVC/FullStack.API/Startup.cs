using FullStack.API.Data;
using FullStack.API.Extensions;
using FullStack.API.Repositories;
using FullStack.API.Repositories.Interfaces;
using FullStack.API.Services;
using FullStack.API.Services.Interfaces;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Polly;
using StackExchange.Redis;
using System;
using System.Reflection;

namespace FullStack.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<APIDbContext>(options =>
                options.UseSqlServer(Configuration["ConnectionString"],
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: Int32.Parse(Configuration["DBConnectionRetries"]), 
                        maxRetryDelay: TimeSpan.FromSeconds(30), 
                        errorNumbersToAdd: null);
                }));

            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<IMobileRepository, MobileRepository>();
            services.AddHttpClient<IWeatherService, WeatherService>();

            services.AddSingleton<IConnectionMultiplexer>(sp =>
                ConnectionMultiplexer.Connect(Configuration["RedisConnectionsString"]));
            services.AddSingleton<ICacheService, RedisCacheService>();

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddHealthChecks(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FullStack.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FullStack.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                {
                    Predicate = r => r.Name.Contains("self")
                });
            });

            RunDbMigrations(app);
        }

        private void RunDbMigrations(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var retryPolicy = Policy
                    .Handle<SqlException>()
                    .WaitAndRetry(Int32.Parse(Configuration["DBConnectionRetries"]),
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    );

                retryPolicy.Execute( () =>
                {
                    var dbContext = scope.ServiceProvider.GetService<APIDbContext>();
                    dbContext.Database.Migrate();
                });
            }
        }
    }
}
