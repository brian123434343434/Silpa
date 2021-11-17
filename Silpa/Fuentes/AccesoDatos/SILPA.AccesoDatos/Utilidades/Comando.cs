using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SILPA.AccesoDatos.Utilidades
{
    public class Comando
    {
        private SqlConnection _conn;
        private SqlCommand _comm;

        private CommandType _tipoComando;
        private string _sql;
        private bool _conectado = false;

        public SqlParameterCollection Parametros
        {
            get
            {
                return _comm.Parameters;
            }
        }

        public Comando(CommandType tipo, string sql)
        {
            _tipoComando = tipo;
            _sql = sql;
            InitConexion();
            InitComando();
        }

        private void InitConexion()
        {
            try
            {
                _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SILPAConnectionString"].ConnectionString);
                _conn.Open();
                _conectado = true;
            }
            catch (Exception ex)
            {
                _conectado = false;
            }
        }

        private void InitComando()
        {
            _comm = new SqlCommand();
            _comm.Connection = _conn;
            _comm.CommandType = _tipoComando;
            _comm.CommandText = _sql;
        }

        public void AddParametro(string nombre, SqlDbType tipoPar, ParameterDirection direccion, object valor)
        {
            SqlParameter par = new SqlParameter();
            try
            {
                par.ParameterName = nombre;
                par.Direction = direccion;
                par.SqlDbType = tipoPar;
                par.Value = valor;
                if (valor.ToString() != "")
                    par.Size = valor.ToString().Length;
                _comm.Parameters.Add(par);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message + " " + "No se puede crear el Parametro con los valores asignados");
            }
        }

        public DataTable EjecutarDataTable()
        {
            SqlDataAdapter adap = new SqlDataAdapter();
            DataTable data = new DataTable();
            SqlCommand com = new SqlCommand();
            try
            {
                adap.SelectCommand = _comm;
                adap.Fill(data);
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                Desconectar();
            }

        }

        private void Desconectar()
        {
            _conn.Close();
            _conectado = false;
            _comm.Dispose();
            _conn.Close();
            _conn = null;
        }

        public void Ejecutar()
        {
            try
            {
                _comm.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message + " " + "No es posible ejecutar sentencia " + _comm.CommandText);
            }
            finally
            {
                Desconectar();
            }
        }
    }
}