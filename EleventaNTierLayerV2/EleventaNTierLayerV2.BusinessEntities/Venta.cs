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

        [Required(ErrorMessage = "El campo Sucursal es obligatorio")]
        public string Sucursal { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El campo Importe es obligatorio")]
        public double Importe { get; set; }

        public int CantidadDeArticulos { get; set; }

        public string Caja { get; set; }

        public virtual ICollection<Corte> Cortes { get; set; }
        public virtual ICollection<Detalle_Venta> Detalle_Ventas { get; set; }

    }
}
