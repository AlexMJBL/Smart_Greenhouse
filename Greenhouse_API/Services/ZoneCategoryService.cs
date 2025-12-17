using Greenhouse_API.DTOs;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class ZoneCategoryService : IZoneCategoryService
    {
        private IRepository<ZoneCategory> _repository;
        private readonly ILogger<ZoneCategoryService> _logger;
        public ZoneCategoryService(IRepository<ZoneCategory> repository, ILogger<ZoneCategoryService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<ZoneCategoryDto>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all zone categories");   
            var categories = await _repository.GetAllAsync();
            return categories.Select(c => new ZoneCategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                HumidityMinPct = c.HumidityMinPct,
                HumidityMaxPct = c.HumidityMaxPct,
                LuminosityMinLux = c.LuminosityMinLux,
                LuminosityMaxLux = c.LuminosityMaxLux,
                TemperatureMinC = c.TemperatureMinC,
                TemperatureMaxC = c.TemperatureMaxC,
                PressureMinPa = c.PressureMinPa,
                PressureMaxPa = c.PressureMaxPa,
                CreatedAt = c.CreatedAt
            });
        }

        public async Task<ZoneCategoryDto?> GetByIdAsync(int id)
        {
            var categorie = await _repository.GetByIdAsync(id);
            if (categorie == null)
            {
                _logger.LogWarning("Zone category with ID: {ZoneCategoryId} not found", id);
                return null;
            }

            _logger.LogInformation("Zone category with ID: {ZoneCategoryId} retrieved", id);
            return new ZoneCategoryDto
            {
                Id = categorie.Id,
                Name = categorie.Name,
                HumidityMinPct = categorie.HumidityMinPct,
                HumidityMaxPct = categorie.HumidityMaxPct,
                LuminosityMinLux = categorie.LuminosityMinLux,  
                LuminosityMaxLux = categorie.LuminosityMaxLux,
                TemperatureMinC = categorie.TemperatureMinC,
                TemperatureMaxC = categorie.TemperatureMaxC,
                PressureMinPa = categorie.PressureMinPa,
                PressureMaxPa = categorie.PressureMaxPa,
                CreatedAt = categorie.CreatedAt
            };
        }

        public async Task<ZoneCategoryDto> CreateAsync(ZoneCategoryWriteDto dto)
        {
            var category = new ZoneCategory
            {
                Name = dto.Name,
                HumidityMinPct =  dto.HumidityMinPct,
                HumidityMaxPct = dto.HumidityMaxPct,
                LuminosityMinLux = dto.LuminosityMinLux,
                LuminosityMaxLux = dto.LuminosityMaxLux,
                TemperatureMinC = dto.TemperatureMinC,
                TemperatureMaxC = dto.TemperatureMaxC,
                PressureMinPa = dto.PressureMinPa,
                PressureMaxPa = dto.PressureMaxPa,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(category);
            _logger.LogInformation("Zone category created with ID: {ZoneCategoryId}", category.Id);

            return new ZoneCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                HumidityMinPct = category.HumidityMinPct,
                HumidityMaxPct = category.HumidityMaxPct,
                LuminosityMinLux = category.LuminosityMinLux,
                LuminosityMaxLux = category.LuminosityMaxLux,
                TemperatureMinC = category.TemperatureMinC,
                TemperatureMaxC = category.TemperatureMaxC,
                PressureMinPa = category.PressureMinPa,
                PressureMaxPa = category.PressureMaxPa,
                CreatedAt = category.CreatedAt
            };
        }

        public async Task<ZoneCategoryDto> UpdateAsync(int id, ZoneCategoryWriteDto dto)
        {
            var categorie = await _repository.GetByIdAsync(id);
            if (categorie == null)
            {
                _logger.LogWarning("Zone category with ID: {ZoneCategoryId} not found for update", id);
                throw new KeyNotFoundException("Zone category not found");
            }

            categorie.Name = dto.Name;
            categorie.HumidityMinPct = dto.HumidityMinPct;
            categorie.HumidityMaxPct = dto.HumidityMaxPct;
            categorie.LuminosityMinLux = dto.LuminosityMinLux;
            categorie.LuminosityMaxLux = dto.LuminosityMaxLux;
            categorie.TemperatureMinC = dto.TemperatureMinC;
            categorie.TemperatureMaxC = dto.TemperatureMaxC;
            categorie.PressureMinPa = dto.PressureMinPa;
            categorie.PressureMaxPa = dto.PressureMaxPa;

            await _repository.SaveAsync();
            _logger.LogInformation("Zone category with ID: {ZoneCategoryId} updated", id);

            return new ZoneCategoryDto
            {
                Id = categorie.Id,
                Name = categorie.Name,
                HumidityMinPct = categorie.HumidityMinPct,
                HumidityMaxPct = categorie.HumidityMaxPct,
                LuminosityMinLux = categorie.LuminosityMinLux,
                LuminosityMaxLux = categorie.LuminosityMaxLux,
                TemperatureMinC = categorie.TemperatureMinC,
                TemperatureMaxC = categorie.TemperatureMaxC,
                PressureMinPa = categorie.PressureMinPa,
                PressureMaxPa = categorie.PressureMaxPa,
                CreatedAt = categorie.CreatedAt
            };
        }

        public async Task DeleteAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
            {
                _logger.LogWarning("Zone category with ID {ZoneCategoryId} not found for deletion", id);
                throw new KeyNotFoundException("Zone category not found");  
            }

            _logger.LogInformation("Zone category with ID {ZoneCategoryId} deleted", id);
        }
    }
}
