using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.LoginDTOs
{
    public class UsuarioSessionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int RolUsuarioId { get; set; }
        public string RolUsuario { get; set; }
    }
}
