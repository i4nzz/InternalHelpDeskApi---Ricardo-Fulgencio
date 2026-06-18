namespace InternalHelpDeskApi.Domain.Entities
{
    public class Atendente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CriadoEm { get; set; }
        public DateTime AtualizadoEm { get; set; }
        public DateTime DataExclusao { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
