using Data.Contracts;
using Data.Implementations;
using Service.Contracts;
using Service.Mappers;
using Shared.DTOs.EstadoDTOs;

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
            return _mapper.ToReadDtoList(estado);
        }

        public async Task<EstadoReadDTO> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");

            var estado = await _estadoRepository.ObtenerPorId(id);
            if (estado == null)
                throw new KeyNotFoundException($"No se encontró ningún estado con ID {id}.");

            return _mapper.ToReadDto(estado);
        }

        public async Task<EstadoReadDTO> CrearAsync(EstadoCreateDTO dto)
        {
            var t = _estadoRepository.FindAll();
            if (t.Count() != 0)
            {
                if (await _estadoRepository.ExistePorNombreAsync(dto.Nombre))
                    throw new ArgumentException("Ya existe un estado con ese nombre.", nameof(dto.Nombre));
            }
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

            if (await _estadoRepository.ExistePorNombreAsync(dto.Nombre))
                throw new ArgumentException("Ya existe un estado con ese nombre.", nameof(dto.Nombre));

            var estado = await _estadoRepository.ObtenerPorId(id);
            if (estado == null)
                throw new KeyNotFoundException($"No se encontró ningún estado con ID {id}.");

            estado.UpdatedDate = DateTime.Now;
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

            await _estadoRepository.Delete(estado);
        }
    }
}
