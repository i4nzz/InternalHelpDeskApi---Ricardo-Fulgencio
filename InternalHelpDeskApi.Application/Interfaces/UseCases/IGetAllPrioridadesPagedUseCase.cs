using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces.UseCases
{
    public interface IGetAllPrioridadesPagedUseCase
    {
        Task<IEnumerable<Prioridade>> GetAllPaged(int pageNumber, int pageSize);
    }
}
