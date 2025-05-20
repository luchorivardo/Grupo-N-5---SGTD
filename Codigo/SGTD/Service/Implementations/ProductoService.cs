using Data.Contracts;
using Shared.Entidades;
using Shared.DTOs.Producto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementations
{
    public class ProductoService : IProductoService
    {
        private IProductoRepository _ProductoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _ProductoRepository = productoRepository;
        }

        public async Task<List<ProductoReadDTO>> ObtenerTodos()
        {
            var productos = await _ProductoRepository.FindAllAsync();
            return productos.Select(a => new ProductoReadDTO
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Cantidad = a.Cantidad,
                Precio = a.Precio,
                EstadoId = a.EstadoId,
                DisciplinaId = a.DisciplinaId,
            }).ToList();
        }

        public async Task<ProductoReadDTO> ObtenerPorId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");

            var producto = await _ProductoRepository.ObtenerPorId(id);
            if (producto == null)
                throw new KeyNotFoundException($"No se encontró ningún producto con ID {id}.");

            return new ProductoReadDTO
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Cantidad = producto.Cantidad,
                Precio = producto.Precio,
                EstadoId = producto.EstadoId,
                DisciplinaId = producto.DisciplinaId,
            };
        }

        public async Task Crear(ProductoCreateDTO dto)
        {
            ValidarProductoCreateDTO(dto);

            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Cantidad = dto.Cantidad,
                Precio = dto.Precio,
                EstadoId = dto.EstadoId,
                DisciplinaId = dto.DisciplinaId,
            };

            await _ProductoRepository.Create(producto);
        }

        public async Task Editar(int id, ProductoUpdateDTO dto)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");

            ValidarProductoUpdateDTO(dto);

            var producto = await _ProductoRepository.ObtenerPorId(id);
            if (producto == null)
                throw new KeyNotFoundException($"No se encontró ningún producto con ID {id}.");

            producto.Nombre = dto.Nombre;
            producto.Cantidad = dto.Cantidad;
            producto.Precio = dto.Precio;
            producto.EstadoId = dto.EstadoId;
            producto.DisciplinaId = dto.DisciplinaId;

            await _ProductoRepository.Update(producto);
        }

        public async Task Eliminar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");

            var producto = await _ProductoRepository.ObtenerPorId(id);
            if (producto == null)
                throw new KeyNotFoundException($"No se encontró ningún producto con ID {id}.");

            _ProductoRepository.Delete(producto);
        }

       
        private void ValidarProductoCreateDTO(ProductoCreateDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre del producto es obligatorio.");

            if (dto.Cantidad < 0)
                throw new ArgumentException("La cantidad no puede ser negativa.");

            if (dto.Precio < 0)
                throw new ArgumentException("El precio no puede ser negativo.");

            if (dto.EstadoId <= 0)
                throw new ArgumentException("El EstadoId debe ser válido.");

            if (dto.DisciplinaId <= 0)
                throw new ArgumentException("El DisciplinaId debe ser válido.");
        }

        private void ValidarProductoUpdateDTO(ProductoUpdateDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre del producto es obligatorio.");

            if (dto.Cantidad < 0)
                throw new ArgumentException("La cantidad no puede ser negativa.");

            if (dto.Precio < 0)
                throw new ArgumentException("El precio no puede ser negativo.");

            if (dto.EstadoId <= 0)
                throw new ArgumentException("El EstadoId debe ser válido.");

            if (dto.DisciplinaId <= 0)
                throw new ArgumentException("El DisciplinaId debe ser válido.");
        }
    }
}

