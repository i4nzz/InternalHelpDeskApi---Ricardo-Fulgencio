using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces.UseCases
{
    public interface IDistribuirChamadosUseCase
    {
        Task<ChamadosDtos?> DistribuirProximoChamado(int atendenteId);
    }
}
