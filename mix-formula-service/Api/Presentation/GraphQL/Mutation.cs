using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;

namespace Api.Presentation.GraphQL;

public class Mutation
{
    public async Task<Hench> CreateHench(
        string name,
        HenchType type,
        int level,
        IRepository<Hench> repository)
    {
        var hench = new Hench
        {
            Id = Guid.NewGuid(),
            Name = name,
            Type = type,
            Level = level
        };

        return await repository.AddAsync(hench);
    }

    public async Task<Item> CreateItem(
        string name,
        string description,
        IRepository<Item> repository)
    {
        var item = new Item
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description
        };

        return await repository.AddAsync(item);
    }

    public async Task<Formula> CreateFormula(
        string name,
        Guid sourceHench1Id,
        Guid sourceHench2Id,
        Guid targetHenchId,
        double successRate,
        IRepository<Formula> repository)
    {
        var formula = new Formula
        {
            Id = Guid.NewGuid(),
            Name = name,
            SourceHench1Id = sourceHench1Id,
            SourceHench2Id = sourceHench2Id,
            TargetHenchId = targetHenchId,
            SuccessRate = successRate
        };

        return await repository.AddAsync(formula);
    }

    public async Task<Map> CreateMap(
        string name,
        List<Guid> henchIds,
        IRepository<Map> mapRepository,
        IRepository<Hench> henchRepository)
    {
        var map = new Map
        {
            Id = Guid.NewGuid(),
            Name = name
        };

        foreach (var henchId in henchIds)
        {
            var hench = await henchRepository.GetByIdAsync(henchId);
            if (hench != null)
            {
                map.Henches.Add(hench);
            }
        }

        return await mapRepository.AddAsync(map);
    }
}
