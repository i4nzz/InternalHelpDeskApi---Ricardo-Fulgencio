using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces.UseCases
{
    public interface IGetChamadoByDescUseCase
    {
        Task<IEnumerable<Chamados>> GetByDesc(string descricao);
    }
}
