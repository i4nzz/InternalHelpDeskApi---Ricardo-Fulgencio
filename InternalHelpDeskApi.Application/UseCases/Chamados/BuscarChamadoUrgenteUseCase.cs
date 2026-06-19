using InternalHelpDeskApi.Application.Interfaces;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;
using InternalHelpDeskApi.Domain.Services;
using InternalHelpDeskApi.Infrastructure.Structures;

namespace InternalHelpDeskApi.Application
{
    public class BuscarChamadoUrgenteUseCase : IBuscarChamadoUrgenteUseCase
    {
        private readonly IChamadoRepository _chamadoRepository;
        public BuscarChamadoUrgenteUseCase(IChamadoRepository chamadoRepository)
        {
            _chamadoRepository = chamadoRepository;
        }
        public async Task<Chamados?> BuscarChamadoUrgente()
        {
            List<Chamados> chamadosAbertos = await _chamadoRepository.GetAllOpen();

            if (chamadosAbertos.Count == 0)
                return null;

            var regrasDePrioridade = new PriorityComparerUseCase();
            var filaDeAtendimento = new FilaPrioridadeHeapUseCase<Chamados>(regrasDePrioridade);

            foreach (var chamado in chamadosAbertos)
            {
                filaDeAtendimento.Enfileirar(chamado);
            }

            Chamados chamadoMaisUrgente = filaDeAtendimento.Desenfileirar();
            return chamadoMaisUrgente;
        }
    }
}
