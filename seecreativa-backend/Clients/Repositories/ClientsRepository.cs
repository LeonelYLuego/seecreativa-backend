using Microsoft.Extensions.Options;
using seecreativa_backend.Clients.Entities;
using seecreativa_backend.Clients.Models;
using seecreativa_backend.Clients.Persistance;
using seecreativa_backend.Core;
using seecreativa_backend.Core.MongoDb;

namespace seecreativa_backend.Clients.Repositories {
	public interface IClientsRepository : IRepository<Client, ClientCreateDto, ClientUpdateDto> {

	}

	public class ClientsRepository : MongoDbRepository<Client, ClientCreateDto, ClientUpdateDto>, IClientsRepository {
		public ClientsRepository(IOptions<MongoDbSettings> settings) : base(new ClientsContext(settings).Clients) { }
	}
}
