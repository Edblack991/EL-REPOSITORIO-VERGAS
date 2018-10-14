using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleventaNTierLayerV2.BusinessEntities
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "La descripcion del producto es necesaria")]
        [DataType(DataType.Text)]
        public String Descripcion { get; set; }

        [Required(ErrorMessage = "La cantidad del producto es necesaria")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El codigo de barras del producto es necesario")]
        [DataType(DataType.Text)]
        public String CodigoBarras { get; set; }

        [Required(ErrorMessage = "El precio del producto debe ser establecido")]
        public Double Precio { get; set; }

        [Required(ErrorMessage = "El precio por mayoreo tiene que estar establecido")]
        public Double PrecioMayoreo { get; set; }

        [Required(ErrorMessage = "El costo del producto es necesario")]
        public Double Costo { get; set; }

        [Required(ErrorMessage = "La ganancia tiene que ser establecida")]
        public String Ganancia { get; set; }

        [Required(ErrorMessage = "El campo es necesario")]
        public int InvMinima { get; set; }

        [Required(ErrorMessage = "El campo es necesario")]
        public int InvMaxima { get; set; }

        [ForeignKey("Departamento")]
        [Required(ErrorMessage = "Es necesaria para la coneccion")]
        public int IdDepartamento { get; set; }
        public Departamento Departamento { get; set; }

        public virtual ICollection<Venta> Ventas { get; set; }
    }
}
