using InternalHelpDeskApi.Application.Interfaces;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Applications
{
    public class DeleteSolicitanteUseCase : IDeleteSolicitanteUseCase
    {
        private readonly ISolicitanteRepository _solicitanteRepository;

        public DeleteSolicitanteUseCase(ISolicitanteRepository solicitanteRepository)
        {
            _solicitanteRepository = solicitanteRepository;
        }

        public async Task DeleteSolicitante(int id)
        {
            var solicitante = await _solicitanteRepository.GetById(id);
            if (solicitante == null)
            {
                throw new Exception($"Solicitante com ID {id} não encontrado.");
            }

            solicitante.AtualizadoEm = DateTime.UtcNow;
            solicitante.DataExclusao = DateTime.UtcNow;
            solicitante.Ativo = false;

            await _solicitanteRepository.UpdateAsync(solicitante);
        }
    }
}
