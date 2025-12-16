using Greenhouse_API.DTOs;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class SensorService : ISensorService
    {
        private SerreContext _context;
        private readonly ILogger<SensorService> _logger;
        private readonly IRepository<Zone, int> _zoneService;

        public SensorService(SerreContext context, ILogger<SensorService> logger, IRepository<Zone, int> zoneService)
        {
            _context = context;
            _logger = logger;
            _zoneService = zoneService;
        }

        public Task<IEnumerable<SensorDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SensorDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<SensorDto> CreateAsync(SensorWriteDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<SensorDto> UpdateAsync(int id, SensorWriteDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
