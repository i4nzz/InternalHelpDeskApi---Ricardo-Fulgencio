using InternalHelpDeskApi.Application.Interfaces.UseCases;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Application.UseCases.Chamados
{
    public class GetAllChamadosPagedUseCase : IGetAllChamadosPagedUseCase
    {
        private readonly IChamadoRepository _chamadoRepository;
        public GetAllChamadosPagedUseCase(IChamadoRepository chamadoRepository)
        {
            _chamadoRepository = chamadoRepository;
        }
        public async Task<IEnumerable<ChamadosDtos>> GetAllDisorderedPaged(int pageNumber, int pageSize)
        {
            var chamados = await _chamadoRepository.GetAllPaged(pageNumber, pageSize);
            return chamados.ToList();
        }
    }
}
