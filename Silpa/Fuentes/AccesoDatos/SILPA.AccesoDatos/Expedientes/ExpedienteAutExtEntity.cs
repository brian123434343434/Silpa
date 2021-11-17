using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Expedientes
{
    public class ExpedienteAutExtEntity : EntidadSerializable
    {
        public ExpedienteAutExtEntity()
        {
           
        }

        // Identificador del objeto
        public string EXT_ID { get; set; }
        // ID Autoridad Ambiental Externa
        public string EXP_ID_AUT_EXT { get; set; }
        // Nombre autoridad Ambienta Externa
        public string EXT_NOMBRE_AUT_EXT { get; set; }
        // Fecha de Envio
        public string EXP_FECHA_REGISTRO { get; set; }
        // Fecha de Envio
        public List<ExpedientesExternosEntity> LISTA_EXT_EXPEDIENTE_AA { get; set; }

        
    }
}
