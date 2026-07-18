using Domain.Entities;
using Domain.Repositories;

namespace Application.UseCases.Formulas;

public sealed class CreateFormulaUseCase(IRepository<Formula> repository)
{
    public Task<Formula> ExecuteAsync(string name, Guid sourceHench1Id, Guid sourceHench2Id, Guid targetHenchId, double successRate) =>
        repository.AddAsync(new Formula { Id = Guid.NewGuid(), Name = name, SourceHench1Id = sourceHench1Id, SourceHench2Id = sourceHench2Id, TargetHenchId = targetHenchId, SuccessRate = successRate });
}

public sealed class GetFormulaByIdUseCase(IRepository<Formula> repository)
{
    public Task<Formula?> ExecuteAsync(Guid id) => repository.GetByIdAsync(id, f => f.SourceHench1!, f => f.SourceHench2!, f => f.TargetHench!);
}

public sealed class GetAllFormulasUseCase(IRepository<Formula> repository)
{
    public Task<IEnumerable<Formula>> ExecuteAsync() => repository.GetAllAsync(f => f.SourceHench1!, f => f.SourceHench2!, f => f.TargetHench!);
}

public sealed class UpdateFormulaUseCase(IRepository<Formula> repository)
{
    public async Task<Formula?> ExecuteAsync(Guid id, string name, Guid sourceHench1Id, Guid sourceHench2Id, Guid targetHenchId, double successRate)
    {
        var formula = await repository.GetByIdAsync(id);
        if (formula is null) return null;
        formula.Name = name; formula.SourceHench1Id = sourceHench1Id; formula.SourceHench2Id = sourceHench2Id; formula.TargetHenchId = targetHenchId; formula.SuccessRate = successRate;
        await repository.UpdateAsync(formula);
        return formula;
    }
}

public sealed class DeleteFormulaUseCase(IRepository<Formula> repository)
{
    public async Task<bool> ExecuteAsync(Guid id)
    {
        if (await repository.GetByIdAsync(id) is null) return false;
        await repository.DeleteAsync(id);
        return true;
    }
}
