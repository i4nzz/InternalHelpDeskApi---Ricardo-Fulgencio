using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces.UseCases
{
    public interface IChamadosUrgentesUseCase
    {
        Task<Chamados?> DistribuirProximoChamado(int atendenteId);
        Task<Chamados?> BuscarChamadoUrgente();
    }
}
