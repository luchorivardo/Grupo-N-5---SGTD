namespace MVC.Models.DTOs.UsuarioDto
{
    public class UsuarioReadDTO
    {
        public int Id { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string Ciudad { get; set; }
        public string Provincia { get; set; }
        public int RolId { get; set; }
        public int? EstadoId { get; set; }
    }
}
