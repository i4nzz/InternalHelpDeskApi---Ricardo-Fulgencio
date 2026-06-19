using InternalHelpDeskApi.Application.Interfaces;
using InternalHelpDeskApi.Application.Interfaces.UseCases.PriorityHeap;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Application.UseCases
{
    public class ObterListaDeChamadosOrdenadosUseCase : IObterListaDeChamadosOrdenadosUseCase
    {
        private readonly IChamadoRepository _chamadoRepository;
        private readonly IFilaPrioridadeHeapUseCase<Chamados> _filaPrioridade;
        private readonly IPriorityComparerUseCase _priorityComparer;

        public ObterListaDeChamadosOrdenadosUseCase(
            IChamadoRepository chamadoRepository,
            IFilaPrioridadeHeapUseCase<Chamados> filaPrioridade,
            IPriorityComparerUseCase priorityComparer)
        {
            _chamadoRepository = chamadoRepository;
            _filaPrioridade = filaPrioridade;
            _priorityComparer = priorityComparer;
        }

        public async Task<List<Chamados>> ObterListaChamadosOrdenados()
        {
            List<Chamados> chamadosAbertos = await _chamadoRepository.GetAllOpen();

            foreach (var chamado in chamadosAbertos)
            {
                _filaPrioridade.Enfileirar(chamado);
            }

            var chamadosOrdenados = new List<Chamados>();

            while (!_filaPrioridade.EstaVazia)
            {
                Chamados proximo = _filaPrioridade.Desenfileirar();
                chamadosOrdenados.Add(proximo);
            }

            return chamadosOrdenados;
        }
    }
}