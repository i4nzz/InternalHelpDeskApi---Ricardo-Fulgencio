namespace InternalHelpDeskApi.Application.DTOs.Prioridades
{
    public class PrioridadeDTO
    {
        public int Id { get; set; }
        public int CategoriaId { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int Peso { get; set; }
    }
}
