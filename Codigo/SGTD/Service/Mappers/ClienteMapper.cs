using Riok.Mapperly.Abstractions;
using Shared.DTOs.ClienteDTOs;
using Shared.Entidades;

[Mapper]
public partial class ClienteMapper
{
    public partial Cliente ToEntity(ClienteCreateDTO dto);
    public partial ClienteReadDTO ToReadDto(Cliente entity);

    public partial void UpdateEntity(ClienteUpdateDTO dto, Cliente entity);
}

