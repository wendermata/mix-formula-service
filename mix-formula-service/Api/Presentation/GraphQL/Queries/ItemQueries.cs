using Application.UseCases.Items;
using Domain.Entities;

namespace Api.Presentation.GraphQL.Queries;

public class ItemQueries
{
    public Task<IEnumerable<Item>> GetItems([Service] GetAllItemsUseCase useCase) =>
        useCase.ExecuteAsync();

    public Task<Item> GetItem(Guid id, [Service] GetItemByIdUseCase useCase) =>
        useCase.ExecuteAsync(id);
}
