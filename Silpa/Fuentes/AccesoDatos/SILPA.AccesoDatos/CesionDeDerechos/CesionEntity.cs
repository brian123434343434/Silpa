using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;  

namespace SILPA.AccesoDatos.CesionDeDerechos
{
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/CesionDerechos.xsd")]
    [System.Xml.Serialization.XmlRootAttribute("CesionDerechos", Namespace = "http://tempuri.org/CesionDerechos.xsd", IsNullable = false)]
    public class CesionEntity : EntidadSerializable
    {

        public CesionEntity()
        { 
        }
        private string _numeroSilpa;
        public string NumeroSilpa
        {
            get { return _numeroSilpa; }
            set { _numeroSilpa = value; }
        }

        private string _idCesionario;

        public string IdCesionario
        {
            get { return _idCesionario; }
            set { _idCesionario = value; }
        }

    }
}
