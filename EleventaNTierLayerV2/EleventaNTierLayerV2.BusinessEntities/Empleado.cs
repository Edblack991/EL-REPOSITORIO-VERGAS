using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleventaNTierLayerV2.BusinessEntities
{
    public class Empleado
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Es necesaria para conoser el nombre del empleado")]
        public String Nombre { get; set; }

        [Required(ErrorMessage = "Es necesaria para conoser el puesto del empleado")]
        public String Puesto { get; set; }

        [Required(ErrorMessage = "Es necesaria para conoser la caja del empleado")]
        public int Caja { get; set; }

        public virtual ICollection<Venta> Ventas { get; set; }

    }
}
