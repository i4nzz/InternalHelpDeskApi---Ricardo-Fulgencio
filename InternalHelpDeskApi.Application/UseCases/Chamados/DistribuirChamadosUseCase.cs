using InternalHelpDeskApi.Application.Interfaces.UseCases;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces; 
using InternalHelpDeskApi.Domain.Services;
using InternalHelpDeskApi.Infrastructure.Structures;

namespace InternalHelpDeskApi.Application.UseCases
{
    public class DistribuirProximoChamadoUseCase : IChamadosUrgentesUseCase
    {
        private readonly IChamadoRepository _chamadoRepository;
        private readonly IAtendenteRepository _atendenteRepository;

        public DistribuirProximoChamadoUseCase(IChamadoRepository chamadoRepository, IAtendenteRepository atendenteRepository)
        {
            _chamadoRepository = chamadoRepository;
            _atendenteRepository = atendenteRepository;
        }

        public async Task<Chamados?> DistribuirProximoChamado(int atendenteId)
        {
            var atendente = await _atendenteRepository.GetById(atendenteId);

            var chamadoUrgente = await BuscarChamadoUrgente();
            if (chamadoUrgente != null)
            {
                chamadoUrgente.Atendente = atendente;
                chamadoUrgente.Status = Domain.Enums.StatusEnum.EmAtendimento;
                await _chamadoRepository.UpdateAsync(chamadoUrgente);
            }
            return chamadoUrgente;
        }
        public async Task<Chamados?> BuscarChamadoUrgente()
        {
            List<Chamados> chamadosAbertos = await _chamadoRepository.GetAllOpen();

            if (chamadosAbertos.Count == 0)
                return null;

            var regrasDePrioridade = new ChamadoPriorityComparer();
            var filaDeAtendimento = new FilaPrioridadeHeap<Chamados>(regrasDePrioridade);

            foreach (var chamado in chamadosAbertos)
            {
                filaDeAtendimento.Enfileirar(chamado);
            }

            Chamados chamadoMaisUrgente = filaDeAtendimento.Desenfileirar();
            return chamadoMaisUrgente;
        }

    }
}