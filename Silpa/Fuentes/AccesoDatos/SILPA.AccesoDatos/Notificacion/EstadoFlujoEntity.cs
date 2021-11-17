using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Notificacion
{
    public class EstadoFlujoEntity
    {
        private int _estadoID;
        private string _estadoNombre;
        private int _estadoPadreID;
        private string _estadoNombrePadre;
        private string _url;
        private int _nroDiasTransicion;
        private bool _estadoEjecutoria;
        private bool _estadoNotificacion;
        private bool _estadoAnulacion;
        private bool _estadoFinalPublicidad;
        private bool _generaRecurso;

        public int EstadoID
        {
            get { return _estadoID; }
            set { _estadoID = value; }
        }
        public string NombreEstado
        {
            get { return _estadoNombre; }
            set { _estadoNombre = value; }
        }
        public int EstadoPadreID
        {
            get { return _estadoPadreID; }
            set { _estadoPadreID = value; }
        }
        public string NombreEstadoPadre
        {
            get { return _estadoNombrePadre; }
            set { _estadoNombrePadre = value; }
        }
        public string URL
        {
            get { return _url; }
            set { _url = value; }
        }
        public int NroDiasTransicion
        {
            get { return _nroDiasTransicion; }
            set { _nroDiasTransicion = value; }
        }
        public bool EstadoEjecutoria
        {
            get { return _estadoEjecutoria; }
            set { _estadoEjecutoria = value; }
        }
        public bool EstadoNotificacion
        {
            get { return _estadoNotificacion; }
            set { _estadoNotificacion = value; }
        }
        public bool EstadoAnulacion
        {
            get { return _estadoAnulacion; }
            set { _estadoAnulacion = value; }
        }
        public bool EstadoFinalPublicidad
        {
            get { return _estadoFinalPublicidad; }
            set { _estadoFinalPublicidad = value; }
        }
        public bool GeneraRecurso
        {
            get { return _generaRecurso; }
            set { _generaRecurso = value; }
        }
    }
}
