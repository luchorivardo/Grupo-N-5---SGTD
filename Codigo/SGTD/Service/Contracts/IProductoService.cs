using Shared.DTOs.Producto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IProductoService
    {
        Task<List<ProductoReadDTO>> ObtenerTodos();
        Task<ProductoReadDTO> ObtenerPorId(int id);
        Task<ProductoReadDTO> Crear(ProductoCreateDTO dto);
        Task<ProductoReadDTO> Editar(int id, ProductoUpdateDTO dto);
        Task Eliminar(int id);
    }
}
