using seecreativa_backend.Clients.Entities;
using seecreativa_backend.Core;
using System.ComponentModel.DataAnnotations;

namespace seecreativa_backend.Clients.Models {
    public class ClientUpdateDto : UpdateDtoBase<Client> {
        [MinLength(3)]
        [MaxLength(255)]
        public string? Name { get; set; }

        [RegularExpression(@"^([A-ZÑ&]{3,4}) ?(?:- ?)?(\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])) ?(?:- ?)?([A-Z\d]{2})([A\d])$")]
        public string? RFC { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [MinLength(3)]
        [MaxLength(255)]
        public string? Address { get; set; }

        [MinLength(10)]
        [MaxLength(10)]
        [RegularExpression(@"^\d*$")]
        public string? Phone { get; set; }

        public override Client ToEntity(Client entity) {
            if (Name != null) entity.Name = Name;

            entity.RFC = RFC;
            entity.Email = Email;
            entity.Address = Address;
            entity.Phone = Phone;

            return entity;
        }
    }
}
