using MongoDB.Bson;
using seecreativa_backend.Core;
using seecreativa_backend.Orders.Entities;
using seecreativa_backend.Utils.Attributes;
using System.ComponentModel.DataAnnotations;

namespace seecreativa_backend.Orders.Models {
    public class OrderCreateDto : CreateDtoBase<Order> {
        [Required]
        [ValidateId]
        public required string ClientId { get; set; }

        [Required]
        [ValidateId]
        public required string PriceId { get; set; }

        [Required]
        public required List<OrderProductDto> Products { get; set; }

        public override Order ToEntity() {
            return new Order {
                Id = ObjectId.GenerateNewId(),
                ClientId = ClientId,
                PriceId = PriceId,
                CreatedAt = DateTime.Now,
                Products = Products.Select(product => product.ToEntity()).ToList(),
            };
        }
    }
}
