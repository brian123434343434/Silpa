using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.Generico;
using System.Data;
using SoftManagement.Log;

namespace SILPA.LogicaNegocio.Generico
{
    
    public class Departamento
    {

        #region  Objetos

            private DepartamentoDalc _objDepartamentoDalc;

        #endregion
        

        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public Departamento()
            {
                //Creary cargar configuración
                this._objDepartamentoDalc = new DepartamentoDalc();
            }

        #endregion


        #region  Metodos Publicos

            /// <summary>
            /// Lista los departamentos en la BD. Pueden listarse los departamentos por region o uno en particular.
            /// </summary>
            /// <param name="intRegion" >Valor del identificador de la region por el cual se filtraran los departamentos, si es null no existen restricciones</param>
            /// <param name="intIdentity.Id" >Con este valor se lista el departamento con el identificador, , si es null no existen restricciones</param>
            /// <returns>DataSet con los registros y las siguientes columnas: DEP_ID, DEP_NOMBRE, REG_ID, REG_NOMBRE</returns>
            public List<DepartamentoIdentity> ConsultarDepartamentos(Nullable<int> intId, Nullable<int> intRegion)
            {
                DataSet objDatosDepartamentos = null;
                List<DepartamentoIdentity> objLstDepartamentos = null;

                try
                {
                    //Consultar departamentos
                    objDatosDepartamentos = this._objDepartamentoDalc.ListarDepartamentos(intId, intRegion);

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
                catch(Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "Departamento :: ConsultarDepartamentos -> Error bd: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return objLstDepartamentos;
            }


            /// <summary>
            /// Obtener el listado de departamentos (Autoliquidación)
            /// </summary>
            /// <returns>List con la información de los departamentos</returns>
            public List<DepartamentoIdentity> ListarDepartamentosLiquidacion()
            {
                return this._objDepartamentoDalc.ListarDepartamentosLiquidacion();
            }


            /// <summary>
            /// Obtener el listado de departamentos asociados a un medio de transporte (Autoliquidación)
            /// </summary>
            /// <param name="p_intMedioTransporteID">int con el identificador del medio de transporte</param>
            /// <returns>List con la información de los departamentos</returns>
            public List<DepartamentoIdentity> ListarDepartamentosMedioTransporteLiquidacion(int p_intMedioTransporteID)
            {
                return this._objDepartamentoDalc.ListarDepartamentosMedioTransporteLiquidacion(p_intMedioTransporteID);
            }


            /// <summary>
            /// Obtener el listado de departamentos de destino asociados a una atoridad ambiental y medio de transporte (Autoliquidación)
            /// </summary>
            /// <param name="p_intMunipioOrigenID">int con el municipio de origen</param>
            /// <param name="p_intMedioTransporteID">int con el identificador del medio de transporte</param>
            /// <returns>List con la información de los departamentos</returns>
            public List<DepartamentoIdentity> ListarDepartamentosDestinoMedioTransporteLiquidacion(int p_intMunipioOrigenID, int p_intMedioTransporteID)
            {
                return this._objDepartamentoDalc.ListarDepartamentosDestinoMedioTransporteLiquidacion(p_intMunipioOrigenID, p_intMedioTransporteID);
            }


        #endregion

    }
}
