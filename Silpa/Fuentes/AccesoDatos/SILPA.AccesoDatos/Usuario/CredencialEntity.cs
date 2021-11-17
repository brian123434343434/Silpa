using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Usuario
{

    public partial class CredencialEntity : EntidadSerializable
    {
        private string personaIdField;

        private string motivoRechazoField;

        /// <comentarios/>
        public string personaId
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
        public string motivoRechazo
        {
            get
            {
                return this.motivoRechazoField;
            }
            set
            {
                this.motivoRechazoField = value;
            }
        }
    }

}
