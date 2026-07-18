using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;

namespace Application.UseCases.Henches;

public sealed class CreateHenchUseCase(IRepository<Hench> repository)
{
    public Task<Hench> ExecuteAsync(string name, HenchType type, int level) =>
        repository.AddAsync(new Hench { Id = Guid.NewGuid(), Name = name, Type = type, Level = level });
}

public sealed class GetHenchByIdUseCase(IRepository<Hench> repository)
{
    public Task<Hench?> ExecuteAsync(Guid id) => repository.GetByIdAsync(id, h => h.Maps);
}

public sealed class GetAllHenchesUseCase(IRepository<Hench> repository)
{
    public Task<IEnumerable<Hench>> ExecuteAsync() => repository.GetAllAsync(h => h.Maps);
}

public sealed class UpdateHenchUseCase(IRepository<Hench> repository)
{
    public async Task<Hench?> ExecuteAsync(Guid id, string name, HenchType type, int level)
    {
        var hench = await repository.GetByIdAsync(id);
        if (hench is null) return null;
        hench.Name = name; hench.Type = type; hench.Level = level;
        await repository.UpdateAsync(hench);
        return hench;
    }
}

public sealed class DeleteHenchUseCase(IRepository<Hench> repository)
{
    public async Task<bool> ExecuteAsync(Guid id)
    {
        if (await repository.GetByIdAsync(id) is null) return false;
        await repository.DeleteAsync(id);
        return true;
    }
}
