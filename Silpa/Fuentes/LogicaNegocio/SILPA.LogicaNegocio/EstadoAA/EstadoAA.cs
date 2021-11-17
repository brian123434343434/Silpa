using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.EstadoAA; 
using SoftManagement.Log;
using SILPA.Comun;  

namespace SILPA.LogicaNegocio.EstadoAA
{
    public class EstadoAA
    {
        private string _mensaje = "";
        private HistoriaCambioEstadoEntity historiaEstadoAA; 
        public EstadoAA(string mensaje)
        {
            _mensaje = mensaje;
            XmlSerializador _objSer = new XmlSerializador();
            try
            {
                HistoriaCambioEstadoEntity _xmlConsulta = new SILPA.AccesoDatos.EstadoAA.HistoriaCambioEstadoEntity();
                _xmlConsulta = (HistoriaCambioEstadoEntity)_objSer.Deserializar(new HistoriaCambioEstadoEntity(), mensaje);
                historiaEstadoAA = _xmlConsulta;
            }
            finally
            {
                _objSer = null;
            }
        }

        public void Insertar()
        {
            HistoriaCambioEstadoDalc.InsertarCambioHistoria(historiaEstadoAA.FechaRegistro, historiaEstadoAA.NumeroVital, historiaEstadoAA.ValorExpediente.ToString(), historiaEstadoAA.EstadoNuevo.EstId , historiaEstadoAA.Autoridad.IdAutoridad);
        }

    }
}
