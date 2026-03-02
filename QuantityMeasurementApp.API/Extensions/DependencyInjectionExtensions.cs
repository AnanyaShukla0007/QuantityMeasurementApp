using Microsoft.Extensions.DependencyInjection;
using QuantityMeasurementApp.Service.Interfaces;
using QuantityMeasurementApp.Service.Implementations;
using QuantityMeasurementApp.Business.Interfaces;
using QuantityMeasurementApp.Business.Implementations;

namespace QuantityMeasurementApp.API.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddQuantityMeasurementServices(this IServiceCollection services)
        {
            // Service Layer
            services.AddScoped<ILengthService, LengthService>();
            services.AddScoped<IWeightService, WeightService>();
            services.AddScoped<IVolumeService, VolumeService>();
            services.AddScoped<ITemperatureService, TemperatureService>();

            // Business Layer
            services.AddScoped<ILengthBusiness, LengthBusiness>();
            services.AddScoped<IWeightBusiness, WeightBusiness>();
            services.AddScoped<IVolumeBusiness, VolumeBusiness>();
            services.AddScoped<ITemperatureBusiness, TemperatureBusiness>();

            return services;
        }
    }
}