using Greenhouse_Data_MVC.Interfaces;
using Greenhouse_Data_MVC.Services;

namespace Greenhouse_Data_MVC.Extensions
{
    public static class ServiceProxyExtensions
    {
        public static IServiceCollection AddServiceProxies(
            this IServiceCollection services)
        {
            services.AddScoped<IFertilizerServiceProxy, FertilizerServiceProxy>();
            services.AddScoped<IPlantHumidityRecordServiceProxy, PlantHumidityRecordServiceProxy>();
            services.AddScoped<IObservationServiceProxy, ObservationServiceProxy>();
            services.AddScoped<IPlantServiceProxy, PlantServiceProxy>();
            services.AddScoped<IPlantSensorAlertServiceProxy, PlantSensorAlertServiceProxy>();
            services.AddScoped<IWateringServiceProxy, WateringServiceProxy>();
            services.AddScoped<IZonePressureRecordServiceProxy, ZonePressureRecordServiceProxy>();
            services.AddScoped<ISensorServiceProxy, SensorServiceProxy>();
            services.AddScoped<IZoneRecordServiceProxy, ZoneRecordServiceProxy>();
            services.AddScoped<IZoneServiceProxy, ZoneServiceProxy>();
            services.AddScoped<IZoneSensorAlertServiceProxy, ZoneSensorAlertServiceProxy>();

            return services;
        }
    }
}
