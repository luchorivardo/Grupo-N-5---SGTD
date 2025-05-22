using Riok.Mapperly.Abstractions;
using Shared.DTOs.EstadoDTOs;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
