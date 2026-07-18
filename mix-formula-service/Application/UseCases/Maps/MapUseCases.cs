using Domain.Entities;
using Domain.Repositories;

namespace Application.UseCases.Maps;

public sealed class CreateMapUseCase(IRepository<Map> mapRepository, IRepository<Hench> henchRepository)
{
    public async Task<Map> ExecuteAsync(string name, IReadOnlyCollection<Guid> henchIds)
    {
        var map = new Map { Id = Guid.NewGuid(), Name = name };
        foreach (var id in henchIds.Distinct())
        {
            var hench = await henchRepository.GetByIdAsync(id);
            if (hench is not null) map.Henches.Add(hench);
        }
        return await mapRepository.AddAsync(map);
    }
}

public sealed class GetMapByIdUseCase(IRepository<Map> repository)
{
    public Task<Map?> ExecuteAsync(Guid id) => repository.GetByIdAsync(id, m => m.Henches);
}

public sealed class GetAllMapsUseCase(IRepository<Map> repository)
{
    public Task<IEnumerable<Map>> ExecuteAsync() => repository.GetAllAsync(m => m.Henches);
}

public sealed class UpdateMapUseCase(IRepository<Map> mapRepository, IRepository<Hench> henchRepository)
{
    public async Task<Map?> ExecuteAsync(Guid id, string name, IReadOnlyCollection<Guid> henchIds)
    {
        var map = await mapRepository.GetByIdAsync(id, m => m.Henches);
        if (map is null) return null;
        map.Name = name;
        map.Henches.Clear();
        foreach (var henchId in henchIds.Distinct())
        {
            var hench = await henchRepository.GetByIdAsync(henchId);
            if (hench is not null) map.Henches.Add(hench);
        }
        await mapRepository.UpdateAsync(map);
        return map;
    }
}

public sealed class DeleteMapUseCase(IRepository<Map> repository)
{
    public async Task<bool> ExecuteAsync(Guid id)
    {
        if (await repository.GetByIdAsync(id) is null) return false;
        await repository.DeleteAsync(id);
        return true;
    }
}
