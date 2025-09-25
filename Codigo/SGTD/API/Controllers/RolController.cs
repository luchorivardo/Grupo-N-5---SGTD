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
            if (dto == null)
                return BadRequest("Los datos del cliente no pueden ser nulos.");

            try
            {
                var rol = await _rolService.CrearAsync(dto);
                if (rol != null)
                    return CreatedAtAction(nameof(ObtenerRolPorId), new { id = rol.Id }, rol);

                return Conflict("Ya existe un rol con esos datos.");

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error interno al crear el rol.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerRol()
        {

            try
            {
                var rol = await _rolService.ObtenerTodosAsync();
                if (rol == null || !rol.Any())
                    return NoContent();

                return Ok(rol);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno al obtener los roles.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerRolPorId(int id)
        {
            if (id <= 0)
                return BadRequest("El ID debe ser mayor a cero.");

            try
            {
                var rol = await _rolService.ObtenerPorIdAsync(id);
                if (rol != null)

                    return Ok(rol);
                return BadRequest(rol);

            }
            catch (Exception)
            {
                return NotFound($"No se encontró un rol con ID {id}.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarRol(int id, [FromBody] RolUpdateDTO dto)
        {
            if (id <= 0)
                return BadRequest("El ID debe ser mayor a cero.");
            if (dto == null)
                return BadRequest("Los datos de actualización no pueden ser nulos.");

            try
            {
                var rol = await _rolService.Editar(id, dto);
                if (rol != null)
                    return Ok(rol);


                return BadRequest(rol);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno al actualizar el rol.");
            }
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
