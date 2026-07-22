using Domain.Enums;

namespace Application.UseCases.Henches;

public sealed record HenchFilter
{
    public string? Name { get; init; }
    public HenchType? Type { get; init; }
    public int? Level { get; init; }
    public int? MinLevel { get; init; }
    public int? MaxLevel { get; init; }
}
