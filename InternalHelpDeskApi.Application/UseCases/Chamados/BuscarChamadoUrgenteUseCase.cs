using InternalHelpDeskApi.Application.Interfaces;
using InternalHelpDeskApi.Application.Interfaces.UseCases.PriorityHeap;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Application
{
    public class BuscarChamadoUrgenteUseCase : IBuscarChamadoUrgenteUseCase
    {
        private readonly IChamadoRepository _chamadoRepository;
        private readonly IFilaPrioridadeHeapUseCase<Chamados> _filaPrioridade;
        private readonly IPriorityComparerUseCase _priorityComparer;

        public BuscarChamadoUrgenteUseCase(
            IChamadoRepository chamadoRepository,
            IFilaPrioridadeHeapUseCase<Chamados> filaPrioridade,
            IPriorityComparerUseCase priorityComparer)
        {
            _chamadoRepository = chamadoRepository;
            _filaPrioridade = filaPrioridade;
            _priorityComparer = priorityComparer;
        }

        public async Task<Chamados?> BuscarChamadoUrgente()
        {
            List<Chamados> chamadosAbertos = await _chamadoRepository.GetAllOpen();

            if (chamadosAbertos.Count == 0)
                return null;

            foreach (var chamado in chamadosAbertos)
            {
                _filaPrioridade.Enfileirar(chamado);
            }

            Chamados chamadoMaisUrgente = _filaPrioridade.Desenfileirar();
            return chamadoMaisUrgente;
        }
    }
}
