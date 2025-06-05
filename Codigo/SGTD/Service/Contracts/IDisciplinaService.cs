using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTOs.DisciplinaDTOs;

namespace Service.Contracts
{
    public interface IDisciplinaService 
    {
        Task<List<DisciplinaReadDTO>> ObtenerTodosAsync();
        Task<DisciplinaReadDTO> ObtenerPorIdAsync(int id);
        Task<DisciplinaReadDTO> CrearAsync(DisciplinaCreateDTO dto);
        Task<DisciplinaReadDTO> Editar(int id, DisciplinaUpdateDTO dto);
        Task Eliminar(int id);
    }
}
