namespace EleventaNTierLayerV2.BusinessEntities.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EleventaNTierLayerV2.BusinessEntities.EleventaNTierLayerV2DbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EleventaNTierLayerV2.BusinessEntities.EleventaNTierLayerV2DbContext context)
        {
            context.Database.Log = Console.Write;

            using (DbContextTransaction dbTran = context.Database.BeginTransaction())
            {
                try
                {

                    List<Departamento> departamentos = new List<Departamento>();
                    departamentos.Add(new Departamento() { Nombre = "Abarrotes", Descripcion = "Consumo primario" });
                    departamentos.Add(new Departamento() { Nombre = "Electronicos", Descripcion = "Aparatos electricos" });
                    departamentos.Add(new Departamento() { Nombre = "Cocina", Descripcion = "Instrumentos de cocina" });

                    context.Departamentos.AddRange(departamentos);
                    context.SaveChanges();
                    //----------------------------------------------------------------------------------------------------------------------------
                    List<Producto> productos = new List<Producto>();
                    productos.Add(new Producto()
                    {
                        Descripcion = "LECHE NIDO",
                        Cantidad = 10,
                        CodigoBarras = "12345ABCDE",
                        Precio = 70,
                        PrecioMayoreo = 61,
                        Costo = 50,
                        Ganancia = "20%",
                        InvMinima = 10,
                        InvMaxima = 40,
                        IdDepartamento = 1
                    });
                    productos.Add(new Producto()
                    {
                        Descripcion = "Tetrapack Nesquick",
                        Cantidad = 15,
                        CodigoBarras = "67891ABCDE",
                        Precio = 15,
                        PrecioMayoreo = 10,
                        Costo = 7.5,
                        Ganancia = "100%",
                        InvMinima = 10,
                        InvMaxima = 50,
                        IdDepartamento = 1
                    });
                    productos.Add(new Producto()
                    {
                        Descripcion = "Lata Herdez Chile",
                        Cantidad = 20,
                        CodigoBarras = "12346ABCDE",
                        Precio = 12,
                        PrecioMayoreo = 8,
                        Costo = 5,
                        Ganancia = "120%",
                        InvMinima = 10,
                        InvMaxima = 40,
                        IdDepartamento = 1
                    });
                    context.Productos.AddRange(productos);
                    context.SaveChanges();
                    //----------------------------------------------------------------------------------------------------------------------------

                    //                   List<Corte> corte = new List<Corte>();
                    //                   corte.Add(new Corte() { FondoInicial = 500 , CantidadFinal = 2000,  Diferencia = 1500 , Fecha = DateTime.Now,
                    //                       IdVenta =  });

                    //                   context.Cortes.AddRange(corte);
                    //                   context.SaveChanges();
                    ////----------------------------------------------------------------------------------------------------------------------------

                    //                   List<Venta> ventas = new List<Venta>();
                    //                   ventas.Add(new Venta() { NumeroArticulos = , IdProducto = , SubTotal = , Iva = , Total = ,  Pago = ,
                    //                       Cambio = , IdEmpleado = , Fecha = DateTime.Now });

                    //                   context.Ventas.AddRange(ventas);
                    //                   context.SaveChanges();
                    //----------------------------------------------------------------------------------------------------------------------------

                    dbTran.Commit(); //ejecutar cambios
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors: ",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("-Property: \"{0}\",Error; \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    dbTran.Rollback(); //Descartar cambios
                    throw;
                }
            }
            base.Seed(context);
        }
    }
    }
