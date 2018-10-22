using EleventaNTierLayerV2.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleventaNTierLayerV2.DataAccessLayer
{
    public class VentaDAL
    {
        public static bool RealizarVenta(Venta v)
        {

            bool isInsert = false;

            using (EleventaNTierLayerV2DbContext Db = new EleventaNTierLayerV2DbContext())
            {
                Db.Ventas.Add(v);

                int rowsAffected = Db.SaveChanges();

                if (rowsAffected > 0)
                {
                    isInsert = true;
                }
            }

            return isInsert;

        }

        public static int UltimoRegistro()
        {
            int venta;

            using (EleventaNTierLayerV2DbContext Db = new EleventaNTierLayerV2DbContext())
            {
                venta = Db.Ventas.OrderByDescending(x => x.Id).First().Id;
            }

            return venta;

        }


    }
}
