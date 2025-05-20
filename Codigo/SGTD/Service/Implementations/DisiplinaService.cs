using Data.Contracts;
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
            var disciplina = new Disciplina
            {
                Nombre = dto.Nombre,
            };
            await _DisciplinaRepository.Create(disciplina);
        }

        public async Task Editar(int id, DisciplinaUpdateDTO dto)
        {
            var disciplina = await _DisciplinaRepository.ObtenerPorId(id);
            if (disciplina == null) return;

            disciplina.Nombre = dto.Nombre;

            await _DisciplinaRepository.Update(disciplina);
        }

        public async Task Eliminar(int id)
        {
            var disciplina = await _DisciplinaRepository.ObtenerPorId(id);
            _DisciplinaRepository.Delete(disciplina);
        }
    }
}
