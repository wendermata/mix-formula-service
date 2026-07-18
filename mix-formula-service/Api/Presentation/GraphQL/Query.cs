using Application.UseCases.Formulas;
using Application.UseCases.Henches;
using Application.UseCases.Items;
using Application.UseCases.Maps;
using Domain.Entities;

namespace Api.Presentation.GraphQL;

public class Query
{
    public Task<IEnumerable<Hench>> GetHenches(GetAllHenchesUseCase useCase) => useCase.ExecuteAsync();

    public Task<Hench?> GetHench(Guid id, GetHenchByIdUseCase useCase) => useCase.ExecuteAsync(id);

    public Task<IEnumerable<Item>> GetItems(GetAllItemsUseCase useCase) => useCase.ExecuteAsync();

    public Task<Item?> GetItem(Guid id, GetItemByIdUseCase useCase) => useCase.ExecuteAsync(id);

    public Task<IEnumerable<Formula>> GetFormulas(GetAllFormulasUseCase useCase) => useCase.ExecuteAsync();

    public Task<Formula?> GetFormula(Guid id, GetFormulaByIdUseCase useCase) => useCase.ExecuteAsync(id);

    public Task<IEnumerable<Map>> GetMaps(GetAllMapsUseCase useCase) => useCase.ExecuteAsync();

    public Task<Map?> GetMap(Guid id, GetMapByIdUseCase useCase) => useCase.ExecuteAsync(id);
}
