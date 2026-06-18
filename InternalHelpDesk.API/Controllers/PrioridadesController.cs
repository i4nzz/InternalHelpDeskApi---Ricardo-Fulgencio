using InternalHelpDeskApi.Application.DTOs.Prioridades;
using InternalHelpDeskApi.Application.Interfaces.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace InternalHelpDesk.API.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de prioridades de chamados.
    /// </summary>
    [Route("api/chamado-ti/prioridades")]
    [ApiController]
    public class PrioridadesController : ControllerBase
    {
        private readonly IGetPrioridadeByIdUseCase _getPrioridadeByIdUseCase;
        private readonly IGetAllPrioridadesPagedUseCase _getAllPrioridadesPagedUseCase;
        private readonly ICriarPrioridadeUseCase _criarPrioridadeUseCase;
        private readonly IUpdatePrioridadeUseCase _updatePrioridadeUseCase;
        private readonly IDeletePrioridadeUseCase _deletePrioridadeUseCase;

        public PrioridadesController(
            IGetPrioridadeByIdUseCase getPrioridadeByIdUseCase,
            IGetAllPrioridadesPagedUseCase getAllPrioridadesPagedUseCase,
            ICriarPrioridadeUseCase criarPrioridadeUseCase,
            IUpdatePrioridadeUseCase updatePrioridadeUseCase,
            IDeletePrioridadeUseCase deletePrioridadeUseCase)
        {
            _getPrioridadeByIdUseCase = getPrioridadeByIdUseCase;
            _getAllPrioridadesPagedUseCase = getAllPrioridadesPagedUseCase;
            _criarPrioridadeUseCase = criarPrioridadeUseCase;
            _updatePrioridadeUseCase = updatePrioridadeUseCase;
            _deletePrioridadeUseCase = deletePrioridadeUseCase;
        }

        /// <summary>
        /// Lista todas as prioridades de forma paginada.
        /// </summary>
        /// <param name="pageNumber">Número da página desejada.</param>
        /// <param name="pageSize">Quantidade de registros por página.</param>
        /// <returns>Lista paginada de prioridades.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPaged(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _getAllPrioridadesPagedUseCase.GetAllPaged(pageNumber, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Consulta uma prioridade específica pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador da prioridade.</param>
        /// <returns>Dados da prioridade encontrada.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _getPrioridadeByIdUseCase.GetById(id);
            return Ok(result);
        }

        /// <summary>
        /// Cadastra uma nova prioridade.
        /// </summary>
        /// <param name="prioridade">Dados da prioridade que será cadastrada.</param>
        /// <returns>Prioridade criada.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Criar([FromBody] PrioridadeDTO prioridade)
        {
            var result = await _criarPrioridadeUseCase.CriarPrioridade(prioridade);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Atualiza os dados de uma prioridade existente.
        /// </summary>
        /// <param name="id">Identificador da prioridade que será atualizada.</param>
        /// <param name="prioridade">Novos dados da prioridade.</param>
        /// <returns>Resposta sem conteúdo quando a atualização é concluída.</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PrioridadeDTO prioridade)
        {
            await _updatePrioridadeUseCase.UpdatePrioridade(id, prioridade);
            return NoContent();
        }

        /// <summary>
        /// Deleta uma prioridade.
        /// </summary>
        /// <param name="id">Identificador da prioridade que será deletada.</param>
        /// <returns>Resposta sem conteúdo quando a deleção é concluída.</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _deletePrioridadeUseCase.DeletePrioridade(id);
            return NoContent();
        }
    }
}
