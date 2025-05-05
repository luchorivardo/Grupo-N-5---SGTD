using Data.Context;
using Data.Repositorios.Implementations;
using Data.Contracts;
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
    }
}
