using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Enums;

namespace InternalHelpDeskApi.Domain.Interfaces;

public interface IChamadoRepository
{
    Task<Chamado?> ObterPorIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<Chamado>> ObterTodosAsync(CancellationToken ct = default);
    Task<IEnumerable<Chamado>> ObterAbertosOrdenadosPorPrioridadeAsync(CancellationToken ct = default);
    Task<IEnumerable<Chamado>> ObterPorSolicitanteAsync(Guid solicitanteId, CancellationToken ct = default);
    Task<IEnumerable<Chamado>> ObterPorStatusAsync(StatusChamadoEnum status, CancellationToken ct = default);
    Task AdicionarAsync(Chamado chamado, CancellationToken ct = default);
    Task AtualizarAsync(Chamado chamado, CancellationToken ct = default);
    Task<int> SalvarAsync(CancellationToken ct = default);
}
