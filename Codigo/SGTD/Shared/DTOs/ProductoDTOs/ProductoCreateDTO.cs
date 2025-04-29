using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Producto
{
    public class ProductoCreateDTO
    {
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public float Precio { get; set; }
        public int EstadoId { get; set; }
        public int DisciplinaId { get; set; }
    }
}
