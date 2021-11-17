using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Generico
{
    public class TipoContactoDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

         /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public TipoContactoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public List<TipoContactoIdentity> ListaTipoContacto()
        {
            try
            {
                List<TipoContactoIdentity> lstTipoContacto = new List<TipoContactoIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_TIPO_CONTACTO");
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        lstTipoContacto.Add(new TipoContactoIdentity { TipoContactoId = Convert.ToInt32(reader["TIPO_CONTACTO_ID"]), NombreTipoContacto = reader["NOMBRE_TIPO_CONTACTO"].ToString() });
                    }

                }
                return lstTipoContacto;

            }
            catch (SqlException sqle)
            {
                return null;
            }
        }

    }
}
