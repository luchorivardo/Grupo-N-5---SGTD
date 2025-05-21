using Data.Contracts;
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
        public DisiplinaService(IDisciplinaRepository disciplinaRepository)
        {
            _DisciplinaRepository = disciplinaRepository;
        }

        public async Task<List<DisciplinaReadDTO>> ObtenerTodos()
        {
            var animales = await _DisciplinaRepository.FindAllAsync();

            return animales.Select(a => new DisciplinaReadDTO
            {
                Id = a.Id,
                Nombre = a.Nombre,
            }).ToList();
        }

        public async Task<DisciplinaReadDTO> ObtenerPorId(int id)
        {
            var a = await _DisciplinaRepository.ObtenerPorId(id);
            if (a == null)
            {
                return null;
            }

            return new DisciplinaReadDTO
            {
                Id = a.Id,
                Nombre = a.Nombre
            };
        }

        public async Task Crear(DisciplinaCreateDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre de la disciplina es obligatorio.");

            var disciplina = new Disciplina
            {
                Nombre = dto.Nombre,
            };
            await _DisciplinaRepository.Create(disciplina);
        }

        public async Task Editar(int id, DisciplinaUpdateDTO dto)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre de la disciplina es obligatorio.");

            var disciplina = await _DisciplinaRepository.ObtenerPorId(id);
            if (disciplina == null)
                throw new KeyNotFoundException($"No se encontró ningúna disciplina con ID {id}.");

            disciplina.Nombre = dto.Nombre;

            await _DisciplinaRepository.Update(disciplina);
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
