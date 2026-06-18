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
        private readonly IChamadosUrgentesUseCase _ChamadosUrgentesUseCase;
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
            IChamadosUrgentesUseCase ChamadosUrgentesUseCase,
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
            _ChamadosUrgentesUseCase = ChamadosUrgentesUseCase;
            _getChamadoByDescUseCase = getChamadoByDescUseCase;
            _getChamadosByCPFSolicitanteUseCase = getChamadosByCPFSolicitanteUseCase;
        }

        [HttpGet("chamados-ti")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDisorderedPaged([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var result = await _getAllChamadosUseCase.GetAllDisorderedPaged(pageNumber, pageSize);
            return Ok(result);
        }
        [HttpGet]
        [Route("chamados-ti/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var result = await _getChamadoByIdUseCase.GetById(id);
            return Ok(result);
        }


        [HttpPost]
        [Route("chamados-ti")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarChamado(ChamadosDto chamado)
        {
            var result = await _criarChamadoUseCase.CriarChamado(chamado);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut]
        [Route("chamados-ti/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int id, ChamadosDto chamado)
        {
            await _updateChamadoUseCase.UpdateChamado(id, chamado);
            return NoContent();
        }
        [HttpDelete]
        [Route("chamados-ti")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _softDeleteChamado.SoftDeleteChamado(id);
            return NoContent();
        }

        [HttpGet]
        [Route("chamados-ti/buscar")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Buscar([FromQuery] string? cpf, [FromQuery] string? descricao)
        {
            if (!string.IsNullOrWhiteSpace(cpf))
            {
                var result = await _getChamadosByCPFSolicitanteUseCase.GetByCPF(cpf);
                return Ok(result);
            }

            if (!string.IsNullOrWhiteSpace(descricao))
            {
                var result = await _getChamadoByDescUseCase.GetByDesc(descricao);
                return Ok(result);
            }

            return BadRequest("Informe o parâmetro 'cpf' ou 'descricao' para realizar a busca.");
        }

        [HttpPost]
        [Route("chamados-ti/proximo/atender")]
        [ProducesResponseType(typeof(Chamados), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DistribuirChamado([FromQuery] int id)
        {
            var result = await _ChamadosUrgentesUseCase.DistribuirProximoChamado(id);
            return Ok(result);
        }


        [HttpGet("chamados-ti/proximo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetChamadoUrgente()
        {
            var result = await _ChamadosUrgentesUseCase.BuscarChamadoUrgente();
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

    }
}
