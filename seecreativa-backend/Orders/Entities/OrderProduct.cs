using seecreativa_backend.Core;
using seecreativa_backend.Orders.Models;
using seecreativa_backend.Prices.Entity;
using seecreativa_backend.Products.Entities;

namespace seecreativa_backend.Orders.Entities {
    public class OrderProduct : EntityBase {
        public required string ProductId { get; set; }
        public required int Quantity { get; set; }
        public required int Delivered { get; set; }
        public required float Price { get; set; }

        public static float GetPrice(Price price, Product product) {
            if (product.Weight <= price.MinWeight)
                return price.MinPrice;
            else
                return (float)Math.Round(product.Weight * price.Factor, 2);
        }

        public OrderProductResponseDto ToResponse() {
            return new OrderProductResponseDto {
                Id = Id.ToString(),
                ProductId = ProductId,
                Quantity = Quantity,
                Delivered = Delivered,
                Price = Price
            };
        }
    }
}
