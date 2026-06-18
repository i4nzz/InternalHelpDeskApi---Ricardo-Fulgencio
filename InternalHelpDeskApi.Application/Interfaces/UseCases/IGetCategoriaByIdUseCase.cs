using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces.UseCases
{
    public interface IGetCategoriaByIdUseCase
    {
        Task<Categoria> GetById(int id);
    }
}
