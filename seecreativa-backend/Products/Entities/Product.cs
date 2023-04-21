using seecreativa_backend.Core;

namespace seecreativa_backend.Products.Entities {
    public class Product : EntityBase {
        public required string Code { get; set; }
        public required float Weight { get; set; }
        public float? With { get; set; }
        public float? Length { get; set; }
        public required float Height { get; set; }
        public float? Diameter { get; set; }
        public required string ClassificationId { get; set; }
    }
}
