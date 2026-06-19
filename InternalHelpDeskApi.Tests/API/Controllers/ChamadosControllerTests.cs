using InternalHelpDesk.API.Controllers;
using InternalHelpDeskApi.Application.Interfaces;
using InternalHelpDeskApi.Application.UseCases;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InternalHelpDeskApi.Tests.API.Controllers
{
    public class ChamadosControllerTests
    {
        private readonly Mock<ISoftDeleteChamadoUseCase> _softDeleteChamadoUseCaseMock = new();
        private readonly Mock<IGetChamadoByIdUseCase> _getChamadoByIdUseCaseMock = new();
        private readonly Mock<IGetAllChamadosPagedUseCase> _getAllChamadosPagedUseCaseMock = new();
        private readonly Mock<ICriarChamadoUseCase> _criarChamadoUseCaseMock = new();
        private readonly Mock<IUpdateChamadoUseCase> _updateChamadoUseCaseMock = new();
        private readonly Mock<IObterListaDeChamadosOrdenadosUseCase> _obterListaDeChamadosOrdenadosUseCaseMock = new();
        private readonly Mock<IBuscarChamadoUrgenteUseCase> _buscarChamadoUrgenteUseCaseMock = new();
        private readonly Mock<IChamadosUrgentesUseCase> _chamadosUrgentesUseCaseMock = new();
        private readonly Mock<IGetChamadoByDescUseCase> _getChamadoByDescUseCaseMock = new();
        private readonly Mock<IGetChamadosByCPFSolicitanteUseCase> _getChamadosByCPFSolicitanteUseCaseMock = new();

        private ChamadosController CriarController()
        {
            return new ChamadosController(
                _softDeleteChamadoUseCaseMock.Object,
                _getChamadoByIdUseCaseMock.Object,
                _getAllChamadosPagedUseCaseMock.Object,
                _criarChamadoUseCaseMock.Object,
                _updateChamadoUseCaseMock.Object,
                _obterListaDeChamadosOrdenadosUseCaseMock.Object,
                _buscarChamadoUrgenteUseCaseMock.Object,
                _chamadosUrgentesUseCaseMock.Object,
                _getChamadoByDescUseCaseMock.Object,
                _getChamadosByCPFSolicitanteUseCaseMock.Object
            );
        }

        [Fact]
        public async Task CriarChamado_DeveRetornarCreatedAtAction_QuandoChamadoForCriado()
        {
            var dto = ChamadoFactory.CriarChamadoDto();
            var chamadoCriado = ChamadoFactory.CriarChamado(id: 1);

            _criarChamadoUseCaseMock
                .Setup(useCase => useCase.CriarChamado(dto))
                .ReturnsAsync(chamadoCriado);

            var controller = CriarController();

            var resultado = await controller.CriarChamado(dto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(resultado);

            Assert.Equal(nameof(ChamadosController.GetById), createdResult.ActionName);
            Assert.Equal(chamadoCriado, createdResult.Value);

            _criarChamadoUseCaseMock.Verify(
                useCase => useCase.CriarChamado(dto),
                Times.Once
            );
        }

        [Fact]
        public async Task Update_DeveRetornarNoContent_QuandoChamadoForAtualizado()
        {
            var id = 1;
            var dto = ChamadoFactory.CriarChamadoDto(
                titulo: "Servidor normalizado",
                descricao: "Chamado atualizado após correção do problema."
            );

            _updateChamadoUseCaseMock
                .Setup(useCase => useCase.UpdateChamado(id, dto))
                .Returns(Task.CompletedTask);

            var controller = CriarController();

            var resultado = await controller.Update(id, dto);

            Assert.IsType<NoContentResult>(resultado);

            _updateChamadoUseCaseMock.Verify(
                useCase => useCase.UpdateChamado(id, dto),
                Times.Once
            );
        }

        [Fact]
        public async Task Delete_DeveRetornarNoContent_QuandoChamadoForExcluidoLogicamente()
        {
            var id = 1;

            _softDeleteChamadoUseCaseMock
                .Setup(useCase => useCase.SoftDeleteChamado(id))
                .Returns(Task.CompletedTask);

            var controller = CriarController();

            var resultado = await controller.Delete(id);

            Assert.IsType<NoContentResult>(resultado);

            _softDeleteChamadoUseCaseMock.Verify(
                useCase => useCase.SoftDeleteChamado(id),
                Times.Once
            );
        }

        [Fact]
        public async Task Buscar_DeveRetornarBadRequest_QuandoCpfEDescricaoNaoForemInformados()
        {
            var controller = CriarController();

            var resultado = await controller.Buscar(null, null);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(resultado);

            Assert.Equal(
                "Informe o parâmetro 'cpf' ou 'descricao' para realizar a busca.",
                badRequestResult.Value
            );
        }

        [Fact]
        public async Task DistribuirChamado_DeveRetornarOk_QuandoChamadoForDistribuido()
        {
            var atendenteId = 1;
            var chamado = ChamadoFactory.CriarChamado(id: 10);

            _chamadosUrgentesUseCaseMock
                .Setup(useCase => useCase.DistribuirProximoChamado(atendenteId))
                .ReturnsAsync(chamado);

            var controller = CriarController();

            var resultado = await controller.DistribuirChamado(atendenteId);

            var okResult = Assert.IsType<OkObjectResult>(resultado);

            Assert.Equal(chamado, okResult.Value);

            _chamadosUrgentesUseCaseMock.Verify(
                useCase => useCase.DistribuirProximoChamado(atendenteId),
                Times.Once
            );
        }
    }
}