using System.Text;
using System.Security.Cryptography;
using System;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace SILPA.Comun
{
    public class EnDecript
    {


        public static string Encriptar(string msg)
        {
            string llave = "$Gattaca S.A. Ideas complejas, con soluciones s$";
            string desplazamiento = "G4tt4c4$";
            UnicodeEncoding UC = new UnicodeEncoding();
            string _mensaje = msg;
            byte[] _bytesMensaje = UC.GetBytes(_mensaje);
            TripleDESCryptoServiceProvider _tripeDes = new TripleDESCryptoServiceProvider();
            Rfc2898DeriveBytes k2 = new Rfc2898DeriveBytes(llave, UC.GetBytes(desplazamiento));
            _tripeDes.Key = k2.GetBytes(Convert.ToInt32(_tripeDes.KeySize / 8));
            _tripeDes.IV = k2.GetBytes(Convert.ToInt32(_tripeDes.BlockSize / 8));
            ICryptoTransform transform = _tripeDes.CreateEncryptor();
            byte[] texto = transform.TransformFinalBlock(_bytesMensaje, 0, _bytesMensaje.Length);

            StringBuilder hex = new StringBuilder(texto.Length * 2);
            foreach (byte b in texto)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();

        }
        public static string Encriptar(string msg, string newLlave)
        {
            string llave = newLlave;//"$Gattaca S.A. Ideas complejas, con soluciones s$";
            string desplazamiento = "G4tt4c4$";
            UnicodeEncoding UC = new UnicodeEncoding();
            string _mensaje = msg;
            byte[] _bytesMensaje = UC.GetBytes(_mensaje);
            TripleDESCryptoServiceProvider _tripeDes = new TripleDESCryptoServiceProvider();
            Rfc2898DeriveBytes k2 = new Rfc2898DeriveBytes(llave, UC.GetBytes(desplazamiento));
            _tripeDes.Key = k2.GetBytes(Convert.ToInt32(_tripeDes.KeySize / 8));
            _tripeDes.IV = k2.GetBytes(Convert.ToInt32(_tripeDes.BlockSize / 8));
            ICryptoTransform transform = _tripeDes.CreateEncryptor();
            byte[] texto = transform.TransformFinalBlock(_bytesMensaje, 0, _bytesMensaje.Length);

            StringBuilder hex = new StringBuilder(texto.Length * 2);
            foreach (byte b in texto)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }
        public static string EncriptarCorreo(string msg)
        {
            string llave = "softmanagement";
            string desplazamiento = "123";
            UnicodeEncoding UC = new UnicodeEncoding();
            string _mensaje = msg;
            byte[] _bytesMensaje = UC.GetBytes(_mensaje);
            TripleDESCryptoServiceProvider _tripeDes = new TripleDESCryptoServiceProvider();
            Rfc2898DeriveBytes k2 = new Rfc2898DeriveBytes(llave, UC.GetBytes(desplazamiento));
            _tripeDes.Key = k2.GetBytes(Convert.ToInt32(_tripeDes.KeySize / 8));
            _tripeDes.IV = k2.GetBytes(Convert.ToInt32(_tripeDes.BlockSize / 8));
            ICryptoTransform transform = _tripeDes.CreateEncryptor();
            byte[] texto = transform.TransformFinalBlock(_bytesMensaje, 0, _bytesMensaje.Length);

            StringBuilder hex = new StringBuilder(texto.Length * 2);
            foreach (byte b in texto)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }

        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];

            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            return bytes;
        }

        public static string Desencriptar(string msg)
        {
            ICryptoTransform transform = null;
            try
            {

                string paramllave = System.Configuration.ConfigurationManager.AppSettings["desc"].ToString();
                string paramdesplazamiento = System.Configuration.ConfigurationManager.AppSettings["cry"].ToString();
                //string llave = "$Gattaca S.A. Ideas complejas, con soluciones s$";
                string llave = ObtenerParametro(paramllave);
                // string desplazamiento = "G4tt4c4$";
                string desplazamiento = ObtenerParametro(paramdesplazamiento);
                UnicodeEncoding UC = new UnicodeEncoding();
                byte[] _bytesMensaje = StringToByteArray(msg);

                TripleDESCryptoServiceProvider _tripeDes = new TripleDESCryptoServiceProvider();
                Rfc2898DeriveBytes k2 = new Rfc2898DeriveBytes(llave, UC.GetBytes(desplazamiento));
                _tripeDes.Key = k2.GetBytes(Convert.ToInt32(_tripeDes.KeySize / 8));
                _tripeDes.IV = k2.GetBytes(Convert.ToInt32(_tripeDes.BlockSize / 8));

                transform = _tripeDes.CreateDecryptor();
                byte[] texto = transform.TransformFinalBlock(_bytesMensaje, 0, _bytesMensaje.Length);
                return UC.GetString(texto);
            }
            catch (CryptographicException ex)
            {
                return "";
            }

        }

        public static string DesencriptarDesplazamiento(string msg)
        {
            ICryptoTransform transform = null;
            try
            {                
                string llave = "$Gattaca S.A. Ideas complejas, con soluciones s$";                
                string desplazamiento = "G4tt4c4$";                
                UnicodeEncoding UC = new UnicodeEncoding();
                byte[] _bytesMensaje = StringToByteArray(msg);

                TripleDESCryptoServiceProvider _tripeDes = new TripleDESCryptoServiceProvider();
                Rfc2898DeriveBytes k2 = new Rfc2898DeriveBytes(llave, UC.GetBytes(desplazamiento));
                _tripeDes.Key = k2.GetBytes(Convert.ToInt32(_tripeDes.KeySize / 8));
                _tripeDes.IV = k2.GetBytes(Convert.ToInt32(_tripeDes.BlockSize / 8));

                transform = _tripeDes.CreateDecryptor();
                byte[] texto = transform.TransformFinalBlock(_bytesMensaje, 0, _bytesMensaje.Length);
                return UC.GetString(texto);
            }
            catch (CryptographicException ex)
            {
                return "";
            }

        }

        public static string DesencriptarCorreo(string msg)
        {
            
            string llave = "softmanagement";
            string desplazamiento = "123";
            UnicodeEncoding UC = new UnicodeEncoding();
            string _mensaje = msg;
            byte[] _bytesMensaje = UC.GetBytes(_mensaje);
            TripleDESCryptoServiceProvider _tripeDes = new TripleDESCryptoServiceProvider();
            Rfc2898DeriveBytes k2 = new Rfc2898DeriveBytes(llave, UC.GetBytes(desplazamiento));
            _tripeDes.Key = k2.GetBytes(Convert.ToInt32(_tripeDes.KeySize / 8));
            _tripeDes.IV = k2.GetBytes(Convert.ToInt32(_tripeDes.BlockSize / 8));

            ICryptoTransform transform = _tripeDes.CreateDecryptor();
            byte[] texto = transform.TransformFinalBlock(_bytesMensaje, 0, _bytesMensaje.Length);
            return UC.GetString(texto);

        }
        //JACOSTA 20120105.
        private static string Llave()
        {
            try
            {
               Configuracion objConfiguracion = new Configuracion();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { 38 };
                DbCommand cmd = db.GetStoredProcCommand("GEN_LISTAR_PARAMETRO", parametros);
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0].Rows[0]["PARAMETRO"].ToString());
            }

            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }


        private static string ObtenerParametro(string param)
        {
            try
            {
                Configuracion objConfiguracion = new Configuracion();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { param };
                DbCommand cmd = db.GetStoredProcCommand("GEN_LISTAR_PARAMETRO", parametros);
                DataSet ds_datos = new DataSet();
                ds_datos = db.ExecuteDataSet(cmd);
                return (ds_datos.Tables[0].Rows[0]["PARAMETRO"].ToString());
            }

            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
        }



    }
}
