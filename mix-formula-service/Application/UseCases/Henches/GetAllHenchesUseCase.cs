using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Henches;

public sealed class GetAllHenchesUseCase(IRepository<Hench> repository, ILogger<GetAllHenchesUseCase> logger)
{
    public async Task<IEnumerable<Hench>> ExecuteAsync()
    {
        try
        {
            logger.LogInformation("Fetching all henches");
            return await repository.GetAllAsync(h => h.Maps);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching all henches");
            throw;
        }
    }
}
