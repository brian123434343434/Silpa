using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;

namespace SILPA.AccesoDatos.Generico
{
    public class TipoDocumentoDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public TipoDocumentoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public TipoDocumentoIdentity ObtenerTipoDocumento(int? intTipoDocumento, string strNombreDocumento)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { intTipoDocumento, strNombreDocumento,1 };

                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTAR_TIPO_DOCUMENTO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                TipoDocumentoIdentity _objTipoDocumento = new TipoDocumentoIdentity();

                if (dsResultado == null || dsResultado.Tables[0].Rows.Count == 0)
                {
                    parametros = new object[] { intTipoDocumento, strNombreDocumento, 0};
                    cmd = db.GetStoredProcCommand("BAS_LISTAR_TIPO_DOCUMENTO", parametros);
                    dsResultado = db.ExecuteDataSet(cmd);
                }
            
                if (dsResultado != null && dsResultado.Tables[0].Rows.Count > 0)
                {
                    _objTipoDocumento.ID = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ID"]);
                    _objTipoDocumento.Nombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["NOMBRE_DOCUMENTO"]);
                    _objTipoDocumento.HabilitadoReposicion = Convert.ToBoolean( dsResultado.Tables[0].Rows[0]["HABILITADO_REPOSICION"]);
                    if (dsResultado.Tables[0].Rows[0]["ID_FLUJO_NOT_ELEC"].ToString() == "")
                        _objTipoDocumento.IdFlujoNotElec = null;
                    else
                        _objTipoDocumento.IdFlujoNotElec = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ID_FLUJO_NOT_ELEC"].ToString());
                    if (dsResultado.Tables[0].Rows[0]["ID_BPM_PARAMETRO"] != DBNull.Value)
                    {
                        _objTipoDocumento.ParametroBPM = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ID_BPM_PARAMETRO"]);
                    }
                    else
                    {
                        _objTipoDocumento.ParametroBPM = 0;
                    }
                }
                return _objTipoDocumento;
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al obtener Tipo de Documento.";
                throw new Exception(strException, ex);
            }
        }

        /// <summary>
        /// Lista los tipos de documento disponibles
        /// </summary>
        /// <param name="intTipoDocumento">n/a</param>
        /// <param name="strNombreDocumento">n/a</param>
        /// <returns>DataSet con los tipos de documento</returns>
        public DataSet ListarTiposDeDocumento(int? intTipoDocumento, string strNombreDocumento)
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intTipoDocumento, strNombreDocumento,1 };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTAR_TIPO_DOCUMENTO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            parametros = new object[] { intTipoDocumento, strNombreDocumento, 0 };
            cmd = db.GetStoredProcCommand("BAS_LISTAR_TIPO_DOCUMENTO", parametros);
            
            DataSet dsResultado2 = db.ExecuteDataSet(cmd);

            foreach (DataRow dt in dsResultado2.Tables[0].Rows)
            {
                DataRow dr = dsResultado.Tables[0].NewRow();
                dr["ID"] = dt["ID"];
                dr["NOMBRE_DOCUMENTO"] = dt["NOMBRE_DOCUMENTO"];
                dr["ID_BPM_PARAMETRO"] = dt["ID_BPM_PARAMETRO"];
                dr["HABILITADO_REPOSICION"] = dt["HABILITADO_REPOSICION"];
                dr["CODIGO_CONDICION_BPM"] = dt["CODIGO_CONDICION_BPM"];
                dsResultado.Tables[0].Rows.Add(dr);
            }
            return dsResultado;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="intTipoDocumento">int: identificador del tipo de documento gen_tipo_documento: opcional</param>
        /// <param name="strNombreDocumento">string: nombre del tipo de documento: opcional</param>
        /// <returns>DataSet: listadod de Documentos para notificación</returns>
        public DataSet ListarTiposDeDocumentoNotificacion(int? intTipoDocumento, string strNombreDocumento) 
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intTipoDocumento, strNombreDocumento};
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTAR_GEN_TIPO_DOCUMENTO_NOTIFICACION", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return dsResultado;
        }


        /// <summary>
        /// Lista los tipos de documento disponibles
        /// </summary>
        /// <param name="intTipoDocumento">n/a</param>
        /// <param name="strNombreDocumento">n/a</param>
        /// <returns>Lista con los tipos de documento</returns>
        public List<TipoDocumentoIdentity> ListarTiposDeDocumento()
        {

            List<TipoDocumentoIdentity> lista = listarTiposDocumento(1);
            List<TipoDocumentoIdentity> lista2 = listarTiposDocumento(0);
            foreach(TipoDocumentoIdentity tipoDoc in lista2)
            {
                lista.Add(tipoDoc);
            }
            return lista;
        }

        public List<TipoDocumentoIdentity> listarTiposDocumento(int habilitado)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { null, null, habilitado };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTAR_TIPO_DOCUMENTO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            List<TipoDocumentoIdentity> listaDocumentos = new List<TipoDocumentoIdentity>();
            TipoDocumentoIdentity tipoDocumento;
            foreach (DataRow dr in dsResultado.Tables[0].Rows)
            {
                tipoDocumento = new TipoDocumentoIdentity();
                tipoDocumento.ID = Convert.ToInt32(dr["ID"]);
                tipoDocumento.Nombre = Convert.ToString(dr["NOMBRE_DOCUMENTO"]);
                if (dr["ID_BPM_PARAMETRO"] != DBNull.Value)
                    tipoDocumento.ParametroBPM = Convert.ToInt32(dr["ID_BPM_PARAMETRO"]);
                tipoDocumento.HabilitadoReposicion = Convert.ToBoolean(dr["HABILITADO_REPOSICION"]);
                listaDocumentos.Add(tipoDocumento);
            }
            return listaDocumentos;
        }
        /// <summary>
        /// Inserta un Tipo de Documento
        /// </summary>
        /// <param name="tipoDocumento">Objeto con el Tipo de Documento</param>
        public void InsertarTipoDocumento(TipoDocumentoIdentity tipoDocumento)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { tipoDocumento.Nombre, tipoDocumento.ParametroBPM, tipoDocumento.HabilitadoReposicion };
            DbCommand cmd = db.GetStoredProcCommand("BAS_INSERTAR_TIPO_DOCUMENTO", parametros);
            try
            {
                db.ExecuteDataSet(cmd);
                //return Int32.Parse(cmd.Parameters["ID"].Value.ToString());
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }

        /// <summary>
        /// Modifica un tipo de Documento
        /// </summary>
        /// <param name="tipoDocumento">Objeto con el Tipo de documento</param>
        /// <returns>Resultado de la Transacción</returns>
        public bool ModificarTipoDocumento(TipoDocumentoIdentity tipoDocumento)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { tipoDocumento.ID, tipoDocumento.Nombre, tipoDocumento.ParametroBPM,tipoDocumento.HabilitadoReposicion };
            DbCommand cmd = db.GetStoredProcCommand("BAS_MODIFICAR_TIPO_DOCUMENTO", parametros);
            try
            {
                db.ExecuteNonQuery(cmd);
                return true;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
            
        }

        //BAS_ELIMINAR_TIPO_DOCUMENTO

        /// <summary>
        /// Elimina un tipo de documento
        /// </summary>
        /// <param name="tipoDocumento"></param>
        /// <returns></returns>
        public bool EliminarTipoDocumento(TipoDocumentoIdentity tipoDocumento)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { tipoDocumento.ID};
            DbCommand cmd = db.GetStoredProcCommand("BAS_ELIMINAR_TIPO_DOCUMENTO", parametros);
            try
            {
                db.ExecuteNonQuery(cmd);
                return true;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }

        }

        /// <summary>
        /// Lista los tipos de documento disponibles
        /// </summary>
        /// <param name="intTipoDocumento">n/a</param>
        /// <param name="strNombreDocumento">n/a</param>
        /// <returns>DataSet con los tipos de documento</returns>
        public DataSet ListarTiposDeDocumentoActo(int? intTipoDocumento, string strNombreDocumento)
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intTipoDocumento, strNombreDocumento};
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTAR_TIPO_DOCUMENTO_ACTO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            return dsResultado;
        }



        /// <summary>
        /// Obtiene la condicion asociada a un documento
        /// </summary>
        /// <param name="intTipoDocumento"></param>
        /// <returns></returns>
        public string ObtenerCondicionTipoDocumento(int intTipoDocumento)
        {
            try
            {
	            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
	            object[] parametros = new object[] { intTipoDocumento, 0 };
	            DbCommand cmd = db.GetStoredProcCommand("BAS_CONDICION_TIPO_DOCUMENTO", parametros);
	            db.ExecuteNonQuery(cmd);
	            return db.GetParameterValue(cmd, "Condicion").ToString();
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al obtener la condición asociada a un documento.";
                throw new Exception(strException, ex);
            }
        }


        /// <summary>
        /// Obtiene el indicador de si el documento es por causa de una comunicación a o desde una EE
        /// </summary>
        /// <param name="intTipoDocumento">int: código del tipo de documento </param>
        /// <returns>bool: True si es por causa de una comunicacion EE - false en caso contrario</returns>
        public bool ObtenerDocumentoEE(int intTipoDocumento)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intTipoDocumento, 0 };
            DbCommand cmd = db.GetStoredProcCommand("BAS_TIPO_DOCUMENTO_EE", parametros);
            db.ExecuteNonQuery(cmd);
            
            //int result = (int)(db.GetParameterValue(cmd, "ComunicacionEE"));
            bool result = (bool)db.GetParameterValue(cmd, "ComunicacionEE");

            return Convert.ToBoolean(result);
        }


    }
}
