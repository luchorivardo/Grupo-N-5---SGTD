﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entidades
{
    public class Factura : EntidadBase
    {
        public DateTime FechaEmision { get; set; }
        public int Monto { get; set; }
        public string DireccionFiscal { get; set; }
        public int IdFiscal { get; set; }
        public string Descripcion { get; set; }
        public string RazonSocial { get; set; }
        public int CantidadProductos { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
    }
}
