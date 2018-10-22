using EleventaNTierLayerV2.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleventaNTierLayerV2.BusinessLogicLayer
{
    public class VentaBLL
    {
        public static string RealizarVenta(Venta v)
        {
            string msgError = string.Empty;

            bool isCheked = DataAccessLayer.VentaDAL.RealizarVenta(v);

            if (isCheked != true)
            {
                msgError = "No se puedo Insertar la venta";
            }

            return msgError;
        }

        public static int UltimoRegistro()
        {

            return DataAccessLayer.VentaDAL.UltimoRegistro();
        
        }
    }
}
