using Microsoft.Extensions.Options;
using seecreativa_backend.Classifications.Entity;
using seecreativa_backend.Classifications.Models;
using seecreativa_backend.Classifications.Persistance;
using seecreativa_backend.Core;
using seecreativa_backend.Core.MongoDb;

namespace seecreativa_backend.Classifications.Repositories {
    public interface IClassificationsRepository : IRepository<Classification, ClassificationCreateDto, ClassificationUpdateDto> {

    }
    public class ClassificationsRepository : MongoDbRepository<Classification, ClassificationCreateDto, ClassificationUpdateDto>, IClassificationsRepository {
        public ClassificationsRepository(IOptions<MongoDbSettings> settings) : base(new ClassificationsContext(settings).Classifications) { }
    }
}
