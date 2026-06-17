using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces.UseCases
{
    public interface IObterListaDeChamadosOrdenadosUseCase
    {
        Task<List<ChamadosDtos>> ObterListaChamadosOrdenados();
    }
}
