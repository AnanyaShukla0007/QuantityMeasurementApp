FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore QMAService.API.csproj
RUN dotnet publish QMAService.API.csproj -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
EXPOSE 5002
ENV ASPNETCORE_URLS=http://+:5002
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet","QMAService.API.dll"]