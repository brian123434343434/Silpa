using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Generico
{
    /// <summary>
    /// Clase utilizada para consultar la Información Adicional de BPM
    /// </summary>
    public class InformacionAdicionalIdentity
    {
        private string _rutaDocumento;

        public string RutaDocumento
        {
            get { return _rutaDocumento; }
            set { _rutaDocumento = value; }
        }

        private string _numeroRadicado;

        public string NumeroRadicado
        {
            get { return _numeroRadicado; }
            set { _numeroRadicado = value; }
        }


    }
}
