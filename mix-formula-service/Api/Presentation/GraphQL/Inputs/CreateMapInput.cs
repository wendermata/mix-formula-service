namespace Api.Presentation.GraphQL.Inputs;

public record CreateMapInput(string Name, List<Guid> HenchIds);
