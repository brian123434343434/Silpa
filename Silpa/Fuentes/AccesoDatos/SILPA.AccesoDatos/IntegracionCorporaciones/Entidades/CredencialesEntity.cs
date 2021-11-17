using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.IntegracionCorporaciones.Entidades
{
    public class CredencialesEntity
    {
        #region Propiedades

            /// <summary>
            /// Usuario de autenticacion
            /// </summary>
            public string  Usuario { get; set; }

            /// <summary>
            /// Clave de acceso a los servicios REST
            /// </summary>
            public string Clave { get; set; }

        #endregion


        #region Metodos Publicos

            /// <summary>
            /// Retornar en un string la información del objeto
            /// </summary>
            /// <returns>string con la información contenida en el objeto</returns>
            public override string ToString()
            {
                string strDatos = "Usuario: " + (this.Usuario ?? "null")  + " -- " +
                                   "Clave: " +  (this.Clave ?? "null");
                
                return strDatos;
            }

        #endregion
    }
}
