FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore ./AuthService.API.csproj
RUN dotnet publish ./AuthService.API.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 5001
ENV ASPNETCORE_URLS=http://+:5001

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AuthService.API.dll"]