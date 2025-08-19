using System.ComponentModel.DataAnnotations;

namespace MVC.Models.DTOs.UsuarioDto
{
    public class UsuarioUpdateDTO
    {
        [Required(ErrorMessage = "El ID es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID debe ser mayor a cero.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El tipo de documento es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El tipo de documento debe ser válido.")]
        public int TipoDocumento { get; set; }

        [Required(ErrorMessage = "El número de documento es obligatorio.")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "El número de documento debe tener entre 6 y 15 caracteres.")]
        public string NumeroDocumento { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido no puede superar los 50 caracteres.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
        public string CorreoElectronico { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Contrasenia { get; set; }

        [Required(ErrorMessage = "La ciudad es obligatoria.")]
        [StringLength(50, ErrorMessage = "La ciudad no puede superar los 50 caracteres.")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "La provincia es obligatoria.")]
        [StringLength(50, ErrorMessage = "La provincia no puede superar los 50 caracteres.")]
        public string Provincia { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El rol debe ser válido.")]
        public int RolId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El estado debe ser válido si se especifica.")]
        public int? EstadoId { get; set; }
    }
}
