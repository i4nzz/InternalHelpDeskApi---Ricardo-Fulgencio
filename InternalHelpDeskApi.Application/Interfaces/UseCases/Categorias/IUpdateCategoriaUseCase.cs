using InternalHelpDeskApi.Application.DTOs.Categorias;

namespace InternalHelpDeskApi.Application.Interfaces
{
    public interface IUpdateCategoriaUseCase
    {
        Task UpdateCategoria(int id, CategoriaDTO categoria);
    }
}
