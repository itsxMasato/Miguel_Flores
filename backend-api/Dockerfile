# ETAPA 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Al estar dentro de /backend-api, solo copiamos el csproj que está ahí
COPY *.csproj ./
RUN dotnet restore

# Copiamos el resto del código del backend
COPY . ./
RUN dotnet publish -c Release -o out

# ETAPA 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Render usa el puerto 10000 por defecto
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "prueba.dll"]