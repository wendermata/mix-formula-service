using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Formulas;

public sealed class DeleteFormulaUseCase(IRepository<Formula> repository, ILogger<DeleteFormulaUseCase> logger)
{
    public async Task<bool> ExecuteAsync(Guid id)
    {
        try
        {
            logger.LogInformation("Deleting formula with id: {Id}", id);
            if (await repository.GetByIdAsync(id) is null)
            {
                logger.LogWarning("Formula with id: {Id} not found for deletion", id);
                return false;
            }
            await repository.DeleteAsync(id);
            logger.LogInformation("Formula with id: {Id} deleted successfully", id);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while deleting formula with id: {Id}", id);
            throw;
        }
    }
}
