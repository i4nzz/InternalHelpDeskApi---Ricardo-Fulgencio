using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces
{
    public interface IObterListaDeChamadosOrdenadosUseCase
    {
        Task<List<Chamados>> ObterListaChamadosOrdenados();
    }
}
