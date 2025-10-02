using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.ProveedorDTOs
{
    public class ProveedorReadDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Cuit { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Ciudad { get; set; }
        public string Provincia { get; set; }
        public List<int> RubroIds { get; set; }
        public int EstadoId { get; set; }
    }
}
