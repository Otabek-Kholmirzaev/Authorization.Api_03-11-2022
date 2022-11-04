using System.ComponentModel.DataAnnotations;

namespace Authorization.Api.Models
{
    public class CreateUserModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Roles { get; set; }
    }
}
