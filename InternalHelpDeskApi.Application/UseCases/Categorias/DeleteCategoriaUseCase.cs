using InternalHelpDeskApi.Application.Interfaces;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Application
{
    public class DeleteCategoriaUseCase : IDeleteCategoriaUseCase
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public DeleteCategoriaUseCase(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task DeleteCategoria(int id)
        {
            var categoria = await _categoriaRepository.GetById(id);
            if (categoria == null)
            {
                throw new Exception($"Categoria com ID {id} não encontrada.");
            }

            categoria.AtualizadoEm = DateTime.UtcNow;
            categoria.DataExclusao = DateTime.UtcNow;
            categoria.Ativo = false;

            await _categoriaRepository.UpdateAsync(categoria);
        }
    }
}
