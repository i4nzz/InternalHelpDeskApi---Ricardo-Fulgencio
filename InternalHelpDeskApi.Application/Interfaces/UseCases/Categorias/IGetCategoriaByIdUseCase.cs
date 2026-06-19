using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces
{
    public interface IGetCategoriaByIdUseCase
    {
        Task<Categoria> GetById(int id);
    }
}
