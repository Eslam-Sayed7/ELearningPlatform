using Microsoft.AspNetCore.Identity;

namespace Core.Entities;
public class AppUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    // public string UserName { get; set; }
    
    public string? ProfilePicture { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? RefreshToken { get; set; }
 
    public DateTime? RefreshTokenExpiryTime { get; set; }
}

    
