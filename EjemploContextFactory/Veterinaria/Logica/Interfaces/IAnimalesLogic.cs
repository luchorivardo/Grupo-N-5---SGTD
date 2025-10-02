using Entidades.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Interfaces
{
    public interface IAnimalesLogic
    {
        Task<List<AnimalDTO>> ObtenerTodosAsync();
        Task<AnimalDTO> ObtenerPorIdAsync(int id);
        Task CrearAsync(CrearAnimalDTO dto);
        Task EditarAsync(int id, CrearAnimalDTO dto);
        Task EliminarAsync(int id);
    }
}
