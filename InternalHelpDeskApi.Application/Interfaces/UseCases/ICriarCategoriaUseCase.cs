using InternalHelpDeskApi.Application.DTOs.Categorias;
using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces.UseCases
{
    public interface ICriarCategoriaUseCase
    {
        Task<Categoria> CriarCategoria(CategoriaDTO categoria);
    }
}
