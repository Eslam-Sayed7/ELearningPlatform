using Microsoft.AspNetCore.Http;

namespace Infrastructure.Data.Models;

public class UpdateProfilePictureModel
{
    public Guid Id { get; set; }
    public IFormFile PictureFile { get; set; }
    
}