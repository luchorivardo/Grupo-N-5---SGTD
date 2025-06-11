using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Service.Implementations;
using Shared.DTOs.FacturaDTOs;
using Shared.DTOs.ProductoDTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private IFacturaService _facturaService;

        public FacturaController (IFacturaService facturaService)
        {
            _facturaService = facturaService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearFactura([FromBody] FacturaCreateDTO dto)
        {
            var factura = await _facturaService.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerFacturaPorId), new { id = factura.Id }, factura);

        }


        [HttpGet]
        public async Task<IActionResult> ObtenerFactura()
        {
            var factura = await _facturaService.ObtenerTodosAsync();
            return Ok(factura);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerFacturaPorId(int id)
        {
            var factura = await _facturaService.ObtenerPorIdAsync(id);
            if (factura == null) return NotFound();
            return Ok(factura);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarFactura(int id, [FromBody] FacturaUpdateDTO dto)
        {
            var factura = await _facturaService.Editar(id, dto);
            if (factura == null) return NotFound();
            return Ok(factura);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarFactura(int id)
        {
            try
            {
                await _facturaService.Eliminar(id);
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
                return StatusCode(500, "Ocurrio un error interno al eliminar la factura");
            }

        }
    }
}
