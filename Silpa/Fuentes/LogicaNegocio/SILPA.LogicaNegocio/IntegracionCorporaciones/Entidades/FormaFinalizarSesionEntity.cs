using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.IntegracionCorporaciones.Entidades
{
    public class FormaFinalizarSesionEntity
    {
         #region Propiedades


            /// <summary>
            /// Identificador de la sesion para acceso web
            /// </summary>
            public string SessionWebID { get; set; }


            /// <summary>
            /// identificador de la sesion
            /// </summary>
            public string SessionID { get; set; }


        #endregion


        #region Metodos Publicos

            /// <summary>
            /// Retornar en un string la información del objeto
            /// </summary>
            /// <returns>string con la información contenida en el objeto</returns>
            public override string ToString()
            {
                string strDatos = "SessionWebID: " + (this.SessionWebID ?? "null")  + " -- " +
                                    "SessionID: " + (this.SessionID ?? "null");

                return strDatos;
            }

        #endregion
    }
}
