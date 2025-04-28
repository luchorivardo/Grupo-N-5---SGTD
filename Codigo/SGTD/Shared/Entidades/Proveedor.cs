using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entidades
{
    public class Proveedor : EntidadBase
    {
        public string Nombre { get; set; }
        public string Cuit { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public Ciudad Ciudad { get; set; }
        public int CiudadId { get; set; }
        public Estado Estado { get; set; }
        public int EstadoId { get; set; }

        public override string ToString()
        {
            return $"{Nombre}";
        }
    }
}
