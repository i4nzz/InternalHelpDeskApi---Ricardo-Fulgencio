using InternalHelpDeskApi.Application.UseCases;
using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces.UseCases
{
    public interface IUpdateChamadoUseCase
    {
        Task UpdateChamado(int id, ChamadosDto chamado);
    }
}
