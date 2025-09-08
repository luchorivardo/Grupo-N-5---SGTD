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
            if (dto == null)
                return BadRequest("Los datos del cliente no pueden ser nulos.");

            try
            {
                var factura = await _facturaService.CrearAsync(dto);
                if (factura != null)
                    return CreatedAtAction(nameof(ObtenerFacturaPorId), new { id = factura.Id }, factura);
               
                return Conflict("Ya existe una factura con esos datos.");
                
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error interno al crear la factura.");
            }

        }


        [HttpGet]
        public async Task<IActionResult> ObtenerFactura()
        {
            try
            {
                var facturas = await _facturaService.ObtenerTodosAsync();
                if (facturas == null || !facturas.Any())
                    return NoContent();
                return Ok(facturas);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error interno al obtener las facturas.");
            }

        }
           

            [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerFacturaPorId(int id)
        {
            if (id <= 0)
                return BadRequest("El ID debe ser mayor a cero.");
            try
            {
                var factura = await _facturaService.ObtenerPorIdAsync(id);
                if (factura != null)
                    return Ok(factura);
                return BadRequest(factura);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); 
            }
            catch (Exception)
            {
                return NotFound($"No se encontró un cliente con ID {id}.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarFactura(int id, [FromBody] FacturaUpdateDTO dto)
        {
            if (id <= 0)
                return BadRequest("El ID debe ser mayor a cero.");
            if (dto == null)
                return BadRequest("Los datos de actualización no pueden ser nulos.");
            try
            {
                var factura = await _facturaService.Editar(id, dto);
                if (factura != null)
                    return Ok(factura);


                return BadRequest(factura);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno al actualizar la factura.");
            }
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
