using InternalHelpDeskApi.Domain.Enums;

namespace InternalHelpDeskApi.Domain.Entities;

public class Funcionario
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Setor { get; private set; } = string.Empty;
    public CargoFuncionarioEnum Cargo { get; private set; }
    public DateTime CriadoEm { get; private set; }
    public bool Ativo { get; private set; }

    // EF
    protected Funcionario() { }

    public Funcionario(string nome, string email, string setor, CargoFuncionarioEnum cargo)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Email = email;
        Setor = setor;
        Cargo = cargo;
        CriadoEm = DateTime.UtcNow;
        Ativo = true;
    }

    public void Atualizar(string nome, string setor, CargoFuncionarioEnum cargo)
    {
        Nome = nome;
        Setor = setor;
        Cargo = cargo;
    }

    public void Desativar()
    {
        Ativo = false;
    }

    public void Ativar()
    {
        Ativo = true;
    }

    /// <summary>
    /// Peso do cargo no cálculo de prioridade do Heap.
    /// Diretores têm chamados com peso adicional ao score.
    /// </summary>
    public int PesoCargo()
    {
        return (int)Cargo;
    }
}
