using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Xml;
using System.Globalization;
//using System.Web.UI;
//using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;

namespace SILPA.Comun
{
    
    /// <summary>
    /// clase con la funcionalidad de funciones variadas
    /// </summary>
    public static class Tools
    {

        /// <summary>
        /// Rellena una cadena con una cantida de caracteres,
        /// con una longitud específica a la izquierda o a la derecha
        /// </summary>
        /// <param name="strOrigen">string: cadena original</param>
        /// <param name="strRelleno">string: cadena de relleno</param>
        /// <param name="intLongRequerida">int: longitud deseada</param>
        /// <param name="orientacion">enum: Izquierda, Derecha</param>
        /// <returns>string resultante con la longitud y caracteres deseados</returns>
        public static string RellenarCadena(string strOrigen, string strRelleno, int intLongRequerida, Orientacion orientacion)
        {
            string resultado = string.Empty;

            if (strOrigen.Length < intLongRequerida)
            {
                for (int i = 0; i < intLongRequerida; i++)
                {
                    resultado = resultado + strRelleno;
                }

                if (orientacion == Orientacion.IZQUIERDA)
                {
                    resultado = resultado + strOrigen;
                }
                else
                {
                    resultado = strOrigen + resultado;
                }
            }
            else
            {
                resultado = strOrigen;
            }

            return resultado;
        }

       /// <summary>
       /// Se obtiene el consecutivo
       /// </summary>
       /// <param name="tabla"></param>
       /// <param name="nombreColumna"></param>
       /// <returns></returns>
        public static int ObtenerConsecutivo(DataTable tabla, string nombreColumna)
        {
            try
            {
                int consecutivo = 0;
                string filtro = nombreColumna + " > 0";
                string orden = nombreColumna + " DESC";
                DataRow[] row = tabla.Select(filtro, orden, DataViewRowState.CurrentRows);
                if (row.Length > 0)
                    if (IsNumeric(row[0][0]))
                        consecutivo = Convert.ToInt32(row[0][nombreColumna]);
                consecutivo = consecutivo + 1;

                return consecutivo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    //    //Se valida si existe el item en la lista
    //    public static bool ElementoDuplicadoLista(ListItemCollection lista, string valor)
    //    {
    //        try
    //        {
    //            bool validar = true;
    //            ListItem item = null;
    //            item = lista.FindByValue(valor);
    //            if (item != null)
    //                validar = false;

    //            return validar;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    //Se valida si existe el item en la lista
    //    public static bool ElementoDuplicadoListaTexto(ListItemCollection lista, string valor)
    //    {
    //        try
    //        {
    //            bool validar = true;
    //            ListItem item = null;
    //            item = lista.FindByText(valor);
    //            if (item != null)
    //                validar = false;

    //            return validar;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }


        /// <summary>
        /// Se valida si es numerico
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(object expression)
        {
            try
            {
                bool isNum;
                double retNum;
                isNum = Double.TryParse(Convert.ToString(expression), NumberStyles.Any, NumberFormatInfo.InvariantInfo, out retNum);
                return isNum;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }


    /* Composición del número silpa
    ================================================================================================================
    02      000010200876        010         035.
    .	    .	                .           .
    .	    .	                .           .
    .	    .	                .           ...........> Un cochino consecutivo que se reinicia anualité
    .	    .                   .
    .	    .                   .
    .	    .                   .......................> Tres últimos dígitos del año en curso
    .	    .   
    .	    .
    .	    ...........................................> Cedula del solicitante
    .
    .
    ...................................................> El número del tipo de trámite  en silpa_pre.dbo.gen_tipo_tramite
    ================================================================================================================
     */
    public static class EstructuraNumeroSilpa 
    {
        /// <summary>
        /// el  número silpa completo.
        /// </summary>
        private static string numeroSilpa;
        public static string NumeroSilpa
        {
            get 
            {
                return numeroSilpa; 
            }
            set 
            {
                numeroSilpa = value;
                /// 01      234567890123        456         789   
                /// 02      000010200876        010         035.
                tipoTramite    = int.Parse(numeroSilpa.Substring(0,2));
                cedula         = numeroSilpa.Substring(2, 12);
                anio           = numeroSilpa.Substring(13, 3);
                consecutivo    = int.Parse(numeroSilpa.Substring(16, 3));
            }
        }
        
        /// <summary>
        /// Tipo de trámite
        /// </summary>
        private static int tipoTramite;
        public static int TipoTramite 
        {
            get { return tipoTramite; }
        }

        /// <summary>
        /// la cédula del solicitante
        /// </summary>
        private static string cedula;
        public static string Cedula { get { return cedula; } }

        /// <summary>
        /// Los tres últimos digitos del año
        /// </summary>
        private static string anio;
        public static string Anio
        {
            get { return anio; }
        }

        /// <summary>
        /// consecutivo que se actualiza cada año
        /// </summary>
        private static int consecutivo;
        public static int Consecutivo
        {
            get { return consecutivo; }
        }
    }


}
