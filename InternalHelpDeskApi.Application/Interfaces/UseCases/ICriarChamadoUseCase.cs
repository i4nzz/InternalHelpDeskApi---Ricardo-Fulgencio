using InternalHelpDeskApi.Application.UseCases;
using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces.UseCases
{
    public interface ICriarChamadoUseCase
    {
        Task<Chamados> CriarChamado(ChamadosDto novoChamado);  
    }
}
