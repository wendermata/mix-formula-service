using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Maps;

public sealed class GetAllMapsUseCase(IRepository<Map> repository, ILogger<GetAllMapsUseCase> logger)
{
    public async Task<IEnumerable<Map>> ExecuteAsync()
    {
        try
        {
            logger.LogInformation("Fetching all maps");
            return await repository.GetAllAsync(m => m.Henches);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching all maps");
            throw;
        }
    }
}
