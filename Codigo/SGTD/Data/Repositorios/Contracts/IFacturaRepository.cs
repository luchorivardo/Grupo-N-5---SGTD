using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorios.Contracts
{
    public interface IFacturaRepository : IRepository<Factura>
    {
        IQueryable<Factura> Query();
    }
}
