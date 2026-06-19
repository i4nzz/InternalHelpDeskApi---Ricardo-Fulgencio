using InternalHelpDeskApi.Application.Interfaces;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Applications
{
    public class GetAllPrioridadesPagedUseCase : IGetAllPrioridadesPagedUseCase
    {
        private readonly IPrioridadeRepository _prioridadeRepository;

        public GetAllPrioridadesPagedUseCase(IPrioridadeRepository prioridadeRepository)
        {
            _prioridadeRepository = prioridadeRepository;
        }

        public async Task<IEnumerable<Prioridade>> GetAllPaged(int pageNumber, int pageSize)
        {
            return await _prioridadeRepository.GetAllPaged(pageNumber, pageSize);
        }
    }
}
