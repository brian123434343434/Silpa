using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using SoftManagement.Log;  


namespace SILPA.Comun
{
    
    /// <summary>
    /// Clase que permite en envío y recepción de documentos 
    /// desde y hasta a una especificación de servidor de documentos
    /// </summary>
    public partial class TraficoDocumento
    {
        /// <summary>
        /// campo para almacenar la ruta de acceso al documento.
        /// </summary>
        private string _ubicacion;
        public string Ubicacion 
        { 
            get { return this._ubicacion; }
            set { this._ubicacion = value;  } 
        }


        /// <summary>
        /// contenido binario del documento para su transferencia
        /// </summary>
        private byte[] _bytes;
        public byte[] Bytes
        {
            get { return this._bytes; }
            set { this._bytes = value; }
        }


        /// <summary>
        /// Ubicación de los archivos en el file traffic - durectorio físico
        /// </summary>
        private string _fileTraffic;
        public string FileTraffic 
        { 
            get { return this._fileTraffic; } 
            set { this._fileTraffic = value; } 
        }


        /// <summary>
        /// objeto de configuración de datos
        /// </summary>
        Configuracion objConfiguracion;

        /// <summary>
        /// Campo que guarda el listado de documentos adjuntos
        /// </summary>
        private ListaDocumentoAdjuntoType _listaDocumentos;

        public ListaDocumentoAdjuntoType ListaDocumentos
        {
            get { return _listaDocumentos; }
            set { _listaDocumentos = value; }
        }

        /// <summary>
        /// url http del file traffic
        /// </summary>
        private string _urlFileTraffic;
        public string UrlFilTraffic
        {
            get { return this._urlFileTraffic; }
            set { this._urlFileTraffic = value; }
        }

        /// <summary>
        /// constructor
        /// </summary>
        public TraficoDocumento() 
        {
            this.objConfiguracion = new Configuracion();
            
            /// se carga el fileTraffic desde el web.config
            this._fileTraffic = this.objConfiguracion.FileTraffic;
            this._urlFileTraffic = this.objConfiguracion.UrlFileTraffic;
        }

        /// <summary>
        /// Método que recibe un archivo binario
        /// </summary>
        /// <param name="bteBytes">byte[] bytes del archivo</param>
        /// <param name="strNombre">string: nombre del archivo con el que se guardar en el servidor de archivos</param>
        /// <returns></returns>
        //public bool RecibirDocumento(byte[] bteBytes, string strNombreDocumento) 
        //{
        //    bool resultado = false;

        //    try 
        //    {
        //         System.IO.File.WriteAllBytes(strNombreDocumento, bteBytes);
        //    }
        //    catch(Exception e)
        //    {
        //        resultado = false;
        //    }
        //    return resultado;
        //}


        /// <summary>
        /// Método que recibe un listado de archivos binarios con sus nombres para ser guardados
        /// </summary>
        /// <param name="bteBytes">byte[] bytes del archivo</param>
        /// <param name="strNombre">string: nombre del archivo con el que se guardar en el servidor de archivos</param>
        /// <returns></returns>
        //public bool RecibirDocumento(List<byte[]> lstBytesDocumento, List<string> lstStrNombreDocumento)
        //{
        //    bool resultado = false;

        //    try
        //    {
        //        for(int i = 0; i<lstBytesDocumento.Count;i++)
        //        {
        //            System.IO.File.WriteAllBytes(lstStrNombreDocumento[i], lstBytesDocumento[i]);
        //        }
                
        //    }
        //    catch (Exception e)
        //    {
        //        resultado = false;
        //    }
        //    return resultado;
        //}



       /// <summary>
       /// Método que recibe un listado de documentos mediante un Xml 
       /// </summary>
       /// <param name="XmlDataDocumento">string: xml de datos</param>
       /// <returns></returns>
        //public bool RecibirDocumento(string strNumeroSilpa, string usuario, string objXmlDataDocumento, ref string ruta)
        //{
        //    /// 
        //    bool resultado = false;
        //    string dir = string.Empty;

        //    try
        //    {

        //        /// si el usuario es nulo entonces se toma por sancionatorio.
        //        if (usuario == null)
        //        {
        //            usuario = this.objConfiguracion.NombreQuejas;
        //            /// Se crea el directorio en el FileTraffic.
        //            dir = this.CrearDirectorio(this._fileTraffic, usuario, strNumeroSilpa);
        //        }
        //        else
        //        {
        //            /// Se crea el directorio en el FileTraffic.
        //            dir = this.CrearDirectorio(this._fileTraffic, strNumeroSilpa, usuario);
        //        }

        //        // deserializando el objeto ... 
        //        this._listaDocumentos = new ListaDocumentoAdjuntoType();
        //        this._listaDocumentos = (ListaDocumentoAdjuntoType)this._listaDocumentos.Deserializar(objXmlDataDocumento);

        //        /// Se crea el directorio en el FileTraffic.
        //        ///dir = this.CrearDirectorio(this._fileTraffic, strNumeroSilpa, usuario);

        //        //string fechaArchivo = Tools.RellenarCadena(DateTime.Now.Minute.ToString(), "0", 2, Orientacion.IZQUIERDA);
        //        string fechaArchivo = DateTime.Now.ToString("yyyyMMddmmss");
        //        //fechaArchivo = fechaArchivo + Tools.RellenarCadena(DateTime.Now.Second.ToString(), "0", 2, Orientacion.IZQUIERDA);

        //        /// retorna el path de guardado de los archivos
        //        //ruta = this._urlFileTraffic + @"/" + strNumeroSilpa + @"/" + usuario + @"/" + fechaArchivo;
        //        ruta = this._fileTraffic + @"\" + strNumeroSilpa + @"\" + usuario + @"\" + fechaArchivo;
                
        //        string nombreArchivo = string.Empty;
        //        string extension = string.Empty;


        //        foreach( documentoAdjuntoType lstDocumentos in this._listaDocumentos.ListaDocumento)
        //        {
        //            extension = System.IO.Path.GetExtension(lstDocumentos.nombreArchivo);
        //            nombreArchivo = System.IO.Path.GetFileNameWithoutExtension(lstDocumentos.nombreArchivo);
        //            nombreArchivo = dir + nombreArchivo + "_" + fechaArchivo + extension;

        //            System.IO.File.WriteAllBytes(nombreArchivo, lstDocumentos.bytes);
        //        }
        //        resultado = true;
        //    }
        //    catch (Exception e)
        //    {
        //        resultado = false;
        //    }
        //    return resultado;
        //}


        /// <summary>
        /// Método que recibe un listado de documentos mediante un Xml 
        /// </summary>
        /// <param name="XmlDataDocumento">string: xml dedatos</param>
        /// <returns></returns>
        //public bool RecibirDocumento(string strNumeroSilpa, string usuario, string objXmlDataDocumento)
        //{
        //    /// 
        //    bool resultado = false;
        //    string dir = string.Empty;

        //    try
        //    {

        //        /// si el usuario es nulo entonces se toma por sancionatorio.
        //        if (usuario == null)
        //        {
        //            usuario = this.objConfiguracion.NombreQuejas;
        //            /// Se crea el directorio en el FileTraffic.
        //            dir = this.CrearDirectorio(this._fileTraffic, usuario, strNumeroSilpa);
        //        }
        //        else
        //        {
        //            /// Se crea el directorio en el FileTraffic.
        //            dir = this.CrearDirectorio(this._fileTraffic, strNumeroSilpa, usuario);
        //        }

        //        // deserializando el objeto ... 
        //        this._listaDocumentos = new ListaDocumentoAdjuntoType();
        //        this._listaDocumentos = (ListaDocumentoAdjuntoType)this._listaDocumentos.Deserializar(objXmlDataDocumento);

        //        /// Se crea el directorio en el FileTraffic.
        //        //dir = this.CrearDirectorio(this._fileTraffic, strNumeroSilpa, usuario);

        //        //string fechaArchivo = Tools.RellenarCadena(DateTime.Now.Minute.ToString(), "0", 2, Orientacion.IZQUIERDA);
        //        string fechaArchivo = DateTime.Now.ToString("yyyyMMddmmss");
        //        //fechaArchivo = fechaArchivo + Tools.RellenarCadena(DateTime.Now.Second.ToString(), "0", 2, Orientacion.IZQUIERDA);

        //        string nombreArchivo = string.Empty;
        //        string extension = string.Empty;


        //        foreach (documentoAdjuntoType lstDocumentos in this._listaDocumentos.ListaDocumento)
        //        {
        //            extension = System.IO.Path.GetExtension(lstDocumentos.nombreArchivo);
        //            nombreArchivo = System.IO.Path.GetFileNameWithoutExtension(lstDocumentos.nombreArchivo);
        //            nombreArchivo = dir + nombreArchivo + "_" + fechaArchivo + extension;

        //            System.IO.File.WriteAllBytes(nombreArchivo, lstDocumentos.bytes);
        //        }

        //        resultado = true;
        //    }
        //    catch (Exception e)
        //    {
        //        resultado = false;
        //    }
        //    return resultado;
        //}



        /// <summary>
        /// Método que recibe los documentos adjuntos y los envía al FileTraffic
        /// </summary>
        /// <param name="strRaiz">string: raíz del fileTraffic</param>
        /// <param name="strNumeroSilpa">string: Número silpa como parte del primer nivel</param>
        /// <param name="usuario">string: se usa el usuario como segundo nivel de carpeta en la creación del filetraffic</param>
        /// <param name="lstBytesDocumento">Lst<Byte[]>Listado de bytes de los documentos</param>
        /// <param name="lstStrNombreDocumento">string[]: listado de nombres de los archivos adjuntos</param>
        /// <returns>bool: true /false del estado de la ejecución de la operación</returns>
        //public bool RecibirDocumento(string strNumeroSilpa, string usuario, 
        //                            List<byte[]> lstBytesDocumento, List<string> lstStrNombreDocumento)
        //{
        //    /// 
        //    bool resultado = false;
        //    string dir = string.Empty;

        //    try
        //    {
        //        /// si el usuario es nulo entonces se toma por sancionatorio.
        //        if (usuario == null)
        //        {
        //            usuario = this.objConfiguracion.NombreQuejas;
        //            /// Se crea el directorio en el FileTraffic.
        //            dir = this.CrearDirectorio(this._fileTraffic, usuario, strNumeroSilpa);
        //        }
        //        else 
        //        {
        //            /// Se crea el directorio en el FileTraffic.
        //            dir = this.CrearDirectorio(this._fileTraffic, strNumeroSilpa, usuario);
        //        }


        //        //string fechaArchivo = Tools.RellenarCadena(DateTime.Now.Minute.ToString(), "0", 2, Orientacion.IZQUIERDA);
        //        //fechaArchivo = fechaArchivo + Tools.RellenarCadena(DateTime.Now.Second.ToString(), "0", 2, Orientacion.IZQUIERDA);

        //        string fechaArchivo = DateTime.Now.ToString("yyyyMMddmmss");

        //        string nombreArchivo = string.Empty;
        //        string extension = string.Empty;

        //        for (int i = 0; i < lstBytesDocumento.Count; i++)
        //        {
        //            extension = System.IO.Path.GetExtension(lstStrNombreDocumento[i]);
        //            nombreArchivo = System.IO.Path.GetFileNameWithoutExtension(lstStrNombreDocumento[i]);
        //            nombreArchivo = dir + nombreArchivo + "_" + fechaArchivo + extension;

        //            System.IO.File.WriteAllBytes(nombreArchivo, lstBytesDocumento[i]);
        //        }
        //        resultado = true;
        //    }
        //    catch (Exception e)
        //    {
        //        resultado = false;
        //    }

        //    return resultado;
        //}




        /// <summary>
        /// Método que recibe los documentos adjuntos y los envía al FileTraffic
        /// </summary>
        /// <param name="strRaiz">string: raíz del fileTraffic</param>
        /// <param name="strNumeroSilpa">string: Número silpa como parte del primer nivel</param>
        /// <param name="usuario">string: se usa el usuario como segundo nivel de carpeta en la creación del filetraffic</param>
        /// <param name="lstBytesDocumento">Lst<Byte[]>Listado de bytes de los documentos</param>
        /// <param name="lstStrNombreDocumento">string[]: listado de nombres de los archivos adjuntos</param>
        /// <returns>bool: true /false del estado de la ejecución de la operación</returns>
        public bool RecibirDocumento(string strNumeroSilpa, 
                                     string usuario,
                                     List<byte[]> lstBytesDocumento, 
                                     ref List<string> lstStrNombreDocumento, 
                                     ref string ruta)
        {
            //SMLog.Escribir(Severidad.Informativo, "FileTraffic: Inicio de RecibirDocumento");     
            bool resultado = false;
            string dir = string.Empty;
            
            try
            {
                // si el usuario es nulo entonces se toma por sancionatorio.
                if (usuario == null)
                {
                    usuario = this.objConfiguracion.NombreQuejas;
                }

                // Se crea el directorio en el FileTraffic.
                //SMLog.Escribir(Severidad.Informativo, "FileTraffic: strNumeroSilpa " + strNumeroSilpa);
                //SMLog.Escribir(Severidad.Informativo, "FileTraffic: usuario " + usuario);     
                dir = this.CrearDirectorio(this._fileTraffic, strNumeroSilpa, usuario);
                //SMLog.Escribir(Severidad.Informativo, "FileTraffic: Direccion del archivo " + dir);     

                string fechaArchivo = DateTime.Now.ToString("yyyyMMddhhmmss");

                ruta = dir;

                string nombreArchivo = string.Empty;
                string extension = string.Empty;

                //for (int i = 0; i < lstBytesDocumento.Count; i++)
                for (int i = 0; i < lstStrNombreDocumento.Count; i++)
                {
                    string archivo = "";
                    if (lstStrNombreDocumento[i].Contains(";"))
                    {
                        string[] archivoTypeControl = lstStrNombreDocumento[i].Split(';');
                        archivo = archivoTypeControl[0];
                    }
                    else
                    {
                        archivo = lstStrNombreDocumento[i];
                    }
                    extension = System.IO.Path.GetExtension(archivo);
                    nombreArchivo = System.IO.Path.GetFileNameWithoutExtension(archivo);
                    nombreArchivo = dir + nombreArchivo + "_" + fechaArchivo + extension;

                    lstStrNombreDocumento[i] = nombreArchivo;
                    //SMLog.Escribir(Severidad.Informativo, "FileTraffic: lstStrNombreDocumento[i] " + lstStrNombreDocumento[i]);    
                    if (System.IO.File.Exists(nombreArchivo))
                    {
                        //------------------- INICIO DE ESCRITURA TEMPORAL (ELIMINAR) -------------------------
                        try
                        {
                            System.IO.File.Delete(nombreArchivo);
                        }
                        catch (Exception ex)
                        {
                            throw new ApplicationException("Error al tratar de eliminar el archivo " + nombreArchivo + " " + ex.ToString(), ex); 
                            //using (System.IO.StreamWriter archivo = new System.IO.StreamWriter(@"E:\VITAL\logInfo.txt", true))
                            //{
                            //    archivo.WriteLine("Error al tratar de eliminar el archivo " + nombreArchivo + " " + ex.ToString());
                            //}
                            return false; 
                        }
                        //----- FIN DE ESCRITURA --------------------------------------------------------------
                    }
                    System.IO.File.WriteAllBytes(nombreArchivo, lstBytesDocumento[i]);
                }
                resultado = true;
            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Informativo, "FileTraffic: Error" + ex.Message);
                
                string strException = "Validar los pasos efectuados al recibir los documentos adjuntos y enviarlos al FileTraffic.";
                throw new Exception(strException, ex);

                resultado = false;
            }

            return resultado;
        }



     
        public bool RecibirDocumento2(string strNumeroSilpa, string usuario,string rutaBPM,ref List<string> lstStrNombreDocumento,ref string ruta)
        {
            /// 
            //SMLog.Escribir(Severidad.Informativo, "FileTraffic: Inicio de RecibirDocumento");     
            bool resultado = false;
            string dir = string.Empty;

            SMLog.Escribir(Severidad.Informativo, "Entro a RecibirDocumento2-->");
            

            try
            {
                /// si el usuario es nulo entonces se toma por sancionatorio.
                if (usuario == null)
                {

                    usuario = this.objConfiguracion.NombreQuejas;
                }

                /// Se crea el directorio en el FileTraffic.
                //SMLog.Escribir(Severidad.Informativo, "FileTraffic: strNumeroSilpa " + strNumeroSilpa);
                //SMLog.Escribir(Severidad.Informativo, "FileTraffic: usuario " + usuario);     
                dir = this.CrearDirectorio(this._fileTraffic, strNumeroSilpa, usuario);
                //SMLog.Escribir(Severidad.Informativo, "FileTraffic: Direccion del archivo " + dir);     

                string fechaArchivo = DateTime.Now.ToString("yyyyMMddhhmmss");

                ruta = dir;

                SMLog.Escribir(Severidad.Informativo, "lstStrNombreDocumento -->" + lstStrNombreDocumento.Count);

                string nombreArchivo = string.Empty;
                string nombreArchivoBPM = string.Empty;
                string extension = string.Empty;

                // ELimina de la lista los archivos que no existan
                for (int i = lstStrNombreDocumento.Count - 1; i >= 0; i--)
                {
                    string archivo = "";
                    if (lstStrNombreDocumento[i].Contains(";"))
                    {
                        string[] archivoTypeControl = lstStrNombreDocumento[i].Split(';');
                        archivo = archivoTypeControl[0];
                    }
                    else
                    {
                        archivo = lstStrNombreDocumento[i];
                    }

                    archivo = rutaBPM + archivo;
                    if (!System.IO.File.Exists(archivo))
                    {
                        lstStrNombreDocumento.RemoveAt(i);
                    }
                    
                }

                //for (int i = 0; i < lstBytesDocumento.Count; i++)
                for (int i = 0; i < lstStrNombreDocumento.Count; i++)
                {

                    string archivo = "";
                    if (lstStrNombreDocumento[i].Contains(";"))
                    {
                        string[] archivoTypeControl = lstStrNombreDocumento[i].Split(';');
                        archivo = archivoTypeControl[0];
                    }
                    else
                    {
                        archivo = lstStrNombreDocumento[i];
                    }
                    extension = System.IO.Path.GetExtension(archivo);
                    nombreArchivo = System.IO.Path.GetFileNameWithoutExtension(archivo);

                    SMLog.Escribir(Severidad.Informativo, "nombreArchivo -->" + nombreArchivo + extension);
                  
                    nombreArchivoBPM = rutaBPM + nombreArchivo + extension;
                    nombreArchivo = dir + nombreArchivo + "_" + fechaArchivo + extension;
                   
                    lstStrNombreDocumento[i] = nombreArchivo;

                    //SMLog.Escribir(Severidad.Informativo, "FileTraffic: lstStrNombreDocumento[i] " + lstStrNombreDocumento[i]);    
                    if (System.IO.File.Exists(nombreArchivo))
                    {
                        //------------------- INICIO DE ESCRITURA TEMPORAL (ELIMINAR) -------------------------
                        try
                        {

                            System.IO.File.Delete(nombreArchivo);
                        }
                        catch (Exception ex)
                        {
                            throw new ApplicationException("Error al tratar de eliminar el archivo " + nombreArchivo + " " + ex.ToString(), ex);
                            //using (System.IO.StreamWriter archivo = new System.IO.StreamWriter(@"E:\VITAL\logInfo.txt", true))
                            //{
                            //    archivo.WriteLine("Error al tratar de eliminar el archivo " + nombreArchivo + " " + ex.ToString());
                            //}
                            return false;
                        }

                        //----- FIN DE ESCRITURA --------------------------------------------------------------
                    }

                    SMLog.Escribir(Severidad.Informativo, "nombreArchivoBPM -->" + nombreArchivoBPM);

                    SMLog.Escribir(Severidad.Informativo, "nombreArchivoDestino -->" + nombreArchivo);

                    FileInfo archivoBPM = new FileInfo(nombreArchivoBPM);
                    //archivoBPM.MoveTo(nombreArchivo);
                    archivoBPM.CopyTo(nombreArchivo);
                    //System.IO.File.Copy(nombreArchivoBPM, nombreArchivo, true);

                    //System.IO.File.WriteAllBytes(nombreArchivo, lstBytesDocumento[i]);
                }
                resultado = true;

            }
            catch (Exception e)
            {
                SMLog.Escribir(Severidad.Informativo, "FileTraffic: Error" + e.Message);
                resultado = false;
            }

            return resultado;
        }
        public bool RecibirDocumento(string strNumeroSilpa, string usuario, string rutaBPM, string rutaRepoUsuario, ref List<string> lstStrNombreDocumento, ref string ruta)
        {
            /// 
            //SMLog.Escribir(Severidad.Informativo, "FileTraffic: Inicio de RecibirDocumento");     
            bool resultado = false;
            string dir = string.Empty;

            SMLog.Escribir(Severidad.Informativo, "Entro a RecibirDocumento2-->");


            try
            {
                /// si el usuario es nulo entonces se toma por sancionatorio.
                if (usuario == null)
                {

                    usuario = this.objConfiguracion.NombreQuejas;
                }

                /// Se crea el directorio en el FileTraffic.
                //SMLog.Escribir(Severidad.Informativo, "FileTraffic: strNumeroSilpa " + strNumeroSilpa);
                //SMLog.Escribir(Severidad.Informativo, "FileTraffic: usuario " + usuario);     
                dir = this.CrearDirectorio(this._fileTraffic, strNumeroSilpa, usuario);
                //SMLog.Escribir(Severidad.Informativo, "FileTraffic: Direccion del archivo " + dir);     

                string fechaArchivo = DateTime.Now.ToString("yyyyMMddhhmmss");

                ruta = dir;

                SMLog.Escribir(Severidad.Informativo, "lstStrNombreDocumento -->" + lstStrNombreDocumento.Count);

                string nombreArchivo = string.Empty;
                string nombreArchivoBPM = string.Empty;
                string extension = string.Empty;

                // ELimina de la lista los archivos que no existan
                for (int i = lstStrNombreDocumento.Count - 1; i >= 0; i--)
                {
                    string archivo = "";
                    string type = "";
                    if (lstStrNombreDocumento[i].Contains(";"))
                    {
                        string[] archivoTypeControl = lstStrNombreDocumento[i].Split(';');
                        archivo = archivoTypeControl[0];
                        type = archivoTypeControl[1];
                    }
                    else
                    {
                        archivo = lstStrNombreDocumento[i];
                    }
                    if (type == "FileUpLoad")
                        archivo = rutaBPM + archivo;
                    else if (type == "DropDownList")
                        archivo = rutaRepoUsuario + archivo;

                    SMLog.Escribir(Severidad.Informativo, "Origen Archivo -->" + archivo + ", Tipo:" + type);
                    if (!System.IO.File.Exists(archivo))
                    {
                        lstStrNombreDocumento.RemoveAt(i);
                    }
                }
                //for (int i = 0; i < lstBytesDocumento.Count; i++)
                for (int i = 0; i < lstStrNombreDocumento.Count; i++)
                {
                    string[] archivoTypeControl = lstStrNombreDocumento[i].Split(';');
                    string archivo = archivoTypeControl[0];
                    string type = archivoTypeControl[1];
                    extension = System.IO.Path.GetExtension(archivo);
                    nombreArchivo = System.IO.Path.GetFileNameWithoutExtension(archivo);

                    SMLog.Escribir(Severidad.Informativo, "nombreArchivo -->" + nombreArchivo + extension);

                    if (type == "FileUpLoad")
                        nombreArchivoBPM = rutaBPM + nombreArchivo + extension;
                    else if (type == "DropDownList")
                        nombreArchivoBPM = rutaRepoUsuario + nombreArchivo + extension;
                    else
                        nombreArchivoBPM = rutaBPM + nombreArchivo + extension;
                    nombreArchivo = dir + nombreArchivo + "_" + fechaArchivo + extension;
                    

                    lstStrNombreDocumento[i] = nombreArchivo;

                    //SMLog.Escribir(Severidad.Informativo, "FileTraffic: lstStrNombreDocumento[i] " + lstStrNombreDocumento[i]);    
                    if (System.IO.File.Exists(nombreArchivo))
                    {
                        //------------------- INICIO DE ESCRITURA TEMPORAL (ELIMINAR) -------------------------
                        try
                        {
                            System.IO.File.Delete(nombreArchivo);
                        }
                        catch (Exception ex)
                        {
                            throw new ApplicationException("Error al tratar de eliminar el archivo " + nombreArchivo + " " + ex.ToString(), ex);
                            //using (System.IO.StreamWriter archivo = new System.IO.StreamWriter(@"E:\VITAL\logInfo.txt", true))
                            //{
                            //    archivo.WriteLine("Error al tratar de eliminar el archivo " + nombreArchivo + " " + ex.ToString());
                            //}
                            return false;
                        }

                        //----- FIN DE ESCRITURA --------------------------------------------------------------
                    }

                    SMLog.Escribir(Severidad.Informativo, "nombreArchivoBPM -->" + nombreArchivoBPM);

                    SMLog.Escribir(Severidad.Informativo, "nombreArchivoDestino -->" + nombreArchivo);

                    FileInfo archivoBPM = new FileInfo(nombreArchivoBPM);

                    //jmartinez 22-05-2018 realizamos una copia del archivo y se remonbra el original en gattaca para verificar que se haya copiado
                    //archivoBPM.MoveTo(nombreArchivo);
                    archivoBPM.CopyTo(nombreArchivo);
                    



                }
                resultado = true;

            }
            catch (Exception e)
            {
                SMLog.Escribir(Severidad.Informativo, "FileTraffic: Error" + e.Message);
                resultado = false;
            }

            return resultado;
        }

        /*
        public bool RecibirDocumentoRecurso(string ruta,
                                    List<byte[]> lstBytesDocumento,
                                    ref List<string> lstStrNombreDocumento)
        {
            /// 
            //SMLog.Escribir(Severidad.Informativo, "FileTraffic: Inicio de RecibirDocumento");     
            bool resultado = false;
            
            try
            {
                string fechaArchivo = DateTime.Now.ToString("yyyyMMddhhmmss");


                string nombreArchivo = string.Empty;
                string extension = string.Empty;

                //for (int i = 0; i < lstBytesDocumento.Count; i++)
                for (int i = 0; i < lstStrNombreDocumento.Count; i++)
                {
                    extension = System.IO.Path.GetExtension(lstStrNombreDocumento[i]);
                    nombreArchivo = System.IO.Path.GetFileNameWithoutExtension(lstStrNombreDocumento[i]);
                    nombreArchivo = ruta + nombreArchivo + "_" + fechaArchivo + extension;

                    lstStrNombreDocumento[i] = nombreArchivo;
                    //SMLog.Escribir(Severidad.Informativo, "FileTraffic: lstStrNombreDocumento[i] " + lstStrNombreDocumento[i]);    
                    if (System.IO.File.Exists(nombreArchivo))
                    {
                        //------------------- INICIO DE ESCRITURA TEMPORAL (ELIMINAR) -------------------------
                        try
                        {

                            System.IO.File.Delete(nombreArchivo);
                        }
                        catch (Exception ex)
                        {
                            throw new ApplicationException("Error al tratar de eliminar el archivo " + nombreArchivo + " " + ex.ToString(), ex);
                            //using (System.IO.StreamWriter archivo = new System.IO.StreamWriter(@"E:\VITAL\logInfo.txt", true))
                            //{
                            //    archivo.WriteLine("Error al tratar de eliminar el archivo " + nombreArchivo + " " + ex.ToString());
                            //}
                            return false;
                        }

                        //----- FIN DE ESCRITURA --------------------------------------------------------------
                    }
                    System.IO.File.WriteAllBytes(nombreArchivo, lstBytesDocumento[i]);
                }
                resultado = true;

            }
            catch (Exception e)
            {
                SMLog.Escribir(Severidad.Informativo, "FileTraffic: Error" + e.Message);
                resultado = false;
            }

            return resultado;
        }

        */
        /*
        public bool RecibirDocumentoRecurso2(string ruta,
                                 string rutaBPM,
                                 ref List<string> lstStrNombreDocumento)
        {
            /// 
            //SMLog.Escribir(Severidad.Informativo, "FileTraffic: Inicio de RecibirDocumento");     
            bool resultado = false;
            SMLog.Escribir(Severidad.Informativo, "Entro a RecibirDocumentoRecurso2-->");
            try
            {
                string fechaArchivo = DateTime.Now.ToString("yyyyMMddhhmmss");


                string nombreArchivo = string.Empty;
                string extension = string.Empty;
                string nombreArchivoBPM = string.Empty;

                // ELimina de la lista los archivos que no existan
                for (int i = lstStrNombreDocumento.Count - 1; i >= 0; i--)
                {
                    var archivo = rutaBPM + lstStrNombreDocumento[i];
                    if (!System.IO.File.Exists(archivo))
                    {
                        lstStrNombreDocumento.RemoveAt(i);
                    }

                }

                //for (int i = 0; i < lstBytesDocumento.Count; i++)
                for (int i = 0; i < lstStrNombreDocumento.Count; i++)
                {
                    extension = System.IO.Path.GetExtension(lstStrNombreDocumento[i]);
                    nombreArchivo = System.IO.Path.GetFileNameWithoutExtension(lstStrNombreDocumento[i]);

                    SMLog.Escribir(Severidad.Informativo, "nombreArchivo -->" + nombreArchivo + extension);
                    nombreArchivoBPM = rutaBPM + nombreArchivo + extension;
                    nombreArchivo = ruta + nombreArchivo + "_" + fechaArchivo + extension;

                    lstStrNombreDocumento[i] = nombreArchivo;
                    //SMLog.Escribir(Severidad.Informativo, "FileTraffic: lstStrNombreDocumento[i] " + lstStrNombreDocumento[i]);    
                    if (System.IO.File.Exists(nombreArchivo))
                    {
                        //------------------- INICIO DE ESCRITURA TEMPORAL (ELIMINAR) -------------------------
                        try
                        {

                            System.IO.File.Delete(nombreArchivo);
                        }
                        catch (Exception ex)
                        {
                            throw new ApplicationException("Error al tratar de eliminar el archivo " + nombreArchivo + " " + ex.ToString(), ex);
                            //using (System.IO.StreamWriter archivo = new System.IO.StreamWriter(@"E:\VITAL\logInfo.txt", true))
                            //{
                            //    archivo.WriteLine("Error al tratar de eliminar el archivo " + nombreArchivo + " " + ex.ToString());
                            //}
                            return false;
                        }

                        //----- FIN DE ESCRITURA --------------------------------------------------------------
                    }

                    SMLog.Escribir(Severidad.Informativo, "nombreArchivoBPM -->" + nombreArchivoBPM);

                    SMLog.Escribir(Severidad.Informativo, "nombreArchivoDestino -->" + nombreArchivo);

                    System.IO.File.Copy(nombreArchivoBPM, nombreArchivo, true);

           
                    //System.IO.File.WriteAllBytes(nombreArchivo, lstBytesDocumento[i]);
                }
                resultado = true;

            }
            catch (Exception e)
            {
                SMLog.Escribir(Severidad.Informativo, "FileTraffic: Error" + e.Message);
                resultado = false;
            }

            return resultado;
        }

        */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentos"></param>
        /// <param name="ruta"></param>
        /// <returns></returns>
        public bool RecibirDocumentoReenvio(documentoAdjuntoType[] documentos, string ruta)
        {
            try
            {
	            bool result = false;
	            List<byte[]> lstBytesDocumento = new List<byte[]>();
	            List<string> lstStrNombreDocumento = new List<string>();
	
	            if(!System.IO.Directory.Exists(ruta)){
	                System.IO.Directory.CreateDirectory(ruta);
	            }   
				
	            foreach (documentoAdjuntoType adj in documentos)
	            {
	                lstBytesDocumento.Add(adj.bytes);
	                lstStrNombreDocumento.Add(ruta+@"\"+adj.nombreArchivo);
	            }
				
	            for (int i = 0; i < lstStrNombreDocumento.Count; i++)
	            {
	                string nombreArchivo = lstStrNombreDocumento[i];
	                //SMLog.Escribir(Severidad.Informativo, "FileTraffic: lstStrNombreDocumento[i] " + lstStrNombreDocumento[i]);    
	                if (System.IO.File.Exists(nombreArchivo))
	                {
	                    //------------------- INICIO DE ESCRITURA TEMPORAL (ELIMINAR) -------------------------
	                    try
	                    {
	                        System.IO.File.Delete(nombreArchivo);
	                        result = true;
	                    }
	                    catch (Exception ex)
	                    {
	                        throw new ApplicationException("Error al tratar de eliminar el archivo " + nombreArchivo + " " + ex.ToString(), ex);
	                        //using (System.IO.StreamWriter archivo = new System.IO.StreamWriter(@"E:\VITAL\logInfo.txt", true))
	                        //{
	                        //    archivo.WriteLine("Error al tratar de eliminar el archivo " + nombreArchivo + " " + ex.ToString());
	                        //}
	                        result = false;
	                    }
	                    //----- FIN DE ESCRITURA --------------------------------------------------------------
	                }
	                System.IO.File.WriteAllBytes(nombreArchivo, lstBytesDocumento[i]);
	            }
	            return result;
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Recibir Documento Reenvío.";
                throw new Exception(strException, ex);
            }
        }


        /*
        /// <summary>
        /// Método que recibe los documentos adjuntos y los envía al FileTraffic
        /// </summary>
        /// <param name="strRaiz">string: raíz del fileTraffic</param>
        /// <param name="strNumeroSilpa">string: Número silpa como parte del primer nivel</param>
        /// <param name="usuario">string: se usa el usuario como segundo nivel de carpeta en la creación del filetraffic</param>
        /// <param name="lstBytesDocumento">Lst<Byte[]>Listado de bytes de los documentos</param>
        /// <param name="lstStrNombreDocumento">string[]: listado de nombres de los archivos adjuntos</param>
        /// <param name="idAAdestino">int: identificador de la autoridad o EE destino </param>
        /// <returns>bool: true /false del estado de la ejecución de la operación</returns>
        public bool RecibirDocumentoEE(string strNumeroSilpa, string usuario,
                                    List<byte[]> lstBytesDocumento,
                                    ref List<string> lstStrNombreDocumento,
                            ref string ruta, int _idAADestino
            )
        {
            /// 
            //SMLog.Escribir(Severidad.Informativo, "FileTraffic: Inicio de RecibirDocumento");     
            bool resultado = false;
            string dir = string.Empty;

            try
            {
                /// si el usuario es nulo entonces se toma por sancionatorio.
                if (usuario == null)
                {

                    usuario = this.objConfiguracion.NombreQuejas;
                }

                /// Se crea el directorio en el FileTraffic.
                //SMLog.Escribir(Severidad.Informativo, "FileTraffic: strNumeroSilpa " + strNumeroSilpa);
                //SMLog.Escribir(Severidad.Informativo, "FileTraffic: usuario " + usuario);     

                // Se compone el usuario mas el identificador de la autoridad ambiental
                usuario = usuario +"_" +_idAADestino.ToString();

                dir = this.CrearDirectorio(this._fileTraffic, strNumeroSilpa, usuario);
                //SMLog.Escribir(Severidad.Informativo, "FileTraffic: Direccion del archivo " + dir);     

                string fechaArchivo = DateTime.Now.ToString("yyyyMMddhhmmss");

                ruta = dir;

                string nombreArchivo = string.Empty;
                string extension = string.Empty;

                for (int i = 0; i < lstStrNombreDocumento.Count; i++)
                {
                    extension = System.IO.Path.GetExtension(lstStrNombreDocumento[i]);
                    nombreArchivo = System.IO.Path.GetFileNameWithoutExtension(lstStrNombreDocumento[i]);
                    nombreArchivo = dir + nombreArchivo + "_" + fechaArchivo + extension;

                    lstStrNombreDocumento[i] = nombreArchivo;
                    //SMLog.Escribir(Severidad.Informativo, "FileTraffic: lstStrNombreDocumento[i] " + lstStrNombreDocumento[i]);    
                    if (System.IO.File.Exists(nombreArchivo))
                    {
                        //------------------- INICIO DE ESCRITURA TEMPORAL (ELIMINAR) -------------------------
                        try
                        {

                            System.IO.File.Delete(nombreArchivo);
                        }
                        catch (Exception ex)
                        {
                            throw new ApplicationException("Error al tratar de eliminar el archivo " + nombreArchivo + " " + ex.ToString(), ex);
                            //using (System.IO.StreamWriter archivo = new System.IO.StreamWriter(@"E:\VITAL\logInfo.txt", true))
                            //{
                            //    archivo.WriteLine("Error al tratar de eliminar el archivo " + nombreArchivo + " " + ex.ToString());
                            //}
                            return false;
                        }

                        //----- FIN DE ESCRITURA --------------------------------------------------------------
                    }
                    System.IO.File.WriteAllBytes(nombreArchivo, lstBytesDocumento[i]);
                }
                resultado = true;

            }
            catch (Exception e)
            {
                SMLog.Escribir(Severidad.Informativo, "FileTraffic: Error" + e.Message);
                resultado = false;
            }

            return resultado;
        }

        */
        /// <summary>
        /// Método que recibe los documentos adjuntos y los envía al FileTraffic
        /// </summary>
        /// <param name="strRaiz">string: raíz del fileTraffic</param>
        /// <param name="strNumeroSilpa">string: Número silpa como parte del primer nivel</param>
        /// <param name="usuario">string: se usa el usuario como segundo nivel de carpeta en la creación del filetraffic</param>
        /// <param name="lstBytesDocumento">Lst<Byte[]>Listado de bytes de los documentos</param>
        /// <param name="lstStrNombreDocumento">string[]: listado de nombres de los archivos adjuntos</param>
        /// <param name="idAAdestino">int: identificador de la autoridad o EE destino </param>
        /// <returns>bool: true /false del estado de la ejecución de la operación</returns>
        public bool RecibirDocumentoEE2(string strNumeroSilpa, string usuario,
                                    string rutaBPM,
                                    ref List<string> lstStrNombreDocumento,
                            ref string ruta, int _idAADestino
            )
        {
            /// 
            //SMLog.Escribir(Severidad.Informativo, "FileTraffic: Inicio de RecibirDocumento");     
            bool resultado = false;
            string dir = string.Empty;
            SMLog.Escribir(Severidad.Informativo, "Entro a  RecibirDocumentoEE2-->");
            try
            {
                /// si el usuario es nulo entonces se toma por sancionatorio.
                if (usuario == null)
                {

                    usuario = this.objConfiguracion.NombreQuejas;
                }

                SMLog.Escribir(Severidad.Informativo, "lstStrNombreDocumento -->" + lstStrNombreDocumento.Count);


                /// Se crea el directorio en el FileTraffic.
                //SMLog.Escribir(Severidad.Informativo, "FileTraffic: strNumeroSilpa " + strNumeroSilpa);
                //SMLog.Escribir(Severidad.Informativo, "FileTraffic: usuario " + usuario);     

                // Se compone el usuario mas el identificador de la autoridad ambiental
                usuario = usuario + "_" + _idAADestino.ToString();

                dir = this.CrearDirectorio(this._fileTraffic, strNumeroSilpa, usuario);
                //SMLog.Escribir(Severidad.Informativo, "FileTraffic: Direccion del archivo " + dir);     

                string fechaArchivo = DateTime.Now.ToString("yyyyMMddhhmmss");

                ruta = dir;

                string nombreArchivo = string.Empty;
                string extension = string.Empty;
                string nombreArchivoBPM = string.Empty;

                // ELimina de la lista los archivos que no existan
                for (int i = lstStrNombreDocumento.Count - 1; i >= 0; i--)
                {
                    var archivo = rutaBPM + lstStrNombreDocumento[i];
                    if (!System.IO.File.Exists(archivo))
                    {
                        lstStrNombreDocumento.RemoveAt(i);
                    }

                }

                for (int i = 0; i < lstStrNombreDocumento.Count; i++)
                {
                    extension = System.IO.Path.GetExtension(lstStrNombreDocumento[i]);
                    nombreArchivo = System.IO.Path.GetFileNameWithoutExtension(lstStrNombreDocumento[i]);
                    SMLog.Escribir(Severidad.Informativo, "nombreArchivo -->" + nombreArchivo + extension);
                    nombreArchivoBPM = rutaBPM + nombreArchivo + extension;
                    nombreArchivo = dir + nombreArchivo + "_" + fechaArchivo + extension;
                   
                    lstStrNombreDocumento[i] = nombreArchivo;
                    //SMLog.Escribir(Severidad.Informativo, "FileTraffic: lstStrNombreDocumento[i] " + lstStrNombreDocumento[i]);    
                    if (System.IO.File.Exists(nombreArchivo))
                    {
                        //------------------- INICIO DE ESCRITURA TEMPORAL (ELIMINAR) -------------------------
                        try
                        {

                            System.IO.File.Delete(nombreArchivo);
                        }
                        catch (Exception ex)
                        {
                            throw new ApplicationException("Error al tratar de eliminar el archivo " + nombreArchivo + " " + ex.ToString(), ex);
                            //using (System.IO.StreamWriter archivo = new System.IO.StreamWriter(@"E:\VITAL\logInfo.txt", true))
                            //{
                            //    archivo.WriteLine("Error al tratar de eliminar el archivo " + nombreArchivo + " " + ex.ToString());
                            //}
                            return false;
                        }

                        //----- FIN DE ESCRITURA --------------------------------------------------------------
                    }
                    SMLog.Escribir(Severidad.Informativo, "nombreArchivoBPM -->" + nombreArchivoBPM);

                    SMLog.Escribir(Severidad.Informativo, "nombreArchivoDestino -->" + nombreArchivo);
                 
                    System.IO.File.Copy(nombreArchivoBPM, nombreArchivo, true);

                      //System.IO.File.WriteAllBytes(nombreArchivo, lstBytesDocumento[i]);
                }
                resultado = true;

            }
            catch (Exception e)
            {
                SMLog.Escribir(Severidad.Informativo, "FileTraffic: Error" + e.Message);
                resultado = false;
            }

            return resultado;
        }


        /// <summary>
        /// Método para enviar documentos 
        /// desde el servidor de la aplicación
        /// </summary>
        /// <returns></returns>
        public bool EnviarDocumento() 
        {
            bool resultado = false;
            return resultado;
        }


        /// <summary>
        ///  Método para cargar los bytes de los archivos
        /// </summary>
        /// <param name="pathDocumento"></param>
        public void LeerDocumento(string pathDocumento) 
        {
            this.Bytes = System.IO.File.ReadAllBytes(pathDocumento);
        }


        /// <summary>
        /// Método para guardar el archivo
        /// </summary>
        public void SalvarDocumento(string nombreDocumento) 
        {
            System.IO.File.WriteAllBytes(nombreDocumento, this.Bytes);
        }

        /// <summary>
        /// Método que construye el directorio par Filetraffic
        /// </summary>
        /// <param name="strRaiz">string: raiz del directortio ( variable FileTraffic del web.config )</param>
        /// <param name="strPrimerNivel">string: Carpeta de primer nivel ( número silpa del tramite: AAAAMMDDXXX) </param>
        /// <param name="strSegundoNivel">string: carpeta de segundo nivel (se usa el usuario )</param>
        /// <returns>string: directorio creado</returns>
        public string CrearDirectorio(string strRaiz, string strPrimerNivel, string strSegundoNivel)
        {
            /// Se construye el directorio:
            string fecha;// = DateTime.Now.Year.ToString();

            //fecha = DateTime.Now.ToString("yyyyMMdd");
            fecha = DateTime.Now.ToString("yyyyMMddhhmmss");

            string directorio = strRaiz + fecha + @"\" + strPrimerNivel + @"\" + strSegundoNivel + @"\";


            //System.IO.Directory.
            try
            {
                if (!System.IO.Directory.Exists(directorio))
                {
                    System.IO.Directory.CreateDirectory(directorio);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al crear el directorio " + directorio + " " + ex.ToString(), ex); 
            }
            
            return directorio;
        }

        public string CrearDirectorioNUR(string strRaiz, string strCarpetaNURs, string NUR)
        {
            string directorio = strRaiz + @"\" + strCarpetaNURs + @"\" + NUR + @"\";
            try
            {
                if (!System.IO.Directory.Exists(directorio))
                {
                    System.IO.Directory.CreateDirectory(directorio);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al crear el directorio " + directorio + " " + ex.ToString(), ex);
            }

            return directorio;
        }

        /// <summary>
        /// Método que permite listar los documentos incluidos en un directorio .. 
        /// </summary>
        /// <returns> List[string]: listado de los archvios en el directorio </returns>
        public List<string> ListarDocumentosDirectorio(string strPath) 
        {
            List<string> result = null;
            if (System.IO.Directory.Exists(strPath))
            {
                result = new List<string>();
                result.AddRange(System.IO.Directory.GetFiles(strPath));
            }
            return result;
        }




        
        
    }


    #region Clases para documentos adjuntos ...

    /*
     * clases que permiten la carga de listados de archivos binarios para enviar mediante el 
     * servicio web Filetraffic
     */
    /// <comentarios/>
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    //[System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/Publicacion.xsd")]
    //[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://tempuri.org/Publicacion.xsd", IsNullable = false)]
    public partial class ListaDocumentoAdjuntoType : EntidadSerializable
    {

        private documentoAdjuntoType[] listaDocumentoField;

        /// <comentarios/>
       // [System.Xml.Serialization.XmlElementAttribute("ListaDocumento")]
        public documentoAdjuntoType[] ListaDocumento
        {
            get
            {
                return this.listaDocumentoField;
            }
            set
            {
                this.listaDocumentoField = value;
            }
        }
    }

    ///// <comentarios/>
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    //[System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/Publicacion.xsd")]
    public partial class documentoAdjuntoType : EntidadSerializable
    {

        private string nombreArchivoField;

        private byte[] bytesField;

        /// <comentarios/>
        public string nombreArchivo
        {
            get
            {
                return this.nombreArchivoField;
            }
            set
            {
                this.nombreArchivoField = value;
            }
        }

        /// <comentarios/>
        public byte[] bytes
        {
            get
            {
                return this.bytesField;
            }
            set
            {
                this.bytesField = value;
            }
        }
    }

    #endregion

}
