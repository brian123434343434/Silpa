using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;
using System.Data.Common;
using System.Data;

namespace SILPA.AccesoDatos.Generico
{
    public class SolicitanteDalc
    {
        private Configuracion objConfiguracion;

        public SolicitanteEntity ConsultaSolicitante(int? idusuario, string numeroIdentificacion)
        {
            objConfiguracion = new Configuracion();
            SolicitanteEntity solicitante = new SolicitanteEntity();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());
            object[] parametros = new object[] { idusuario, numeroIdentificacion };
            DbCommand cmd = db.GetStoredProcCommand("SS_USU_CON_SOLICITANTE_POR_ID_APPLICATION_USER", parametros);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    solicitante.ID = Convert.ToInt32(reader["PER_ID"]);
                    solicitante.PrimerNombre = reader["PER_PRIMER_NOMBRE"].ToString();
                    solicitante.SegundoNombre = reader["PER_SEGUNDO_NOMBRE"].ToString();
                    solicitante.PrimerApellido = reader["PER_PRIMER_APELLIDO"].ToString();
                    solicitante.SegundoApellido = reader["PER_SEGUNDO_APELLIDO"].ToString();
                    solicitante.RazonSocial = reader["PER_RAZON_SOCIAL"].ToString();
                    solicitante.NombreCompleto = reader["NOMBRE_COMPLETO"].ToString(); ;
                    solicitante.NumeroIdentificacion= reader["PER_NUMERO_IDENTIFICACION"].ToString();
                    solicitante.TipoIdentificacion = Convert.ToInt32(reader["TID_ID"]);
                    solicitante.LugarExpedicion = reader["PER_LUGAR_EXPEDICION_DOC"].ToString();
                    solicitante.Telefono = reader["PER_TELEFONO"].ToString();
                    solicitante.Celular = reader["PER_CELULAR"].ToString();
                    solicitante.Fax = reader["PER_FAX"].ToString();
                    solicitante.CorreoElectronico = reader["PER_CORREO_ELECTRONICO"].ToString();
                    solicitante.TipoPersona = Convert.ToInt32(reader["PER_TIPO_PERSONA"]);
                    solicitante.TarjetaProfesional = reader["PER_TARJETA_PROFESIONAL"].ToString();
                    solicitante.SolPadreId= Convert.ToInt32(reader["PER_ID_SOLICITANTE"]);
                    solicitante.AutId = Convert.ToInt32(reader["AUT_ID"]);
                    solicitante.TipoSolicitante = Convert.ToInt32(reader["TSO_ID"]);
                    solicitante.Activo = Convert.ToBoolean(reader["PER_ACTIVO"]);
                    solicitante.Sila = Convert.ToBoolean(reader["PER_SILA"]);
                    solicitante.IdentificacionNotificacion = reader["TID_NOTIFICACION"].ToString();
                    solicitante.EsNotificacionElectronica = Convert.ToBoolean(reader["ES_NOTIFICACION_ELECTRONICA"]);
                    solicitante.EsNotificacionElectronica_AA = Convert.ToBoolean(reader["ES_NOTIFICACION_ELECTRONICA_X_AA"]);
                    solicitante.EsNotificacionElectronica_EXP = Convert.ToBoolean(reader["ES_NOTIFICACION_ELECTRONICA_X_EXP"]);
          
                }
            }
            return solicitante;
        }
    }
}
