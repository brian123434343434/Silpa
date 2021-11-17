using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos;  

namespace SILPA.LogicaNegocio.Generico
{
    public class NumeroSilpa
    {
        public NumeroSilpa()
        { 
        }

        public string TomaNumeroSilpa(int idInstancia)
        {
            SILPA.AccesoDatos.Generico.NumeroSilpaDalc dalc = new SILPA.AccesoDatos.Generico.NumeroSilpaDalc();
            return dalc.NumeroSilpa(idInstancia);  
        }
        /// <summary>
        /// Metodo que genera el mensaje de solicitud recibida en VITAL
        /// </summary>
        /// <param name="numeroVITAL">Nro VITAL de la solicitud</param>
        /// <returns></returns>
        public string mensajeSolicitudRecibida(string numeroVITAL)
        {
            SILPA.AccesoDatos.Generico.NumeroSilpaDalc dalc = new SILPA.AccesoDatos.Generico.NumeroSilpaDalc();
            return dalc.mensajeSolicitudRecibida(numeroVITAL);  
        }

        public string mensajeSolicitudRecibida(int IdProcessInstance, out string str_NumeroVital)
        {
            SILPA.AccesoDatos.Generico.NumeroSilpaDalc dalc = new SILPA.AccesoDatos.Generico.NumeroSilpaDalc();
            return dalc.mensajeSolicitudRecibida(IdProcessInstance, out str_NumeroVital);
        }

        public string mensajeSolicitudRadicada(int id)
        {
            SILPA.AccesoDatos.Generico.NumeroSilpaDalc dalc = new SILPA.AccesoDatos.Generico.NumeroSilpaDalc();
            return dalc.mensajeSolicitudRadicada(id);
        }

        /// <summary>
        /// Obtiene el identtificador Processinstance
        /// mediante el número vital completo
        /// </summary>
        /// <param name="NumeroVital"></param>
        /// <returns>int : processinstance</returns>
        public int ObtenerProcessInstance(string NumeroVital)
        {
            try
            {
                SILPA.AccesoDatos.Generico.NumeroSilpaDalc dalc = new SILPA.AccesoDatos.Generico.NumeroSilpaDalc();
                string strResult = dalc.ProcessInstance(NumeroVital);
                if (!string.IsNullOrEmpty(strResult))
                {
                    return int.Parse(strResult);
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al obtener el identtificador del Processinstance.";
                throw new Exception(strException, ex);
            }
        }
    }
}
