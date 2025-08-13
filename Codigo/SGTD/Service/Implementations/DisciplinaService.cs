using Data.Contracts;
using Data.Implementations;
using Data.Repositorios.Contracts;
using Service.Contracts;
using Service.Mappers;
using Shared.DTOs.ClienteDTOs;
using Shared.DTOs.DisciplinaDTOs;
using Shared.DTOs.ProductoDTOs;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class DisciplinaService : IDisciplinaService
    {
        private IDisciplinaRepository _DisciplinaRepository;
        private readonly DisciplinaMapper _mapper = new DisciplinaMapper();
        public DisciplinaService(IDisciplinaRepository disciplinaRepository)
        {
            _DisciplinaRepository = disciplinaRepository;
        }

        public async Task<List<DisciplinaReadDTO>> ObtenerTodosAsync()
        {
            var disciplina = await _DisciplinaRepository.FindAllAsync();

            return _mapper.ToReadDtoList(disciplina);
        }

        public async Task<DisciplinaReadDTO> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");

            var disciplina = await _DisciplinaRepository.ObtenerPorId(id);
            if (disciplina == null)
                throw new KeyNotFoundException($"No se encontró ningúna disciplina con ID {id}.");

            return _mapper.ToReadDto(disciplina);
        }

        public async Task<DisciplinaReadDTO> CrearAsync(DisciplinaCreateDTO dto)
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

            disciplina.UpdatedDate = DateTime.Now;
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
            await _DisciplinaRepository.Delete(disciplina);
        }
    }
}
