using InternalHelpDeskApi.Application.Interfaces.UseCases;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;
using Mapster;

namespace InternalHelpDeskApi.Application.UseCases
{
    public class CriarChamadoUseCase : ICriarChamadoUseCase
    {
        private readonly IChamadoRepository _chamadoRepository;
        public CriarChamadoUseCase(IChamadoRepository chamadoRepository)
        {
            _chamadoRepository = chamadoRepository;
        }
        public async Task<Chamados> CriarChamado(ChamadosDto novoChamado)
        {
            var chamado = novoChamado.Adapt<Chamados>();
            return await _chamadoRepository.AddAsync(chamado);
        }
    }
}
