using seecreativa_backend.Clients.Repositories;

namespace seecreativa_backend.Clients {
	public static class ClientsModule {
		public static void AddClients(this IServiceCollection services) {
			services.AddScoped<IClientsRepository, ClientsRepository>();
		}
	}
}
