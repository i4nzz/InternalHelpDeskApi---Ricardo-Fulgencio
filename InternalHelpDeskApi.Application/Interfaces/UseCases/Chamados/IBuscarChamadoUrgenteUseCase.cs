using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces
{
    public interface IBuscarChamadoUrgenteUseCase
    {
        Task<Chamados?> BuscarChamadoUrgente();
    }
}
