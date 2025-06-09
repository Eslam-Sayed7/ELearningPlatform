using Infrastructure.Dtos;

namespace Infrastructure.Data.Models;

public class UpdateUserModel
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
}