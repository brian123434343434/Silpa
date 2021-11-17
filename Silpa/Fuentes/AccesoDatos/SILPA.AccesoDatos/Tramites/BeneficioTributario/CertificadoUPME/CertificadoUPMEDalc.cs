using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.AccesoDatos.Tramites.BeneficioTributario.CertificadoUPME.Entidades;
using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Tramites.BeneficioTributario.CertificadoUPME
{
    public class CertificadoUPMEDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        #region Constructor
        public CertificadoUPMEDalc()
        {
            objConfiguracion = new Configuracion();
        }
        #endregion Constructor

        #region Metodos
        public void InsertarCertificado(ref CertificadoUPMEEntity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                    {
                        0,
                        objIdentity.numeroSILPA,
                        objIdentity.numeroCertificado,
                        objIdentity.tipoIdentificacion,
                        objIdentity.tipoCertificacion,
                        objIdentity.nombreProyecto,
                        objIdentity.ubicacionGeografia,
                        objIdentity.departamento,
                        objIdentity.municipio,
                        objIdentity.energiaAnualGenerada,
                        objIdentity.fuenteNoConvencional,
                        objIdentity.fuenteConvencionalSustituirID,
                        objIdentity.emisionesCO2,
                        objIdentity.calculoANLA,
                        objIdentity.valorTotalInversion,
                        objIdentity.valorIVA,
                        objIdentity.latitud,
                        objIdentity.longitud,
                        objIdentity.etapa,
                        objIdentity.subFuenteConvencionalSustituirID
                    };
                DbCommand cmd = db.GetStoredProcCommand("SP_INSERTAR_CERTIFICADO_UPME", parametros);
                db.ExecuteNonQuery(cmd);
                string _certificadoID = cmd.Parameters["@P_CERTIFICADO_ID"].Value.ToString();
                objIdentity.certificadoID = Int32.Parse(_certificadoID);

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public void insertarBien(bienesEntity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                    {
                        objIdentity.certificadoID,
                        objIdentity.elemento,
                        objIdentity.subpartida_arancelaria,
                        objIdentity.cantidad,
                        objIdentity.marca,
                        objIdentity.modelo,
                        objIdentity.fabricante,
                        objIdentity.proveedor,
                        objIdentity.funcion,
                        objIdentity.valor_total,
                        objIdentity.iva
                    };
                DbCommand cmd = db.GetStoredProcCommand("SP_INSERTAR_CERTIFICADO_BIEN", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public void insertarServicio(serviciosEntity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                    {
                        objIdentity.certificadoID,
                        objIdentity.servicio,
                        objIdentity.proveedor,
                        objIdentity.alcance,
                        objIdentity.valor_total,
                        objIdentity.iva
                    };
                DbCommand cmd = db.GetStoredProcCommand("SP_INSETAR_CERTIFICADO_SERVICIO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public void insertarSolicitante(solicitanteEntity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                    {
                        objIdentity.certificadoID,
                        objIdentity.nombre,
                        objIdentity.sectorProductivo,
                        objIdentity.codigoCIIU,
                        objIdentity.tipoIdentificacion,
                        objIdentity.identificacion,
                        objIdentity.telefono,
                        objIdentity.domicilio,
                        objIdentity.direccion,
                        objIdentity.nombreContacto,
                        objIdentity.emailContacto,
                        objIdentity.telefonoContacto,
                        objIdentity.TipoSolicitante
                    };
                DbCommand cmd = db.GetStoredProcCommand("SP_INSERTAR_CERTIFICADO_SOLICITANTE", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public void guardarSoportePago(string ruta, int certificadoID, string numeroReferencia)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                    {
                        certificadoID,
                        ruta,
                        numeroReferencia
                    };
                DbCommand cmd = db.GetStoredProcCommand("SP_ACTULIZAR_SOPORTE_PAGO_CERTIFICADO_UPME", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public void guardarRutaDescripcionProyecto(string ruta, int certificadoID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                    {
                        certificadoID,
                        ruta
                    };
                DbCommand cmd = db.GetStoredProcCommand("SP_ACTULIZAR_DESCRIPCION_PROYECTO_CERTIFICADO_UPME", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public List<FuenteConvencionalSustituirEntity> lstFuenteConvencionalSustituir()
        {
            List<FuenteConvencionalSustituirEntity> lstDatos = new List<FuenteConvencionalSustituirEntity>();
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPCERUP_LISTA_FUENTE_CONVENCIONAL");
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        lstDatos.Add(new FuenteConvencionalSustituirEntity { fuetenConvencionalID = Convert.ToInt32(reader["FUENTE_CONVENCIONAL_ID"]), descripcion = reader["DESCRIPCION"].ToString() });
                    }
                }
                return lstDatos;

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public List<SubFuenteConvencionalSustituirEntity> lstSubFuenteConvencionalSustituir(int fuenteConvencionalSustituirID)
        {
            List<SubFuenteConvencionalSustituirEntity> lstDatos = new List<SubFuenteConvencionalSustituirEntity>();
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPCERUP_LISTA_SUB_FUENTE_CONVENCIONAL");
                db.AddInParameter(cmd, "P_FUENTE_CONVENCIONAL_ID", DbType.Int32, fuenteConvencionalSustituirID);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        lstDatos.Add(new SubFuenteConvencionalSustituirEntity { subFuenteConvencionalSustituirID = Convert.ToInt32(reader["SUB_FUENTE_CONVENCIAL_ID"]), descripcion = reader["DESCRIPCION"].ToString() });
                    }
                }
                return lstDatos;

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public CertificadoUPMEEntity ConsultaCertificadoUPMEFNCE(int certificadoID)
        {
            try
            {
                CertificadoUPMEEntity objCertificadoUPMEEntity = new CertificadoUPMEEntity();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_CONSULTAR_CERTIFICADO_UPME");
                db.AddInParameter(cmd, "P_CERTIFICADO_ID", DbType.Int32, certificadoID);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        objCertificadoUPMEEntity.certificadoID = Convert.ToInt32(reader["CERTIFICADO_ID"]);
                        objCertificadoUPMEEntity.departamento = reader["DEPARTAMENTO"].ToString();
                        objCertificadoUPMEEntity.tipoIdentificacion = reader["TIPO_IDENTIFICACION"].ToString();
                        objCertificadoUPMEEntity.numeroCertificado = reader["NUMERO_CERTIFICADO"].ToString();
                        objCertificadoUPMEEntity.tipoCertificacion = reader["TIPO_CERTIFICADO"].ToString();
                        objCertificadoUPMEEntity.nombreProyecto = reader["NOMBRE_PROYECTO"].ToString();
                        objCertificadoUPMEEntity.municipio = reader["MUNICIPIO"].ToString();
                        objCertificadoUPMEEntity.etapa = reader["ETAPA"].ToString();
                        objCertificadoUPMEEntity.latitud = Convert.ToDecimal(reader["LATITUD"]);
                        objCertificadoUPMEEntity.longitud = Convert.ToDecimal(reader["LONGITUD"]);
                        objCertificadoUPMEEntity.valorTotalInversion = Convert.ToDecimal(reader["VALOR_TOTAL_INVERSION"]);
                        objCertificadoUPMEEntity.energiaAnualGenerada = Convert.ToDecimal(reader["ENERGIA_ANUAL_GENERA"]);
                        objCertificadoUPMEEntity.fuenteNoConvencional = reader["FUENTE_NO_CONVENCIONAL_UTILIZAR"].ToString();
                        objCertificadoUPMEEntity.fuenteConvencionalSustituirID = Convert.ToInt32(reader["FUENTE_CONVENCIONAL_SUSTITUIR_ID"]);
                        objCertificadoUPMEEntity.fuenteConvencionalSustituir = reader["FUENTE_CONVENCIONAL_SUSTITUIR"].ToString();
                        objCertificadoUPMEEntity.subFuenteConvencionalSustituirID = Convert.ToInt32(reader["SUB_FUENTE_CONVENCIONAL_SUSTITUIR_ID"]);
                        objCertificadoUPMEEntity.subFuenteConvencionalSustituir = reader["SUB_FUENTE_CONVENCIONAL_SUSTITUIR"].ToString();
                        objCertificadoUPMEEntity.emisionesCO2 = Convert.ToDecimal(reader["EMISIONES_CO2"]);
                        objCertificadoUPMEEntity.valorIVA = Convert.ToDecimal(reader["VALOR_IVA"]);
                        objCertificadoUPMEEntity.numeroReferenciaPago = reader["NUMERO_REFERENCIA"].ToString();
                        objCertificadoUPMEEntity.rutaDescripcionProyecto = reader["DESCRIPCION_PROYECTO"].ToString();
                        objCertificadoUPMEEntity.rutaSoportePago = reader["SOPORTE_PAGO"].ToString();
                        objCertificadoUPMEEntity.solicitantePrincial = solicitantePrincipal(certificadoID);
                        objCertificadoUPMEEntity.lstBienes = listaBienes(certificadoID);
                        objCertificadoUPMEEntity.lstServicios = listaServicios(certificadoID);
                        objCertificadoUPMEEntity.lstSolicitanteSecundario = listaSolicitanteSecundario(certificadoID);
                    }
                }
                return objCertificadoUPMEEntity;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public List<bienesEntity> listaBienes(int certificadoID)
        {
            try
            {
                List<bienesEntity> LstBien = new List<bienesEntity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_CONSULTAR_CERTIFICADO_BIEN");
                db.AddInParameter(cmd, "P_CERTIFICADO_ID", DbType.Int32, certificadoID);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        LstBien.Add(new bienesEntity
                        {
                            certificadoID = Convert.ToInt32(reader["CERTIFICADO_ID"]),
                            elemento = reader["ELEMENTO"].ToString(),
                            subpartida_arancelaria = reader["SUBPARTIDA_ARANCELARIA"].ToString(),
                            cantidad = reader["CANTIDAD"].ToString(),
                            marca = reader["MARCA"].ToString(),
                            modelo = reader["MODELO"].ToString(),
                            fabricante = reader["FABRICANTE"].ToString(),
                            proveedor = reader["PROVEEDOR"].ToString(),
                            funcion = reader["FUNCION"].ToString(),
                            valor_total = Convert.ToDecimal(reader["VALOR_TOTAL"]),
                            iva = Convert.ToDecimal(reader["IVA"])
                        });
                    }
                }
                return LstBien;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<serviciosEntity> listaServicios(int certificadoID)
        {
            try
            {
                List<serviciosEntity> LstServicio = new List<serviciosEntity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_CONSULTAR_CERTIFICADO_SERVICIO");
                db.AddInParameter(cmd, "P_CERTIFICADO_ID", DbType.Int32, certificadoID);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        LstServicio.Add(new serviciosEntity
                        {
                            certificadoID = Convert.ToInt32(reader["CERTIFICADO_ID"]),
                            servicio = reader["SERVICIO"].ToString(),
                            proveedor = reader["PROVEEDOR"].ToString(),
                            alcance = reader["ALCANCE"].ToString(),
                            valor_total = Convert.ToDecimal(reader["VALOR_TOTAL"]),
                            iva = Convert.ToDecimal(reader["IVA"])
                        });
                    }
                }
                return LstServicio;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<solicitanteEntity> listaSolicitanteSecundario(int certificadoID)
        {
            try
            {
                List<solicitanteEntity> LstServicio = new List<solicitanteEntity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_CONSULTAR_CERTIFICADO_SOLICITANTE");
                db.AddInParameter(cmd, "P_CERTIFICADO_ID", DbType.Int32, certificadoID);
                db.AddInParameter(cmd, "P_TIPO_SOLICITANTE", DbType.String, "secundario");
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        LstServicio.Add(new solicitanteEntity
                        {
                            solicitanteID = Convert.ToInt32(reader["SOLICITANTE_ID"]),
                            certificadoID = Convert.ToInt32(reader["CERTIFICADO_ID"]),
                            nombre = reader["NOMBRE"].ToString(),
                            sectorProductivo = reader["SECTOR_PRODUCTIVO"].ToString(),
                            codigoCIIU = reader["CODIGO_CIIU"].ToString(),
                            tipoIdentificacion = reader["TIPO_IDENTIFICACION"].ToString(),
                            identificacion = reader["IDENTIFICACION"].ToString(),
                            telefono = reader["TELEFONO"].ToString(),
                            domicilio = reader["DOMICILIO"].ToString(),
                            direccion = reader["DIRECCION"].ToString(),
                            nombreContacto = reader["NOMBRE_CONTACTO"].ToString(),
                            emailContacto = reader["EMAIL_CONTACTO"].ToString(),
                            telefonoContacto = reader["TELEFONO_CONTACTO"].ToString(),
                            TipoSolicitante = enumTipoSolicitante.secundario
                        });
                    }
                }
                return LstServicio;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public solicitanteEntity solicitantePrincipal(int certificadoID)
        {
            try
            {
                solicitanteEntity objsolicitantePrincipal = new solicitanteEntity();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_CONSULTAR_CERTIFICADO_SOLICITANTE");
                db.AddInParameter(cmd, "P_CERTIFICADO_ID", DbType.Int32, certificadoID);
                db.AddInParameter(cmd, "P_TIPO_SOLICITANTE", DbType.String, "principal");
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                            objsolicitantePrincipal.solicitanteID = Convert.ToInt32(reader["SOLICITANTE_ID"]);
                            objsolicitantePrincipal.certificadoID = Convert.ToInt32(reader["CERTIFICADO_ID"]);
                            objsolicitantePrincipal.nombre = reader["NOMBRE"].ToString();
                            objsolicitantePrincipal.sectorProductivo = reader["SECTOR_PRODUCTIVO"].ToString();
                            objsolicitantePrincipal.codigoCIIU = reader["CODIGO_CIIU"].ToString();
                            objsolicitantePrincipal.tipoIdentificacion = reader["TIPO_IDENTIFICACION"].ToString();
                            objsolicitantePrincipal.identificacion = reader["IDENTIFICACION"].ToString();
                            objsolicitantePrincipal.telefono = reader["TELEFONO"].ToString();
                            objsolicitantePrincipal.domicilio = reader["DOMICILIO"].ToString();
                            objsolicitantePrincipal.direccion = reader["DIRECCION"].ToString();
                            objsolicitantePrincipal.nombreContacto = reader["NOMBRE_CONTACTO"].ToString();
                            objsolicitantePrincipal.emailContacto = reader["EMAIL_CONTACTO"].ToString();
                            objsolicitantePrincipal.telefonoContacto = reader["TELEFONO_CONTACTO"].ToString();
                            objsolicitantePrincipal.TipoSolicitante = enumTipoSolicitante.principal;
                    }
                }
                return objsolicitantePrincipal;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion Metodos

    }
}
