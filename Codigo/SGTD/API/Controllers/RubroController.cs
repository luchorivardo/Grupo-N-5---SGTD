using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Service.Implementations;
using Shared.DTOs.RubroDTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RubroController : ControllerBase
    {
        private readonly IRubroService _rubroService;

        public RubroController(IRubroService rubroService)
        {
            _rubroService = rubroService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearRubro([FromBody] RubroCreateDTO dto)
        {
            var rubro = await _rubroService.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerRubroPorId), new { id = rubro.Id }, rubro);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerRubro()
        {
            var rubro = await _rubroService.ObtenerTodosAsync();
            return Ok(rubro);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerRubroPorId(int id)
        {
            var rubro = await _rubroService.ObtenerPorIdAsync(id);
            if (rubro == null) return NotFound();
            return Ok(rubro);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarRubro(int id, [FromBody] RubroUpdateDTO dto)
        {
            var rubro = await _rubroService.Editar(id, dto);
            if (rubro == null) return NotFound();
            return Ok(rubro);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarRubro(int id)
        {
            try
            {
                await _rubroService.Eliminar(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // 404 si no encuentra nada el hdp
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // 400 si el id es invalido
            }
            catch (Exception ex)
            {
                // log para ver que error tengo jajan´t :(
                return StatusCode(500, "Ocurrio un error interno al eliminar el rubro");
            }
        }
    }
}
