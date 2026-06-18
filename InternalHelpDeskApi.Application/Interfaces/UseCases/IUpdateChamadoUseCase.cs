using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces.UseCases
{
    public interface IUpdateChamadoUseCase
    {
        Task UpdateChamado(Chamados chamado);
    }
}
