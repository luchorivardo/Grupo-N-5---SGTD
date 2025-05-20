using Shared.DTOs.Producto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IProductoService
    {
        Task<List<ProductoReadDTO>> ObtenerTodos();
        Task<ProductoReadDTO> ObtenerPorId(int id);
        Task Crear(ProductoCreateDTO dto);
        Task Editar(int id, ProductoUpdateDTO dto);
        Task Eliminar(int id);
    }
}
