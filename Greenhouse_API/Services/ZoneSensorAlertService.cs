using Greenhouse_API.DTOs;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Greenhouse_API.Services
{
    public class ZoneSensorAlertService : IZoneSensorAlertService
    {
        private SerreContext _context;
        private readonly ILogger<ZoneSensorAlertService> _logger;

        private readonly IRepository<Zone, int> _zoneService;

        public ZoneSensorAlertService(SerreContext context, ILogger<ZoneSensorAlertService> logger, IRepository<Zone, int> zoneService)
        {
            _context = context;
            _logger = logger;
            _zoneService = zoneService;   
        }

        public Task<IEnumerable<ZoneSensorAlertDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ZoneSensorAlertDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ZoneSensorAlertDto> CreateAsync(ZoneSensorAlertWriteDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ZoneSensorAlertDto> UpdateAsync(int id, ZoneSensorAlertWriteDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
