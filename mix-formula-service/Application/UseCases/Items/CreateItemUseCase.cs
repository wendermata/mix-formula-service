using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Items;

public sealed class CreateItemUseCase(IRepository<Item> repository, ILogger<CreateItemUseCase> logger)
{
    public async Task<Item> ExecuteAsync(string name, string description)
    {
        logger.LogInformation("Creating item with name: {Name}", name);
        var item = await repository.AddAsync(new Item { Id = Guid.NewGuid(), Name = name, Description = description });
        logger.LogInformation("Item created successfully with id: {Id}", item.Id);
        return item;
    }
}
