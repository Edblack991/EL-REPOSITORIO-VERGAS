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

        }

        DbSet<Corte> Cortes { get; set; }
        DbSet<Departamento> Departamentos { get; set; }
        DbSet<Venta> Ventas { get; set; }
        DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
