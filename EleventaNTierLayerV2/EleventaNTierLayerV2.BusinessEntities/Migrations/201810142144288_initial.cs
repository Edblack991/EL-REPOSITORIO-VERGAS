namespace EleventaNTierLayerV2.BusinessEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Corte",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FondoInicial = c.Double(nullable: false),
                        CantidadFinal = c.Double(nullable: false),
                        Diferencia = c.Double(nullable: false),
                        Fecha = c.DateTime(nullable: false, precision: 0),
                        IdVenta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Venta", t => t.IdVenta, cascadeDelete: true)
                .Index(t => t.IdVenta)

                ;

            CreateTable(
                "dbo.Venta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroArticulos = c.Int(nullable: false),
                        IdProducto = c.Int(nullable: false),
                        SubTotal = c.Double(nullable: false),
                        Iva = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                        Pago = c.Double(nullable: false),
                        Cambio = c.Double(nullable: false),
                        IdEmpleado = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empleado", t => t.IdEmpleado, cascadeDelete: true)
                .ForeignKey("dbo.Producto", t => t.IdProducto, cascadeDelete: true)
                .Index(t => t.IdEmpleado)
                .Index(t => t.IdProducto)
                ;
            
            CreateTable(
                "dbo.Empleado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, unicode: false),
                        Puesto = c.String(nullable: false, unicode: false),
                        Caja = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Producto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, unicode: false),
                        Cantidad = c.Int(nullable: false),
                        CodigoBarras = c.String(nullable: false, unicode: false),
                        Precio = c.Double(nullable: false),
                        PrecioMayoreo = c.Double(nullable: false),
                        Costo = c.Double(nullable: false),
                        Ganancia = c.String(nullable: false, unicode: false),
                        InvMinima = c.Int(nullable: false),
                        InvMaxima = c.Int(nullable: false),
                        IdDepartamento = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departamento", t => t.IdDepartamento, cascadeDelete: true)
             .Index(t => t.IdDepartamento);
            
            CreateTable(
                "dbo.Departamento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, unicode: false),
                        Descripcion = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Corte", "IdVenta", "dbo.Venta");
            DropForeignKey("dbo.Venta", "IdProducto", "dbo.Producto");
            DropForeignKey("dbo.Producto", "IdDepartamento", "dbo.Departamento");
            DropForeignKey("dbo.Venta", "IdEmpleado", "dbo.Empleado");
            DropIndex("dbo.Corte", new[] { "IdVenta" });
            DropIndex("dbo.Venta", new[] { "IdProducto" });
            DropIndex("dbo.Producto", new[] { "IdDepartamento" });
            DropIndex("dbo.Venta", new[] { "IdEmpleado" });
            DropTable("dbo.Departamento");
            DropTable("dbo.Producto");
            DropTable("dbo.Empleado");
            DropTable("dbo.Venta");
            DropTable("dbo.Corte");
        }
    }
}
