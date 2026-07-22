using System.Linq.Expressions;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Henches;

public sealed class GetAllHenchesUseCase(IRepository<Hench> repository, ILogger<GetAllHenchesUseCase> logger)
{
    public async Task<IEnumerable<Hench>> ExecuteAsync(HenchFilter? filter = null)
    {
        logger.LogInformation("Fetching henches");

        Expression<Func<Hench, bool>>? predicate = null;

        if (filter is not null)
        {
            if (filter.Name is not null)
                predicate = AddCondition(predicate, h => h.Name.Contains(filter.Name));
            if (filter.Type is not null)
                predicate = AddCondition(predicate, h => h.Type == filter.Type);
            if (filter.Level is not null)
                predicate = AddCondition(predicate, h => h.Level == filter.Level);
            if (filter.MinLevel is not null)
                predicate = AddCondition(predicate, h => h.Level >= filter.MinLevel);
            if (filter.MaxLevel is not null)
                predicate = AddCondition(predicate, h => h.Level <= filter.MaxLevel);
        }

        return await repository.GetAllAsync(predicate, h => h.Maps);
    }

    private static Expression<Func<T, bool>> AddCondition<T>(
        Expression<Func<T, bool>>? existing, Expression<Func<T, bool>> condition)
    {
        if (existing is null)
            return condition;

        var parameter = Expression.Parameter(typeof(T));
        var body = Expression.AndAlso(
            Expression.Invoke(existing, parameter),
            Expression.Invoke(condition, parameter));
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }
}
