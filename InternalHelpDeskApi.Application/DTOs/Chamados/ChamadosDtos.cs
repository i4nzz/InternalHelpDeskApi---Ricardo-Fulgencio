using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Enums;

namespace InternalHelpDeskApi.Application.UseCases;

public class ChamadosDto
{
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int CategoriaID { get; set; }
    public int PrioridadeId { get; set; }
    public StatusEnum Status { get; set; } = StatusEnum.Aberto;
    public int SolicitanteID { get; set; }
    public Atendente? Atendente { get; set; } = null;
}
