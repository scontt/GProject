using GProject.Application.Repository;
using GProject.Infrastructure.Repository;

namespace GProject;

public static class Registrator
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddSingleton((IConfigurationRoot)configuration)
            .InstallRepositories();
    }

    private static IServiceCollection InstallRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IGameRepository, GameRepository>();
    }
}
