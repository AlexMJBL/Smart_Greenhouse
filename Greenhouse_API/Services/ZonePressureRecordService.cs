using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Greenhouse_API.Services;

namespace Greenhouse_API.Services
{
    public class ZonePressureRecordService : IRepository<ZonePressureRecord, int>
    {
        private SerreContext _context;
        private readonly ILogger<ZonePressureRecordService> _logger;
        public ZonePressureRecordService(SerreContext context, ILogger<ZonePressureRecordService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<ZonePressureRecord>> GetAllAsync()
        {
            return await _context.ZonePressureRecords.ToListAsync();
        }

        public async Task<IEnumerable<ZonePressureRecord>> GetAllWithFilter(Expression<Func<ZonePressureRecord, bool>>? filter = null)
        {
            IQueryable<ZonePressureRecord> query = _context.ZonePressureRecords;

            if (filter != null)
                query = query.Where(filter);

            return await query.ToListAsync();
        }

        public async Task<ZonePressureRecord?> GetByIdAsync(int id)
        {
            return await _context.ZonePressureRecords.FindAsync(id);
        }

  public async Task<ZonePressureRecord> AddAsync(ZonePressureRecord zonePressureRecord)
{


    _context.ZonePressureRecords.Add(zonePressureRecord);
    await _context.SaveChangesAsync();

    _logger.LogInformation($"ZonePressureRecord with ID {zonePressureRecord.Id} added successfully.");
    return zonePressureRecord;
}

    public async Task<ZonePressureRecord> UpdateAsync(int id, ZonePressureRecord zonePressureRecord)
{
    if (id != zonePressureRecord.Id)
        throw new ArgumentException("ID mismatch between route and body.");

    _context.ZonePressureRecords.Update(zonePressureRecord);
    await _context.SaveChangesAsync();

    _logger.LogInformation($"ZonePressureRecord with ID {zonePressureRecord.Id} updated successfully.");
    return zonePressureRecord;
}

        public async Task<bool> DeleteAsync(int id)
        {
            var zonePressureRecord = await _context.ZonePressureRecords.FindAsync(id);
            if (zonePressureRecord == null)
            {
                return false;
            }

            _context.ZonePressureRecords.Remove(zonePressureRecord);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"ZonePressureRecord with ID {id} deleted successfully.");
            return true;
        }
    
    }
}
