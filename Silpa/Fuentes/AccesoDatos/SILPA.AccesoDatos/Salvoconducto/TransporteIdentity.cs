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
    public partial class TransporteIdentity : EntidadSerializable
    {
        private int _idSalvoconducto;

        private int modoTransporteField;

        private string nombreEmpresaField;

        private int tipoVehiculoField;

        private string matriculaField;

        private int tipoIdentificacionResField;

        private string numeroIdentificacionField;

        private string nombreResponsableField;

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
        public int ModoTransporte
        {
            get
            {
                return this.modoTransporteField;
            }
            set
            {
                this.modoTransporteField = value;
            }
        }


        /// <comentarios/>
        public string NombreEmpresa
        {
            get
            {
                return this.nombreEmpresaField;
            }
            set
            {
                this.nombreEmpresaField = value;
            }
        }

        /// <comentarios/>
        public int TipoVehiculo
        {
            get
            {
                return this.tipoVehiculoField;
            }
            set
            {
                this.tipoVehiculoField = value;
            }
        }

        /// <comentarios/>
        public string Matricula
        {
            get
            {
                return this.matriculaField;
            }
            set
            {
                this.matriculaField = value;
            }
        }

        /// <comentarios/>
        public int TipoIdentificacionRes
        {
            get
            {
                return this.tipoIdentificacionResField;
            }
            set
            {
                this.tipoIdentificacionResField = value;
            }
        }


        /// <comentarios/>
        public string NumeroIdentificacion
        {
            get
            {
                return this.numeroIdentificacionField;
            }
            set
            {
                this.numeroIdentificacionField = value;
            }
        }

        /// <comentarios/>
        public string NombreResponsable
        {
            get
            {
                return this.nombreResponsableField;
            }
            set
            {
                this.nombreResponsableField = value;
            }
        }
    }
}
