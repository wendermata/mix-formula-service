using Application.UseCases.Formulas;
using Api.Presentation.GraphQL.Inputs;
using Domain.Entities;

namespace Api.Presentation.GraphQL.Mutations;

public class FormulaMutations
{
    public Task<Formula> CreateFormula(CreateFormulaInput input, [Service] CreateFormulaUseCase useCase) =>
        useCase.ExecuteAsync(input.Name, input.SourceHench1Id, input.SourceHench2Id, input.TargetHenchId, input.SuccessRate);

    public Task<Formula?> UpdateFormula(UpdateFormulaInput input, [Service] UpdateFormulaUseCase useCase) =>
        useCase.ExecuteAsync(input.Id, input.Name, input.SourceHench1Id, input.SourceHench2Id, input.TargetHenchId, input.SuccessRate);

    public Task<bool> DeleteFormula(Guid id, [Service] DeleteFormulaUseCase useCase) =>
        useCase.ExecuteAsync(id);
}
