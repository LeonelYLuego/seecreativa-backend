using Microsoft.Extensions.Options;
using seecreativa_backend.Core;
using seecreativa_backend.Users.Entities;
using seecreativa_backend.Users.Persistance;

namespace seecreativa_backend.Users.Repositories
{
    public interface IUsersRepository : IRepository<User>
    {

    }
    public class UsersRepository : MongoDbRepository<User>, IUsersRepository
    {
        public UsersRepository(IOptions<MongoDbSettings> settings) : base(new UsersContext(settings).Users) { }
    }
}
