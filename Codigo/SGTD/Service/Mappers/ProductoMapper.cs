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
        public void MapProveedores(ProductoCreateDTO dto, Producto entity)
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
        public List<ProductoReadDTO> ToReadDtoList(IEnumerable<Producto> entities)
            => entities.Select(ToReadDto).ToList();
    }
}
