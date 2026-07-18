using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Formulas;

public sealed class GetFormulaByIdUseCase(IRepository<Formula> repository, ILogger<GetFormulaByIdUseCase> logger)
{
    public async Task<Formula?> ExecuteAsync(Guid id)
    {
        try
        {
            logger.LogInformation("Fetching formula by id: {Id}", id);
            return await repository.GetByIdAsync(id, f => f.SourceHench1!, f => f.SourceHench2!, f => f.TargetHench!);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching formula by id: {Id}", id);
            throw;
        }
    }
}
