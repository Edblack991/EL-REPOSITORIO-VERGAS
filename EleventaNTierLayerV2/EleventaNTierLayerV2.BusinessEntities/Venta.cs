using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleventaNTierLayerV2.BusinessEntities
{
    public class Venta
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Cantidad de articulos que decea comprar")]
        public int NumeroArticulos { get; set; }

        [ForeignKey("Producto")]
        [Required(ErrorMessage = "Es necesaria para la coneccion")]
        public int IdProducto { get; set; }
        public Producto Producto { get; set; }

        [Required(ErrorMessage = "Es necesario llenar este campo")]
        public Double SubTotal { get; set; }

        [Required(ErrorMessage = "Cargo por los productos")]
        public Double Iva { get; set; }

        [Required(ErrorMessage = "Es necesario saber el costo final de los productos")]
        public Double Total { get; set; }

        [Required(ErrorMessage = "Cantidad que pago el cliente")]
        public Double Pago { get; set; }

        [Required(ErrorMessage = "Diferencia entre el pago y el total")]
        public Double Cambio { get; set; }

        [ForeignKey("Empleado")]
        [Required(ErrorMessage = "Es necesaria para la coneccion")]
        public int IdEmpleado { get; set; }
        public Empleado Empleado { get; set; }

        [Required(ErrorMessage = "Fecha de expedicion de la venta")]
        [DataType(DataType.DateTime)]
        public DateTime Fecha { get; set; }

        public virtual ICollection<Corte> Cortes { get; set; }


    }
}
