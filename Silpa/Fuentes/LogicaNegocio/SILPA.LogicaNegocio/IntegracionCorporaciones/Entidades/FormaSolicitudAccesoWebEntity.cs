using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.IntegracionCorporaciones.Entidades
{
    public class FormaSolicitudAccesoWebEntity
    {
         #region Propiedades


            /// <summary>
            /// Identificador del usuario que realiza solicitud de ingreso
            /// </summary>
            public int UsuarioID { get; set; }

            /// <summary>
            /// Opcion a la cual se desea acceder
            /// </summary>
            public int OpcionID { get; set; }

            /// <summary>
            /// Identificador de la sesion
            /// </summary>
            public string SessionID { get; set; }

            /// <summary>
            /// Lista de roles separados por '-'
            /// </summary>
            public string RolesUsuario { get; set; }

            /// <summary>
            /// Direccion ip del usuario
            /// </summary>
            public string IPUsuario { get; set; }

            /// <summary>
            /// URL de retorno al finalizar sesion
            /// </summary>
            public string URLRetorno { get; set; }


        #endregion


        #region Metodos Publicos

            /// <summary>
            /// Retornar en un string la información del objeto
            /// </summary>
            /// <returns>string con la información contenida en el objeto</returns>
            public override string ToString()
            {
                string strDatos = "UsuarioID: " + this.UsuarioID.ToString() + " -- " +
                                    "OpcionID: " + this.OpcionID.ToString() + " -- " +
                                    "SessionID: " + (this.SessionID ?? "null")  + " -- " +
                                    "IPUsuario: " + (this.IPUsuario ?? "null") + " -- " +
                                    "URLRetorno: " + (this.URLRetorno ?? "null");

                return strDatos;
            }

        #endregion
    }
}
