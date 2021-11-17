using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;

namespace SoftManagement.LogWS
{
    public static class SMLogWS
    {
        public static Int64 EscribirInicio(string strNombreWS, string strNombreMetodo, string strMensaje, string strNoVital, int iAutoridad)
        {
            try
            {
                return AccesoDatos.LogWSDao.GuardarLog(strNombreWS, strNombreMetodo, (int)Severidad.Inicio, "Inicio: " + strMensaje, strNoVital, iAutoridad, 0);
            }
            catch (Exception ex)
            {
                Logica.SMLogWSErrores.Escribir(ex.ToString());
                return -1;
            }
        }

        public static void EscribirFinalizar(string strNombreWS, string strNombreMetodo, string strMensaje, string strNoVital, int iAutoridad, Int64 iIdPadre)
        {
            try
            {
                AccesoDatos.LogWSDao.GuardarLog(strNombreWS, strNombreMetodo, (int)Severidad.Finaliza, "Finaliza: " + strMensaje, strNoVital, iAutoridad, iIdPadre);
            }
            catch (Exception ex)
            {
                Logica.SMLogWSErrores.Escribir(ex.ToString());
            }
        }

        public static void EscribirExcepcion(string strNombreWS, string strNombreMetodo, string strMensaje, Int64 iIdPadre)
        {
            try
            { 
                AccesoDatos.LogWSDao.GuardarLog(strNombreWS, strNombreMetodo, (int)Severidad.Excepcion, "Excepcion: " + strMensaje, "0", 0, iIdPadre);
            }
            catch (Exception ex)
            {
                Logica.SMLogWSErrores.Escribir(ex.ToString());
            }
        }
    }
}
