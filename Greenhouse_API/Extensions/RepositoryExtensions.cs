using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Greenhouse_API.Services;

namespace Greenhouse_API.Extensions
{
    // This static class provides a centralized way to register all repository-service 
    // implementations used throughout the application. By grouping the dependency 
    // injection bindings here, we ensure cleaner configuration in Program.cs and 
    // maintain a clear separation of responsibility. Each repository is mapped to its 
    // corresponding service, allowing the application to resolve data access logic 
    // based on the generic IRepository interface.

    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // Fertilizers
            services.AddScoped<IRepository<Fertilizer, int>, FertilizerService>();

            // Observations / Specimens
            services.AddScoped<IRepository<Observation, int>, ObservationService>();
            services.AddScoped<IRepository<Specimen, int>, SpecimenService>();

            // Plants
            services.AddScoped<IRepository<Plant, int>, PlantService>();
            services.AddScoped<IRepository<PlantAlert, int>, PlantAlertService>();
            services.AddScoped<IRepository<PlantHumidityRecord, int>, PlantHumidityRecordService>();
            services.AddScoped<IRepository<Watering, int>, WateringService>();

            // Soil
            services.AddScoped<IRepository<SoilHumidityCategory, int>, SoilHumidityCategoryService>();

            // Sensors
            services.AddScoped<IRepository<Sensor, string>, SensorService>();
            services.AddScoped<IRepository<SensorAlert, int>, SensorAlertService>();

            // Zones
            services.AddScoped<IRepository<Zone, int>, ZoneService>();
            services.AddScoped<IRepository<ZoneRecord, int>, ZoneRecordService>();
            services.AddScoped<IRepository<ZoneAlert, int>, ZoneAlertService>();
            services.AddScoped<IRepository<ZoneCategory, int>, ZoneCategoryService>();
            services.AddScoped<IRepository<ZonePressureRecord, int>, ZonePressureRecordService>();

            return services;
        }
    }
}
