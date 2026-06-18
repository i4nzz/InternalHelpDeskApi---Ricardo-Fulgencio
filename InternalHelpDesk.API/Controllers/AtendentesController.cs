using InternalHelpDeskApi.Application.DTOs.Atendentes;
using InternalHelpDeskApi.Application.Interfaces.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace InternalHelpDesk.API.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de atendentes.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AtendentesController : ControllerBase
    {
        private readonly IGetAtendenteByIdUseCase _getAtendenteByIdUseCase;
        private readonly IGetAllAtendentesPagedUseCase _getAllAtendentesPagedUseCase;
        private readonly ICriarAtendenteUseCase _criarAtendenteUseCase;
        private readonly IUpdateAtendenteUseCase _updateAtendenteUseCase;
        private readonly IDeleteAtendenteUseCase _deleteAtendenteUseCase;

        public AtendentesController(
            IGetAtendenteByIdUseCase getAtendenteByIdUseCase,
            IGetAllAtendentesPagedUseCase getAllAtendentesPagedUseCase,
            ICriarAtendenteUseCase criarAtendenteUseCase,
            IUpdateAtendenteUseCase updateAtendenteUseCase,
            IDeleteAtendenteUseCase deleteAtendenteUseCase)
        {
            _getAtendenteByIdUseCase = getAtendenteByIdUseCase;
            _getAllAtendentesPagedUseCase = getAllAtendentesPagedUseCase;
            _criarAtendenteUseCase = criarAtendenteUseCase;
            _updateAtendenteUseCase = updateAtendenteUseCase;
            _deleteAtendenteUseCase = deleteAtendenteUseCase;
        }

        /// <summary>
        /// Lista todos os atendentes de forma paginada.
        /// </summary>
        /// <param name="pageNumber">Número da página desejada.</param>
        /// <param name="pageSize">Quantidade de registros por página.</param>
        /// <returns>Lista paginada de atendentes.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPaged(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _getAllAtendentesPagedUseCase.GetAllPaged(pageNumber, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Consulta um atendente específico pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador do atendente.</param>
        /// <returns>Dados do atendente encontrado.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _getAtendenteByIdUseCase.GetById(id);
            return Ok(result);
        }

        /// <summary>
        /// Cadastra um novo atendente.
        /// </summary>
        /// <param name="atendente">Dados do atendente que será cadastrado.</param>
        /// <returns>Atendente criado.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Criar([FromBody] AtendenteDTO atendente)
        {
            var result = await _criarAtendenteUseCase.CriarAtendente(atendente);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Atualiza os dados de um atendente existente.
        /// </summary>
        /// <param name="id">Identificador do atendente que será atualizado.</param>
        /// <param name="atendente">Novos dados do atendente.</param>
        /// <returns>Resposta sem conteúdo quando a atualização é concluída.</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] AtendenteDTO atendente)
        {
            await _updateAtendenteUseCase.UpdateAtendente(id, atendente);
            return NoContent();
        }

        /// <summary>
        /// Deleta um atendente.
        /// </summary>
        /// <param name="id">Identificador do atendente que será deletado.</param>
        /// <returns>Resposta sem conteúdo quando a deleção é concluída.</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _deleteAtendenteUseCase.DeleteAtendente(id);
            return NoContent();
        }
    }
}
