namespace seecreativa_backend.Prices.Models {
    public class PriceResponseDto {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required float MinWeight { get; set; }
        public required float MinPrice { get; set; }
        public required float Factor { get; set; }
    }
}
