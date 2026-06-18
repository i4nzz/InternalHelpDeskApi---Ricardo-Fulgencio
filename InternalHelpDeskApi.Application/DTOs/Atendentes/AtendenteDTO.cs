namespace InternalHelpDeskApi.Application.DTOs.Atendentes
{
    public class AtendenteDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Ativo { get; set; }
    }
}
