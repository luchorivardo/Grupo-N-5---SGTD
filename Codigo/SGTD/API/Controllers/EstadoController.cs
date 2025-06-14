using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Service.Implementations;
using Shared.DTOs.EstadoDTOs;

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
            var estado = await _estadoService.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerEstadoPorId), new { id = estado.Id }, estado);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerEstado()
        {
            var estado = await _estadoService.ObtenerTodosAsync();
            return Ok(estado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerEstadoPorId(int id)
        {
            var estado = await _estadoService.ObtenerPorIdAsync(id);
            if (estado == null) return NotFound();
            return Ok(estado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarEstado(int id, [FromBody] EstadoUpdateDTO dto)
        {
            var estado = await _estadoService.Editar(id, dto);
            if (estado == null) return NotFound();
            return Ok(estado);
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
