using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
namespace SILPA.AccesoDatos.Salvoconducto
{
    public class ParametrizacionSunlDalc
    {
        private Configuracion objConfiguracion;

        public ParametrizacionSunlDalc()
        {
            objConfiguracion = new Configuracion();
        }


        public List<ParametrizacionSunlIdentity.EspecieTaxonomia> ListarEspecies(string strNombreCientifico, int pClaseRecursoID)
        {
            try
            {
                List<ParametrizacionSunlIdentity.EspecieTaxonomia> LstEspecies = new List<ParametrizacionSunlIdentity.EspecieTaxonomia>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTAR_ESPECIE_SUNL");
                db.AddInParameter(cmd, "P_NOMBRE_COMUN", DbType.String, strNombreCientifico);
                db.AddInParameter(cmd, "P_CLASE_RECURSO_ID", DbType.Int32, pClaseRecursoID);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        LstEspecies.Add(new ParametrizacionSunlIdentity.EspecieTaxonomia
                        {
                            EspecieTaxonimiaID = Convert.ToInt32(reader["ESPECIE_TAXONOMIA_ID"]),
                            NombreComun = Convert.ToString(reader["NOMBRE_COMUN"]),
                            NombreCientifico = Convert.ToString(reader["NOMBRE_CIENTIFICO"]),
                            ClaseRecursoID = Convert.ToInt32(reader["CLASE_RECURSO_ID"]),
                            StrClaseREcurso = Convert.ToString(reader["CLASE_RECURSO"]),
                            CodigoIdeam = Convert.ToString(reader["CODIGO_IDEAM"]),
                        });
                    }
                }
                return LstEspecies;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ObtenerTipoProductoDisponible(int pClaseProductoID)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTAR_TIPO_PRODUCTO_DISPONIBLE");
            db.AddInParameter(cmd, "P_CLASE_PRODUCTO_ID", DbType.Int32, pClaseProductoID);
            return db.ExecuteDataSet(cmd).Tables[0];
        }

        public DataTable ObtenerClaseProductoDisponible(int pTipoProductoID)
        {
            
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTAR_CLASE_PRODUCTO_DISPONIBLE");
            db.AddInParameter(cmd, "P_TIPO_PRODUCTO_ID", DbType.Int32, pTipoProductoID);
            return db.ExecuteDataSet(cmd).Tables[0];
        }

        public DataTable ObtenerUnidadMedidaDisponible(int pTipoProductoID)
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTAR_UNIDAD_MEDIDA_DISPONIBLE");
            db.AddInParameter(cmd, "P_TIPO_PRODUCTO_ID", DbType.Int32, pTipoProductoID);
            return db.ExecuteDataSet(cmd).Tables[0];
        }



        public List<ParametrizacionSunlIdentity.ClaseProducto> ListarClaseProducto(int pClaseRecursoID, string pStrClaseProducto )
        {
            try
            {
                List<ParametrizacionSunlIdentity.ClaseProducto> LstClaseProducto = new List<ParametrizacionSunlIdentity.ClaseProducto>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTAR_CLASE_PRODUCTO_SUNL");
                db.AddInParameter(cmd, "P_CLASE_RECURSO_ID", DbType.Int32, pClaseRecursoID);
                db.AddInParameter(cmd, "P_CLASE_PRODUCTO", DbType.String, pStrClaseProducto);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        LstClaseProducto.Add(new ParametrizacionSunlIdentity.ClaseProducto
                        {
                            ClaseProductoID = Convert.ToInt32(reader["CLASE_PRODUCTO_ID"]),
                            ClaseRecursoID = Convert.ToInt32(reader["CLASE_RECURSO_ID"]),
                            StrClaseProducto = Convert.ToString(reader["CLASE_PRODUCTO"]),
                            StrClaseRecurso = Convert.ToString(reader["CLASE_RECURSO"]),
                            Salvoconducto = Convert.ToBoolean(reader["SALVOCONDUCTO"]),
                            Aprovechamiento = Convert.ToBoolean(reader["APROVECHAMIENTO"]),
                            CodigoIdeam = Convert.ToString(reader["CODIGO_IDEAM"])
                        });
                    }
                }
                return LstClaseProducto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

            
        public List<ParametrizacionSunlIdentity.TipoProducto> ListarTipoProducto(string strTipoPrdoucto)
        {
            try
            {
                List<ParametrizacionSunlIdentity.TipoProducto> LstTipoProducto = new List<ParametrizacionSunlIdentity.TipoProducto>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTAR_TIPO_PRODUCTO_SUNL");
                db.AddInParameter(cmd, "P_TIPO_PRODUCTO", DbType.String, strTipoPrdoucto);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        LstTipoProducto.Add(new ParametrizacionSunlIdentity.TipoProducto
                        {
                            TipoProductoID = Convert.ToInt32(reader["TIPO_PRODUCTO_ID"]),
                            StrTipoProducto = Convert.ToString(reader["TIPO_PRODUCTO"]),
                            Formula = Convert.ToString(reader["FORMULA"]),
                            Salvoconducto = Convert.ToBoolean(reader["SALVOCONDUCTO"]),
                            Aprovechamiento = Convert.ToBoolean(reader["APROVECHAMIENTO"]),
                            CodigoIdeam = Convert.ToString(reader["CODIGO_IDEAM"])
                        });
                    }
                }
                return LstTipoProducto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ParametrizacionSunlIdentity.ClaseProductoXTipoProducto> ListarClaseTipoProducto(int pClaseProductoID, int pTipoProductoID )
        {
            try
            {
                List<ParametrizacionSunlIdentity.ClaseProductoXTipoProducto> LstClaseTipoProducto = new List<ParametrizacionSunlIdentity.ClaseProductoXTipoProducto>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTAR_CLASE_TIPO_PRODUCTO_SUNL");
                db.AddInParameter(cmd, "P_CLASE_PRODUCTO_ID", DbType.Int32, pClaseProductoID);
                db.AddInParameter(cmd, "P_TIPO_PRODUCTO_ID", DbType.Int32, pTipoProductoID);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        LstClaseTipoProducto.Add(new ParametrizacionSunlIdentity.ClaseProductoXTipoProducto
                        {
                            ClaseProductoID  = Convert.ToInt32(reader["CLASE_PRODUCTO_ID"]),
                            TipoProductoID  = Convert.ToInt32(reader["TIPO_PRODUCTO_ID"]),
                            StrTipoProducto = Convert.ToString(reader["TIPO_PRODUCTO"]),
                            StrClaseProducto = Convert.ToString(reader["CLASE_PRODUCTO"]),
                            Salvoconducto = Convert.ToBoolean(reader["SALVOCONDUCTO"]),
                            Aprovechamiento = Convert.ToBoolean(reader["APROVECHAMIENTO"])
                        });
                    }
                }
                return LstClaseTipoProducto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ParametrizacionSunlIdentity.TipoProductoUnidadMedida> ListarTipoProductoUnidadMedida(int pTipoProductoID, int pUnidadMedidaID)
        {
            try
            {
                List<ParametrizacionSunlIdentity.TipoProductoUnidadMedida> TipoProductoUnidadMedida = new List<ParametrizacionSunlIdentity.TipoProductoUnidadMedida>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTAR_TIPO_PROD_UNID_MED_SUNL");
                db.AddInParameter(cmd, "P_TIPO_PRODUCTO_ID", DbType.Int32, pTipoProductoID);
                db.AddInParameter(cmd, "P_UNIDAD_MEDIDA_ID", DbType.Int32, pUnidadMedidaID);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        TipoProductoUnidadMedida.Add(new ParametrizacionSunlIdentity.TipoProductoUnidadMedida
                        {
                            TipoProductoID = Convert.ToInt32(reader["TIPO_PRODUCTO_ID"]),
                            StrTipoProducto = Convert.ToString(reader["TIPO_PRODUCTO"]),
                            UnidadMedidaID = Convert.ToInt32(reader["UNIDAD_MEDIDA_ID"]),
                            StrUnidadMedida = Convert.ToString(reader["UNIDAD_MEDIDAD"]),
                        });
                    }
                }
                return TipoProductoUnidadMedida;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void InsertarEspecie(ParametrizacionSunlIdentity.EspecieTaxonomia ObjEspecies)
        {
            string Mensaje = string.Empty;

            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_INSERTAR_ESPECIE_TAXONOMIA_SUNL");
                db.AddInParameter(cmd, "P_ESPECIE_TAXONOMIA_ID", DbType.Int32, ObjEspecies.EspecieTaxonimiaID);
                db.AddInParameter(cmd, "P_NOMBRE_COMUN", DbType.String, ObjEspecies.NombreComun);
                db.AddInParameter(cmd, "P_NOMBRE_CIENTIFICO", DbType.String, ObjEspecies.NombreCientifico);
                db.AddInParameter(cmd, "P_CLASE_RECURSO_ID", DbType.Int32, ObjEspecies.ClaseRecursoID);
                db.AddInParameter(cmd, "P_CODIGO_IDEAM", DbType.String, ObjEspecies.CodigoIdeam);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int InsertarTipoProducto(ParametrizacionSunlIdentity.TipoProducto ObjTipoProducto)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_INSERTAR_TIPO_PRODUCTO_SUNL");
                db.AddInParameter(cmd, "P_TIPO_PRODUCTO_ID", DbType.Int32, ObjTipoProducto.TipoProductoID);
                db.AddInParameter(cmd, "P_TIPO_PRODUCTO", DbType.String, ObjTipoProducto.StrTipoProducto);
                db.AddInParameter(cmd, "P_SALVOCONDUCTO", DbType.Byte, ObjTipoProducto.Salvoconducto);
                db.AddInParameter(cmd, "P_APROVECHAMIENTO", DbType.Byte, ObjTipoProducto.Aprovechamiento);
                db.AddInParameter(cmd, "P_FORMULA", DbType.String, ObjTipoProducto.Formula);
                db.AddInParameter(cmd, "P_CODIGO_IDEAM", DbType.String, ObjTipoProducto.CodigoIdeam);
                db.AddOutParameter(cmd, "P_TIPO_PRODUCTO_ID_OUT", DbType.Int32, 10);
                db.ExecuteNonQuery(cmd);

                ObjTipoProducto.TipoProductoID = Convert.ToInt32(db.GetParameterValue(cmd, "@P_TIPO_PRODUCTO_ID_OUT"));
                return ObjTipoProducto.TipoProductoID;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public int InsertarClaseProducto(ParametrizacionSunlIdentity.ClaseProducto ObjClaseProducto)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_INSERTAR_CLASE_PRODUCTO_SUNL");
                db.AddInParameter(cmd, "@P_CLASE_PRODUCTO_ID", DbType.Int32, ObjClaseProducto.ClaseProductoID);
                db.AddInParameter(cmd, "@P_CLASE_RECURSO_ID", DbType.Int32, ObjClaseProducto.ClaseRecursoID);
                db.AddInParameter(cmd, "@P_CLASE_PRODUCTO", DbType.String, ObjClaseProducto.StrClaseProducto);
                db.AddInParameter(cmd, "@P_SALVOCONDUCTO", DbType.Byte, ObjClaseProducto.Salvoconducto);
                db.AddInParameter(cmd, "@P_APROVECHAMIENTO", DbType.Byte, ObjClaseProducto.Aprovechamiento);
                db.AddInParameter(cmd, "P_CODIGO_IDEAM", DbType.String, ObjClaseProducto.CodigoIdeam);
                db.AddOutParameter(cmd, "P_CLASE_PRODUCTO_ID_OUT", DbType.Int32, 10);
                db.ExecuteNonQuery(cmd);

                ObjClaseProducto.ClaseRecursoID = Convert.ToInt32(db.GetParameterValue(cmd, "@P_CLASE_PRODUCTO_ID_OUT"));
                return ObjClaseProducto.ClaseRecursoID;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void InsertarClaseTipoProducto(ParametrizacionSunlIdentity.ClaseProductoXTipoProducto ObjClaseProductoXTipoProducto)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_INSERTAR_CLASE_PROD_X_TIPO_PROD_SUNL");
                db.AddInParameter(cmd, "P_CLASE_PRODUCTO_ID", DbType.Int32, ObjClaseProductoXTipoProducto.ClaseProductoID);
                db.AddInParameter(cmd, "P_TIPO_PRODUCTO_ID", DbType.String, ObjClaseProductoXTipoProducto.TipoProductoID);
                db.AddInParameter(cmd, "P_SALVOCONDUCTO", DbType.Byte, ObjClaseProductoXTipoProducto.Salvoconducto);
                db.AddInParameter(cmd, "P_APROVECHAMIENTO", DbType.Byte, ObjClaseProductoXTipoProducto.Aprovechamiento);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void InsertarTipoProductoXUnidadMedida (ParametrizacionSunlIdentity.TipoProductoUnidadMedida ObjTipoProductoUnidadMedida)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_INSERTAR_TIPO_PROD_X_UNID_MED_SUNL");
                db.AddInParameter(cmd, "P_TIPO_PRODUCTO_ID", DbType.Int32, ObjTipoProductoUnidadMedida.TipoProductoID);
                db.AddInParameter(cmd, "P_UNIDAD_MEDIDA_ID", DbType.Int32, ObjTipoProductoUnidadMedida.UnidadMedidaID);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    

        public void EliminarClaseTipoProducto(int pClaseProductoId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_ELIMINAR_CLASE_TIPO_PRODUCTO_SUNL");
                db.AddInParameter(cmd, "P_CLASE_PRODUCTO_ID", DbType.Int32, pClaseProductoId);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EliminarTipoProductoUnidadMedida(int pTipoProductoId)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_ELIMINAR_TIPO_PRODUCTO_UNIDAD_MEDIDA_SUNL");
                db.AddInParameter(cmd, "P_TIPO_PRODUCTO_ID", DbType.Int32, pTipoProductoId);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
