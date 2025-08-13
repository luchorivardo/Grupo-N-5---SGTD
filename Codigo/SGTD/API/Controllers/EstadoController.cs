using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Service.Implementations;
using Shared.DTOs.EstadoDTOs;
using Shared.Entidades;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly IEstadoService _estadoService;

        public EstadoController(IEstadoService estadoService)
        {
            _estadoService = estadoService;
        }

        [HttpPost]
        public async Task<IActionResult> EstadoRol([FromBody] EstadoCreateDTO dto)
        {
            if (dto == null)
                return BadRequest("Los datos del cliente no pueden ser nulos.");

            try
            {
                var estado = await _estadoService.CrearAsync(dto);
                if (estado != null)
                    return CreatedAtAction(nameof(ObtenerEstadoPorId), new { id = estado.Id }, estado);

                return Conflict("Ya existe un estado con esos datos.");

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error interno al crear el estado.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerEstado()
        {
            try
            {
                var estado = await _estadoService.ObtenerTodosAsync();
                if (estado == null || !estado.Any())
                    return NoContent();

                return Ok(estado);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno al obtener los estados.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerEstadoPorId(int id)
        {
            if (id <= 0)
                return BadRequest("El ID debe ser mayor a cero.");

            try
            {
                var estado = await _estadoService.ObtenerPorIdAsync(id);
                if (estado != null)

                    return Ok(estado);
                return BadRequest(estado);

            }
            catch (Exception)
            {
                return NotFound($"No se encontró un estado con ID {id}.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarEstado(int id, [FromBody] EstadoUpdateDTO dto)
        {
            if (id <= 0)
                return BadRequest("El ID debe ser mayor a cero.");
            if (dto == null)
                return BadRequest("Los datos de actualización no pueden ser nulos.");

            try
            {
                var estado = await _estadoService.Editar(id, dto);
                if (estado != null)
                    return Ok(estado);


                return BadRequest(estado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno al actualizar el estado.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarEstado(int id)
        {
            try
            {
                await _estadoService.Eliminar(id);
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
                return StatusCode(500, "Ocurrio un error interno al eliminar el estado");
            }
        }
    }
}
