using System.ComponentModel.DataAnnotations;

namespace MVC.Models.DTOs.RubroDto
{
    public class RubroUpdateDTO
    {
        [Required(ErrorMessage = "El ID es obligatorio.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; }
    }
}
