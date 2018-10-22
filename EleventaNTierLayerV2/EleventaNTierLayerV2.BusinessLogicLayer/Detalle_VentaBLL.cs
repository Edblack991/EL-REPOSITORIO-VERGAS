using EleventaNTierLayerV2.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleventaNTierLayerV2.BusinessLogicLayer
{
    public class Detalle_VentaBLL
    {/// <summary>
    /// metodo para validar el regisrto de la insersion de el detalle de las ventas
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
        public static bool insertar(Detalle_Venta d)
        {

            return DataAccessLayer.Detalle_VentaDAL.insertar(d);

        }
    }
}
