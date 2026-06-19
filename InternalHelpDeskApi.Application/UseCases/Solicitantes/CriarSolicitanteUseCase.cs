using InternalHelpDeskApi.Application.DTOs.Solicitantes;
using InternalHelpDeskApi.Application.Interfaces;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Applications
{
    public class CriarSolicitanteUseCase : ICriarSolicitanteUseCase
    {
        private readonly ISolicitanteRepository _solicitanteRepository;

        public CriarSolicitanteUseCase(ISolicitanteRepository solicitanteRepository)
        {
            _solicitanteRepository = solicitanteRepository;
        }

        public async Task<Solicitante> CriarSolicitante(SolicitanteDTO solicitanteDto)
        {
            var solicitante = new Solicitante
            {
                Nome = solicitanteDto.Nome,
                Email = solicitanteDto.Email,
                CPF = solicitanteDto.CPF,
                Ativo = solicitanteDto.Ativo,
                CriadoEm = DateTime.UtcNow,
                AtualizadoEm = DateTime.UtcNow
            };

            return await _solicitanteRepository.AddAsync(solicitante);
        }
    }
}
