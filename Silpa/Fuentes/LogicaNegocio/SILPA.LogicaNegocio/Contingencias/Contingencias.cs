using SILPA.AccesoDatos.Contingencias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Contingencias
{
    public class Contingencias
    {
        private ContingenciasDalc vContingenciasDalc;

        public Contingencias()
        {
            vContingenciasDalc = new ContingenciasDalc();
        }

        public string obtenerDestinadariosNivelEmergencia(string strMunicipio, string strNivelEmergencia)
        {
            int intNivelEmergencia = 0;
            switch (strNivelEmergencia.Trim())
            {
                case "1": //Local-menor
                    intNivelEmergencia = 1;
                    break;
                case "2": //Regional-medio
                    intNivelEmergencia = 2;
                    break;
                case "3": //Nacional-mayor
                    intNivelEmergencia = 3;
                    break;
                case "4": //Transfronterizo-mayor
                    intNivelEmergencia = 4;
                    break;
                default:
                    break;
            }
            DataTable dtdestinatarios = vContingenciasDalc.obtenerDestinadariosNivelEmergencia(strMunicipio, intNivelEmergencia);
            string strDestinatarios = string.Empty;
            foreach (DataRow dtrdestinatario in dtdestinatarios.Rows)
            {
                strDestinatarios += dtrdestinatario["CORREOS_NOTIFICACION"] + ";";
            }
            return strDestinatarios;
        }
    }
}
