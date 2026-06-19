using InternalHelpDeskApi.Application.Interfaces;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Applications
{
    public class GetAllAtendentesPagedUseCase : IGetAllAtendentesPagedUseCase
    {
        private readonly IAtendenteRepository _atendenteRepository;

        public GetAllAtendentesPagedUseCase(IAtendenteRepository atendenteRepository)
        {
            _atendenteRepository = atendenteRepository;
        }

        public async Task<IEnumerable<Atendente>> GetAllPaged(int pageNumber, int pageSize)
        {
            return await _atendenteRepository.GetAllPaged(pageNumber, pageSize);
        }
    }
}
