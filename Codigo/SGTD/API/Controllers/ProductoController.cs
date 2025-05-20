using Data.Contracts;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Producto;

namespace API.Controllers
{
    public class ProductoController : Controller
    {

        private IProductoService _ProductoRepository;

        public ProductoController(IProductoService productoRepository)
        {
            _ProductoRepository = productoRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ProductoCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _ProductoRepository.Crear(dto);
            return Ok();
        }
    }
}
