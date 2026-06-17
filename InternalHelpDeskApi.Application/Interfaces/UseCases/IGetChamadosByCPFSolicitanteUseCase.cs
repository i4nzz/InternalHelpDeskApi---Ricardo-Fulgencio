using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces.UseCases
{
    public interface IGetChamadosByCPFSolicitanteUseCase
    {
        Task<IEnumerable<ChamadosDtos>> GetByCPF(string cpfSolicitante);
    }
}
