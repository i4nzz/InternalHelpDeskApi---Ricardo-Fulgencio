using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces
{
    public interface IGetAllAtendentesPagedUseCase
    {
        Task<IEnumerable<Atendente>> GetAllPaged(int pageNumber, int pageSize);
    }
}
