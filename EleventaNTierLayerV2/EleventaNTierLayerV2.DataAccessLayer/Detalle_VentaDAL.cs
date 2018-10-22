using EleventaNTierLayerV2.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleventaNTierLayerV2.DataAccessLayer
{
    public class Detalle_VentaDAL
    {
        /// <summary>
        /// metodo para poder insertar el detalle de las ventas
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static bool insertar(Detalle_Venta d)
        {
            bool isInsert = false;

            using (EleventaNTierLayerV2DbContext Db = new EleventaNTierLayerV2DbContext())
            {

                Db.Detalle_Ventas.Add(d);

                int rowsAffected = Db.SaveChanges();

                if (rowsAffected > 0)
                {
                    isInsert = true;
                }

            }
            return isInsert;

        }
    }
}
