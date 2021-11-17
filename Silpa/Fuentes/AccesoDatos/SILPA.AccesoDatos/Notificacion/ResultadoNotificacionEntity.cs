using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Notificacion
{
    public class ResultadoNotificacionEntity
    {
        #region Atributos
        /// <summary>
        /// Identificador del Resultado
        /// </summary>
        private int _id;

        /// <summary>
        /// Descripción del Resultado
        /// </summary>
        private string _resultado;

        /// <summary>
        /// Determina si el Resultado se encuentra Activo
        /// </summary>
        private decimal _activo; 
        #endregion

        #region Propiedades
        public decimal Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }


        public string Resultado
        {
            get { return _resultado; }
            set { _resultado = value; }
        }


        public int ID
        {
            get { return _id; }
            set { _id = value; }
        } 
        #endregion

    }
}
