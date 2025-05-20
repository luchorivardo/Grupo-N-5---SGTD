using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IDisiplinaService 
    {
        Task<List<DisciplinaReadDTO>> ObtenerTodos();
        Task<DisciplinaReadDTO> ObtenerPorId(int id);
        Task Crear(DisciplinaCreateDTO dto);
        Task Editar(int id, DisciplinaUpdateDTO dto);
        Task Eliminar(int id);
    }
}
