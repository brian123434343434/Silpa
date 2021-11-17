using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Generico
{
    public partial class CredTipoUsuario : EntidadSerializable
    {
        private string personaIdField;

        private string tipoUsuarioIdField;

        /// <comentarios/>
        public string IdPersona
        {
            get
            {
                return this.personaIdField;
            }
            set
            {
                this.personaIdField = value;
            }
        }

        /// <comentarios/>
        public string IdTipoUsuario
        {
            get
            {
                return this.tipoUsuarioIdField;
            }
            set
            {
                this.tipoUsuarioIdField = value;
            }
        }
    }
}
