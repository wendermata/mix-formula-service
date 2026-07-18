namespace Api.Presentation.GraphQL.Inputs;

public record UpdateItemInput(Guid Id, string Name, string Description);
