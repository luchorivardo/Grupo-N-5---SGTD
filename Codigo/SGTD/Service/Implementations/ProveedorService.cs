﻿using Data.Contracts;
using Service.Contracts;
using Shared.DTOs.ProveedorDTOs;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class ProveedorService : IProveedorService
    {
        private IProveedorRepository _proveedorRepository;
        private readonly ProveedorMapper _mapper = new ProveedorMapper();

        public ProveedorService(IProveedorRepository proveedorRepository)
        {
            _proveedorRepository = proveedorRepository;
        }

        public async Task<List<ProveedorReadDTO>> ObtenerTodosAsync()
        {
            var proveedores = await _proveedorRepository.FindAllAsync();

            return _mapper.ToReadDtoList(proveedores);
        }

        public async Task<ProveedorReadDTO> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");

            var proveedor = await _proveedorRepository.ObtenerPorId(id);
            if (proveedor == null)
                throw new KeyNotFoundException($"No se encontró ningún proveedor con ID {id}.");

            return _mapper.ToReadDto(proveedor);
        }
        public async Task<ProveedorReadDTO> CrearAsync(ProveedorCreateDTO dto)
        {
            ValidarProveedorCreateDTO(dto);

            var proveedor = _mapper.ToEntity(dto);
            _mapper.MapRubros(dto, proveedor);
            await _proveedorRepository.Create(proveedor);

            return _mapper.ToReadDto(proveedor);
        }

        public async Task<ProveedorReadDTO> Editar(int id, ProveedorUpdateDTO dto)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");

            ValidarProveedorUpdateDTO(dto);

            var proveedor = await _proveedorRepository.ObtenerPorId(id);
            if (proveedor == null)
                throw new KeyNotFoundException($"No se encontró ningún proveedor con ID {id}.");

            proveedor.UpdatedDate = DateTime.Now;
            _mapper.UpdateEntity(dto, proveedor);
            await _proveedorRepository.Update(proveedor);

            return _mapper.ToReadDto(proveedor);
        }

        public async Task Eliminar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");

            var proveedor = await _proveedorRepository.ObtenerPorId(id);
            if (proveedor == null)
                throw new KeyNotFoundException($"No se encontró ningún proveedor con ID {id}.");

            await _proveedorRepository.Delete(proveedor);
        }

        private void ValidarProveedorCreateDTO(ProveedorCreateDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");
            if (!EsCuitValido(dto.Cuit))
                throw new ArgumentException("El CUIT no es válido. Formato esperado: XX-XXXXXXXX-X");
            if (string.IsNullOrWhiteSpace(dto.Direccion))
                throw new ArgumentException("La dirección es obligatoria.");
            if (!EsCorreoValido(dto.Correo))
                throw new ArgumentException("El correo no es válido.");
            if (!EsTelefonoValido(dto.Telefono))
                throw new ArgumentException("El número de teléfono debe comenzar con + y tener entre 7 y 15 dígitos.");
            if (string.IsNullOrWhiteSpace(dto.Ciudad))
                throw new ArgumentException("La ciudad es obligatoria.", nameof(dto.Ciudad));
            if (dto.Ciudad.Length > 50)
                throw new ArgumentException("La ciudad debe tener al menos 50 caracteres.", nameof(dto.Ciudad));
            if (string.IsNullOrWhiteSpace(dto.Provincia))
                throw new ArgumentException("La provincia es obligatoria.", nameof(dto.Provincia));
            if (dto.Provincia.Length > 50)
                throw new ArgumentException("La provincia debe tener al menos 50 caracteres.", nameof(dto.Provincia));
            if (dto.EstadoId <= 0)
                throw new ArgumentException("Debe seleccionar un estado válido.");
        }

        private void ValidarProveedorUpdateDTO(ProveedorUpdateDTO dto)
        {
            if (dto.Id <= 0)
                throw new ArgumentException("ID inválido.");
            ValidarProveedorCreateDTO(new ProveedorCreateDTO
            {
                Nombre = dto.Nombre,
                Cuit = dto.Cuit,
                Direccion = dto.Direccion,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                Ciudad = dto.Ciudad,
                Provincia = dto.Provincia,
                EstadoId = dto.EstadoId
            });
        }

        private bool EsTelefonoValido(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                return false;

            var regex = new Regex(@"^\+\d{7,15}$");
            return regex.IsMatch(telefono);
        }

        private bool EsCorreoValido(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                return false;

            var regex = new Regex(@"^[\w\.-]+@[\w\.-]+\.\w+$");
            return regex.IsMatch(correo);
        }

        private bool EsCuitValido(string cuit)
        {
            if (string.IsNullOrWhiteSpace(cuit))
                return false;

            var regex = new Regex(@"^\d{2}-\d{8}-\d$");
            return regex.IsMatch(cuit);
        }
       
    }
}
