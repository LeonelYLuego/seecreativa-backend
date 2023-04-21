using MongoDB.Bson;
using seecreativa_backend.Classifications.Entity;
using seecreativa_backend.Core;
using System.ComponentModel.DataAnnotations;

namespace seecreativa_backend.Classifications.Models {
    public class ClassificationCreateDto : CreateDtoBase<Classification> {
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public required string Name { get; set; }

        public override Classification ToEntity() {
            return new Classification {
                Id = ObjectId.GenerateNewId(),
                Name = Name
            };
        }
    }
}
