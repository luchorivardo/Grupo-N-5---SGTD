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
    public class RubroRepository : Repository<Rubro>, IRubroRepository
    {
        public RubroRepository(AppDbContext context) : base(context) { }
    }
}
