using Greenhouse_API.Data;
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
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Repository générique
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // Services métier
            services.AddScoped<IFertilizerService, FertilizerService>();
            services.AddScoped<IObservationService, ObservationService>();
            services.AddScoped<ISpecimenService, SpecimenService>();

            services.AddScoped<IPlantService, PlantService>();
            services.AddScoped<IPlantSensorAlertService, PlantAlertService>();
            services.AddScoped<IPlantHumidityRecordService, PlantHumidityRecordService>();
            services.AddScoped<IWateringService, WateringService>();

            services.AddScoped<ISoilHumidityCategoryService, SoilHumidityCategoryService>();

            services.AddScoped<ISensorService, SensorService>();
            services.AddScoped<ISensorAlertService, SensorAlertService>();

            services.AddScoped<IZoneService, ZoneService>();
            services.AddScoped<IZoneRecordService, ZoneRecordService>();
            services.AddScoped<IZoneAlertService, ZoneSensorAlertService>();
            services.AddScoped<IZoneCategoryService, ZoneCategoryService>();
            services.AddScoped<IZonePressureRecordService, ZonePressureRecordService>();

            return services;
        }
    }
