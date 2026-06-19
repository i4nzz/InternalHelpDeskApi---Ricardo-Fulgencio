using InternalHelpDeskApi.Application.DTOs.Categorias;
using InternalHelpDeskApi.Application.Interfaces;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Application
{
    public class UpdateCategoriaUseCase : IUpdateCategoriaUseCase
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public UpdateCategoriaUseCase(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task UpdateCategoria(int id, CategoriaDTO categoriaDto)
        {
            var categoria = await _categoriaRepository.GetById(id);
            if (categoria == null)
            {
                throw new Exception($"Categoria com ID {id} não encontrada.");
            }

            categoria.Nome = categoriaDto.Nome;
            categoria.Peso = categoriaDto.Peso;
            categoria.AtualizadoEm = DateTime.UtcNow;

            await _categoriaRepository.UpdateAsync(categoria);
        }
    }
}
