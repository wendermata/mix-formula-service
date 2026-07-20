using Domain.Entities;
using Domain.Repositories;
using Application.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Maps;

public sealed class DeleteMapUseCase(IRepository<Map> repository, ILogger<DeleteMapUseCase> logger)
{
    public async Task<bool> ExecuteAsync(Guid id)
    {
        logger.LogInformation("Deleting map with id: {Id}", id);
        _ = await repository.GetByIdAsync(id) ?? throw new NotFoundException(nameof(Map), id);
        await repository.DeleteAsync(id);
        logger.LogInformation("Map with id: {Id} deleted successfully", id);
        return true;
    }
}
