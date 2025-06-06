using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTOs.ProveedorDTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorService _proveedorService;

        public ProveedorController(IProveedorService proveedorService)
        {
            _proveedorService = proveedorService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearProveedor([FromBody] ProveedorCreateDTO dto)
        {
            var proveedor = await _proveedorService.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerProveedorPorId), new { id = proveedor.Id }, proveedor);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProveedor()
        {
            var proveedor = await _proveedorService.ObtenerTodosAsync();
            return Ok(proveedor);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerProveedorPorId(int id)
        {
            var proveedor = await _proveedorService.ObtenerPorIdAsync(id);
            if (proveedor == null) return NotFound();
            return Ok(proveedor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProveedor(int id, [FromBody] ProveedorUpdateDTO dto)
        {
            var proveedor = await _proveedorService.Editar(id, dto);
            if (proveedor == null) return NotFound();
            return Ok(proveedor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProveedor(int id)
        {
            var proveedor = _proveedorService.Eliminar(id);
            if (proveedor == null) return NotFound();
            return NoContent();
        }
    }
}
