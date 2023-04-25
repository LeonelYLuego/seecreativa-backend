using MongoDB.Bson;
using seecreativa_backend.Core;
using seecreativa_backend.Orders.Entities;
using seecreativa_backend.Utils.Attributes;
using System.ComponentModel.DataAnnotations;

namespace seecreativa_backend.Orders.Models {
    public class OrderProductDto : CreateDtoBase<OrderProduct> {
        [Required]
        [ValidateId]
        public required string ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public required int Quantity { get; set; }

        public override OrderProduct ToEntity() {
            return new OrderProduct {
                Id = ObjectId.GenerateNewId(),
                ProductId = ProductId,
                Quantity = Quantity,
                Delivered = 0,
                Price = 0
            };
        }
    }
}
