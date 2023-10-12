# Bruk et offisielt .NET 7.0 SDK-bilde som byggeimage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Kopier csproj og gjenopprett avhengigheter
COPY ["Nøsted.csproj", "Nøsted/"]
RUN dotnet restore "Nøsted/Nøsted.csproj"

# Kopier resten av prosjektet og bygg
COPY . .
WORKDIR "/src/Nøsted"
RUN dotnet build "Nøsted.csproj" -c Release -o /app/build

# Bygg en produksjonsklar image
FROM build AS publish
RUN dotnet publish "Nøsted.csproj" -c Release -o /app/publish

# Bruk et ASP.NET Core runtime-bilde som baseimage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nøsted.dll"]
