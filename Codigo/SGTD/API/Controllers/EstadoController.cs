using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
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
            var rol = _estadoService.Eliminar(id);
            if (rol == null) return NotFound();       // 404 si no estaba
            return NoContent();         // 204 si todo OK
        }
    }
}
