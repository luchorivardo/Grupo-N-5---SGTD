using Shared.DTOs.ClienteDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IClienteService
    {
        Task<List<ClienteReadDTO>> ObtenerTodosAsync();
        Task<ClienteReadDTO> ObtenerPorIdAsync(int id);
        Task<ClienteReadDTO> CrearAsync(ClienteCreateDTO dto);
        Task<ClienteReadDTO> Editar(int id, ClienteUpdateDTO dto);
        Task Eliminar(int id);

    }
}
