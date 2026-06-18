using InternalHelpDeskApi.Application.Interfaces.UseCases;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Application.UseCases.Solicitantes
{
    public class GetSolicitanteByIdUseCase : IGetSolicitanteByIdUseCase
    {
        private readonly ISolicitanteRepository _solicitanteRepository;

        public GetSolicitanteByIdUseCase(ISolicitanteRepository solicitanteRepository)
        {
            _solicitanteRepository = solicitanteRepository;
        }

        public async Task<Solicitante> GetById(int id)
        {
            var solicitante = await _solicitanteRepository.GetById(id);
            if (solicitante == null)
            {
                throw new Exception($"Solicitante com ID {id} não encontrado.");
            }
            return solicitante;
        }
    }
}
