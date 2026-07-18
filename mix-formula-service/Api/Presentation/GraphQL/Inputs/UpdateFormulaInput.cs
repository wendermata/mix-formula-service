namespace Api.Presentation.GraphQL.Inputs;

public record UpdateFormulaInput(Guid Id, string Name, Guid SourceHench1Id, Guid SourceHench2Id, Guid TargetHenchId, double SuccessRate);
