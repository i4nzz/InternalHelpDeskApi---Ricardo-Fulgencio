using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Domain.Interfaces;

public interface IFuncionarioRepository
{
    Task<Funcionario?> ObterPorIdAsync(Guid id, CancellationToken ct = default);
    Task<Funcionario?> ObterPorEmailAsync(string email, CancellationToken ct = default);
    Task<IEnumerable<Funcionario>> ObterTodosAsync(CancellationToken ct = default);
    Task AdicionarAsync(Funcionario funcionario, CancellationToken ct = default);
    Task AtualizarAsync(Funcionario funcionario, CancellationToken ct = default);
    Task<int> SalvarAsync(CancellationToken ct = default);
}
