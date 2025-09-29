using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entidades
{
    public class Usuario : EntidadBase
    {
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contrasenia { get; set; }
        public string Ciudad { get; set; }
        public string Provincia { get; set; }
        public int RolId { get; set; }
        public Rol Rol { get; set; }
        public int? EstadoId { get; set; }
        public Estado Estado { get; set; }
    }
}
