namespace InternalHelpDeskApi.Domain.Entities
{
    public class Prioridade
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int Peso { get; set; }
        public Categoria Categoria { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }
}
