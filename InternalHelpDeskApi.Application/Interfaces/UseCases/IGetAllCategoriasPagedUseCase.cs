using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces.UseCases
{
    public interface IGetAllCategoriasPagedUseCase
    {
        Task<IEnumerable<Categoria>> GetAllPaged(int pageNumber, int pageSize);
    }
}
