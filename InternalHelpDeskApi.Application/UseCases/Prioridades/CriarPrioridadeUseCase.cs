using InternalHelpDeskApi.Application.DTOs.Prioridades;
using InternalHelpDeskApi.Application.Interfaces.UseCases;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Application.UseCases.Prioridades
{
    public class CriarPrioridadeUseCase : ICriarPrioridadeUseCase
    {
        private readonly IPrioridadeRepository _prioridadeRepository;

        public CriarPrioridadeUseCase(IPrioridadeRepository prioridadeRepository)
        {
            _prioridadeRepository = prioridadeRepository;
        }

        public async Task<Prioridade> CriarPrioridade(PrioridadeDTO prioridadeDto)
        {
            var prioridade = new Prioridade
            {
                CategoriaId = prioridadeDto.CategoriaId,
                Descricao = prioridadeDto.Descricao,
                Peso = prioridadeDto.Peso,
                CriadoEm = DateTime.UtcNow
            };

            return await _prioridadeRepository.AddAsync(prioridade);
        }
    }
}
