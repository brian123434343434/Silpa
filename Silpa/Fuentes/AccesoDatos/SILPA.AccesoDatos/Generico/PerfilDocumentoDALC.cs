using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;

namespace SILPA.AccesoDatos.Generico {
    class PerfilDocumentoDALC {

        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;
        
        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public PerfilDocumentoDALC( ){
            objConfiguracion = new Configuracion();
        }
        
        public DataTable getPerfilesDocumentos( PerfilDocumentoIdentity filtro ){

            SqlDatabase db = new SqlDatabase( objConfiguracion.SilpaCnx.ToString( ) );
            object[] parametros = new object[] { filtro.TipoPerfil, filtro.IdTipo, ( filtro.RequireLogin != null ? ( filtro.RequireLogin == true ? "1" : "0" ) : null ), filtro.Descripcion, ( filtro.Estado != null ? ( filtro.Estado == true ? "1" : "0" ) : null ) };
            DbCommand cmd = db.GetStoredProcCommand( "SP_CONSULTAR_PERFILDOCUMENTO", parametros );
            DataSet dsResultado = db.ExecuteDataSet( cmd );

            if( dsResultado != null && dsResultado.Tables.Count > 0 )
                return dsResultado.Tables[ 0 ];

            return null;
        }

        public void guardarPerfilDocumento( PerfilDocumentoIdentity pDocumento ) {
            SqlDatabase db = new SqlDatabase( objConfiguracion.SilpaCnx.ToString( ) );
            object[] parametros = new object[] { pDocumento.TipoPerfil, pDocumento.IdTipo, ( pDocumento.RequireLogin != null ? ( pDocumento.RequireLogin == true ? "1" : "0" ) : null ), pDocumento.Descripcion, ( pDocumento.Estado != null ? ( pDocumento.Estado == true ? "1" : "0" ) : null ) };
            DbCommand cmd = db.GetStoredProcCommand( "SP_GUARDAR_PERFILDOCUMENTO", parametros );
            db.ExecuteDataSet( cmd );
        }
    }
}