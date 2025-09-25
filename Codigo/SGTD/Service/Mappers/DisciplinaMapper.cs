using Riok.Mapperly.Abstractions;
using Shared.DTOs.ClienteDTOs;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTOs.DisciplinaDTOs;

namespace Service.Mappers
{
    [Mapper]
    public partial class DisciplinaMapper
    {
        public partial Disciplina ToEntity(DisciplinaCreateDTO dto);
        public partial DisciplinaReadDTO ToReadDto(Disciplina entity);

        public partial void UpdateEntity(DisciplinaUpdateDTO dto, Disciplina entity);

        public List<DisciplinaReadDTO> ToReadDtoList(IEnumerable<Disciplina> entities)
            => entities.Select(ToReadDto).ToList();

    }
}
