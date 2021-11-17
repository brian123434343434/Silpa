using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.ConsultaPublica
{
    public class ConsultaPublicaEntity : EntidadSerializable
    {
        public ConsultaPublicaEntity()
        { }
        public int ID_CONSULTA_PUBLICA { get; set; }
        public int TAR_SOL_ID { get; set; }
        public string SOL_NUM_SILPA { get; set; }
        public int SEC_ID { get; set; }
        public string SEC_NOMBRE { get; set; }
        public int SEC_PADRE_ID { get; set; }
        public string NOMBRE_SEC_PADRE { get; set; }
        public string AUT_NOMBRE { get; set; }
        public string TRA_NOMBRE { get; set; }
        public string EXPEDIENTE { get; set; }
        public string NOMBRE_PROYECTO { get; set; }
        public string TAR_FECHA_CREACION { get; set; }
        public string TAR_FECHA_FINALIZACION { get; set; }
        public string MUNICIPIO { get; set; }
        public string DEPARTAMENTO { get; set; }
        public string NOMBRE_COMPLETO { get; set; }
        public string SOL_ID_SOLICITANTE { get; set; }
        public string ORIGEN { get; set; }
        public string NUM_DOCUMENTO { get; set; }

        /// <summary>
        /// Parametro asociado para la busqueda de consulta publica: Numero de Paginas devueltas en una consulta
        /// </summary>
        public int temporalNumeroPaginas { get; set; }

        /// <summary>
        /// Parametro asociado para la busqueda de consulta publica: Numero de Registros devueltos en una consulta
        /// </summary>
        public int temporalNumeroRegistros { get; set; }
    }
}
