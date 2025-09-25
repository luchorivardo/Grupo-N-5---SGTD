﻿using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.UsuarioDTOs
{
    public class UsuarioReadDTO
    {
        public int Id { get; set; }
        public int TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contrasenia { get; set; }
        public int CiudadId { get; set; }
        public int ProvinciaId { get; set; }
        public int RolId { get; set; }
        public int? EstadoId { get; set; }
    }
}
