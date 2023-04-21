using seecreativa_backend.Classifications.Models;
using seecreativa_backend.Core;

namespace seecreativa_backend.Classifications.Entity {
    public class Classification : EntityBase {
        public required string Name { get; set; }

        public ClassificationResponseDto ToResponse() {
            return new ClassificationResponseDto {
                Id = Id.ToString(),
                Name = Name
            };
        }
    }
}
