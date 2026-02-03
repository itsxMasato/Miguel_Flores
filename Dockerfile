# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copiar archivos de proyecto
COPY *.csproj ./
RUN dotnet restore

# Copiar todo el proyecto
COPY . ./

# Publicar en Release
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

# Copiar desde la etapa de build
COPY --from=build /app/publish .

# Exponer puerto
EXPOSE 5000

# Comando para iniciar tu app
ENTRYPOINT ["dotnet", "prueba.dll"]
