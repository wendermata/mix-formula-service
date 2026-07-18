using Domain.Entities;
using Domain.Repositories;

namespace Application.UseCases.Items;

public sealed class CreateItemUseCase(IRepository<Item> repository)
{
    public Task<Item> ExecuteAsync(string name, string description) =>
        repository.AddAsync(new Item { Id = Guid.NewGuid(), Name = name, Description = description });
}

public sealed class GetItemByIdUseCase(IRepository<Item> repository)
{
    public Task<Item?> ExecuteAsync(Guid id) => repository.GetByIdAsync(id);
}

public sealed class GetAllItemsUseCase(IRepository<Item> repository)
{
    public Task<IEnumerable<Item>> ExecuteAsync() => repository.GetAllAsync();
}

public sealed class UpdateItemUseCase(IRepository<Item> repository)
{
    public async Task<Item?> ExecuteAsync(Guid id, string name, string description)
    {
        var item = await repository.GetByIdAsync(id);
        if (item is null) return null;
        item.Name = name; item.Description = description;
        await repository.UpdateAsync(item);
        return item;
    }
}

public sealed class DeleteItemUseCase(IRepository<Item> repository)
{
    public async Task<bool> ExecuteAsync(Guid id)
    {
        if (await repository.GetByIdAsync(id) is null) return false;
        await repository.DeleteAsync(id);
        return true;
    }
}
