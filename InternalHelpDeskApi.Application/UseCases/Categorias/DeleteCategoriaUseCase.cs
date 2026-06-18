using InternalHelpDeskApi.Application.Interfaces.UseCases;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Application.UseCases.Categorias
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

            // Implementar soft delete ou hard delete conforme necessário
            // Por enquanto, vamos remover a entidade
            throw new NotImplementedException("Deletar categoria requer implementação adicional.");
        }
    }
}
