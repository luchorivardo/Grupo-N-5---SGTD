using Data.Contracts;
using Data.Implementations;
using Service.Contracts;
using Service.Mappers;
using Shared.DTOs.RubroDTOs;

namespace Service.Implementations
{
    public class RubroService : IRubroService
    {
        private IRubroRepository _rubroRepository;
        private readonly RubroMapper _mapper = new RubroMapper();

        public RubroService(IRubroRepository rubroRepository)
        {
            _rubroRepository = rubroRepository;
        }

        public async Task<List<RubroReadDTO>> ObtenerTodosAsync()
        {
            var rubro = await _rubroRepository.FindAllAsync();
            return _mapper.ToReadDtoList(rubro);
        }

        public async Task<RubroReadDTO> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");

            var rubro = await _rubroRepository.ObtenerPorId(id);
            if (rubro == null)
                throw new KeyNotFoundException($"No se encontró ningún rol con ID {id}.");

            return _mapper.ToReadDto(rubro);
        }

        public async Task<RubroReadDTO> CrearAsync(RubroCreateDTO dto)
        {
            var t = _rubroRepository.FindAll();
            if (t.Count() != 0)
            {
                if (await _rubroRepository.ExistePorNombreAsync(dto.Nombre))
                    throw new ArgumentException("Ya existe un rubro con ese nombre.", nameof(dto.Nombre));
            }
            if (string.IsNullOrEmpty(dto.Nombre))
                throw new ArgumentException("El nombre del rol es obligatorio.");

            var rubro = _mapper.ToEntity(dto);
            await _rubroRepository.Create(rubro);

            return _mapper.ToReadDto(rubro);
        }

        public async Task<RubroReadDTO> Editar(int id, RubroUpdateDTO dto)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");

            if (await _rubroRepository.ExistePorNombreAsync(dto.Nombre))
                throw new ArgumentException("Ya existe un rubro con ese nombre.", nameof(dto.Nombre));

            if (string.IsNullOrEmpty(dto.Nombre))
                throw new ArgumentException("El nombre del rol es obligatorio.");

            var rubro = await _rubroRepository.ObtenerPorId(id);
            if (rubro == null)
                throw new KeyNotFoundException($"No se encontró ningún estado con ID {id}.");

            rubro.UpdatedDate = DateTime.Now;
            _mapper.UpdateEntity(dto, rubro);

            await _rubroRepository.Update(rubro);

            return _mapper.ToReadDto(rubro);
        }

        public async Task Eliminar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");

            var rubro = await _rubroRepository.ObtenerPorId(id);
            if (rubro == null)
                throw new KeyNotFoundException($"No se encontró ningún cliente con ID {id}.");

            await _rubroRepository.Delete(rubro);
        }
    }
}
