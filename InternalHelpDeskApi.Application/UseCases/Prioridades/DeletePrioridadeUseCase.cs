using InternalHelpDeskApi.Application.Interfaces;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Applications
{
    public class DeletePrioridadeUseCase : IDeletePrioridadeUseCase
    {
        private readonly IPrioridadeRepository _prioridadeRepository;

        public DeletePrioridadeUseCase(IPrioridadeRepository prioridadeRepository)
        {
            _prioridadeRepository = prioridadeRepository;
        }

        public async Task DeletePrioridade(int id)
        {
            var prioridade = await _prioridadeRepository.GetById(id);
            if (prioridade == null)
            {
                throw new Exception($"Prioridade com ID {id} não encontrada.");
            }

            prioridade.AtualizadoEm = DateTime.UtcNow;
            prioridade.DataExclusao = DateTime.UtcNow;
            prioridade.Ativo = false;
            await _prioridadeRepository.UpdateAsync(prioridade);
        }
    }
}
