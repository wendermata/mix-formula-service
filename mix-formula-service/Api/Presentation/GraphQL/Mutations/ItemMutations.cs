using Application.UseCases.Items;
using Api.Presentation.GraphQL.Inputs;
using Domain.Entities;

namespace Api.Presentation.GraphQL.Mutations;

public class ItemMutations
{
    public Task<Item> CreateItem(CreateItemInput input, [Service] CreateItemUseCase useCase) =>
        useCase.ExecuteAsync(input.Name, input.Description);

    public Task<Item> UpdateItem(UpdateItemInput input, [Service] UpdateItemUseCase useCase) =>
        useCase.ExecuteAsync(input.Id, input.Name, input.Description);

    public Task<bool> DeleteItem(Guid id, [Service] DeleteItemUseCase useCase) =>
        useCase.ExecuteAsync(id);
}
