using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Enums;

namespace InternalHelpDeskApi.Application.UseCases.Chamados;

public class CriarChamadosDtos
{
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public Categoria Categoria { get; set; }
    public StatusChamadoEnum Status { get; set; } = StatusChamadoEnum.Aberto;
    public string? Resolucao { get; set; }
    public Solicitante Solicitante { get; set; }
    public Atendente? Atendente { get; set; } = null;
}
