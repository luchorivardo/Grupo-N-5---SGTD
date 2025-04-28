using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entidades
{
    public class ProductoProveedor : EntidadBase
    {

        public Producto Productos { get; set; }
        public Proveedor Proveedores { get; set; }

    }
}
