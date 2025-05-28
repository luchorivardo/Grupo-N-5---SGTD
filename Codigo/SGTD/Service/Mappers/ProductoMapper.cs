using Shared.DTOs.ClienteDTOs;
using Shared.DTOs.Producto;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mappers
{
    public class ProductoMapper
    {
        public partial Producto ToEntity(ProductoCreateDTO dto);
        public partial ProductoReadDTO ToReadDto(Producto entity);

        public partial void UpdateEntity(ProductoUpdateDTO dto, Producto entity);

        public List<ProductoReadDTO> ToReadDtoList(IEnumerable<Producto> entities)
            => entities.Select(ToReadDto).ToList();
    }
}
