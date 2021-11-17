using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Tramites.BeneficioTributario.CertificadoUPME;

namespace SILPA.LogicaNegocio.Tramites.BeneficioTributario.CertificadoUPME
{
    public class LogUPME
    {
        public LogCertificadoUPMEDalc _LogCertificadoUPMEDalc { get; set; }
        #region Constructores
        public LogUPME()
        {
            _LogCertificadoUPMEDalc = new LogCertificadoUPMEDalc();
        }
        #endregion Constructores
        #region Metodos Publicos
        /// <summary>
        /// inserta un nuevo registro en el log
        /// </summary>
        /// <param name="strMetodoServicio">Nombre del metodo del servicio</param>
        /// <param name="strDatosEnviados">datos enviados al servicio</param>
        /// <param name="strResultado">resultado del consumo del servicio</param>
        public void insertarRegistroLog(string strMetodoServicio, string strDatosEnviados, string strResultado)
        {
            _LogCertificadoUPMEDalc.InsertarLog(strMetodoServicio, strDatosEnviados, strResultado);
        }
        #endregion Metodos Publicos
    }

    

}
