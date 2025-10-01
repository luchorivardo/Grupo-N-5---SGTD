using Data.Context;
using Data.Contracts;
using Data.Repositorios.Implementations;
using Microsoft.EntityFrameworkCore;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementations
{
    public class EstadoRepository : Repository<Estado>, IEstadoRepository
    {
        public EstadoRepository(AppDbContext context) : base(context) { }

        public async Task<bool> ExistePorNombreAsync(string nombre, int? excludeUserId = null)
        {
            return await _context.Estados
                .AnyAsync(u => u.Nombre == nombre && (excludeUserId == null || u.Id != excludeUserId));
        }
    }
}
