using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using SILPA.Comun;

namespace SILPA.LogicaNegocio.Generico
{
 public  class EmitirDocumento
    {

        public EmitirDocumento()
        { 

        }

        /// <summary>
        /// 12-jul-2010 - aegb: cu emitir documento
        /// Inserta un registro de la tabla emitir documento  
        /// </summary>
        public long InsertarDocumento(EmitirDocumentoIdentity objIdentity)
        {
            EmitirDocumentoDalc dalc = new EmitirDocumentoDalc();
           return dalc.InsertarDocumento(objIdentity);
        }

        /// <summary>
        /// 12-jul-2010 - aegb: cu emitir documento
        /// Lista la informacion del acto administrativo a notificar
        /// </summary>
        /// <param name="objIdentity">Objeto documento</param>
        /// <returns></returns>
        public void ConsultarDocumento(ref EmitirDocumentoIdentity objIdentity)
        {
            EmitirDocumentoDalc dalc = new EmitirDocumentoDalc();
            dalc.ConsultarDocumento(ref objIdentity);
        }

        /// <summary>
        /// 12-jul-2010 - aegb: cu emitir documento
        /// Lista la informacion del acto administrativo a notificar
        /// </summary>
        /// <param name="objIdentity">Objeto documento</param>
        /// <returns></returns>
     public DataTable ConsultarDocumento(long personaId, string numeroSilpa, string numeroExpediente, int tipoActo, string numeroActo, string fechaDesde, string fechaHasta)
        {
            EmitirDocumentoDalc dalc = new EmitirDocumentoDalc();
            return dalc.ConsultarDocumento(personaId, numeroSilpa, numeroExpediente, tipoActo, numeroActo, fechaDesde, fechaHasta);
        }

        /// <summary>
        /// 12-jul-2010 - aegb: cu emitir documento
        /// Inserta un registro de la tabla emitir documento detalle
        /// </summary>
        public void InsertarDocumentoDetalle(long documentoId, string nombre)
        {
            EmitirDocumentoDalc dalc = new EmitirDocumentoDalc();
            dalc.InsertarDocumentoDetalle(documentoId, nombre);

        }

        /// <summary>
        /// 12-jul-2010 - aegb: cu emitir documento
        /// Lista la informacion de detalle del acto administrativo a notificar
        /// </summary>
        /// <param name="documentoId">ID del documento</param>
        /// <returns></returns>
        public DataTable ConsultarDocumentoDetalle(long documentoId)
        {
            EmitirDocumentoDalc dalc = new EmitirDocumentoDalc();
            return dalc.ConsultarDocumentoDetalle(documentoId);
        }

        /// <summary>
        /// 12-jul-2010 - aegb: cu emitir documento
        /// Inserta un registro de la tabla emitir documento persona
        /// </summary>
        public void InsertarDocumentoPersona(long documentoId, long personaId)
        {

            EmitirDocumentoDalc dalc = new EmitirDocumentoDalc();
            dalc.InsertarDocumentoPersona(documentoId, personaId);

        }

        /// <summary>
        /// 12-jul-2010 - aegb: cu emitir documento
        /// Lista la informacion del personas a notificar el acto administrativo
        /// </summary>
        /// <param name="documentoId">ID del documento</param>
        /// <returns></returns>
        public DataTable ConsultarDocumentoPersonas(long documentoId)
        {
            EmitirDocumentoDalc dalc = new EmitirDocumentoDalc();
            return dalc.ConsultarDocumentoPersonas(documentoId);
        }

    }
}
