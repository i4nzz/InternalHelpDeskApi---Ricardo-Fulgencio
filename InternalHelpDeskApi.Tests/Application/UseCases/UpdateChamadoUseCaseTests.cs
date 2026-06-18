using InternalHelpDeskApi.Application.UseCases;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Enums;
using InternalHelpDeskApi.Domain.Interfaces;
using Moq;

namespace InternalHelpDeskApi.Tests.Application.UseCases
{
    public class UpdateChamadoUseCaseTests
    {
        [Fact]
        public async Task UpdateChamado_DeveAtualizarChamadoComIdInformado()
        {
            var repositoryMock = new Mock<IChamadoRepository>();

            var id = 1;

            var dto = new ChamadosDto
            {
                Titulo = "Servidor principal normalizado",
                Descricao = "Chamado atualizado após correção do problema.",
                CategoriaID = 2,
                PrioridadeId = 3,
                SolicitanteID = 1,
                Status = StatusChamadoEnum.EmAtendimento
            };

            repositoryMock
                .Setup(repo => repo.UpdateAsync(It.IsAny<Chamados>()))
                .Returns(Task.CompletedTask);

            var useCase = new UpdateChamadoUseCase(repositoryMock.Object);

            await useCase.UpdateChamado(id, dto);

            repositoryMock.Verify(repo => repo.UpdateAsync(It.Is<Chamados>(chamado =>
                chamado.Id == id &&
                chamado.Titulo == dto.Titulo &&
                chamado.Descricao == dto.Descricao &&
                chamado.PrioridadeId == dto.PrioridadeId &&
                chamado.Status == dto.Status
            )), Times.Once);
        }
    }
}