namespace MVC.Models.ViewModels
{
    public class ProveedorIndexVm
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Cuit { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Ciudad { get; set; }
        public string Provincia { get; set; }
        public int EstadoId { get; set; }
        public string EstadoNombre { get; set; }
        public List<int> RubroIds { get; set; }
        public List<string> RubroNombres { get; set; }
    }
}
