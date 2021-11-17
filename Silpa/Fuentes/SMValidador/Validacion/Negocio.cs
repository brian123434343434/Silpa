using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SILPA.Validador
{
    public class Negocio
    {
        private bool _correcto = true ;
        private string _idCampo;
        private object _valor;
        private DataTable _datos = new DataTable();
        private ValidarCampo _validacion;
        
        public bool Correcto
        {
            get { return _correcto; }
            set { _correcto = value; }
        }
        private System.Collections.ArrayList _mensaje;

        public System.Collections.ArrayList Mensaje
        {
            get { return _mensaje; }
            set { _mensaje = value; }
        }

        private Campo _campo;  
        
        public Negocio(string idCampo, object valor)
        {
            _idCampo = idCampo;
            _valor = valor;
  
            _validacion = new SILPA.Validador.ValidarCampo();
            _datos = _validacion.BuscarRegistroCampo(_idCampo);
            _mensaje = new System.Collections.ArrayList();
            _campo = new SILPA.Validador.Campo();
            if (_datos.Rows.Count != 0)
                _campo.BuscarRegistro(idCampo);
        }

        public void Validador()
        {
            string sentencia = "";
            Validacion val = new SILPA.Validador.Validacion();
  
            foreach (DataRow r in _datos.Rows)
            {
                string valorSentencia = "";
                val.BuscarRegistro(Convert.ToInt32(r["ID_VALIDACION"]));
                sentencia = val.Sentencia.Replace("<VALOR>", _campo.TipoDato.Separador + _valor.ToString() + _campo.TipoDato.Separador);

                valorSentencia = EjecutarSecuencia(sentencia);
                if (valorSentencia != "1")
                    _mensaje.Add("la validacion " + val.Descripcion + " No cumple para el valor " + _valor.ToString()); 
            }
            if (_mensaje.Count != 0)
                _correcto = false; 
        }

        private string EjecutarSecuencia(string senetencia)
        {
            string salida = "";
            DataBase db;
            string cadena = Utilidad._utilidad._cadena; try
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
                salida = db.EjecutarFuncion("SELECT " + senetencia).ToString() ;
                return salida;
            }
            finally
            {
                db = null;
            }    
        }
    }
}
