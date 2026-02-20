FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["tests/RollerFate.UnitTests/RollerFate.UnitTests.csproj", "tests/RollerFate.UnitTests/"]
RUN dotnet restore "tests/RollerFate.UnitTests/RollerFate.UnitTests.csproj"
COPY . .
WORKDIR "/src/tests/RollerFate.UnitTests"
RUN dotnet build "./RollerFate.UnitTests.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RollerFate.UnitTests.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RollerFate.UnitTests.dll"]
