using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace seecreativa_backend.Utils.Attributes {
    public class ValidateIdAttribute : ValidationAttribute {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {
            string? id = value as string;
            if (id == null) {
                return new ValidationResult("Id must be a MongoDB Id");
            }
            if (ObjectId.TryParse(id, out _)) {
                return ValidationResult.Success;
            } else {
                return new ValidationResult("Id must be a MongoDB Id");
            }
        }
    }
}
