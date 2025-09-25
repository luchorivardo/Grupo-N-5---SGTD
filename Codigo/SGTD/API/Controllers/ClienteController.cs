using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Service.Implementations;
using Shared.DTOs.ClienteDTOs;
using Shared.Entidades;

namespace API.Controllers
{
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
                if (dto == null)
                    return BadRequest("Los datos del cliente no pueden ser nulos.");

                try
                {
                    var cliente = await _clienteService.CrearAsync(dto);
                    if (cliente != null)
                        return CreatedAtAction(nameof(ObtenerClientePorId), new { id = cliente.Id }, cliente);

                    return Conflict("Ya existe un cliente con esos datos.");

                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (Exception)
                {
                    return StatusCode(500, "Ocurrió un error interno al crear el cliente.");
                }
            }

            [HttpGet]
            public async Task<IActionResult> ObtenerCliente()
            {
                try
                {
                    var clientes = await _clienteService.ObtenerTodosAsync();
                    if (clientes == null || !clientes.Any())
                        return NoContent();

                    return Ok(clientes);
                }
                catch (Exception)
                {
                    return StatusCode(500, "Error interno al obtener los clientes.");
                }
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> ObtenerClientePorId(int id)
            {
                if (id <= 0)
                    return BadRequest("El ID debe ser mayor a cero.");

                try
                {
                    var cliente = await _clienteService.ObtenerPorIdAsync(id);
                    if (cliente != null)

                        return Ok(cliente);
                    return BadRequest( cliente);

                }
                catch (Exception)
                {
                    return NotFound($"No se encontró un cliente con ID {id}.");
                }
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> ActualizarCliente(int id, [FromBody] ClienteUpdateDTO dto)
            {
                if (id <= 0)
                    return BadRequest("El ID debe ser mayor a cero.");
                if (dto == null)
                    return BadRequest("Los datos de actualización no pueden ser nulos.");

                try
                {
                    var cliente = await _clienteService.Editar(id, dto);
                    if (cliente != null)
                        return Ok(cliente);


                    return BadRequest(cliente);
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (Exception)
                {
                    return StatusCode(500, "Error interno al actualizar el cliente.");
                }
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> EliminarCliente(int id)
            {
                if (id <= 0)
                    return BadRequest("El ID debe ser mayor a cero.");

                try
                {
                    await _clienteService.Eliminar(id);
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
                    return StatusCode(500, "Ocurrió un error interno al eliminar el cliente.");
                }
            }
        }
    }

    //[Route("api/[controller]")]
    //[ApiController]
    //public class ClienteController : ControllerBase
    //{
    //    private readonly IClienteService _clienteService;

    //    public ClienteController(IClienteService clienteService)
    //    {
    //        _clienteService = clienteService;
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> CrearCliente([FromBody] ClienteCreateDTO dto)
    //    {
    //        var cliente = await _clienteService.CrearAsync(dto);
    //        return CreatedAtAction(nameof(ObtenerClientePorId), new { id = cliente.Id }, cliente);
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> ObtenerClietne()
    //    {
    //        var cliente = await _clienteService.ObtenerTodosAsync();
    //        return Ok(cliente);
    //    }

    //    [HttpGet("{id}")]
    //    public async Task<IActionResult> ObtenerClientePorId(int id)
    //    {
    //        var cliente = await _clienteService.ObtenerPorIdAsync(id);
    //        if (cliente == null) return NotFound();
    //        return Ok(cliente);
    //    }

    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> ActualizarCliente(int id, [FromBody] ClienteUpdateDTO dto)
    //    {
    //        var cliente = await _clienteService.Editar(id, dto);
    //        if (cliente == null) return NotFound();
    //        return Ok(cliente);
    //    }

    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> EliminarCliente(int id)
    //    {
    //        try
    //        {
    //            await _clienteService.Eliminar(id);
    //            return NoContent();
    //        }
    //        catch (KeyNotFoundException ex)
    //        {
    //            return NotFound(ex.Message); // 404 si no encuentra nada el hdp
    //        }
    //        catch (ArgumentException ex)
    //        {
    //            return BadRequest(ex.Message); // 400 si el id es invalido
    //        }
    //        catch (Exception ex)
    //        {
    //            // log para ver que error tengo jajan´t :(
    //            return StatusCode(500, "Ocurrio un error interno al eliminar el cliente");
    //        }
    //    }
    //}
}
