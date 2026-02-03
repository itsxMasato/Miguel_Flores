# -------- BUILD --------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# limpiar cualquier sdk previo
RUN dotnet --info

COPY backend-api.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish backend-api.csproj -c Release -o /app/publish

# -------- RUNTIME --------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:$PORT

ENTRYPOINT ["dotnet", "backend-api.dll"]
