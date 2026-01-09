using Newtonsoft.Json;

namespace GProject.Domain.Dto;

public class GamePatch
{
    [JsonProperty("gameId")]
    public string GameId { get; set; } = null!;
    [JsonProperty("listId")]
    public string ListId { get; set; } = null!;
}