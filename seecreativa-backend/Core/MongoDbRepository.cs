using MongoDB.Bson;
using MongoDB.Driver;

namespace seecreativa_backend.Core
{
    public class MongoDbRepository<T> : IRepository<T> where T : EntityBase
    {
        protected readonly IMongoCollection<T> _collection;

        public MongoDbRepository(IMongoCollection<T> collection)
        {
            _collection = collection;
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return (await GetByIdAsync(entity.Id.ToString()))!;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(x => true).ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(string id)
        {
            var entity = await _collection.Find(x => x.Id == GetObjectId(id)).FirstOrDefaultAsync();
            if (entity == null) return null;
            return entity;
        }

        public virtual async Task<T?> UpdateAsync(T entity)
        {
            var foundEntity = await _collection.Find(x => x.Id == GetObjectId(entity.Id.ToString())).FirstOrDefaultAsync();
            if (foundEntity == null) return null;
            var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
            await _collection.ReplaceOneAsync(filter, entity);
            return await GetByIdAsync(entity.Id.ToString());
        }

        public virtual async Task<bool> DeleteAsync(string id)
        {
            var entity = await _collection.Find(x => x.Id == GetObjectId(id)).FirstOrDefaultAsync();
            if (entity == null) return false;
            var filter = Builders<T>.Filter.Eq(x => x.Id, GetObjectId(id));
            var result = await _collection.DeleteOneAsync(filter);
            if (result == null || result.DeletedCount == 0) return false;
            return true;
        }

        protected static ObjectId GetObjectId(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId internalId))
                internalId = ObjectId.Empty;
            return internalId;
        }
    }
}
