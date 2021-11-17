using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Notificacion;

namespace SILPA.LogicaNegocio.Notificacion
{
    public class EnlaceDocumentoSila
    {
        
        
        #region Metodos Publicos


            /// <summary>
            /// Consultar la información de un enlace
            /// </summary>
            /// <param name="p_strEnlaceID">string con la información del enlace</param>
            /// <param name="p_strLlave">string con la llave</param>
            /// <returns>EnlaceDocumentoSilaEntity con la información del enlace</returns>
            public EnlaceDocumentoSilaEntity ConsultarEnlace(string p_strEnlaceID, string p_strLlave)
            {
                EnlaceDocumentoSilaDalc objEnlaceDocumentoSilaDalc = new EnlaceDocumentoSilaDalc();
                return objEnlaceDocumentoSilaDalc.ConsultarEnlace(p_strEnlaceID, p_strLlave);
            }


            /// <summary>
            /// Crear un nuevo enlace
            /// </summary>
            /// <param name="p_objEnlace">EnlaceDocumentoSilaEntity con la informcaion del enlace</param>
            public void CrearEnlace(EnlaceDocumentoSilaEntity p_objEnlace)
            {
                EnlaceDocumentoSilaDalc objEnlaceDocumentoSilaDalc = new EnlaceDocumentoSilaDalc();
                objEnlaceDocumentoSilaDalc.CrearEnlace(p_objEnlace);
            }

        #endregion


    }
}
