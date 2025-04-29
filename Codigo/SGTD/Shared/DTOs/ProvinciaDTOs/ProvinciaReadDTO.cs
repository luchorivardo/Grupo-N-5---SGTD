using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.ProvinciaDTOs
{
    public class ProvinciaReadDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<int> CiudadId { get; set; }
    }
}
