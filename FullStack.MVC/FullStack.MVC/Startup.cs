using FullStack.MVC.Data;
using FullStack.MVC.Data.Models;
using FullStack.MVC.Extensions;
using FullStack.MVC.Services;
using FullStack.MVC.Services.Interfaces;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using System;
using System.Reflection;

namespace FullStack.MVC
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration["ConnectionString"],
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: Int32.Parse(Configuration["DBConnectionRetries"]), 
                        maxRetryDelay: TimeSpan.FromSeconds(30), 
                        errorNumbersToAdd: null);
                }));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddHealthChecks(Configuration);

            services
                .AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddHttpClient<IWeatherService, WeatherService>();
            services.AddHttpClient<IBrandService, BrandService>();
            services.AddHttpClient<IMobileService, MobileService>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();

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

            var retryPolicy = Policy
                    .Handle<SqlException>()
                    .WaitAndRetry(Int32.Parse(Configuration["DBConnectionRetries"]),
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    );

            retryPolicy.Execute(() =>
            {
                app.SeedDb().GetAwaiter().GetResult();
            });
        }
    }
}
