using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Maps;

public sealed class CreateMapUseCase(IRepository<Map> mapRepository, IRepository<Hench> henchRepository, ILogger<CreateMapUseCase> logger)
{
    public async Task<Map> ExecuteAsync(string name, IReadOnlyCollection<Guid> henchIds)
    {
        logger.LogInformation("Creating map with name: {Name} and {HenchCount} henches", name, henchIds.Count);
        var map = new Map { Id = Guid.NewGuid(), Name = name };
        foreach (var id in henchIds.Distinct())
        {
            var hench = await henchRepository.GetByIdAsync(id);
            if (hench is not null) map.Henches.Add(hench);
        }
        var result = await mapRepository.AddAsync(map);
        logger.LogInformation("Map created successfully with id: {Id}", result.Id);
        return result;
    }
}
