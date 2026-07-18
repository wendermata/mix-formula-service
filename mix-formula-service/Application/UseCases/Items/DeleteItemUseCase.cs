using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Items;

public sealed class DeleteItemUseCase(IRepository<Item> repository, ILogger<DeleteItemUseCase> logger)
{
    public async Task<bool> ExecuteAsync(Guid id)
    {
        try
        {
            logger.LogInformation("Deleting item with id: {Id}", id);
            if (await repository.GetByIdAsync(id) is null)
            {
                logger.LogWarning("Item with id: {Id} not found for deletion", id);
                return false;
            }
            await repository.DeleteAsync(id);
            logger.LogInformation("Item with id: {Id} deleted successfully", id);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while deleting item with id: {Id}", id);
            throw;
        }
    }
}
