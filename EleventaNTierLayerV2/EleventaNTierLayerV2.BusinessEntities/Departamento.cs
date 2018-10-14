using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleventaNTierLayerV2.BusinessEntities
{
    public class Departamento
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [DataType(DataType.Text)]
        public String Nombre { get; set; }

        [Required(ErrorMessage = "La descripcion es obligatorio")]
        [DataType(DataType.Text)]
        public String Descripcion { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }

    }
}

