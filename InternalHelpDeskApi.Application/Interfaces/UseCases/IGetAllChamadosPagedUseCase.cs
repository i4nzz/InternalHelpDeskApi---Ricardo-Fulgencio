using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces.UseCases
{
    public interface IGetAllChamadosPagedUseCase
    {
        Task<IEnumerable<Chamado>> GetAllDisorderedPaged(int pageNumber, int pageSize);
    }
}
