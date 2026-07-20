using Domain.Entities;
using Domain.Repositories;
using Application.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Formulas;

public sealed class DeleteFormulaUseCase(IRepository<Formula> repository, ILogger<DeleteFormulaUseCase> logger)
{
    public async Task<bool> ExecuteAsync(Guid id)
    {
        logger.LogInformation("Deleting formula with id: {Id}", id);
        _ = await repository.GetByIdAsync(id) ?? throw new NotFoundException(nameof(Formula), id);
        await repository.DeleteAsync(id);
        logger.LogInformation("Formula with id: {Id} deleted successfully", id);
        return true;
    }
}
