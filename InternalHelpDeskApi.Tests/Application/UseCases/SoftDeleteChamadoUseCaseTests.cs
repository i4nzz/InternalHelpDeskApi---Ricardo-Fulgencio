using InternalHelpDeskApi.Application;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;
using Moq;

namespace InternalHelpDeskApi.Tests.Application.UseCases
{
    public class SoftDeleteChamadoUseCaseTests
    {
        [Fact]
        public async Task SoftDeleteChamado_DeveExcluirLogicamente_QuandoChamadoExistir()
        {
            var repositoryMock = new Mock<IChamadoRepository>();

            var chamado = new Chamados
            {
                Id = 1,
                Titulo = "Servidor principal caiu",
                Descricao = "Problema crítico afetando toda a empresa.",
                CategoriaId = 1,
                PrioridadeId = 1,
                CriadoEm = DateTime.Now,
                AtualizadoEm = DateTime.Now
            };

            repositoryMock
                .Setup(repo => repo.GetById(1))
                .ReturnsAsync(chamado);

            repositoryMock
                .Setup(repo => repo.SoftDeleteAsync(chamado))
                .Returns(Task.CompletedTask);

            var useCase = new SoftDeleteChamadoUseCases(repositoryMock.Object);

            await useCase.SoftDeleteChamado(1);

            repositoryMock.Verify(repo => repo.GetById(1), Times.Once);
            repositoryMock.Verify(repo => repo.SoftDeleteAsync(chamado), Times.Once);
        }

        [Fact]
        public async Task SoftDeleteChamado_DeveLancarExcecao_QuandoChamadoNaoExistir()
        {
            var repositoryMock = new Mock<IChamadoRepository>();

            repositoryMock
                .Setup(repo => repo.GetById(99))
                .ReturnsAsync((Chamados)null!);

            var useCase = new SoftDeleteChamadoUseCases(repositoryMock.Object);

            var exception = await Assert.ThrowsAsync<Exception>(() => useCase.SoftDeleteChamado(99));

            Assert.Equal("Chamado com ID 99 não encontrado.", exception.Message);

            repositoryMock.Verify(repo => repo.GetById(99), Times.Once);
            repositoryMock.Verify(repo => repo.SoftDeleteAsync(It.IsAny<Chamados>()), Times.Never);
        }
    }
}