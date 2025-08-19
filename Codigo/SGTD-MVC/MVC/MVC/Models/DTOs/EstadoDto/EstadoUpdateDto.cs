using System.ComponentModel.DataAnnotations;

namespace MVC.Models.DTOs.EstadoDto
{
    public class EstadoUpdateDTO
    {
        [Required(ErrorMessage = "El ID es obligatorio.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; }
    }
}
