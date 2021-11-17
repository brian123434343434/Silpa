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
using System;

namespace SILPA.AccesoDatos.Generico
{
    public class AnexarDocumentacionSoporteDalc
    {
        /// <summary>
        /// Objeto de configuracion - contien las varables el Web.config
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor
        /// </summary>
        public AnexarDocumentacionSoporteDalc()
        {
            objConfiguracion = new Configuracion();
        }

        
        /// <summary>
        /// Método que permite guardar el detalle de los documentos anexos.
        /// </summary>
        /// <param name="intIdAA"></param>
        /// <param name="strNumeroDocumento"></param>
        /// <returns></returns>
        /// 

        /*
         * [ID_RADICACION] [bigint] NOT NULL,
	[ID_FORMULARIO] [int] NULL,
	[PATH_DOCUMENTO] [varchar](2000) COLLATE Modern_Spanish_CI_AS NOT NULL,
	[INFO_DETALLE_RADICACION]
         */
        public void ObtenerDatosDocumento(AnexarDocumentacionSoporteIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] 
                {objIdentity.InfoAdicionalDocumento 
                };

                DbCommand cmd = db.GetStoredProcCommand("GEH_GENERAR_DETALLE_RADICACION_DOCUMENTO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);


                /*
                 *  [dbo].[GEH_GENERAR_DETALLE_RADICACION_DOCUMENTO]
(
		@ID_RADICACION INT,			--- El identificador de la radicacion
		@ID_FORMULARIO INT = NULL,  --- el identificador del formulario asociado (seleccionado de lista)
		@PATH_DOCUMENTO VARCHAR(2000) = NULL, --- nombde del documento adjunto
		@INFO_DETALLE_RADICACION VARCHAR(2000) = NULL --
                 */


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
