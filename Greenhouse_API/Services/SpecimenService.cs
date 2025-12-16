using Greenhouse_API.DTOs;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class SpecimenService : ISpecimenService
    {
        private SerreContext _context;
        private readonly ILogger<SpecimenService> _logger;
        private readonly IRepository<SoilHumidityCategory, int> _soilHumidityCategoryService;
        
        public SpecimenService(SerreContext context, ILogger<SpecimenService> logger, IRepository<SoilHumidityCategory, int> soilHumidityCategoryService)
        {
            _context = context;
            _logger = logger;
            _soilHumidityCategoryService = soilHumidityCategoryService;
        }

        public Task<IEnumerable<SpecimenDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SpecimenDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<SpecimenDto> CreateAsync(SpecimenWriteDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<SpecimenDto> UpdateAsync(int id, SpecimenWriteDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
