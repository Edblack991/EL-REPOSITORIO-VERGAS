using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleventaNTierLayerV2.BusinessEntities
{
    public class Corte
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Es necesario saber el saldo incial")]
        public Double FondoInicial { get; set; }

        [Required(ErrorMessage = "Se necesita conocer la cantidad final")]
        public Double CantidadFinal { get; set; }

        [Required(ErrorMessage = "Es necesaria para la tabla")]
        public Double Diferencia { get; set; }

        [Required(ErrorMessage = "Es necesaria para determinar la fecha de expdicion")]
        [DataType(DataType.DateTime)]
        public DateTime Fecha { get; set; }

        [ForeignKey("Venta")]
        [Required(ErrorMessage = "Es necesaria para la coneccion")]
        public int IdVenta { get; set; }
        public Venta Venta { get; set; }

    }
}
