using MongoDB.Bson;
using seecreativa_backend.Users.Entities;
using System.ComponentModel.DataAnnotations;

namespace seecreativa_backend.Users.Models
{
    public class UserUpdateDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public required string Username { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public required string Password { get; set; }

        public User ToUser(string id)
        {
            return new User
            {
                Id = ObjectId.Parse(id),
                Username = Username,
                PasswordHash = User.HashPassword(Password),
            };
        }
    }
}
