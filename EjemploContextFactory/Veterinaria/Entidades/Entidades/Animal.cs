using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Entidades
{
    public class Animal
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Raza { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public int IdTipo { get; set; }
        public Tipo? Tipo { get; set; }
        public int DuenoId { get; set; }
        public Duenio? Dueno { get; set; }
    }
}
