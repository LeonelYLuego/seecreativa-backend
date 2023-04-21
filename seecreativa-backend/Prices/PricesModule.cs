using seecreativa_backend.Prices.Repositories;

namespace seecreativa_backend.Prices {
    public static class PricesModule {
        public static void AddPrices(this IServiceCollection services) {
            services.AddScoped<IPricesRepository, PricesRepository>();
        }
    }
}
