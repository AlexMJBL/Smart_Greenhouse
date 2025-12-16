using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Greenhouse_API.Services;
using Greenhouse_API.DTOs;

namespace Greenhouse_API.Services
{
    public class ZonePressureRecordService : IZonePressureRecordService
    {
        private SerreContext _context;
        private readonly ILogger<ZonePressureRecordService> _logger;
        public ZonePressureRecordService(SerreContext context, ILogger<ZonePressureRecordService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<IEnumerable<ZonePressureRecordDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ZonePressureRecordDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ZonePressureRecordDto> CreateAsync(ZonePressureRecordWriteDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ZonePressureRecordDto> UpdateAsync(int id, ZonePressureRecordWriteDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
