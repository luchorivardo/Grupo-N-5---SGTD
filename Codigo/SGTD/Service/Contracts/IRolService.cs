using Shared.DTOs.RolDTOs;

namespace Service.Contracts
{
    public interface IRolService
    {
        Task<List<RolReadDTO>> ObtenerTodosAsync();
        Task<RolReadDTO> ObtenerPorIdAsync(int id);
        Task<RolReadDTO> CrearAsync(RolCreateDTO dto);
        Task<RolReadDTO> Editar(int id, RolUpdateDTO dto);
        Task Eliminar(int id);
    }
}

