using Application.UseCases.Henches;
using Domain.Entities;

namespace Api.Presentation.GraphQL.Queries;

public class HenchQueries
{
    public Task<IEnumerable<Hench>> GetHenches([Service] GetAllHenchesUseCase useCase) =>
        useCase.ExecuteAsync();

    public Task<Hench> GetHench(Guid id, [Service] GetHenchByIdUseCase useCase) =>
        useCase.ExecuteAsync(id);
}
