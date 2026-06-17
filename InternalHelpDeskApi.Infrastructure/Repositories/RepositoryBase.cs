using InternalHelpDeskApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InternalHelpDeskApi.Infrastructure
{
    public class RepositoryBase
    {
        private readonly DataBase _context;

        public RepositoryBase(DataBase context)
        {
            _context = context;
        }

        public async Task<TEntity?> GetById<TEntity>(int id) where TEntity : class
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
        public async Task<IEnumerable<TEntity>> GetAllPaged<TEntity>(int pagina, int tamanhoPagina) where TEntity : class
        {
            return await _context.Set<TEntity>()
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();
        }

        public async Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            var entry = await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
