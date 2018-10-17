using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleventaNTierLayerV2.BusinessLogicLayer
{
    public class DepartamentoBLL
    {

        public static DataTable CargarDepartamenTable()
        {

            DataTable dt = new DataTable();

            dt = DataAccessLayer.DepartamentoDAL.CargarDepartamenTable();

            return dt;

        }

        /// <summary>
        /// Metodo que nos servira para validar los DataAnnotation del proyecto BusinessEntities.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        public static bool Validate<T>(T obj, out ICollection<ValidationResult> results)
        {
            results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, new ValidationContext(obj), results, true);
        }
    }
}
