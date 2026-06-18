using InternalHelpDeskApi.Application.DTOs.Atendentes;
using InternalHelpDeskApi.Application.Interfaces.UseCases;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Application.UseCases.Atendentes
{
    public class UpdateAtendenteUseCase : IUpdateAtendenteUseCase
    {
        private readonly IAtendenteRepository _atendenteRepository;

        public UpdateAtendenteUseCase(IAtendenteRepository atendenteRepository)
        {
            _atendenteRepository = atendenteRepository;
        }

        public async Task UpdateAtendente(int id, AtendenteDTO atendenteDto)
        {
            var atendente = await _atendenteRepository.GetById(id);
            if (atendente == null)
            {
                throw new Exception($"Atendente com ID {id} não encontrado.");
            }

            atendente.Nome = atendenteDto.Nome;
            atendente.Email = atendenteDto.Email;
            atendente.Ativo = atendenteDto.Ativo;
            atendente.AtualizadoEm = DateTime.UtcNow;

            await _atendenteRepository.UpdateAsync(atendente);
        }
    }
}
