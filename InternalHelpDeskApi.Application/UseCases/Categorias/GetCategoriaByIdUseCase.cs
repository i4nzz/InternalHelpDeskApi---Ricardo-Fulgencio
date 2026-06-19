using InternalHelpDeskApi.Application.DTOs.Categorias;
using InternalHelpDeskApi.Application.Interfaces;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Application
{
    public class GetCategoriaByIdUseCase : IGetCategoriaByIdUseCase
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public GetCategoriaByIdUseCase(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<Categoria> GetById(int id)
        {
            var categoria = await _categoriaRepository.GetById(id);
            if (categoria == null)
            {
                throw new Exception($"Categoria com ID {id} não encontrada.");
            }
            return categoria;
        }
    }
}
