using Shared.DTOs.EstadoDTOs;

namespace Service.Contracts
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
