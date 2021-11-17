using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Expedientes
{
    public class ExpedienteSilaEntity : EntidadSerializable
    {
        public ExpedienteSilaEntity()
        { }
        #region Atributos

        #region Documentacion Atributo
        /// <summary>
        /// Identificador del expediente
        /// </summary>
        #endregion
        public int exp_id { get; set; }

        #region Documentacion Atributo
        /// <summary>
        /// Fecha de registro del expediente
        /// </summary>
        #endregion
        public DateTime fecha_creacion { get; set; }

       #region Documentacion Atributo
        /// <summary>
        /// Nombre del expediente
        /// </summary>
        #endregion
        public string str_expediente_nombre { get; set; }

        #region Documentacion Atributo
        /// <summary>
        /// Identificador del sector
        /// </summary>
        #endregion
        public int sec_id { get; set; }

        #region Documentacion Atributo
        /// <summary>
        /// Nombre del sector
        /// </summary>
        #endregion
        public string str_sec_nombre { get; set; }

        #region Documentacion Atributo
        /// <summary>
        /// Actual estado del expediente
        /// </summary>
        #endregion
        public int int_est_id { get; set; }

        #region Documentacion Atributo
        /// <summary>
        /// Nombre del estado
        /// </summary>
        #endregion
        public string str_est_nombre { get; set; }

        #region Documentacion Atributo
        /// <summary>
        /// Descripcion del expediente
        /// </summary>
        #endregion
        public string str_exp_descripcion { get; set; }

        #region Documentacion Atributo
        /// <summary>
        /// Codigo calculado del expediente
        /// </summary>
        #endregion
        public string str_exp_codigo { get; set; }

        #region Documentacion Atributo
        /// <summary>
        /// Número del expediente en vital
        /// </summary>
        #endregion
        public string str_exp_numero_vital { get; set; }

        #region Documentacion Atributo
        /// <summary>
        /// Numero de radicacion
        /// </summary>
        #endregion
        public string str_numero_radicacion { get; set; }

        #region Documentacion Atributo
        /// <summary>
        /// identificador del tramite
        /// </summary>
        #endregion
        public int str_numero_tramite { get; set; }

        #region Documentacion Atributo
        /// <summary>
        /// identificador del solicitante
        /// </summary>
        #endregion
        public int str_solicitante_id { get; set; }

        #region Documentacion Atributo
        /// <summary>
        /// nombre del solicitante
        /// </summary>
        #endregion
        public string str_nombre_solicitante{ get; set; }

        #region Documentacion Atributo
        /// <summary>
        /// nombre del tramite
        /// </summary>
        #endregion
        public string str_nombre_tramite { get; set; }

        #region Documentacion Atributo
        /// <summary>
        /// ubicacion del expediente
        /// </summary>
        #endregion
        public string str_nombre_ubicacion { get; set; }

        #region Documentacion Atributo
        /// <summary>
        /// AUTORIDAD AMBIENTAL ASOCIADA
        /// </summary>
        #endregion
        public string str_nombre_autoridad_ambiental { get; set; }

        #region Documentacion Atributo
        /// <summary>
        /// identificador del ADM
        /// </summary>
        #endregion
        public int str_numero_adm{ get; set; }
        #endregion




    }
}
