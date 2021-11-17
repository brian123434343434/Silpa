using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Salvoconducto
{
    /// <comentarios/>
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    //[System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/Salvoconducto.xsd")]
    public partial class EspecimenIdentity : EntidadSerializable
    {
        private int _idSalvoconducto;

        private string nombreCientificoField;

        private string nombreComunField;

        private string descripcionEspecimenField;

        private string identificacionEspecimenField;

        private decimal cantidadEspecimenField;

        private int unidadMedidaIdField;

        private string unidadMedidaField;

        private string dimensionesEspecimenField;

        private int recursoIdField;


        public int idSalvoconducto
        {
            get
            {
                return this._idSalvoconducto;
            }
            set
            {
                this._idSalvoconducto = value;
            }
        }

        /// <comentarios/>
        public string NombreCientifico
        {
            get
            {
                return this.nombreCientificoField;
            }
            set
            {
                this.nombreCientificoField = value;
            }
        }


        /// <comentarios/>
        public string NombreComun
        {
            get
            {
                return this.nombreComunField;
            }
            set
            {
                this.nombreComunField = value;
            }
        }

        /// <comentarios/>
        public string DescripcionEspecimen
        {
            get
            {
                return this.descripcionEspecimenField;
            }
            set
            {
                this.descripcionEspecimenField = value;
            }
        }

        /// <comentarios/>
        public string IdentificacionEspecimen
        {
            get
            {
                return this.identificacionEspecimenField;
            }
            set
            {
                this.identificacionEspecimenField = value;
            }
        }

        /// <comentarios/>
        public decimal CantidadEspecimen
        {
            get
            {
                return this.cantidadEspecimenField;
            }
            set
            {
                this.cantidadEspecimenField = value;
            }
        }

        /// <comentarios/>
        public int UnidadMedidaId
        {
            get
            {
                return this.unidadMedidaIdField;
            }
            set
            {
                this.unidadMedidaIdField = value;
            }
        }

        /// <comentarios/>
        public string UnidadMedida
        {
            get
            {
                return this.unidadMedidaField;
            }
            set
            {
                this.unidadMedidaField = value;
            }
        }

        /// <comentarios/>
        public string DimensionesEspecimen
        {
            get
            {
                return this.dimensionesEspecimenField;
            }
            set
            {
                this.dimensionesEspecimenField = value;
            }
        }

        public int RecursoId
        {
            get
            {
                return this.recursoIdField;
            }
            set
            {
                this.recursoIdField = value;
            }
        }
    }
}
