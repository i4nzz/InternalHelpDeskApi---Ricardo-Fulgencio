using InternalHelpDeskApi.Application.Interfaces;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Application
{
    public class DistribuirProximoChamadoUseCase : IChamadosUrgentesUseCase
    {
        private readonly IChamadoRepository _chamadoRepository;
        private readonly IAtendenteRepository _atendenteRepository;
        private readonly IPriorityComparerUseCase _priorityComparerUseCase;
        private readonly IBuscarChamadoUrgenteUseCase _buscarChamadoUrgenteUseCase;
        public DistribuirProximoChamadoUseCase(IChamadoRepository chamadoRepository, IAtendenteRepository atendenteRepository, IPriorityComparerUseCase priorityComparerUseCase, IBuscarChamadoUrgenteUseCase buscarChamadoUrgenteUseCase)
        {
            _chamadoRepository = chamadoRepository;
            _atendenteRepository = atendenteRepository;
            _priorityComparerUseCase = priorityComparerUseCase;
            _buscarChamadoUrgenteUseCase = buscarChamadoUrgenteUseCase;
        }

        public async Task<Chamados?> DistribuirProximoChamado(int atendenteId)
        {
            var atendente = await _atendenteRepository.GetById(atendenteId);

            var chamadoUrgente = await _buscarChamadoUrgenteUseCase.BuscarChamadoUrgente();
            if (chamadoUrgente != null)
            {
                chamadoUrgente.Atendente = atendente;
                chamadoUrgente.Status = Domain.Enums.StatusEnum.EmAtendimento;
                await _chamadoRepository.UpdateAsync(chamadoUrgente);
            }
            return chamadoUrgente;
        }

    }
}