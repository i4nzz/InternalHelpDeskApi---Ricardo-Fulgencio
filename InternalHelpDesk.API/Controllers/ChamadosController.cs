using InternalHelpDeskApi.Application.Interfaces.UseCases;
using InternalHelpDeskApi.Application.UseCases;
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
        private readonly IDistribuirChamadosUseCase _distribuirChamadosUseCase;
        private readonly IGetChamadoByDescUseCase _getChamadoByDescUseCase;
        private readonly IGetChamadosByCPFSolicitanteUseCase _getChamadosByCPFSolicitanteUseCase;
        public ChamadosController
            (
            ISoftDeleteChamadoUseCase softDeleteChamado,
            IGetChamadoByIdUseCase getChamadoByIdUseCase,
            IGetAllChamadosPagedUseCase getAllChamadosPagedUseCase,
            ICriarChamadoUseCase criarChamadoUseCase,
            IUpdateChamadoUseCase updateChamadoUseCase,
            IObterListaDeChamadosOrdenadosUseCase obterListaDeChamadosOrdenadosUseCase,
            IDistribuirChamadosUseCase distribuirChamadosUseCase,
            IGetChamadoByDescUseCase getChamadoByDescUseCase,
            IGetChamadosByCPFSolicitanteUseCase getChamadosByCPFSolicitanteUseCase
            )
        {
            _softDeleteChamado = softDeleteChamado;
            _getChamadoByIdUseCase = getChamadoByIdUseCase;
            _getAllChamadosUseCase = getAllChamadosPagedUseCase;
            _criarChamadoUseCase = criarChamadoUseCase;
            _updateChamadoUseCase = updateChamadoUseCase;
            _obterListaDeChamadosOrdenadosUseCase = obterListaDeChamadosOrdenadosUseCase;
            _distribuirChamadosUseCase = distribuirChamadosUseCase;
            _getChamadoByDescUseCase = getChamadoByDescUseCase;
            _getChamadosByCPFSolicitanteUseCase = getChamadosByCPFSolicitanteUseCase;
        }

        [HttpGet("chamados-ti/GetAllDisordered")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDisordered([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var result = await _getAllChamadosUseCase.GetAllDisorderedPaged(pageNumber, pageSize);
            return Ok(result);
        }
        [HttpGet]
        [Route("chamados-ti/GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var result = await _getChamadoByIdUseCase.GetById(id);
            return Ok(result);
        }

        [HttpGet("chamados-ti/ordenados")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllOrdered()
        {
            var result = await _obterListaDeChamadosOrdenadosUseCase.ObterListaChamadosOrdenados();
            return Ok(result);
        }

        [HttpPost]
        [Route("chamados-ti")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarChamado(CriarChamadosDto chamado)
        {
            var result = await _criarChamadoUseCase.CriarChamado(chamado);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut]
        [Route("chamados-ti")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(Chamados chamado)
        {
            await _updateChamadoUseCase.UpdateChamado(chamado);
            return NoContent();
        }
        [HttpDelete]
        [Route("chamados-ti")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _softDeleteChamado.SoftDeleteChamado(id);
            return NoContent();
        }

        [HttpGet]
        [Route("chamados-ti/distribuir")]
        [ProducesResponseType(typeof(Chamados), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DistribuirChamado([FromQuery] int id)
        {
            var result = await _distribuirChamadosUseCase.DistribuirProximoChamado(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("chamados-ti/buscar-por-cpf")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCpf(string cpf)
        {
            var result = await _getChamadosByCPFSolicitanteUseCase.GetByCPF(cpf);
            return Ok(result);
        }

        [HttpGet]
        [Route("chamados-ti/buscar-por-descricao")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByDesc(string descricao)
        {
            var result = await _getChamadoByDescUseCase.GetByDesc(descricao);
            return Ok(result);
        }
    }
}
