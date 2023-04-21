using MongoDB.Bson;
using seecreativa_backend.Clients.Entities;
using seecreativa_backend.Core;
using System.ComponentModel.DataAnnotations;

namespace seecreativa_backend.Clients.Models {
	public class ClientCreateDto : CreateDtoBase<Client> {
		[Required]
		[MinLength(3)]
		[MaxLength(255)]
		public required string Name { get; set; }

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

		public override Client ToEntity() {
			return new Client {
				Id = ObjectId.GenerateNewId(),
				Name = Name,
				RFC = RFC,
				Email = Email,
				Address = Address,
				Phone = Phone
			};
		}
	}
}
