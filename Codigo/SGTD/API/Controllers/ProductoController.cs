using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTOs.ProductoDTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromBody] ProductoCreateDTO dto)
        {
            var producto = await _productoService.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerProductoPorId), new { id = producto.Id }, producto);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProducto()
        {
            var producto = await _productoService.ObtenerTodosAsync();
            return Ok(producto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerProductoPorId(int id)
        {
            var producto = await _productoService.ObtenerPorIdAsync(id);
            if (producto == null) return NotFound();
            return Ok(producto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] ProductoUpdateDTO dto)
        {
            var producto = await _productoService.Editar(id, dto);
            if (producto == null) return NotFound();
            return Ok(producto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var producto = _productoService.Eliminar(id);
            if (producto == null) return NotFound();
            return NoContent();
        }
    }
}
