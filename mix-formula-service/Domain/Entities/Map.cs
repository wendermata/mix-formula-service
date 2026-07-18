namespace Domain.Entities;

public class Map
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Hench> Henches { get; set; } = new();
}
