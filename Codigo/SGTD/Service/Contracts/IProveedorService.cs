using Shared.DTOs.ProveedorDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IProveedorService
    {
        Task<List<ProveedorReadDTO>> ObtenerTodos();
        Task<ProveedorReadDTO> ObtenerPorId(int id);
        Task<ProveedorReadDTO> CrearAsync(ProveedorCreateDTO dto);
        Task<ProveedorReadDTO> EditarAsync(int id, ProveedorUpdateDTO dto);
        Task EliminarAsync(int id);
    }
}
