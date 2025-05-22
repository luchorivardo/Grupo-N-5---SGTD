using Shared.DTOs.EstadoDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IEstadoService
    {
        Task<List<EstadoReadDTO>> ObtenerTodosAsync();
        Task<EstadoReadDTO> ObtenerPorIdAsync(int id);
        Task<EstadoReadDTO> CrearAsync(EstadoCreateDTO dto);
        Task<EstadoReadDTO> Editar(int id, EstadoUpdateDTO dto);
        Task Eliminar(int id);
    }
}
