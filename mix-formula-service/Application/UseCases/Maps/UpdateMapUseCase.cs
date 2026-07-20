using Domain.Entities;
using Domain.Repositories;
using Application.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Maps;

public sealed class UpdateMapUseCase(IRepository<Map> mapRepository, IRepository<Hench> henchRepository, ILogger<UpdateMapUseCase> logger)
{
    public async Task<Map> ExecuteAsync(Guid id, string name, IReadOnlyCollection<Guid> henchIds)
    {
        logger.LogInformation("Updating map with id: {Id}", id);
        var map = await mapRepository.GetByIdAsync(id, m => m.Henches) ?? throw new NotFoundException(nameof(Map), id);
        map.Name = name;
        map.Henches.Clear();
        foreach (var henchId in henchIds.Distinct())
        {
            var hench = await henchRepository.GetByIdAsync(henchId);
            if (hench is not null) map.Henches.Add(hench);
        }
        await mapRepository.UpdateAsync(map);
        logger.LogInformation("Map with id: {Id} updated successfully", id);
        return map;
    }
}
