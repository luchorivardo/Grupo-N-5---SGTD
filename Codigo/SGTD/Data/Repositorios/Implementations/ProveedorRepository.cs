using Data.Context;
using Data.Repositorios.Implementations;
using Data.Contracts;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data.Implementations
{
    public class ProveedorRepository : Repository<Proveedor>, IProveedorRepository
    {
        public ProveedorRepository(AppDbContext context) : base(context) { }

        public async Task<List<Proveedor>> FindAllAsyncConRubros()
        {
            return await _context.Proveedores
                .Include(p => p.RubrosProveedor)
                    .ThenInclude(rp => rp.Rubro) // si también querés los datos del Rubro
                .ToListAsync();
        }

        public async Task<Proveedor> ObtenerPorIdConRubros(int id)
        {
            return await _context.Proveedores
                .Include(p => p.RubrosProveedor)
                    .ThenInclude(rp => rp.Rubro)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
