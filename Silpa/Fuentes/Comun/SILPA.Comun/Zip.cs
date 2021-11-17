using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace SILPA.Comun
{
    /// <summary>
    /// Clase que permite comprimir y descomprimir archivos en formato ZIP
    /// </summary>
    public class Zip
    {

        /// <summary>
        /// Toma un archivo y lo convierte en Zip
        /// a partir de la ruta de acceso a disco del archivo
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool CrearZip(string file)
        {
            try
            {
                string zipfilename = Path.ChangeExtension(file, ".zip");

                // 'using' statements gaurantee the stream is closed properly which is a big source
                // of problems otherwise.  Its exception safe as well which is great.

                using (ZipOutputStream s = new ZipOutputStream(File.Create(zipfilename)))
                {

                    s.SetLevel(9); // 0 - store only to 9 - means best compression

                    byte[] buffer = new byte[4096];

                    // Using GetFileName makes the result compatible with XP
                    // as the resulting path is not absolute.
                    ZipEntry entry = new ZipEntry(Path.GetFileName(file));

                    // Setup the entry data as required.

                    // Crc and size are handled by the library for seakable streams
                    // so no need to do them here.

                    // Could also use the last write time or similar for the file.
                    entry.DateTime = DateTime.Now;
                    s.PutNextEntry(entry);

                    using (FileStream fs = File.OpenRead(file))
                    {

                        // Using a fixed size buffer here makes no noticeable difference for output
                        // but keeps a lid on memory usage.
                        int sourceBytes;
                        do
                        {
                            sourceBytes = fs.Read(buffer, 0, buffer.Length);
                            s.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }

                    // Finish/Close arent needed strictly as the using statement does this automatically

                    // Finish is important to ensure trailing information for a Zip file is appended.  Without this
                    // the created file would be invalid.
                    s.Finish();

                    // Close is important to wrap things up and unlock the file.
                    s.Close();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
                //Console.WriteLine("Exception during processing {0}", ex);

                // No need to rethrow the exception as for our purposes its handled.
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileBytes">Arreglo de bytes que componen el archivo</param>
        /// <param name="file">string : lugar de Destino del zip</param>
        /// <returns>True : si exito / flase si fracasa</returns>
        public bool CrearZipFromBytes(Byte[] fileBytes, string file)
        {
            try
            {
                string zipfilename = Path.ChangeExtension(file, ".zip");

                // 'using' statements gaurantee the stream is closed properly which is a big source
                // of problems otherwise.  Its exception safe as well which is great.

                //StreamReader stream =  new StreamReader();
                
                Stream stream = null;
                stream.Write(fileBytes, 0, fileBytes.Length);
                StreamReader streamRdr = new StreamReader(stream);
                
                using (ZipOutputStream s = new ZipOutputStream(stream))
                {

                    s.SetLevel(9); // 0 - store only to 9 - means best compression
                    

                    byte[] buffer = new byte[4096];

                    // Using GetFileName makes the result compatible with XP
                    // as the resulting path is not absolute.
                    ZipEntry entry = new ZipEntry(Path.GetFileName(file));

                    // Setup the entry data as required.

                    // Crc and size are handled by the library for seakable streams
                    // so no need to do them here.

                    // Could also use the last write time or similar for the file.
                    entry.DateTime = DateTime.Now;
                    s.PutNextEntry(entry);

                    using (FileStream fs = File.OpenRead(file))
                    {

                        // Using a fixed size buffer here makes no noticeable difference for output
                        // but keeps a lid on memory usage.
                        int sourceBytes;
                        do
                        {
                            sourceBytes = fs.Read(buffer, 0, buffer.Length);
                            s.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }

                    // Finish/Close arent needed strictly as the using statement does this automatically

                    // Finish is important to ensure trailing information for a Zip file is appended.  Without this
                    // the created file would be invalid.
                    s.Finish();

                    // Close is important to wrap things up and unlock the file.
                    s.Close();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
                //Console.WriteLine("Exception during processing {0}", ex);

                // No need to rethrow the exception as for our purposes its handled.
            }
        }



    }
}
