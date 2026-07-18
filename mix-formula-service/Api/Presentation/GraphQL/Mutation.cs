using Application.UseCases.Formulas;
using Application.UseCases.Henches;
using Application.UseCases.Items;
using Application.UseCases.Maps;
using Domain.Entities;
using Domain.Enums;

namespace Api.Presentation.GraphQL;

public class Mutation
{
    public Task<Hench> CreateHench(string name, HenchType type, int level, CreateHenchUseCase useCase) => useCase.ExecuteAsync(name, type, level);
    public Task<Hench?> UpdateHench(Guid id, string name, HenchType type, int level, UpdateHenchUseCase useCase) => useCase.ExecuteAsync(id, name, type, level);
    public Task<bool> DeleteHench(Guid id, DeleteHenchUseCase useCase) => useCase.ExecuteAsync(id);

    public Task<Item> CreateItem(string name, string description, CreateItemUseCase useCase) => useCase.ExecuteAsync(name, description);
    public Task<Item?> UpdateItem(Guid id, string name, string description, UpdateItemUseCase useCase) => useCase.ExecuteAsync(id, name, description);
    public Task<bool> DeleteItem(Guid id, DeleteItemUseCase useCase) => useCase.ExecuteAsync(id);

    public Task<Formula> CreateFormula(string name, Guid sourceHench1Id, Guid sourceHench2Id, Guid targetHenchId, double successRate, CreateFormulaUseCase useCase) => useCase.ExecuteAsync(name, sourceHench1Id, sourceHench2Id, targetHenchId, successRate);
    public Task<Formula?> UpdateFormula(Guid id, string name, Guid sourceHench1Id, Guid sourceHench2Id, Guid targetHenchId, double successRate, UpdateFormulaUseCase useCase) => useCase.ExecuteAsync(id, name, sourceHench1Id, sourceHench2Id, targetHenchId, successRate);
    public Task<bool> DeleteFormula(Guid id, DeleteFormulaUseCase useCase) => useCase.ExecuteAsync(id);

    public Task<Map> CreateMap(string name, List<Guid> henchIds, CreateMapUseCase useCase) => useCase.ExecuteAsync(name, henchIds);
    public Task<Map?> UpdateMap(Guid id, string name, List<Guid> henchIds, UpdateMapUseCase useCase) => useCase.ExecuteAsync(id, name, henchIds);
    public Task<bool> DeleteMap(Guid id, DeleteMapUseCase useCase) => useCase.ExecuteAsync(id);
}
