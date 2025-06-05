using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTOs.DisciplinaDTOs;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplinaController : ControllerBase
    {
        private readonly IDisciplinaService _disciplinaService;

        public DisciplinaController(IDisciplinaService disciplinaService)
        {
            _disciplinaService = disciplinaService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearDisciplina([FromBody] DisciplinaCreateDTO dto)
        {
            var disciplina = await _disciplinaService.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerDisciplinaPorId), new { id = disciplina.Id }, disciplina);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerDisciplina()
        {
            var disciplina = await _disciplinaService.ObtenerTodosAsync();
            return Ok(disciplina);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerDisciplinaPorId(int id)
        {
            var disciplina = await _disciplinaService.ObtenerPorIdAsync(id);
            if (disciplina == null) return NotFound();
            return Ok(disciplina);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarDisciplina(int id, [FromBody] DisciplinaUpdateDTO dto)
        {
            var disciplina = await _disciplinaService.Editar(id, dto);
            if (disciplina == null) return NotFound();
            return Ok(disciplina);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarDisciplina(int id)
        {
            var disciplina = _disciplinaService.Eliminar(id);
            if (disciplina == null) return NotFound();
            return NoContent();
        }
    }
}
