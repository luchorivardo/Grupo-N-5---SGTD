using Riok.Mapperly.Abstractions;
using Shared.DTOs.ProductoDTOs;
using Shared.DTOs.ProveedorDTOs;
using Shared.Entidades;

namespace Service.Mappers
{
    [Mapper]
    public partial class ProductoMapper
    {
        public partial Producto ToEntity(ProductoCreateDTO dto);
        public partial ProductoReadDTO ToReadDto(Producto entity);

        public partial void UpdateEntity(ProductoUpdateDTO dto, Producto entity);
        public void CreateMapProveedores(ProductoCreateDTO dto, Producto entity)
        {
            if (dto.ProveedorIds == null) return;

            foreach (var proveedorId in dto.ProveedorIds)
            {
                entity.ProductoProveedor.Add(new ProductoProveedor
                {
                    ProveedorId = proveedorId
                });
            }
        }
        public void UpdateMapProveedores(ProductoUpdateDTO dto, Producto entity)
        {
            if (dto.ProveedorIds == null) return;

            // Quitar los rubros que ya no están en el DTO
            foreach (var rp in entity.ProductoProveedor.ToList())
            {
                if (!dto.ProveedorIds.Contains(rp.ProveedorId))
                    entity.ProductoProveedor.Remove(rp);
            }

            // Agregar los rubros que no existían
            var existingIds = entity.ProductoProveedor.Select(rp => rp.ProveedorId).ToHashSet();
            foreach (var proveedorId in dto.ProveedorIds)
            {
                if (!existingIds.Contains(proveedorId))
                {
                    entity.ProductoProveedor.Add(new ProductoProveedor
                    {
                        ProveedorId = proveedorId
                    });
                }
            }
        }
        public ProductoReadDTO ToReadDtoWithProveedores(Producto entity)
        {
            var dto = ToReadDto(entity);

            // Mapeamos los RubroIds desde la tabla intermedia
            dto.ProveedorIds = entity.ProductoProveedor?
                .Select(rp => rp.ProveedorId)
                .ToList() ?? new List<int>();

            return dto;
        }
        public List<ProductoReadDTO> ToReadDtoList(IEnumerable<Producto> entities)
            => entities.Select(ToReadDtoWithProveedores).ToList();
    }
}
