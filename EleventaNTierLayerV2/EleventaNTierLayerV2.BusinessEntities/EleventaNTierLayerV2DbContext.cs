using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleventaNTierLayerV2.BusinessEntities
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]

    public class EleventaNTierLayerV2DbContext : DbContext 
    {
        public EleventaNTierLayerV2DbContext() : base("EleventaNTierLayerV2DbContext")
        {
            //Database.SetInitializer<EleventaNTierLayerV2DbContext>(new EleventaNTierLayerV2DbInitializer());
        }

        public DbSet<Corte> Cortes { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Detalle_Venta> Detalle_Ventas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
