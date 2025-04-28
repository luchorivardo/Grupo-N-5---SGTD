using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entidades
{
    public class Cliente : EntidadBase
    {
        public string Nombre { get; set; }
        public int Dni { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public Ciudad Ciudad { get; set; }

    }
}
