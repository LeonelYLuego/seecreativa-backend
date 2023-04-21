using Microsoft.Extensions.Options;
using MongoDB.Driver;
using seecreativa_backend.Clients.Entities;
using seecreativa_backend.Core.MongoDb;

namespace seecreativa_backend.Clients.Persistance {
	public class ClientsContext : MongoDbContext<Client> {
		public ClientsContext(IOptions<MongoDbSettings> settings) : base(settings) { }

		public IMongoCollection<Client> Clients => GetCollection("clients");
	}
}
