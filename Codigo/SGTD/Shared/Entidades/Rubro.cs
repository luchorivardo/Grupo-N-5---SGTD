﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entidades
{
    public class Rubro : EntidadBase
    {
        public string Nombre { get; set; }
        public ICollection<RubroProveedor> ProveedoresRubro { get; set; } = new List<RubroProveedor>();
    }
}
