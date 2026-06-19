using InternalHelpDeskApi.Application.Interfaces;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Application.UseCases
{
    public class GetAllChamadosPagedUseCase : IGetAllChamadosPagedUseCase
    {
        private readonly IChamadoRepository _chamadoRepository;
        public GetAllChamadosPagedUseCase(IChamadoRepository chamadoRepository)
        {
            _chamadoRepository = chamadoRepository;
        }
        public async Task<IEnumerable<Chamados>> GetAllDisorderedPaged(int pageNumber, int pageSize)
        {
            var chamados = await _chamadoRepository.GetAllPaged(pageNumber, pageSize);
            return chamados.ToList();
        }
    }
}
