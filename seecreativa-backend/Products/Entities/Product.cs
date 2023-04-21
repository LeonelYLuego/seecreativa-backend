using seecreativa_backend.Classifications.Models;
using seecreativa_backend.Core;
using seecreativa_backend.Products.Models;

namespace seecreativa_backend.Products.Entities {
    public class Product : EntityBase {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required float Weight { get; set; }
        public float? With { get; set; }
        public float? Length { get; set; }
        public required float Height { get; set; }
        public float? Diameter { get; set; }
        public required string ClassificationId { get; set; }
        public required List<string> ImageUrls { get; set; }

        public ProductResponseDto ToResponse() {
            return new ProductResponseDto {
                Id = Id.ToString(),
                Code = Code,
                Name = Name,
                Weight = Weight,
                With = With,
                Length = Length,
                Height = Height,
                Diameter = Diameter,
                ClassificationId = ClassificationId,
                ImageUrls = ImageUrls
            };
        }

        public ProductWithClassificationResponseDto ToResponseWithClassification(ClassificationResponseDto classification) {
            return new ProductWithClassificationResponseDto {
                Id = Id.ToString(),
                Code = Code,
                Diameter = Diameter,
                Height = Height,
                Length = Length,
                Name = Name,
                Weight = Weight,
                With = With,
                ImageUrls = ImageUrls,
                Classification = classification
            };
        }
    }
}
