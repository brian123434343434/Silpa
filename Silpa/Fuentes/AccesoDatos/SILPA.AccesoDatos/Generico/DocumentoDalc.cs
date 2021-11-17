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
    public class DocumentoDalc
    {
        ///// <summary>
        ///// Objeto de configuracion de la aplicación
        ///// </summary>
        //private Configuracion objConfiguracion;
        
        ///// <summary>
        ///// Constructor vacio inicia el objeto de configuración
        ///// </summary>
        //public DocumentoDalc()
        //{
        //    objConfiguracion = new Configuracion();
        //}

        ///// <summary>
        ///// Constructor de la clase que a su vez carga los valores para una identidad de departamento 
        ///// cuyo valor del identificador corresponda con la BD
        ///// </summary>
        ///// <param name="objIdentity.Id">Valor del identificador del departamento a cargar, en la propiedad ID del objetoIdentity</param>
        //public DocumentoDalc(ref DocumentoIdentity objIdentity)
        //{
        //    try
        //    {
        //        SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
        //        object[] parametros = new object[] {  };
        //        DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_DEPARTAMENTO", parametros);
        //        DataSet dsResultado = db.ExecuteDataSet(cmd);

        //        //objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["DEP_ID"]);
        //        //objIdentity.Nombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["DEP_NOMBRE"]);
        //        //objIdentity.Region = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["REG_ID"]);
        //    }
        //    catch (SqlException sqle)
        //    {
        //        throw new Exception(sqle.Message);
        //    }


        //}

        ///// <summary>
        ///// Carga los valores para una identidad de Departamento cuyo valor del identificador corresponda 
        ///// con la BD
        ///// </summary>
        ///// <param name="objIdentity.Id">Valor del identificador del Departamento a cargar, en la propiedad ID del objetoIdentity</param>
        //public void ObtenerDocumento(ref DocumentoIdentity objIdentity)
        //{
        //    try
        //    {
        //        SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
        //        object[] parametros = new object[] {  };
        //        DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_DEPARTAMENTO", parametros);
        //        DataSet dsResultado = db.ExecuteDataSet(cmd);

        //        //objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["DEP_ID"]);
        //        //objIdentity.Nombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["DEP_NOMBRE"]);
        //        //objIdentity.Region = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["REG_ID"]);
        //    }
        //    catch (SqlException sqle)
        //    {
        //        throw new Exception(sqle.Message);
        //    }
        //}

        ///// <summary>
        ///// Lista los departamentos en la BD. Pueden listarse los departamentos por region o uno en particular.
        ///// </summary>
        ///// <param name="objIdentity.Region" >Valor del identificador de la region por el cual se filtraran los departamentos, si es null no existen restricciones</param>
        ///// <param name="objIdentity.Id" >Con este valor se lista el departamento con el identificador, , si es null no existen restricciones</param>
        ///// <returns>DataSet con los registros y las siguientes columnas: DEP_ID, DEP_NOMBRE, REG_ID, REG_NOMBRE</returns>
        //public DataSet ListarDocumentos(DocumentoIdentity objIdentity)
        //{
        //    try
        //    {
        //        SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
        //        object[] parametros = new object[] {   };
        //        DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_DEPARTAMENTO", parametros);
        //        DataSet dsResultado = db.ExecuteDataSet(cmd);
        //        return (dsResultado);

        //    }
        //    catch (SqlException sqle)
        //    {
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// Lista los departamentos en la BD. Pueden listarse los departamentos por region o uno en particular.
        ///// </summary>
        ///// <param name="intRegion" >Valor del identificador de la region por el cual se filtraran los departamentos, si es null no existen restricciones</param>
        ///// <param name="intIdentity.Id" >Con este valor se lista el departamento con el identificador, , si es null no existen restricciones</param>
        ///// <returns>DataSet con los registros y las siguientes columnas: DEP_ID, DEP_NOMBRE, REG_ID, REG_NOMBRE</returns>
        //public DataSet ListarDocumentos(Nullable<int> intId, Nullable<int> intRegion)
        //{
        //    //  GenericDalc.cm
        //    objConfiguracion = new Configuracion();

        //    try
        //    {
        //        SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

        //        object[] parametros = new object[] { intId, intRegion };

        //        DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_DEPARTAMENTO", parametros);
        //        DataSet dsResultado = db.ExecuteDataSet(cmd);
        //        return (dsResultado);

        //    }
        //    catch (SqlException sqle)
        //    {
        //        return null;
        //    }
        //}
    }
}
