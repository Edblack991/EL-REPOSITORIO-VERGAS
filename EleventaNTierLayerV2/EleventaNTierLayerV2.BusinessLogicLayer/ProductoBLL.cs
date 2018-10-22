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
        public static string InsertarProducto(Producto p)
        {

            //variable para mostrar errores sugeridos dentro de las validaciones
            string msgError = String.Empty;

            //Coleccion para recuperar los mensajes de error ocurridos por los DataAnnotations
            ICollection<ValidationResult> results = null;

            //Aquise debe colocar la condicional recibiendo como parametros la entidad que se quiere
            //validar y la coleccion anteriormente creada, ambos parametros deberan ir en
            //el metodo validate
            if (!Validate(p, out results))
            {
                //En caso de si existir errores de los DataAnnotatios se debera guardar ese
                //ese error en la variable string creada
                msgError = String.Join("\n", results.Select(o => o.ErrorMessage));
            }
            else
            {
                //En caso de no exixistir ningun error, se procedera a contiuar con el proceso
                //Deberemos implementar un try-catch para recuperar errores no contrlados
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

        public static DataTable Productos(string CodigoBarras)
        {
            DataTable dt = new DataTable();

            dt = DataAccessLayer.ProductoDAL.Productos(CodigoBarras);

            return dt;
        }

        public static Producto Productos_Buscar(Producto p)
        {

            Producto px2 = new Producto();

            px2 = DataAccessLayer.ProductoDAL.BuscarProducto(p);
 
            return px2;

        }

        public static string ProductoEliminar(string CodeBar)
        {

            string msgError = string.Empty;

            bool isSaved = DataAccessLayer.ProductoDAL.EliminarProducto(CodeBar);

            if (isSaved != true)
            {

                msgError = "No se puedo Eliminar el Producto";

            }

            return msgError;

        }

        public static string Modificar_Producto(Producto p)
        {

            string msgError = string.Empty;

            bool isSaved = DataAccessLayer.ProductoDAL.ModificarProducto(p);

            if (isSaved != true)
            {

                msgError = "No se puedo Modificar el Producto";

            }

            return msgError;

        }

        public static DataTable Select_Inventario(string Codebar)
        {

            DataTable dt = new DataTable();

            dt = DataAccessLayer.ProductoDAL.InventarioView(Codebar);

            return dt;

        }

        public static string Modificar_Inventario(Producto p)
        {

            string msgError = string.Empty;

            bool isSaved = DataAccessLayer.ProductoDAL.InventarioModifi(p);

            if (isSaved != true)
            {

                msgError = "\n \t El Inventario no se Pudo Modificar";

            }

            return msgError;

        }

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
