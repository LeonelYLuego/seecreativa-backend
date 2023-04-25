namespace seecreativa_backend.Orders.Models {
    public class OrderResponseDto {
        public required string Id { get; set; }
        public required string ClientId { get; set; }
        public required string PriceId { get; set; }
        public required DateTime CreatedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public required List<OrderProductResponseDto> Products { get; set; }
    }
}
