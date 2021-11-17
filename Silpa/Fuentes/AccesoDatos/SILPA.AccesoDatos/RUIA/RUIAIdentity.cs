using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.RUIA
{
    ///// <comentarios/>
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    //[System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/RUIA.xsd")]
    //[System.Xml.Serialization.XmlRootAttribute("sancion", Namespace = "http://tempuri.org/RUIA.xsd", IsNullable = false)]
    public partial class SancionType : EntidadSerializable
    {
        //private string numeroSilpaField;

        private int autoridadIdField;

        private int municipioIdField;

        private int veredaIdField;

        private int corregimientoIdField;

        private int tipoFaltaIdField;

        private string descripcionNormaField;

        private int tipoSancionPrincipalIdField;

        private string sancionAplicadaField;

        private string numeroExpField;

        private string numeroActoField;

        private string fechaExpActoField;

        private string fechaCumplimientoField;   
     
        private int tipoPersonaIdField;

        private string razonSocialField;

        private string nitField;

        private string primerNombreField;

        private string segundoNombreField;

        private string primerApellidoField;

        private string segundoApellidoField;

        private int tipoIdentificacionIdField;

        private string numeroIdentificacionField;

        private int municipioIdentificacionIdField;
        // 30-sep-2010 - aegb : incidencia 2284
        private int? tramiteModificacionField;

        private string tipoDocumentoField;

        private string observaciones;

        private SancionAccesoriaType[] sancionAccesoriaField;

        ///// <comentarios/>
        //public string numeroSilpa
        //{
        //    get
        //    {
        //        return this.numeroSilpaField;
        //    }
        //    set
        //    {
        //        this.numeroSilpaField = value;
        //    }
        //}

        /// <comentarios/>
        public int autoridadId
        {
            get
            {
                return this.autoridadIdField;
            }
            set
            {
                this.autoridadIdField = value;
            }
        }

        /// <comentarios/>
        public int municipioId
        {
            get
            {
                return this.municipioIdField;
            }
            set
            {
                this.municipioIdField = value;
            }
        }

        /// <comentarios/>
        public int veredaId
        {
            get
            {
                return this.veredaIdField;
            }
            set
            {
                this.veredaIdField = value;
            }
        }

        /// <comentarios/>
        public int corregimientoId
        {
            get
            {
                return this.corregimientoIdField;
            }
            set
            {
                this.corregimientoIdField = value;
            }
        }

        /// <comentarios/>
        public int tipoFaltaId
        {
            get
            {
                return this.tipoFaltaIdField;
            }
            set
            {
                this.tipoFaltaIdField = value;
            }
        }

        /// <comentarios/>
        public string descripcionNorma
        {
            get
            {
                return this.descripcionNormaField;
            }
            set
            {
                this.descripcionNormaField = value;
            }
        }

        /// <comentarios/>
        public int tipoSancionPrincipalId
        {
            get
            {
                return this.tipoSancionPrincipalIdField;
            }
            set
            {
                this.tipoSancionPrincipalIdField = value;
            }
        }

        /// <comentarios/>
        public string sancionAplicada
        {
            get
            {
                return this.sancionAplicadaField;
            }
            set
            {
                this.sancionAplicadaField = value;
            }
        }

        /// <comentarios/>
        public string numeroExp
        {
            get
            {
                return this.numeroExpField;
            }
            set
            {
                this.numeroExpField = value;
            }
        }

        /// <comentarios/>
        public string numeroActo
        {
            get
            {
                return this.numeroActoField;
            }
            set
            {
                this.numeroActoField = value;
            }
        }

        /// <comentarios/>
        public string fechaExpActo
        {
            get
            {
                return this.fechaExpActoField;
            }
            set
            {
                this.fechaExpActoField = value;
            }
        }

        /// <comentarios/>
        public string fechaCumplimiento
        {
            get
            {
                return this.fechaCumplimientoField;
            }
            set
            {
                this.fechaCumplimientoField = value;
            }
        }

        /// <comentarios/>
        public int tipoPersonaId
        {
            get
            {
                return this.tipoPersonaIdField;
            }
            set
            {
                this.tipoPersonaIdField = value;
            }
        }

        /// <comentarios/>
        public string razonSocial
        {
            get
            {
                return this.razonSocialField;
            }
            set
            {
                this.razonSocialField = value;
            }
        }

        /// <comentarios/>
        public string nit
        {
            get
            {
                return this.nitField;
            }
            set
            {
                this.nitField = value;
            }
        }

        /// <comentarios/>
        public string primerNombre
        {
            get
            {
                return this.primerNombreField;
            }
            set
            {
                this.primerNombreField = value;
            }
        }

        /// <comentarios/>
        public string segundoNombre
        {
            get
            {
                return this.segundoNombreField;
            }
            set
            {
                this.segundoNombreField = value;
            }
        }

        /// <comentarios/>
        public string primerApellido
        {
            get
            {
                return this.primerApellidoField;
            }
            set
            {
                this.primerApellidoField = value;
            }
        }

        /// <comentarios/>
        public string segundoApellido
        {
            get
            {
                return this.segundoApellidoField;
            }
            set
            {
                this.segundoApellidoField = value;
            }
        }

        /// <comentarios/>
        public int tipoIdentificacionId
        {
            get
            {
                return this.tipoIdentificacionIdField;
            }
            set
            {
                this.tipoIdentificacionIdField = value;
            }
        }

        /// <comentarios/>
        public string numeroIdentificacion
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
        public int municipioIdentificacionId
        {
            get
            {
                return this.municipioIdentificacionIdField;
            }
            set
            {
                this.municipioIdentificacionIdField = value;
            }
        }

        // 30-sep-2010 - aegb : incidencia 2284
        /// <comentarios/>
        public int? tramiteModificacion
        {
            get
            {
                return this.tramiteModificacionField;
            }
            set
            {
                this.tramiteModificacionField = value;
            }
        }

        public string tipoDocumento
        {
            get
            {
                return this.tipoDocumentoField;
            }
            set
            {
                this.tipoDocumentoField = value;
            }
        }

        public string Observaciones
        {
            get { return this.observaciones; }
            set { this.observaciones = value; }
        }
        /// <comentarios/>
        //[System.Xml.Serialization.XmlElementAttribute("sancionAccesoria")]
        public SancionAccesoriaType[] sancionAccesoria
        {
            get
            {
                return this.sancionAccesoriaField;
            }
            set
            {
                this.sancionAccesoriaField = value;
            }
        }

        public string fechaEjecutoriaActo { get; set; }

        public SancionType() { }
    }

    ///// <comentarios/>
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    //[System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/RUIA.xsd")]
    //[System.Xml.Serialization.XmlRootAttribute("SancionAccesoria", Namespace = "http://tempuri.org/RUIA.xsd", IsNullable = false)]
    public partial class SancionAccesoriaType
    {

        private int tipoSancionAccesoriaIdField;

        private string sancionAccesoriaNombreField;

        /// <comentarios/>
        public int tipoSancionAccesoriaId
        {
            get
            {
                return this.tipoSancionAccesoriaIdField;
            }
            set
            {
                this.tipoSancionAccesoriaIdField = value;
            }
        }

        /// <comentarios/>
        public string sancionAccesoriaNombre
        {
            get
            {
                return this.sancionAccesoriaNombreField;
            }
            set
            {
                this.sancionAccesoriaNombreField = value;
            }
        }
    }
}
