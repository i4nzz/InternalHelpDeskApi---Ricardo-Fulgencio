using InternalHelpDeskApi.Application.UseCases;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Enums;
using InternalHelpDeskApi.Domain.Interfaces;
using Moq;

namespace InternalHelpDeskApi.Tests.Application.UseCases
{
    public class CriarChamadoUseCaseTests
    {
        [Fact]
        public async Task CriarChamado_DeveAdicionarChamadoERetornarChamadoCriado()
        {
            var repositoryMock = new Mock<IChamadoRepository>();

            var dto = new ChamadosDto
            {
                Titulo = "Servidor principal caiu",
                Descricao = "Problema crítico afetando toda a empresa.",
                CategoriaId = 1,
                PrioridadeId = 1,
                SolicitanteId = 1,
                Status = StatusEnum.Aberto
            };

            repositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Chamados>()))
                .ReturnsAsync((Chamados chamado) =>
                {
                    chamado.Id = 1;
                    return chamado;
                });

            var useCase = new CriarChamadoUseCase(repositoryMock.Object);

            var resultado = await useCase.CriarChamado(dto);

            Assert.NotNull(resultado);
            Assert.Equal(1, resultado.Id);
            Assert.Equal(dto.Titulo, resultado.Titulo);
            Assert.Equal(dto.Descricao, resultado.Descricao);
            Assert.Equal(dto.PrioridadeId, resultado.PrioridadeId);
            Assert.Equal(dto.Status, resultado.Status);

            repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Chamados>()), Times.Once);
        }
    }
}