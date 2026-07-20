using Domain.Entities;
using Domain.Repositories;
using Application.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Henches;

public sealed class DeleteHenchUseCase(IRepository<Hench> repository, ILogger<DeleteHenchUseCase> logger)
{
    public async Task<bool> ExecuteAsync(Guid id)
    {
        logger.LogInformation("Deleting hench with id: {Id}", id);
        _ = await repository.GetByIdAsync(id) ?? throw new NotFoundException(nameof(Hench), id);
        await repository.DeleteAsync(id);
        logger.LogInformation("Hench with id: {Id} deleted successfully", id);
        return true;
    }
}
