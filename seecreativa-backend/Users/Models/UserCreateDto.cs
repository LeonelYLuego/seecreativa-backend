using MongoDB.Bson;
using seecreativa_backend.Users.Entities;
using System.ComponentModel.DataAnnotations;

namespace seecreativa_backend.Users.Models
{
    public class UserCreateDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public required string Username { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public required string Password { get; set; }

        public User ToUser()
        {
            return new User
            {
                Id = ObjectId.GenerateNewId(),
                Username = Username,
                PasswordHash = User.HashPassword(Password),
            };
        }
    }
}
