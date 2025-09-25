﻿using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Service.Implementations;
using Shared.DTOs.RubroDTOs;
using Shared.DTOs.UsuarioDTOs;
using Shared.Entidades;

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
            if (dto == null)
                return BadRequest("Los datos del cliente no pueden ser nulos.");

            try
            {
                var usuario = await _usuarioService.CrearAsync(dto);
                if (usuario != null)
                    return CreatedAtAction(nameof(ObtenerUsuarioPorId), new { id = usuario.Id }, usuario);

                return Conflict("Ya existe un usuario con esos datos.");

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error interno al crear el usuario.");
            }

        }

        [HttpGet]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            try
            {
                var usuario = await _usuarioService.ObtenerTodosAsync();
                if (usuario == null || !usuario.Any())
                    return NoContent();

                return Ok(usuario);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno al obtener los usuario.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerUsuarioPorId(int id)
        {
            if (id <= 0)
                return BadRequest("El ID debe ser mayor a cero.");

            try
            {
                var usuario = await _usuarioService.ObtenerPorIdAsync(id);
                if (usuario != null)

                    return Ok(usuario);
                return BadRequest(usuario);

            }
            catch (Exception)
            {
                return NotFound($"No se encontró un usuario con ID {id}.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(int id, [FromBody] UsuarioUpdateDTO dto)
        {
            if (id <= 0)
                return BadRequest("El ID debe ser mayor a cero.");
            if (dto == null)
                return BadRequest("Los datos de actualización no pueden ser nulos.");

            try
            {
                var usuario = await _usuarioService.Editar(id, dto);
                if (usuario != null)
                    return Ok(usuario);


                return BadRequest(usuario);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno al actualizar el usuario.");
            }
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
