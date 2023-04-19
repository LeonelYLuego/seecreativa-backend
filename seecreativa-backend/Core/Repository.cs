namespace seecreativa_backend.Core
{
    public interface IRepository<T, TCreateDto, TUpdateDto>
    {
        public Task<T> CreateAsync(TCreateDto createDto);
        public Task<IEnumerable<T>> GetAllAsync();

        public Task<T?> GetByIdAsync(string id);

        public Task<T?> UpdateByIdAsync(string id, TUpdateDto updateDto);

        public Task<bool> DeleteByIdAsync(string id);
    }
}
