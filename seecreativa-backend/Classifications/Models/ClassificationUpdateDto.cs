using seecreativa_backend.Classifications.Entity;
using seecreativa_backend.Core;
using System.ComponentModel.DataAnnotations;

namespace seecreativa_backend.Classifications.Models {
    public class ClassificationUpdateDto : UpdateDtoBase<Classification> {
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public required string Name { get; set; }

        public override Classification ToEntity(Classification entity) {
            entity.Name = Name;
            return entity;
        }
    }
}
