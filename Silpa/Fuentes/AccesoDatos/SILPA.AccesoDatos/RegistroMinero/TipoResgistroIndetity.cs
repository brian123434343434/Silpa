using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SILPA.Comun;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace SILPA.AccesoDatos.RegistroMinero
{
    public class TipoResgistroIndetity
    {
        private Configuracion objConfiguracion;

        public int TipoRegistroID { get; set; }
        public string NombreTipoRegistro { get; set; }

        public TipoResgistroIndetity()
        {
            objConfiguracion = new Configuracion();
        }
        public List<TipoResgistroIndetity> ListaTipoRegistro()
        {
            List<TipoResgistroIndetity> LstTipoRegistro = new List<TipoResgistroIndetity>();

            Database db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("SP_LISTA_TIPO_REGISTRO_MINERO");
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    TipoResgistroIndetity tiporesgistro = new TipoResgistroIndetity();
                    tiporesgistro.TipoRegistroID = Convert.ToInt32(reader["TIPO_REGISTRO_MINERO_ID"]);
                    tiporesgistro.NombreTipoRegistro = reader["NOMBRE_TIPO_REGISTRO_MINERO"].ToString();
                    LstTipoRegistro.Add(tiporesgistro);
                }
            }
            return LstTipoRegistro;
        }
    }
}
