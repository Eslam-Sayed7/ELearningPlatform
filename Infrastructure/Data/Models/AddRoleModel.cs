using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Data;
public class AddRoleModel
{
        [Required]
        public Guid  UserId { get; set; }

        [Required]
        public string Role { get; set; }

}