using seecreativa_backend.Core;
using seecreativa_backend.Users.Models;

namespace seecreativa_backend.Users.Entities {
    public class User : EntityBase {
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public bool IsAdmin { get; set; } = false;

        public static string HashPassword(string password) {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool ComparePasswords(string passwordHash, string password) {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

        public UserResponseDto ToResponse() {
            return new UserResponseDto {
                Id = Id.ToString(),
                Username = Username,
                IsAdmin = IsAdmin,
            };
        }
    }
}
