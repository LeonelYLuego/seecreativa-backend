using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace seecreativa_backend.Utils.Attributes {
    public class ValidateOptionalIdAttribute : ValidationAttribute {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {
            string? id = value as string;
            if(id == null) {
                return ValidationResult.Success;
            }
            if (ObjectId.TryParse(id, out _)) {
                return ValidationResult.Success;
            } else {
                return new ValidationResult("Id must be a MongoDB Id");
            }
        }
    }
}
