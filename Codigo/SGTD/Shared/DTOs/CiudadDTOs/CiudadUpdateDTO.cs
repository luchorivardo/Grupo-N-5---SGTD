using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.CiudadDTOs
{
    public class CiudadUpdateDTO
    {
        public int Id {  get; set; }
        public string Nombre { get; set; }
        public int ProvinciaId { get; set; }
    }
}
