using InternalHelpDeskApi.Application.DTOs.Solicitantes;

namespace InternalHelpDeskApi.Application.Interfaces
{
    public interface IUpdateSolicitanteUseCase
    {
        Task UpdateSolicitante(int id, SolicitanteDTO solicitante);
    }
}
