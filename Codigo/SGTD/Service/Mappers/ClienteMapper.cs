using Riok.Mapperly.Abstractions;
using Shared.DTOs.ClienteDTOs;
using Shared.DTOs.RolDTOs;
using Shared.Entidades;

[Mapper]
public partial class ClienteMapper
{
    public partial Cliente ToEntity(ClienteCreateDTO dto);
    public partial ClienteReadDTO ToReadDto(Cliente entity);

    public partial void UpdateEntity(ClienteUpdateDTO dto, Cliente entity);

    public List<ClienteReadDTO> ToReadDtoList(IEnumerable<Cliente> entities)
        => entities.Select(ToReadDto).ToList();
}

