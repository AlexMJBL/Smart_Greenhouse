using Greenhouse_Data_MVC.Interfaces;
using Greenhouse_Data_MVC.Services;

namespace Greenhouse_Data_MVC.Extensions
{
    public static class ServiceProxyExtensions
    {
        public static IServiceCollection AddServiceProxies(
            this IServiceCollection services)
        {

            services.AddHttpClient<IPlantHumidityRecordServiceProxy, PlantHumidityRecordServiceProxy>();
            services.AddHttpClient<IObservationServiceProxy, ObservationServiceProxy>();
            services.AddHttpClient<IPlantServiceProxy, PlantServiceProxy>();
            services.AddHttpClient<IPlantSensorAlertServiceProxy, PlantSensorAlertServiceProxy>();
            services.AddHttpClient<IZonePressureRecordServiceProxy, ZonePressureRecordServiceProxy>();
            services.AddHttpClient<ISensorServiceProxy, SensorServiceProxy>();
            services.AddHttpClient<IZoneRecordServiceProxy, ZoneRecordServiceProxy>();
            services.AddHttpClient<IZoneServiceProxy, ZoneServiceProxy>();
            services.AddHttpClient<IZoneSensorAlertServiceProxy, ZoneSensorAlertServiceProxy>();

            return services;
        }
    }
}
