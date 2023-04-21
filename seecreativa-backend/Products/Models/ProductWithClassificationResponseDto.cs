using seecreativa_backend.Classifications.Models;

namespace seecreativa_backend.Products.Models {
    public class ProductWithClassificationResponseDto {
        public required string Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required float Weight { get; set; }
        public float? With { get; set; }
        public float? Length { get; set; }
        public required float Height { get; set; }
        public float? Diameter { get; set; }
        public required List<string> ImageUrls { get; set; }
        public required ClassificationResponseDto Classification { get; set; }
    }
}
