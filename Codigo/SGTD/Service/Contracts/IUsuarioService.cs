using Shared.DTOs.UsuarioDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IUsuarioService
    {
        Task<List<UsuarioReadDTO>> ObtenerTodosAsync();
        Task<UsuarioReadDTO> ObtenerPorIdAsync(int id);
        Task<UsuarioReadDTO> CrearAsync(UsuarioCreateDTO dto);
        Task<UsuarioReadDTO> Editar(int id, UsuarioUpdateDTO dto);
        Task Eliminar(int id);
    }
}
