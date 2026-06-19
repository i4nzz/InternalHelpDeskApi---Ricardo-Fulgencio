using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces
{
    public interface IGetAllCategoriasPagedUseCase
    {
        Task<IEnumerable<Categoria>> GetAllPaged(int pageNumber, int pageSize);
    }
}
