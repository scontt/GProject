using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RollerFate.Application.Abstractions.Repository;
using RollerFate.Domain.Dto;

namespace RollerFate.Infrastructure.Utility
{
    public class SteamService(IGameRepository gameRepository)
    {
        private readonly string key = "6D95FAF9A3B77A8F224B12AC2B873184";
        private readonly string baseUrl = "https://api.steampowered.com";
        private readonly IGameRepository gameRepository = gameRepository;
        private readonly RestClient client = new("https://api.steampowered.com");

        public void UpdateGames()
        {
            int gamesCount = 50000;
            RestRequest request = new($"IStoreService/GetAppList/v1?key=6D95FAF9A3B77A8F224B12AC2B873184&max_results={gamesCount}&include_dlc=true");
            var response = client.Get(request);
            var parsedResponse = JObject.Parse(response.Content.ToString()!);

            var games = JsonConvert.DeserializeObject<List<GameDto>>(parsedResponse["response"]["apps"].ToString());

            Console.WriteLine(games.Count);
        
            Console.WriteLine($"Id: {games.Last().Id} Name: {games.Last().Name}");

            while (gamesCount % 50000 == 0)
            {
                request = new($"IStoreService/GetAppList/v1?key=6D95FAF9A3B77A8F224B12AC2B873184&max_results={games.Last().Id}&last_appid={games.Last().Id}");
                response = client.Get(request);

                parsedResponse = JObject.Parse(response.Content.ToString()!);
                games = JsonConvert.DeserializeObject<List<GameDto>>(parsedResponse["response"]["apps"].ToString());

                Console.WriteLine($"Id: {games.Last().Id} Name: {games.Last().Name}");

                gamesCount += games.Count;
            }
        }
    }
}