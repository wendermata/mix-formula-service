using Domain.Entities;
using Domain.Repositories;
using Application.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Items;

public sealed class DeleteItemUseCase(IRepository<Item> repository, ILogger<DeleteItemUseCase> logger)
{
    public async Task<bool> ExecuteAsync(Guid id)
    {
        logger.LogInformation("Deleting item with id: {Id}", id);
        _ = await repository.GetByIdAsync(id) ?? throw new NotFoundException(nameof(Item), id);
        await repository.DeleteAsync(id);
        logger.LogInformation("Item with id: {Id} deleted successfully", id);
        return true;
    }
}
