using seecreativa_backend.Core;
using seecreativa_backend.Products.Entities;
using seecreativa_backend.Utils.Attributes;
using System.ComponentModel.DataAnnotations;

namespace seecreativa_backend.Products.Models {
    public class ProductUpdateDto : UpdateDtoBase<Product> {
        [RegularExpression(@"^[A-Z]{1,2}\d{1,3}$", ErrorMessage = "Invalid Code")]
        public string? Code { get; set; }

        [MinLength(3)]
        [MaxLength(255)]
        public string? Name { get; set; }

        [Range(0, float.MaxValue)]
        public float? Weight { get; set; }

        [Range(0, float.MaxValue)]
        public float? Width { get; set; }

        [Range(0, float.MaxValue)]
        public float? Length { get; set; }

        [Range(0, float.MaxValue)]
        public float? Height { get; set; }

        [Range(0, float.MaxValue)]
        public float? Diameter { get; set; }

        [ValidateOptionalId]
        public string? ClassificationId { get; set; }

        public override Product ToEntity(Product entity) {
            if (Code != null) entity.Code = Code;
            if (Name != null) entity.Name = Name;
            if (Weight != null) entity.Weight = (float)Weight;
            if (Height != null) entity.Height = (float)Height;
            if (ClassificationId != null) entity.ClassificationId = ClassificationId;

            entity.Width = Width;
            entity.Length = Length;
            entity.Diameter = Diameter;

            return entity;
        }
    }
}
