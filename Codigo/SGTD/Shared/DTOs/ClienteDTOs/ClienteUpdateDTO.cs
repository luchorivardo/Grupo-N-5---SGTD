using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.ClienteDTOs
{
    public class ClienteUpdateDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Dni { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "La ciudad no puede superar los 50 caracteres.")]
        public string Ciudad { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "La provincia no puede superar los 50 caracteres.")]
        public string Provincia { get; set; }
    }
}
