using Data.Contracts;
using Service.Contracts;
using Service.Mappers;
using Shared.DTOs.RolDTOs;


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
            return _mapper.ToReadDtoList(rol);
        }

        public async Task<RolReadDTO> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");

            var rol = await _rolRepository.ObtenerPorId(id);
            if (rol == null)
                throw new KeyNotFoundException($"No se encontró ningún rol con ID {id}.");

            return _mapper.ToReadDto(rol);
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
