namespace MVC.Models.Entity
{
    public class Proveedor : EntidadBase
    {
        public string Nombre { get; set; }
        public string Cuit { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Ciudad { get; set; }
        public string Provincia { get; set; }
        public Estado Estado { get; set; }
        public int EstadoId { get; set; }
    }
}
