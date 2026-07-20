using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Formulas;

public sealed class GetAllFormulasUseCase(IRepository<Formula> repository, ILogger<GetAllFormulasUseCase> logger)
{
    public async Task<IEnumerable<Formula>> ExecuteAsync()
    {
        logger.LogInformation("Fetching all formulas");
        return await repository.GetAllAsync(f => f.SourceHench1!, f => f.SourceHench2!, f => f.TargetHench!);
    }
}
