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

namespace SILPA.AccesoDatos.AudienciaPublica
{
   public class FuncionarioPublicoDalc
    {
     /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public FuncionarioPublicoDalc()
        {
            objConfiguracion = new Configuracion();
        }
        

        /// <summary>
        /// Lista los funcionarios publicos en la BD.
        /// </summary>
        /// <param name="intFuncionario" >Valor del identificador del funcionario publico por el cual se filtraran.
        /// si es null no existen restricciones.</param>
        /// <returns>DataSet con los registros y las siguientes columnas:  FPU_ID, FPU_NOMBRE, FPU_EXTENSION</returns>
       public DataSet ListarFuncionario(Nullable<int> intFuncionario)
       {
           SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
           object[] parametros = new object[] { intFuncionario };
           DbCommand cmd = db.GetStoredProcCommand("AUD_LISTA_FUNCIONARIO_PUBLICO", parametros);
           DataSet dsResultado = db.ExecuteDataSet(cmd);
           return (dsResultado);
       }

    }
}
