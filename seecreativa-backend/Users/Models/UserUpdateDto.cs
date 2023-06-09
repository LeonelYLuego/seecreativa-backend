﻿using seecreativa_backend.Core;
using seecreativa_backend.Users.Entities;
using System.ComponentModel.DataAnnotations;

namespace seecreativa_backend.Users.Models {
    public class UserUpdateDto : UpdateDtoBase<User> {
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public required string Username { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public required string Password { get; set; }

        [Range(typeof(bool), "false", "true", ErrorMessage = "The IsAdmin field must be bool.")]
        public bool? IsAdmin { get; set; }

        public override User ToEntity(User entity) {
            entity.Username = Username;
            entity.PasswordHash = User.HashPassword(Password);
            if (IsAdmin.HasValue) {
                entity.IsAdmin = IsAdmin.Value;
            }
            return entity;
        }
    }
}
