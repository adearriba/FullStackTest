﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FullStack.API.Extensions
{
    public static class HealthChecksExtensions
    {
        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());

            hcBuilder
                .AddSqlServer(
                    configuration["ConnectionString"],
                    name: "FullStackDb-check",
                    tags: new string[] { "fullstackdb-api" })
                .AddRedis(
                    configuration["RedisConnectionsString"],
                    name: "RedisCache-check",
                    tags: new string[] { "fullstackcache-api" },
                    failureStatus: HealthStatus.Degraded);

            return services;
        }
    }
}
