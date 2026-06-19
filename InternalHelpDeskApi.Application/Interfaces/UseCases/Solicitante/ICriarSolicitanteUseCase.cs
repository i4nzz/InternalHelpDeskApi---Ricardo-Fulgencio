using InternalHelpDeskApi.Application.DTOs.Solicitantes;
using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces
{
    public interface ICriarSolicitanteUseCase
    {
        Task<Solicitante> CriarSolicitante(SolicitanteDTO solicitante);
    }
}
