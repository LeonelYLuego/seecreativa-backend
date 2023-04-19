using seecreativa_backend.Core;
using seecreativa_backend.Users.Models;

namespace seecreativa_backend.Users.Entities
{
    public class User : EntityBase
    {
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }

        public static string HashPassword(string password)
        {
            return "hashed" + password;
        }

        public UserResponseDto ToResponse()
        {
            return new UserResponseDto
            {
                Id = Id.ToString(),
                Username = Username,
            };
        }
    }
}
