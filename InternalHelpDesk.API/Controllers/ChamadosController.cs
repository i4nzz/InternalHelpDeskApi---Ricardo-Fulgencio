using Microsoft.AspNetCore.Mvc;

namespace InternalHelpDesk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChamadosController : ControllerBase
    {
        [HttpGet()]
        public IActionResult GetAll()
        {
            return Ok();
        }
        [HttpGet("GetById{id}")]
        public IActionResult GetById(int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult CriarChamado()
        {
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok();
        }
        [HttpPut]
        public IActionResult Update()
        {
            return Ok();
        }
        public IActionResult GetAll()
        {
            return Ok();
        }
    }
}
