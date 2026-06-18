using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Domain.Interfaces;

public interface IChamadoRepository : IRepositoryBase<Chamados>
{
    Task SoftDeleteAsync(Chamados chamados);
    Task<List<Chamados>> GetByDesc(string descricao);
    Task<List<Chamados>> GetAllOpen();
    Task<List<Chamados>> GetByCPF(string cpf);
}
