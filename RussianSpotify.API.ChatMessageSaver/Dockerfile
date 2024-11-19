FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RussianSpotify.API.ChatMessageSaver/RussianSpotify.API.ChatMessageSaver.csproj", "RussianSpotify.API.ChatMessageSaver/"]
COPY ["RussianSpotify.API.Core/RussianSpotify.API.Core.csproj", "RussianSpotify.API.Core/"]
COPY ["RussianSpotify.API.Client/RussianSpotify.API.Client.csproj", "RussianSpotify.API.Client/"]
COPY ["RussianSpotify.API.Contracts/RussianSpotify.API.Contracts.csproj", "RussianSpotify.API.Contracts/"]
COPY ["RussianSpotify.API.Shared/RussianSpotify.API.Shared.csproj", "RussianSpotify.API.Shared/"]
RUN dotnet restore "RussianSpotify.API.ChatMessageSaver/RussianSpotify.API.ChatMessageSaver.csproj"
COPY . .
WORKDIR "/src/RussianSpotify.API.ChatMessageSaver"
RUN dotnet build "RussianSpotify.API.ChatMessageSaver.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RussianSpotify.API.ChatMessageSaver.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RussianSpotify.API.ChatMessageSaver.dll"]
