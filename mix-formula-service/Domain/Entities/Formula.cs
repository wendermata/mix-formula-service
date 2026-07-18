namespace Domain.Entities;

public class Formula
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public Guid SourceHench1Id { get; set; }
    public Hench? SourceHench1 { get; set; }

    public Guid SourceHench2Id { get; set; }
    public Hench? SourceHench2 { get; set; }

    public Guid TargetHenchId { get; set; }
    public Hench? TargetHench { get; set; }

    public double SuccessRate { get; set; }
}
