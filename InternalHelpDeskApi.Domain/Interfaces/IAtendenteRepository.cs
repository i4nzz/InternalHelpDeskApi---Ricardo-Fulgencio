using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Enums;

namespace InternalHelpDeskApi.Domain.Interfaces;

public interface IAtendenteRepository
{
    Task<Atendente?> ObterPorIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<Atendente>> ObterTodosAsync(CancellationToken ct = default);
    Task<IEnumerable<Atendente>> ObterDisponiveisAsync(CancellationToken ct = default);
    Task<Atendente?> ObterDisponivelParaCategoriaAsync(CategoriaChamadoEnum categoria, CancellationToken ct = default);
    Task AdicionarAsync(Atendente atendente, CancellationToken ct = default);
    Task AtualizarAsync(Atendente atendente, CancellationToken ct = default);
    Task<int> SalvarAsync(CancellationToken ct = default);
}
