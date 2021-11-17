using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos;  
using System.Data; 

namespace SILPA.LogicaNegocio.Utilidad
{
    public class Utilidad
    {
        AccesoDatos.Utilidades.Comando _comando;
        bool _activo;
        string _sentencia;

        public string Sentencia
        {
            get { return _sentencia; }
            set { _sentencia = value; }
        }  


        public Utilidad(string sentencia)
        {
            _comando = new SILPA.AccesoDatos.Utilidades.Comando(CommandType.Text,sentencia);
            _activo = AnalisisSentencia(sentencia);
        }

        public DataTable Ejecutar()
        {
            if (_activo == true)
            {
                return _comando.EjecutarDataTable();
            }
            else
            {
                return new DataTable(); 
            }
        }

        private bool AnalisisSentencia(string sentencia)
        {
            if (sentencia.ToUpper().IndexOf(ConandoProhibido.DELETE.ToUpper()) > 0)
                return false;
            if (sentencia.ToUpper().IndexOf(ConandoProhibido.DROP.ToUpper()) > 0)
                return false;
            if (sentencia.ToUpper().IndexOf(ConandoProhibido.INSERT.ToUpper()) > 0)
                return false;
            if (sentencia.ToUpper().IndexOf(ConandoProhibido.UPDATE.ToUpper()) > 0)
                return false;
            return true; 
        }
        
        private static class ConandoProhibido
        {
            public const string INSERT = "insert";
            public const string DELETE = "delete";
            public const string UPDATE = "update";
            public const string DROP = "drop";
        };
        
        
    }
}
