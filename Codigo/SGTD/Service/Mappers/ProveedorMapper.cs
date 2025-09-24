using Riok.Mapperly.Abstractions;
using Shared.DTOs.ClienteDTOs;
using Shared.DTOs.ProductoDTOs;
using Shared.DTOs.ProveedorDTOs;
using Shared.Entidades;

[Mapper]
public partial class ProveedorMapper
{
    public partial Proveedor ToEntity(ProveedorCreateDTO dto);
    public partial ProveedorReadDTO ToReadDto(Proveedor entity);

    public partial void UpdateEntity(ProveedorUpdateDTO dto, Proveedor entity);
    public void MapRubros(ProveedorCreateDTO dto, Proveedor entity)
    {
        if (dto.RubroIds == null) return;

        foreach (var rubroId in dto.RubroIds)
        {
            entity.RubrosProveedor.Add(new RubroProveedor
            {
                RubroId = rubroId
            });
        }
    }

    public List<ProveedorReadDTO> ToReadDtoList(IEnumerable<Proveedor> entities)
    => entities.Select(ToReadDto).ToList();
}
