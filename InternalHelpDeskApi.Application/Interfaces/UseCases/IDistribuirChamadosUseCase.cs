using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces.UseCases
{
    public interface IDistribuirChamadosUseCase
    {
        Task<Chamados?> DistribuirProximoChamado(int atendenteId);
    }
}
