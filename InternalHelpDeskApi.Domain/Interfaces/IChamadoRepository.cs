using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Domain.Interfaces;

public interface IChamadoRepository : IRepositoryBase<ChamadosDtos>
{
    Task SoftDeleteAsync(ChamadosDtos chamados);
    Task<List<ChamadosDtos>> GetByDesc(string descricao);
    Task<List<ChamadosDtos>> GetAllOpen();
    Task<List<ChamadosDtos>> GetByCPF(string cpf);
}
