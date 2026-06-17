namespace InternalHelpDeskApi.Domain.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<TEntity?> GetById(int id);
        Task<IEnumerable<TEntity>> GetAllPaged(int pagina, int tamanhoPagina);
        Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : class;
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class;
    }
}
