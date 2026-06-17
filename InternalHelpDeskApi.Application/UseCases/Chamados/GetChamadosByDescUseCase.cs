using InternalHelpDeskApi.Application.Interfaces.UseCases;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Application.UseCases.Chamados
{
    public class GetChamadosByDescUseCase : IGetChamadoByDescUseCase
    {
        private readonly IChamadoRepository _chamadoRepository;
        public GetChamadosByDescUseCase(IChamadoRepository chamadoRepository)
        {
            _chamadoRepository = chamadoRepository;
        }
        public async Task<IEnumerable<ChamadosDtos>> GetByDesc(string descricao)
        {
            var chamados = await _chamadoRepository.GetByDesc(descricao);
            return (chamados);
        }
    }
}
