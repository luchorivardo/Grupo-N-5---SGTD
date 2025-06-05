using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
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
            var rubro = _rubroService.Eliminar(id);
            if (rubro == null) return NotFound();
            return NoContent();
        }
    }
}
