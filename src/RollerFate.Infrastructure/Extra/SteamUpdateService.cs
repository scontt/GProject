using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RollerFate.Application.Repository;
using RollerFate.Domain.Entities.Database;
using RollerFate.Domain.Steam;

namespace RollerFate.Infrastructure.Extra;

public class SteamUpdateService : BackgroundService
{
    public SteamUpdateService(IServiceProvider provider)
    {
        serviceProvider = provider;
    }
    private readonly IServiceProvider serviceProvider;
    private readonly string key = "6D95FAF9A3B77A8F224B12AC2B873184";
    private readonly List<App> apps = [];
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await GetSteamStoreGamesAsync();
        await UpdateDatabaseGames();

        PeriodicTimer timer = new(TimeSpan.FromHours(12));

        while(await timer.WaitForNextTickAsync())
        {    
            await GetSteamStoreGamesAsync();
            await UpdateDatabaseGames();
        }
    }

    private async Task GetSteamStoreGamesAsync()
    {
        int lastAppId = 0;
        do
        {
            RestClient client = new("https://api.steampowered.com/");
            RestRequest request = new($"IStoreService/GetAppList/v1?key={key}&max_results=20");

            var response = await client.GetAsync(request);

            if (!string.IsNullOrEmpty(response.Content) && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var parsedResponse = JObject.Parse(response.Content);
                apps.AddRange(JsonConvert.DeserializeObject<List<App>>(parsedResponse["response"]!["apps"]!.ToString())!);
                lastAppId = apps.Last().AppId;
            }
        } while(apps.Count % 50000 == 0);
    }

    private async Task UpdateDatabaseGames()
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var gameRepository = scopedServices.GetRequiredService<IGameRepository>();

            List<Game> games = [.. await gameRepository.GetAllAsync()];

            foreach (var item in apps)
            {
                if (!games.Any(x => x.Name == item.Name))
                {
                    await gameRepository.AddAsync(new()
                    {
                        Id = item.AppId,
                        Name = item.Name,
                    });
                }
            }
        }
    }
}
