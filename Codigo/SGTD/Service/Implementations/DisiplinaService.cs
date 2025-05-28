using Data.Contracts;
using Service.Mappers;
using Shared.DTOs.ClienteDTOs;
using Shared.DTOs.Producto;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementations
{
    public class DisiplinaService : IDisiplinaService
    {
        private IDisciplinaRepository _DisciplinaRepository;
        private readonly DisciplinaMapper _mapper = new DisciplinaMapper();
        public DisiplinaService(IDisciplinaRepository disciplinaRepository)
        {
            _DisciplinaRepository = disciplinaRepository;
        }

        public async Task<List<DisciplinaReadDTO>> ObtenerTodos()
        {
            var disciplina = await _DisciplinaRepository.FindAllAsync();

            return _mapper.ToReadDtoList(disciplina);
        }

        public async Task<DisciplinaReadDTO> ObtenerPorId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");

            var disciplina = await _DisciplinaRepository.ObtenerPorId(id);
            if (disciplina == null)
                throw new KeyNotFoundException($"No se encontró ningúna disciplina con ID {id}.");

            return _mapper.ToReadDto(disciplina);
        }

        public async Task<DisciplinaReadDTO> Crear(DisciplinaCreateDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre de la disciplina es obligatorio.");
            var disciplina = _mapper.ToEntity(dto);
            await _DisciplinaRepository.Create(disciplina);

            return _mapper.ToReadDto(disciplina);
        }

        public async Task<DisciplinaReadDTO> Editar(int id, DisciplinaUpdateDTO dto)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre de la disciplina es obligatorio.");

            var disciplina = await _DisciplinaRepository.ObtenerPorId(id);
            if (disciplina == null)
                throw new KeyNotFoundException($"No se encontró ningúna disciplina con ID {id}.");

            _mapper.UpdateEntity(dto, disciplina);

            await _DisciplinaRepository.Update(disciplina);

            return _mapper.ToReadDto(disciplina);
        }

        public async Task Eliminar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");

            var disciplina = await _DisciplinaRepository.ObtenerPorId(id);
            if (disciplina == null)
                throw new KeyNotFoundException($"No se encontró ningúna disciplina con ID {id}.");
            _DisciplinaRepository.Delete(disciplina);
        }
    }
}
