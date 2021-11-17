using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;

namespace SILPA.AccesoDatos.Comunicacion
{
    ///// <comentarios/>
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    //[System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/ComunicacionVisita.xsd")]
    //[System.Xml.Serialization.XmlRootAttribute("comunicacionVisita", Namespace = "http://tempuri.org/ComunicacionVisita.xsd", IsNullable = false)]

    public class ComunicacionVisitaType : EntidadSerializable
    {
        private string numSilpaField;

        private string numExpedienteField;

        private string fechaInicialField;

        private string fechaFinalField;

        private string descripcionVisitaField;

        private ResponsableType[] responsableField;

        /// <comentarios/>
        public string numSilpa
        {
            get  { return this.numSilpaField;  }
            set { this.numSilpaField = value;  }
        }

        /// <comentarios/>
        public string numExpediente
        {
            get  { return this.numExpedienteField; }
            set  {  this.numExpedienteField = value; }
        }

        /// <comentarios/>
        public string fechaInicial
        {
            get  {  return this.fechaInicialField;  }
            set  {  this.fechaInicialField = value;  }
        }

        /// <comentarios/>
        public string fechaFinal
        {
            get { return this.fechaFinalField;  }
            set {  this.fechaFinalField = value;  }
        }

        /// <comentarios/>
        public string descripcionVisita
        {
            get  { return this.descripcionVisitaField;  }
            set   { this.descripcionVisitaField = value;  }
        }

        /// <comentarios/>
        //[System.Xml.Serialization.XmlElementAttribute("responsable")]
        public ResponsableType[] responsable
        {
            get   {  return this.responsableField;   }
            set  { this.responsableField = value;    }
        }
    }

    ///// <comentarios/>
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    //[System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/ComunicacionVisita.xsd")]
    //[System.Xml.Serialization.XmlRootAttribute("Responsable", Namespace = "http://tempuri.org/ComunicacionVisita.xsd", IsNullable = false)]
    public partial class ResponsableType
    {

        private string nombreField;

        private string cedulaField;

        private string cargoField;
   
        /// <comentarios/>
        public string nombre
        {
            get  {  return this.nombreField; }
            set  {  this.nombreField = value; }
        }

        /// <comentarios/>
        public string cedula
        {
            get   { return this.cedulaField;   }
            set  {  this.cedulaField = value;  }
        }

        /// <comentarios/>
        public string cargo
        {
            get {   return this.cargoField;  }
            set  {    this.cargoField = value;    }
        }    
    }
}