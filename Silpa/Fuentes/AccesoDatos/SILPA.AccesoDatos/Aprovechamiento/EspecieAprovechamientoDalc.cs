using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Aprovechamiento
{
    public class EspecieAprovechamientoDalc
    {
        private Configuracion objConfiguracion;

        public EspecieAprovechamientoDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public void CrearEspecieAprovechamiento(EspecieAprovechamientoIdentity vEspecieAprovechamientoIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_INSERTAR_ESPECIE_APROVECHAMIENTO");
                db.AddInParameter(cmd, "P_APROVECHAMIENTO_ID", DbType.Int32, vEspecieAprovechamientoIdentity.AprovechamientoID);
                db.AddInParameter(cmd, "P_ESPECIE_TAXONOMIA_ID", DbType.Int32, vEspecieAprovechamientoIdentity.EspecieTaxonomiaID);
                db.AddInParameter(cmd, "P_UNIDAD_MEDIDA_ID", DbType.Int32, vEspecieAprovechamientoIdentity.UnidadMedidaID);
                db.AddInParameter(cmd, "P_CANTIDAD_ESPECIE", DbType.Double, vEspecieAprovechamientoIdentity.Cantidad);
                db.AddInParameter(cmd, "P_CANTIDAD_ESPECIE_LETRAS", DbType.String, vEspecieAprovechamientoIdentity.CantidadEspecieLetras);
                db.AddInParameter(cmd, "P_CLASE_PRODUCTO_ID", DbType.Int32, vEspecieAprovechamientoIdentity.ClaseProductoID);
                db.AddInParameter(cmd, "P_TIPO_PRODUCTO_ID", DbType.Int32, vEspecieAprovechamientoIdentity.TipoProductoID);
                db.AddInParameter(cmd, "P_CANTIDAD_VOLUMEN_REMANENTE", DbType.Double, vEspecieAprovechamientoIdentity.CantidadVolumenRemanente);
                db.AddInParameter(cmd, "P_CANTIDAD_VOLUMEN_MOV", DbType.Double, vEspecieAprovechamientoIdentity.CantidadVolumenMovilizar);
                db.AddInParameter(cmd, "P_CANTIDAD_VOLUMEN_MOVIDO", DbType.Double, vEspecieAprovechamientoIdentity.CantidadMovido);
                //JMARTINEZ SALVOCONDUCTO FASE 2
                db.AddInParameter(cmd, "P_NOMBRE_COMUN_ESPECIE", DbType.String, vEspecieAprovechamientoIdentity.NombreComunEspecie);

                db.AddInParameter(cmd, "P_DIAMETRO_ALTURA_PECHO", DbType.Double, vEspecieAprovechamientoIdentity.DiametroAlturaPecho);
                db.AddInParameter(cmd, "P_ALTURA_COMERCIAL", DbType.Double, vEspecieAprovechamientoIdentity.DiametroAlturaPecho);
                db.AddInParameter(cmd, "P_TRATAMIENTO_SILVICULTURA_ID", DbType.Int32, vEspecieAprovechamientoIdentity.TratamientoSilviculturaID);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void EliminarEspeciesAprovechamiento(int AprovechamientoID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_ELIMINAR_ESPECIE_APROVECHAMIENTO");
                db.AddInParameter(cmd, "P_APROVECHAMIENTO_ID", DbType.Int32, AprovechamientoID);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<EspecieAprovechamientoIdentity> ListaRecursosAprovechamiento(int AprovechamientoID)
        {
            try
            {
                List<EspecieAprovechamientoIdentity> LstEspecie = new List<EspecieAprovechamientoIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_CONSULTAR_ESPECIE_APROVECHAMIENTO");
                db.AddInParameter(cmd, "P_APROVECHAMIENTO_ID", DbType.Int32, AprovechamientoID);

                //jmartinez Salvoconducto Fase 2
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        LstEspecie.Add(new EspecieAprovechamientoIdentity { 
                            EspecieAprovechamientoID = Convert.ToInt32(reader["ESPECIE_SALVOCONDUCTO_ID"]),
                            AprovechamientoID = Convert.ToInt32(reader["APROVECHAMIENTO_ID"]), 
                            EspecieTaxonomiaID = Convert.ToInt32(reader["ESPECIE_TAXONOMIA_ID"]), 
                            UnidadMedidaID = Convert.ToInt32(reader["UNIDAD_MEDIDA_ID"]), 
                            Cantidad = Convert.ToDouble(reader["CANTIDAD_ESPECIE"]),
                            NombreEspecie = reader["NOMBRE_CIENTIFICO"].ToString(),
                            UnidadMedida = reader["UNIDAD_MEDIDAD"].ToString(),
                            CantidadEspecieLetras = reader["CANTIDAD_ESPECIE_LETRAS"].ToString(),
                            TipoProductoID = Convert.ToInt32(reader["TIPO_PRODUCTO_ID"]),
                            TipoProducto = reader["TIPO_PRODUCTO"].ToString(),
                            ClaseProductoID = Convert.ToInt32(reader["CLASE_PRODUCTO_ID"]),
                            ClaseProducto = reader["CLASE_PRODUCTO"].ToString(),
                            CantidadVolumenMovilizar = reader["CANTIDAD_VOLUMEN_MOVILIZAR"].ToString() != string.Empty ? Convert.ToDouble(reader["CANTIDAD_VOLUMEN_MOVILIZAR"].ToString()) : 0,
                            CantidadVolumenRemanente = reader["CANTIDAD_VOLUMEN_REMANENTE"].ToString() != string.Empty ? Convert.ToDouble(reader["CANTIDAD_VOLUMEN_REMANENTE"].ToString()) : 0,
                            CantidadDisponible = Convert.ToDouble(reader["CANTIDAD_DISPONIBLE"]), //saldo del volumen o cantidad de la especie
                            SaldoCntVolumen = Convert.ToDouble(reader["CANTIDAD_DISPONIBLE"]), //saldo del volumen o cantidad de la especie
                            CntVolumenMovido = Convert.ToDouble(reader["CANTIDAD_MOVIDA"]), //cantidad volumen o cantidad movida de la especie
                            CodigoIDEAMClaseProducto = reader["CODIGO_IDEAM_CLASE_PRODUCTO"].ToString(),
                            CodigoIDEAMEspecie = reader["CODIGO_IDEAM_ESPECIE"].ToString(),
                            CodigoIDEAMUnidadMedida = reader["CODIGO_IDEAM_UNIDAD_MEDIDA"].ToString(),
                            NombreComunEspecie = reader["NOMBRE_COMUN_ESPECIE"].ToString(),
                            CodigoIDEAMTipoProducto = reader["CODIGO_IDEAM_TIPO_PRODUCTO"].ToString(),
                            DiametroAlturaPecho = Convert.ToDouble(reader["DIAMETRO_ALTURA_PECHO"].ToString()),
                            AlturaComercial = Convert.ToDouble(reader["ALTURA_COMERCIAL"].ToString()),
                            TratamientoSilviculturaID  = Convert.ToInt32(reader["TRATAMIENTO_SILVICULTURA_ID"].ToString()),
                            CodigoIdeamTratamientoSilvID = reader["CODIGO_IDEAM_TRATAMIENTO_SILVICULTURA"].ToString(),
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
