using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTOs.RolDTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearRol([FromBody] RolCreateDTO dto)
        {
            var rol = await _rolService.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerRolPorId), new { id = rol.Id }, rol);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerRol()
        {
            var rol = await _rolService.ObtenerTodosAsync();
            return Ok(rol);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerRolPorId(int id)
        {
            var rol = await _rolService.ObtenerPorIdAsync(id);
            if (rol == null) return NotFound();
            return Ok(rol);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarRol(int id, [FromBody] RolUpdateDTO dto)
        {
            var rol = await _rolService.Editar(id, dto);
            if (rol == null) return NotFound();
            return Ok(rol);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarAnimal(int id)
        {
            var rol = _rolService.Eliminar(id);
            if (rol == null) return NotFound();       // 404 si no estaba
            return NoContent();         // 204 si todo OK
        }
    }
}
