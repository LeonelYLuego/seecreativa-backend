using seecreativa_backend.Core;
using seecreativa_backend.Orders.Models;

namespace seecreativa_backend.Orders.Entities {
    public class Order : EntityBase {
        public required string ClientId { get; set; }
        public required string PriceId { get; set; }
        public required DateTime CreatedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public required List<OrderProduct> Products { get; set; }

        public OrderResponseDto ToResponse() {
            return new OrderResponseDto {
                Id = Id.ToString(),
                ClientId = ClientId,
                PriceId = PriceId,
                CreatedAt = CreatedAt,
                DeliveredAt = DeliveredAt,
                Products = Products.Select(product => product.ToResponse()).ToList(),
            };
        }
    }
}
