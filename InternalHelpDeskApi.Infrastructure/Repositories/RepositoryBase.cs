using InternalHelpDeskApi.Domain.Interfaces;
using InternalHelpDeskApi.Domain.Interfaces;
using InternalHelpDeskApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InternalHelpDeskApi.Infrastructure
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly DataBase _context;

        public RepositoryBase(DataBase context)
        {
            _context = context;
        }

        public async Task<TEntity?> GetById(int id) 
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllPaged(int pagina, int tamanhoPagina)
        {
            return await _context.Set<TEntity>()
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();
        }

        public async Task<T> AddAsync<T>(T entity) where T : class
        {
            var entry = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
