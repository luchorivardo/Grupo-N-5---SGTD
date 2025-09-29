using Microsoft.AspNetCore.Mvc;
using Service.Implementations;
using Shared.DTOs.LoginDTOs;

namespace API.Controllers
{
    public class AuthController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public AuthController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var user = await _usuarioService.LoginAsync(login.Email, login.Password);

            if (user == null)
                return Unauthorized("Credenciales inválidas");

            var usuarioDTO = new UsuarioSessionDTO
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                RolUsuarioId = user.RolId
            };

            return Ok(usuarioDTO);
        }
    }
}
