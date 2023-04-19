using Microsoft.Extensions.Options;
using seecreativa_backend.Core;
using seecreativa_backend.Core.MongoDb;
using seecreativa_backend.Users.Entities;
using seecreativa_backend.Users.Models;
using seecreativa_backend.Users.Persistance;

namespace seecreativa_backend.Users.Repositories
{
    public interface IUsersRepository : IRepository<User, UserCreateDto, UserUpdateDto>
    {

    }

    public class UsersRepository : MongoDbRepository<User, UserCreateDto, UserUpdateDto>, IUsersRepository
    {
        public UsersRepository(IOptions<MongoDbSettings> settings) : base(new UsersContext(settings).Users) { }
    }
}
