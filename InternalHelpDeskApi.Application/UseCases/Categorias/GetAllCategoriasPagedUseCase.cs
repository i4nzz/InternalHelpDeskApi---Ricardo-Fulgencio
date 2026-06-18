using InternalHelpDeskApi.Application.Interfaces.UseCases;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Application.UseCases.Categorias
{
    public class GetAllCategoriasPagedUseCase : IGetAllCategoriasPagedUseCase
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public GetAllCategoriasPagedUseCase(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<Categoria>> GetAllPaged(int pageNumber, int pageSize)
        {
            return await _categoriaRepository.GetAllPaged(pageNumber, pageSize);
        }
    }
}
