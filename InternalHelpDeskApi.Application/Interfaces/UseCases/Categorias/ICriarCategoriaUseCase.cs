using InternalHelpDeskApi.Application.DTOs.Categorias;
using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces
{
    public interface ICriarCategoriaUseCase
    {
        Task<Categoria> CriarCategoria(CategoriaDTO categoria);
    }
}
