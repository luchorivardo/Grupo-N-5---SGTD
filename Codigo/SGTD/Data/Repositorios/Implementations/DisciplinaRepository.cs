using Data.Context;
using Data.Contracts;
using Data.Repositorios.Contracts;
using Data.Repositorios.Implementations;
using Microsoft.EntityFrameworkCore;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorios.Implementations
{
    public class DisciplinaRepository : Repository<Disciplina>, IDisciplinaRepository
    {
        public DisciplinaRepository(AppDbContext context) : base(context) { }
        public async Task<bool> ExistePorNombreAsync(string nombre, int? excludeUserId = null)
        {
            return await _context.Disciplinas
                .AnyAsync(u => u.Nombre == nombre && (excludeUserId == null || u.Id != excludeUserId));
        }
    }
}
