using Domain.Entities;
using Domain.Repositories;
using Application.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Henches;

public sealed class GetHenchByIdUseCase(IRepository<Hench> repository, ILogger<GetHenchByIdUseCase> logger)
{
    public async Task<Hench> ExecuteAsync(Guid id)
    {
        logger.LogInformation("Fetching hench by id: {Id}", id);
        var hench = await repository.GetByIdAsync(id, h => h.Maps);
        return hench ?? throw new NotFoundException(nameof(Hench), id);
    }
}
