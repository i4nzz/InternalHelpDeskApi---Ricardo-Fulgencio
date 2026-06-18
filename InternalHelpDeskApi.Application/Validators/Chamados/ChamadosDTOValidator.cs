using FluentValidation;

namespace InternalHelpDeskApi.Application.UseCases;

public class ChamadosDtoValidator : AbstractValidator<ChamadosDto>
{
    public ChamadosDtoValidator()
    {
        RuleFor(x => x.Titulo)
            .NotEmpty().WithMessage("O título do chamado é obrigatório.")
            .MaximumLength(100).WithMessage("O título deve ter no máximo 100 caracteres.");

        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("A descrição do chamado é obrigatória.")
            .MinimumLength(10).WithMessage("A descrição deve ter pelo menos 10 caracteres.");

        RuleFor(x => x.CategoriaId)
            .GreaterThan(0).WithMessage("Informe uma categoria válida.");

        RuleFor(x => x.PrioridadeId)
            .GreaterThan(0).WithMessage("Informe uma prioridade válida.");

        RuleFor(x => x.SolicitanteId)
            .GreaterThan(0).WithMessage("Informe um solicitante válido.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("O status fornecido é inválido.");
    }
}