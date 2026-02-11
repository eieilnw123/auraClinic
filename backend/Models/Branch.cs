namespace Backend.Models;

public class Branch
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public Guid TenantId { get; set; }
}