using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.ProveedorDTOs
{
    public class ProveedorUpdateDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Cuit { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "La ciudad no puede superar los 50 caracteres.")]
        public string Ciudad { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "La provincia no puede superar los 50 caracteres.")]
        public string Provincia { get; set; }
        public int EstadoId { get; set; }
    }
}
