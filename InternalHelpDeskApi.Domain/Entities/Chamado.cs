using InternalHelpDeskApi.Domain.Enums;

namespace InternalHelpDeskApi.Domain.Entities;
public class Chamados
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; }
    public int? PrioridadeId { get; set; }
    public Prioridade? Prioridade { get; set; }
    public StatusChamadoEnum Status { get; set; }
    public int? SolicitanteId { get; set; }
    public Solicitante? Solicitante { get; set; }
    public int? AtendenteId { get; set; }
    public Atendente? Atendente { get; set; } = null;
    public DateTime CriadoEm { get; set; }
    public DateTime AtualizadoEm { get; set; }
    public DateTime DataExclusao { get; set; }

}

