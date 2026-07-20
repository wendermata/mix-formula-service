using Domain.Entities;
using Domain.Repositories;
using Application.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Maps;

public sealed class GetMapByIdUseCase(IRepository<Map> repository, ILogger<GetMapByIdUseCase> logger)
{
    public async Task<Map> ExecuteAsync(Guid id)
    {
        logger.LogInformation("Fetching map by id: {Id}", id);
        var map = await repository.GetByIdAsync(id, m => m.Henches);
        return map ?? throw new NotFoundException(nameof(Map), id);
    }
}
