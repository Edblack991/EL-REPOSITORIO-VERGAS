using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleventaNTierLayerV2.BusinessEntities
{
    public class Detalle_Venta
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Producto")]
        [Required(ErrorMessage ="El campo IdProducto es obligatorio")]
        public int IdProducto { get; set; }
        public Producto Producto { get; set; }

        [ForeignKey("Venta")]
        [Required(ErrorMessage ="El campo IdVenta es obligatorio")]
        public int IdVenta { get; set; }
        public Venta Venta { get; set; }

        [Required(ErrorMessage ="El campo cantidad es obligatorio")]
        [DataType(DataType.Text)]
        public int Cantidad { get; set; }

        [Required(ErrorMessage ="El campo total es obligatorio")]
        public double Total { get; set; }
    }
}
