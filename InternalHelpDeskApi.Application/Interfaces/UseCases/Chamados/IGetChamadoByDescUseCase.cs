using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces
{
    public interface IGetChamadoByDescUseCase
    {
        Task<IEnumerable<Chamados>> GetByDesc(string descricao);
    }
}
