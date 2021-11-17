using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SILPA.Validador;  



namespace SILPA.ValidadorNegocio.Logica
{
    public class Revision
    {
        private string _com;
        private string _sentencia;

        public string Sentencia
        {
            get { return _sentencia; }
        }
        public Revision(string com)
        {
            _com = com;
            SILPA.Validador.Utilidad._utilidad._cadena = Utilidad.Compartido._compartido.cadenaSQL;
            SILPA.Validador.Utilidad._utilidad._tipoMotor = SILPA.Validador.Utilidad.TipoMotor.SQLSERVER;  
        }

        public bool EncontroComando()
        {
            DataBase db;
            DataTable reg = new DataTable();
            string cadena = SILPA.Validador.Utilidad._utilidad._cadena;
            try
            {
                switch (SILPA.Validador.Utilidad._utilidad._tipoMotor)
                {
                    case SILPA.Validador.Utilidad.TipoMotor.ORACLE: db = new DataBase(Utilidad.Compartido._compartido.cadenaSQL , DataBase.TipoMotor.ORACLE);
                        break;
                    case SILPA.Validador.Utilidad.TipoMotor.SQLSERVER: db = new DataBase(Utilidad.Compartido._compartido.cadenaSQL, DataBase.TipoMotor.SQLSERVER);
                        break;
                    default: db = new DataBase(Utilidad.Compartido._compartido.cadenaSQL, DataBase.TipoMotor.SQLSERVER);
                        break;
                }
                reg = db.EjecutarProcedimientoRs("VAB_LISTA_VALIDACION_NEGOCIO");

                foreach(DataRow r in reg.Rows)
                {
                    if (r["VALIDACION_NEGOCIO"].ToString().ToUpper() == _com.ToUpper())
                    {
                        _sentencia = r["PROCEDIMIENTO"].ToString();
                        return true;
                    }
                }
                return false;
            }
            finally
            {
                db = null;
            }
        }

    }
}
