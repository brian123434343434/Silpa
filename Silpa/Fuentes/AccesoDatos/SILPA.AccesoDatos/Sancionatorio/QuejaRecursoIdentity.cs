using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Sancionatorio
{
    public class QuejaRecursoIdentity : EntidadSerializable
    {
        public QuejaRecursoIdentity()
        {
        }

        #region Definicion de los atributos

        /// <summary>
        /// Identificadoe de la relacion Queja - Recurso
        /// </summary>
        private Int64 _idQuejaRecurso;

        /// <summary>
        /// Identificadro de la queja
        /// </summary>
        private Int64 _idQueja;

        /// <summary>
        /// Identificador del recurso
        /// </summary>
        private int _idRecurso;

        /// <summary>
        /// Descripcion de otro recurso
        /// </summary>
        private string _otroRecurso;

        /// <summary>
        /// Estado de la queja y el recurso
        /// </summary>
        private bool _activoQuejaRecurso;        
        
        #endregion

        #region Propiedades....

        public Int64 IdQuejaRecurso
        {
            get { return _idQuejaRecurso; }
            set { _idQuejaRecurso = value; }
        }

        public Int64 IdQueja
        {
            get { return _idQueja; }
            set { _idQueja = value; }
        }

        public int IdRecurso
        {
            get { return _idRecurso; }
            set { _idRecurso = value; }
        }

        public string OtroRecurso
        {
            get { return _otroRecurso; }
            set { _otroRecurso = value; }
        }

        public bool ActivoQuejaRecurso
        {
            get { return _activoQuejaRecurso; }
            set { _activoQuejaRecurso = value; }
        }

        #endregion
    }
}
