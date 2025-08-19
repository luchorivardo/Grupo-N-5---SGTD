namespace MVC.Models.Entity
{
    public class Cliente : EntidadBase
    {
        public string Nombre { get; set; }
        public int Dni { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Provincia { get; set; }
    }
}
