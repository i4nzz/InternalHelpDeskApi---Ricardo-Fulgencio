using InternalHelpDeskApi.Application.DTOs.Prioridades;
using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces.UseCases
{
    public interface ICriarPrioridadeUseCase
    {
        Task<Prioridade> CriarPrioridade(PrioridadeDTO prioridade);
    }
}
