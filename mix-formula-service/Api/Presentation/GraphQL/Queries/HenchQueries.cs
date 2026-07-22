using Application.UseCases.Henches;
using Domain.Entities;

namespace Api.Presentation.GraphQL.Queries;

[ExtendObjectType("Query")]
public class HenchQueries
{
    public Task<IEnumerable<Hench>> GetHenches(HenchFilter? filter, [Service] GetAllHenchesUseCase useCase) =>
        useCase.ExecuteAsync(filter);

    public Task<Hench> GetHench(Guid id, [Service] GetHenchByIdUseCase useCase) =>
        useCase.ExecuteAsync(id);
}
