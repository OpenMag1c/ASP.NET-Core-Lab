using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WebAPI.HealthCheck;

namespace WebAPI.Infrastructure
{
    public static class HealthCheckServicesExtensions
    {
        public static IServiceCollection AddHealthCheck(this IServiceCollection services, string connectionString)
        {
            services.AddHealthChecks()
                // Add a health check for a SQL Server database
                .AddCheck(
                    "usersdb-check",
                    new SqlConnectionHealthCheck(connectionString),
                    HealthStatus.Unhealthy,
                    new string[] { "usersdb" });

            return services;
        }
    }
}