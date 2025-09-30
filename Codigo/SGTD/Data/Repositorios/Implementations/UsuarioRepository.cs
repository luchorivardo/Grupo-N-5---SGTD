using Data.Context;
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
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext context) : base(context) { }

        public async Task<bool> ExistePorDniAsync(string dni, int? excludeUserId = null)
        {
            return await _context.Usuarios
                .AnyAsync(u => u.NumeroDocumento == dni && (excludeUserId == null || u.Id != excludeUserId));
        }

        public async Task<bool> ExistePorCorreoAsync(string correo, int? excludeUserId = null)
        {
            return await _context.Usuarios
                .AnyAsync(u => u.CorreoElectronico == correo && (excludeUserId == null || u.Id != excludeUserId));


        }

        public async Task<Usuario> ObtenerPorCorreoAsync(string email)
        {
            return await _context.Usuarios
                .Include(u => u.Rol) // 🔑 Carga el rol junto con el usuario
                .FirstOrDefaultAsync(u => u.CorreoElectronico == email);
        }

        //public async Task<Usuario?> ObtenerPorCorreoAsync(string correo)
        //{
        //    return await _context.Usuarios
        //        .FirstOrDefaultAsync(u => u.CorreoElectronico == correo);
        //}
    }
}
