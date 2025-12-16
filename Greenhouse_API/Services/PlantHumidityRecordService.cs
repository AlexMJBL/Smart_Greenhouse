using Greenhouse_API.DTOs;
using Greenhouse_API.Enums;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class PlantHumidityRecordService : IPlantHumidityRecordService
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

        public async Task<PlantHumidityRecordDto> CreateAsync(PlantHumidityWriteDto dto)
        {
            var plant = await _plantService.GetByIdAsync(dto.PlantId);
            if (plant == null)
            {
                _logger.LogWarning("Plant with ID: {PlantId} not found for humidity record creation", dto.PlantId);
                throw new NotFoundException("Plant not found");
            }

            var specimen = await _specimenService.GetByIdAsync(plant.SpecimenId);
            if (specimen == null)
            {
                _logger.LogWarning("Specimen with ID: {SpecimenId} not found for plant ID: {PlantId}", plant.SpecimenId, plant.Id);
                throw new NotFoundException("Specimen not found");
            }

            var soilHumidityCategory = await _soilHumidityCategoryService.GetByIdAsync(specimen.SoilHumidityCatId);
            if (soilHumidityCategory == null)
            {
                _logger.LogWarning("Soil Humidity Category with ID: {CategoryId} not found for specimen ID: {SpecimenId}", specimen.SoilHumidityCatId, specimen.Id);
                throw new NotFoundException("Soil Humidity Category not found");
            }

            bool inRange = true;
            AlertReason? reason = null;
            if (dto.RecordPct < soilHumidityCategory.MinHumidityPct|| dto.RecordPct > soilHumidityCategory.MaxHumidityPct)
            {
                inRange = false;
                reason = dto.RecordPct < soilHumidityCategory.MinHumidityPct ? AlertReason.Under : AlertReason.Over;
            }
      

            var record = new PlantHumidityRecord
            {
                RecordPct = dto.RecordPct,
                InRange = inRange,
                PlantId = dto.PlantId,
                CreatedAt = DateTime.UtcNow
            };

            if (!inRange && reason.HasValue)
            {
                await _plantAlertService.CreateAsync(new PlantSensorAlertPartial
                {
                    PlantId = dto.PlantId,
                    Reason = reason.Value,
                });
            }

            await _repository.AddAsync(record);
            _logger.LogInformation("Plant humidity record created with ID: {RecordId}", record.Id);

            return new PlantHumidityRecordDto
            {
                Id = record.Id,
                RecordPct = record.RecordPct,
                InRange = record.InRange,
                PlantId = record.PlantId,
                CreatedAt = record.CreatedAt
            };
        }

        public async Task DeleteAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (!deleted)
            {
                _logger.LogWarning("Plant humidity record with ID {RecordId} not found for deletion", id);
                throw new NotFoundException("Plant humidity record not found");
            }

            _logger.LogInformation("Plant humidity record with ID {RecordId} deleted", id); 
        }

        public async Task<IEnumerable<PlantHumidityRecordDto>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all plant humidity records");
            var records = await _repository.GetAllAsync();

            return records.Select(r => new PlantHumidityRecordDto
            {
                Id = r.Id,
                RecordPct = r.RecordPct,
                InRange = r.InRange,
                PlantId = r.PlantId,
                CreatedAt = r.CreatedAt
            });

        }

        public async Task<PlantHumidityRecordDto?> GetByIdAsync(int id)
        {
            var record = await _repository.GetByIdAsync(id);
            if (record == null)
            {
                _logger.LogWarning("Plant humidity record with ID: {RecordId} not found", id);
                return null;
            }

            _logger.LogInformation("Plant humidity record with ID: {RecordId} retrieved", id);
            return new PlantHumidityRecordDto
            {
                Id = record.Id,
                RecordPct = record.RecordPct,
                InRange = record.InRange,
                PlantId = record.PlantId,
                CreatedAt = record.CreatedAt
            };
        }
    }
}