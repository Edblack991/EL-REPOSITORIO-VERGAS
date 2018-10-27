using EleventaNTierLayerV2.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleventaNTierLayerV2.ConsoleApp
{
    class Program
    {   /// <summary>
        /// Variables utilizadas en los ciclos
        /// </summary>
        public static int Categoria, Operaciones, Desicion;
        public static int CantidadDeArticulos = 0;
        public static double subtotal = 0;
        public static double iva = 0;
        public static double total = 0;
        public static string codeBar;
        public static int quantity;

        /// <summary>  
        /// Metodo Identificar categoria 
        /// </summary>
        /// <returns>Retornara la categoria correspondiente</returns>
        public static int Identificar_Categoria()
        {
            do
            {
                try
                {
                    Console.WriteLine("¿Que categoria desea operar?\n" +
                        "1.- Ventas.\n" +
                        "2.- Productos.\n" +
                        "3.- Inventario.\n");
                    Categoria = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex)
                {
                }
            } while (Categoria > 3 || Categoria < 1);

            return Categoria;
        }

        #region Metodos para el manejo de Productos
        /// <summary>
        /// Metodo para identificar que se espera hacer con los productos 
        /// </summary>
        /// <returns>Regresa la operacion deseada a realizar</returns>
        public static int Identificar_Operacion2()
        {
            do
            {
                try
                {
                    Console.WriteLine("-Productos-\n");
                    Console.WriteLine("¿Que operacion desea realizar?\n" +
                        "1.- Nuevo Producto.\n" +
                        "2.- Modificar Producto.\n" +
                        "3.- Eliminar Producto.\n" +
                        "4.- Catalogo.\n");

                    Operaciones = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex)
                {
                }
            } while (Operaciones > 5 || Operaciones < 1);

            return Operaciones;
        }
        #endregion

        #region Metodos VENTAS
        public static void Cobrar_Producto(DataTable dt,string codeBar,int quantity)
        {
            Console.Clear();

            Producto p = new Producto();
            string CodBar = string.Empty;
            int resp;
            do
            {

                Console.Clear();

                Console.Write("\nCodigo de Barras: "); CodBar = Console.ReadLine();

                BuscarProducto(CodBar, dt);

                ExtensionDataTable.PrintToConsole(dt);

                Console.WriteLine("\n");

                ImprimirTabla();

                Console.WriteLine("\n¿Desea Ingresar  otro Articulo?\n"
                                     + "1.- Si\n"
                                     + "2.- No");
                resp = Convert.ToInt32(Console.ReadLine());

            } while (resp == 1);

            if (resp == 2)
            {
                Console.Clear();

                string msgError = string.Empty;

                Venta v = new Venta();

                v.Sucursal = "Gral Escobedo";
                v.Fecha = DateTime.Now;
                v.Importe = total;
                v.CantidadDeArticulos = CantidadDeArticulos;
                v.Caja = "1";

                msgError = BusinessLogicLayer.VentaBLL.RealizarVenta(v);

                if (string.IsNullOrEmpty(msgError))
                {
                    int saleId = BusinessLogicLayer.VentaBLL.UltimoRegistro();

                    if (saleId > 0)
                    {

                        foreach (DataRow row in dt.Rows)
                        {

                            Detalle_Venta d = new Detalle_Venta();

                            d.IdVenta = saleId;
                            d.IdProducto = Convert.ToInt32(BusinessLogicLayer.ProductoBLL.ProductoCodigoId(row["Codigo Barras"].ToString()));
                            d.Cantidad = Convert.ToInt32(row["Cantidad"].ToString());
                            d.Total = Convert.ToDouble(row["Total"].ToString());

                            bool isCheked = BusinessLogicLayer.Detalle_VentaBLL.insertar(d);

                            codeBar = row["Codigo Barras"].ToString();
                            quantity = 1;

                            BusinessLogicLayer.ProductoBLL.ModificarInventarioVenta(codeBar,quantity);

                            if (isCheked)
                            {

                                Console.WriteLine("\n\tCOMPRA REALIZADA");
                                Console.WriteLine("\n\tREGRESE PRONTO LO ESTAREMOS ESPERANDO");
                                Console.WriteLine("");

                            }
                        }
                    }

                }
                else
                {
                    Console.WriteLine(msgError);
                    Console.ReadLine();
                }
            }
        }

        public static void ImprimirTabla()
        {

            DataTable dt = new DataTable();

            dt.Columns.AddRange(new DataColumn[]
            {

                new DataColumn("TIPO_PAGO", typeof(string)),
                new DataColumn("CANTIDAD_ARTICULOS", typeof(int)),
                new DataColumn("SUBTOTAL", typeof(string)),
                new DataColumn("IVA", typeof(string)),
                new DataColumn("TOTAL", typeof(string))

            });

            var row = dt.NewRow();

            row["TIPO_PAGO"] = "EFECTIVO";
            row["CANTIDAD_ARTICULOS"] = CantidadDeArticulos;
            row["SUBTOTAL"] = "$ " + subtotal;
            row["IVA"] = "$ " + iva;
            row["TOTAL"] = "$ " + total;

            dt.Rows.Add(row);

            ExtensionDataTable.PrintToConsole(dt);


        }

        public static void BuscarProducto(string Codebar, DataTable dt)
        {

            Producto p = new Producto();

            p = BusinessLogicLayer.ProductoBLL.ProductoCodigo(Codebar);

            if (p != null)
            {

                var row = dt.NewRow();

                row["Codigo Barras"] = p.CodigoBarras;
                row["Descripcion"] = p.Descripcion;
                row["CANTIDAD"] = 1;
                row["Precio Unitario"] = p.Precio;
                row["Total"] = (p.Precio * .16) + p.Precio;

                dt.Rows.Add(row);

                CantidadDeArticulos = CantidadDeArticulos + 1;
                subtotal = subtotal + Convert.ToDouble(row["Precio Unitario"].ToString());
                iva = iva + Convert.ToDouble(row["Precio Unitario"]) * .16;
                total = total + Convert.ToDouble(row["Total"].ToString());

            }
            else
            {

                Console.WriteLine("\n\tNo se pudo encontrar el producto");
                Console.ReadLine();

            }

        }

        public static void Actualizar(DataTable dt)
        {

            dt.Columns.AddRange(new DataColumn[]
{

                    new DataColumn("Codigo Barras", typeof(string)),
                    new DataColumn("Descripcion", typeof(string)),
                    new DataColumn("CANTIDAD", typeof(string)),
                    new DataColumn("Precio Unitario", typeof(double)),
                    new DataColumn("Total", typeof(double))

            });

        }

        #endregion

        #region Metodos PRODUCTOS
        public static void New_Product()
        {
            Producto p = new Producto();
            DataTable dt = new DataTable();
            int useInv; bool isCheked = false;
            try
            {
                Console.WriteLine("-Nuevo Producto-\n");
                Console.Write("Descripcion: "); p.Descripcion = Console.ReadLine();
                Console.Write("Codigo de Barras: "); p.CodigoBarras = Console.ReadLine();
                Console.Write("Costo: "); p.Costo = Convert.ToDouble(Console.ReadLine());
                Console.Write("Precio: "); p.Precio = Convert.ToDouble(Console.ReadLine());
                Console.Write("Ganancia: %"); p.Ganancia = Console.ReadLine();
                Console.Write("Precio Mayoreo: "); p.PrecioMayoreo = Convert.ToDouble(Console.ReadLine());

              
                    Console.Write("¿El producto usa Inventario?\n" +
                        "1.- Si.\n" +
                        "2.- No.\n");

                    useInv = Convert.ToInt32(Console.ReadLine());

                p.UsaInventario = isCheked;
                Console.Write("Cantidad: "); p.Cantidad = Convert.ToInt32(Console.ReadLine());
                Console.Write("Inventario Minimo "); p.InvMinima = Convert.ToInt32(Console.ReadLine());
                Console.Write("Inventario Maximo "); p.InvMaxima = Convert.ToInt32(Console.ReadLine());

                dt = BusinessLogicLayer.DepartamentoBLL.CargarDepartamenTable();

                Console.WriteLine("Los departamentos existentes son:");

                foreach (DataRow item in dt.Rows)
                {
                    Console.WriteLine("\t{0}.- {1}", item["Id"].ToString(), item["Nombre"].ToString(), item["Descripcion"].ToString());
                }
                do
                {
                    Console.WriteLine("Departamento: "); p.IdDepartamento = Convert.ToInt32(Console.ReadLine());
                } while (p.IdDepartamento > dt.Rows.Count || p.IdDepartamento < 0);

                string msgError = BusinessLogicLayer.ProductoBLL.InsertarProducto(p);

                if (string.IsNullOrEmpty(msgError))
                {
                    Console.Write("\n El producto se inserto correctamente");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine(msgError);
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message.ToString());

            }
}

        public static void Update_Producto()
        {
            Producto p = new Producto(); DataTable dt = new DataTable(); DataTable dep = new DataTable();
            Producto old = new Producto();
            int modificar, camp, cVen, Inv, mod; bool isCheked = false;
            string barcode = string.Empty;

            Console.WriteLine("\n");
            Console.WriteLine("-Modificar Producto-\n");
            Console.WriteLine("Ingrese el codigo de barras del producto que quiera modificar");
            barcode = Console.ReadLine().ToString().Trim();
            old.CodigoBarras = barcode;

            try
            {

                dt = BusinessLogicLayer.ProductoBLL.Productos(barcode);
                p = BusinessLogicLayer.ProductoBLL.BuscarProductos(old);

                if (p != null)
                {

                    Console.WriteLine("\n¿Este es el producto requerdo?...");


                    foreach (DataRow item in dt.Rows)
                    {

                        Console.WriteLine("\t1.- Id: {0}\n \t2.- Descripcion: {1}\n \t3.- Codigo de Barras: {2}\n \t4.- Departamento: {3}\n \t5.- \t6.- Costo: {4}\n \t7.- Ganancia: {5}\n \t8.- Precio: {6}\n \t9.- Precio al Mayoreo: {7}\n \t10.- Uso de Inventario: {8}\n \t11.- Cantidad: {9}\n \t12.- nventario Minimo: {10}\n \t13.- Inventario Maximo: {11}\n",
                            item["Id"].ToString(), item["Descripcion"].ToString(), item["Codigo de Barras"].ToString(), item["Departamento"].ToString(),
                            item["Costo"].ToString(), item["Ganancia"].ToString(), item["Precio"].ToString(), item["Precio al Mayoreo"].ToString(),
                            item["Uso de Inventario"].ToString(), item["Cantidad"].ToString(), item["Inventario Minimo"].ToString(), item["Inventario Maximo"].ToString());

                    }
                    do
                    {

                        Console.WriteLine("¿Desea Modificar el Producto?...\n"
                            + "1.- Si\n"
                            + "2.- No");

                        modificar = Convert.ToInt32(Console.ReadLine());

                    } while (modificar > 2 || modificar < 1);

                    if (modificar == 1)
                    {

                        do
                        {

                            Console.WriteLine("\n Selecciona el campo que deseas modificar");
                            camp = Convert.ToInt32(Console.ReadLine());

                            switch (camp)
                            {

                                case 1:

                                    Console.WriteLine("\n\tLo sentimos no podemos modificar el Id del Producto");

                                    break;

                                case 2:

                                    Console.WriteLine("\nIngresa una nueva Descripcion"); p.Descripcion = Console.ReadLine();

                                    break;

                                case 3:

                                    Console.WriteLine("\nIngresa un nuevo Codigo de Barras"); p.CodigoBarras = Console.ReadLine();

                                    break;

                                case 4:

                                    Console.WriteLine("\nLos Departamentos son...");

                                    dep = BusinessLogicLayer.DepartamentoBLL.CargarDepartamenTable();

                                    foreach (DataRow item in dep.Rows)
                                    {

                                        Console.WriteLine("\t{0}.- {1}", item["Id"].ToString(), item["Nombre"].ToString(), item["Descripcion"].ToString());

                                    }
                                    do
                                    {

                                        Console.WriteLine("Departamento: "); p.IdDepartamento = Convert.ToInt32(Console.ReadLine());

                                    } while (p.IdDepartamento > dep.Rows.Count || p.IdDepartamento < 0);

                                    break;

                                case 5:

                                    Console.WriteLine("\nIngrasa el nuevo costo"); p.Costo = Convert.ToDouble(Console.ReadLine());

                                    break;

                                case 6:

                                    Console.Write("\nGanancia %"); p.Ganancia = Console.ReadLine();

                                    break;

                                case 7:

                                    Console.WriteLine("\n Ingresa el nuevo Precio del Producto $"); p.Precio = Convert.ToDouble(Console.ReadLine());

                                    break;

                                case 8:

                                    Console.WriteLine("\n Ingresa el nuevo Precio de Mayoreo"); p.PrecioMayoreo = Convert.ToDouble(Console.ReadLine());

                                    break;

                                case 9:

                                    do
                                    {

                                        Console.WriteLine("El Producto usa Inventario \n"
                                            + "1.- Si \n"
                                            + "2.- No");

                                        Inv = Convert.ToInt32(Console.ReadLine());

                                        if (Inv == 1)
                                        {

                                            isCheked = true;

                                        }
                                        else if (Inv == 2)
                                        {

                                            isCheked = false;

                                        }
                                        else
                                        {

                                            Console.WriteLine("Porfavor eliga una de las Opciones Mostradas");

                                        }

                                    } while (Inv > 2 || Inv < 1);

                                    switch (Inv)
                                    {

                                        case 1:

                                            p.UsaInventario = isCheked;
                                            Console.WriteLine("Cantidad Minima en el Inventario:"); p.InvMinima = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine("Cantidad Maxima en el Inventario: "); p.InvMaxima = Convert.ToInt32(Console.ReadLine());

                                            break;

                                        case 2:

                                            p.UsaInventario = isCheked;
                                            p.InvMinima = 0;
                                            p.InvMaxima = 0;

                                            break;

                                    }

                                    break;

                            }

                            Console.WriteLine("Deseas modificar algun otro campo?\n"
                                + "1.- Si\n"
                                + "2.- No");
                            mod = Convert.ToInt32(Console.ReadLine());

                        } while (mod == 1);

                        string msgError = BusinessLogicLayer.ProductoBLL.ProductoModificar(p);

                        if (string.IsNullOrEmpty(msgError))
                        {

                            Console.WriteLine("\tSE MODIFICO EL PRODUCTO CON EXITO");
                            Console.ReadLine();

                        }
                        else
                        {

                            Console.WriteLine(msgError);

                        }

                    }

                }
                else
                {

                    Console.WriteLine("\n\t PRODUCTO NO ENCONTRADO");
                    Console.ReadLine();

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message.ToString());

            }
        }

        public static void Delete_Producto()
        {
            DataTable dt = new DataTable(); Producto p = new Producto(); Producto old = new Producto();
            string codebar = string.Empty;

            Console.WriteLine("\n");
            Console.WriteLine("-Eliminar-\n");
            Console.WriteLine("Ingresa el Codifo de Barras que quieres Eliminar"); p.CodigoBarras = Console.ReadLine().ToString().Trim();

            try
            {

                dt = BusinessLogicLayer.ProductoBLL.Productos(p.CodigoBarras);
                p = BusinessLogicLayer.ProductoBLL.BuscarProductos(p);

                if (p != null)
                {

                    Console.WriteLine("Se eliminara el producto: ");

                    foreach (DataRow item in dt.Rows)
                    {

                        Console.WriteLine("\t1.- Id: {0}\n \t2.- Descripcion: {1}\n \t3.- Codigo de Barras: {2}\n \t4.- Departamento: {3}\n \t5.- \t6.- Costo: {4}\n \t7.- Ganancia: {5}\n \t8.- Precio: {6}\n \t9.- Precio al Mayoreo: {7}\n \t10.- Uso de Inventario: {8}\n \t11.- Cantidad: {9}\n \t12.- nventario Minimo: {10}\n \t13.- Inventario Maximo: {11}\n",
                            item["Id"].ToString(), item["Descripcion"].ToString(), item["Codigo de Barras"].ToString(), item["Departamento"].ToString(),
                            item["Costo"].ToString(), item["Ganancia"].ToString(), item["Precio"].ToString(), item["Precio al Mayoreo"].ToString(),
                            item["Uso de Inventario"].ToString(), item["Cantidad"].ToString(), item["Inventario Minimo"].ToString(), item["Inventario Maximo"].ToString());

                    }

                    Console.WriteLine("¿Desea Borrar el Producto?\n"
                        + "1.- Si\n"
                        + "2.- No");
                    int delete = Convert.ToInt32(Console.ReadLine());

                    if (delete == 1)
                    {

                        string msgError = BusinessLogicLayer.ProductoBLL.EliminarProducto(p.CodigoBarras);

                        if (string.IsNullOrEmpty(msgError))
                        {

                            Console.WriteLine("\n \t Se Borro el Producto correctamente");
                            Console.ReadLine();
                        }
                        else
                        {

                            Console.Write(msgError);

                        }
                    }
                }
                else
                {

                    Console.WriteLine("\n\tPRODUCTO NO ENCONTRADO");
                    Console.ReadLine();

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message.ToString());

            }
        }

        public static void View_Catalogo()
        {
            DataTable dt = new DataTable();

            Console.WriteLine("\nCATALOGO\n");

            dt = BusinessLogicLayer.ProductoBLL.Catalogo();

            foreach (DataRow item in dt.Rows)
            {

                Console.WriteLine("\t1.- Id: {0}\n \t2.- Descripcion: {1}\n \t3.- Departamento: {2}\n \t4.- Precio: {3}\n",
                    item["Id"].ToString(), item["Descripcion"].ToString(), item["Departamento"].ToString(), item["Precio $"].ToString());
                Console.Read();

            }
        }
        #endregion

        #region metodos INVENTARIO
        public static void View_Inventario()
        {

            DataTable dt = new DataTable(); Producto p = new Producto(); Producto product = new Producto();
            string BarCode; int res, camp, mod; 

            Console.Write("Ingresa el codigo de Barras: "); BarCode = Console.ReadLine().ToString().Trim();
            product.CodigoBarras = BarCode;
            try
            {

                dt = BusinessLogicLayer.ProductoBLL.InventarioSelec(BarCode);
                p = BusinessLogicLayer.ProductoBLL.BuscarProductos(product);

                if (p != null)
                {

                    foreach (DataRow item in dt.Rows)
                    {

                        Console.WriteLine("\t1.- Descripcion: {0}\n \t2.- Codigo de Barras: {1}\n \t3.- Costo: {2}\n \t4.- Precio: {3}\n \t5.- Cantidad: {4}\n \t6.- Inventario Minimo: {5}\n \t7.- Inventario Maximo: {6}\n",
                            item["Descripcion"].ToString(), item["Codigo de Barras"].ToString(),
                            item["Costo"].ToString(), item["Precio"].ToString(),
                            item["Cantidad"].ToString(), item["Inventario Minimo"].ToString(), item["Inventario Maximo"].ToString());

                    }

                    do
                    {

                        Console.WriteLine("\n¿Deseas Modificar el Inventario?\n"
                                            + "1.- Si\n"
                                            + "2.- No");
                        res = Convert.ToInt32(Console.ReadLine());

                    } while (res > 2 || res < 1);

                    do
                    {

                        Console.WriteLine("\n¿Que campo deseas Modificar?");
                        camp = Convert.ToInt32(Console.ReadLine());

                        switch (camp)
                        {

                            case 1:

                                Console.Write("\nIngresa una nueva Descripcion: "); p.Descripcion = Console.ReadLine();

                                break;

                            case 2:

                                Console.Write("\nIngresa un Nuevo codigo de Barras: "); p.CodigoBarras = Console.ReadLine();

                                break;

                            case 3:

                                Console.Write("\nIngresa un nuevo Costo: "); p.Costo = Convert.ToDouble(Console.ReadLine());

                                break;

                            case 4:

                                Console.WriteLine("\nEl Precio se Modifica Automaticamente cuando se Modifica el Costo");

                                break;

                            case 5:

                                Console.Write("\nIngresa la nueva Cantidad actual del Producto: "); p.Cantidad = Convert.ToInt32(Console.ReadLine());

                                break;

                            case 6:

                                Console.Write("\nIngresa el nuevo Inventario Minimo del Producto: "); p.InvMinima = Convert.ToInt32(Console.ReadLine());

                                break;

                            case 7:

                                Console.Write("\nIngresa la nueva Cantidad Maxima del Producto: "); p.InvMaxima = Convert.ToInt32(Console.ReadLine());

                                break;
                        }

                        Console.WriteLine("\nDeseas Modificar algun otro campo?\n"
                            + "1.- Si\n"
                            + "2.- No");
                        mod = Convert.ToInt32(Console.ReadLine());

                    } while (mod == 1);

                    string msgError = BusinessLogicLayer.ProductoBLL.ModifiInventario(p);

                    if (string.IsNullOrEmpty(msgError))
                    {

                        Console.WriteLine("\n\t EL INVENTARIO SE MODIFICO EXITOSAMENTE");
                        Console.ReadLine();

                    }
                    else
                    {

                        Console.WriteLine(msgError);
                        Console.ReadLine();

                    }

                }
                else
                {

                    Console.WriteLine("\n\t PRODUCTO NO ENCONTRADO ");
                    Console.ReadLine();

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message.ToString());

            }
        }

        #endregion

        static void Main(string[] args)
        {
            DataTable dt = new DataTable();
            do {
                Identificar_Categoria();

                switch (Categoria)
                {
                    case 1:
                            Actualizar(dt);
                            Cobrar_Producto(dt,codeBar,quantity);
                        break;

                    case 2:
                        Identificar_Operacion2();

                        switch (Operaciones)
                        {
                            case 1:;
                                Console.Clear();
                                New_Product();
                                Console.Write("-----------------------------------------------------------");
                                break;
                            case 2:
                                Console.Clear();
                                Update_Producto();
                                Console.Write("-----------------------------------------------------------");
                                break;
                            case 3:
                                Console.Clear();
                                Delete_Producto();
                                Console.Write("-----------------------------------------------------------");
                                break;
                            case 4:
                                Console.Clear();
                                View_Catalogo();
                                Console.Write("-----------------------------------------------------------");
                                break;
                        }
                        break;

                    case 3:
                        Console.Clear();
                        View_Inventario();
                        break;

                }

                Console.WriteLine("Desea Realizar otra operacion?:\n" +
                 "1.- Si.\n" +
                 "2.- No.");
                Desicion = Console.Read();
            } while (Desicion == 1);
            Console.ReadKey();
        }
        
    }
}

