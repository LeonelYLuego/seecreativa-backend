using seecreativa_backend.Orders.Repositories;

namespace seecreativa_backend.Orders {
    public static class OrdersModule {
        public static void AddOrders(this IServiceCollection services) {
            services.AddScoped<IOrdersRepository, OrdersRepository>();
        }
    }
}
