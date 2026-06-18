using InternalHelpDeskApi.Domain.Enums;

namespace InternalHelpDeskApi.Domain.Entities
{
    public class Prioridade
    {
        public int Id { get; set; }
        public int CategoriaId { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public StatusEnum Status { get; set; } = string.Empty;
        public int Peso { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }
        public DateTime? DataExclusao { get; set; }
    }
}
