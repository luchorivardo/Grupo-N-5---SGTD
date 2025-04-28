using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entidades
{
    public class Ciudad 
    {
        public int Id { get; set; }
        public string Nombre { get; set; } 
        public int ProvinciaId { get; set; }  
        public Provincias Provincia { get; set; } 
    }
}
