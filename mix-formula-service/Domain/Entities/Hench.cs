using Domain.Enums;

namespace Domain.Entities;

public class Hench
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public HenchType Type { get; set; }
    public int Level { get; set; }

    public List<Map> Maps { get; set; } = new();
}
