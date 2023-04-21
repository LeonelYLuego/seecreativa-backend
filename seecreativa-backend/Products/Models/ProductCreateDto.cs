using MongoDB.Bson;
using seecreativa_backend.Core;
using seecreativa_backend.Products.Entities;
using seecreativa_backend.Utils.Attributes;
using System.ComponentModel.DataAnnotations;

namespace seecreativa_backend.Products.Models {
    public class ProductCreateDto : CreateDtoBase<Product> {
        [Required]
        [RegularExpression(@"^[A-Z]{1,2}\d{1,3}$", ErrorMessage = "Invalid Code")]
        public required string Code { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public required string Name { get; set; }

        [Required]
        [Range(0, float.MaxValue)]
        public required float Weight { get; set; }

        [Range(0, float.MaxValue)]
        public float? With { get; set; }

        [Range(0, float.MaxValue)]
        public float? Length { get; set; }

        [Required]
        [Range(0, float.MaxValue)]
        public required float Height { get; set; }

        [Range(0, float.MaxValue)]
        public float? Diameter { get; set; }

        [Required]
        [ValidateId]
        public required string ClassificationId { get; set; }

        public override Product ToEntity() {
            return new Product {
                Id = ObjectId.GenerateNewId(),
                Name = Name,
                Code = Code,
                Weight = Weight,
                With = With,
                Length = Length,
                Height = Height,
                Diameter = Diameter,
                ImageUrls = new List<string>(),
                ClassificationId = ClassificationId
            };
        }
    }
}
