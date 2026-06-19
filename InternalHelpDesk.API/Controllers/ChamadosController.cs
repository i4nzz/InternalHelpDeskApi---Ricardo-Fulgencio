using InternalHelpDeskApi.Application.Interfaces;
using InternalHelpDeskApi.Application.UseCases;
using InternalHelpDeskApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InternalHelpDesk.API.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento dos chamados internos de TI.
    /// </summary>
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
        private readonly IBuscarChamadoUrgenteUseCase _buscarChamadoUrgenteUseCase;
        private readonly IChamadosUrgentesUseCase _chamadosUrgentesUseCase;
        private readonly IGetChamadoByDescUseCase _getChamadoByDescUseCase;
        private readonly IGetChamadosByCPFSolicitanteUseCase _getChamadosByCPFSolicitanteUseCase;

        public ChamadosController(
            ISoftDeleteChamadoUseCase softDeleteChamado,
            IGetChamadoByIdUseCase getChamadoByIdUseCase,
            IGetAllChamadosPagedUseCase getAllChamadosPagedUseCase,
            ICriarChamadoUseCase criarChamadoUseCase,
            IUpdateChamadoUseCase updateChamadoUseCase,
            IObterListaDeChamadosOrdenadosUseCase obterListaDeChamadosOrdenadosUseCase,
            IBuscarChamadoUrgenteUseCase buscarChamadoUrgenteUseCase,
            IChamadosUrgentesUseCase chamadosUrgentesUseCase,
            IGetChamadoByDescUseCase getChamadoByDescUseCase,
            IGetChamadosByCPFSolicitanteUseCase getChamadosByCPFSolicitanteUseCase)
        {
            _softDeleteChamado = softDeleteChamado;
            _getChamadoByIdUseCase = getChamadoByIdUseCase;
            _getAllChamadosUseCase = getAllChamadosPagedUseCase;
            _criarChamadoUseCase = criarChamadoUseCase;
            _updateChamadoUseCase = updateChamadoUseCase;
            _obterListaDeChamadosOrdenadosUseCase = obterListaDeChamadosOrdenadosUseCase;
            _buscarChamadoUrgenteUseCase = buscarChamadoUrgenteUseCase;
            _chamadosUrgentesUseCase = chamadosUrgentesUseCase;
            _getChamadoByDescUseCase = getChamadoByDescUseCase;
            _getChamadosByCPFSolicitanteUseCase = getChamadosByCPFSolicitanteUseCase;
        }

        /// <summary>
        /// Lista os chamados ativos de forma paginada, sem aplicar ordenação por prioridade.
        /// </summary>
        /// <param name="pageNumber">Número da página desejada.</param>
        /// <param name="pageSize">Quantidade de registros por página.</param>
        /// <returns>Lista paginada de chamados.</returns>
        [HttpGet("chamados-ti")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDisorderedPaged(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _getAllChamadosUseCase.GetAllDisorderedPaged(pageNumber, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Consulta um chamado específico pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador do chamado.</param>
        /// <returns>Dados do chamado encontrado.</returns>
        [HttpGet("chamados-ti/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _getChamadoByIdUseCase.GetById(id);
            return Ok(result);
        }

        /// <summary>
        /// Cadastra um novo chamado interno de TI.
        /// </summary>
        /// <param name="chamado">Dados do chamado que será cadastrado.</param>
        /// <returns>Chamado criado.</returns>
        [HttpPost("chamados-ti")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarChamado([FromBody] ChamadosDto chamado)
        {
            var result = await _criarChamadoUseCase.CriarChamado(chamado);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Atualiza os dados de um chamado existente.
        /// </summary>
        /// <param name="id">Identificador do chamado que será atualizado.</param>
        /// <param name="chamado">Novos dados do chamado.</param>
        /// <returns>Resposta sem conteúdo quando a atualização é concluída.</returns>
        [HttpPut("chamados-ti/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ChamadosDto chamado)
        {
            await _updateChamadoUseCase.UpdateChamado(id, chamado);
            return NoContent();
        }

        /// <summary>
        /// Realiza a exclusão lógica de um chamado.
        /// </summary>
        /// <param name="id">Identificador do chamado que será excluído logicamente.</param>
        /// <returns>Resposta sem conteúdo quando a exclusão lógica é concluída.</returns>
        [HttpDelete("chamados-ti/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _softDeleteChamado.SoftDeleteChamado(id);
            return NoContent();
        }

        /// <summary>
        /// Busca chamados por CPF do solicitante ou por descrição.
        /// </summary>
        /// <param name="cpf">CPF do solicitante.</param>
        /// <param name="descricao">Texto da descrição do chamado.</param>
        /// <returns>Lista de chamados encontrados conforme o filtro informado.</returns>
        [HttpGet("chamados-ti/buscar")]
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

        /// <summary>
        /// Distribui o próximo chamado prioritário para atendimento.
        /// </summary>
        /// <param name="id">Identificador do atendente que receberá o chamado.</param>
        /// <returns>Chamado distribuído para atendimento.</returns>
        [HttpPost("chamados-ti/proximo/atender")]
        [ProducesResponseType(typeof(Chamados), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DistribuirChamado([FromQuery] int id)
        {
            var result = await _chamadosUrgentesUseCase.DistribuirProximoChamado(id);
            return Ok(result);
        }

        /// <summary>
        /// Consulta o próximo chamado mais urgente da fila de prioridade.
        /// </summary>
        /// <returns>Chamado mais urgente disponível.</returns>
        [HttpGet("chamados-ti/proximo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetChamadoUrgente()
        {
            var result = await _buscarChamadoUrgenteUseCase.BuscarChamadoUrgente();
            return Ok(result);
        }

        /// <summary>
        /// Lista todos os chamados ordenados pela regra de prioridade.
        /// </summary>
        /// <returns>Lista de chamados ordenados pela prioridade calculada.</returns>
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