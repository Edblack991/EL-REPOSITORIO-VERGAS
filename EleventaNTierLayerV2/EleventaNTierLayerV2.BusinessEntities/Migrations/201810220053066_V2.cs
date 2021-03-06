namespace EleventaNTierLayerV2.BusinessEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V2 : DbMigration
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
                .Index(t => t.IdVenta);
            
            CreateTable(
                "dbo.Venta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sucursal = c.String(nullable: false, unicode: false),
                        Fecha = c.DateTime(nullable: false, precision: 0),
                        Importe = c.Double(nullable: false),
                        CantidadDeArticulos = c.Int(nullable: false),
                        Caja = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Detalle_Venta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdProducto = c.Int(nullable: false),
                        IdVenta = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        Total = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Producto", t => t.IdProducto, cascadeDelete: true)
                .ForeignKey("dbo.Venta", t => t.IdVenta, cascadeDelete: true)
                .Index(t => t.IdProducto)
                .Index(t => t.IdVenta);
            
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
                        UsaInventario = c.Boolean(nullable: false),
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
            DropForeignKey("dbo.Detalle_Venta", "IdVenta", "dbo.Venta");
            DropForeignKey("dbo.Detalle_Venta", "IdProducto", "dbo.Producto");
            DropForeignKey("dbo.Producto", "IdDepartamento", "dbo.Departamento");
            DropIndex("dbo.Corte", new[] { "IdVenta" });
            DropIndex("dbo.Detalle_Venta", new[] { "IdVenta" });
            DropIndex("dbo.Detalle_Venta", new[] { "IdProducto" });
            DropIndex("dbo.Producto", new[] { "IdDepartamento" });
            DropTable("dbo.Departamento");
            DropTable("dbo.Producto");
            DropTable("dbo.Detalle_Venta");
            DropTable("dbo.Venta");
            DropTable("dbo.Corte");
        }
    }
}
