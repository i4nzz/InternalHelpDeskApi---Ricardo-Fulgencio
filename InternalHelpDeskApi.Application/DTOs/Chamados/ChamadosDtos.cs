using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Enums;

namespace InternalHelpDeskApi.Application.UseCases;

public class CriarChamadosDto
{
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int CategoriaID { get; set; }
    public StatusChamadoEnum Status { get; set; } = StatusChamadoEnum.Aberto;
    public int SolicitanteID { get; set; }
    public Atendente? Atendente { get; set; } = null;
}
