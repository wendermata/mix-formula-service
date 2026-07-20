using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Formulas;

public sealed class CreateFormulaUseCase(IRepository<Formula> repository, ILogger<CreateFormulaUseCase> logger)
{
    public async Task<Formula> ExecuteAsync(string name, Guid sourceHench1Id, Guid sourceHench2Id, Guid targetHenchId, double successRate)
    {
        logger.LogInformation("Creating formula with name: {Name}", name);
        var formula = await repository.AddAsync(new Formula
        {
            Id = Guid.NewGuid(),
            Name = name,
            SourceHench1Id = sourceHench1Id,
            SourceHench2Id = sourceHench2Id,
            TargetHenchId = targetHenchId,
            SuccessRate = successRate
        });
        logger.LogInformation("Formula created successfully with id: {Id}", formula.Id);
        return formula;
    }
}
