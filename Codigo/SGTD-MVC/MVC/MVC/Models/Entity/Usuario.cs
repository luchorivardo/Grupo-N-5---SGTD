namespace MVC.Models.Entity
{
    public class Usuario : EntidadBase
    {
        public int TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contrasenia { get; set; }
        public string Ciudad { get; set; }
        public string Provincia { get; set; }
        public int RolId { get; set; }
        public Rol Rol { get; set; }
        public int? EstadoId { get; set; }
        public Estado Estado { get; set; }
    }
}
