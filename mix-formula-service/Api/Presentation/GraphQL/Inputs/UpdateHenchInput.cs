using Domain.Enums;

namespace Api.Presentation.GraphQL.Inputs;

public record UpdateHenchInput(Guid Id, string Name, HenchType Type, int Level);
