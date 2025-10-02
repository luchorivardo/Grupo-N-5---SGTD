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
    public void CreateMapRubros(ProveedorCreateDTO dto, Proveedor entity)
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
    public void UpdateMapRubros(ProveedorUpdateDTO dto, Proveedor entity)
    {
        if (dto.RubroIds == null) return;

        // Quitar los rubros que ya no están en el DTO
        foreach (var rp in entity.RubrosProveedor.ToList())
        {
            if (!dto.RubroIds.Contains(rp.RubroId))
                entity.RubrosProveedor.Remove(rp);
        }

        // Agregar los rubros que no existían
        var existingIds = entity.RubrosProveedor.Select(rp => rp.RubroId).ToHashSet();
        foreach (var rubroId in dto.RubroIds)
        {
            if (!existingIds.Contains(rubroId))
            {
                entity.RubrosProveedor.Add(new RubroProveedor
                {
                    RubroId = rubroId
                });
            }
        }
    }
    public ProveedorReadDTO ToReadDtoWithRubros(Proveedor entity)
    {
        var dto = ToReadDto(entity);

        // Mapeamos los RubroIds desde la tabla intermedia
        dto.RubroIds = entity.RubrosProveedor?
            .Select(rp => rp.RubroId)
            .ToList() ?? new List<int>();

        return dto;
    }

    public List<ProveedorReadDTO> ToReadDtoList(IEnumerable<Proveedor> entities)
    => entities.Select(ToReadDtoWithRubros).ToList();
}
