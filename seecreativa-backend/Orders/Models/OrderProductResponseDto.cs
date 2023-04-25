namespace seecreativa_backend.Orders.Models {
    public class OrderProductResponseDto {
        public required string Id { get; set; }
        public required string ProductId { get; set; }
        public required int Quantity { get; set; }
        public required int Delivered { get; set; }
        public required float Price { get; set; }
    }
}
