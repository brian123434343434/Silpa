using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.GenerarSalvoConducto
{
    public class WSRespuestaSalvoConducto : SILPA.Comun.EntidadSerializable
    {
         /// <summary>
        /// Constructor sin parametros
        /// </summary>
        public WSRespuestaSalvoConducto() { }

      
        public string NumeroVital  { get; set; }
        public string NumeroActoAdministrivo { get; set; }
        public bool Exito { get; set; }
        public string Comentario { get; set; }
        public SILPA.AccesoDatos.GenerarSalvoConducto.SalActoAdministrativo ActoAdministrativo { get; set; }

    }
}
