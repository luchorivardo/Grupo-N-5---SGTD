using System.ComponentModel.DataAnnotations;

namespace MVC.Models.DTOs.ClienteDto
{
    public class ClienteCreateDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio.")]
        [Range(1000000, 99999999, ErrorMessage = "El DNI debe estar entre 1.000.000 y 99.999.999.")]
        public int Dni { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "El teléfono no tiene un formato válido.")]
        [StringLength(20, ErrorMessage = "El teléfono no puede superar los 20 caracteres.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [StringLength(150, ErrorMessage = "La dirección no puede superar los 150 caracteres.")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "La ciudad es obligatoria.")]
        [StringLength(50, ErrorMessage = "La ciudad no puede superar los 50 caracteres.")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "La provincia es obligatoria.")]
        [StringLength(50, ErrorMessage = "La provincia no puede superar los 50 caracteres.")]
        public string Provincia { get; set; }
    }
}
