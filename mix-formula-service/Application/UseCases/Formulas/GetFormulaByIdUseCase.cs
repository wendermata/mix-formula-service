using Domain.Entities;
using Domain.Repositories;
using Application.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Formulas;

public sealed class GetFormulaByIdUseCase(IRepository<Formula> repository, ILogger<GetFormulaByIdUseCase> logger)
{
    public async Task<Formula> ExecuteAsync(Guid id)
    {
        logger.LogInformation("Fetching formula by id: {Id}", id);
        var formula = await repository.GetByIdAsync(id, f => f.SourceHench1!, f => f.SourceHench2!, f => f.TargetHench!);
        return formula ?? throw new NotFoundException(nameof(Formula), id);
    }
}
