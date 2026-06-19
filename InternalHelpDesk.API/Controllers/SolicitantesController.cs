using InternalHelpDeskApi.Application.DTOs.Solicitantes;
using InternalHelpDeskApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InternalHelpDesk.API.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de solicitantes.
    /// </summary>
    [Route("api/chamado-ti/solicitantes")]
    [ApiController]
    public class SolicitantesController : ControllerBase
    {
        private readonly IGetSolicitanteByIdUseCase _getSolicitanteByIdUseCase;
        private readonly IGetAllSolicitantesPagedUseCase _getAllSolicitantesPagedUseCase;
        private readonly ICriarSolicitanteUseCase _criarSolicitanteUseCase;
        private readonly IUpdateSolicitanteUseCase _updateSolicitanteUseCase;
        private readonly IDeleteSolicitanteUseCase _deleteSolicitanteUseCase;

        public SolicitantesController(
            IGetSolicitanteByIdUseCase getSolicitanteByIdUseCase,
            IGetAllSolicitantesPagedUseCase getAllSolicitantesPagedUseCase,
            ICriarSolicitanteUseCase criarSolicitanteUseCase,
            IUpdateSolicitanteUseCase updateSolicitanteUseCase,
            IDeleteSolicitanteUseCase deleteSolicitanteUseCase)
        {
            _getSolicitanteByIdUseCase = getSolicitanteByIdUseCase;
            _getAllSolicitantesPagedUseCase = getAllSolicitantesPagedUseCase;
            _criarSolicitanteUseCase = criarSolicitanteUseCase;
            _updateSolicitanteUseCase = updateSolicitanteUseCase;
            _deleteSolicitanteUseCase = deleteSolicitanteUseCase;
        }

        /// <summary>
        /// Lista todos os solicitantes de forma paginada.
        /// </summary>
        /// <param name="pageNumber">Número da página desejada.</param>
        /// <param name="pageSize">Quantidade de registros por página.</param>
        /// <returns>Lista paginada de solicitantes.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPaged(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _getAllSolicitantesPagedUseCase.GetAllPaged(pageNumber, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Consulta um solicitante específico pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador do solicitante.</param>
        /// <returns>Dados do solicitante encontrado.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _getSolicitanteByIdUseCase.GetById(id);
            return Ok(result);
        }

        /// <summary>
        /// Cadastra um novo solicitante.
        /// </summary>
        /// <param name="solicitante">Dados do solicitante que será cadastrado.</param>
        /// <returns>Solicitante criado.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Criar([FromBody] SolicitanteDTO solicitante)
        {
            var result = await _criarSolicitanteUseCase.CriarSolicitante(solicitante);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Atualiza os dados de um solicitante existente.
        /// </summary>
        /// <param name="id">Identificador do solicitante que será atualizado.</param>
        /// <param name="solicitante">Novos dados do solicitante.</param>
        /// <returns>Resposta sem conteúdo quando a atualização é concluída.</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] SolicitanteDTO solicitante)
        {
            await _updateSolicitanteUseCase.UpdateSolicitante(id, solicitante);
            return NoContent();
        }

        /// <summary>
        /// Deleta um solicitante.
        /// </summary>
        /// <param name="id">Identificador do solicitante que será deletado.</param>
        /// <returns>Resposta sem conteúdo quando a deleção é concluída.</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _deleteSolicitanteUseCase.DeleteSolicitante(id);
            return NoContent();
        }
    }
}
