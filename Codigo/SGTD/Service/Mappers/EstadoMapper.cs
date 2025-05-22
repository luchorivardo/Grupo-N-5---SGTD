using Riok.Mapperly.Abstractions;
using Shared.DTOs.EstadoDTOs;
using Shared.Entidades;

namespace Service.Mappers
{
    [Mapper]
    public partial class EstadoMapper
    {
        public partial Estado ToEntity(EstadoCreateDTO dto);
        public partial EstadoReadDTO ToReadDto(Estado entity);

        public partial void UpdateEntity(EstadoUpdateDTO dto, Estado entity);
    }
}
