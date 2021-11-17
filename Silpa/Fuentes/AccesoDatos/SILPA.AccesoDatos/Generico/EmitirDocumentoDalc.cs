using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
   public class EmitirDocumentoDalc
    {
       private Configuracion objConfiguracion;

        public EmitirDocumentoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// 12-jul-2010 - aegb: cu emitir documento
        /// Inserta un registro de la tabla emitir documento  
        /// </summary>
       public long InsertarDocumento(EmitirDocumentoIdentity objIdentity)
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

            object[] parametros = new object[] { 
                                                  objIdentity.DocumentoId,
                                                  objIdentity.AutoridadId,
                                                  objIdentity.NumeroSilpa,
                                                  objIdentity.NumeroExpediente,
                                                  objIdentity.TipoDocumentoId,
                                                  objIdentity.ActoAdministrativo,
                                                  objIdentity.FechaActoAdministrativo,  
                                                  objIdentity.DescripcionActoAdministrativo,   
                                                  objIdentity.PersonaId
                                                     
                };

            DbCommand cmd = db.GetStoredProcCommand("GEN_CREAR_EMITIR_DOCUMENTO", parametros);
            db.ExecuteDataSet(cmd);
           return Int64.Parse(cmd.Parameters["@P_ID"].Value.ToString());

        }             
  
        /// <summary>
        /// 12-jul-2010 - aegb: cu emitir documento
        /// Lista la informacion del acto administrativo a notificar
        /// </summary>
        /// <param name="objIdentity">Objeto documento</param>
        /// <returns></returns>
        public void ConsultarDocumento(ref EmitirDocumentoIdentity objIdentity)
        {
            //  GenericDalc.cm
            objConfiguracion = new Configuracion();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { objIdentity.DocumentoId
                                                , objIdentity.PersonaId 
                                                };

            DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_EMITIR_DOCUMENTO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            objIdentity.ActoAdministrativo = dsResultado.Tables[0].Rows[0]["EMD_NUMERO_ACTO"].ToString();
            objIdentity.AutoridadId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["EMD_AUT_ID"]);
            objIdentity.DescripcionActoAdministrativo = dsResultado.Tables[0].Rows[0]["EMD_DESCRIPCION_ACTO"].ToString();
            objIdentity.DocumentoId = Convert.ToInt64(dsResultado.Tables[0].Rows[0]["EMD_ID"]);
            objIdentity.FechaActoAdministrativo = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["EMD_FECHA_ACTO"]);
            objIdentity.NumeroExpediente = dsResultado.Tables[0].Rows[0]["EMD_NUMERO_EXPEDIENTE"].ToString();
            objIdentity.NumeroSilpa = dsResultado.Tables[0].Rows[0]["EMD_NUMERO_SILPA"].ToString();
            objIdentity.PersonaId = Convert.ToInt64(dsResultado.Tables[0].Rows[0]["EMD_PER_ID"]);
            //objIdentity.RadicacionId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["EMD_RADICACION_ID"]);
            objIdentity.TipoDocumentoId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["EMD_TIPO_DOCUMENTO_ID"]);         
        }

        /// <summary>
        /// 12-jul-2010 - aegb: cu emitir documento
        /// Lista la informacion del acto administrativo a notificar
        /// </summary>
        /// <param name="objIdentity">Objeto documento</param>
        /// <returns></returns>
       public DataTable ConsultarDocumento(long personaId, string numeroSilpa, string numeroExpediente, int tipoActo, string numeroActo, string fechaDesde, string fechaHasta)
        {
            //  GenericDalc.cm
            objConfiguracion = new Configuracion();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] {personaId, numeroSilpa, numeroExpediente, tipoActo, numeroActo, fechaDesde, fechaHasta
                                                };

            DbCommand cmd = db.GetStoredProcCommand("GEN_CONSULTA_EMITIR_DOCUMENTO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            return dsResultado.Tables[0];
        }

        /// <summary>
        /// 12-jul-2010 - aegb: cu emitir documento
        /// Inserta un registro de la tabla emitir documento detalle
        /// </summary>
       public void InsertarDocumentoDetalle(long documentoId, string nombre)
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

            object[] parametros = new object[] { documentoId, nombre    };

            DbCommand cmd = db.GetStoredProcCommand("GEN_CREAR_EMITIR_DOCUMENTO_DETALLE", parametros);
            db.ExecuteDataSet(cmd);
        
        }

        /// <summary>
        /// 12-jul-2010 - aegb: cu emitir documento
        /// Lista la informacion de detalle del acto administrativo a notificar
        /// </summary>
        /// <param name="documentoId">ID del documento</param>
        /// <returns></returns>
        public DataTable ConsultarDocumentoDetalle(long documentoId)
        {
            //  GenericDalc.cm
            objConfiguracion = new Configuracion();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { documentoId };

            DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_EMITIR_DOCUMENTO_DETALLE", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            return dsResultado.Tables[0];
          
        }

        /// <summary>
        /// 12-jul-2010 - aegb: cu emitir documento
        /// Inserta un registro de la tabla emitir documento persona
        /// </summary>
       public void InsertarDocumentoPersona(long documentoId, long personaId)
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

            object[] parametros = new object[] { documentoId, personaId };

            DbCommand cmd = db.GetStoredProcCommand("GEN_CREAR_EMITIR_DOCUMENTO_PERSONA", parametros);
            db.ExecuteDataSet(cmd);

        }

        /// <summary>
        /// 12-jul-2010 - aegb: cu emitir documento
        /// Lista la informacion del personas a notificar el acto admisistrativo
        /// </summary>
        /// <param name="documentoId">ID del documento</param>
        /// <returns></returns>
        public DataTable ConsultarDocumentoPersonas(long documentoId)
        {
            //  GenericDalc.cm
            objConfiguracion = new Configuracion();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { documentoId };

            DbCommand cmd = db.GetStoredProcCommand("GEN_LISTA_EMITIR_DOCUMENTO_PERSONAS", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            return dsResultado.Tables[0];
        }

    
    }
}
