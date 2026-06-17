using InternalHelpDeskApi.Application.Interfaces.UseCases;
using InternalHelpDeskApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InternalHelpDesk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChamadosController : ControllerBase
    {
        private readonly ISoftDeleteChamadoUseCase _softDeleteChamado;
        private readonly IGetChamadoByIdUseCase _getChamadoByIdUseCase;
        private readonly IGetAllChamadosPagedUseCase _getAllChamadosUseCase;
        private readonly ICriarChamadoUseCase _criarChamadoUseCase;
        private readonly IUpdateChamadoUseCase _updateChamadoUseCase;
        private readonly IObterListaDeChamadosOrdenadosUseCase _obterListaDeChamadosOrdenadosUseCase;
        public ChamadosController
            (
            ISoftDeleteChamadoUseCase softDeleteChamado,
            IGetChamadoByIdUseCase getChamadoByIdUseCase,
            IGetAllChamadosPagedUseCase getAllChamadosPagedUseCase,
            ICriarChamadoUseCase criarChamadoUseCase,
            IUpdateChamadoUseCase updateChamadoUseCase,
            IObterListaDeChamadosOrdenadosUseCase obterListaDeChamadosOrdenadosUseCase
            )
        {
            _softDeleteChamado = softDeleteChamado;
            _getChamadoByIdUseCase = getChamadoByIdUseCase;
            _getAllChamadosUseCase = getAllChamadosPagedUseCase;
            _criarChamadoUseCase = criarChamadoUseCase;
            _updateChamadoUseCase = updateChamadoUseCase;
            _obterListaDeChamadosOrdenadosUseCase = obterListaDeChamadosOrdenadosUseCase;
        }

        [HttpGet("chamados-ti")]
        public IActionResult GetAllDisordered([FromQuery]int pageNumber, [FromQuery]int pageSize)
        {
           var result = _getAllChamadosUseCase.GetAllDisorderedPaged(pageNumber, pageSize);
            return Ok(result);
        }
        [HttpGet]
        [Route("chamados-ti/{id}")]
        public IActionResult GetById([FromQuery] int id)
        {
            var result = _getChamadoByIdUseCase.GetById(id);
            return Ok(result);
        }

        [HttpGet("chamados-ti/ordenados")]
        public IActionResult GetAllOrdered()
        {
            var result = _obterListaDeChamadosOrdenadosUseCase.ObterListaChamadosOrdenados();
            return Ok(result);
        }

        [HttpPost]
        [Route("chamados-ti")]
        public IActionResult CriarChamado(Chamado chamado)
        {
            var result = _criarChamadoUseCase.CriarChamado(chamado);
            return NoContent();
        }

        [HttpPut]
        [Route("chamados-ti")]
        public IActionResult Update(Chamado chamado)
        {
            _updateChamadoUseCase.UpdateChamado(chamado);
            return NoContent();
        }
        [HttpDelete]
        [Route("chamados-ti/{id}")]
        public IActionResult Delete([FromQuery] int id)
        {
            _softDeleteChamado.SoftDeleteChamado(id);
            return NoContent();
        }
    }
}
