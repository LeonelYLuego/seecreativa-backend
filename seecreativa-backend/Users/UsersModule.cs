using seecreativa_backend.Users.Repositories;

namespace seecreativa_backend.Users
{
    public static class UsersModule
    {
        public static void AddUsers(this IServiceCollection services)
        {
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddScoped<AuthRepository>();
            services.AddScoped<IAuthRepository>(provider => provider.GetService<AuthRepository>()!);
        }
    }
}
