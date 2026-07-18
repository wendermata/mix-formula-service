namespace Api.Presentation.GraphQL.Inputs;

public record UpdateMapInput(Guid Id, string Name, List<Guid> HenchIds);
