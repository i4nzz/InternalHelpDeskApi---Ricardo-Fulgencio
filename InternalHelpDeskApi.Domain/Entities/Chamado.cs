using InternalHelpDeskApi.Domain.Enums;

namespace InternalHelpDeskApi.Domain.Entities;
public class Chamado
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public Categoria Categoria { get; set; }
    public StatusChamadoEnum Status { get; set; }
    public DateTime AberturaEm { get; set; }
    public DateTime? InicioAtendimentoEm { get; set; }
    public DateTime? EncerramentoEm { get; set; }
    public string? Resolucao { get; set; }
    public Solicitante Solicitante { get; set; }
    public Atendente? Atendente { get; set; } = null;

}

