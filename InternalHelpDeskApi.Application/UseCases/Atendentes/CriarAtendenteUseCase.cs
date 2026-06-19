using InternalHelpDeskApi.Application.DTOs.Atendentes;
using InternalHelpDeskApi.Application.Interfaces;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Applications
{
    public class CriarAtendenteUseCase : ICriarAtendenteUseCase
    {
        private readonly IAtendenteRepository _atendenteRepository;

        public CriarAtendenteUseCase(IAtendenteRepository atendenteRepository)
        {
            _atendenteRepository = atendenteRepository;
        }

        public async Task<Atendente> CriarAtendente(AtendenteDTO atendenteDto)
        {
            var atendente = new Atendente
            {
                Nome = atendenteDto.Nome,
                Email = atendenteDto.Email,
                Ativo = atendenteDto.Ativo,
                CriadoEm = DateTime.UtcNow,
                AtualizadoEm = DateTime.UtcNow
            };

            return await _atendenteRepository.AddAsync(atendente);
        }
    }
}
