using GProject.Domain.Dto;
using GProject.Domain.Entities.Database;
using Mapster;

namespace GProject.Infrastructure.Utility;

public class MapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GameList, GameListDto>()
              .Map(dest => dest.Games, 
                   src => src.Games == null 
                          ? null 
                          : src.Games.Adapt<List<GameDto>>());

        config.Default.PreserveReference(true);
    }
}