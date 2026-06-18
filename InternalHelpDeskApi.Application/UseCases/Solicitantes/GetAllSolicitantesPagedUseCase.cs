using InternalHelpDeskApi.Application.Interfaces.UseCases;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Application.UseCases.Solicitantes
{
    public class GetAllSolicitantesPagedUseCase : IGetAllSolicitantesPagedUseCase
    {
        private readonly ISolicitanteRepository _solicitanteRepository;

        public GetAllSolicitantesPagedUseCase(ISolicitanteRepository solicitanteRepository)
        {
            _solicitanteRepository = solicitanteRepository;
        }

        public async Task<IEnumerable<Solicitante>> GetAllPaged(int pageNumber, int pageSize)
        {
            return await _solicitanteRepository.GetAllPaged(pageNumber, pageSize);
        }
    }
}
