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
    /// Clase encargada de la manipulación de los datos de municipio
    /// </summary>
    public class MunicipioDalc
    {

        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public MunicipioDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Constructor de la clase que a su vez carga los valores para una identidad de Municipio 
        /// cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador del municipio a cargar, en la propiedad ID del objetoIdentity</param>
        public MunicipioDalc(ref MunicipioIdentity objIdentity)
        {
            objConfiguracion = new Configuracion();
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.Id, objIdentity.DeptoId, objIdentity.RegionalId };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_MUNICIPIO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["MUN_ID"]);
                objIdentity.DeptoId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["MUN_VALOR_TIQUETE"]);
                objIdentity.MunicipioValor = (float)(dsResultado.Tables[0].Rows[0]["DEP_ID"]);
                objIdentity.NombreMunicipio = Convert.ToString(dsResultado.Tables[0].Rows[0]["MUN_NOMBRE"]);
                objIdentity.RegionalId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["RGN_ID"]);
                objIdentity.UbicacionId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["UBI_ID"]);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }


        }

        /// <summary>
        /// Carga los valores para una identidad de Municipio cuyo valor del identificador corresponda 
        /// con la BD        
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador del municipio a cargar, en la propiedad ID del objetoIdentity</param>
        public void ObtenerMunicipios(ref MunicipioIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] {objIdentity.RegionalId, objIdentity.DeptoId, objIdentity.Id };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_MUNICIPIO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                objIdentity.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["MUN_ID"]);
                objIdentity.DeptoId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["DEP_ID"]);
                objIdentity.MunicipioValor =Convert.ToSingle(dsResultado.Tables[0].Rows[0]["MUN_VALOR_TIQUETE"]);
                objIdentity.NombreMunicipio = Convert.ToString(dsResultado.Tables[0].Rows[0]["MUN_NOMBRE"]);
                objIdentity.RegionalId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["RGN_ID"]);
                objIdentity.UbicacionId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["UBI_ID"]);



            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }


        }

        /// <summary>
        /// Lista los municipios en la BD. Pueden listarse los municipios por departamento, regional o un municipio en particular.
        /// </summary>
        /// <param name="objIdentity.RegionalId" >Valor del identificador de la regional por el que se filtraran los municipios, si es null no existen restricciones</param>
        /// <param name="objIdentity.DeptoId" >Valor del identificador del departamento por el que se filtraran los municipios, si es null no existen restricciones</param>
        /// <param name="objIdentity.Id" >Con este valor se lista el municipio con el identificador, si es null no existen restricciones</param>
        /// <returns>DataSet con los registros y las siguientes columnas:  MUN_ID, MUN_NOMBRE, MUN_VALOR_TIQUETE, DEP_ID, DEP_NOMBRE, RGN_ID, RGN_NOMBRE, UBI_ID, UBI_NOMBRE, </returns>
        public DataSet ListarMunicipios(MunicipioIdentity objIdentity)
        {

            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.RegionalId, objIdentity.DeptoId, objIdentity.Id  };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_MUNICIPIO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException sqle)
            {
                return null;
            }
        }

        /// <summary>
        /// Lista los municipios. Exposible listar los municipios de un departamento o una regional.
        /// </summary>
        /// <param name="intId" >Con solo este valor se lista el municipio con el ID</param>
        /// <param name="intDeptoId" >Con solo este valor se listan los municipios del departamento en particular</param>
        /// <param name="intRegionalId" >Con solo este valor se listan los municipios del la regional en particular</param>
        /// <returns>DataSet con los registros y las siguientes columnas:  MUN_ID, MUN_NOMBRE, MUN_VALOR_TIQUETE, DEP_ID, DEP_NOMBRE, RGN_ID, RGN_NOMBRE, UBI_ID, UBI_NOMBRE</returns>
        public DataSet ListarMunicipios(Nullable<int> intId, Nullable<int> intDeptoId, Nullable<int> intRegionalId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] {intRegionalId, intDeptoId, intId};

                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_MUNICIPIO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                return null;
            }
        }


        /// <summary>
        /// Obtener el listado de municipios asociados a un medio de transporte (Autoliquidación)
        /// </summary>
        /// <param name="p_intDepartamentoID">int con el identificador del deparrtamento al cual pertenecen los municipios</param>
        /// <param name="p_intMedioTransporteID">int con el identificador del medio de transporte</param>
        /// <returns>List con la información de los municipios</returns>
        public List<MunicipioIdentity> ListarMuncicipiosDepartamentoMedioTransporteLiquidacion(int p_intDepartamentoID, int p_intMedioTransporteID)
        {
            SqlDatabase objBaseDatos = null;
            DbCommand objCommand = null;
            DataSet objDatosMunicipios = null;
            object[] objParametros = null;
            List<MunicipioIdentity> objLstMunicipios = null;


            try
            {
                //Cargar la configuración
                objConfiguracion = new Configuracion();

                //Crear la conexión
                objBaseDatos = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar los parametros
                objParametros = new object[] { p_intDepartamentoID, p_intMedioTransporteID };

                //Obtener datos de los departamentos
                objCommand = objBaseDatos.GetStoredProcCommand("AUTOLIQ_LISTA_MUNICIPIOS_ORIGEN_MEDIO", objParametros);
                objDatosMunicipios = objBaseDatos.ExecuteDataSet(objCommand);

                //Verificar si se encontro información
                if (objDatosMunicipios != null && objDatosMunicipios.Tables != null && objDatosMunicipios.Tables.Count > 0 && objDatosMunicipios.Tables[0].Rows.Count > 0)
                {
                    //Crear listado
                    objLstMunicipios = new List<MunicipioIdentity>();

                    //Ciclo que carga los departamentos
                    foreach (DataRow objDatosMunicipio in objDatosMunicipios.Tables[0].Rows)
                    {
                        objLstMunicipios.Add(new MunicipioIdentity
                                                    {
                                                        Id = Convert.ToInt32(objDatosMunicipio["MUN_ID"]),
                                                        NombreMunicipio = objDatosMunicipio["MUN_NOMBRE"].ToString()
                                                    }
                                            );
                    }
                }

            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "MunicipioDalc :: ListarMuncicipiosDepartamentoAutoridadAmbientalMedioTransporteLiquidacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "MunicipioDalc :: ListarMuncicipiosDepartamentoAutoridadAmbientalMedioTransporteLiquidacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }

            return objLstMunicipios;
        }


        /// <summary>
        /// Obtener el listado de municipios destino asociados a un medio de transporte (Autoliquidación)
        /// </summary>
        /// <param name="p_intMunipioOrigenID">int con el municipio de origen</param>
        /// <param name="p_intDepartamentoID">int con el identificador del deparrtamento al cual pertenecen los municipios</param>
        /// <param name="p_intMedioTransporteID">int con el identificador del medio de transporte</param>
        /// <returns>List con la información de los municipios</returns>
        public List<MunicipioIdentity> ListarMuncicipiosDestinoDepartamentoMedioTransporteLiquidacion(int p_intMunipioOrigenID, int p_intDepartamentoID, int p_intMedioTransporteID)
        {
            SqlDatabase objBaseDatos = null;
            DbCommand objCommand = null;
            DataSet objDatosMunicipios = null;
            object[] objParametros = null;
            List<MunicipioIdentity> objLstMunicipios = null;


            try
            {
                //Cargar la configuración
                objConfiguracion = new Configuracion();

                //Crear la conexión
                objBaseDatos = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar los parametros
                objParametros = new object[] { p_intMunipioOrigenID, p_intDepartamentoID, p_intMedioTransporteID };

                //Obtener datos de los departamentos
                objCommand = objBaseDatos.GetStoredProcCommand("AUTOLIQ_LISTA_MUNICIPIOS_DESTINO_MEDIO", objParametros);
                objDatosMunicipios = objBaseDatos.ExecuteDataSet(objCommand);

                //Verificar si se encontro información
                if (objDatosMunicipios != null && objDatosMunicipios.Tables != null && objDatosMunicipios.Tables.Count > 0 && objDatosMunicipios.Tables[0].Rows.Count > 0)
                {
                    //Crear listado
                    objLstMunicipios = new List<MunicipioIdentity>();

                    //Ciclo que carga los departamentos
                    foreach (DataRow objDatosMunicipio in objDatosMunicipios.Tables[0].Rows)
                    {
                        objLstMunicipios.Add(new MunicipioIdentity
                        {
                            Id = Convert.ToInt32(objDatosMunicipio["MUN_ID"]),
                            NombreMunicipio = objDatosMunicipio["MUN_NOMBRE"].ToString()
                        }
                                            );
                    }
                }

            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "MunicipioDalc :: ListarMuncicipiosDestinoDepartamentoAutoridadAmbientalMedioTransporteLiquidacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "MunicipioDalc :: ListarMuncicipiosDestinoDepartamentoAutoridadAmbientalMedioTransporteLiquidacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }

            return objLstMunicipios;
        }

    }
}
