using Datos.Context;
using Datos.Repositorios.Interfaces;
using Entidades.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Datos.Repositorios
{
    public class AnimalRepositorio : IAnimalRepositorio
    {
        private readonly AppDbContext _context;

        public AnimalRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Animal>> ObtenerTodosAsync()
        {
            return await _context.Animales
                .Include(a => a.Tipo)
                .Include(a => a.Dueno)
                .ToListAsync();
        }

        public async Task<Animal> ObtenerPorIdAsync(int id)
        {
            return await _context.Animales
                .Include(a => a.Tipo)
                .Include(a => a.Dueno)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AgregarAsync(Animal animal)
        {
            await _context.Animales.AddAsync(animal);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Animal animal)
        {
            _context.Animales.Update(animal);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var animal = await _context.Animales.FindAsync(id);
            if (animal != null)
            {
                _context.Animales.Remove(animal);
                await _context.SaveChangesAsync();
            }
        }
    }
}
