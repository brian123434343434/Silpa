using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.Comun;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data.SqlClient;

namespace SILPA.AccesoDatos.Utilidades
{
    public class UbicacionDalc
    {
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio
        /// </summary>
        public UbicacionDalc()
        {
            objConfiguracion = new Configuracion();
        }

        ///// <summary>
        ///// Modifica en la tabla Expediente_Ubicacion
        ///// </summary>
        ///// <param name="objIdentity">Objeto que contiene los valores de los campos que se van a modificiar</param>
        //public void ActualizarUbicacion(ref UbicacionIdentity objIdentity)
        //{
        //    try
        //    {
        //        SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());

        //        object[] parametros = new object[] 
        //                              { 
        //                                  objIdentity.IdUbicacion,
        //                                  objIdentity.IdDepartamento,
        //                                  objIdentity.Idmunicipio,
        //                                  objIdentity.IdCorregimiento,
        //                                  objIdentity.IdCuenca,
        //                                  objIdentity.UbicacionID,
        //                                  objIdentity.UbicacionNombre
        //                              };

        //        DbCommand cmd = db.GetStoredProcCommand("GEN_ASOCIAR_RADICACION_AA", parametros);
        //        DataSet dsResultado = db.ExecuteDataSet(cmd);
        //        objIdentity.IdUbicacion = int.Parse(cmd.Parameters[7].Value.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}


        /// <summary>
        /// Insertar en la tabla Expediente_Ubicacion
        /// </summary>
        /// <param name="objIdentity">Objeto que contiene los valores de los campos que se van a insertar</param>
        public void InsertarUbicacion(ref UbicacionIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());

                object[] parametros = new object[] 
                                      { objIdentity.IdUbicacion,
                                          objIdentity.IdDepartamento,
                                          objIdentity.Idmunicipio,
                                          objIdentity.IdCorregimiento,
                                          objIdentity.IdCuenca,
                                          objIdentity.UbicacionID,
                                          objIdentity.UbicacionNombre
                                      };

                DbCommand cmd = db.GetStoredProcCommand("SS_ADI_UBICACION", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                objIdentity.IdUbicacion = int.Parse(cmd.Parameters["@ID_UBICACION"].Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Retorna un DataSet con los datos de una ubicacion especifica
        /// </summary>
        /// <param name="intIdAA">int: indentificador de la ubicacion</param>
        /// <returns>DataSet: Conjunto de resultados de ubicacion</returns>
        public DataSet ObtenerUbicacion(int intIdUbicacion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());

                object[] parametros = new object[] { intIdUbicacion };

                DbCommand cmd = db.GetStoredProcCommand("SS_CON_UBICACION", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;

            }
            catch (SqlException sqle)
            {
                return null;
            }

        }

        /// <summary>
        /// Lista en un DataSet los departamentos y los municipios asociados
        /// </summary>
        /// <param name="idDepto">Identificador del Departamento se puede usar el comodin -1 para imprimir todos</param>
        /// <param name="idMun">Identificador del municipio se puede usar el comodin -1 para imprimir todos</param>
        /// <returns>Retorna un DataSet con todos los departamentos y sus municipios definidos en los cireterios de busqueda</returns>
        public DataSet ListarDeptosMunicipios(int idDepto, int idMun)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());
                object[] parametros = new object[] { idDepto, idMun };
                DbCommand cmd = db.GetStoredProcCommand("SS_DEPTO_RELACIONES_MUN", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista en un DataSet los departamentos
        /// </summary>
        /// <param name="idDepto">Identificador del Departamento se puede usar el comodin -1 para imprimir todos</param>
        /// <returns>Retorna un DataSet con todos los departamentos</returns>
        public DataSet ListarDeptos(int idDepto)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());
                object[] parametros = new object[] { idDepto };
                DbCommand cmd = db.GetStoredProcCommand("SS_LISTAR_DEPTO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista en un DataSet los corregimientos del municipio
        /// </summary>
        /// <param name="idMunicipio">Identificador del municipio</param>
        /// <returns>Retorna un DataSet con todos los corregimientos del municipio</returns>
        public DataSet ListarCorregimientos(int idMunicipio)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());
                object[] parametros = new object[] { idMunicipio };
                DbCommand cmd = db.GetStoredProcCommand("SS_LISTAR_CORREGIMIENTO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista en un DataSet con las veredas pertenecientes a un municipio
        /// </summary>
        /// <param name="idMunicipio">Identificador del municipio </param>
        /// <returns>Retorna un DataSet con todos las veredas del municipio</returns>
        public DataSet ListarVeredas(int idMunicipio)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());
                object[] parametros = new object[] { idMunicipio };
                DbCommand cmd = db.GetStoredProcCommand("SS_LISTAR_VEREDAS", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista en un DataSet con las cuencas
        /// </summary>
        /// <returns>Retorna un DataSet con todos las cuencas</returns>
        public DataSet ListarCuencas()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SS_LISTAR_CUENCAS");
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return dsResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
