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
    public class TipoContactoCampoDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public TipoContactoCampoDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public List<TipoContactoCampoIdentity> ListaTipoContactoCampo(int tipoContactoId)
        {
            try
            {
                List<TipoContactoCampoIdentity> lstTipoContactoCampo = new List<TipoContactoCampoIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());
                object[] parametros = new object[] { tipoContactoId };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_TIPO_CONTACTO_CAMPO", parametros);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        lstTipoContactoCampo.Add(new TipoContactoCampoIdentity
                        {
                            TipoContactoId = Convert.ToInt32(reader["TIPO_CONTACTO_ID"]),
                            NombreCampo = reader["NOMBRE_CAMPO"].ToString(),
                            TipoCampo = reader["TIPO_CAMPO"].ToString(),
                            TipoControl = reader["TIPO_CONTROL"].ToString(),
                            EtiquetaCampo = reader["ETIQUETA_CAMPO"].ToString(),
                            Titulo = reader["TOOL_TIP"].ToString(),
                            EsObligatorio = Convert.ToBoolean(reader["ES_OBLIGATORIO"]),
                            CampoId = Convert.ToInt32(reader["CAMPO_ID"])
                        });
                    }

                }
                return lstTipoContactoCampo;

            }
            catch (SqlException sqle)
            {
                return null;
            }
        }
    }
}
