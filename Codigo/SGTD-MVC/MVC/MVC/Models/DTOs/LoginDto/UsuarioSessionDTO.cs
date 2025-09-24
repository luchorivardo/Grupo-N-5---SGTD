namespace MVC.Models.DTOs.LoginDto
{
    public class UsuarioSessionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int RolUsuarioId { get; set; }

        public string RolUsuario { get; set; }
    }
}
