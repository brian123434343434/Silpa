using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.Servicios.Sancionatorio
{
    public class SancionatorioFachada
    {
        public SILPA.LogicaNegocio.Sancionatorio.Queja _objSAN;

        //public SancionatorioFachada();

        public bool EnviarInformacionQueja(string xmlDatos)
        {
            _objSAN = new SILPA.LogicaNegocio.Sancionatorio.Queja();
            //_objSAN.InsertarQueja();
            _objSAN.EnviarCorreoQueja(xmlDatos);
            return true;
        }
    }
}
