using InternalHelpDeskApi.Application.Interfaces.UseCases;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Application.UseCases.Solicitantes
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
            solicitante.Status = Domain.Enums.StatusEnum.Cancelado;
        }
    }
}
