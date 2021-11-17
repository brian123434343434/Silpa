using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    public class TipoUsuarioIdentity : EntidadSerializable
    {
        public TipoUsuarioIdentity()
        {
            this._idTipoUsuario = 0;
            this._idPersona = 0;
            this._TipoUsuario = "";
            this._WorkFlowId = 0;
        }

        /// <summary>
        /// Constructor con parametros
        /// </summary>
        /// <param name="TipoPersonaId">Int64: identificador de usuario</param>
        /// <param name="Descripcion">string: Descripcion Tipo Usuario</param>
        /// <param name="idWorkFlow">Id WorkFlow de la tabla Participant roll group</param>
        public TipoUsuarioIdentity(int intTipoPersonaId,int idPersona,string strTipoPersona,int intWorkFlowId)
        {
            this._idTipoUsuario = intTipoPersonaId;
            this._TipoUsuario = strTipoPersona;
            this._idPersona = idPersona;
            this._WorkFlowId = intWorkFlowId;
        }

        #region declaracion de campos ...        

        /// <summary>
        /// Identificador del Tipo de Usuario
        /// UserId: texto
        /// </summary>
        private int _idTipoUsuario;
        private int _idPersona;
        /// <summary>
        /// Descripcion del tipo de Usuario
        /// </summary>
        private string _TipoUsuario;
        /// <summary>
        /// Identificador del grupo de WorkFlow
        /// </summary>
        private int _WorkFlowId;
        /// <summary>
        /// Segundo Nombre de la Persona
        /// </summary>
        
        #endregion

        #region declaracion de Propiedades ...
        public int IdTipoUsuario 
        { 
            get { return this._idTipoUsuario; } 
            set { this._idTipoUsuario= value; } 
        }
        public int IdPersona
        {
            get { return this._idPersona; }
            set { this._idPersona = value; }
        }
        public string TipoUsuario
        {
            get { return _TipoUsuario; }
            set { _TipoUsuario = value; }
        }
        public int WorkFlowId 
        { 
            get 
            { return this._WorkFlowId; } 
            set { this._WorkFlowId = value; } 
        }        
        #endregion

    }
}
