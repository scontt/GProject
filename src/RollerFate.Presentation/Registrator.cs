using RollerFate.Application.Abstractions.Auth;
using RollerFate.Application.Abstractions.Repository;
using RollerFate.Infrastructure.Auth;
using RollerFate.Infrastructure.Extra;
using RollerFate.Infrastructure.Repository;

namespace RollerFate.Presentation;

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
            .AddScoped<IGameRepository, GameRepository>()
            .AddScoped<IGameListRepository, GameListRepository>()
            .AddScoped<IGamesGameListRepository, GamesGameListRepository>();
    }

    private static IServiceCollection InstallServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<IJwtSigningService, JwtSigningService>()
            .AddTransient<IAuthService, AuthService>()
            .AddHostedService<SteamUpdateService>();
    }
}
