using InternalHelpDeskApi.Application.Interfaces.UseCases;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Application.UseCases.Atendentes
{
    public class GetAtendenteByIdUseCase : IGetAtendenteByIdUseCase
    {
        private readonly IAtendenteRepository _atendenteRepository;

        public GetAtendenteByIdUseCase(IAtendenteRepository atendenteRepository)
        {
            _atendenteRepository = atendenteRepository;
        }

        public async Task<Atendente> GetById(int id)
        {
            var atendente = await _atendenteRepository.GetById(id);
            if (atendente == null)
            {
                throw new Exception($"Atendente com ID {id} não encontrado.");
            }
            return atendente;
        }
    }
}
