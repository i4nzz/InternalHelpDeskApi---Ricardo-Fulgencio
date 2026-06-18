using FluentValidation.TestHelper;
using InternalHelpDeskApi.Application.UseCases;
using InternalHelpDeskApi.Domain.Enums;

namespace InternalHelpDeskApi.Tests.Application.Validators
{
    public class ChamadosDTOValidatorTests
    {
        private readonly ChamadosDtoValidator _validator;

        public ChamadosDTOValidatorTests()
        {
            _validator = new ChamadosDtoValidator();
        }

        [Fact]
        public void Validate_DevePassar_QuandoDtoForValido()
        {
            var dto = CriarDtoValido();

            var resultado = _validator.TestValidate(dto);

            resultado.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoTituloEstiverVazio()
        {
            var dto = CriarDtoValido();
            dto.Titulo = string.Empty;

            var resultado = _validator.TestValidate(dto);

            resultado.ShouldHaveValidationErrorFor(x => x.Titulo)
                .WithErrorMessage("O título do chamado é obrigatório.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoTituloTiverMaisDe100Caracteres()
        {
            var dto = CriarDtoValido();
            dto.Titulo = new string('A', 101);

            var resultado = _validator.TestValidate(dto);

            resultado.ShouldHaveValidationErrorFor(x => x.Titulo)
                .WithErrorMessage("O título deve ter no máximo 100 caracteres.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoDescricaoEstiverVazia()
        {
            var dto = CriarDtoValido();
            dto.Descricao = string.Empty;

            var resultado = _validator.TestValidate(dto);

            resultado.ShouldHaveValidationErrorFor(x => x.Descricao)
                .WithErrorMessage("A descrição do chamado é obrigatória.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoDescricaoTiverMenosDe10Caracteres()
        {
            var dto = CriarDtoValido();
            dto.Descricao = "Curta";

            var resultado = _validator.TestValidate(dto);

            resultado.ShouldHaveValidationErrorFor(x => x.Descricao)
                .WithErrorMessage("A descrição deve ter pelo menos 10 caracteres.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoCategoriaForInvalida()
        {
            var dto = CriarDtoValido();
            dto.CategoriaID = 0;

            var resultado = _validator.TestValidate(dto);

            resultado.ShouldHaveValidationErrorFor(x => x.CategoriaID)
                .WithErrorMessage("Informe uma categoria válida.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoPrioridadeForInvalida()
        {
            var dto = CriarDtoValido();
            dto.PrioridadeId = 0;

            var resultado = _validator.TestValidate(dto);

            resultado.ShouldHaveValidationErrorFor(x => x.PrioridadeId)
                .WithErrorMessage("Informe uma prioridade válida.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoSolicitanteForInvalido()
        {
            var dto = CriarDtoValido();
            dto.SolicitanteID = 0;

            var resultado = _validator.TestValidate(dto);

            resultado.ShouldHaveValidationErrorFor(x => x.SolicitanteID)
                .WithErrorMessage("Informe um solicitante válido.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoStatusForInvalido()
        {
            var dto = CriarDtoValido();
            dto.Status = (StatusChamadoEnum)999;

            var resultado = _validator.TestValidate(dto);

            resultado.ShouldHaveValidationErrorFor(x => x.Status)
                .WithErrorMessage("O status fornecido é inválido.");
        }

        private static ChamadosDto CriarDtoValido()
        {
            return new ChamadosDto
            {
                Titulo = "Servidor principal caiu",
                Descricao = "Problema crítico afetando toda a empresa.",
                CategoriaID = 1,
                PrioridadeId = 1,
                SolicitanteID = 1,
                Status = StatusChamadoEnum.Aberto
            };
        }
    }
}