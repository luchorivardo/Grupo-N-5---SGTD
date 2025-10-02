using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.FacturaDTOs
{
    //public class FacturaCreateDTO
    //{
    //    [Required(ErrorMessage = "La fecha de emisión es obligatoria.")]
    //    public DateTime FechaEmision { get; set; }

    //    [Required(ErrorMessage = "El monto es obligatorio.")]
    //    [Range(1, int.MaxValue, ErrorMessage = "El monto debe ser mayor a 0.")]
    //    public int Monto { get; set; }

    //    [Required(ErrorMessage = "La dirección fiscal es obligatoria.")]
    //    [StringLength(150, ErrorMessage = "La dirección fiscal no puede superar los 150 caracteres.")]
    //    public string DireccionFiscal { get; set; }

    //    [Required(ErrorMessage = "El ID fiscal es obligatorio.")]
    //    public int IdFiscal { get; set; }

    //    [Required(ErrorMessage = "La descripción es obligatoria.")]
    //    [StringLength(250, ErrorMessage = "La descripción no puede superar los 250 caracteres.")]
    //    public string Descripcion { get; set; }

    //    [Required(ErrorMessage = "La razón social es obligatoria.")]
    //    [StringLength(100, ErrorMessage = "La razón social no puede superar los 100 caracteres.")]
    //    public string RazonSocial { get; set; }

    //    [Required(ErrorMessage = "La cantidad de productos es obligatoria.")]
    //    [Range(1, int.MaxValue, ErrorMessage = "Debe haber al menos un producto.")]
    //    public int CantidadProductos { get; set; }

    //    [Required(ErrorMessage = "El ID del producto es obligatorio.")]
    //    public int ProductoId { get; set; }

    //    [Required(ErrorMessage = "El ID del usuario es obligatorio.")]
    //    public int UsuarioId { get; set; }

    //    [Required(ErrorMessage = "El ID del cliente es obligatorio.")]
    //    public int ClienteId { get; set; }
    //}
    public class FacturaCreateDTO
    {
        [Required]
        public DateTime FechaEmision { get; set; }

        [Required, StringLength(150)]
        public string DireccionFiscal { get; set; }

        [Required]
        public int IdFiscal { get; set; }

        [Required, StringLength(250)]
        public string Descripcion { get; set; }

        [Required, StringLength(100)]
        public string RazonSocial { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [Required]
        public List<FacturaProductoCreateDTO> Productos { get; set; } = new();
    }
}
