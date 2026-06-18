using InternalHelpDeskApi.Application.Interfaces.UseCases;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;
using InternalHelpDeskApi.Domain.Services;
using InternalHelpDeskApi.Infrastructure.Structures;

namespace InternalHelpDeskApi.Application.UseCases
{
    public class ObterListaDeChamadosOrdenadosUseCase : IObterListaDeChamadosOrdenadosUseCase
    {
        private readonly IChamadoRepository _chamadoRepository;

        public ObterListaDeChamadosOrdenadosUseCase(IChamadoRepository chamadoRepository)
        {
            _chamadoRepository = chamadoRepository;
        }

        public async Task<List<Chamados>> ObterListaChamadosOrdenados()
        {
            List<Chamados> chamadosAbertos = await _chamadoRepository.GetAllOpen();

            var regrasDePrioridade = new ChamadoPriorityComparer();
            var filaDeAtendimento = new FilaPrioridadeHeap<Chamados>(regrasDePrioridade);

            foreach (var chamado in chamadosAbertos)
            {
                filaDeAtendimento.Enfileirar(chamado);
            }

            var chamadosOrdenados = new List<Chamados>();

            while (!filaDeAtendimento.EstaVazia)
            {
                Chamados proximo = filaDeAtendimento.Desenfileirar();
                chamadosOrdenados.Add(proximo);
            }

            return chamadosOrdenados;
        }
    }
}