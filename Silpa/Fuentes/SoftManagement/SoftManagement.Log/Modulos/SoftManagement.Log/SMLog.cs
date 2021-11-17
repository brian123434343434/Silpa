using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;

namespace SoftManagement.Log
{
    public static class SMLog
    {
        public static void Escribir(Exception excepcion)
        {
            SMLog.Escribir(Severidad.Critico, excepcion.ToString(), null);
        }

        public static void Escribir(Severidad severidad, string mensaje)
        {
            SMLog.Escribir(severidad, mensaje, null);
        }

        public static void Escribir(Severidad severidad, string mensaje, string usuario)
        {
            string maquina = Environment.MachineName; //Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
            try
            {
                if (usuario == null)
                {
                    if (HttpContext.Current != null)
                    {
                        usuario = HttpContext.Current.User.Identity.Name;
                    }
                }
                AccesoDatos.LogDao.GuardarLog(maquina, mensaje, severidad, usuario);
            }
            catch (Exception ex)
            {
                Logica.SMLogErrores.Escribir(ex.ToString());
            }
        }


        #region jmartinez Creo funcionalidad para guardar log correos
        public static void EscribirCorreo(int IdCorreo ,Severidad severidad, string mensaje, Boolean EsError)
        {
            SMLog.EscribirCorreo(IdCorreo, severidad, mensaje, null, EsError);
        }


        public static void EscribirCorreo(int IdCorreo, Severidad severidad, string mensaje, string usuario, Boolean EsError)
        {
            string maquina = Environment.MachineName; //Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
            try
            {
                if (usuario == null)
                {
                    if (HttpContext.Current != null)
                    {
                        usuario = HttpContext.Current.User.Identity.Name;
                    }
                }
                AccesoDatos.LogDao.GuardarLogCorreo(IdCorreo, maquina, mensaje, EsError,severidad, usuario);
            }
            catch (Exception ex)
            {
                Logica.SMLogErrores.Escribir(ex.ToString());
            }
        }
        #endregion






    }
}
