FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/RollerFate.Presentation/RollerFate.Presentation.csproj", "src/RollerFate.Presentation/"]
COPY ["src/RollerFate.Application/RollerFate.Application.csproj", "src/RollerFate.Application/"]
COPY ["src/RollerFate.Domain/RollerFate.Domain.csproj", "src/RollerFate.Domain/"]
COPY ["src/RollerFate.Infrastructure/RollerFate.Infrastructure.csproj", "src/RollerFate.Infrastructure/"]
RUN dotnet restore "src/RollerFate.Presentation/RollerFate.Presentation.csproj"
COPY . .
WORKDIR "/src/src/RollerFate.Presentation"
RUN dotnet build "./RollerFate.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RollerFate.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RollerFate.Presentation.dll"]
