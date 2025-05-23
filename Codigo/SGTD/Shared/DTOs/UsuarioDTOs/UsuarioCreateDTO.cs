using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.UsuarioDTOs
{
    using System.ComponentModel.DataAnnotations;

    public class UsuarioCreateDTO
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El tipo de documento debe ser válido.")]
        public int TipoDocumento { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "El número de documento debe tener entre 6 y 15 caracteres.")]
        public string NumeroDocumento { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El apellido no puede superar los 50 caracteres.")]
        public string Apellido { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
        public string CorreoElectronico { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Contrasenia { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La ciudad debe ser válida.")]
        public int CiudadId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La provincia debe ser válida.")]
        public int ProvinciaId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El rol debe ser válido.")]
        public int RolId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El estado debe ser válido si se especifica.")]
        public int? EstadoId { get; set; }
    }

}
