using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;


namespace SILPA.Comun
{
    
    /// <summary>
    /// Clase que contiene el método para serializarse a XML
    /// </summary>
    [Serializable]
    public abstract class EntidadSerializable
    {
        /// <summary>
        /// objeto para serailizar la clase
        /// </summary>
        private XmlSerializador XmlSerial;

        /// <summary>
        /// Método que permite a cada clase representarse como XML.
        /// </summary>
        /// <returns>string: Contiene representación XML del objeto</returns>
        public virtual string GetXml() 
        {
            //XmlSerial = new XmlSerializador();
            //return this.XmlSerial.serializar(this);
            return (string)(new XmlSerializador().serializar(this));
        }

        /// <summary>
        /// Obtiene el objeto a serializado
        /// </summary>
        /// <returns>objeto</returns>
        public virtual object GetSerializedObject(object obj, string xmlObjectString)
        {
            //XmlSerial = new XmlSerializador();
            //return this.XmlSerial.serializar(this);
            return (object)(new XmlSerializador().Deserializar(obj, xmlObjectString));
        }


        /// <summary>
        /// Obtiene el objeto a serializado
        /// </summary>
        /// <returns>objeto</returns>
        public virtual object Deserializar(string xmlObjectString)
        {
            //XmlSerial = new XmlSerializador();
            //return this.XmlSerial.serializar(this);
            return (object)(new XmlSerializador().Deserializar(this, xmlObjectString));
        }

    }
}
