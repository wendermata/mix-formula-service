using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Maps;

public sealed class GetMapByIdUseCase(IRepository<Map> repository, ILogger<GetMapByIdUseCase> logger)
{
    public async Task<Map?> ExecuteAsync(Guid id)
    {
        try
        {
            logger.LogInformation("Fetching map by id: {Id}", id);
            return await repository.GetByIdAsync(id, m => m.Henches);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching map by id: {Id}", id);
            throw;
        }
    }
}
