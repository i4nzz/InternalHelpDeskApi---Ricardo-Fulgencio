using InternalHelpDeskApi.Application.UseCases;
using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces
{
    public interface ICriarChamadoUseCase
    {
        Task<Chamados> CriarChamado(ChamadosDto novoChamado);  
    }
}
