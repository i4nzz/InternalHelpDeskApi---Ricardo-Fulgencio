using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces.UseCases
{
    public interface IGetAllChamadosPagedUseCase
    {
        Task<IEnumerable<Chamados>> GetAllDisorderedPaged(int pageNumber, int pageSize);
    }
}
