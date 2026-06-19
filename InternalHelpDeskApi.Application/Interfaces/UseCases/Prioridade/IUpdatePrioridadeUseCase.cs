using InternalHelpDeskApi.Application.DTOs.Prioridades;

namespace InternalHelpDeskApi.Application.Interfaces
{
    public interface IUpdatePrioridadeUseCase
    {
        Task UpdatePrioridade(int id, PrioridadeDTO prioridade);
    }
}
