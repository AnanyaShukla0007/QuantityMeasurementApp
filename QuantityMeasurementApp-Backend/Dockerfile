FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY QuantityMeasurementApp.Model/QuantityMeasurementApp.Model.csproj QuantityMeasurementApp.Model/
COPY QuantityMeasurementApp.Business/QuantityMeasurementApp.Business.csproj QuantityMeasurementApp.Business/
COPY QuantityMeasurementApp.Repository/QuantityMeasurementApp.Repository.csproj QuantityMeasurementApp.Repository/
COPY QuantityMeasurementApp.API/QuantityMeasurementApp.API.csproj QuantityMeasurementApp.API/

RUN dotnet restore QuantityMeasurementApp.API/QuantityMeasurementApp.API.csproj

COPY . .

RUN dotnet publish QuantityMeasurementApp.API/QuantityMeasurementApp.API.csproj \
    -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "QuantityMeasurementApp.API.dll"]
