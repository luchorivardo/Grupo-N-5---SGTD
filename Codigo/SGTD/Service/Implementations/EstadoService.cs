using Data.Contracts;
using Service.Mappers;
using Shared.DTOs.EstadoDTOs;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class EstadoService : IEstadoService
    {
        private IEstadoRepository _estadoRepository;
        private readonly EstadoMapper _mapper = new EstadoMapper();

        public EstadoService(IEstadoRepository estadoRepository)
        {
            _estadoRepository = estadoRepository;
        }

        public async Task<List<EstadoReadDTO>> ObtenerTodosAsync()
        {
            var estado = await _estadoRepository.FindAllAsync();
            return estado.Select(a => new EstadoReadDTO
            {
                Id = a.Id,
                Nombre = a.Nombre
            }).ToList();
        }

        public async Task<EstadoReadDTO> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");

            var estado = await _estadoRepository.ObtenerPorId(id);
            if (estado == null)
                throw new KeyNotFoundException($"No se encontró ningún estado con ID {id}.");

            return new EstadoReadDTO
            {
                Id = estado.Id,
                Nombre = estado.Nombre
            };
        }

        public async Task<EstadoReadDTO> CrearAsync(EstadoCreateDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Nombre))
                throw new ArgumentException("El nombre del estado es obligatorio.");

            var estado = _mapper.ToEntity(dto);
            await _estadoRepository.Create(estado);

            return _mapper.ToReadDto(estado);
        }

        public async Task<EstadoReadDTO> Editar(int id, EstadoUpdateDTO dto)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");
            if (string.IsNullOrEmpty(dto.Nombre))
                throw new ArgumentException("El nombre del estado es obligatorio.");

            var estado = await _estadoRepository.ObtenerPorId(id);
            if (estado == null)
                throw new KeyNotFoundException($"No se encontró ningún estado con ID {id}.");

            _mapper.UpdateEntity(dto, estado);

            await _estadoRepository.Update(estado);

            return _mapper.ToReadDto(estado);
        }

        public async Task Eliminar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");

            var estado = await _estadoRepository.ObtenerPorId(id);
            if (estado == null)
                throw new KeyNotFoundException($"No se encontró ningún cliente con ID {id}.");

            _estadoRepository.Delete(estado);
        }
    }
}
