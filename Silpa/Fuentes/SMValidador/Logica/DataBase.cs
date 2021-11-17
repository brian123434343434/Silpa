using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;

namespace SILPA.Validador
{
    public class DataBase
    {
        private IDbConnection _conexion;
        private TipoMotor _motor;
        private System.Collections.ArrayList _colleccion;

        public enum TiposDatos { CADENA, NUMERICO, FECHA }
        public enum TipoMotor { SQLSERVER, ORACLE }
        

        public DataBase(string cadena, TipoMotor tipoMotor)
        {
            _motor = tipoMotor;
            CrearConexion(cadena);
            _colleccion = new System.Collections.ArrayList(); 
            //_colleccion = new SqlParameterCollection((); 
        }

        private void CrearConexion(string cadena)
        {
            switch (_motor)
            { 
                case TipoMotor.ORACLE:
                    _conexion = new OracleConnection(cadena); 
                    break;
                case TipoMotor.SQLSERVER:
                    _conexion = new SqlConnection(cadena);
                    break; 
            }
            _conexion.Open(); 
        }

        public DataTable EjecutarProcedimientoRs(string procedimiento)
        {
            IDbCommand proc = new SqlCommand();
            IDbDataAdapter adaptador = new SqlDataAdapter();
            DataSet tabla = new DataSet(); 
            try
            {
                proc.Connection = (IDbConnection) _conexion;  
                proc.CommandType = CommandType.StoredProcedure;
                if (_colleccion.Count != 0)
                { 
                    foreach(IDbDataParameter p in this._colleccion)
                    {
                        proc.Parameters.Add(p); 
                    }
                }
                    
                proc.CommandText = procedimiento;
                adaptador.SelectCommand = proc;
                proc.ExecuteNonQuery();

                adaptador.Fill(tabla);
                return tabla.Tables[0]; 
            }
            finally
            {
                if (_conexion != null)
                    _conexion.Close();
                _conexion = null;
            }
        }
        
        public void EjecutarProcedimiento(string procedimiento)
        {
            IDbCommand proc = new SqlCommand();
            DataSet tabla = new DataSet();
            try
            {
                proc.Connection = (IDbConnection)_conexion;
                proc.CommandType = CommandType.StoredProcedure;
                if (_colleccion.Count != 0)
                {
                    foreach (IDbDataParameter p in this._colleccion)
                    {
                        proc.Parameters.Add(p);
                    }
                }

                proc.CommandText = procedimiento;
                proc.ExecuteNonQuery();
                
            }
            finally
            {
                if (_conexion != null)
                    _conexion.Close();
                _conexion = null;
            }
        }

        public void AdicionarParametros(string nombre, ParameterDirection direccion, SqlDbType tipo, object valor)
        { 
            SqlParameter parametro = new SqlParameter();

            parametro.ParameterName = "@" + nombre;  
            parametro.Direction = direccion;
            parametro.SqlDbType = tipo;
            if (direccion != ParameterDirection.Output)
            {
                parametro.Value = valor;
                parametro.Size = valor.ToString().Length;   
            }
            _colleccion.Add (parametro);  
        }

        public object EjecutarFuncion(string funcion)
        {
            IDbCommand proc = new SqlCommand();
            try
            {
                proc.Connection = (IDbConnection)_conexion;
                proc.CommandType = CommandType.Text;
                if (_colleccion.Count != 0)
                {
                    foreach (IDbDataParameter p in this._colleccion)
                    {
                        proc.Parameters.Add(p);
                    }
                }

                proc.CommandText = funcion;
                return proc.ExecuteScalar();
            }
            finally
            {
                if (_conexion != null)
                    _conexion.Close();
                _conexion = null;
            }
        }

        public DataTable EjecutarFuncionDirect(string funcion)
        {
            IDbCommand proc = new SqlCommand();
            IDbDataAdapter adap = new SqlDataAdapter();
            DataSet datas = new DataSet();    
            try
            {
                proc.Connection = (IDbConnection)_conexion;
                proc.CommandType = CommandType.Text;
                proc.CommandText = funcion;
                adap.SelectCommand = proc;
                adap.Fill(datas);
                return datas.Tables[0]; 
            }
            finally
            {
                if (_conexion != null)
                    _conexion.Close();
                _conexion = null;
            }
        }
    }
}
