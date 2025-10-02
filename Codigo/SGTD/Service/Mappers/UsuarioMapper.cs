using Riok.Mapperly.Abstractions;
using Shared.DTOs.ClienteDTOs;
using Shared.DTOs.UsuarioDTOs;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mappers
{
    [Mapper]
    public partial class UsuarioMapper
    {
        public partial Usuario ToEntity(UsuarioCreateDTO dto);
        public partial UsuarioReadDTO ToReadDto(Usuario entity);

        public partial void UpdateEntity(UsuarioUpdateDTO dto, Usuario entity);
        public List<UsuarioReadDTO> ToReadDtoList(IEnumerable<Usuario> entities)
            => entities.Select(ToReadDto).ToList();
    }
}
