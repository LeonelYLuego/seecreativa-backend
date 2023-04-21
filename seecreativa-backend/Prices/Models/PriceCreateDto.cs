using MongoDB.Bson;
using seecreativa_backend.Core;
using seecreativa_backend.Prices.Entity;
using System.ComponentModel.DataAnnotations;

namespace seecreativa_backend.Prices.Models {
    public class PriceCreateDto : CreateDtoBase<Price> {
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public required string Name { get; set; }

        [Required]
        [Range(0, float.MaxValue)]
        public required float MinWeight { get; set; }

        [Required]
        [Range(0, float.MaxValue)]
        public required float MinPrice { get; set; }

        [Required]
        [Range(0, float.MaxValue)]
        public required float Factor { get; set; }

        public override Price ToEntity() {
            return new Price {
                Id = ObjectId.GenerateNewId(),
                Name = Name,
                MinWeight = MinWeight,
                MinPrice = MinPrice,
                Factor = Factor
            };
        }
    }
}
