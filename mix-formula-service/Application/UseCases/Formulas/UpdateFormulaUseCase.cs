using Domain.Entities;
using Domain.Repositories;
using Application.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Formulas;

public sealed class UpdateFormulaUseCase(IRepository<Formula> repository, ILogger<UpdateFormulaUseCase> logger)
{
    public async Task<Formula> ExecuteAsync(Guid id, string name, Guid sourceHench1Id, Guid sourceHench2Id, Guid targetHenchId, double successRate)
    {
        logger.LogInformation("Updating formula with id: {Id}", id);
        var formula = await repository.GetByIdAsync(id) ?? throw new NotFoundException(nameof(Formula), id);
        formula.Name = name;
        formula.SourceHench1Id = sourceHench1Id;
        formula.SourceHench2Id = sourceHench2Id;
        formula.TargetHenchId = targetHenchId;
        formula.SuccessRate = successRate;
        await repository.UpdateAsync(formula);
        logger.LogInformation("Formula with id: {Id} updated successfully", id);
        return formula;
    }
}
