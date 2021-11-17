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

namespace SILPA.AccesoDatos.Generico
{
   public class FormularioPersonaDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
       public FormularioPersonaDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Lista la configuracion del formulario por tipo de tramite para el tipo de persona en la BD.
        /// </summary>
        /// <param name="intFormulario" >Valor del identificador del formulario por el cual se filtraran.
        /// <param name="intProceso" >Valor del identificador del proceso por el cual se filtraran.
        /// si es null no existen restricciones.</param>
        /// <returns>DataSet con los registros y las siguientes columnas:  FPE_ID, FPE_NOMBRE, PTR_ID</returns>

       public DataSet ListarFormularioPersona(Nullable<int> intFormulario, Nullable<int> intProceso, string strFormulario)
       {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

            object[] parametros = new object[] { intFormulario, intProceso, strFormulario };

            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_FORMULARIO_PERSONA", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return (dsResultado);
        }
        
    }
}
