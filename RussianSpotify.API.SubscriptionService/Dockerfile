FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5113
EXPOSE 5114

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RussianSpotify.API.SubscriptionService/RussianSpotify.API.SubscriptionService.csproj", "RussianSpotify.API.SubscriptionService/"]
COPY ["RussianSpotify.API.Grpc/RussianSpotify.API.Grpc.csproj", "RussianSpotify.API.Grpc/"]
COPY ["RussianSpotify.API.Shared/RussianSpotify.API.Shared.csproj", "RussianSpotify.API.Shared/"]
RUN dotnet restore "RussianSpotify.API.SubscriptionService/RussianSpotify.API.SubscriptionService.csproj"
COPY . .
WORKDIR "/src/RussianSpotify.API.SubscriptionService"
RUN dotnet build "RussianSpotify.API.SubscriptionService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RussianSpotify.API.SubscriptionService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RussianSpotify.API.SubscriptionService.dll"]
