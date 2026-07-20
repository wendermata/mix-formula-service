using Domain.Entities;
using Domain.Repositories;
using Application.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Items;

public sealed class UpdateItemUseCase(IRepository<Item> repository, ILogger<UpdateItemUseCase> logger)
{
    public async Task<Item> ExecuteAsync(Guid id, string name, string description)
    {
        logger.LogInformation("Updating item with id: {Id}", id);
        var item = await repository.GetByIdAsync(id) ?? throw new NotFoundException(nameof(Item), id);
        item.Name = name;
        item.Description = description;
        await repository.UpdateAsync(item);
        logger.LogInformation("Item with id: {Id} updated successfully", id);
        return item;
    }
}
