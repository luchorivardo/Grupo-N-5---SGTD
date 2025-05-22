using Riok.Mapperly.Abstractions;
using Shared.DTOs.EstadoDTOs;
using Shared.DTOs.RolDTOs;
using Shared.Entidades;

namespace Service.Mappers
{
    [Mapper]
    public partial class EstadoMapper
    {
        public partial Estado ToEntity(EstadoCreateDTO dto);
        public partial EstadoReadDTO ToReadDto(Estado entity);

        public partial void UpdateEntity(EstadoUpdateDTO dto, Estado entity);

        public List<EstadoReadDTO> ToReadDtoList(IEnumerable<Estado> entities)
        => entities.Select(ToReadDto).ToList();
    }
}
