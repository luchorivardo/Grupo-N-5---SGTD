using Entidades.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<Duenio> Duenios { get; set; }
        public DbSet<Animal> Animales { get; set; }
        public DbSet<Atencion> Atenciones { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Data Source=DESKTOP-Q1A9JPS\\SQLEXPRESS;Initial Catalog=veterinaria1;Integrated Security=True;TrustServerCertificate=true");
        //    }
        //}

    }
}
