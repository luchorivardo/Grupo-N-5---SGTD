using Data.Contracts;
using Service.Mappers;
using Shared.DTOs.RolDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class RolService : IRolService
    {
        private IRolRepository _rolRepository;
        private readonly RolMapper _mapper = new RolMapper();

        public RolService(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        public async Task<List<RolReadDTO>> ObtenerTodosAsync()
        {
            var rol = await _rolRepository.FindAllAsync();
            return rol.Select(a => new RolReadDTO
            {
                Id = a.Id,
                Nombre = a.Nombre
            }).ToList();
        }

        public async Task<RolReadDTO> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");

            var rol = await _rolRepository.ObtenerPorId(id);
            if (rol == null)
                throw new KeyNotFoundException($"No se encontró ningún rol con ID {id}.");

            return new RolReadDTO
            {
                Id = rol.Id,
                Nombre = rol.Nombre
            };
        }

        public async Task<RolReadDTO> CrearAsync(RolCreateDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Nombre))
                throw new ArgumentException("El nombre del rol es obligatorio.");

            var rol = _mapper.ToEntity(dto);
            await _rolRepository.Create(rol);

            return _mapper.ToReadDto(rol);
        }

        public async Task<RolReadDTO> Editar(int id, RolUpdateDTO dto)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");
            if (string.IsNullOrEmpty(dto.Nombre))
                throw new ArgumentException("El nombre del rol es obligatorio.");

            var rol = await _rolRepository.ObtenerPorId(id);
            if (rol == null)
                throw new KeyNotFoundException($"No se encontró ningún estado con ID {id}.");

            _mapper.UpdateEntity(dto, rol);

            await _rolRepository.Update(rol);

            return _mapper.ToReadDto(rol);
        }

        public async Task Eliminar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");

            var rol = await _rolRepository.ObtenerPorId(id);
            if (rol == null)
                throw new KeyNotFoundException($"No se encontró ningún cliente con ID {id}.");

            _rolRepository.Delete(rol);
        }
    }
}
