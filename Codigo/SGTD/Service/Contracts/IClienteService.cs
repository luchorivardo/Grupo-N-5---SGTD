using Shared.DTOs.ClienteDTOs;

namespace Service.Contracts
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
