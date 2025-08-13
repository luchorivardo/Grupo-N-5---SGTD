using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Service.Implementations;
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
            if (dto == null)
                return BadRequest("Los datos del cliente no pueden ser nulos.");

            try
            {
                var disciplina = await _disciplinaService.CrearAsync(dto);
                if (disciplina != null)
                    return CreatedAtAction(nameof(ObtenerDisciplinaPorId), new { id = disciplina.Id }, disciplina);

                return Conflict("Ya existe una disciplina con esos datos.");

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error interno al crear la disciplina.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerDisciplina()
        {
            try
            {
                var disciplina = await _disciplinaService.ObtenerTodosAsync();
                if (disciplina == null || !disciplina.Any())
                    return NoContent();

                return Ok(disciplina);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno al obtener las disciplinas.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerDisciplinaPorId(int id)
        {
            if (id <= 0)
                return BadRequest("El ID debe ser mayor a cero.");

            try
            {
                var disciplina = await _disciplinaService.ObtenerPorIdAsync(id);
                if (disciplina != null)

                    return Ok(disciplina);
                return BadRequest(disciplina);

            }
            catch (Exception)
            {
                return NotFound($"No se encontró una disciplina con ID {id}.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarDisciplina(int id, [FromBody] DisciplinaUpdateDTO dto)
        {
            if (id <= 0)
                return BadRequest("El ID debe ser mayor a cero.");
            if (dto == null)
                return BadRequest("Los datos de actualización no pueden ser nulos.");

            try
            {
                var disciplina = await _disciplinaService.Editar(id, dto);
                if (disciplina != null)
                    return Ok(disciplina);


                return BadRequest(disciplina);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno al actualizar la disciplina.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarDisciplina(int id)
        {
            try
            {
                await _disciplinaService.Eliminar(id);
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
                return StatusCode(500, "Ocurrio un error interno al eliminar la disciplina");
            }
        }
    }
}
