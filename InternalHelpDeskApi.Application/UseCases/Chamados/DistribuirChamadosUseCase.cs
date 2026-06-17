using InternalHelpDeskApi.Application.Interfaces.UseCases;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces; 
using InternalHelpDeskApi.Domain.Services;
using InternalHelpDeskApi.Infrastructure.Structures;

namespace InternalHelpDeskApi.Application.UseCases
{
    public class DistribuirProximoChamadoUseCase : IDistribuirChamadosUseCase
    {
        private readonly IChamadoRepository _chamadoRepository;
        private readonly IAtendenteRepository _atendenteRepository;

        public DistribuirProximoChamadoUseCase(IChamadoRepository chamadoRepository, IAtendenteRepository atendenteRepository)
        {
            _chamadoRepository = chamadoRepository;
            _atendenteRepository = atendenteRepository;
        }

        public async Task<Chamado?> DistribuirProximoChamado(int atendenteId)
        {
            var atendente = await _atendenteRepository.GetById(atendenteId);

            var chamadoUrgente = await BuscarChamadoUrgente(atendenteId);
            if (chamadoUrgente != null)
            {
                chamadoUrgente.Atendente = atendente;
                chamadoUrgente.Status = Domain.Enums.StatusChamadoEnum.EmAtendimento;
                await _chamadoRepository.UpdateAsync(chamadoUrgente);
            }
            return chamadoUrgente;
        }
        private async Task<Chamado?> BuscarChamadoUrgente(int atendenteId)
        {
            List<Chamado> chamadosAbertos = await _chamadoRepository.GetAllOpen();

            if (chamadosAbertos.Count == 0)
                return null;

            var regrasDePrioridade = new ChamadoPriorityComparer();
            var filaDeAtendimento = new FilaPrioridadeHeap<Chamado>(regrasDePrioridade);

            foreach (var chamado in chamadosAbertos)
            {
                filaDeAtendimento.Enfileirar(chamado);
            }

            Chamado chamadoMaisUrgente = filaDeAtendimento.Desenfileirar();
            return chamadoMaisUrgente;
        }

    }
}