using Shared.DTOs.RubroDTOs;

namespace Service.Contracts
{
    public interface IRubroService
    {
        Task<List<RubroReadDTO>> ObtenerTodosAsync();
        Task<RubroReadDTO> ObtenerPorIdAsync(int id);
        Task<RubroReadDTO> CrearAsync(RubroCreateDTO dto);
        Task<RubroReadDTO> Editar(int id, RubroUpdateDTO dto);
        Task Eliminar(int id);
    }
}
