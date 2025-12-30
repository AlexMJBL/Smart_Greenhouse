using Greenhouse_Ressource_MVC.Interfaces;
using Greenhouse_Ressource_MVC.Services;

namespace Greenhouse_Ressource_MVC.Extensions
{
    public static class ServiceProxyExtensions
    {
        public static IServiceCollection AddServiceProxies(
            this IServiceCollection services)
        {

            services.AddScoped<IFertilizerServiceProxy, FertilizerServiceProxy>();
            services.AddScoped<IObservationServiceProxy, ObservationServiceProxy>();
            services.AddScoped<IPlantServiceProxy, PlantServiceProxy>();
            services.AddScoped<ISpecimenServiceProxy, SpecimenServiceProxy>();
            services.AddScoped<ISoilHumidityCategoryServiceProxy, SoilHumidityCategoryServiceProxy>();
            services.AddScoped<ISensorServiceProxy, SensorServiceProxy>();
            services.AddScoped<IWateringServiceProxy, WateringServiceProxy>();
            services.AddScoped<IZoneServiceProxy, ZoneServiceProxy>();
            services.AddScoped<IZoneCategoryServiceProxy, ZoneCategoryServiceProxy>();

            return services;
        }
    }
}
