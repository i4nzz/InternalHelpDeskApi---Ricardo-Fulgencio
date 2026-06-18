using InternalHelpDeskApi.Application.Interfaces.UseCases;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Application.UseCases
{
    public class UpdateChamadoUseCase : IUpdateChamadoUseCase
    {
        private readonly IChamadoRepository _chamadoRepository;
        public UpdateChamadoUseCase(IChamadoRepository chamadoRepository)
        {
            _chamadoRepository = chamadoRepository;
        }
        public Task UpdateChamado(Chamados chamado)
        {
            return _chamadoRepository.UpdateAsync(chamado);
        }
    }
}
