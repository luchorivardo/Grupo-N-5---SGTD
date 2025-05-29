using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IDisciplinaService 
    {
        Task<List<DisciplinaReadDTO>> ObtenerTodos();
        Task<DisciplinaReadDTO> ObtenerPorId(int id);
        Task<DisciplinaReadDTO> Crear(DisciplinaCreateDTO dto);
        Task<DisciplinaReadDTO> Editar(int id, DisciplinaUpdateDTO dto);
        Task Eliminar(int id);
    }
}
