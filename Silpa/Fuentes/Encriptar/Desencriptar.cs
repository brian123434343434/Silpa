using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;


namespace Encriptar
{
    public class Desencriptar
    {
        private string _mensaje = "";
        private Byte[] _bytesMensaje;
        private TripleDESCryptoServiceProvider _tripeDes;

        public Desencriptar(string msg, string llave, string desplazameinto)
        {
            UnicodeEncoding UC = new UnicodeEncoding();

            _mensaje = msg;
            _bytesMensaje = UC.GetBytes(_mensaje);

            _tripeDes = new TripleDESCryptoServiceProvider();

            _tripeDes = new TripleDESCryptoServiceProvider();
            _tripeDes.IV = UC.GetBytes(desplazameinto);
            _tripeDes.Key = UC.GetBytes(llave);

        }

        public string ValorDesencriptado()
        {
            ICryptoTransform transform = _tripeDes.CreateDecryptor();
            byte[] texto = transform.TransformFinalBlock(_bytesMensaje, 0, _bytesMensaje.Length);   
            UnicodeEncoding UC = new UnicodeEncoding();
            return UC.GetString(texto); 
        }
    }
}
