using Riok.Mapperly.Abstractions;
using Shared.DTOs.ClienteDTOs;
using Shared.DTOs.ProveedorDTOs;
using Shared.Entidades;

[Mapper]
public partial class ProveedorMapper
{
    public partial Proveedor ToEntity(ProveedorCreateDTO dto);
    public partial ProveedorReadDTO ToReadDto(Proveedor entity);

    public partial void UpdateEntity(ProveedorUpdateDTO dto, Proveedor entity);

    public List<ProveedorReadDTO> ToReadDtoList(IEnumerable<Proveedor> entities)
    => entities.Select(ToReadDto).ToList();
}
