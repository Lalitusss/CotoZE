# Imagen base para producción con runtime ASP.NET (Linux)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Imagen base para build con SDK .NET (Linux)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CotoReservaApi.csproj", "./"]
RUN dotnet restore "./CotoReservaApi.csproj"
COPY . .
RUN dotnet build "./CotoReservaApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publica el proyecto en modo Release
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CotoReservaApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagen final para producción
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Aquí defines la variable de entorno para que la app escuche en todas las interfaces en el puerto que desees (ejemplo 8080)
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "CotoReservaApi.dll"]
