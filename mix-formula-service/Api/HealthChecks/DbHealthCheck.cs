using Infrastructure.Database;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Api.HealthChecks;

public class DbHealthCheck(IServiceScopeFactory scopeFactory) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            using var scope = scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await dbContext.Database.CanConnectAsync(cancellationToken);
            return HealthCheckResult.Healthy("Database is reachable.");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Database is not reachable.", ex);
        }
    }
}
