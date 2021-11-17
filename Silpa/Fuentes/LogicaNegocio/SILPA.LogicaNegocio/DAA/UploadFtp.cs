using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using SoftManagement.Log;

namespace SILPA.LogicaNegocio.DAA
{
    public class UploadFtp
    {

        public bool ExisteCarpetaFtp(string RemotePath, string Login, string Password)
        {
            bool bExiste = true;

            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(RemotePath);
                request.Credentials = new NetworkCredential(Login, Password);
                request.Method = WebRequestMethods.Ftp.ListDirectory;

                FtpWebResponse respuesta = (FtpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    FtpWebResponse respuesta = (FtpWebResponse)ex.Response;
                    if (respuesta.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        bExiste = false;
                    }
                }
            }
            return bExiste;
        }
        public bool ExisteArchivoFtp(string RemotePath, string Login, string Password)
        {
            bool bExiste = true;

            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(RemotePath);
                request.Credentials = new NetworkCredential(Login, Password);
                request.Method = WebRequestMethods.Ftp.GetFileSize;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    FtpWebResponse respuesta = (FtpWebResponse)ex.Response;
                    if (respuesta.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        bExiste = false;
                    }
                }
            }
                return bExiste;

            
        }


        public string CrearCarpetaFtp(string ftpAddress, string pathToCreate, string login, string password, int ContSaltoDirectorios)
        {
            FtpWebRequest reqFTP = null;
            Stream ftpStream = null;
            

            string[] Ruta = pathToCreate.Split('\\');
            pathToCreate = string.Empty;
            for (int i = ContSaltoDirectorios; i <= Ruta.Length - 1; i++)
            {
                if (Ruta[i].ToString().Length > 0 && !string.IsNullOrEmpty(Ruta[i].ToString()))
                {
                    pathToCreate = pathToCreate + Ruta[i].ToString() + '\\';
                }
            }

            string[] subDirs = pathToCreate.Split('\\');
            string currentDir = string.Format("ftp://{0}", ftpAddress);

            foreach (string subDir in subDirs)
            {
                try
                {
                    if (!string.IsNullOrEmpty(subDir) && subDir.Length > 0 )
                    {
                        currentDir = currentDir + "/" + subDir;

                        if (!ExisteCarpetaFtp(currentDir, login, password))
                        {
                            reqFTP = (FtpWebRequest)FtpWebRequest.Create(currentDir);
                            reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                            reqFTP.UseBinary = true;
                            reqFTP.Credentials = new NetworkCredential(login, password);
                            FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                            ftpStream = response.GetResponseStream();
                            ftpStream.Close();
                            response.Close();
                        }
                    }
                }
                catch (WebException exc)
                {
                    SMLog.Escribir(Severidad.Critico, "Creacion Archivos FTP -> error en creacion del directorio ftp:: " + exc.Message);
                    return string.Empty;
                }
            }

            SMLog.Escribir(Severidad.Informativo, "Creacion Archivos FTP -> se crea el directorio ftp:  " + currentDir);
            return currentDir;
        }

        public List<string> SubirArchivosFtp(string FilePath, string RemotePath, string Login, string Password, List<string> LstArchivosCopiar)
        {
            List<string> LstArchivos = new List<string>();
            List<string> ArchivosCopiar = new List<string>();
            try
            {
                if (System.IO.Directory.Exists(FilePath))
                {
                    if (LstArchivosCopiar != null && LstArchivosCopiar.Count > 0)
                    {
                        ArchivosCopiar = LstArchivosCopiar;
                    }
                    else
                    {
                        foreach (var archivos in Directory.GetFiles(FilePath))
                        {
                            ArchivosCopiar.Add(archivos);
                        }
                    }

                    //foreach (var LocalFile in Directory.GetFiles(FilePath))
                    foreach (var LocalFile in ArchivosCopiar)
                    {
                        string url = Path.Combine(RemotePath, Path.GetFileName(LocalFile.ToString()));

                        if (!ExisteArchivoFtp(url, Login, Password))
                        {
                            using (FileStream fs = new FileStream(LocalFile.ToString(), FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                // Creo el objeto ftp
                                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(url);

                                // Fijo las credenciales, usuario y contraseña
                                ftp.Credentials = new NetworkCredential(Login, Password);

                                // Le digo que no mantenga la conexión activa al terminar.
                                ftp.KeepAlive = false;

                                // Indicamos que la operación es subir un archivo...
                                ftp.Method = WebRequestMethods.Ftp.UploadFile;

                                // &#8230; en modo binario &#8230; (podria ser como ASCII)
                                ftp.UseBinary = true;

                                // Indicamos la longitud total de lo que vamos a enviar.
                                ftp.ContentLength = fs.Length;

                                // Desactivo cualquier posible proxy http.
                                // Ojo pues de saltar este paso podría usar 
                                // un proxy configurado en iexplorer
                                ftp.Proxy = null;

                                // Pongo el stream al inicio
                                fs.Position = 0;

                                // Configuro el buffer a 2 KBytes
                                int buffLength = 2048;
                                byte[] buff = new byte[buffLength];

                                int contentLen;

                                // obtener el stream del socket sobre el que se va a escribir.
                                using (Stream strm = ftp.GetRequestStream())
                                {
                                    // Leer del buffer 2kb cada vez
                                    contentLen = fs.Read(buff, 0, buffLength);

                                    // mientras haya datos en el buffer &#8230;.
                                    while (contentLen != 0)
                                    {
                                        // escribir en el stream de conexión
                                        //el contenido del stream del fichero
                                        strm.Write(buff, 0, contentLen);
                                        contentLen = fs.Read(buff, 0, buffLength);
                                    }
                                }
                                url = url.Replace("//", "\\");
                                url = url.Replace("/", "\\");

                                SMLog.Escribir(Severidad.Informativo, "Creacion Archivos FTP :: GeneracionCarpetasServidorSIGPRO -> se crea los archivos ftp:  " + url );
                                LstArchivos.Add(url);
                            }
                        }
                        else
                        {
                            LstArchivos.Add(url);
                        }
                    }
                }
                
                //using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            }
            catch (WebException exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "Creacion Archivos FTP :: GrabacionArchivosRepositorioSIGPRO -> Error Inesperado: " + exc.Message);
            }
            return LstArchivos;
        }
    }
}
