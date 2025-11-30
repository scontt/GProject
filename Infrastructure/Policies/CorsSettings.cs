namespace GProject.Infrastructure.Policies;

public sealed class CorsSettings
{
    public string[]? AllowedHosts { get; init; }

    public string[]? AllowedHeaders { get; init; }

    public string[]? AllowedMethods { get; init; }
}
