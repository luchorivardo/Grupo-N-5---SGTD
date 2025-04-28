using Entidades.DTOs;
using Logica.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalesLogic _servicio;

        public AnimalController(IAnimalesLogic servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var lista = await _servicio.ObtenerTodosAsync();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var animal = await _servicio.ObtenerPorIdAsync(id);
            if (animal == null) return NotFound();
            return Ok(animal);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CrearAnimalDTO dto)
        {
            await _servicio.CrearAsync(dto);
            return Created("", dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CrearAnimalDTO dto)
        {
            await _servicio.EditarAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _servicio.EliminarAsync(id);
            return NoContent();
        }
    }
}

