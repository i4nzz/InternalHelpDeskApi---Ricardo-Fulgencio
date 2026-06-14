using InternalHelpDeskApi.Domain.Enums;

namespace InternalHelpDeskApi.Domain.Entities;

public class Atendente
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Matricula { get; private set; } = string.Empty;
    public List<CategoriaChamadoEnum> Especialidades { get; private set; } = new();
    public bool Disponivel { get; private set; }
    public DateTime CriadoEm { get; private set; }
    public bool Ativo { get; private set; }

    // Navegação EF Core
    public ICollection<Chamado> Chamados { get; private set; } = new List<Chamado>();
    protected Atendente() { }

    public Atendente(string nome, string email, string matricula, List<CategoriaChamadoEnum> especialidades)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Email = email;
        Matricula = matricula;
        Especialidades = especialidades;
        Disponivel = true;
        CriadoEm = DateTime.UtcNow;
        Ativo = true;
    }

    public void MarcarOcupado()
    {
        Disponivel = false;
    }

    public void MarcarDisponivel()
    {
        Disponivel = true;
    }
    public void Desativar()
    {
        Ativo = false;
    }

    public bool AtendeCategorias(CategoriaChamadoEnum categoria)
    {
        return Especialidades.Contains(categoria) || !Especialidades.Any();
    }
}
