using seecreativa_backend.Core;
using seecreativa_backend.Prices.Entity;
using System.ComponentModel.DataAnnotations;

namespace seecreativa_backend.Prices.Models {
    public class PriceUpdateDto : UpdateDtoBase<Price> {
        [MinLength(3)]
        [MaxLength(255)]
        public string? Name { get; set; }

        [Range(0, float.MaxValue)]
        public float? MinWeight { get; set; }

        [Range(0, float.MaxValue)]
        public float? MinPrice { get; set; }

        [Range(0, float.MaxValue)]
        public float? Factor { get; set; }

        public override Price ToEntity(Price entity) {
            if (Name != null) entity.Name = Name;
            if (MinWeight != null) entity.MinWeight = (float)MinWeight;
            if (MinPrice != null) entity.MinPrice = (float)MinPrice;
            if (Factor != null) entity.Factor = (float)Factor;
            return entity;
        }
    }
}
