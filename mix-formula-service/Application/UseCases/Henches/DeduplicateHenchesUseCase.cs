using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Henches;

public sealed class DeduplicateHenchesUseCase(
    IRepository<Hench> henchRepository,
    IRepository<Formula> formulaRepository,
    ILogger<DeduplicateHenchesUseCase> logger)
{
    public async Task<int> ExecuteAsync()
    {
        logger.LogInformation("Starting hench deduplication");

        var henches = (await henchRepository.GetAllAsync()).ToList();

        var duplicateGroups = henches
            .GroupBy(h => new { h.Name, h.Type})
            .Where(g => g.Count() > 1)
            .ToList();

        if (duplicateGroups.Count == 0)
        {
            logger.LogInformation("No duplicate henches found");
            return 0;
        }

        logger.LogInformation("Found {GroupCount} groups of duplicate henches", duplicateGroups.Count);

        var formulas = (await formulaRepository.GetAllAsync()).ToList();

        int totalRemoved = 0;

        foreach (var group in duplicateGroups)
        {
            var ordered = group.OrderBy(h => h.Id).ToList();
            var keep = ordered.First();
            var duplicates = ordered.Skip(1).ToList();
            var duplicateIds = duplicates.Select(d => d.Id).ToHashSet();

            var affectedFormulas = formulas
                .Where(f => duplicateIds.Contains(f.SourceHench1Id)
                         || duplicateIds.Contains(f.SourceHench2Id)
                         || duplicateIds.Contains(f.TargetHenchId))
                .ToList();

            foreach (var formula in affectedFormulas)
            {
                bool changed = false;

                if (duplicateIds.Contains(formula.SourceHench1Id))
                {
                    formula.SourceHench1Id = keep.Id;
                    changed = true;
                }
                if (duplicateIds.Contains(formula.SourceHench2Id))
                {
                    formula.SourceHench2Id = keep.Id;
                    changed = true;
                }
                if (duplicateIds.Contains(formula.TargetHenchId))
                {
                    formula.TargetHenchId = keep.Id;
                    changed = true;
                }

                if (changed)
                {
                    await formulaRepository.UpdateAsync(formula);
                }
            }

            foreach (var dup in duplicates)
            {
                await henchRepository.DeleteAsync(dup.Id);
                totalRemoved++;
            }

            logger.LogInformation(
                "Deduplicated hench '{Name}' (Type: {Type}, Level: {Level}): kept {KeepId}, removed {Count} duplicates, updated {FormulaCount} formulas",
                keep.Name, keep.Type, keep.Level, keep.Id, duplicates.Count, affectedFormulas.Count);
        }

        logger.LogInformation("Deduplication complete: {TotalRemoved} duplicate henches removed", totalRemoved);
        return totalRemoved;
    }
}
