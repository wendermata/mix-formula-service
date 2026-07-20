using Application.UseCases.Formulas;
using Domain.Entities;

namespace Api.Presentation.GraphQL.Queries;

public class FormulaQueries
{
    public Task<IEnumerable<Formula>> GetFormulas([Service] GetAllFormulasUseCase useCase) =>
        useCase.ExecuteAsync();

    public Task<Formula> GetFormula(Guid id, [Service] GetFormulaByIdUseCase useCase) =>
        useCase.ExecuteAsync(id);
}
