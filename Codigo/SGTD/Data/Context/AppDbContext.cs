using Microsoft.EntityFrameworkCore;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Ciudad> Tipos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<ProductoProveedor> ProductosProveedores { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Rubro> Rubros { get; set; }
        public DbSet<RubroProveedor> RubrosProveedores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-KFU2M4R9;Database=TiendaDeportivaDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;Encrypt=False");
        }
    }
}
