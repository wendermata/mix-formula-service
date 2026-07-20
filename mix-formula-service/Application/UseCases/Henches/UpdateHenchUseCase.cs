using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Application.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Henches;

public sealed class UpdateHenchUseCase(IRepository<Hench> repository, ILogger<UpdateHenchUseCase> logger)
{
    public async Task<Hench> ExecuteAsync(Guid id, string name, HenchType type, int level)
    {
        logger.LogInformation("Updating hench with id: {Id}", id);
        var hench = await repository.GetByIdAsync(id) ?? throw new NotFoundException(nameof(Hench), id);
        hench.Name = name;
        hench.Type = type;
        hench.Level = level;
        await repository.UpdateAsync(hench);
        logger.LogInformation("Hench with id: {Id} updated successfully", id);
        return hench;
    }
}
