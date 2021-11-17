using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.AccesoDatos.Parametrizacion;  

namespace SILPA.LogicaNegocio.Parametrizacion
{
   public class ParametrizacionDocumentos
    {



        /// <summary>
        /// Seleccionar los tipos de adquisición de documentos 
        /// </summary>
        /// <returns></returns>
  
        public DataTable ListarTipoAdquisicion()
        {
            ParametrizacionDocumentoDalc docParam = new ParametrizacionDocumentoDalc();
            return docParam.ListarTipoAdquisicion();

        }



        /// <summary>
        /// Seleccionar las entidades externas
        /// </summary>
        /// <returns></returns>
        public DataTable ListarEntidadesExternas()
        {
            ParametrizacionDocumentoDalc entidadExt = new ParametrizacionDocumentoDalc();
            return entidadExt.ListarEntidadesExternas();

        }

        /// metodo que permite agregra un  registro a la base de datos
        /// con la informacion nueva de la parametrización de un documento
       public Boolean crearParametrizacionDocumento(string strDocNombre, string strEnlaceAplicativo, int intCodAquisicion, int intEntidaExterna, string strCodigoProceso)
        {
            ParametrizacionDocumentoDalc paramDocumento = new ParametrizacionDocumentoDalc();
            return paramDocumento.agregarDocumentos(strDocNombre, strEnlaceAplicativo, intCodAquisicion, intEntidaExterna, strCodigoProceso);

        }


       public DataTable ListarParametrizacionDocumento(Nullable<int> intAdquisicion, Nullable<int> intEntidad)
        {
            ParametrizacionDocumentoDalc paramDoc = new ParametrizacionDocumentoDalc();
            return paramDoc.ListarParametrizacionDocumento(intAdquisicion, intEntidad);

        }

       public ParametrizacionDocumentoEntity ConsultarDocumentoXDocID(int DocID)
       {
           ParametrizacionDocumentoDalc paramDoc = new ParametrizacionDocumentoDalc();
           return paramDoc.ConsultarDocumentoXDocID(DocID);
       }
       public void ActualizarDocumento(ParametrizacionDocumentoEntity pParametrizacionDocumentoEntity)
       {
           ParametrizacionDocumentoDalc paramDoc = new ParametrizacionDocumentoDalc();
           paramDoc.ActualizarDocumento(pParametrizacionDocumentoEntity);
       }
    }
}
