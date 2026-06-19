using InternalHelpDeskApi.Application.DTOs.Atendentes;

namespace InternalHelpDeskApi.Application.Interfaces
{
    public interface IUpdateAtendenteUseCase
    {
        Task UpdateAtendente(int id, AtendenteDTO atendente);
    }
}
