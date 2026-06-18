namespace InternalHelpDeskApi.Application.DTOs.Solicitantes
{
    public class SolicitanteDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public bool Ativo { get; set; }
    }
}
