using Greenhouse_API.DTOs;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class PlantHumidityRecordService : IPlantHumidityService
    {
        private readonly IRepository<PlantHumidityRecord> _repository;
        private readonly ILogger<PlantHumidityRecordService> _logger;

        private readonly IPlantService _plantService;
        private readonly IPlantSensorAlertService _plantAlertService;
        private readonly ISpecimenService _specimenService;
        private readonly ISoilHumidityCategoryService _soilHumidityCategoryService;

        public PlantHumidityRecordService(IRepository<PlantHumidityRecord> repository, ILogger<PlantHumidityRecordService> logger,
            IPlantService plantService, IPlantSensorAlertService plantAlertService, ISpecimenService specimenService, ISoilHumidityCategoryService soilHumidityCategoryService)
        {
            _repository = repository;
            _logger = logger;
            _plantService = plantService;
            _plantAlertService = plantAlertService;
            _specimenService = specimenService;
            _soilHumidityCategoryService = soilHumidityCategoryService;
        }

        public Task<PlantHumidityRecordDto> CreateAsync(PlantHumidityWriteDto dto)
        {
            
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PlantHumidityRecordDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PlantHumidityRecordDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PlantHumidityRecordDto> UpdateAsync(int id, PlantHumidityWriteDto dto)
        {
            throw new NotImplementedException();
        }
    }
}