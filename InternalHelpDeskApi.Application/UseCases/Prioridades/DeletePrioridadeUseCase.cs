using InternalHelpDeskApi.Application.Interfaces.UseCases;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Application.UseCases.Prioridades
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
            prioridade.Status = Domain.Enums.StatusEnum.Cancelado;
        }
    }
}
