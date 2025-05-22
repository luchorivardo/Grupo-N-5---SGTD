using Riok.Mapperly.Abstractions;
using Shared.DTOs.RolDTOs;
using Shared.Entidades;

namespace Service.Mappers
{
    [Mapper]
    public partial class RolMapper
    {
        public partial Rol ToEntity(RolCreateDTO dto);
        public partial RolReadDTO ToReadDto(Rol entity);

        public partial void UpdateEntity(RolUpdateDTO dto, Rol entity);
    }
}
