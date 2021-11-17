using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SoftManagement.Log;
using System.Data.SqlClient;
using SILPA.Comun;
using System.Data.Common;
using System.Data;

namespace SILPA.AccesoDatos.BPMProcess
{
    public class ProcessDalc
    {
        private Configuracion objConfiguracion;

        public ProcessDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public void CrearProceso(ref ProcessIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.IdForm, objIdentity.IdUser, objIdentity.Cadena,"",0 };
                DbCommand cmd = db.GetStoredProcCommand("BPM_CREAR_PROCESO", parametros);
                db.ExecuteNonQuery(cmd);
                objIdentity.Resp = cmd.Parameters["@RESP"].Value.ToString();                
                objIdentity.IdFormInstance = Int64.Parse(cmd.Parameters["@P_FORMINSTANCE"].Value.ToString());

            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, sqle.ToString());
                throw new Exception(sqle.Message);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Crear Proceso.";
                throw new Exception(strException, ex);
            }
        }

        public string GetNumeroSilpa(Int64 processinstance)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { processinstance, 30 };
                DbCommand cmd = db.GetStoredProcCommand("BPM_CONSULTAR_NUMERO_SILPA", parametros);
                db.ExecuteNonQuery(cmd);
                return cmd.Parameters["@P_NUMEROSILPA"].Value.ToString();                

            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, sqle.ToString());
                throw new Exception(sqle.Message);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al consultar el Número VITAL.";
                throw new Exception(strException, ex);
            }
        }

        public Int64 GetProcessCase(Int64 FormId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { FormId, 30 };
                DbCommand cmd = db.GetStoredProcCommand("BPM_CONSULTAR_PROCESS_CASE", parametros);
                db.ExecuteNonQuery(cmd);
                return Int64.Parse(cmd.Parameters["@P_IDPROCESSCASE"].Value.ToString());

            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, sqle.ToString());
                throw new Exception(sqle.Message);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al consultar el número VITAL después de ejecutar STARPROCESINSTANCE.";
                throw new Exception(strException, ex);
            }
        }


        public List<FormularioIdentity> ObtenerFormularios()
        {
            try
            {
                FormularioIdentity objIdentity = new FormularioIdentity();
                List<FormularioIdentity> _listaFormularios = new List<FormularioIdentity>();

                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                DbCommand cmd = db.GetStoredProcCommand("BPM_OBTENER_LISTADO_FORMS");
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                foreach (DataRow drResultado in dsResultado.Tables[0].Rows)
                {
                    objIdentity = new FormularioIdentity();
                    objIdentity.Id = Convert.ToInt32(drResultado["ID"]);
                    objIdentity.Nombre = Convert.ToString(drResultado["NAME"]);                    

                    _listaFormularios.Add(objIdentity);
                }

                return _listaFormularios;

            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, sqle.ToString());
                throw new Exception(sqle.Message);
            }
        }


        public List<CampoIdentity> ObtenerCampos(Int64 idForm)
        {
            try
            {
                CampoIdentity objIdentity = new CampoIdentity();
                List<CampoIdentity> _listaCampos = new List<CampoIdentity>();
                object[] parametros = new object[] { idForm};
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                DbCommand cmd = db.GetStoredProcCommand("BPM_OBTENER_LISTADO_FIELDS",parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                foreach (DataRow drResultado in dsResultado.Tables[0].Rows)
                {
                    objIdentity = new CampoIdentity();
                    objIdentity.NumRow = Convert.ToInt32(drResultado["NUM_ROW"]);
                    objIdentity.Id = Convert.ToInt32(drResultado["ID"]);
                    objIdentity.Nombre = Convert.ToString(drResultado["NOMBRE"]);
                    objIdentity.TipoDato = Convert.ToString(drResultado["TIPO_DATO"]);
                    objIdentity.Requerido = Convert.ToBoolean(drResultado["REQUERIDO"]);
                    objIdentity.Tipo = Convert.ToString(drResultado["TIPO"]);

                    _listaCampos.Add(objIdentity);
                }

                return _listaCampos;

            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, sqle.ToString());
                throw new Exception(sqle.Message);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Obtener Campos.";
                throw new Exception(strException, ex);
            }
        }


        public DataSet ObtenerParametrosPorTramite(Int64 tramiteId, Int64 perId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { tramiteId,perId };
                DbCommand cmd = db.GetStoredProcCommand("BPM_CONSULTAR_PARAMETROS_TRAMITE", parametros);
                DataSet ds_usuario = new DataSet();
                ds_usuario = db.ExecuteDataSet(cmd);
                return ds_usuario;
            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, sqle.Message.ToString());
                return null;
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Obtener Parámetros por Trámite.";
                throw new Exception(strException, ex);
            }
        }

        public DataSet ConsultarAADepto(Int64 autId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { autId };
                DbCommand cmd = db.GetStoredProcCommand("BPM_CONSULTA_DEPARTAMENTO_JURISDICCION", parametros);
                DataSet ds_usuario = new DataSet();
                ds_usuario = db.ExecuteDataSet(cmd);
                return ds_usuario;
            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, sqle.Message.ToString());
                return null;
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Consultar Departamento.";
                throw new Exception(strException, ex);
            }
        }


        public void ActualizarAAProcesoSolicitudComEE(long ProcessInstance, int autId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { ProcessInstance, autId };
                DbCommand cmd = db.GetStoredProcCommand("GEN_ACTUALIZAR_SOLICITUD_COMUNICACION_EE", parametros);
                int i = db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, sqle.Message.ToString());
            }            
        }

        /// <summary>
        /// guarda la relación entre dos procesos padre hijo (los numero silpa)
        /// ejemplo el caso de información adicional encualquier momento
        /// </summary>
        /// <param name="padre">string: número silpa del trámite desde donde se origina el segundo proceso</param>
        /// <param name="hijo">string: número silpa del trámite hijo originado</param>
        public void CrearRelacionSilpaPadreHijo(string padre, string hijo)
        {
            try
            {
	            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
	            object[] parametros = new object[] { padre, hijo };
	            DbCommand cmd = db.GetStoredProcCommand("GEN_RELACIONAR_PADRE_HIJO_INFO_ADICIONAL", parametros);
	            int i = db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, sqle.Message.ToString());
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al guardar la relación entre dos procesos padre hijo (los números VITAL).";
                throw new Exception(strException, ex);
            }
        }
    }
}
