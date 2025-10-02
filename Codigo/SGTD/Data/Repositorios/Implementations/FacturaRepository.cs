using Data.Context;
using Data.Repositorios.Contracts;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorios.Implementations
{
    public class FacturaRepository : Repository<Factura>, IFacturaRepository
    {
        public FacturaRepository(AppDbContext context) : base(context) { }

        public IQueryable<Factura> Query()
        {
            return _context.Facturas.AsQueryable();
        }
    }
}
