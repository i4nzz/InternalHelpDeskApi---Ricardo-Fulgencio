namespace InternalHelpDeskApi.Application.DTOs.Atendentes
{
    public class AtendenteDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Ativo { get; set; }
    }
}
