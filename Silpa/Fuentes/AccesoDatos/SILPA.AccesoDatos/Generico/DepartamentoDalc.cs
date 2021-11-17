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
using SoftManagement.Log;

namespace SILPA.AccesoDatos.Generico
{
    /// <summary>
    /// Clase encargada de la manipulación de los datos Departamento
    /// </summary>
    public class DepartamentoDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public DepartamentoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Constructor de la clase que a su vez carga los valores para una identidad de departamento 
        /// cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador del departamento a cargar, en la propiedad ID del objetoIdentity</param>
        public DepartamentoDalc(ref DepartamentoIdentity objIdentity)
        {
            objConfiguracion = new Configuracion();
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.Id, objIdentity.Region };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_DEPARTAMENTO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["DEP_ID"]);
                objIdentity.Nombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["DEP_NOMBRE"]);
                objIdentity.Region = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["REG_ID"]);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }


        }

        /// <summary>
        /// Carga los valores para una identidad de Departamento cuyo valor del identificador corresponda 
        /// con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador del Departamento a cargar, en la propiedad ID del objetoIdentity</param>
        public void ObtenerDepartamentos(ref DepartamentoIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] {objIdentity.Region, objIdentity.Id};
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_DEPARTAMENTO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["DEP_ID"]);
                objIdentity.Nombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["DEP_NOMBRE"]);
                objIdentity.Region = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["REG_ID"]);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Lista los departamentos en la BD. Pueden listarse los departamentos por region o uno en particular.
        /// </summary>
        /// <param name="objIdentity.Region" >Valor del identificador de la region por el cual se filtraran los departamentos, si es null no existen restricciones</param>
        /// <param name="objIdentity.Id" >Con este valor se lista el departamento con el identificador, , si es null no existen restricciones</param>
        /// <returns>DataSet con los registros y las siguientes columnas: DEP_ID, DEP_NOMBRE, REG_ID, REG_NOMBRE</returns>
        public DataSet ListarDepartamentos(DepartamentoIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.Id, objIdentity.Region  };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_DEPARTAMENTO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                return null;
            }
        }

        /// <summary>
        /// Lista los departamentos en la BD. Pueden listarse los departamentos por region o uno en particular.
        /// </summary>
        /// <param name="intRegion" >Valor del identificador de la region por el cual se filtraran los departamentos, si es null no existen restricciones</param>
        /// <param name="intIdentity.Id" >Con este valor se lista el departamento con el identificador, , si es null no existen restricciones</param>
        /// <returns>DataSet con los registros y las siguientes columnas: DEP_ID, DEP_NOMBRE, REG_ID, REG_NOMBRE</returns>
        public DataSet ListarDepartamentos(Nullable<int> intId, Nullable<int> intRegion)
        {
            //  GenericDalc.cm
            objConfiguracion = new Configuracion();

            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { intId, intRegion };

                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_DEPARTAMENTO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                return null;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="intId">Valor del identificador de la region por el cual se filtraran los departamentos, si es null no existen restricciones</param>
        /// <param name="intRegion">Con este valor se lista el departamento con el identificador, , si es null no existen restricciones</param>
        /// <param name="intAutID">Id de la autoridad ambiental</param>
        /// <returns></returns>
        public DataSet ListarDepartamentosPorAutoridadAmbiental(Nullable<int> intId, Nullable<int> intRegion, Nullable<int> intAutID)
        {
            //  GenericDalc.cm
            objConfiguracion = new Configuracion();

            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { intId, intRegion, intAutID };

                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_DEPARTAMENTO_AUT", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                return null;
            }
        }


        /// <summary>
        /// Obtener el listado de departamentos (Autoliquidación)
        /// </summary>
        /// <returns>List con la información de los departamentos</returns>
        public List<DepartamentoIdentity> ListarDepartamentosLiquidacion()
        {
            SqlDatabase objBaseDatos = null;
            DbCommand objCommand = null;
            DataSet objDatosDepartamentos = null;
            List<DepartamentoIdentity> objLstDepartamentos = null;

            try
            {
                //Cargar la configuración
                objConfiguracion = new Configuracion();

                //Crear la conexión
                objBaseDatos = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                
                //Obtener datos de los departamentos
                objCommand = objBaseDatos.GetStoredProcCommand("AUTOLIQ_LISTA_DEPARTAMENTOS");
                objDatosDepartamentos = objBaseDatos.ExecuteDataSet(objCommand);

                //Verificar si se encontro información
                if (objDatosDepartamentos != null && objDatosDepartamentos.Tables != null && objDatosDepartamentos.Tables.Count > 0 && objDatosDepartamentos.Tables[0].Rows.Count > 0)
                {
                    //Crear listado
                    objLstDepartamentos = new List<DepartamentoIdentity>();

                    //Ciclo que carga los departamentos
                    foreach (DataRow objDatosDepartamento in objDatosDepartamentos.Tables[0].Rows)
                    {
                        objLstDepartamentos.Add(new DepartamentoIdentity
                                                    {
                                                        Id = Convert.ToInt32(objDatosDepartamento["DEP_ID"]),
                                                        Nombre = objDatosDepartamento["DEP_NOMBRE"].ToString()
                                                    }
                                                );
                    }
                }

            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DepartamentoDalc :: ListarDepartamentosLiquidacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DepartamentoDalc :: ListarDepartamentosLiquidacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }

            return objLstDepartamentos;
        }

        

        /// <summary>
        /// Obtener el listado de departamentos asociados a un medio de transporte (Autoliquidación)
        /// </summary>
        /// <param name="p_intMedioTransporteID">int con el identificador del medio de transporte</param>
        /// <returns>List con la información de los departamentos</returns>
        public List<DepartamentoIdentity> ListarDepartamentosMedioTransporteLiquidacion(int p_intMedioTransporteID)
        {
            SqlDatabase objBaseDatos = null;
            DbCommand objCommand = null;
            DataSet objDatosDepartamentos = null;
            object[] objParametros = null;
            List<DepartamentoIdentity> objLstDepartamentos = null;


            try
            {
                //Cargar la configuración
                objConfiguracion = new Configuracion();

                //Crear la conexión
                objBaseDatos = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar los parametros
                objParametros = new object[] { p_intMedioTransporteID };

                //Obtener datos de los departamentos
                objCommand = objBaseDatos.GetStoredProcCommand("AUTOLIQ_LISTA_DEPARTAMENTOS_ORIGEN_MEDIO", objParametros);
                objDatosDepartamentos = objBaseDatos.ExecuteDataSet(objCommand);

                //Verificar si se encontro información
                if (objDatosDepartamentos != null && objDatosDepartamentos.Tables != null && objDatosDepartamentos.Tables.Count > 0 && objDatosDepartamentos.Tables[0].Rows.Count > 0)
                {
                    //Crear listado
                    objLstDepartamentos = new List<DepartamentoIdentity>();

                    //Ciclo que carga los departamentos
                    foreach (DataRow objDatosDepartamento in objDatosDepartamentos.Tables[0].Rows)
                    {
                        objLstDepartamentos.Add(new DepartamentoIdentity
                                                    {
                                                        Id = Convert.ToInt32(objDatosDepartamento["DEP_ID"]),
                                                        Nombre = objDatosDepartamento["DEP_NOMBRE"].ToString()
                                                    }
                                                );
                    }
                }

            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DepartamentoDalc :: ListarDepartamentosMedioTransporteLiquidacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DepartamentoDalc :: ListarDepartamentosMedioTransporteLiquidacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }

            return objLstDepartamentos;
        }


        /// <summary>
        /// Obtener el listado de departamentos de destino asociados a una atoridad ambiental y medio de transporte (Autoliquidación)
        /// </summary>
        /// <param name="p_intMunipioOrigenID">int con el municipio de origen</param>
        /// <param name="p_intMedioTransporteID">int con el identificador del medio de transporte</param>
        /// <returns>List con la información de los departamentos</returns>
        public List<DepartamentoIdentity> ListarDepartamentosDestinoMedioTransporteLiquidacion(int p_intMunipioOrigenID, int p_intMedioTransporteID)
        {
            SqlDatabase objBaseDatos = null;
            DbCommand objCommand = null;
            DataSet objDatosDepartamentos = null;
            object[] objParametros = null;
            List<DepartamentoIdentity> objLstDepartamentos = null;


            try
            {
                //Cargar la configuración
                objConfiguracion = new Configuracion();

                //Crear la conexión
                objBaseDatos = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar los parametros
                objParametros = new object[] { p_intMunipioOrigenID, p_intMedioTransporteID };

                //Obtener datos de los departamentos
                objCommand = objBaseDatos.GetStoredProcCommand("AUTOLIQ_LISTA_DEPARTAMENTOS_DESTINO_MEDIO", objParametros);
                objDatosDepartamentos = objBaseDatos.ExecuteDataSet(objCommand);

                //Verificar si se encontro información
                if (objDatosDepartamentos != null && objDatosDepartamentos.Tables != null && objDatosDepartamentos.Tables.Count > 0 && objDatosDepartamentos.Tables[0].Rows.Count > 0)
                {
                    //Crear listado
                    objLstDepartamentos = new List<DepartamentoIdentity>();

                    //Ciclo que carga los departamentos
                    foreach (DataRow objDatosDepartamento in objDatosDepartamentos.Tables[0].Rows)
                    {
                        objLstDepartamentos.Add(new DepartamentoIdentity
                        {
                            Id = Convert.ToInt32(objDatosDepartamento["DEP_ID"]),
                            Nombre = objDatosDepartamento["DEP_NOMBRE"].ToString()
                        }
                                                );
                    }
                }

            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DepartamentoDalc :: ListarDepartamentosDestinoMedioTransporteLiquidacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DepartamentoDalc :: ListarDepartamentosDestinoMedioTransporteLiquidacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }

            return objLstDepartamentos;
        }


    }
}
