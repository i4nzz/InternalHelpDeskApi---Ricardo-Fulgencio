using InternalHelpDeskApi.Application.Interfaces;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Application
{
    public class SoftDeleteChamadoUseCases : ISoftDeleteChamadoUseCase
    {
        private readonly IChamadoRepository _chamadoRepository;
        public SoftDeleteChamadoUseCases(IChamadoRepository chamadoRepository)
        {
            _chamadoRepository = chamadoRepository;
        }
        public async Task SoftDeleteChamado(int id)
        {
            var chamado = await _chamadoRepository.GetById(id);
            if (chamado == null)
            {
                throw new Exception($"Chamado com ID {id} não encontrado.");
            }
            await _chamadoRepository.SoftDeleteAsync(chamado);
        }
    }
}
