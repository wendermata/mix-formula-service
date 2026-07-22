using Application.UseCases.Henches;
using Api.Presentation.GraphQL.Inputs;
using Domain.Entities;

namespace Api.Presentation.GraphQL.Mutations;

[ExtendObjectType("Mutation")]
public class HenchMutations
{
    public Task<Hench> CreateHench(CreateHenchInput input, [Service] CreateHenchUseCase useCase) =>
        useCase.ExecuteAsync(input.Name, input.Type, input.Level);

    public Task<Hench> UpdateHench(UpdateHenchInput input, [Service] UpdateHenchUseCase useCase) =>
        useCase.ExecuteAsync(input.Id, input.Name, input.Type, input.Level);

    public Task<bool> DeleteHench(Guid id, [Service] DeleteHenchUseCase useCase) =>
        useCase.ExecuteAsync(id);

    public Task<int> DeduplicateHenches([Service] DeduplicateHenchesUseCase useCase) =>
        useCase.ExecuteAsync();
}
