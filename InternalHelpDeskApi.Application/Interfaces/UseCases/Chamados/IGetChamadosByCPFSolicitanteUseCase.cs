using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces
{
    public interface IGetChamadosByCPFSolicitanteUseCase
    {
        Task<IEnumerable<Chamados>> GetByCPF(string cpfSolicitante);
    }
}
