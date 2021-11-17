using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace SoftManagement.Persistencia
{
    internal  class Conexion
    {
        DataSet _ds;
        string _strConexion;
        AnalizadorTablas analizer;
        string _tabla;
        public Conexion(string strConexion, DataSet ds, string tabla)
        {
            _ds = ds;
            _strConexion = strConexion;
            _tabla = tabla;
            analizer = new AnalizadorTablas(_ds, tabla); 
        }


        public void cargar() {
            using (SqlConnection cnn = new SqlConnection())
            {
                cnn.ConnectionString = _strConexion;
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = analizer.getSelect();
                adp.SelectCommand.Connection = cnn;
                adp.FillSchema(_ds, SchemaType.Mapped, _tabla);
                adp.Fill(_ds, _tabla);
                adp.InsertCommand = analizer.getInsert();
                adp.UpdateCommand = analizer.getUpdate();
                adp.DeleteCommand = analizer.getdelete();

                adp.InsertCommand.Connection = cnn;
                adp.UpdateCommand.Connection = cnn;
                adp.DeleteCommand.Connection = cnn;
            }
        
        }
        public void cargar(List<SqlParameter> parametros)
        {
            using (SqlConnection cnn = new SqlConnection())
            {
                cnn.ConnectionString = _strConexion;
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = analizer.getSelect();
                adp.SelectCommand.Connection = cnn;

               

                foreach (SqlParameter  item in parametros)
                {
                    adp.SelectCommand.Parameters.Add(item);
                    adp.SelectCommand.CommandText += " AND " + item.SourceColumn + "=" + item.ParameterName;
 
                }
                adp.FillSchema(_ds, SchemaType.Mapped, _tabla);
                adp.Fill(_ds, _tabla);
                adp.InsertCommand = analizer.getInsert();
                adp.UpdateCommand = analizer.getUpdate();
                adp.DeleteCommand = analizer.getdelete();
                adp.InsertCommand.Connection = cnn;
                adp.UpdateCommand.Connection = cnn;
                adp.DeleteCommand.Connection = cnn;
            }


        }
        public void RegistrarCambios()
        {
            using (SqlConnection cnn=new SqlConnection())
            {
                cnn.ConnectionString = _strConexion;
                
                SqlDataAdapter  adp= new SqlDataAdapter();
                adp.SelectCommand = analizer.getSelect();
                adp.InsertCommand = analizer.getInsert();
                adp.UpdateCommand = analizer.getUpdate();
                adp.DeleteCommand = analizer.getdelete();
                
                adp.SelectCommand.Connection = cnn;
                adp.InsertCommand.Connection = cnn;
                adp.UpdateCommand.Connection = cnn;
                adp.DeleteCommand.Connection = cnn;
                adp.Update(_ds, _tabla);

            }
        
        }

    }
}
