using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.Generico;
using System.Data;

namespace SILPA.LogicaNegocio.Generico {
    class PerfilDocumento {
        public PerfilDocumento( ){}

        public DataTable getPerfilesDocumentos( SILPA.AccesoDatos.Generico.PerfilDocumentoIdentity filtro ){
            PerfilDocumento pDocumentos = new PerfilDocumento( );
            return pDocumentos.getPerfilesDocumentos( filtro );
        }

        public void guardarPerfilDocumento( PerfilDocumentoIdentity pDocumento ) {
            PerfilDocumento pDocumentos = new PerfilDocumento( );
            pDocumentos.guardarPerfilDocumento( pDocumento );
        }
    }
}
