using Application.UseCases.Maps;
using Api.Presentation.GraphQL.Inputs;
using Domain.Entities;

namespace Api.Presentation.GraphQL.Mutations;

public class MapMutations
{
    public Task<Map> CreateMap(CreateMapInput input, [Service] CreateMapUseCase useCase) =>
        useCase.ExecuteAsync(input.Name, input.HenchIds);

    public Task<Map> UpdateMap(UpdateMapInput input, [Service] UpdateMapUseCase useCase) =>
        useCase.ExecuteAsync(input.Id, input.Name, input.HenchIds);

    public Task<bool> DeleteMap(Guid id, [Service] DeleteMapUseCase useCase) =>
        useCase.ExecuteAsync(id);
}
