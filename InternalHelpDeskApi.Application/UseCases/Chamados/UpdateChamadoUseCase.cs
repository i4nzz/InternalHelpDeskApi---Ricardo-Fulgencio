using InternalHelpDeskApi.Application.Interfaces;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;
using Mapster;

namespace InternalHelpDeskApi.Application.UseCases
{
    public class UpdateChamadoUseCase : IUpdateChamadoUseCase
    {
        private readonly IChamadoRepository _chamadoRepository;
        public UpdateChamadoUseCase(IChamadoRepository chamadoRepository)
        {
            _chamadoRepository = chamadoRepository;
        }
        public Task UpdateChamado(int id, ChamadosDto chamado)
        {
            var chamadoBanco = chamado.Adapt<Chamados>();
            chamadoBanco.Id = id;
            return _chamadoRepository.UpdateAsync(chamadoBanco);
        }
    }
}
