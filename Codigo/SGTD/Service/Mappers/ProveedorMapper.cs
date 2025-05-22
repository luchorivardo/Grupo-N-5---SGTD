using Riok.Mapperly.Abstractions;
using Shared.DTOs.ProveedorDTOs;
using Shared.Entidades;

[Mapper]
public partial class ProveedorMapper
{
    public partial Proveedor ToEntity(ProveedorCreateDTO dto);
    public partial ProveedorReadDTO ToReadDto(Proveedor entity);

    public partial void UpdateEntity(ProveedorUpdateDTO dto, Proveedor entity);
}
