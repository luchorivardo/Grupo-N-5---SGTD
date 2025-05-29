using Riok.Mapperly.Abstractions;
using Shared.DTOs.Producto;
using Shared.Entidades;

namespace Service.Mappers
{
    [Mapper]
    public partial class ProductoMapper
    {
        public partial Producto ToEntity(ProductoCreateDTO dto);
        public partial ProductoReadDTO ToReadDto(Producto entity);

        public partial void UpdateEntity(ProductoUpdateDTO dto, Producto entity);

        public List<ProductoReadDTO> ToReadDtoList(IEnumerable<Producto> entities)
            => entities.Select(ToReadDto).ToList();
    }
}
