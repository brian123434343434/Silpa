using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Publicacion
{
    public class ComentarioIdentity : EntidadSerializable
    {
        /// <summary>
        /// Identificador del comentario
        /// </summary>
        private Int64 _idComentario;
      
        /// <summary>
        /// Identificador de la publicacion
        /// </summary>
        private Int64 _idPublicacion;

        /// <summary>
        /// Almacena el texto del comentario
        /// </summary>
        private string _texComentario;
        
        /// <summary>
        /// Almacena la fecha del comentario
        /// </summary>
        private string _fecComentario;

        /// <summary>
        /// Almacena el estado del comentario
        /// </summary>
        private bool _actComentario;

        #region Propiedades

        public Int64 IdComentario
        {
            get { return _idComentario; }
            set { _idComentario = value; }
        }


        public Int64 IdPublicacion
        {
            get { return _idPublicacion; }
            set { _idPublicacion = value; }
        }

        public string TexComentario
        {
            get { return _texComentario; }
            set { _texComentario = value; }
        }

        public string FecComentario
        {
            get { return _fecComentario; }
            set { _fecComentario = value; }
        }
        
        public bool ActComentario
        {
            get { return _actComentario; }
            set { _actComentario = value; }
        }

        #endregion


    }
}
