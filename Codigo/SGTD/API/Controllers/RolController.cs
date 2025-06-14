using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Service.Implementations;
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
        public async Task<IActionResult> EliminarRol(int id)
        {
            try
            {
                await _rolService.Eliminar(id);
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
                return StatusCode(500, "Ocurrio un error interno al eliminar el rol");
            }
        }
    }
}
