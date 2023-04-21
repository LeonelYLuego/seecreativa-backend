using seecreativa_backend.Classifications.Repositories;

namespace seecreativa_backend.Classifications {
    public static class ClassificationsModule {
        public static void AddClassifications(this IServiceCollection services) {
            services.AddScoped<IClassificationsRepository, ClassificationsRepository>();
        }
    }
}
