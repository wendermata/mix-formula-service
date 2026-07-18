using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Items;

public sealed class UpdateItemUseCase(IRepository<Item> repository, ILogger<UpdateItemUseCase> logger)
{
    public async Task<Item?> ExecuteAsync(Guid id, string name, string description)
    {
        try
        {
            logger.LogInformation("Updating item with id: {Id}", id);
            var item = await repository.GetByIdAsync(id);
            if (item is null)
            {
                logger.LogWarning("Item with id: {Id} not found", id);
                return null;
            }
            item.Name = name;
            item.Description = description;
            await repository.UpdateAsync(item);
            logger.LogInformation("Item with id: {Id} updated successfully", id);
            return item;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while updating item with id: {Id}", id);
            throw;
        }
    }
}
