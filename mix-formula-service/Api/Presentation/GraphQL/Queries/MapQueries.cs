using Application.UseCases.Maps;
using Domain.Entities;

namespace Api.Presentation.GraphQL.Queries;

[ExtendObjectType("Query")]
public class MapQueries
{
    public Task<IEnumerable<Map>> GetMaps([Service] GetAllMapsUseCase useCase) =>
        useCase.ExecuteAsync();

    public Task<Map> GetMap(Guid id, [Service] GetMapByIdUseCase useCase) =>
        useCase.ExecuteAsync(id);
}
