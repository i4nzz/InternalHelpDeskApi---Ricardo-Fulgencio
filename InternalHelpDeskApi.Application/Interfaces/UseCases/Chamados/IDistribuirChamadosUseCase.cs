using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces
{
    public interface IChamadosUrgentesUseCase
    {
        Task<Chamados?> DistribuirProximoChamado(int atendenteId);
    }
}
