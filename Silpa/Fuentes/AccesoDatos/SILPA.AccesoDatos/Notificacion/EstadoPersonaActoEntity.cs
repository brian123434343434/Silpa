using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Notificacion
{
    public class EstadoPersonaActoEntity
    {
        #region Atributos
        private decimal _idActo;
        private int _idEstado;
        private decimal _idPersona;
        private DateTime _fechaEstado;
        private string _rutaDocumentos;
        private int _id;
        private bool _sistemaPDI;
        private int _estadoActual;
        private int _enviaCorreo;
        #endregion
        #region Propiedades

        public decimal IdActo
        {
            get { return _idActo; }
            set { _idActo = value; }
        }
        public int IdEstado
        {
            get { return _idEstado; }
            set { _idEstado = value; }
        }
        public decimal IdPersona
        {
            get { return _idPersona; }
            set { _idPersona = value; }
        }
        public DateTime FechaEstado
        {
            get { return _fechaEstado; }
            set { _fechaEstado = value; }
        }
        public string RutaDocumentos
        {
            get { return _rutaDocumentos; }
            set { _rutaDocumentos= value; }
        }
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public bool SistemaPDI
        {
            get { return _sistemaPDI; }
            set { _sistemaPDI = value; }
        }
        public int EstadoActual
        {
            get { return _estadoActual; }
            set { _estadoActual = value; }
        }
        public int EnviaCorreo
        {
            get { return _enviaCorreo; }
            set { _enviaCorreo = value; }
        }
        #endregion


    }
}
