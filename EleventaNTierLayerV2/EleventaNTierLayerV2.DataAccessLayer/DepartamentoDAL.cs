using EleventaNTierLayerV2.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleventaNTierLayerV2.DataAccessLayer
{
    public class DepartamentoDAL
    {
        private static EleventaNTierLayerV2DbContext dbCtx = new EleventaNTierLayerV2DbContext();

        public static DataTable CargarDepartamenTable()
        {
            DataTable departamentos = new DataTable();

            var result = dbCtx.Departamentos.ToList();

            departamentos.Columns.AddRange(new DataColumn[]{
                    new DataColumn("Id", typeof(int)),
                    new DataColumn("Nombre", typeof(string)),
                    new DataColumn("Descripcion", typeof(string)),
            });

            result.ToList().ForEach(x =>
            {
                //Variable para guardar las filas traidas de la base de datos.
                var row = departamentos.NewRow();

                //Asignarle a cada columna los valores de las filas.
                row["Id"] = x.Id;
                row["Nombre"] = x.Nombre;
                row["Descripcion"] = x.Descripcion; 

                //Añadir las filas tomadas de la bd a mi datatable.
                departamentos.Rows.Add(row);

            });

            return departamentos;
        }

    }
}
