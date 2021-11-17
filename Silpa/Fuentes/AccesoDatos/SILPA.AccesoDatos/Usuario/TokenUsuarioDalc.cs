using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using System.Configuration;
using SILPA.Comun;
using System.Data.SqlClient;

namespace SILPA.AccesoDatos.Usuario
{
    /// <summary>
    /// Clase con los metodos de acceso a la capa de datos para el Token de usuario
    /// </summary>
    public class TokenUsuarioDalc
    {
        /// <summary>
        /// variable que permite acceder a los registros de configuración del sistema
        /// </summary>
        private Configuracion objConfiguracion;
        public TokenUsuarioDalc()
        {
            objConfiguracion = new Configuracion();
        }
        /// <summary>
        /// Metodo que por medio del Id de usuario encuetra el ultimo token activi
        /// para él.
        /// </summary>
        /// <param name="id">numerico : Identificador del usuario</param>
        /// <returns>objeto TokenUsuarioIdentity</returns>
        public TokenUsuarioIdentity ObtenerToken(int id)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            try
            {
                DbParameter par = new SqlParameter();
                par.DbType = DbType.Decimal;  
                par.Direction = ParameterDirection.Input;
                par.ParameterName = "@P_IDUSER";
                par.Value = id;

                DbCommand cmd = db.GetSqlStringCommand("SELECT SILPA_PRE.DBO.GEN_LISTAR_ULTIMO_TOKEN_ACTIVO(@P_IDUSER)");
                cmd.Parameters.Add(par);  

                DataSet dsResultado = db.ExecuteDataSet(cmd);

                TokenUsuarioIdentity objIdentity = new TokenUsuarioIdentity();

                //Debería estar implementado el SP de almacenamiento y busqueda de la
                //identidad - MIRM
                objIdentity.IdUsuario = new UsuarioIdentity();
                objIdentity.IdUsuario.Id = id;
                if (dsResultado.Tables[0].Rows[0][0] != null)
                    objIdentity.Token = dsResultado.Tables[0].Rows[0][0].ToString();
                return objIdentity;
            }
            finally
            {
                db = null;
            }
        }
    }
}
