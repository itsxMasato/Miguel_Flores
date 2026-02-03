FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY backend-api/backend-api.csproj backend-api/
RUN dotnet restore backend-api/backend-api.csproj

COPY . .
RUN dotnet publish backend-api/backend-api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:$PORT

ENTRYPOINT ["dotnet", "backend-api.dll"]
