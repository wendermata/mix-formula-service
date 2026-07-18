using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Henches;

public sealed class DeleteHenchUseCase(IRepository<Hench> repository, ILogger<DeleteHenchUseCase> logger)
{
    public async Task<bool> ExecuteAsync(Guid id)
    {
        try
        {
            logger.LogInformation("Deleting hench with id: {Id}", id);
            if (await repository.GetByIdAsync(id) is null)
            {
                logger.LogWarning("Hench with id: {Id} not found for deletion", id);
                return false;
            }
            await repository.DeleteAsync(id);
            logger.LogInformation("Hench with id: {Id} deleted successfully", id);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while deleting hench with id: {Id}", id);
            throw;
        }
    }
}
