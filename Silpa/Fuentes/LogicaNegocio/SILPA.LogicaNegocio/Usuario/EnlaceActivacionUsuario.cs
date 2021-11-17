using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Usuario;

namespace SILPA.LogicaNegocio.Usuario
{
    public class EnlaceActivacionUsuario
    {
                
        #region Metodos Publicos


        /// <summary>
        /// Consultar la información de un enlace
        /// </summary>
        /// <param name="p_strLlave">string con la llave</param>
        /// <returns>EnlaceActivacionUsuarioEntity con la información del enlace</returns>
        public EnlaceActivacionUsuarioEntity ConsultarEnlace(string p_strLlave)
            {
                EnlaceActivacionUsuarioDalc objEnlaceActivacionUsuarioDalc = new EnlaceActivacionUsuarioDalc();
                return objEnlaceActivacionUsuarioDalc.ConsultarEnlace(p_strLlave);
            }


            /// <summary>
            /// Crear un nuevo enlace
            /// </summary>
            /// <param name="p_objEnlace">EnlaceActivacionUsuarioEntity con la informcaion del enlace</param>
            public void CrearEnlace(EnlaceActivacionUsuarioEntity p_objEnlace)
            {
                EnlaceActivacionUsuarioDalc objEnlaceActivacionUsuarioDalc = new EnlaceActivacionUsuarioDalc();
                objEnlaceActivacionUsuarioDalc.CrearEnlace(p_objEnlace);
            }


            /// <summary>
            /// Editar un enlace
            /// </summary>
            /// <param name="p_objEnlace">EnlaceActivacionUsuarioEntity con la informcaion del enlace</param>
            public void EditarEnlace(EnlaceActivacionUsuarioEntity p_objEnlace)
            {
                EnlaceActivacionUsuarioDalc objEnlaceActivacionUsuarioDalc = new EnlaceActivacionUsuarioDalc();
                objEnlaceActivacionUsuarioDalc.EditarEnlace(p_objEnlace);
            }

        #endregion
        
    }
}
