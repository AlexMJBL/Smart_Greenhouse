using Greenhouse_Ressource_MVC.Interfaces;
using Greenhouse_Ressource_MVC.Services;

namespace Greenhouse_Ressource_MVC.Extensions
{
    public static class ServiceProxyExtensions
    {
        public static IServiceCollection AddServiceProxies(
            this IServiceCollection services)
        {

            services.AddHttpClient<IFertilizerServiceProxy, FertilizerServiceProxy>();
            services.AddHttpClient<IObservationServiceProxy, ObservationServiceProxy>();
            services.AddHttpClient<IPlantServiceProxy, PlantServiceProxy>();
            services.AddHttpClient<ISpecimenServiceProxy, SpecimenServiceProxy>();
            services.AddHttpClient<ISoilHumidityCategoryServiceProxy, SoilHumidityCategoryServiceProxy>();
            services.AddHttpClient<ISensorServiceProxy, SensorServiceProxy>();
            services.AddHttpClient<IWateringServiceProxy, WateringServiceProxy>();
            services.AddHttpClient<IZoneServiceProxy, ZoneServiceProxy>();
            services.AddHttpClient<IZoneCategoryServiceProxy, ZoneCategoryServiceProxy>();

            return services;
        }
    }
}
