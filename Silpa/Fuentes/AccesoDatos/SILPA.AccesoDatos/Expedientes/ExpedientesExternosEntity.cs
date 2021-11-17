using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Expedientes
{
    public class ExpedientesExternosEntity : EntidadSerializable
    {
        public ExpedientesExternosEntity()
        {
           
        }


        // Lista de Codigos Expedientes
        public string EXT_EXPEDIENTE_AA { get; set; }

        // Lista de Codigos Expedientes
        public string EXP_ID_SOLICITANTE_AA { get; set; }

        // Lista de Codigos NUMERO SILPA
        public string EXP_NUMERO_SILPA { get; set; }

        // Lista de Codigos Expedientes
        public string EXP_ID { get; set; }

        // Lista de Tramite ID
        public string Tipo_Tramite_ID { get; set; }

        // Lista de Codigos Expedientes
        public string EXP_NOMBRE_SOLICITANTE_AA { get; set; }

        // Lista de Codigos Expedientes
        public string EXP_IDENTIFICACION_SOLICITANTE_AA { get; set; }

        // Lista de Codigos Expedientes
        public string EXP_NOMBREE_AA { get; set; }


        // Lista de Codigos Expedientes
        public bool? ACTIVO { get; set; }
    }
}
