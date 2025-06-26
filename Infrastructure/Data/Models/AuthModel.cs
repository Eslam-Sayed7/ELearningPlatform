using System.Text.Json.Serialization;
using Core.Entities;

namespace Infrastructure.Data;
public class AuthModel {
    public string Message { get; set; }
    public bool IsAuthenticated { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }
    public AppUser User { get; set; }
    public string? Token { get; set; }
    [JsonIgnore]
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

}
