using Shared.DTOs.FacturaDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IFacturaService
    {

        Task<FacturaReadDTO> Editar(int id, FacturaUpdateDTO dto);
        Task Eliminar(int id);
    }
}
