using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces.UseCases
{
    public interface IGetAllSolicitantesPagedUseCase
    {
        Task<IEnumerable<Solicitante>> GetAllPaged(int pageNumber, int pageSize);
    }
}
