using System;
using System.Collections.Generic;
using System.Text;
using System.Data; 
using SILPA.Validador;  

namespace ValidadorNegocio
{
    public class Validador
    {
        /// <summary>
        /// variable que recibe el xml de validacion
        /// </summary>
        private string _comando;
        private string _nombreValidacion;
        private string _procedimiento;

        public string Procedimiento
        {
            get { return _procedimiento; }
            set { _procedimiento = value; }
        }


        /// <summary>
        /// Variable y propiedad que recibe y envia si el comando ingresado requiere validacion
        /// por negocio dentro de la aplicacion
        /// </summary>
        private bool _requieRevalidacion;
        public bool RequieRevalidacion
        {
          get { return _requieRevalidacion; }
          set { _requieRevalidacion = value; }
        }
        /// <summary>
        /// Construtor de la clase
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="cadenaCx"></param>
        public Validador(string xml, string cadenaCx)
        {
            _comando = xml;
            SILPA.ValidadorNegocio.Utilidad.Compartido.Init(cadenaCx);
            RealizarValidacion();
        }

        private bool RealizarValidacion()
        {
            SILPA.ValidadorNegocio.Logica.Revision rev;     
            int inicio = 0, fin = 0 ;
            bool salida = false; 
            inicio = _comando.IndexOf("<name>");
            if (inicio > 0)
            {
                fin = _comando.IndexOf(@"</name>");
                //_nombreValidacion = _comando.Substring(inicio + "<name>".Length, fin - inicio);
                _nombreValidacion = _comando.Substring(inicio + "<name>".Length, fin - inicio - @"</name>".Length + 1);
                rev = new SILPA.ValidadorNegocio.Logica.Revision(_nombreValidacion);
                salida = rev.EncontroComando();
                _procedimiento = rev.Sentencia;
                _requieRevalidacion = salida; 
                
            }
            return salida;
        }

        public string Mensaje(string procedimiento)
        { 
            DataBase db;
            string cadena = Utilidad._utilidad._cadena;
            try
            {
                switch (Utilidad._utilidad._tipoMotor)
                {
                    case Utilidad.TipoMotor.ORACLE: db = new DataBase(cadena, DataBase.TipoMotor.ORACLE);
                        break;
                    case Utilidad.TipoMotor.SQLSERVER: db = new DataBase(cadena, DataBase.TipoMotor.SQLSERVER);
                        break;
                    default: db = new DataBase(cadena, DataBase.TipoMotor.SQLSERVER);
                        break;
                }

                return db.EjecutarFuncionDirect(procedimiento).Rows[0][0].ToString();
            }
            finally
            {
                db = null;
            }
        }

    }
}
