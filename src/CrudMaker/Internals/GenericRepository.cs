using CrudMaker.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CrudMaker.Internals
{
    public class GenericRepository<TEntity, TDbContext> : IRepository<TEntity>
        where TEntity : class, IIdentity
        where TDbContext : DbContext
    {
        private readonly TDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(TDbContext context)
        {
            _context = context;
            _dbSet = DbContextAccessor.GetDbSet<TEntity, TDbContext>(_context);
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            entity.Id = Guid.NewGuid();

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
