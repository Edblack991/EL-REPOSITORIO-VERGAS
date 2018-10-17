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
            DataTable departaments = new DataTable();

            var result = dbCtx.Departamentos.ToString();

            departaments.Columns.AddRange(new DataColumn[]{
                    new DataColumn("Id", typeof(int)),
                    new DataColumn("Nombre", typeof(string))
            });

            result.ToList().ForEach(x =>
            {
                //Variable para guardar las filas traidas de la base de datos.
                var row = departaments.NewRow();

                //Asignarle a cada columna los valores de las filas.
                //row["Id"] = x.Id;
                //row["Nombre"] = x.Nombre;

                //Añadir las filas tomadas de la bd a mi datatable.
                departaments.Rows.Add(row);

            });

            return departaments;
        }

    }
}
