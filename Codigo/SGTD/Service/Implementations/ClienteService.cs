using Data.Contracts;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.IdentityModel.Tokens;
using Shared.DTOs.ClienteDTOs;
using Shared.DTOs.Producto;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Data.Implementations
{
    public class ClienteService : IClienteService
    {
        private IClienteRepository _clienteRepository;
        private readonly ClienteMapper _mapper = new ClienteMapper();

        public ClienteService(IClienteRepository productoRepository)
        {
            _clienteRepository = productoRepository;
        }

        public async Task<List<ClienteReadDTO>> ObtenerTodosAsync()
        {
            var clientes = await _clienteRepository.FindAllAsync();
            return clientes.Select(a => new ClienteReadDTO
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Dni = a.Dni,
                Telefono = a.Telefono,
                Direccion = a.Direccion
            }).ToList();
        }

        public async Task<ClienteReadDTO> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");

            var clientes = await _clienteRepository.ObtenerPorId(id);
            if (clientes == null)
                throw new KeyNotFoundException($"No se encontró ningún estado con ID {id}.");

            return new ClienteReadDTO
            {
                Id = clientes.Id,
                Nombre = clientes.Nombre,
                Dni = clientes.Dni,
                Telefono = clientes.Telefono,
                Direccion = clientes.Direccion,
            };
        }

        public async Task<ClienteReadDTO> CrearAsync(ClienteCreateDTO dto)
        {
            ValidarClienteCreateDTO(dto);

            var cliente = _mapper.ToEntity(dto);
            await _clienteRepository.Create(cliente);

            return _mapper.ToReadDto(cliente);
        }

        public async Task<ClienteReadDTO> Editar(int id, ClienteUpdateDTO dto)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");

            ValidarClienteUpdateDTO(dto);

            var cliente = await _clienteRepository.ObtenerPorId(id);
            if (cliente == null)
                throw new KeyNotFoundException($"No se encontró ningún cliente con ID {id}.");

            _mapper.UpdateEntity(dto, cliente);

            await _clienteRepository.Update(cliente);

            return _mapper.ToReadDto(cliente);
        }

        public async Task Eliminar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");

            var cliente = await _clienteRepository.ObtenerPorId(id);
            if (cliente == null)
                throw new KeyNotFoundException($"No se encontró ningún cliente con ID {id}.");

            _clienteRepository.Delete(cliente);
        }


        private void ValidarClienteCreateDTO(ClienteCreateDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Nombre))
                throw new ArgumentException("El nombre del cliente es obligatorio.");
            if (dto.Dni > 99999999)
                throw new ArgumentException("El DNI debe contener 7 números como máximo.");
            if (!EsTelefonoValido(dto.Telefono))
                throw new ArgumentException("El numero de telefono deve comenzar con + y contener entre 7 y 15 caracteres.");
            if (string.IsNullOrEmpty(dto.Direccion))
                throw new ArgumentException("La dirección del cliente es obligatoria.");
            if (dto.CiudadId <= 0)
                throw new ArgumentException("Debe seleccionar una ciudad");
        }

        private void ValidarClienteUpdateDTO(ClienteUpdateDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Nombre))
                throw new ArgumentException("El nombre del cliente es obligatorio.");
            if (dto.Dni > 99999999)
                throw new ArgumentException("El DNI debe contener 7 números como máximo.");
            if (!EsTelefonoValido(dto.Telefono))
                throw new ArgumentException("El numero de telefono deve comenzar con + y contener entre 7 y 15 caracteres.");
            if (string.IsNullOrEmpty(dto.Direccion))
                throw new ArgumentException("La dirección del cliente es obligatoria.");
            if (dto.CiudadId <= 0)
                throw new ArgumentException("Debe seleccionar una ciudad");
        }

        public bool EsTelefonoValido(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                return false;

            var regex = new Regex(@"^\+\d{7,15}$");
            return regex.IsMatch(telefono);
        }
    }
}
