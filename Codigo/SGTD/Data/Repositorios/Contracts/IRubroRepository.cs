﻿using Data.Repositorios.Contracts;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IRubroRepository : IRepository<Rubro>
    {
        Task<bool> ExistePorNombreAsync(string nombre, int? excludeUserId = null);
    }
}
