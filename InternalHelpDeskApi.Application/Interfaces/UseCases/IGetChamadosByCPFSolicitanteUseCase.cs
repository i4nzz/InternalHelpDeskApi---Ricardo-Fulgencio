using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces.UseCases
{
    public interface IGetChamadosByCPFSolicitanteUseCase
    {
        Task<IEnumerable<Chamado>> GetByCPF(string cpfSolicitante);
    }
}
