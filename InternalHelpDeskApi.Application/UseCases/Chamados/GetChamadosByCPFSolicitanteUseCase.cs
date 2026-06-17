using InternalHelpDeskApi.Application.Interfaces.UseCases;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Application.UseCases.Chamados
{
    public class GetChamadosByCPFSolicitanteUseCase : IGetChamadosByCPFSolicitanteUseCase
    {
        private readonly IChamadoRepository _chamadoRepository;
        public GetChamadosByCPFSolicitanteUseCase(IChamadoRepository chamadoRepository)
        {
            _chamadoRepository = chamadoRepository;
        }
        public async Task<IEnumerable<ChamadosDtos>> GetByCPF(string cpfSolicitante)
        {
            var chamados = await _chamadoRepository.GetByCPF(cpfSolicitante);
            return (chamados);
        }
    }
}
