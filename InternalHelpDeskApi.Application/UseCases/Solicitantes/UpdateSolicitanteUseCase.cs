using InternalHelpDeskApi.Application.DTOs.Solicitantes;
using InternalHelpDeskApi.Application.Interfaces.UseCases;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Application.UseCases.Solicitantes
{
    public class UpdateSolicitanteUseCase : IUpdateSolicitanteUseCase
    {
        private readonly ISolicitanteRepository _solicitanteRepository;

        public UpdateSolicitanteUseCase(ISolicitanteRepository solicitanteRepository)
        {
            _solicitanteRepository = solicitanteRepository;
        }

        public async Task UpdateSolicitante(int id, SolicitanteDTO solicitanteDto)
        {
            var solicitante = await _solicitanteRepository.GetById(id);
            if (solicitante == null)
            {
                throw new Exception($"Solicitante com ID {id} não encontrado.");
            }

            solicitante.Nome = solicitanteDto.Nome;
            solicitante.Email = solicitanteDto.Email;
            solicitante.CPF = solicitanteDto.CPF;
            solicitante.Ativo = solicitanteDto.Ativo;
            solicitante.AtualizadoEm = DateTime.UtcNow;

            await _solicitanteRepository.UpdateAsync(solicitante);
        }
    }
}
