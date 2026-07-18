using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Maps;

public sealed class DeleteMapUseCase(IRepository<Map> repository, ILogger<DeleteMapUseCase> logger)
{
    public async Task<bool> ExecuteAsync(Guid id)
    {
        try
        {
            logger.LogInformation("Deleting map with id: {Id}", id);
            if (await repository.GetByIdAsync(id) is null)
            {
                logger.LogWarning("Map with id: {Id} not found for deletion", id);
                return false;
            }
            await repository.DeleteAsync(id);
            logger.LogInformation("Map with id: {Id} deleted successfully", id);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while deleting map with id: {Id}", id);
            throw;
        }
    }
}
