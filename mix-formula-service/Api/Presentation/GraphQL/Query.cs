using Domain.Entities;
using Domain.Repositories;

namespace Api.Presentation.GraphQL;

public class Query
{
    public async Task<IEnumerable<Hench>> GetHenches(IRepository<Hench> repository) =>
        await repository.GetAllAsync(h => h.Maps);

    public async Task<Hench?> GetHench(Guid id, IRepository<Hench> repository) =>
        await repository.GetByIdAsync(id, h => h.Maps);

    public async Task<IEnumerable<Item>> GetItems(IRepository<Item> repository) =>
        await repository.GetAllAsync();

    public async Task<Item?> GetItem(Guid id, IRepository<Item> repository) =>
        await repository.GetByIdAsync(id);

    public async Task<IEnumerable<Formula>> GetFormulas(IRepository<Formula> repository) =>
        await repository.GetAllAsync(f => f.SourceHench1!, f => f.SourceHench2!, f => f.TargetHench!);

    public async Task<Formula?> GetFormula(Guid id, IRepository<Formula> repository) =>
        await repository.GetByIdAsync(id, f => f.SourceHench1!, f => f.SourceHench2!, f => f.TargetHench!);

    public async Task<IEnumerable<Map>> GetMaps(IRepository<Map> repository) =>
        await repository.GetAllAsync(m => m.Henches);

    public async Task<Map?> GetMap(Guid id, IRepository<Map> repository) =>
        await repository.GetByIdAsync(id, m => m.Henches);
}
