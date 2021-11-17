using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Salvoconducto
{
    public class EspecimenNewDalc
    {
        private Configuracion objConfiguracion;

        public EspecimenNewDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public void CrearEspecieSalvoconducto(EspecimenNewIdentity vEspecimenNewIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_INSERTAR_ESPECIE_SALVOCONDUCTO");
                db.AddInParameter(cmd, "P_SALVOCONDUCTO_ID", DbType.Int32, vEspecimenNewIdentity.SalvocoductoID);
                db.AddInParameter(cmd, "P_ESPECIE_TAXONOMIA_ID", DbType.Int32, vEspecimenNewIdentity.EspecieTaxonomiaID);
                db.AddInParameter(cmd, "P_CLASE_PRODUCTO_ID", DbType.Int32, vEspecimenNewIdentity.ClaseProductoID);
                db.AddInParameter(cmd, "P_TIPO_PRODUCTO_ID", DbType.Int32, vEspecimenNewIdentity.TipoProductoId);
                db.AddInParameter(cmd, "P_DESCRIPCION", DbType.String, vEspecimenNewIdentity.Descripcion);
                db.AddInParameter(cmd, "P_CANTIDAD", DbType.Double, vEspecimenNewIdentity.Cantidad);
                db.AddInParameter(cmd, "P_VOLUMEN", DbType.Double, vEspecimenNewIdentity.Volumen);
                db.AddInParameter(cmd, "P_UNIDAD_MEDIDA_ID", DbType.Int32, vEspecimenNewIdentity.UnidadMedidaId);
                db.AddInParameter(cmd, "P_VOLUMEN_BRUTO", DbType.Decimal, vEspecimenNewIdentity.VolumenBruto);
                db.AddInParameter(cmd, "P_CANTIDAD_LETRAS", DbType.String, vEspecimenNewIdentity.CantidadLetras);
                db.AddInParameter(cmd, "P_SALVOCONDUCTO_ORIGEN_ID", DbType.Int32, vEspecimenNewIdentity.SalvoconductoOrigenId);
                db.AddInParameter(cmd, "P_DIMENSIONES", DbType.String, vEspecimenNewIdentity.Dimensiones);
                db.AddInParameter(cmd, "P_APROVECHAMIENTO_ORIGEN_ID", DbType.Int32, vEspecimenNewIdentity.AprovechamientoOrigenId);
                db.AddInParameter(cmd, "P_IDENTIFICACION", DbType.String, vEspecimenNewIdentity.Identificacion);
                db.AddInParameter(cmd, "P_CANTIDAD_MOVIDA", DbType.Double, vEspecimenNewIdentity.CantidadMovido);
                //jmartinez salvoconducto fase 2
                db.AddInParameter(cmd, "P_NOMBRE_COMUN_ESPECIE", DbType.String, vEspecimenNewIdentity.NombreComunEspecie);
                db.AddInParameter(cmd, "P_IDENTITY", DbType.Int32, vEspecimenNewIdentity.Identity);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void EliminarEspeciesSalvoconducto(int SalvoconductoID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_ELIMINAR_ESPECIE_SALVOCONDUCTO");
                db.AddInParameter(cmd, "P_SALVOCONDUCTO_ID", DbType.Int32, SalvoconductoID);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<EspecimenNewIdentity> ListaEspecieSalvoconducto(int pSalvoconductoID)
        {
            try
            {
                List<EspecimenNewIdentity> LstEspecie = new List<EspecimenNewIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_CONSULTAR_ESPECIE_SALVOCONDUCTO");
                db.AddInParameter(cmd, "P_SALVOCONDUCTO_ID", DbType.Int32, pSalvoconductoID);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        LstEspecie.Add(new EspecimenNewIdentity
                        {
                            EspecieSalvoconductoID = Convert.ToInt32(reader["ESPECIA_SALV_ID"]),
                            SalvocoductoID = Convert.ToInt32(reader["SALVOCONDUCTO_ID"]),
                            EspecieTaxonomiaID = Convert.ToInt32(reader["ESPECIE_TAXONOMIA_ID"]),
                            ClaseProductoID = Convert.ToInt32(reader["CLASE_PRODUCTO_ID"]),
                            ClaseProducto = reader["CLASE_PRODUCTO"].ToString(),
                            TipoProductoId = Convert.ToInt32(reader["TIPO_PRODUCTO_ID"]),
                            TipoProducto = reader["TIPO_PRODUCTO"].ToString(),
                            UnidadMedidaId = Convert.ToInt32(reader["UNIDAD_MEDIDA_ID"]),
                            Descripcion = reader["DESCRIPCION"].ToString(),
                            Cantidad = Convert.ToDouble(reader["CANTIDAD"]),
                            Volumen = Convert.ToDouble(reader["VOLUMEN"]),
                            NombreEspecie = reader["NOMBRE_CIENTIFICO"].ToString(),
                            UnidadMedida = reader["UNIDAD_MEDIDAD"].ToString(),
                            CantidadDisponible = Convert.ToDouble(reader["CANTIDAD_DISPONIBLE"]),
                            Dimensiones = reader["DIMENSIONES"].ToString(),
                            Identificacion = reader["IDENTIFICACION"].ToString(),
                            NombreComunEspecie = reader["NOMBRE_COMUN_ESPECIE"].ToString(),
                            CodigoIdeamClaseRecurso = reader["CODIGO_IDEAM_CLASE_RECURSO"].ToString(),
                            CodigoIdeamEspecie = reader["CODIGO_IDEAM_ESPECIE"].ToString(),
                            CodigoIdeamUnidadMedida = reader["CODIGO_IDEAM_UNIDAD_MEDIDA"].ToString(),
                            CodigoIdeamTipoProducto = reader["CODIGO_IDEAM_TIPO_PRODUCTO"].ToString(),
                            CodigoIdeamClaseProducto = reader["CODIGO_IDEAM_CLASE_PRODUCTO"].ToString(),
                            NumeroSUNLAnterior = reader["NUMERO_SUNL_ANTERIOR"].ToString(),
                            NumeroSUNL = reader["NUMERO_SUNL"].ToString(),
                        });
                    }
                }
                return LstEspecie;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
