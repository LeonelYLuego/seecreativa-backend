using Microsoft.Extensions.Options;
using MongoDB.Driver;
using seecreativa_backend.Core.MongoDb;
using seecreativa_backend.Users.Entities;

namespace seecreativa_backend.Users.Persistance
{
    public class UsersContext : MongoDbContext<User>
    {
        public UsersContext(IOptions<MongoDbSettings> settings) : base(settings) { }

        public IMongoCollection<User> Users => GetCollection<User>("users");
    }
}
