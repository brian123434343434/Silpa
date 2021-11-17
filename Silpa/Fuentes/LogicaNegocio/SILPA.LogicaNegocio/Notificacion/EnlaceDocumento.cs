using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Notificacion;

namespace SILPA.LogicaNegocio.Notificacion
{
    public class EnlaceDocumento
    {
        
        
        #region Metodos Publicos


            /// <summary>
            /// Consultar la información de un enlace
            /// </summary>
            /// <param name="p_strEnlaceID">string con la información del enlace</param>
            /// <param name="p_strLlave">string con la llave</param>
            /// <returns>EnlaceDocumentoEntity con la información del enlace</returns>
            public EnlaceDocumentoEntity ConsultarEnlace(string p_strEnlaceID, string p_strLlave)
            {
                EnlaceDocumentoDalc objEnlaceDocumentoDalc = new EnlaceDocumentoDalc();
                return objEnlaceDocumentoDalc.ConsultarEnlace(p_strEnlaceID, p_strLlave);
            }


            /// <summary>
            /// Crear un nuevo enlace
            /// </summary>
            /// <param name="p_objEnlace">EnlaceDocumentoEntity con la informcaion del enlace</param>
            public void CrearEnlace(EnlaceDocumentoEntity p_objEnlace)
            {
                EnlaceDocumentoDalc objEnlaceDocumentoDalc = new EnlaceDocumentoDalc();
                objEnlaceDocumentoDalc.CrearEnlace(p_objEnlace);
            }

        #endregion


    }
}
