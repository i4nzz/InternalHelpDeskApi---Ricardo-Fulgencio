using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Domain.Interfaces;

public interface IChamadoRepository
{
    Task<Chamado?> GetByIdAsync(int id);
    Task<IEnumerable<Chamado>> GetAllAsync();
    Task<IEnumerable<Chamado>> GetByDescriptionAsync(string description);
    Task AddAsync(Chamado chamado);
    Task UpdateAsync(Chamado chamado);
    Task SaveAsync();
}
