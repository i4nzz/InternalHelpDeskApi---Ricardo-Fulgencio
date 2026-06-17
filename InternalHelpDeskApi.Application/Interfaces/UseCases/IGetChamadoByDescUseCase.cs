using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces.UseCases
{
    public interface IGetChamadoByDescUseCase
    {
        Task<IEnumerable<Chamado>> ExecuteAsync(string descricao);
    }
}
