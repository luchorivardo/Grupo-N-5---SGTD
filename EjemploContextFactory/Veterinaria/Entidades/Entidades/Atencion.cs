using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Entidades
{
    public class Atencion
    {
        public int Id { get; set; }
        public string Motivo { get; set; }
        public string Tratamiento { get; set; }
        public string Medicamentos { get; set; }
        public DateTime Fecha { get; set; }
    }
}
