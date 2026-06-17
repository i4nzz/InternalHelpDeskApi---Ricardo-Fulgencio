using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Domain.Interfaces;

public interface IChamadoRepository : IRepositoryBase<Chamado>
{
    Task SoftDeleteAsync(Chamado chamados);
    Task<List<Chamado>> GetByDesc(string descricao);
    Task<List<Chamado>> GetAllOpen();
    Task<List<Chamado>> GetByCPF(string cpf);
}
