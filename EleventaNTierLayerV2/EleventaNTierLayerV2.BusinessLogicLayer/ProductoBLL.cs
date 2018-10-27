using EleventaNTierLayerV2.BusinessEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleventaNTierLayerV2.BusinessLogicLayer
{
    /// <summary>
    /// clase para validar todos los dataAnnotations del proyecto ademas de aplicar las reglas del negocio
    /// </summary>
    public class ProductoBLL
    {
        /// <summary>
        /// Validar la insercion del producto en la base de datos
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string InsertarProducto(Producto p)
        {

            string msgError = String.Empty;

            ICollection<ValidationResult> results = null;

            if (!Validate(p, out results))
            {
                msgError = String.Join("\n", results.Select(o => o.ErrorMessage));
            }
            else
            {
                try
                {
                    bool isInserted = DataAccessLayer.ProductoDAL.InsertarProducto(p);

                    if (!isInserted)
                        msgError = "No se puede realizar correctamente el regristro de la marca";
                } catch (Exception ex)
                {
                    msgError = ex.Message;
                }
            }
            return msgError;
        }
        /// <summary>
        /// Validar el metodo para mostrar los productos mediante una consulta en cuanto al codigo de barras
        /// </summary>
        /// <param name="CodigoBarras"></param>
        /// <returns></returns>
        public static DataTable Productos(string CodigoBarras)
        {
            DataTable dt = new DataTable();

            dt = DataAccessLayer.ProductoDAL.Productos(CodigoBarras);

            return dt;
        }
        /// <summary>
        /// metodo que valida la informacion atraida para buscar un producto deseado
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Producto BuscarProductos(Producto p)
        {

            Producto px2 = new Producto();

            px2 = DataAccessLayer.ProductoDAL.BuscarProducto(p);
 
            return px2;

        }
        /// <summary>
        /// metodo para validar la eliminacion de un producto 
        /// </summary>
        /// <param name="CodeBar"></param>
        /// <returns></returns>
        public static string EliminarProducto(string CodeBar)
        {

            string msgError = string.Empty;

            bool isSaved = DataAccessLayer.ProductoDAL.EliminarProducto(CodeBar);

            if (isSaved != true)
            {

                msgError = "No se puedo Eliminar el Producto";

            }

            return msgError;

        }
        /// <summary>
        /// metodo para validar la modificacion del producto mediante el parametro p 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string ProductoModificar(Producto p)
        {

            string msgError = string.Empty;

            bool isSaved = DataAccessLayer.ProductoDAL.ModificarProducto(p);

            if (isSaved != true)
            {

                msgError = "No se puedo Modificar el Producto";

            }

            return msgError;

        }
        /// <summary>
        /// metodo para ordenar y ver los productos en el inventario mediante el codigo de barras
        /// </summary>
        /// <param name="Codebar"></param>
        /// <returns></returns>
        public static DataTable InventarioSelec(string Codebar)
        {

            DataTable dt = new DataTable();

            dt = DataAccessLayer.ProductoDAL.InventarioView(Codebar);

            return dt;

        }
        /// <summary>
        /// metodo para lograr la modificacion del inventario
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string ModifiInventario(Producto p)
        {

            string msgError = string.Empty;

            bool isSaved = DataAccessLayer.ProductoDAL.InventarioModifi(p);

            if (isSaved != true)
            {

                msgError = "\n \t El Inventario no se Pudo Modificar";

            }

            return msgError;

        }
        /// <summary>
        /// metodo para validar el catalogo atraido 
        /// </summary>
        /// <returns></returns>
        public static DataTable Catalogo()
        {

            DataTable dt = new DataTable();

            dt = DataAccessLayer.ProductoDAL.Catalogo();

            return dt;

        }

        public static Producto ProductoCodigo(string CodeBar)
        {

            Producto p = null;

            return p = DataAccessLayer.ProductoDAL.ProductoCodigo(CodeBar);

        }

        public static int ProductoCodigoId(string codeBar)
        {

            return DataAccessLayer.ProductoDAL.ProductoCodigoId(codeBar);

        
}

        public static void ModificarInventarioVenta(string codeBar, int quantity)
        {
            DataAccessLayer.ProductoDAL.ModificarInventarioVenta(codeBar,quantity);
        }

        /// <summary>
        /// Metodo que nos servira para validar los DataAnnotation del proyecto BusinessEntities.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        public static bool Validate<T>(T obj, out ICollection<ValidationResult> results)
        {
            results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, new ValidationContext(obj), results, true);
        }
    }
}
