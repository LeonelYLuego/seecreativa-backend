using MongoDB.Driver;

namespace seecreativa_backend.Core
{
    public interface IRepository<T>
    {
        public Task<T> CreateAsync(T entity);
        public Task<IEnumerable<T>> GetAllAsync();

        public Task<T?> GetByIdAsync(string id);

        public Task<T?> UpdateAsync(T entity);

        public Task<bool> DeleteAsync(string id);
    }
}
