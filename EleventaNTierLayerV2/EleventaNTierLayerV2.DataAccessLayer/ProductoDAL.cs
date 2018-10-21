using EleventaNTierLayerV2.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleventaNTierLayerV2.DataAccessLayer
{
    public class ProductoDAL
    {
        #region Metodos CRUD para productos

        /// <summary>
        /// Metodo para ingresar productos
        /// </summary>
        /// <param name="p"></param>
        /// <returns>una respuesta true bool</returns>
        public static bool InsertarProducto(Producto p)
        {
            bool isInserted = false;

            using (EleventaNTierLayerV2DbContext Db = new EleventaNTierLayerV2DbContext())
            {
                //Insertar un metodo en el contexto
                Db.Productos.Add(p);

                //Validamos la insercion correctamente
                isInserted = Db.SaveChanges() > 0 ;

                return isInserted;
            }
        }

        /// <summary>
        /// Metodo para buscar un producto por medio del codigo de barras
        /// </summary>
        /// <param name="p"></param>
        /// <returns>el elemento buscado por medio del id</returns>
        public static Producto BuscarProducto(Producto p)
        {

            Producto px2 = new Producto();

            using (EleventaNTierLayerV2DbContext Db = new EleventaNTierLayerV2DbContext())
            {

                px2 = Db.Productos.Where(x => x.CodigoBarras == p.CodigoBarras).FirstOrDefault();

            }

            return px2;

        }

        /// <summary>
        /// Metodo para eliminar un producto de la base de datos
        /// </summary>
        /// <param name="Bar"></param>
        /// <returns>una variable bool en true</returns>
        public static bool EliminarProducto(string CodeBar)
        {

            bool isSaved = false;

            using (EleventaNTierLayerV2DbContext Db = new EleventaNTierLayerV2DbContext())
            {

                var result = Db.Productos.Where(x => x.CodigoBarras == CodeBar).FirstOrDefault();

                Db.Productos.Remove(result);

                int rowsAffected = Db.SaveChanges();

                if (rowsAffected > 0)
                {

                    isSaved = true;

                }

            }

            return isSaved;

        }

        /// <summary>
        /// Metodo para modificar un producto
        /// </summary>
        /// <param name="p"></param>
        /// <returns>Me retorna una variable en true</returns>
        public static bool ModificarProducto(Producto p)
        {
            
            bool isSaved = false;

            using (EleventaNTierLayerV2DbContext Db = new EleventaNTierLayerV2DbContext())
            {

                Db.Entry(p).State = System.Data.Entity.EntityState.Modified;

                int rowsAffected = Db.SaveChanges();

                if (rowsAffected > 0)
                {

                    isSaved = true;

                }

            }

            return isSaved;

        }
        #endregion

        #region DataTables que se van a necesitar para los procesos
        /// <summary>
        /// Metodo que nos trae de manera ordenada los datos mediante una consulta
        /// </summary>
        /// <param name="CodigoBarras">se utiliza para mandar a llamar el objeto filtrado por el codigo de barras</param>
        /// <returns>regresara los datos obtenidos a travez de la consulta</returns>
        
        public static DataTable Productos(string CodigoBarras)
        {

            DataTable dt = new DataTable();

            using (EleventaNTierLayerV2DbContext dbCtx = new EleventaNTierLayerV2DbContext())
            {

                var resul = dbCtx.Productos.Where(x => x.CodigoBarras == CodigoBarras).ToList();

                dt.Columns.AddRange(new DataColumn[]
                {

                    new DataColumn("Id", typeof(int)),
                    new DataColumn("Descripcion", typeof(string)),
                    new DataColumn("Cantidad", typeof(int)),
                    new DataColumn("Codigo de Barras", typeof(string)),
                    new DataColumn("Precio", typeof(double)),
                    new DataColumn("Precio al Mayoreo", typeof(double)),
                    new DataColumn("Costo", typeof(double)),
                    new DataColumn("Ganancia", typeof(string)),
                    new DataColumn("Uso de Inventario", typeof(bool)),
                    new DataColumn("Inventario Minimo", typeof(int)),
                    new DataColumn("Inventario Maximo", typeof(int)),
                    new DataColumn("Departamento", typeof(int)),

                });

                resul.ToList().ForEach(x =>
                {

                    var row = dt.NewRow();

                    row["Id"] = x.Id;
                    row["Descripcion"] = x.Descripcion;
                    row["Codigo de Barras"] = x.CodigoBarras;
                    row["Departamento"] = x.IdDepartamento;
                    row["costo"] = x.Costo;
                    row["Ganancia"] = x.Ganancia;
                    row["Precio"] = x.Precio;
                    row["Precio al Mayoreo"] = x.PrecioMayoreo;

                    if (x.UsaInventario == true)
                    {

                        row["Uso de Inventario"] = true;

                    }
                    else
                    {

                        row["Uso de Inventario"] = false;

                    }
                    row["Cantidad"] = x.Cantidad;
                    row["Inventario Minimo"] = x.InvMinima;
                    row["Inventario Maximo"] = x.InvMaxima;

                    dt.Rows.Add(row);

                });

            }

            return dt;

        }

        public static DataTable Catalogo()
        {

            DataTable dt = new DataTable();

            using (EleventaNTierLayerV2DbContext Db = new EleventaNTierLayerV2DbContext())
            {

                Producto p = new Producto();

                var result = (from product in Db.Productos
                              join dep in Db.Departamentos on product.IdDepartamento equals dep.Id
                              select new
                              {
                                  product.Id,
                                  product.Descripcion,
                                  dep.Nombre,
                                  product.Precio
                              }).ToList();

                dt.Columns.AddRange(new DataColumn[]
                {

                    new DataColumn("Id", typeof(int)),
                    new DataColumn("Descripcion", typeof(string)),
                    new DataColumn("Departamento", typeof(string)),
                    new DataColumn("Precio $", typeof(double))

                });

                result.ToList().ForEach(x =>
                {

                    var row = dt.NewRow();

                    row["Id"] = x.Id;
                    row["Descripcion"] = x.Descripcion;
                    row["Departamento"] = x.Nombre;
                    row["Precio $"] = x.Precio;

                    dt.Rows.Add(row);

                });

            }

            return dt;
        }
            #endregion

        #region metodos para manipular el inventario
            /// <summary>
            /// Metodo de para traer los datos necesarios solicitados por el inventario 
            /// </summary>
            /// <param name="CodeBar"></param>
            /// <returns> la informacion de los productos </returns>
            public static DataTable InventarioView(string CodeBar)
        {

            DataTable dt = new DataTable();

            using (EleventaNTierLayerV2DbContext Db = new EleventaNTierLayerV2DbContext())
            {

                var result = (from p in Db.Productos where p.CodigoBarras == CodeBar select new
                               {
                                  p.Descripcion,
                                  p.Cantidad,
                                  p.CodigoBarras,
                                  p.Precio,
                                  p.Costo,
                                  p.InvMinima,
                                  p.InvMaxima,
                              }).ToList();

                dt.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("Descripcion", typeof(string)),
                    new DataColumn("Cantidad", typeof(int)),
                    new DataColumn("Codigo de Barras", typeof(string)),
                    new DataColumn("Precio", typeof(double)),
                    new DataColumn("Costo", typeof(double)),
                    new DataColumn("Inventario Minimo", typeof(int)),
                    new DataColumn("Inventario Maximo", typeof(int))
                });

                result.ToList().ForEach(x =>
                {

                    var row = dt.NewRow();

                    row["Descripcion"] = x.Descripcion;
                    row["Cantidad"] = x.Cantidad;
                    row["Codigo de Barras"] = x.CodigoBarras;
                    row["Precio"] = x.Precio;
                    row["Costo"] = x.Costo;
                    row["Inventario Minimo"] = x.InvMinima;
                    row["Inventario Maximo"] = x.InvMaxima;

                    dt.Rows.Add(row);

                });

            }
            return dt;
        }

        /// <summary>
        /// Metodo que me permite modificar el inventario 
        /// </summary>
        /// <param name="p"></param>
        /// <returns>el producto actualizado</returns>
        public static bool InventarioModifi(Producto p)
        {

            bool isCheked = false;

            using (EleventaNTierLayerV2DbContext dbCtx = new EleventaNTierLayerV2DbContext())
            {

                dbCtx.Entry(p).State = System.Data.Entity.EntityState.Modified;

                int rowsAffected = dbCtx.SaveChanges();
                if (rowsAffected > 0)
                {

                    isCheked = true;

                }

            }

            return isCheked;

        }
        #endregion
    }
}
