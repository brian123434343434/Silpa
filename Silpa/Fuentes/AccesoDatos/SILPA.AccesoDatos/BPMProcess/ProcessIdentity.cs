using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.BPMProcess
{
    public class ProcessIdentity:EntidadSerializable
    {
        public ProcessIdentity()
        {
        }

        #region Definicion de los atributos

        /// <summary>
        /// Identificador Id del formulario
        /// </summary>
        private Int64 _idForm;

        /// <summary>
        /// Id de Usuario
        /// </summary>
        private Int64 _idUser;


        
        /// <summary>
        /// Valor de la cadena con los parametros a insertar
        /// </summary>
        private string _cadena;

        /// <summary>
        /// Orden del Campo
        /// </summary>
        private Int64 _orden;

        /// <summary>
        /// Respuesta de Consulta
        /// </summary>
        private string _resp;


        /// <summary>
        /// Id Form Instance
        /// </summary>
        private Int64 _idFormInstance;

        #endregion


        #region Propiedades....

        public Int64 IdForm
        {
            get { return _idForm; }
            set { _idForm = value; }
        }

        public Int64 IdUser
        {
            get { return _idUser; }
            set { _idUser = value; }
        }
        

        public string Cadena
        {
            get { return _cadena; }
            set { _cadena = value; }
        }

        public string Resp
        {
            get { return _resp; }
            set { _resp = value; }
        }

        public Int64 IdFormInstance
        {
            get { return _idFormInstance; }
            set { _idFormInstance = value; }
        }

        #endregion
    
    }
}
