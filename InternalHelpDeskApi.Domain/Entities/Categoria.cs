using InternalHelpDeskApi.Domain.Enums;

namespace InternalHelpDeskApi.Domain.Entities
{
    public class Categoria
    {
        public string Nome { get; set; } = string.Empty;
        public int Id { get; set; }
        public int Peso { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }
        public DateTime? DataExclusao { get; set; }
        public StatusEnum Status { get; set; }
    }
}
