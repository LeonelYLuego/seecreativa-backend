using seecreativa_backend.Clients.Models;
using seecreativa_backend.Core;

namespace seecreativa_backend.Clients.Entities {
    public class Client : EntityBase {
        public required string Name { get; set; }
        public string? RFC { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }

        public ClientResponseDto ToResponse() {
            return new ClientResponseDto {
                Id = Id.ToString(),
                Name = Name,
                RFC = RFC,
                Email = Email,
                Address = Address,
                Phone = Phone,
            };
        }
    }
}
