using InternalHelpDeskApi.Application.DTOs.Prioridades;
using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces
{
    public interface ICriarPrioridadeUseCase
    {
        Task<Prioridade> CriarPrioridade(PrioridadeDTO prioridade);
    }
}
