using seecreativa_backend.Products.Repositories;

namespace seecreativa_backend.Products {
    public static class ProductsModule {
        public static void AddProducts(this IServiceCollection services) {
            services.AddScoped<IProductsRepository, ProductsRepository>();
        }
    }
}
