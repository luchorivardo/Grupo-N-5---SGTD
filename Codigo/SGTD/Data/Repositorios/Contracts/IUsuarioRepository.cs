using Data.Repositorios.Contracts;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorios.Contracts
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<bool> ExistePorDniAsync(string dni, int? excludeUserId = null);
        Task<bool> ExistePorCorreoAsync(string correo, int? excludeUserId = null);
        Task<Usuario?> ObtenerPorCorreoAsync(string email);
    }
}
