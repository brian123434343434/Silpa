using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.AccesoDatos.Generico;
using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Aprovechamiento
{
    public class AprovechamientoDalc
    {
        private Configuracion objConfiguracion;

        public AprovechamientoDalc()
        {
            objConfiguracion = new Configuracion();
        }
        public string CrearAprovechamiento(ref AprovechamientoIdentity vAprovechamientoIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_INSERTAR_APROVECHAMIENTO");
                db.AddInParameter(cmd, "P_NUMERO", DbType.String, vAprovechamientoIdentity.Numero);
                db.AddInParameter(cmd, "P_TIPO_APROVECHAMIENTO_ID", DbType.Int32, vAprovechamientoIdentity.TipoAprovechamientoID);
                db.AddInParameter(cmd, "P_CLASE_RECURSO_ID", DbType.Int32, vAprovechamientoIdentity.ClaseRecursoId);
                db.AddInParameter(cmd, "P_FECHA_EXPEDICION", DbType.DateTime, vAprovechamientoIdentity.FechaExpedicion);
                db.AddInParameter(cmd, "P_MOD_ADQ_RECURSO_ID", DbType.Int32, vAprovechamientoIdentity.ModoAdquisicionRecursoID);
                db.AddInParameter(cmd, "P_DEPTO_PROCEDENCIA_ID", DbType.Int32, vAprovechamientoIdentity.DepartamentoProcedenciaID);
                db.AddInParameter(cmd, "P_MUNPIO_PROCEDENCIA_ID", DbType.Int32, vAprovechamientoIdentity.MunicipioProcedenciaID);
                db.AddInParameter(cmd, "P_CORREGIMIENTO_PROCEDENCIA", DbType.String, vAprovechamientoIdentity.CorregimientoProcedencia);
                db.AddInParameter(cmd, "P_VEREDA_PROCEDENCIA", DbType.String, vAprovechamientoIdentity.VeredaProcedencia);
                db.AddInParameter(cmd, "P_AUTORIDAD_EMISORA_ID", DbType.Int32, vAprovechamientoIdentity.AutoridadEmisoraID);
                db.AddInParameter(cmd, "P_AUTORIDAD_OTORGA_ID", DbType.Int32, vAprovechamientoIdentity.AutoridadOtorgaID);
                db.AddInParameter(cmd, "P_FECHA_DOC_OTORGA", DbType.DateTime, vAprovechamientoIdentity.FechaDocOtorga);
                db.AddInParameter(cmd, "P_NUMERO_DOC_OTORGA", DbType.String, vAprovechamientoIdentity.NumeroDocOtorga);
                db.AddInParameter(cmd, "P_SOL_OTORGA_ID", DbType.Int32, vAprovechamientoIdentity.SolicitanteOtorgaID);
                db.AddInParameter(cmd, "P_FORMA_OTORGAMIENTO_ID", DbType.Int32, vAprovechamientoIdentity.FormatOtorgamientoID);
                db.AddInParameter(cmd, "P_SOL_ID", DbType.Int32, vAprovechamientoIdentity.SolicitanteID);
                db.AddInParameter(cmd, "P_PAIS_PROCEDENCIA", DbType.String, vAprovechamientoIdentity.PaisProcedencia);
                db.AddInParameter(cmd, "P_ESTABLECIMIENTO_PROCEDENCIA", DbType.String, vAprovechamientoIdentity.EstablecimientoProcedencia);
                db.AddInParameter(cmd, "P_FECHA_DESDE", DbType.DateTime, vAprovechamientoIdentity.FechaDesde);
                db.AddInParameter(cmd, "P_FECHA_HASTA", DbType.DateTime, vAprovechamientoIdentity.FechaHasta);
                db.AddInParameter(cmd, "P_FINALIDAD_ID", DbType.Int32, vAprovechamientoIdentity.FinalidadID);
                db.AddInParameter(cmd, "P_PREDIO_ESTABLECIMIENTO", DbType.String, vAprovechamientoIdentity.Predio);
                db.AddInParameter(cmd, "P_USUARIO_REGISTRO", DbType.String, vAprovechamientoIdentity.UsuarioRegistra);
                db.AddInParameter(cmd, "P_AREA_TOTAL_AUTO", DbType.Double, vAprovechamientoIdentity.AreaTotalAutorizada);
                //JMARTINEZ SALVOCONDUCTO FASE 2
                db.AddInParameter(cmd, "P_FECHA_FINALIZACION", DbType.DateTime, vAprovechamientoIdentity.FechaFinalizacion);
                db.AddInParameter(cmd, "P_COD_UBIC_ARBOL_AISLADO", DbType.Int32, vAprovechamientoIdentity.CodigoUbicacionArbolAislado);
                db.AddOutParameter(cmd, "P_APROVECHAMIENTO_ID", DbType.Int32, 0);
                db.ExecuteNonQuery(cmd);
                vAprovechamientoIdentity.AprovechamientoID = Convert.ToInt32(db.GetParameterValue(cmd, "P_APROVECHAMIENTO_ID"));
                return vAprovechamientoIdentity.Numero;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public AprovechamientoIdentity ConsultaAprovechamientoXAprovechamientoId(int pAprovechamientoId)
        {
            try
            {
                AprovechamientoIdentity vAprovechamientoIdentity = new AprovechamientoIdentity();
                PersonaDalc per = new PersonaDalc();
                DireccionPersonaDalc DalcDir = new DireccionPersonaDalc();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_CONSULTAR_APROVECHAMIENTO");
                db.AddInParameter(cmd, "P_APROVECHAMIENTO_ID", DbType.Int32, pAprovechamientoId);

                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        vAprovechamientoIdentity.AprovechamientoID = Convert.ToInt32(reader["APROVECHAMIENTO_ID"]);
                        vAprovechamientoIdentity.Numero = reader["NUMERO"].ToString();
                        vAprovechamientoIdentity.ClaseRecursoId = Convert.ToInt32(reader["CLASE_RECURSO_ID"]);
                        vAprovechamientoIdentity.ClaseRecurso = reader["CLASE_RECURSO"].ToString();
                        vAprovechamientoIdentity.TipoAprovechamiento = reader["TIPO_APROVECHAMIENTO"].ToString();
                        vAprovechamientoIdentity.TipoAprovechamientoID = Convert.ToInt32(reader["TIPO_APROVECHAMIENTO_ID"]);
                        vAprovechamientoIdentity.FechaExpedicion = Convert.ToDateTime(reader["FECHA_EXPEDICION"]);
                        if (reader["MOD_ADQ_RECURSO_ID"].ToString() != string.Empty)
                        {
                            vAprovechamientoIdentity.ModoAdquisicionRecursoID = Convert.ToInt32(reader["MOD_ADQ_RECURSO_ID"]);
                            vAprovechamientoIdentity.ModoAdquisicionRecurso = reader["MODO_ADQUISICION"].ToString();
                        }
                        vAprovechamientoIdentity.DepartamentoProcedenciaID = Convert.ToInt32(reader["DEPTO_PROCEDENCIA_ID"]);
                        vAprovechamientoIdentity.DepartamentoProcedencia = reader["DEP_NOMBRE"].ToString();
                        vAprovechamientoIdentity.MunicipioProcedenciaID = Convert.ToInt32(reader["MUNPIO_PROCEDENCIA_ID"]);
                        vAprovechamientoIdentity.MunicipioProcedencia = reader["MUN_NOMBRE"].ToString();
                        vAprovechamientoIdentity.CorregimientoProcedencia = reader["CORREGIMIENTO_PROCEDENCIA"].ToString();
                        vAprovechamientoIdentity.VeredaProcedencia = reader["VEREDA_PROCEDENCIA"].ToString();
                        if (reader["AUTORIDAD_EMISORA_ID"].ToString() != string.Empty)
                        {
                            vAprovechamientoIdentity.AutoridadEmisoraID = (Int32)(Convert.IsDBNull(reader["AUTORIDAD_EMISORA_ID"]) ? null : reader["AUTORIDAD_EMISORA_ID"]);
                            vAprovechamientoIdentity.AutoridadEmisora = reader["AUT_NOMBRE_EMI"].ToString();
                        }
                        if (reader["AUTORIDAD_OTORGA_ID"].ToString() != string.Empty)
                        {
                            vAprovechamientoIdentity.AutoridadOtorgaID = (Int32)(Convert.IsDBNull(reader["AUTORIDAD_OTORGA_ID"]) ? null : reader["AUTORIDAD_OTORGA_ID"]);
                            vAprovechamientoIdentity.AutoridadOtorga = reader["AUT_NOMBRE_OTO"].ToString();
                        }
                        if (reader["FECHA_DOC_OTORGA"].ToString() != string.Empty)
                            vAprovechamientoIdentity.FechaDocOtorga = Convert.ToDateTime(reader["FECHA_DOC_OTORGA"]);
                        vAprovechamientoIdentity.NumeroDocOtorga = reader["NUMERO_DOC_OTORGA"].ToString();
                        if (reader["SOL_OTORGA_ID"].ToString() != string.Empty)
                        {
                            vAprovechamientoIdentity.SolicitanteOtorgaID = Convert.ToInt32(reader["SOL_OTORGA_ID"]);
                            vAprovechamientoIdentity.SolicitanteOtorga = per.ConsultarPersonaPorIdAppUser(Convert.ToInt64(reader["SOL_OTORGA_ID"]));

                            DireccionPersonaIdentity dirprincipal = new DireccionPersonaIdentity { IdPersona = vAprovechamientoIdentity.SolicitanteOtorga.PersonaId };
                            DalcDir.ObtenerDireccionPersona(ref dirprincipal);
                            vAprovechamientoIdentity.SolicitanteOtorga.DireccionPersona = dirprincipal;
                        }
                        if (reader["FORMA_OTORGAMIENTO_ID"].ToString() != string.Empty)
                        {
                            vAprovechamientoIdentity.FormatOtorgamientoID = Convert.ToInt32(reader["FORMA_OTORGAMIENTO_ID"]);
                            vAprovechamientoIdentity.FormaOtorgamiento = reader["FORMA_OTORGAMIENTO"].ToString();
                        }
                        if (reader["SOL_ID"].ToString() != string.Empty)
                        {
                            vAprovechamientoIdentity.SolicitanteID = Convert.ToInt32(reader["SOL_ID"]);
                            vAprovechamientoIdentity.Solicitante = per.ConsultarPersonaPorIdAppUser(Convert.ToInt64(reader["SOL_ID"]));

                            DireccionPersonaIdentity dirprincipal = new DireccionPersonaIdentity { IdPersona = vAprovechamientoIdentity.Solicitante.PersonaId };
                            DalcDir.ObtenerDireccionPersona(ref dirprincipal);
                            vAprovechamientoIdentity.Solicitante.DireccionPersona = dirprincipal;
                        }
                        vAprovechamientoIdentity.RutaArchivo = reader["RUTA_ARCHIVO"].ToString();
                        vAprovechamientoIdentity.PaisProcedencia = reader["PAIS_PROCEDENCIA"].ToString();
                        vAprovechamientoIdentity.EstablecimientoProcedencia = reader["ESTABLECIMIENTO_PROCEDENCIA"].ToString();
                        vAprovechamientoIdentity.Predio = reader["PREDIO_ESTABLECIMIENTO"].ToString();
                        if (reader["FECHA_DESDE"].ToString() != string.Empty)
                            vAprovechamientoIdentity.FechaDesde = Convert.ToDateTime(reader["FECHA_DESDE"]);
                        if(reader["FECHA_HASTA"].ToString() != string.Empty)
                            vAprovechamientoIdentity.FechaHasta = Convert.ToDateTime(reader["FECHA_HASTA"]);
                        if (reader["FINALIDAD_ID"].ToString() != string.Empty)
                        {
                            vAprovechamientoIdentity.FinalidadID = Convert.ToInt32(reader["FINALIDAD_ID"]);
                            vAprovechamientoIdentity.Finalidad = reader["FINALIDAD"].ToString();
                        }

                        if (!string.IsNullOrEmpty(reader["AREA_TOTAL_AUTO"].ToString()))
                        {
                            vAprovechamientoIdentity.AreaTotalAutorizada = Convert.ToDouble(reader["AREA_TOTAL_AUTO"]);
                        }

                        //jmartinez Salvoconducto Fase 2
                        if (reader["COD_IDEAM_FORMA_OTORGAMIENTO"].ToString() != string.Empty)
                            vAprovechamientoIdentity.CodigoIDEAMFormaOtorgamiento = reader["COD_IDEAM_FORMA_OTORGAMIENTO"].ToString();

                        if (reader["COD_IDEAM_CLASE_RECURSO"].ToString() != string.Empty)
                            vAprovechamientoIdentity.CodigoIDEAMClaseRecurso = reader["COD_IDEAM_CLASE_RECURSO"].ToString();

                        if (reader["FECHA_FINALIZACION"].ToString() != string.Empty)
                            vAprovechamientoIdentity.FechaFinalizacion = Convert.ToDateTime(reader["FECHA_FINALIZACION"].ToString());

                        if (reader["MODO_ADQUISICION_IDEAM"].ToString() != string.Empty)
                            vAprovechamientoIdentity.CodigoIDEAMModoAdquisicion = reader["MODO_ADQUISICION_IDEAM"].ToString();

                        if (reader["COD_UBIC_ARBOL_AISLADO"].ToString() != string.Empty)
                            vAprovechamientoIdentity.CodigoUbicacionArbolAislado = Convert.ToInt32(reader["COD_UBIC_ARBOL_AISLADO"].ToString());

                        if (reader["COD_IDEAM_UBIC_ARBOL_AISLADO"].ToString() != string.Empty)
                            vAprovechamientoIdentity.CodigoIDEAMUbicacionArbolAislado = reader["COD_IDEAM_UBIC_ARBOL_AISLADO"].ToString();



                        EspecieAprovechamientoDalc vEspecieAprovechamientoDalc = new EspecieAprovechamientoDalc();
                        vAprovechamientoIdentity.LstEspecies = vEspecieAprovechamientoDalc.ListaRecursosAprovechamiento(vAprovechamientoIdentity.AprovechamientoID);
                        CoordenadaAprovechamientoDalc vCoordenadaAprovechamientoDalc = new CoordenadaAprovechamientoDalc();
                        vAprovechamientoIdentity.LstCoordenadas = vCoordenadaAprovechamientoDalc.ListaCoordenadasAprovechamiento(vAprovechamientoIdentity.AprovechamientoID);
                    }
                    else
                    {
                        return null;
                    }

                }
                return vAprovechamientoIdentity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<AprovechamientoIdentity> ConsultaAprovechamientoAutoridadSolicitante(int? pAutoridadID, int pUsuarioId,int pClaseRecursoID)
        {
        try
            {
                EspecieAprovechamientoDalc vEspecieAprovechamientoDalc = new EspecieAprovechamientoDalc();
                List<AprovechamientoIdentity> vLstAprovechamientoIdentity = new List<AprovechamientoIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_CONSULTAR_APROVECHAMIENTO_AUT_SOLICITANTE");
                db.AddInParameter(cmd, "P_AUTORIDAD_ID", DbType.Int32, pAutoridadID);
                db.AddInParameter(cmd, "P_SOL_ID", DbType.Int32, pUsuarioId);
                db.AddInParameter(cmd, "P_CLASE_RECURSO_ID", DbType.Int32, pClaseRecursoID);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        vLstAprovechamientoIdentity.Add(new AprovechamientoIdentity()
                        {
                            AprovechamientoID = Convert.ToInt32(reader["APROVECHAMIENTO_ID"]),
                            Numero = reader["NUMERO"].ToString(),
                            ClaseRecursoId = Convert.ToInt32(reader["CLASE_RECURSO_ID"]),
                            TipoAprovechamientoID = Convert.ToInt32(reader["TIPO_APROVECHAMIENTO_ID"] != DBNull.Value ? reader["TIPO_APROVECHAMIENTO_ID"] : null),
                            TipoAprovechamiento = reader["TIPO_APROVECHAMIENTO"].ToString(),
                            FechaExpedicion = Convert.ToDateTime(reader["FECHA_EXPEDICION"]),
                            ModoAdquisicionRecursoID = Convert.ToInt32(reader["MOD_ADQ_RECURSO_ID"] != DBNull.Value ? reader["MOD_ADQ_RECURSO_ID"]: null),
                            DepartamentoProcedenciaID = Convert.ToInt32(reader["DEPTO_PROCEDENCIA_ID"]),
                            MunicipioProcedenciaID = Convert.ToInt32(reader["MUNPIO_PROCEDENCIA_ID"]),
                            CorregimientoProcedencia = reader["CORREGIMIENTO_PROCEDENCIA"].ToString(),
                            VeredaProcedencia = reader["VEREDA_PROCEDENCIA"].ToString(),
                            FormatOtorgamientoID = Convert.ToInt32(reader["FORMA_OTORGAMIENTO_ID"]!= DBNull.Value ? reader["FORMA_OTORGAMIENTO_ID"]: null),
                            AutoridadEmisoraID = pAutoridadID,
                            SolicitanteID = pUsuarioId,
                            Detalle = string.Format("{0} - {1} - {2} - {3}", reader["NUMERO"].ToString(), Convert.ToDateTime(reader["FECHA_EXPEDICION"]).ToShortDateString(), reader["AUT_NOMBRE_EMI"].ToString(), reader["TIPO_APROVECHAMIENTO"].ToString()),
                            LstEspecies = vEspecieAprovechamientoDalc.ListaRecursosAprovechamiento(Convert.ToInt32(reader["APROVECHAMIENTO_ID"]))
                        });
                    }
                }
                return vLstAprovechamientoIdentity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EliminarCargueAprovechamiento(int AprovechamientoID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_ELIMINAR_APROVECHAMIENTO");
                db.AddInParameter(cmd, "P_APROVECHAMIENTO_ID", DbType.Int32, AprovechamientoID);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void ActualizarRutaArchivoSaldoAprovechamiento(int aprovechamientoID, string rutaArchivo)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_ACTALIZAR_RUTA_ARCHIVO_SALDO_APROVECHAMIENTO");
                db.AddInParameter(cmd, "P_APROVECHAMIENTO_ID", DbType.Int32, aprovechamientoID);
                db.AddInParameter(cmd, "P_RUTA_ARCHIVO", DbType.String, rutaArchivo);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Metodo para consultar los documentos cargados manualmente
        /// </summary>
        /// <param name="autoridadID"></param>
        /// <param name="strNumeroActo"></param>
        /// <param name="intSolicitanteId"></param>
        /// <param name="fechaDesde"></param>
        /// <param name="fechaHasta"></param>
        /// <param name="intClaseRecursoID"></param>
        /// <param name="intFormaOtorgamientoID"></param>
        /// <param name="intModoAdquisicionID"></param>
        /// <param name="intDepartametnoProID"></param>
        /// <param name="intMunicipipoProID"></param>
        /// <param name="strNombrePredio"></param>
        /// <param name="intTipoDocumentoID"></param>
        /// <returns></returns>
        public DataTable ConsultaDocumentosCargados(int? autoridadID, string strNumeroActo, int? intSolicitanteId, DateTime? fechaDesde, DateTime? fechaHasta, int? intClaseRecursoID, int? intFormaOtorgamientoID, int? intModoAdquisicionID, int? intDepartametnoProID, int? intMunicipipoProID, string strNombrePredio, int intTipoDocumentoID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_REPORTE_DOCUMENTOS");
                db.AddInParameter(cmd, "P_AUT_ID", DbType.Int32, autoridadID);
                db.AddInParameter(cmd, "P_NUMERO_ACTO", DbType.String, strNumeroActo);
                db.AddInParameter(cmd, "P_SOLICITANTE", DbType.Int32, intSolicitanteId);
                db.AddInParameter(cmd, "P_CLASE_RECURSO_ID", DbType.Int32, intClaseRecursoID);
                db.AddInParameter(cmd, "P_FORMA_OTORGAMIENTO_ID", DbType.Int32, intFormaOtorgamientoID);
                db.AddInParameter(cmd, "P_MODO_ADQUISICION_ID", DbType.Int32, intModoAdquisicionID);
                db.AddInParameter(cmd, "P_DEPTO_PROCEDENCIA_ID", DbType.Int32, intDepartametnoProID);
                db.AddInParameter(cmd, "P_MUN_PROCEDENCIA_ID", DbType.Int32, intMunicipipoProID);
                db.AddInParameter(cmd, "P_NOMBRE_PREDIO", DbType.String, strNombrePredio);
                db.AddInParameter(cmd, "P_FECHA_DESDE", DbType.DateTime, fechaDesde);
                db.AddInParameter(cmd, "P_FECHA_HASTA", DbType.DateTime, fechaHasta);
                db.AddInParameter(cmd, "P_TIPO_DOCUMENTO_ID", DbType.Int32, intTipoDocumentoID);

                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///  Metodo para consultar los documentos cargados manualmente con informacion completa descargable en excel
        /// </summary>
        /// <param name="autoridadID"></param>
        /// <param name="strNumeroActo"></param>
        /// <param name="intSolicitanteId"></param>
        /// <param name="fechaDesde"></param>
        /// <param name="fechaHasta"></param>
        /// <param name="intClaseRecursoID"></param>
        /// <param name="intFormaOtorgamientoID"></param>
        /// <param name="intModoAdquisicionID"></param>
        /// <param name="intDepartametnoProID"></param>
        /// <param name="intMunicipipoProID"></param>
        /// <param name="strNombrePredio"></param>
        /// <param name="intTipoDocumentoID"></param>
        /// <returns></returns>
        public DataTable ConsultaDocumentosCargadosFullInfo(int? autoridadID, string strNumeroActo, int? intSolicitanteId, DateTime? fechaDesde, DateTime? fechaHasta, int? intClaseRecursoID, int? intFormaOtorgamientoID, int? intModoAdquisicionID, int? intDepartametnoProID, int? intMunicipipoProID, string strNombrePredio, int intTipoDocumentoID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_REPORTE_DOCUMENTOS_FULL_INFO");
                db.AddInParameter(cmd, "P_AUT_ID", DbType.Int32, autoridadID);
                db.AddInParameter(cmd, "P_NUMERO_ACTO", DbType.String, strNumeroActo);
                db.AddInParameter(cmd, "P_SOLICITANTE", DbType.Int32, intSolicitanteId);
                db.AddInParameter(cmd, "P_CLASE_RECURSO_ID", DbType.Int32, intClaseRecursoID);
                db.AddInParameter(cmd, "P_FORMA_OTORGAMIENTO_ID", DbType.Int32, intFormaOtorgamientoID);
                db.AddInParameter(cmd, "P_MODO_ADQUISICION_ID", DbType.Int32, intModoAdquisicionID);
                db.AddInParameter(cmd, "P_DEPTO_PROCEDENCIA_ID", DbType.Int32, intDepartametnoProID);
                db.AddInParameter(cmd, "P_MUN_PROCEDENCIA_ID", DbType.Int32, intMunicipipoProID);
                db.AddInParameter(cmd, "P_NOMBRE_PREDIO", DbType.String, strNombrePredio);
                db.AddInParameter(cmd, "P_FECHA_DESDE", DbType.DateTime, fechaDesde);
                db.AddInParameter(cmd, "P_FECHA_HASTA", DbType.DateTime, fechaHasta);
                db.AddInParameter(cmd, "P_TIPO_DOCUMENTO_ID", DbType.Int32, intTipoDocumentoID);

                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public DataTable ConsultaAprovechamientosCargados(int? autoridadID, string strNumeroActo, int? intSolicitanteId, DateTime? fechaDesde, DateTime? fechaHasta, int? intClaseRecursoID, int? intFormaOtorgamientoID, int? intModoAdquisicionID, int? intDepartametnoProID, int? intMunicipipoProID, string strNombrePredio)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_REPORTE_APROVECHAMIENTOS");
                db.AddInParameter(cmd, "P_AUT_ID", DbType.Int32, autoridadID);
                db.AddInParameter(cmd, "P_NUMERO_ACTO", DbType.String, strNumeroActo);
                db.AddInParameter(cmd, "P_SOLICITANTE", DbType.Int32, intSolicitanteId);
                db.AddInParameter(cmd, "P_CLASE_RECURSO_ID", DbType.Int32, intClaseRecursoID);
                db.AddInParameter(cmd, "P_FORMA_OTORGAMIENTO_ID", DbType.Int32, intFormaOtorgamientoID);
                db.AddInParameter(cmd, "P_MODO_ADQUISICION_ID", DbType.Int32, intModoAdquisicionID);
                db.AddInParameter(cmd, "P_DEPTO_PROCEDENCIA_ID", DbType.Int32, intDepartametnoProID);
                db.AddInParameter(cmd, "P_MUN_PROCEDENCIA_ID", DbType.Int32, intMunicipipoProID);
                db.AddInParameter(cmd, "P_NOMBRE_PREDIO", DbType.String, strNombrePredio);
                db.AddInParameter(cmd, "P_FECHA_DESDE", DbType.DateTime, fechaDesde);
                db.AddInParameter(cmd, "P_FECHA_HASTA", DbType.DateTime, fechaHasta);
                return db.ExecuteDataSet(cmd).Tables[0] ;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public DataTable ConsultaAprovechamientosCargadosFullInfo(int? autoridadID, string strNumeroActo, int? intSolicitanteId, DateTime? fechaDesde, DateTime? fechaHasta, int? intClaseRecursoID, int? intFormaOtorgamientoID, int? intModoAdquisicionID, int? intDepartametnoProID, int? intMunicipipoProID, string strNombrePredio)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_REPORTE_APROVECHAMIENTOS_FULL_INFO");
                cmd.CommandTimeout = 2600;
                db.AddInParameter(cmd, "P_AUT_ID", DbType.Int32, autoridadID);
                db.AddInParameter(cmd, "P_NUMERO_ACTO", DbType.String, strNumeroActo);
                db.AddInParameter(cmd, "P_SOLICITANTE", DbType.Int32, intSolicitanteId);
                db.AddInParameter(cmd, "P_CLASE_RECURSO_ID", DbType.Int32, intClaseRecursoID);
                db.AddInParameter(cmd, "P_FORMA_OTORGAMIENTO_ID", DbType.Int32, intFormaOtorgamientoID);
                db.AddInParameter(cmd, "P_MODO_ADQUISICION_ID", DbType.Int32, intModoAdquisicionID);
                db.AddInParameter(cmd, "P_DEPTO_PROCEDENCIA_ID", DbType.Int32, intDepartametnoProID);
                db.AddInParameter(cmd, "P_MUN_PROCEDENCIA_ID", DbType.Int32, intMunicipipoProID);
                db.AddInParameter(cmd, "P_NOMBRE_PREDIO", DbType.String, strNombrePredio);
                db.AddInParameter(cmd, "P_FECHA_DESDE", DbType.DateTime, fechaDesde);
                db.AddInParameter(cmd, "P_FECHA_HASTA", DbType.DateTime, fechaHasta);
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str_ActoAdministrativo"></param>
        /// <param name="intClaseRecursoID"></param>
        /// <param name="int_TipoAprovechamientoID"></param>
        /// <param name="int_autID"></param>
        /// <param name="FecExp"></param>
        /// <returns></returns>
        public bool ValidarActoAdministrativo(string str_ActoAdministrativo, int intClaseRecursoID, int int_TipoAprovechamientoID, int int_autID, DateTime FecExp)
        {
            bool Respuesta = true;

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("SSP_VALIDA_ACTO_ADMIN_APROVECHAMIENTO");
            db.AddInParameter(cmd, "P_ACTO_ADMIN_APROVECHAMIENTO", DbType.String, str_ActoAdministrativo);
            db.AddInParameter(cmd, "P_AUT_ID", DbType.Int32, int_autID);
            db.AddInParameter(cmd, "P_CLASE_RECURSO_ID", DbType.Int32, intClaseRecursoID);
            db.AddInParameter(cmd, "P_TIPO_APROVECHAMIENTO_ID", DbType.Int32, int_TipoAprovechamientoID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Respuesta = Convert.ToBoolean(reader["RESPUESTA"]);
                }
            }
           return Respuesta;
        }
        
    }
}
