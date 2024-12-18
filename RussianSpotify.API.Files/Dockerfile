FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RussianSpotify.API.Files/RussianSpotify.API.Files.csproj", "RussianSpotify.API.Files/"]
COPY ["RussianSpotify.API.Shared/RussianSpotify.API.Shared.csproj", "RussianSpotify.API.Shared/"]
COPY ["RussianSpotify.API.Grpc/RussianSpotify.API.Grpc.csproj", "RussianSpotify.API.Grpc/"]
RUN dotnet restore "RussianSpotify.API.Files/RussianSpotify.API.Files.csproj"
COPY . .
WORKDIR "/src/RussianSpotify.API.Files"
RUN dotnet build "RussianSpotify.API.Files.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RussianSpotify.API.Files.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RussianSpotify.API.Files.dll"]
