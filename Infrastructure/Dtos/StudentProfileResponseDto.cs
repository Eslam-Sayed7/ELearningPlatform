using Microsoft.AspNetCore.Http;

namespace Infrastructure.Dtos;

public class StudentProfileResponseDto
{
    public string FirstName { get; set; }    
    public string LastName { get; set; }
    
    public string? ProfilePictureUrl { get; set; }
}