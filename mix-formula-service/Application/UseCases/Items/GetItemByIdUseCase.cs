using Domain.Entities;
using Domain.Repositories;
using Application.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Items;

public sealed class GetItemByIdUseCase(IRepository<Item> repository, ILogger<GetItemByIdUseCase> logger)
{
    public async Task<Item> ExecuteAsync(Guid id)
    {
        logger.LogInformation("Fetching item by id: {Id}", id);
        var item = await repository.GetByIdAsync(id);
        return item ?? throw new NotFoundException(nameof(Item), id);
    }
}
