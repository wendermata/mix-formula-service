using Domain.Enums;

namespace Api.Presentation.GraphQL.Inputs;

public record CreateHenchInput(string Name, HenchType Type, int Level);
