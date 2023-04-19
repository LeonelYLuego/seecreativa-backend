using Microsoft.Extensions.Options;
using MongoDB.Driver;
using seecreativa_backend.Core;
using seecreativa_backend.Core.MongoDb;
using seecreativa_backend.Users.Entities;
using seecreativa_backend.Users.Models;
using seecreativa_backend.Users.Persistance;

namespace seecreativa_backend.Users.Repositories
{
    public interface IUsersRepository : IRepository<User, UserCreateDto, UserUpdateDto>
    {
        public Task<User?> GetByUsername(string username);
    }

    public class UsersRepository : MongoDbRepository<User, UserCreateDto, UserUpdateDto>, IUsersRepository
    {
        public UsersRepository(IOptions<MongoDbSettings> settings) : base(new UsersContext(settings).Users) { }

        public async Task<User?> GetByUsername(string username)
        {
            var entity = await _collection.Find(x => x.Username == username).FirstOrDefaultAsync();
            if (entity == null) return null;
            return entity;
        }
    }
}
