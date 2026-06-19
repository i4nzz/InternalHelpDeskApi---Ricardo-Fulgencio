using InternalHelpDeskApi.Application.DTOs.Categorias;
using InternalHelpDeskApi.Application.Interfaces;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Application
{
    public class CriarCategoriaUseCase : ICriarCategoriaUseCase
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CriarCategoriaUseCase(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<Categoria> CriarCategoria(CategoriaDTO categoriaDto)
        {
            var categoria = new Categoria
            {
                Nome = categoriaDto.Nome,
                Peso = categoriaDto.Peso,
                CriadoEm = DateTime.UtcNow
            };

            return await _categoriaRepository.AddAsync(categoria);
        }
    }
}
