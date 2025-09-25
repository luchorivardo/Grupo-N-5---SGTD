using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Service.Implementations;
using Shared.DTOs.RubroDTOs;
using Shared.Entidades;

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
            if (dto == null)
                return BadRequest("Los datos del cliente no pueden ser nulos.");

            try
            {
                var rubro = await _rubroService.CrearAsync(dto);
                if (rubro != null)
                    return CreatedAtAction(nameof(ObtenerRubroPorId), new { id = rubro.Id }, rubro);

                return Conflict("Ya existe un rubro con esos datos.");

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error interno al crear el cliente.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerRubro()
        {
            try
            {
                var rubro = await _rubroService.ObtenerTodosAsync();
                if (rubro == null || !rubro.Any())
                    return NoContent();

                return Ok(rubro);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno al obtener los rubros.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerRubroPorId(int id)
        {
            if (id <= 0)
                return BadRequest("El ID debe ser mayor a cero.");

            try
            {
                var rubro = await _rubroService.ObtenerPorIdAsync(id);
                if (rubro != null)

                    return Ok(rubro);
                return BadRequest(rubro);

            }
            catch (Exception)
            {
                return NotFound($"No se encontró un rubro con ID {id}.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarRubro(int id, [FromBody] RubroUpdateDTO dto)
        {
            if (id <= 0)
                return BadRequest("El ID debe ser mayor a cero.");
            if (dto == null)
                return BadRequest("Los datos de actualización no pueden ser nulos.");

            try
            {
                var rubro = await _rubroService.Editar(id, dto);
                if (rubro != null)
                    return Ok(rubro);


                return BadRequest(rubro);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno al actualizar el rubro.");
            }
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
