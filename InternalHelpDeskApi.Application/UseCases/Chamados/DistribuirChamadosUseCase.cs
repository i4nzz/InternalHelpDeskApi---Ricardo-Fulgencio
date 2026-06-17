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

        public async Task<ChamadosDtos?> DistribuirProximoChamado(int atendenteId)
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
        private async Task<ChamadosDtos?> BuscarChamadoUrgente(int atendenteId)
        {
            List<ChamadosDtos> chamadosAbertos = await _chamadoRepository.GetAllOpen();

            if (chamadosAbertos.Count == 0)
                return null;

            var regrasDePrioridade = new ChamadoPriorityComparer();
            var filaDeAtendimento = new FilaPrioridadeHeap<ChamadosDtos>(regrasDePrioridade);

            foreach (var chamado in chamadosAbertos)
            {
                filaDeAtendimento.Enfileirar(chamado);
            }

            ChamadosDtos chamadoMaisUrgente = filaDeAtendimento.Desenfileirar();
            return chamadoMaisUrgente;
        }

    }
}