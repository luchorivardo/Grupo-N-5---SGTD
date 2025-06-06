using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTOs.ClienteDTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearCliente([FromBody] ClienteCreateDTO dto)
        {
            var cliente = await _clienteService.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerClientePorId), new { id = cliente.Id }, cliente);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerClietne()
        {
            var cliente = await _clienteService.ObtenerTodosAsync();
            return Ok(cliente);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerClientePorId(int id)
        {
            var cliente = await _clienteService.ObtenerPorIdAsync(id);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCliente(int id, [FromBody] ClienteUpdateDTO dto)
        {
            var cliente = await _clienteService.Editar(id, dto);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCliente(int id)
        {
            var cliente = _clienteService.Eliminar(id);
            if (cliente == null) return NotFound();
            return NoContent();
        }
    }
}
