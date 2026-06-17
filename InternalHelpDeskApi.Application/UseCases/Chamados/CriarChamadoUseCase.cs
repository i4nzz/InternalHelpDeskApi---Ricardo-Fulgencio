using InternalHelpDeskApi.Application.Interfaces.UseCases;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Application.UseCases.Chamados
{
    public class CriarChamadoUseCase : ICriarChamadoUseCase
    {
        private readonly IChamadoRepository _chamadoRepository;
        public CriarChamadoUseCase(IChamadoRepository chamadoRepository)
        {
            _chamadoRepository = chamadoRepository;
        }
        public async Task<ChamadosDtos> CriarChamado(ChamadosDtos novoChamado)
        {
            return await _chamadoRepository.AddAsync(novoChamado);
        }
    }
}
