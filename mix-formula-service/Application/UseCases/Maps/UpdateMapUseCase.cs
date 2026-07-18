using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Maps;

public sealed class UpdateMapUseCase(IRepository<Map> mapRepository, IRepository<Hench> henchRepository, ILogger<UpdateMapUseCase> logger)
{
    public async Task<Map?> ExecuteAsync(Guid id, string name, IReadOnlyCollection<Guid> henchIds)
    {
        try
        {
            logger.LogInformation("Updating map with id: {Id}", id);
            var map = await mapRepository.GetByIdAsync(id, m => m.Henches);
            if (map is null)
            {
                logger.LogWarning("Map with id: {Id} not found", id);
                return null;
            }
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
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while updating map with id: {Id}", id);
            throw;
        }
    }
}
