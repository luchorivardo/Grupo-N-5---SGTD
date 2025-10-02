using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios.Interfaces
{
    public interface IAnimalRepositorio
    {
        Task<List<Animal>> ObtenerTodosAsync();
        Task<Animal> ObtenerPorIdAsync(int id);
        Task AgregarAsync(Animal animal);
        Task ActualizarAsync(Animal animal);
        Task EliminarAsync(int id);
    }
}
