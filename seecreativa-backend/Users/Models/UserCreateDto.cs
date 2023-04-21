using MongoDB.Bson;
using seecreativa_backend.Core;
using seecreativa_backend.Users.Entities;
using System.ComponentModel.DataAnnotations;

namespace seecreativa_backend.Users.Models {
    public class UserCreateDto : CreateDtoBase<User> {
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public required string Username { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public required string Password { get; set; }

        [Required]
        [Range(typeof(bool), "false", "true", ErrorMessage = "The IsAdmin field must be bool.")]
        public required bool IsAdmin { get; set; }

        public override User ToEntity() {
            return new User {
                Id = ObjectId.GenerateNewId(),
                Username = Username,
                PasswordHash = User.HashPassword(Password),
                IsAdmin = IsAdmin
            };
        }
    }
}
