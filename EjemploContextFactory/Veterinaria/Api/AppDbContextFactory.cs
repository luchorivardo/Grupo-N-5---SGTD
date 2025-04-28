using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Datos.Context; 

namespace Api;
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

    
        optionsBuilder.UseSqlServer("Data Source=DESKTOP-Q1A9JPS\\SQLEXPRESS;Initial Catalog=veterinaria1;Integrated Security=True;TrustServerCertificate=true");

        return new AppDbContext(optionsBuilder.Options);
    }
}
