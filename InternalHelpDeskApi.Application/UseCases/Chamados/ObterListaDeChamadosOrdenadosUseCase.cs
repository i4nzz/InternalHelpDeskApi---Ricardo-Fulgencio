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

        public async Task<List<ChamadosDtos>> ObterListaChamadosOrdenados()
        {
            List<ChamadosDtos> chamadosAbertos = await _chamadoRepository.GetAllOpen();

            var regrasDePrioridade = new ChamadoPriorityComparer();
            var filaDeAtendimento = new FilaPrioridadeHeap<ChamadosDtos>(regrasDePrioridade);

            foreach (var chamado in chamadosAbertos)
            {
                filaDeAtendimento.Enfileirar(chamado);
            }

            var chamadosOrdenados = new List<ChamadosDtos>();

            while (!filaDeAtendimento.EstaVazia)
            {
                ChamadosDtos proximo = filaDeAtendimento.Desenfileirar();
                chamadosOrdenados.Add(proximo);
            }

            return chamadosOrdenados;
        }
    }
}