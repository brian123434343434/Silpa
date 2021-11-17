using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data.SqlClient;

namespace SILPA.AccesoDatos.Generico
{
    public class PermisosCobroDalc
    {

        private Configuracion objConfiguracion;

        #region Creadoras

            /// <summary>
            /// Creadora
            /// </summary>
            public PermisosCobroDalc()
            {
                objConfiguracion = new Configuracion();
            }

        #endregion

        #region Creadoras
        

            /// <summary>
            /// Consultar el listado de permisos relacionados a un cobro
            /// </summary>
            /// <param name="idCobro">int con el identificador del cobro</param>
            /// <returns>List con la informacion de los permisos</returns>
            public List<PermisoCobroIdentity> ObtenerPermisosCobro(long idCobro)
            {
                List<PermisoCobroIdentity> objLstPermisos = null;
                PermisoCobroIdentity objPermiso = null;

                try
                {
                    SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                    object[] parametros = new object[] { idCobro };
                    DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_PERMISO_COBRO", parametros);
                    DataSet dsResultado = db.ExecuteDataSet(cmd);

                    if (dsResultado != null && dsResultado.Tables[0].Rows.Count > 0)
                    {
                        //Crear listado
                        objLstPermisos = new List<PermisoCobroIdentity>();

                        //Ciclo que carga los permisos
                        foreach (DataRow objPermisoFila in dsResultado.Tables[0].Rows)
                        {
                            objPermiso = new PermisoCobroIdentity();
                            objPermiso.PermisoCobroID = Convert.ToInt64(objPermisoFila["PCO_ID"]);
                            objPermiso.CobroID = Convert.ToInt64(objPermisoFila["COB_ID"]);
                            objPermiso.Permiso = objPermisoFila["PERMISO"].ToString();
                            objPermiso.Autoridad = objPermisoFila["AUTORIDAD"].ToString();
                            objPermiso.NumeroPermisos = Convert.ToInt32(objPermisoFila["NUMERO_PERMISOS"]);
                            objPermiso.ValorAdministracion = Convert.ToDecimal(objPermisoFila["VALOR_ADMINISTRACION"]);
                            objPermiso.ValorServicio = Convert.ToDecimal(objPermisoFila["VALOR_SERVICIO"]);
                            objPermiso.ValorTotal = Convert.ToDecimal(objPermisoFila["VALOR_TOTAL"]);

                            objLstPermisos.Add(objPermiso);
                        }
                        
                    }
               
                }
                catch (SqlException sqle)
                {
                    throw new Exception(sqle.Message);
                }

                return objLstPermisos;
            }


            /// <summary>
            /// Inserta un nuevo permiso en la base de datos
            /// </summary>
            /// <param name="p_objPermiso">PermisoCobroIdentity con la informacion del permiso</param>
            public void InsertarPermiso(PermisoCobroIdentity p_objPermiso)
            {
                try
                {
                    SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                    //Se agrega el cobro
                    object[] parametros = new object[] { 
                        p_objPermiso.CobroID,
                        p_objPermiso.Permiso,
                        p_objPermiso.Autoridad,
                        p_objPermiso.NumeroPermisos,
                        p_objPermiso.ValorAdministracion,
                        p_objPermiso.ValorServicio,
                        p_objPermiso.ValorTotal
                    };

                    DbCommand cmd = db.GetStoredProcCommand("GEN_INSERT_PERMISO_COBRO", parametros);
                    db.ExecuteDataSet(cmd);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

        #endregion
    }
}
