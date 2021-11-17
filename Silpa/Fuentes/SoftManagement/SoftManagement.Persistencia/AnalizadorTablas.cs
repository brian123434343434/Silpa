using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace SoftManagement.Persistencia
{
       internal  class AnalizadorTablas
    {
           DataSet _ds;
           string llaves;
           string _nombreTabla;

           SqlCommand cmd;
           /// <summary>
           /// constructor clase 
           /// </summary>
           /// <param name="ds">dataset a descomponer</param>
           /// <param name="nombreTabla">tabla que sera cargada en el dataset ds</param>
           public  AnalizadorTablas(DataSet ds, string nombreTabla)
           {
               this._ds = ds;
               this._nombreTabla = nombreTabla;
           }
           /// <summary>
           /// metodo encargado de obtener las llaves de la tabla cargada, retorna el string  where
           /// y adiciona los parametros al commando que se este ejecutando "insert update delete"
           /// </summary>
           /// <returns></returns>
           private string getLlaves()
           {
               llaves = "";
               DataColumn[] dt;
               dt = _ds.Tables[_nombreTabla].PrimaryKey;
              for (int i = 0; i < dt.Length; i++)
              {
                 
                  if (i == dt.Length-1 )
                      llaves += dt[i].ColumnName + "=@" + dt[i].ColumnName+"  ";
                  else
                      llaves += dt[i].ColumnName + "=@" + dt[i].ColumnName+" AND ";
                  if (!verificaParametro("@" + _ds.Tables[_nombreTabla].Columns[i].ColumnName))
                      cmd.Parameters.Add("@" + _ds.Tables[_nombreTabla].Columns[i].ColumnName, getTipodato(_ds.Tables[_nombreTabla].Columns[i].DataType), 60, _ds.Tables[_nombreTabla].Columns[i].ColumnName);
              }

              return llaves;
           }
           /// <summary>
           /// metodo encargado de verificar si un parametro ya fue adicionado, en el caso de los updates, y selects con where
           /// </summary>
           /// <param name="p"></param>
           /// <returns></returns>
           private bool verificaParametro(string p)
           {
               bool rsta=false;
               foreach (SqlParameter  item in cmd.Parameters )
               {
                   if (item.ParameterName == p)
                       rsta = true;   
               }
               return rsta;
           }
           /// <summary>
           /// retorna el sqlcommand select para la tabla
           /// </summary>
           /// <returns></returns>
           public SqlCommand getSelect()
           {
               StringBuilder strBuilder = new StringBuilder();

               strBuilder.Append("SELECT * FROM ");
               strBuilder.Append(_nombreTabla);
               strBuilder.Append(" where 1=1");     
               cmd = new SqlCommand();
               cmd.CommandText = strBuilder.ToString();// "SELECT * FROM " + _nombreTabla+" where 1=1";
               return cmd;

           } 
           /// <summary>
           /// retorna el sqlcommand update con los parametros y campos a actualizar
           /// </summary>
           /// <returns></returns>
           public SqlCommand getUpdate()
           {
                      String  strSQL; 
               cmd = new SqlCommand();
               strSQL = "UPDATE "+ _nombreTabla + " SET ";               
               for (int i = 0; i < _ds.Tables[_nombreTabla].Columns.Count; i++)
               {
                   if (i == _ds.Tables[_nombreTabla].Columns.Count - 1 && !_ds.Tables[_nombreTabla].Columns[i].AutoIncrement)
                       strSQL += " " + _ds.Tables[_nombreTabla].Columns[i].ColumnName + "=@" + _ds.Tables[_nombreTabla].Columns[i].ColumnName + " ";
                       
                   else if( !_ds.Tables[_nombreTabla].Columns[i].AutoIncrement)
                       strSQL += " " + _ds.Tables[_nombreTabla].Columns[i].ColumnName + "=@" + _ds.Tables[_nombreTabla].Columns[i].ColumnName + ", ";
                  if( !_ds.Tables[_nombreTabla].Columns[i].AutoIncrement)
                      cmd.Parameters.Add("@" + _ds.Tables[_nombreTabla].Columns[i].ColumnName, getTipodato(_ds.Tables[_nombreTabla].Columns[i].DataType), 60, _ds.Tables[_nombreTabla].Columns[i].ColumnName);
               }
               strSQL += " where " + getLlaves();
               cmd.CommandText = strSQL;
               return cmd;
           }
           /// <summary>
           /// mapea el tipo de dato deacuerdo al tipo de la columna del data table, si no existen 
           /// tipos agregarlos a este metodo
           /// </summary>
           /// <param name="type"></param>
           /// <returns></returns>
           private SqlDbType getTipodato(Type type)
           {
               SqlDbType tipo=new SqlDbType();
               switch (type.ToString ())
               {
                   case "System.Double":
                       tipo = SqlDbType.Float;
                       break;
                   case "System.Decimal":
                       tipo= SqlDbType.Decimal;
                       break;
                   case "System.String":
                       tipo = SqlDbType.VarChar;
                       break;
                   case "System.Int16":
                       tipo = SqlDbType.Int;
                       break;
                   case "System.Int32":
                       tipo = SqlDbType.Int;
                       break;
                   case "System.Int64":
                       tipo = SqlDbType.Int;
                       break;
                   case "System.DateTime":
                       tipo = SqlDbType.DateTime;
                       break;                       
               }
               return tipo; 
           }
           /// <summary>
           /// retorna sqlcommand delete con parametros incluidos
           /// </summary>
           /// <returns></returns>
           public SqlCommand  getdelete()
           {
               using (cmd = new SqlCommand())
               {
                   string strSQL;
                   strSQL = "DELETE FROM " + _nombreTabla + "  ";
                   strSQL += " where " + getLlaves();
                   cmd.CommandText = strSQL;
                   return cmd;
               }
           }
           /// <summary>
           /// genera sentencia insert con parametros
           /// </summary>
           /// <returns></returns>
           public SqlCommand  getInsert()
           {
               string strSQL;
               strSQL = "INSERT INTO " + _nombreTabla + "  (";
               for (int i = 0; i < _ds.Tables[_nombreTabla].Columns.Count; i++)
               {
                   if (i == _ds.Tables[_nombreTabla].Columns.Count - 1 && !_ds.Tables[_nombreTabla].Columns[i].AutoIncrement)
                       strSQL += " " + _ds.Tables[_nombreTabla].Columns[i].ColumnName +" )";
                   else if (!_ds.Tables[_nombreTabla].Columns[i].AutoIncrement)
                       strSQL += " " + _ds.Tables[_nombreTabla].Columns[i].ColumnName +", ";
               }
                   strSQL += " VALUES (";
                   for (int i = 0; i < _ds.Tables[_nombreTabla].Columns.Count; i++)
                   {
                       if (i == _ds.Tables[_nombreTabla].Columns.Count - 1 && !_ds.Tables[_nombreTabla].Columns[i].AutoIncrement)
                           strSQL += " " +"@"+ _ds.Tables[_nombreTabla].Columns[i].ColumnName + " )";
                       else if ( !_ds.Tables[_nombreTabla].Columns[i].AutoIncrement)
                           strSQL += " " +"@"+ _ds.Tables[_nombreTabla].Columns[i].ColumnName + ", ";
                       if( !_ds.Tables[_nombreTabla].Columns[i].AutoIncrement)
                       cmd.Parameters.Add("@" + _ds.Tables[_nombreTabla].Columns[i].ColumnName, getTipodato(_ds.Tables[_nombreTabla].Columns[i].DataType), 60, _ds.Tables[_nombreTabla].Columns[i].ColumnName);
                   }
                   cmd.CommandText = strSQL;
                   return cmd;             

           }


    }
}
