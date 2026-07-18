using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Henches;

public sealed class UpdateHenchUseCase(IRepository<Hench> repository, ILogger<UpdateHenchUseCase> logger)
{
    public async Task<Hench?> ExecuteAsync(Guid id, string name, HenchType type, int level)
    {
        try
        {
            logger.LogInformation("Updating hench with id: {Id}", id);
            var hench = await repository.GetByIdAsync(id);
            if (hench is null)
            {
                logger.LogWarning("Hench with id: {Id} not found", id);
                return null;
            }
            hench.Name = name;
            hench.Type = type;
            hench.Level = level;
            await repository.UpdateAsync(hench);
            logger.LogInformation("Hench with id: {Id} updated successfully", id);
            return hench;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while updating hench with id: {Id}", id);
            throw;
        }
    }
}
