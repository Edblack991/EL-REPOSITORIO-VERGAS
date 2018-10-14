using EleventaNTierLayerV2.BusinessEntities;
using System;
using System.Collections.Generic;
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
                        "3.- Inventario.");

                    Categoria = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex)
                {
                }
            } while (Categoria > 3 || Categoria < 1);

            return Categoria;
        }

        #region Metodos para el manejo de Ventas
        /// <summary>
        /// Metodo para identificar que se espera hacer en el apartado ventas
        /// </summary>
        /// <returns>Regresa la operacion deseada a realizar</returns>
        public static int Identificar_Operacion1()
        {
            do
            {
                try
                {
                    Console.WriteLine("-Ventas-\n");
                    Console.WriteLine("¿Que operacion desea realizar?\n" +
                        "1.- Agregar producto.\n" +
                        "2.- Cobrar Producto.\n" +
                        "3.- Entrada/Salida de caja.\n" +
                        "4.- Verificador de precio.\n");

                    Operaciones = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex)
                {
                }
            } while (Operaciones > 4 || Operaciones < 1);

            return Operaciones;
        }
        #endregion

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
                        "4.- Departamentos.\n" +
                        "5.- Catalogo.\n");

                    Operaciones = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex)
                {
                }
            } while (Operaciones > 5 || Operaciones < 1);

            return Operaciones;
        }
        #endregion

        #region Metodos para el manejo de Inventario
        /// <summary>
        /// Metodo para identificar que se espera hacer en el apartado Inventario
        /// </summary>
        /// <returns>Regresa la operacion deseada a realizar</returns>
        public static int Identificar_Operacion3()
        {
            do
            {
                try
                {
                    Console.WriteLine("-Inventario-\n");
                    Console.WriteLine("¿Que operacion desea realizar?\n" +
                        "1.- Ver inventario.\n" +
                        "2.- Modificar inventario.\n");

                    Operaciones = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex)
                {
                }
            } while (Operaciones > 2 || Operaciones < 1);

            return Operaciones;
        }
        #endregion

        #region Metodos VENTAS
        public static void Agregar_Producto()
        {

        }
        public static void Cobrar_Producto()
        {

        }
        public static void CAJA()
        {

        }
        public static void PRECIO()
        {

        }
        #endregion

        #region Metodos PRODUCTOS
        //public static void New_Product()
        //{
        //    Producto p = new Producto();

        //    Console.WriteLine("-Nuevo Producto-\n");
        //    Console.Write("Descripcion: "); //producto.Description = Console.ReadLine();
        //    Console.Write("Cantidad: "); 
        //    Console.Write("Codigo de Barras: "); //producto.BarCode = Convert.ToInt32(Console.ReadLine());
        //    Console.Write("Costo: ");
        //    Console.Write("Precio: "); //producto.Precio_Costo = Convert.ToDouble(Console.ReadLine());
        //    double Ganancia =;
        //    Console.Write("Ganancia: %" + Ganancia); //producto.Ganancia = Convert.ToDouble(Console.ReadLine());
        //    Console.Write("Precio Mayoreo: "); //producto.Precio_Mayoreo = Convert.ToDouble(Console.ReadLine()); 
        //    do
        //    {
        //        Console.Write("¿El producto usa Inventario?\n" +
        //            "1.- Si.\n" +
        //            "2.- No.\n");

        //        useInv = Convert.ToInt32(Console.ReadLine());

        //    } while (useInv > 3 || useInv < 1);
        //    Console.Write("Inventario Minimo");
        //    Console.Write("Inventario Maximo");
        //    Console.WriteLine("Los departamentos existentes son:");

        //    departamentos = BusinessLogicLayer.DepartamentBLL.Cargar_DepartamentNTable();
        //    foreach (DataRow item in departamentos.Rows)
        //    {
        //        Console.WriteLine("{0}.- {1}", item["Id"].ToString(), item["Nombre"].ToString());
        //    }

        //    Console.Write("Departamento: "); producto.DepartmentId = Convert.ToInt32(Console.ReadLine());

        //}

        public static void Update_Producto()
        {

        }

        public static void Delete_Producto()
        {

        }

        public static void View_Departamentos()
        {

        }

        public static void View_Catalogo()
        {

        }
        #endregion

        #region metodos INVENTARIO
        public static void View_Inventario()
        {

        }
        public static void Update_Inventario()
        {

        }
        #endregion

        static void Main(string[] args)
        {
            Identificar_Categoria();

            switch (Categoria)
            {
                case 1:
                    do
                    {
                        Identificar_Operacion1();

                        switch (Operaciones)
                        {
                            case 1:
                                Agregar_Producto();
                                break;
                            case 2:
                                Cobrar_Producto();
                                break;
                            case 3:
                                CAJA();
                                break;
                            case 4:
                                PRECIO();
                                break;
                        }

                        Console.WriteLine("Desea Realizar otra operacion?:\n" +
                            "1.- Si.\n" +
                            "2.- No.");

                        Desicion = Console.Read();
                    } while (Desicion != 1);
                    break;

                case 2:
                    do
                    {
                        Identificar_Operacion2();

                        switch (Operaciones)
                        {
                            case 1:
                                //New_Product();
                                break;
                            case 2:
                                Update_Producto();
                                break;
                            case 3:
                                Delete_Producto();
                                break;
                            case 4:
                                View_Departamentos();
                                break;
                            case 5:
                                View_Catalogo();
                                break;
                        }

                        Console.WriteLine("Desea Realizar otra operacion?:\n" +
                            "1.- Si.\n" +
                            "2.- No.");

                        Desicion = Console.Read();
                    } while (Desicion != 1);
                    break;

                case 3:
                    do
                    {
                        Identificar_Operacion3();

                        switch (Operaciones)
                        {
                            case 1:
                                View_Inventario();
                                break;
                            case 2:
                                Update_Inventario();
                                break;
                        }

                        Console.WriteLine("Desea Realizar otra operacion?:\n" +
                            "1.- Si.\n" +
                            "2.- No.");

                        Desicion = Console.Read();
                    } while (Desicion != 1);
                    break;

            }
 
            Console.ReadKey();
        }
        
    }
}

