using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccessoDatos;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.Parametrizacion;

namespace SILPA.LogicaNegocio.Parametrizacion
{
    public class ConsultaDocumentos
    {
        private ConsultaDocumentoDALC objConsultaDocDALC;
        public ConsultaDocumentos()
        {
            objConsultaDocDALC = new ConsultaDocumentoDALC();
        }

        public DataTable Listar_DocumentosEntidades()
        {
            return objConsultaDocDALC.ListarEntidadesExternas();
        }

        public DataTable Listar_Documentos( Nullable<long> intIdUsuario, Nullable<int > intEexId, string strCodigoSilpa,
            Nullable<System.DateTime> dateFechaInicial, Nullable<System.DateTime> dateFechaFinal)
        {
            return objConsultaDocDALC.Listar_Documentos(intIdUsuario, intEexId, strCodigoSilpa, dateFechaInicial, dateFechaFinal);

        }
           
    }
}
