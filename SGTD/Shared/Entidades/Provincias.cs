using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entidades
{
    public class Provincias 
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ICollection<Ciudad> Ciudades { get; set; }
    }
}
