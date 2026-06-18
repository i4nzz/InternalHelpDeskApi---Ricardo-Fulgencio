using InternalHelpDeskApi.Application.DTOs.Prioridades;
using InternalHelpDeskApi.Application.Interfaces.UseCases;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Application.UseCases.Prioridades
{
    public class UpdatePrioridadeUseCase : IUpdatePrioridadeUseCase
    {
        private readonly IPrioridadeRepository _prioridadeRepository;

        public UpdatePrioridadeUseCase(IPrioridadeRepository prioridadeRepository)
        {
            _prioridadeRepository = prioridadeRepository;
        }

        public async Task UpdatePrioridade(int id, PrioridadeDTO prioridadeDto)
        {
            var prioridade = await _prioridadeRepository.GetById(id);
            if (prioridade == null)
            {
                throw new Exception($"Prioridade com ID {id} não encontrada.");
            }

            prioridade.CategoriaId = prioridadeDto.CategoriaId;
            prioridade.Descricao = prioridadeDto.Descricao;
            prioridade.Peso = prioridadeDto.Peso;
            prioridade.AtualizadoEm = DateTime.UtcNow;

            await _prioridadeRepository.UpdateAsync(prioridade);
        }
    }
}
