using InternalHelpDeskApi.Application.DTOs.Atendentes;
using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces
{
    public interface ICriarAtendenteUseCase
    {
        Task<Atendente> CriarAtendente(AtendenteDTO atendente);
    }
}
