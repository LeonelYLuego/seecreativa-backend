using seecreativa_backend.Core;
using seecreativa_backend.Prices.Models;

namespace seecreativa_backend.Prices.Entity {
    public class Price : EntityBase {
        public required string Name { get; set; }
        public required float MinWeight { get; set; }
        public required float MinPrice { get; set; }
        public required float Factor { get; set; }

        public PriceResponseDto ToResponse() {
            return new PriceResponseDto {
                Id = Id.ToString(),
                Name = Name,
                MinWeight = MinWeight,
                MinPrice = MinPrice,
                Factor = Factor
            };
        }
    }
}
