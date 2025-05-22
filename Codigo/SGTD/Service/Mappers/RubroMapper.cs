using Riok.Mapperly.Abstractions;
using Shared.DTOs.RubroDTOs;
using Shared.Entidades;

namespace Service.Mappers
{
    [Mapper]
    public partial class RubroMapper
    {
        public partial Rubro ToEntity(RubroCreateDTO dto);
        public partial RubroReadDTO ToReadDto(Rubro entity);

        public partial void UpdateEntity(RubroUpdateDTO dto, Rubro entity);
    }
}
