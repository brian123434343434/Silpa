using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.Encuestas.Encuesta.Entity;
using SoftManagement.Log;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace SILPA.AccesoDatos.Encuestas.Encuesta.Dalc
{
    public class SectorEncuestasDalc
    {
        #region  Objetos

            private Configuracion _objConfiguracion;

        #endregion


        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public SectorEncuestasDalc()
            {
                //Creary cargar configuración
                this._objConfiguracion = new Configuracion();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Consultar el listado de sectores que se puede asociar a una solicitud de cambio menor
            /// </summary>
            /// <param name="p_blnActivo">bool que indica si se extrae los sectores activas o inactivas. Opcional </param>
            /// <returns>List con la información de los sectores</returns>
            public List<SectorEncuestasEntity> ConsultarSectores(bool? p_blnActivo = null)
            {
                SqlDatabase objDataBase = null;
                DbCommand objCommand = null;
                DataSet objInformacion = null;
                List<SectorEncuestasEntity> objLstSectores = null;
                SectorEncuestasEntity objSector = null;

                try
                {
                    //Cargar conexion
                    objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                    //Cargar comando
                    objCommand = objDataBase.GetStoredProcCommand("ENC_LISTA_SECTOR");
                    if (p_blnActivo != null)
                        objDataBase.AddInParameter(objCommand, "@P_ACTIVO", DbType.Boolean, p_blnActivo.Value);

                    //Consultar
                    objInformacion = objDataBase.ExecuteDataSet(objCommand);
                    if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                    {
                        //Crear el listado
                        objLstSectores = new List<SectorEncuestasEntity>();

                        //Ciclo que carga la información
                        foreach (DataRow objSectorInformacion in objInformacion.Tables[0].Rows)
                        {
                            //Crear objeto y cargar datos
                            objSector = new SectorEncuestasEntity
                            {
                                SectorID = Convert.ToInt32(objSectorInformacion["ENCSECTOR_ID"]),
                                Sector = objSectorInformacion["DESCRIPCION"].ToString(),
                                SectorSilaID = Convert.ToInt32(objSectorInformacion["SEC_ID"]),
                                Activo = Convert.ToBoolean(objSectorInformacion["ACTIVO"])
                            };

                            //Adiciona al listado
                            objLstSectores.Add(objSector);
                        }
                    }
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "SectorEncuestasDalc :: ConsultarSectores -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "SectorCambioMenorDalc :: ConsultarSectores -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objLstSectores;
            }

        #endregion

    }
}
