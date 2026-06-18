using InternalHelpDeskApi.Application.DTOs.Categorias;
using InternalHelpDeskApi.Application.Interfaces.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace InternalHelpDesk.API.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de categorias de chamados.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IGetCategoriaByIdUseCase _getCategoriaByIdUseCase;
        private readonly IGetAllCategoriasPagedUseCase _getAllCategoriasPagedUseCase;
        private readonly ICriarCategoriaUseCase _criarCategoriaUseCase;
        private readonly IUpdateCategoriaUseCase _updateCategoriaUseCase;
        private readonly IDeleteCategoriaUseCase _deleteCategoriaUseCase;

        public CategoriasController(
            IGetCategoriaByIdUseCase getCategoriaByIdUseCase,
            IGetAllCategoriasPagedUseCase getAllCategoriasPagedUseCase,
            ICriarCategoriaUseCase criarCategoriaUseCase,
            IUpdateCategoriaUseCase updateCategoriaUseCase,
            IDeleteCategoriaUseCase deleteCategoriaUseCase)
        {
            _getCategoriaByIdUseCase = getCategoriaByIdUseCase;
            _getAllCategoriasPagedUseCase = getAllCategoriasPagedUseCase;
            _criarCategoriaUseCase = criarCategoriaUseCase;
            _updateCategoriaUseCase = updateCategoriaUseCase;
            _deleteCategoriaUseCase = deleteCategoriaUseCase;
        }

        /// <summary>
        /// Lista todas as categorias de forma paginada.
        /// </summary>
        /// <param name="pageNumber">Número da página desejada.</param>
        /// <param name="pageSize">Quantidade de registros por página.</param>
        /// <returns>Lista paginada de categorias.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPaged(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _getAllCategoriasPagedUseCase.GetAllPaged(pageNumber, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Consulta uma categoria específica pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador da categoria.</param>
        /// <returns>Dados da categoria encontrada.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _getCategoriaByIdUseCase.GetById(id);
            return Ok(result);
        }

        /// <summary>
        /// Cadastra uma nova categoria.
        /// </summary>
        /// <param name="categoria">Dados da categoria que será cadastrada.</param>
        /// <returns>Categoria criada.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Criar([FromBody] CategoriaDTO categoria)
        {
            var result = await _criarCategoriaUseCase.CriarCategoria(categoria);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Atualiza os dados de uma categoria existente.
        /// </summary>
        /// <param name="id">Identificador da categoria que será atualizada.</param>
        /// <param name="categoria">Novos dados da categoria.</param>
        /// <returns>Resposta sem conteúdo quando a atualização é concluída.</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CategoriaDTO categoria)
        {
            await _updateCategoriaUseCase.UpdateCategoria(id, categoria);
            return NoContent();
        }

        /// <summary>
        /// Deleta uma categoria.
        /// </summary>
        /// <param name="id">Identificador da categoria que será deletada.</param>
        /// <returns>Resposta sem conteúdo quando a deleção é concluída.</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _deleteCategoriaUseCase.DeleteCategoria(id);
            return NoContent();
        }
    }
}
