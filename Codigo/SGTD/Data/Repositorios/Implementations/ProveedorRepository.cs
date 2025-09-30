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
    public class ProveedorRepository : Repository<Proveedor>, IProveedorRepository
    {
        public ProveedorRepository(AppDbContext context) : base(context) { }
    
        public async Task<bool> ExistePorCuitAsync(string cuit, int? excludeUserId = null)
        {
            return await _context.Proveedores
                .AnyAsync(u => u.Cuit == cuit && (excludeUserId == null || u.Id != excludeUserId));
        }
    }
}
