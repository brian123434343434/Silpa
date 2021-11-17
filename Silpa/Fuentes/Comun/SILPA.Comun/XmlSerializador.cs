using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Reflection;

//using System.Runtime.Serialization;
//using System.Runtime.Serialization.Formatters.Binary;

namespace SILPA.Comun
{
    [Serializable]
    public class XmlSerializador
    {
        public PropertyInfo[] ListaPropiedadesSenderInfo; // propiedades del objeto original

        /// <summary>
        /// acion realizada por el Usuario
        /// </summary>

        private string str_file_xml_propiedades;

        public XmlSerializador()
        {
            
        }

        public XmlSerializador(string strFile)
        {
            this.str_file_xml_propiedades = strFile;
        }

        /// <summary>
        /// Método útil para serializar cualquier objeto a xml
        /// autor: Hava
        /// Fecha: 12 de junio de 2009
        /// </summary>
        /// <param name="sender">Objeto genérico que se va a serializar a XML</param>
        /// <returns>String:  XML con el objeto serializado </returns>
        public string serializar(object sender)
        {
            string Xml = string.Empty;

            try
            {
                if (sender != null)
                {
                    XmlSerializer Serializer = null;

                    MemoryStream MyMemoryStream = null;
                    //object ObjectReference = new miClase();
                    object ObjectReference = sender;
                    Type ObjectType = ObjectReference.GetType();
                    //object ObjectReference = this;
                    Serializer = new XmlSerializer(ObjectType);
                    MyMemoryStream = new MemoryStream();
                    using (StreamWriter MyWriter = new StreamWriter(MyMemoryStream))
                    {
                        Serializer.Serialize(MyWriter, ObjectReference);
                        MyWriter.Flush();
                        MyMemoryStream.Position = 0;
                        using (StreamReader MyReader = new StreamReader(MyMemoryStream))
                        {
                            Xml = MyReader.ReadToEnd();
                        }
                    }
                }

            }
            catch(Exception ex)
            {

            }

            return Xml;
        }

        /// <summary>
        /// Incluye los nombres de las etiquetas en 
        /// los datos de cada elemento
        /// Autor: Hava UT -Soft - Netco
        /// 09 de julio de 2009
        /// </summary>
        /// <param name="strXml"></param>
        /// <returns></returns>
        public string LabelData(string strXml)
        {
            string xmlLabeled = string.Empty;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(strXml);

            //XmlNodeList xmlNodes = xmlDoc.ChildNodes;
            //string h = xmlNodes[0].InnerText;
            //h = xmlNodes[1].InnerText;
            //h = xmlNodes[2].InnerText;

            //xmlDoc.DocumentElement.
            //string h = xmlNodes[0].InnerText;
            //h = xmlNodes[1].InnerText;
            //h = xmlNodes[2].InnerText;



            return xmlLabeled;
        }

        /// <summary>
        /// Serializa las propiedades de un objeto a string
        /// Autor: Hava UT -Soft - Netco
        /// 21 de julio de 2009
        /// </summary>
        /// <param name="Sender"></param>
        /// <returns></returns>
        public string StringSerializer(object Sender)
        {
            string objValue;
            string strResult = string.Empty;
            //  se establecen las propiedades no mostrables en la bitacora
            this.SetProperties_NoShow();

            if (Sender != null)
            {
                Type senderType = Sender.GetType();

                this.ListaPropiedadesSenderInfo = Sender.GetType().GetProperties();

                for (int i = 0; i < this.ListaPropiedadesSenderInfo.Length; i++)
                {
                    // PropertyInfo senderInfo = senderType.GetProperty(this.ListaPropiedadesEntityInfo[i].Name);

                    PropertyInfo senderInfo = (PropertyInfo)this.ListaPropiedadesSenderInfo[i];

                    /// Si es propiedad se setea.
                    if (senderInfo.MemberType == MemberTypes.Property && senderInfo.CanRead == true)
                    {
                        // 
                        if (senderInfo.CanRead == true)
                        {
                            // Solo los tipos de datos primitivos
                            if (
                                senderInfo.PropertyType.Name == TypeCode.String.ToString() ||
                                senderInfo.PropertyType.Name == TypeCode.DateTime.ToString() ||
                                senderInfo.PropertyType.Name == TypeCode.Int64.ToString() ||
                                senderInfo.PropertyType.Name == TypeCode.Int32.ToString() ||
                                senderInfo.PropertyType.Name == TypeCode.Int16.ToString() ||
                                senderInfo.PropertyType.Name == TypeCode.Decimal.ToString() ||
                                senderInfo.PropertyType.Name == TypeCode.Double.ToString() ||
                                senderInfo.PropertyType.Name == TypeCode.Char.ToString() ||
                                senderInfo.PropertyType.Name == TypeCode.Boolean.ToString()

                                )
                            {
                                //pinfo.SetValue(Entity, senderInfo.GetValue(Sender, null), null);

                                try
                                {
                                    if (MostrarPropiedad(Sender.ToString(), senderInfo.Name))
                                    {
                                        object obj = senderInfo.GetValue(Sender, null);
                                        if (obj != null) { objValue = obj.ToString(); } else { objValue = null; }
                                        strResult = strResult + "   [ " + senderInfo.Name + ": " + objValue + " ]   ";
                                    }
                                }
                                catch
                                {

                                }
                            }
                        }
                    }
                }
            }// if sender null
            return strResult;
        }


        /// <summary>
        /// Compara los valores de Cada una de las propiedades de los objetos
        /// y determina las propiedades con valores diferentes o que han cambiado
        /// Autor: Hava UT -Soft - Netco
        /// 21 de julio de 2009
        /// </summary>
        /// <param name="objInicial">object: Con los datos recuperados de la DB antes de actualizar</param>
        /// <param name="objFinal">object: con los datos nuevos antes de guardarse en la base de datos</param>
        public string CompararObjetos(object objFinal, object objInicial)
        {
            //string result = string.Empty;

            PropertyInfo[] ListaPropiedades_ObjectFinal_Info; // propiedades del objeto Final
            PropertyInfo[] ListaPropiedades_ObjectInicial_Info; // propiedades del objeto Inicial


            //PropertyInfo objFinalInfo = (PropertyInfo)this.ListaPropiedades_objFinalInfo[i];

            string objValue;
            string objValueInicial;
            string strResult = string.Empty;

            if (objFinal != null)
            {
                Type objFinalType = objFinal.GetType();
                Type objInicialType = objInicial.GetType();

                ListaPropiedades_ObjectFinal_Info = objFinal.GetType().GetProperties();
                ListaPropiedades_ObjectInicial_Info = objInicial.GetType().GetProperties();

                for (int i = 0; i < ListaPropiedades_ObjectFinal_Info.Length; i++)
                {
                    // PropertyInfo senderInfo = senderType.GetProperty(this.ListaPropiedadesEntityInfo[i].Name);

                    PropertyInfo objFinalInfo = (PropertyInfo)ListaPropiedades_ObjectFinal_Info[i];
                    PropertyInfo objInicialInfo = (PropertyInfo)ListaPropiedades_ObjectInicial_Info[i];

                    /// Si es propiedad se setea.
                    if (objFinalInfo.MemberType == MemberTypes.Property && objFinalInfo.CanRead == true)
                    {
                        // 
                        if (objFinalInfo.CanRead == true)
                        {
                            // Solo los tipos de datos primitivos
                            if (
                                objFinalInfo.PropertyType.Name == TypeCode.String.ToString() ||
                                objFinalInfo.PropertyType.Name == TypeCode.DateTime.ToString() ||
                                objFinalInfo.PropertyType.Name == TypeCode.Int64.ToString() ||
                                objFinalInfo.PropertyType.Name == TypeCode.Int32.ToString() ||
                                objFinalInfo.PropertyType.Name == TypeCode.Int16.ToString() ||
                                objFinalInfo.PropertyType.Name == TypeCode.Decimal.ToString() ||
                                objFinalInfo.PropertyType.Name == TypeCode.Double.ToString() ||
                                objFinalInfo.PropertyType.Name == TypeCode.Char.ToString() ||
                                objFinalInfo.PropertyType.Name == TypeCode.Boolean.ToString()

                                )
                            {
                                //pinfo.SetValue(Entity, senderInfo.GetValue(Sender, null), null);

                                if (MostrarPropiedad(objFinal.ToString(), objFinalInfo.Name))
                                {
                                    object objf = objFinalInfo.GetValue(objFinal, null);
                                    object obji = objInicialInfo.GetValue(objInicial, null);

                                    try
                                    {
                                        objf = objFinalInfo.GetValue(objFinal, null);
                                        obji = objInicialInfo.GetValue(objInicial, null);

                                        if (obji != null) { objValueInicial = obji.ToString(); } else { objValueInicial = null; }

                                        if (objf != null) { objValue = objf.ToString(); } else { objValue = null; }

                                        if (objValueInicial != objValue)
                                        {
                                            strResult = strResult + "   [ " + objInicialInfo.Name + ": " + objValueInicial + " ]  ->  ";
                                            strResult = strResult + "   [ " + objFinalInfo.Name + ": " + objValue + " ]*   ";
                                        }
                                    }
                                    catch
                                    {

                                    }
                                }
                            }
                        }
                    }
                }
            }// if sender null
            // Resultado de las comparaciones
            return strResult;
        }


        /// <summary>
        /// Método que determina si la propiedad actual del objeto 
        /// se debe o no mostrar en la bitacora
        /// Autor: Hava UT -Soft - Netco
        /// 22 de julio de 2009
        /// </summary>
        /// <param name="objeto"></param>
        /// <param name="propiedad"></param>
        /// <returns></returns>
        private bool MostrarPropiedad(string objeto, string propiedad)
        {
            bool result = true;
            string token = objeto + "." + propiedad;

            this.SetProperties_NoShow();

            if (this.Property_No_Show.IndexOf(token) >= 0)
            {
                result = false;
            }

            return result;
        }

        #region Listas de propiedades que no se incluyen en la lectura de la bitacora por cada objeto:
        /// <summary>
        /// Autor: Hava UT -Soft - Netco
        /// 22 de julio de 2009
        /// </summary>
        private ArrayList Property_No_Show;
        #endregion

        /// <summary>
        /// carga las propiedade de los objetos que no queremos mostrar en la bitacota
        /// Autor: Hava UT -Soft - Netco
        /// 22 de julio de 2009
        /// </summary>
        public void SetProperties_NoShow()
        {
            // Propiedades no visibles en bitacora:
            XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(str_file_xml_propiedades);
            xmlDoc.Load(this.str_file_xml_propiedades);
            // se obtienen los elementos de la lista de propiedades
            XmlNodeList xmlNodeList = xmlDoc.ChildNodes[1].ChildNodes;
            this.Property_No_Show = new ArrayList();
            foreach (XmlNode node in xmlNodeList)
            {
                Property_No_Show.Add(node.InnerText);
            }
        }


        /// <summary>
        /// Método que permite la deserailizacón de las calses
        /// </summary>
        /// <param name="objeto">Object: por referencia debe estar instanciado</param>
        /// <param name="xmlObject">string: Xml que contiene el estado del objeto</param>
        /// <returns>Object: objeto genérico serializado, requiere el casting especifico de la clase una vez retornado</returns>
        public object Deserializar(object objeto, string xmlObject)
        {
            
            object objectResult = null;

            if (objeto != null && xmlObject != string.Empty)
            {
                // se instancia el serializador
                Type MyType = objeto.GetType();
                XmlSerializer xmlser = new XmlSerializer(MyType);
                //Encoding unicode = Encoding.Unicode;
                Encoding utf8 = Encoding.UTF8;
                byte[] utf8Bytes = utf8.GetBytes(xmlObject);
                //byte[] utf8Bytes = unicode.GetBytes(xmlObject);
                MemoryStream ms = new MemoryStream(utf8Bytes);
                ms.Position = 0;
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.CloseInput = true;
                settings.IgnoreWhitespace = true;
                settings.MaxCharactersInDocument = 0;
                using (XmlReader xmlReader = XmlReader.Create(ms, settings))
                {
                    objectResult = xmlser.Deserialize(xmlReader);
                }
            } 
            return objectResult;
        }
    }
}
