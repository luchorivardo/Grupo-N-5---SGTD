using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTOs
{
    public class CrearAnimalDTO
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Raza { get; set; }
        public string Sexo { get; set; }
        public int IdTipo { get; set; }
        public int DuenoId { get; set; }
    }
}

