using Greenhouse_API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Greenhouse_API.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly GreenHouseDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(GreenHouseDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // ======================
        // READ
        // ======================

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllWithFilter(
            Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            return await query.ToListAsync();
        }

        // ======================
        // CREATE
        // ======================

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // ======================
        // UPDATE
        // ======================

        public async Task<T> UpdateAsync(int id, T entity)
        {
            var existingEntity = await _dbSet.FindAsync(id);

            if (existingEntity == null)
                throw new KeyNotFoundException(
                    $"{typeof(T).Name} with id {id} not found");

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return existingEntity;
        }

        // ======================
        // DELETE
        // ======================

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
