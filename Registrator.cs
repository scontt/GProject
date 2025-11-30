using GProject.Application.Auth;
using GProject.Application.Repository;
using GProject.Infrastructure.Auth;
using GProject.Infrastructure.Repository;

namespace GProject;

public static class Registrator
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddSingleton((IConfigurationRoot)configuration)
            .InstallRepositories()
            .InstallServices();
    }

    private static IServiceCollection InstallRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IGameRepository, GameRepository>();
    }

    private static IServiceCollection InstallServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<IJwtSigningService, JwtSigningService>()
            .AddTransient<IAuthService, AuthService>();
    }
}
