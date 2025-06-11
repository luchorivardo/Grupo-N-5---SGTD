using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Service.Implementations;
using Shared.DTOs.RubroDTOs;
using Shared.DTOs.UsuarioDTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioService _usuarioService;

        public UsuarioController (IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] UsuarioCreateDTO dto)
        {
          var usuario = await _usuarioService.CrearAsync(dto);
          return CreatedAtAction(nameof(ObtenerUsuarioPorId), new { id = usuario.Id }, usuario);
     
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerUsuarios()
        {
          var usuarios = await _usuarioService.ObtenerTodosAsync();
          return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerUsuarioPorId(int id)
        {
          var usuario = await _usuarioService.ObtenerPorIdAsync(id);
            return Ok(usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(int id, [FromBody] UsuarioUpdateDTO dto)
        {
           var usuario = await _usuarioService.Editar(id, dto);
           return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            try
            {
                await _usuarioService.Eliminar(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error interno al eliminar el usuario.");
            }
        }

    }
}
