using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun; 

namespace SILPA.AccesoDatos.ImpresionesFus
{
    /// <summary>
    /// Clase que me permite almacenar la informacion del archivo Fus
    /// </summary>
    public class ImpresionArchivoFus
    {
        /// <summary>
        /// Atributo con la informacion del campo
        /// </summary>
        private string _strCampo;
        public string strCampo
        {
            get { return _strCampo; }
            set { _strCampo = value; }
        }

        /// <summary>
        /// Atributo con la informacion del valor
        /// </summary>
        private string _strValor;
        public string strValor
        {
            get { return _strValor; }
            set { _strValor = value; }
        }

        public ImpresionArchivoFus()
        { 
        }
        public ImpresionArchivoFus(string strCampo, string strValor)
        {
            this._strCampo = strCampo;
            this._strValor = strValor;
        }

    }
}
