// ไฟล์: Models/User.cs
namespace Backend.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty; // Admin, User, Viewer
    public Guid TenantId { get; set; }
}