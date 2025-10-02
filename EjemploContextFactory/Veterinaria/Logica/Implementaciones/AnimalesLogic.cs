using Datos.Repositorios.Interfaces;
using Entidades.DTOs;
using Entidades.Entidades;
using Logica.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Implementaciones
{
    public class AnimalesLogic : IAnimalesLogic
    {
        private readonly IAnimalRepositorio _repo;

        public AnimalesLogic(IAnimalRepositorio repo)
        {
            _repo = repo;
        }

        public async Task<List<AnimalDTO>> ObtenerTodosAsync()
        {
            var animales = await _repo.ObtenerTodosAsync();

            return animales.Select(a => new AnimalDTO
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Raza = a.Raza,
                Edad = a.Edad,
                Sexo = a.Sexo,
                IdTipo = a.IdTipo,
                TipoNombre = a.Tipo?.Nombre, // Asegurate de hacer Include()
                DuenoId = a.DuenoId,
                NombreDueno = $"{a.Dueno?.Nombre} {a.Dueno?.Apellido}"
            }).ToList();
        }

        public async Task<AnimalDTO> ObtenerPorIdAsync(int id)
        {
            var a = await _repo.ObtenerPorIdAsync(id);
            if (a == null)
                return null;

            return new AnimalDTO
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Raza = a.Raza,
                Edad = a.Edad,
                Sexo = a.Sexo,
                IdTipo = a.IdTipo, // o a.IdTipo, según tu modelo
                TipoNombre = a.Tipo?.Nombre,
                DuenoId = a.DuenoId,
                NombreDueno = $"{a.Dueno?.Nombre} {a.Dueno?.Apellido}"
            };
        }

        public async Task CrearAsync(CrearAnimalDTO dto)
        {
            var animal = new Animal
            {
                Nombre = dto.Nombre,
                Edad = dto.Edad,
                Sexo = dto.Sexo,
                Raza = dto.Raza,
                IdTipo = dto.IdTipo,
                DuenoId = dto.DuenoId
            };
            await _repo.AgregarAsync(animal);
        }

        public async Task EditarAsync(int id, CrearAnimalDTO dto)
        {
            var animal = await _repo.ObtenerPorIdAsync(id);
            if (animal == null) return;

            animal.Nombre = dto.Nombre;
            animal.Edad = dto.Edad;

            await _repo.ActualizarAsync(animal);
        }

        public async Task EliminarAsync(int id)
        {
            await _repo.EliminarAsync(id);
        }
    }
}

