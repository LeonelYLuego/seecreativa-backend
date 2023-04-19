using System.ComponentModel.DataAnnotations;

namespace seecreativa_backend.Users.Models
{
    public class AuthLogInDto
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
