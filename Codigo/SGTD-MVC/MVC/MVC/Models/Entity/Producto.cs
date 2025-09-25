namespace MVC.Models.Entity
{
    public class Producto : EntidadBase
    {
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public float Precio { get; set; }
        public Estado Estado { get; set; }
        public int EstadoId { get; set; }
        public Disciplina Disciplina { get; set; }
        public int DisciplinaId { get; set; }
    }
}
