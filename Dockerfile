# Etapa 1: Build (Compilación)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivos de proyecto para restaurar dependencias
# Se usa ./ para referirse al Root Directory que definas en Render
COPY *.csproj ./
RUN dotnet restore

# Copiar el resto de los archivos del backend
COPY . ./

# Publicar el proyecto en modo Release
# Asegúrate de que el nombre del proyecto sea correcto
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Runtime (Ejecución)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copiar los archivos publicados desde la etapa anterior
COPY --from=build /app/publish .

# CONFIGURACIÓN PARA RENDER:
# Render asigna un puerto dinámico en la variable $PORT. 
# ASP.NET Core necesita esta variable para saber dónde escuchar.
ENV ASPNETCORE_URLS=http://+:10000

# Exponer el puerto (informativo para Render)
EXPOSE 10000

# Comando para iniciar la aplicación
# IMPORTANTE: Cambia "prueba.dll" por el nombre real de tu proyecto si es distinto
ENTRYPOINT ["dotnet", "prueba.dll"]