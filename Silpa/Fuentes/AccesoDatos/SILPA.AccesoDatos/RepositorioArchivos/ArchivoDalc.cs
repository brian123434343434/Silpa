using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.RepositorioArchivos
{
    public class ArchivoDalc
    {
        private Configuracion objConfiguracion;

        public ArchivoDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public void InsertarArchivo(Archivo objArchivo)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("REPOSITORIO_ARCH_SP_INSERTAR_ARCHIVO");
                db.AddInParameter(cmd, "P_NOMBRE_ARCHIVO", DbType.String, objArchivo.NombreArchivo);
                db.AddInParameter(cmd, "P_UBICACION", DbType.String, objArchivo.Ubicacion);
                db.AddInParameter(cmd, "P_TIPO_ARCHIVO", DbType.Int32, objArchivo.TipoArchivo);
                db.AddInParameter(cmd, "P_ASOCIADO", DbType.Boolean, objArchivo.Asociado);
                db.AddInParameter(cmd, "P_USUARIO", DbType.Int32, objArchivo.UsuarioID);
                db.AddInParameter(cmd, "P_TAMAÑO", DbType.Decimal, objArchivo.Tamaño);
                db.AddInParameter(cmd, "P_DESC_TIPO_ARCHIVO", DbType.String, objArchivo.DescTipoArchivo);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void EliminarArchivo(Archivo objArchivo)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("REPOSITORIO_ARCH_SP_ELIMINAR_ARCHIVO");
                db.AddInParameter(cmd, "P_NOMBRE_ARCHIVO", DbType.String, objArchivo.NombreArchivo);
                db.AddInParameter(cmd, "P_FILE_ID", DbType.Int32, objArchivo.FileID);
                db.AddInParameter(cmd, "P_USUARIO", DbType.Int32, objArchivo.UsuarioID);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Archivo> ConsultaArchivos(string nombreArchivo, int? fileID, int? usuarioID)
        {
            try 
	        {
                List<Archivo> LstArchivo = new List<Archivo>();
		        SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("REPOSITORIO_ARCH_SP_CONSULTAR_ARCHIVO");
                db.AddInParameter(cmd, "P_NOMBRE_ARCHIVO", DbType.String, nombreArchivo);
                db.AddInParameter(cmd, "P_FILE_ID", DbType.Int32, fileID);
                db.AddInParameter(cmd, "P_USUARIO", DbType.Int32, usuarioID);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        LstArchivo.Add(new Archivo { FileID = Convert.ToInt32(reader["FILE_ID"]), Asociado = Convert.ToBoolean(reader["ASOCIADO"]), 
                            NombreArchivo = reader["NOMBRE_ARCHIVO"].ToString(), TipoArchivo = Convert.ToInt32(reader["TIPO_ARCHIVO"]), 
                            Ubicacion = reader["UBICACION"].ToString(), UsuarioID = Convert.ToInt32(reader["USUARIO"]), 
                            Tamaño = Convert.ToDecimal(reader["TAMAÑO"]), FechaRegistro = Convert.ToDateTime(reader["FECHA_REGISTRO"]),
                                                     DescTipoArchivo = reader["NOMBRE_TIPO_ARCHIVO"].ToString() });
                    }
                }
                return LstArchivo;
	        }
	        catch (Exception)
	        {
		
		        throw;
	        }
            
        }

        public void AsociarArchivo(string nombreArchivo, int? usuarioID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("REPOSITORIO_ARCH_SP_ASOCIAR_ARCHIVO");
                db.AddInParameter(cmd, "P_NOMBRE_ARCHIVO", DbType.String, nombreArchivo);
                db.AddInParameter(cmd, "P_USUARIO", DbType.Int32, usuarioID);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable TablaFormularios()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("REPOSITORIO_ARCH_SP_CONSULTAR_FORMULARIOS");
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
