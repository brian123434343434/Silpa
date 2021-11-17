using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.AccesoDatos.Generico;
using SILPA.Comun;
using SoftManagement.Log;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.Salvoconducto
{
    public class SalvoconductoNewDalc
    {

        private Configuracion objConfiguracion;
        private RutaDalc vRutaDalc;
        private TransporteDalc vTransporteDalc;


        public SalvoconductoNewDalc()
        {
            objConfiguracion = new Configuracion();
            vRutaDalc = new RutaDalc();
            vTransporteDalc = new TransporteDalc();
        }
        /// <summary>
        /// Crea solicitud de salvoconducto todas empiezan en estado Solicitud.
        /// </summary>
        /// <param name="pSalvoconductoNewIdentity"> Objeto SalvoconductoNewIdentity </param>
        /// <returns>Salvoconducto ID</returns>
        public int CrearSolicitudSalvoconducto(ref SalvoconductoNewIdentity pSalvoconductoNewIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_INSERTA_SOLICITUD_SALVOCONDUCTO");
                db.AddOutParameter(cmd, "P_SALVOCONDUCTO_ID", DbType.Int32, 10);
                db.AddInParameter(cmd, "P_TIPO_SALVOCONDUCTO_ID", DbType.Int32, pSalvoconductoNewIdentity.TipoSalvoconductoID);
                db.AddInParameter(cmd, "P_SOLICITANTE_ID", DbType.Int32, pSalvoconductoNewIdentity.SolicitanteID);
                db.AddInParameter(cmd, "P_NUMERO_VITAL_TRAMITE", DbType.String, pSalvoconductoNewIdentity.NumeroVitalTramite);
                db.AddInParameter(cmd, "P_ESTADO_ID", DbType.Int32, pSalvoconductoNewIdentity.EstadoID);
                db.AddInParameter(cmd, "P_CLASE_RECURSO_ID", DbType.Int32, pSalvoconductoNewIdentity.ClaseRecursoID);
                db.AddInParameter(cmd, "P_DEPTO_PROCEDENCIA_ID", DbType.Int32, pSalvoconductoNewIdentity.DepartamentoProcedenciaID);
                db.AddInParameter(cmd, "P_MUNPIO_PROCEDENCIA_ID", DbType.Int32, pSalvoconductoNewIdentity.MunicipioProcedenciaID);
                db.AddInParameter(cmd, "P_CORREGIMIENTO_PROCEDENCIA", DbType.String, pSalvoconductoNewIdentity.CorregimientoProcedencia);
                db.AddInParameter(cmd, "P_VEREDA_PROCEDENCIA", DbType.String, pSalvoconductoNewIdentity.VeredaProcedencia);
                db.AddInParameter(cmd, "P_ESTABLECIMIENTO_PROCEDENCIA", DbType.String, pSalvoconductoNewIdentity.EstablecimientoProcedencia);
                db.AddInParameter(cmd, "P_FORMA_OTORGAMIENTO_ID", DbType.Int32, pSalvoconductoNewIdentity.FormatOtorgamientoID);
                db.AddInParameter(cmd, "P_MODO_ADQUISICION_ID", DbType.Int32, pSalvoconductoNewIdentity.ModoAdquisicionRecursoID);
                db.AddInParameter(cmd, "P_AUTORIDAD_ID_EMISORA", DbType.Int32, pSalvoconductoNewIdentity.AutoridadEmisoraID);
                db.AddInParameter(cmd, "P_FINALIDAD_ID", DbType.Int32, pSalvoconductoNewIdentity.FinalidadID);
                db.AddInParameter(cmd, "P_FECHA_INI_VIGENCIA", DbType.DateTime, pSalvoconductoNewIdentity.FechaInicioVigencia);
                db.AddInParameter(cmd, "P_FECHA_FIN_VIGENCIA", DbType.DateTime, pSalvoconductoNewIdentity.FechaFinalVigencia);

                db.ExecuteNonQuery(cmd);
                pSalvoconductoNewIdentity.SalvoconductoID = Convert.ToInt32(db.GetParameterValue(cmd, "@P_SALVOCONDUCTO_ID"));
                return pSalvoconductoNewIdentity.SalvoconductoID;
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: CrearSolicitudSalvoconducto -> Error bd: " + sqle.Message + " " + sqle.StackTrace);
                throw sqle;
            }
        }
        public void EliminarSalvoconducto(int pSalvoconductoID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_ELIMINAR_SALVOCONDUCTO");
                db.AddInParameter(cmd, "P_SALVOCONDUCTO_ID", DbType.Int32, pSalvoconductoID);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: EliminarSalvoconducto -> Error bd: " + sqle.Message + " " + sqle.StackTrace);
                throw sqle;
            }
        }
        public List<SalvoconductoNewIdentity> ListaSalvoconducto(DateTime? pFechaInicioSol, DateTime? pFechaFinSol, int? pAutoridadID, int? pSolicitanteID, int? pEstadoID, int? pTipoSalvoconductoID, int? pClaseRecursoID, int? pAutoridadAmbientalID, string pNumeroSalvoconducto)
        {
            try
            {
                List<SalvoconductoNewIdentity> LstSalvoconducto = new List<SalvoconductoNewIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_LISTA_SALVOCONDUCTOS");
                db.AddInParameter(cmd, "P_FECHA_INICIO_SOLICITUD", DbType.DateTime, pFechaInicioSol);
                db.AddInParameter(cmd, "P_FECHA_FIN_SOLICITUD", DbType.DateTime, pFechaFinSol);
                db.AddInParameter(cmd, "P_SOLICITANTE_ID", DbType.Int32, pSolicitanteID);
                db.AddInParameter(cmd, "P_AUTORIDAD_ID", DbType.Int32, pAutoridadID);
                db.AddInParameter(cmd, "P_ESTADO_ID", DbType.Int32, pEstadoID);
                db.AddInParameter(cmd, "P_TIPO_SALVOCONDUCTO_ID", DbType.Int32, pTipoSalvoconductoID);
                db.AddInParameter(cmd, "P_CLASE_RECURSO_ID", DbType.Int32, pClaseRecursoID);
                db.AddInParameter(cmd, "P_NUMERO", DbType.String, pNumeroSalvoconducto);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var vSalvoconductoNewIdentity = new SalvoconductoNewIdentity();
                        vSalvoconductoNewIdentity.SalvoconductoID = Convert.ToInt32(reader["SALVOCONDUCTO_ID"]);
                        vSalvoconductoNewIdentity.TipoSalvoconductoID = Convert.ToInt32(reader["TIPO_SALVOCONDUCTO_ID"]);
                        vSalvoconductoNewIdentity.TipoSalvoconducto = reader["TIPO_SALVOCONDUCTO"].ToString();
                        if (reader["VIGENCIA"].ToString() != string.Empty)
                            vSalvoconductoNewIdentity.Vigencia = Convert.ToInt32(reader["VIGENCIA"]);
                        vSalvoconductoNewIdentity.Numero = reader["NUMERO"].ToString();
                        vSalvoconductoNewIdentity.CodigoSeguridad = reader["CODIGO_SEGURIDAD"].ToString();
                        vSalvoconductoNewIdentity.NumeroVitalTramite = reader["NUMERO_VITAL_TRAMITE"].ToString();
                        vSalvoconductoNewIdentity.EstadoID = Convert.ToInt32(reader["ESTADO_ID"]);
                        vSalvoconductoNewIdentity.Estado = reader["ESTADO"].ToString();
                        if (reader["FECHA_EXPEDICION"].ToString() != string.Empty)
                            vSalvoconductoNewIdentity.FechaExpedicion = Convert.ToDateTime(reader["FECHA_EXPEDICION"]);
                        vSalvoconductoNewIdentity.Archivo = reader["ARCHIVO"].ToString();
                        if (reader["FECHA_INI_VIGENCIA"].ToString() != string.Empty)
                            vSalvoconductoNewIdentity.FechaInicioVigencia = Convert.ToDateTime(reader["FECHA_INI_VIGENCIA"]);
                        if (reader["FECHA_FIN_VIGENCIA"].ToString() != string.Empty)
                            vSalvoconductoNewIdentity.FechaFinalVigencia = Convert.ToDateTime(reader["FECHA_FIN_VIGENCIA"]);
                        vSalvoconductoNewIdentity.ClaseRecursoID = Convert.ToInt32(reader["CLASE_RECURSO_ID"]);
                        vSalvoconductoNewIdentity.ClaseRecurso = reader["CLASE_RECURSO"].ToString();
                        vSalvoconductoNewIdentity.DepartamentoProcedenciaID = Convert.ToInt32(reader["DEPTO_PROCEDENCIA_ID"]);
                        vSalvoconductoNewIdentity.DepartamentoProcedencia = reader["DEP_NOMBRE"].ToString();
                        vSalvoconductoNewIdentity.MunicipioProcedenciaID = Convert.ToInt32(reader["MUNPIO_PROCEDENCIA_ID"]);
                        vSalvoconductoNewIdentity.MunicipioProcedencia = reader["MUN_NOMBRE"].ToString();
                        vSalvoconductoNewIdentity.CorregimientoProcedencia = reader["CORREGIMIENTO_PROCEDENCIA"].ToString();
                        vSalvoconductoNewIdentity.VeredaProcedencia = reader["VEREDA_PROCEDENCIA"].ToString();
                        vSalvoconductoNewIdentity.SolicitanteID = Convert.ToInt32(reader["SOLICITANTE_ID"]);
                        vSalvoconductoNewIdentity.Solicitante = reader["SOLICITANTE"].ToString();
                        if (reader["DEPTO_ORIGEN_ID"].ToString() != string.Empty)
                            vSalvoconductoNewIdentity.DepartamentoOrigenID = Convert.ToInt32(reader["DEPTO_ORIGEN_ID"]);
                        if (reader["MUNICIPIO_ORIGEN_ID"].ToString() != string.Empty)
                            vSalvoconductoNewIdentity.MunicipioOrigenID = Convert.ToInt32(reader["MUNICIPIO_ORIGEN_ID"]);
                        if (reader["ESTABLECIMIENTO_PROCEDENCIA"].ToString() != string.Empty)
                            vSalvoconductoNewIdentity.EstablecimientoProcedencia = reader["ESTABLECIMIENTO_PROCEDENCIA"].ToString();
                        if (reader["FORMA_OTORGAMIENTO_ID"].ToString() != string.Empty)
                        {
                            vSalvoconductoNewIdentity.FormatOtorgamientoID = Convert.ToInt32(reader["FORMA_OTORGAMIENTO_ID"]);
                            vSalvoconductoNewIdentity.FormatOtorgamiento = reader["FORMA_OTORGAMIENTO"].ToString();
                        }
                        if (reader["MODO_ADQUISICION_ID"].ToString() != string.Empty)
                        {
                            vSalvoconductoNewIdentity.ModoAdquisicionRecursoID = Convert.ToInt32(reader["MODO_ADQUISICION_ID"]);
                            vSalvoconductoNewIdentity.ModoAdquisicionRecurso = reader["MODO_ADQUISICION"].ToString();
                        }
                        if (reader["AUTORIDAD_ID_EMISORA"].ToString() != string.Empty)
                        {
                            vSalvoconductoNewIdentity.AutoridadEmisoraID = Convert.ToInt32(reader["AUTORIDAD_ID_EMISORA"]);
                            vSalvoconductoNewIdentity.AutoridadEmisora = reader["AUT_NOMBRE"].ToString();
                        }
                        if (reader["FECHA_SOLICITUD"].ToString() != string.Empty)
                            vSalvoconductoNewIdentity.FechaSolicitud = Convert.ToDateTime(reader["FECHA_SOLICITUD"]);
                        if (reader["FINALIDAD_ID"].ToString() != string.Empty)
                        {
                            vSalvoconductoNewIdentity.FinalidadID = Convert.ToInt32(reader["FINALIDAD_ID"]);
                            vSalvoconductoNewIdentity.Finalidad = reader["FINALIDAD"].ToString();
                        }
                        LstSalvoconducto.Add(vSalvoconductoNewIdentity);
                    }
                }
                return LstSalvoconducto;
            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: ListaSalvoconducto -> Error bd: " + sqle.Message + " " + sqle.StackTrace);
                throw sqle;
            }
            catch (Exception exc)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: ListaSalvoconducto -> Error bd: " + exc.Message + " " + exc.StackTrace);
                throw exc;
            }

        }
        public SalvoconductoNewIdentity ConsultaSalvoconductoXSalvoconductoID(int vSalvoconductoID)
        {
            try
            {
                SalvoconductoNewIdentity vSalvoconductoNewIdentity = new SalvoconductoNewIdentity();
                DireccionPersonaDalc DalcDir = new DireccionPersonaDalc();
                EspecimenNewDalc vEspecimenNewDalc = new EspecimenNewDalc();
                RutaDalc vRutaDalc = new RutaDalc();
                TransporteDalc vTransporteDalc = new TransporteDalc();
                PersonaDalc per = new PersonaDalc();
                DireccionPersonaDalc dir = new DireccionPersonaDalc();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_CONSULTAR_SALVOCONDUCTO");
                db.AddInParameter(cmd, "P_SALVOCONDUCTO_ID", DbType.Int32, vSalvoconductoID);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        vSalvoconductoNewIdentity.SalvoconductoID = Convert.ToInt32(reader["SALVOCONDUCTO_ID"]);
                        vSalvoconductoNewIdentity.TipoSalvoconductoID = Convert.ToInt32(reader["TIPO_SALVOCONDUCTO_ID"]);
                        vSalvoconductoNewIdentity.TipoSalvoconducto = reader["TIPO_SALVOCONDUCTO"].ToString();
                        if (reader["VIGENCIA"].ToString() != string.Empty)
                            vSalvoconductoNewIdentity.Vigencia = Convert.ToInt32(reader["VIGENCIA"]);
                        vSalvoconductoNewIdentity.Numero = reader["NUMERO"].ToString();
                        vSalvoconductoNewIdentity.CodigoSeguridad = reader["CODIGO_SEGURIDAD"].ToString();
                        vSalvoconductoNewIdentity.NumeroVitalTramite = reader["NUMERO_VITAL_TRAMITE"].ToString();
                        vSalvoconductoNewIdentity.EstadoID = Convert.ToInt32(reader["ESTADO_ID"]);
                        vSalvoconductoNewIdentity.Estado = reader["ESTADO"].ToString();
                        if (reader["FECHA_EXPEDICION"].ToString() != string.Empty)
                            vSalvoconductoNewIdentity.FechaExpedicion = Convert.ToDateTime(reader["FECHA_EXPEDICION"]);
                        vSalvoconductoNewIdentity.Archivo = reader["ARCHIVO"].ToString();
                        if (reader["FECHA_INI_VIGENCIA"].ToString() != string.Empty)
                            vSalvoconductoNewIdentity.FechaInicioVigencia = Convert.ToDateTime(reader["FECHA_INI_VIGENCIA"]);
                        if (reader["FECHA_FIN_VIGENCIA"].ToString() != string.Empty)
                            vSalvoconductoNewIdentity.FechaFinalVigencia = Convert.ToDateTime(reader["FECHA_FIN_VIGENCIA"]);
                        vSalvoconductoNewIdentity.ClaseRecursoID = Convert.ToInt32(reader["CLASE_RECURSO_ID"]);
                        vSalvoconductoNewIdentity.ClaseRecurso = reader["CLASE_RECURSO"].ToString();
                        vSalvoconductoNewIdentity.DepartamentoProcedenciaID = Convert.ToInt32(reader["DEPTO_PROCEDENCIA_ID"]);
                        vSalvoconductoNewIdentity.DepartamentoProcedencia = reader["DEP_NOMBRE"].ToString();
                        vSalvoconductoNewIdentity.MunicipioProcedenciaID = Convert.ToInt32(reader["MUNPIO_PROCEDENCIA_ID"]);
                        vSalvoconductoNewIdentity.MunicipioProcedencia = reader["MUN_NOMBRE"].ToString();
                        vSalvoconductoNewIdentity.CorregimientoProcedencia = reader["CORREGIMIENTO_PROCEDENCIA"].ToString();
                        vSalvoconductoNewIdentity.VeredaProcedencia = reader["VEREDA_PROCEDENCIA"].ToString();
                        if (reader["DEPTO_ORIGEN_ID"].ToString() != string.Empty)
                            vSalvoconductoNewIdentity.DepartamentoOrigenID = Convert.ToInt32(reader["DEPTO_ORIGEN_ID"]);
                        if (reader["MUNICIPIO_ORIGEN_ID"].ToString() != string.Empty)
                            vSalvoconductoNewIdentity.MunicipioOrigenID = Convert.ToInt32(reader["MUNICIPIO_ORIGEN_ID"]);
                        if (reader["ESTABLECIMIENTO_PROCEDENCIA"].ToString() != string.Empty)
                            vSalvoconductoNewIdentity.EstablecimientoProcedencia = reader["ESTABLECIMIENTO_PROCEDENCIA"].ToString();
                        if (reader["FORMA_OTORGAMIENTO_ID"].ToString() != string.Empty)
                        {
                            vSalvoconductoNewIdentity.FormatOtorgamientoID = Convert.ToInt32(reader["FORMA_OTORGAMIENTO_ID"]);
                            vSalvoconductoNewIdentity.FormatOtorgamiento = reader["FORMA_OTORGAMIENTO"].ToString();
                        }
                        if (reader["MODO_ADQUISICION_ID"].ToString() != string.Empty)
                        {
                            vSalvoconductoNewIdentity.ModoAdquisicionRecursoID = Convert.ToInt32(reader["MODO_ADQUISICION_ID"]);
                            vSalvoconductoNewIdentity.ModoAdquisicionRecurso = reader["MODO_ADQUISICION"].ToString();
                        }
                        if (reader["AUTORIDAD_ID_EMISORA"].ToString() != string.Empty)
                        {
                            vSalvoconductoNewIdentity.AutoridadEmisoraID = Convert.ToInt32(reader["AUTORIDAD_ID_EMISORA"]);
                            vSalvoconductoNewIdentity.AutoridadEmisora = reader["AUT_NOMBRE"].ToString();
                            vSalvoconductoNewIdentity.NombreImagenAutoridad = reader["IMAGEN"].ToString();
                        }
                        if (reader["FECHA_SOLICITUD"].ToString() != string.Empty)
                            vSalvoconductoNewIdentity.FechaSolicitud = Convert.ToDateTime(reader["FECHA_SOLICITUD"]);
                        if (reader["FINALIDAD_ID"].ToString() != string.Empty)
                        {
                            vSalvoconductoNewIdentity.FinalidadID = Convert.ToInt32(reader["FINALIDAD_ID"]);
                            vSalvoconductoNewIdentity.Finalidad = reader["FINALIDAD"].ToString();
                        }
                        if (reader["SOLICITANTE_ID"].ToString() != string.Empty)
                        {
                            vSalvoconductoNewIdentity.SolicitanteID = Convert.ToInt32(reader["SOLICITANTE_ID"]);
                            vSalvoconductoNewIdentity.Solicitante = reader["SOLICITANTE"].ToString();
                            vSalvoconductoNewIdentity.SolicitanteTitularPersonaIdentity = per.ConsultarPersonaPorIdAppUser(Convert.ToInt64(reader["SOLICITANTE_ID"].ToString()));

                            DireccionPersonaIdentity dirprincipal = new DireccionPersonaIdentity { IdPersona = vSalvoconductoNewIdentity.SolicitanteTitularPersonaIdentity.PersonaId };
                            dir.ObtenerDireccionPersona(ref dirprincipal);
                            vSalvoconductoNewIdentity.SolicitanteTitularPersonaIdentity.DireccionPersona = dirprincipal;
                        }

                        if (reader["ID_TIPO_BLOQUEO"].ToString() != string.Empty && Convert.ToInt32(reader["ID_TIPO_BLOQUEO"]) > 0)
                        {
                            vSalvoconductoNewIdentity.IdTipoBloqueo = Convert.ToInt32(reader["ID_TIPO_BLOQUEO"]);
                            vSalvoconductoNewIdentity.MotivoBloqueo = reader["MOTIVO_BLOQUEO"].ToString();
                        }

                        if (reader["MOTIVO_RECHAZO"].ToString() != string.Empty)
                        {
                            vSalvoconductoNewIdentity.MotivoRechazo = reader["MOTIVO_RECHAZO"].ToString();
                        }
                        if (reader["AUTORIDAD_CAMBIA_ESTADO"].ToString() != string.Empty)
                        {
                            vSalvoconductoNewIdentity.AutoridadCambiaEstado = Convert.ToInt32(reader["AUTORIDAD_CAMBIA_ESTADO"]);
                        }

                        //JMARTINEZ SALVOCONDUCTO FASE 2
                        vSalvoconductoNewIdentity.CodigoIdeamTipoSalvoconducto = reader["CODIGO_IDEAM_TIPO_SALVOCONDUCTO"].ToString();
                        vSalvoconductoNewIdentity.CodigoIdeamFinalidadRecurso = reader["CODIGO_IDEAM_FINALIDAD_RECURSO"].ToString();
                        //bosques.tic SUNL 2020 adicion campos aprovechamiento para sunl preimpresos
                        if (reader["TITULAR_APROVECHAMIENTO"].ToString() != string.Empty)
                        {
                            vSalvoconductoNewIdentity.TitularAprovechamientoSUNLPreimpreso = reader["TITULAR_APROVECHAMIENTO"].ToString();
                        }

                        if (reader["IDENTIFICACION_TITULAR_APROV"].ToString() != string.Empty)
                        {
                            vSalvoconductoNewIdentity.IdentificacionTitularAprovechamientoSUNLPreimpreso = reader["IDENTIFICACION_TITULAR_APROV"].ToString();
                        }

                        if (reader["ACTO_ADMIN_APROV"].ToString() != string.Empty)
                        {
                            vSalvoconductoNewIdentity.ActoAdministrativoAprovechamientoSUNLPreimpreso = reader["ACTO_ADMIN_APROV"].ToString();
                        }
                        if (reader["FEC_ACTO_ADMIN_APROV"].ToString() != string.Empty)
                        {
                            vSalvoconductoNewIdentity.FechaActoAdministrativoAprovechamientoSUNLPreimpreso = Convert.ToDateTime(reader["FEC_ACTO_ADMIN_APROV"]);
                        }

                        if (reader["NUMERO_SUN_ANTERIOR"].ToString() != string.Empty)
                        {
                            vSalvoconductoNewIdentity.NumeroSUNAnterior = reader["NUMERO_SUN_ANTERIOR"].ToString();
                        }

                        vSalvoconductoNewIdentity.AutoridadCargueID = Convert.ToInt32(reader["AUTORIDAD_ID_CARGUE"]);

                        vSalvoconductoNewIdentity.Detalle = string.Format("{0} - {1}", vSalvoconductoNewIdentity.Numero, vSalvoconductoNewIdentity.TipoSalvoconducto);
                        vSalvoconductoNewIdentity.LstEspecimen = vEspecimenNewDalc.ListaEspecieSalvoconducto(vSalvoconductoNewIdentity.SalvoconductoID);
                        vSalvoconductoNewIdentity.LstRuta = vRutaDalc.ListaRutaDesplazamiento(vSalvoconductoNewIdentity.SalvoconductoID);
                        vSalvoconductoNewIdentity.LstTransporte = vTransporteDalc.ListaTransporteSalvoconducto(vSalvoconductoNewIdentity.SalvoconductoID);
                        vSalvoconductoNewIdentity.LstSalvoconductoAnterior = ListaSalvoconductoAnterior(vSalvoconductoNewIdentity.SalvoconductoID);
                        vSalvoconductoNewIdentity.LstAprovechamientoOrigen = ListaAprovechamientoOrigen(vSalvoconductoNewIdentity.SalvoconductoID);
                        if (vSalvoconductoNewIdentity.LstAprovechamientoOrigen.Count == 1)
                        {
                            vSalvoconductoNewIdentity.AprovechamientoID = vSalvoconductoNewIdentity.LstAprovechamientoOrigen.FirstOrDefault().AprovechamientoID;
                            vSalvoconductoNewIdentity.Aprovechamiento = (new Aprovechamiento.AprovechamientoDalc()).ConsultaAprovechamientoXAprovechamientoId(vSalvoconductoNewIdentity.AprovechamientoID.Value);
                        }
                        vSalvoconductoNewIdentity.SalvoconductoAsociados = ConsultarSalvoconductosAsociados(vSalvoconductoID);
                    }
                }
                return vSalvoconductoNewIdentity;
            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: ConsultaSalvoconductoXSalvoconductoID -> Error bd: " + sqle.Message + " " + sqle.StackTrace);
                throw sqle;
            }
            catch (Exception exc)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: ConsultaSalvoconductoXSalvoconductoID -> Error bd: " + exc.Message + " " + exc.StackTrace);
                throw exc;
            }
        }

        private byte[] OptenerImagenByte(string p)
        {

            throw new NotImplementedException();
        }
        public bool NumeroSalvoconducto(int pSalvoconductoID, out string pNumeroSalvoconducto, out string mensaje)
        {
            try
            {

                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_GENERAR_NUMERO_SALVOCONDUCTO");
                db.AddOutParameter(cmd, "P_MENSAJE", DbType.String, 100);
                db.AddOutParameter(cmd, "P_NUMERO_SALVOCONDUCTO", DbType.String, 12);
                db.AddInParameter(cmd, "P_SALVOCONDUCTO_ID", DbType.Int32, pSalvoconductoID);
                db.ExecuteNonQuery(cmd);
                pNumeroSalvoconducto = db.GetParameterValue(cmd, "@P_NUMERO_SALVOCONDUCTO").ToString();
                mensaje = db.GetParameterValue(cmd, "@P_MENSAJE").ToString();

                if (pNumeroSalvoconducto != string.Empty)
                    return true;
                else
                    return false;
            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: NumeroSalvoconducto -> Error bd: " + sqle.Message + " " + sqle.StackTrace);
                throw sqle;
            }
            catch (Exception exc)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: NumeroSalvoconducto -> Error bd: " + exc.Message + " " + exc.StackTrace);
                throw exc;
            }
        }
        public void GenerarSalvoconductoMovilizacion(int pSalvoconductoID, string pUsuarioEmite, string pNumeroSalvoconducto, out string pCodigoSeguridad, out string pNumeroSalvoconductoCompleto)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_GENERAR_SALVOCONDUCTO");
                db.AddInParameter(cmd, "P_NUMERO_SALVOCONDUCTO", DbType.Int32, pNumeroSalvoconducto);
                db.AddOutParameter(cmd, "P_NUMERO_SALVOCONDUCTO_COMPLETO", DbType.String, 150);
                db.AddOutParameter(cmd, "P_CODIGO_SEGURIDAD", DbType.String, 50);
                db.AddInParameter(cmd, "P_SALVOCONDUCTO_ID", DbType.Int32, pSalvoconductoID);
                db.AddInParameter(cmd, "P_USUARIO_CAMBIO_ESTADO", DbType.String, pUsuarioEmite);
                db.ExecuteNonQuery(cmd);
                pCodigoSeguridad = db.GetParameterValue(cmd, "@P_CODIGO_SEGURIDAD").ToString();
                pNumeroSalvoconductoCompleto = db.GetParameterValue(cmd, "@P_NUMERO_SALVOCONDUCTO_COMPLETO").ToString();
            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: GenerarSalvoconductoMovilizacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);
                throw sqle;
            }
            catch (Exception exc)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: GenerarSalvoconductoMovilizacion -> Error bd: " + exc.Message + " " + exc.StackTrace);
                throw exc;
            }
        }
        public void CargarSalvoconducto(ref SalvoconductoNewIdentity pSalvoconductoNewIdentity)
        {
            try
            {

                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_CARGAR_SALVOCONDUCTO");
                db.AddOutParameter(cmd, "P_SALVOCONDUCTO_ID", DbType.Int32, 10);
                db.AddInParameter(cmd, "P_TIPO_SALVOCONDUCTO_ID", DbType.Int32, pSalvoconductoNewIdentity.TipoSalvoconductoID);
                db.AddInParameter(cmd, "P_VIGENCIA", DbType.Int32, pSalvoconductoNewIdentity.Vigencia);
                db.AddInParameter(cmd, "P_NUMERO", DbType.String, pSalvoconductoNewIdentity.Numero);
                db.AddInParameter(cmd, "P_SOLICITANTE_ID", DbType.Int32, pSalvoconductoNewIdentity.SolicitanteID);
                db.AddInParameter(cmd, "P_ESTADO_ID", DbType.Int32, pSalvoconductoNewIdentity.EstadoID);
                db.AddInParameter(cmd, "P_FECHA_EXPEDICION", DbType.DateTime, pSalvoconductoNewIdentity.FechaExpedicion);
                db.AddInParameter(cmd, "P_FECHA_INI_VIGENCIA", DbType.DateTime, pSalvoconductoNewIdentity.FechaInicioVigencia);
                db.AddInParameter(cmd, "P_FECHA_FIN_VIGENCIA", DbType.DateTime, pSalvoconductoNewIdentity.FechaFinalVigencia);
                db.AddInParameter(cmd, "P_CLASE_RECURSO_ID", DbType.Int32, pSalvoconductoNewIdentity.ClaseRecursoID);
                db.AddInParameter(cmd, "P_DEPTO_PROCEDENCIA_ID", DbType.Int32, pSalvoconductoNewIdentity.DepartamentoProcedenciaID);
                db.AddInParameter(cmd, "P_MUNPIO_PROCEDENCIA_ID", DbType.Int32, pSalvoconductoNewIdentity.MunicipioProcedenciaID);
                db.AddInParameter(cmd, "P_CORREGIMIENTO_PROCEDENCIA", DbType.String, pSalvoconductoNewIdentity.CorregimientoProcedencia);
                db.AddInParameter(cmd, "P_VEREDA_PROCEDENCIA", DbType.String, pSalvoconductoNewIdentity.VeredaProcedencia);
                db.AddInParameter(cmd, "P_OBSERVACION", DbType.String, pSalvoconductoNewIdentity.Observacion);
                db.AddInParameter(cmd, "P_FORMA_OTORGAMIENTO_ID", DbType.Int32, pSalvoconductoNewIdentity.FormatOtorgamientoID);
                db.AddInParameter(cmd, "P_MODO_ADQUISICION_ID", DbType.Int32, pSalvoconductoNewIdentity.ModoAdquisicionRecursoID);
                db.AddInParameter(cmd, "P_FINALIDAD_ID", DbType.Int32, pSalvoconductoNewIdentity.FinalidadID);
                db.AddInParameter(cmd, "P_USUARIO_CARGUE", DbType.String, pSalvoconductoNewIdentity.UsuarioCargue);
                db.AddInParameter(cmd, "P_AUTORIDAD_ID_CARGUE", DbType.Int32, pSalvoconductoNewIdentity.AutoridadCargueID);
                db.AddInParameter(cmd, "P_AUTORIDAD_ID_EMISORA", DbType.Int32, pSalvoconductoNewIdentity.AutoridadEmisoraID);
                db.AddInParameter(cmd, "P_AUTORIDAD_ID_OTORGA", DbType.Int32, pSalvoconductoNewIdentity.AutoridadOtorgaID);
                db.AddInParameter(cmd, "P_NUMERO_DOC_OTORGA", DbType.String, pSalvoconductoNewIdentity.NumeroDocOtorga);
                db.AddInParameter(cmd, "P_FECHA_DOC_OTORGA", DbType.DateTime, pSalvoconductoNewIdentity.FechaDocOtorga);
                db.AddInParameter(cmd, "P_NUMERO_SUN_ANTERIOR", DbType.String, pSalvoconductoNewIdentity.NumeroSUNAnterior);
                db.AddInParameter(cmd, "P_ESTABLECIMIENTO_PROCEDENCIA", DbType.String, pSalvoconductoNewIdentity.EstablecimientoProcedencia);
                db.AddInParameter(cmd, "P_SOLICITANTE_ID_OTORGA", DbType.Int32, pSalvoconductoNewIdentity.SolicitanteOtorgaID);
                db.AddInParameter(cmd, "P_TITULAR", DbType.String, pSalvoconductoNewIdentity.TitularSalvoconducto);
                db.AddInParameter(cmd, "P_IDENTIFICACION_TITULAR", DbType.String, pSalvoconductoNewIdentity.IdentificacionTitularSalvocoducto);
                db.ExecuteNonQuery(cmd);
                pSalvoconductoNewIdentity.SalvoconductoID = Convert.ToInt32(db.GetParameterValue(cmd, "@P_SALVOCONDUCTO_ID"));

                foreach (TransporteNewIdentity transporte in pSalvoconductoNewIdentity.LstTransporte)
                {
                    transporte.SalvoconductoID = pSalvoconductoNewIdentity.SalvoconductoID;
                    vTransporteDalc.InsertarTransporteSalvoconducto(transporte);
                }
                // consultamos el origen y el destino ruta del salvoconducto
                if (pSalvoconductoNewIdentity.LstRuta.Count > 0)
                {
                    // obtenemos el primer elemento de la lista de rutas.
                    var rutaOrigen = pSalvoconductoNewIdentity.LstRuta.First();
                    var rutaDestino = pSalvoconductoNewIdentity.LstRuta.Last();
                    // insertarmos el origen y destino del salvoconducto
                    RutaDalc objRutaDalc = new RutaDalc();
                    int rutaID = objRutaDalc.InsertarOrigenDestinoSalvoconducto(rutaOrigen, rutaDestino, pSalvoconductoNewIdentity.SalvoconductoID);

                    // removemos la primer y ultima ruta
                    var lstRutaDesplazamiento = pSalvoconductoNewIdentity.LstRuta;
                    foreach (RutaEntity iRuta in lstRutaDesplazamiento)
                    {
                        iRuta.RutaID = rutaID;
                        objRutaDalc.InsertarRutaDesplazamientoSalconducto(iRuta);
                    }

                }
            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: CargarSalvoconducto -> Error bd: " + sqle.Message + " " + sqle.StackTrace);
                throw sqle;
            }
            catch (Exception exc)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: CargarSalvoconducto -> Error bd: " + exc.Message + " " + exc.StackTrace);
                throw exc;
            }
        }
        public void ActualizarRutaArchivoSaldo(int salvoconductoID, string rutaArchivo)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_ACTALIZAR_RUTA_ARCHIVO_SALDO_SALVOCONDUCTO");
                db.AddInParameter(cmd, "P_SALVOCONDUCTO_ID", DbType.Int32, salvoconductoID);
                db.AddInParameter(cmd, "P_ARCHIVO", DbType.String, rutaArchivo);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: ActualizarRutaArchivoSaldo -> Error bd: " + sqle.Message + " " + sqle.StackTrace);
                throw sqle;
            }
            catch (Exception exc)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: ActualizarRutaArchivoSaldo -> Error bd: " + exc.Message + " " + exc.StackTrace);
                throw exc;
            }
        }
        public List<SalvoconductoNewIdentity> ListaSalvoconducto(int? pAutoridadID, int? pSolicitanteID, int? pEstadoID, int? pTipoSalvoconductoID, int pClaseRecursoID)
        {
            try
            {
                List<SalvoconductoNewIdentity> LstSalvoconducto = new List<SalvoconductoNewIdentity>();
                EspecimenNewDalc vEspecimenNewDalc = new EspecimenNewDalc();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_LISTA_SALVOCONDUCTOS_CLASE_RECURSO");
                db.AddInParameter(cmd, "p_CLASE_RECURSO_ID", DbType.Int32, pClaseRecursoID);
                db.AddInParameter(cmd, "P_SOLICITANTE_ID", DbType.Int32, pSolicitanteID);
                db.AddInParameter(cmd, "P_AUTORIDAD_ID_EMISORA", DbType.Int32, pAutoridadID);
                db.AddInParameter(cmd, "P_ESTADO_ID", DbType.Int32, pEstadoID);
                db.AddInParameter(cmd, "P_TIPO_SALVOCONDUCTO_ID", DbType.Int32, pTipoSalvoconductoID);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var vSalvoconductoNewIdentity = new SalvoconductoNewIdentity();
                        vSalvoconductoNewIdentity.SalvoconductoID = Convert.ToInt32(reader["SALVOCONDUCTO_ID"]);
                        vSalvoconductoNewIdentity.TipoSalvoconductoID = Convert.ToInt32(reader["TIPO_SALVOCONDUCTO_ID"]);
                        vSalvoconductoNewIdentity.TipoSalvoconducto = reader["TIPO_SALVOCONDUCTO"].ToString();
                        if (reader["VIGENCIA"].ToString() != string.Empty)
                            vSalvoconductoNewIdentity.Vigencia = Convert.ToInt32(reader["VIGENCIA"]);
                        vSalvoconductoNewIdentity.Numero = reader["NUMERO"].ToString();
                        vSalvoconductoNewIdentity.CodigoSeguridad = reader["CODIGO_SEGURIDAD"].ToString();
                        vSalvoconductoNewIdentity.NumeroVitalTramite = reader["NUMERO_VITAL_TRAMITE"].ToString();
                        vSalvoconductoNewIdentity.EstadoID = Convert.ToInt32(reader["ESTADO_ID"]);
                        vSalvoconductoNewIdentity.Estado = reader["ESTADO"].ToString();
                        if (reader["FECHA_EXPEDICION"].ToString() != string.Empty)
                            vSalvoconductoNewIdentity.FechaExpedicion = Convert.ToDateTime(reader["FECHA_EXPEDICION"]);
                        vSalvoconductoNewIdentity.Archivo = reader["ARCHIVO"].ToString();
                        if (reader["FECHA_INI_VIGENCIA"].ToString() != string.Empty)
                            vSalvoconductoNewIdentity.FechaInicioVigencia = Convert.ToDateTime(reader["FECHA_INI_VIGENCIA"]);
                        if (reader["FECHA_FIN_VIGENCIA"].ToString() != string.Empty)
                            vSalvoconductoNewIdentity.FechaFinalVigencia = Convert.ToDateTime(reader["FECHA_FIN_VIGENCIA"]);
                        vSalvoconductoNewIdentity.ClaseRecursoID = Convert.ToInt32(reader["CLASE_RECURSO_ID"]);
                        vSalvoconductoNewIdentity.ClaseRecurso = reader["CLASE_RECURSO"].ToString();
                        vSalvoconductoNewIdentity.DepartamentoProcedenciaID = Convert.ToInt32(reader["DEPTO_PROCEDENCIA_ID"]);
                        vSalvoconductoNewIdentity.DepartamentoProcedencia = reader["DEP_NOMBRE"].ToString();
                        vSalvoconductoNewIdentity.MunicipioProcedenciaID = Convert.ToInt32(reader["MUNPIO_PROCEDENCIA_ID"]);
                        vSalvoconductoNewIdentity.MunicipioProcedencia = reader["MUN_NOMBRE"].ToString();
                        vSalvoconductoNewIdentity.CorregimientoProcedencia = reader["CORREGIMIENTO_PROCEDENCIA"].ToString();
                        vSalvoconductoNewIdentity.VeredaProcedencia = reader["VEREDA_PROCEDENCIA"].ToString();
                        if (reader["ESTABLECIMIENTO_PROCEDENCIA"].ToString() != string.Empty)
                            vSalvoconductoNewIdentity.EstablecimientoProcedencia = reader["ESTABLECIMIENTO_PROCEDENCIA"].ToString();
                        if (reader["FORMA_OTORGAMIENTO_ID"].ToString() != string.Empty)
                        {
                            vSalvoconductoNewIdentity.FormatOtorgamientoID = Convert.ToInt32(reader["FORMA_OTORGAMIENTO_ID"]);
                            vSalvoconductoNewIdentity.FormatOtorgamiento = reader["FORMA_OTORGAMIENTO"].ToString();
                        }
                        if (reader["MODO_ADQUISICION_ID"].ToString() != string.Empty)
                        {
                            vSalvoconductoNewIdentity.ModoAdquisicionRecursoID = Convert.ToInt32(reader["MODO_ADQUISICION_ID"]);
                            vSalvoconductoNewIdentity.ModoAdquisicionRecurso = reader["MODO_ADQUISICION"].ToString();
                        }
                        if (reader["AUTORIDAD_ID_EMISORA"].ToString() != string.Empty)
                        {
                            vSalvoconductoNewIdentity.AutoridadEmisoraID = Convert.ToInt32(reader["AUTORIDAD_ID_EMISORA"]);
                            vSalvoconductoNewIdentity.AutoridadEmisora = reader["AUT_NOMBRE"].ToString();
                        }
                        if (reader["FECHA_SOLICITUD"].ToString() != string.Empty)
                            vSalvoconductoNewIdentity.FechaSolicitud = Convert.ToDateTime(reader["FECHA_SOLICITUD"]);
                        if (reader["FINALIDAD_ID"].ToString() != string.Empty)
                        {
                            vSalvoconductoNewIdentity.FinalidadID = Convert.ToInt32(reader["FINALIDAD_ID"]);
                            vSalvoconductoNewIdentity.Finalidad = reader["FINALIDAD"].ToString();
                        }
                        vSalvoconductoNewIdentity.Detalle = string.Format("{0} - {1}", reader["NUMERO"].ToString(), reader["TIPO_SALVOCONDUCTO"].ToString());
                        vSalvoconductoNewIdentity.LstEspecimen = vEspecimenNewDalc.ListaEspecieSalvoconducto(vSalvoconductoNewIdentity.SalvoconductoID);
                        LstSalvoconducto.Add(vSalvoconductoNewIdentity);
                    }
                }
                return LstSalvoconducto;
            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: ListaSalvoconducto -> Error bd: " + sqle.Message + " " + sqle.StackTrace);
                throw sqle;
            }
            catch (Exception exc)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: ListaSalvoconducto -> Error bd: " + exc.Message + " " + exc.StackTrace);
                throw exc;
            }

        }
        public void InsertarSalvoconductoAnterior(int pSalvoconductoID, int pSalvoconductoAnteriorID, string pDetalle)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_INSERTAR_SALVOCONDUCTO_ANTERIOR");
                db.AddInParameter(cmd, "P_SALVOCONDUCTO_ID", DbType.Int32, pSalvoconductoID);
                db.AddInParameter(cmd, "P_SALVOCONDUCTO_ANTERIOR_ID", DbType.Int32, pSalvoconductoAnteriorID);
                db.AddInParameter(cmd, "P_DETALLE", DbType.String, pDetalle);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: InsertarSalvoconductoAnterior -> Error bd: " + sqle.Message + " " + sqle.StackTrace);
                throw sqle;
            }
            catch (Exception exc)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: InsertarSalvoconductoAnterior -> Error bd: " + exc.Message + " " + exc.StackTrace);
                throw exc;
            }
        }
        public void InsertarAprovechamientoOrigen(int pSalvoconductoID, int pAprovechamientoOrigenID, string pDetalle)
        {

            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_INSERTAR_APROVECHAMIENTO_ORIGEN");
                db.AddInParameter(cmd, "P_SALVOCONDUCTO_ID", DbType.Int32, pSalvoconductoID);
                db.AddInParameter(cmd, "P_APROVECHAMIENTO_ID", DbType.Int32, pAprovechamientoOrigenID);
                db.AddInParameter(cmd, "P_DETALLE", DbType.String, pDetalle);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: InsertarAprovechamientoOrigen -> Error bd: " + sqle.Message + " " + sqle.StackTrace);
                throw sqle;
            }
            catch (Exception exc)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: InsertarAprovechamientoOrigen -> Error bd: " + exc.Message + " " + exc.StackTrace);
                throw exc;
            }
        }
        public void ActualizarNumeroVitalRegistro(int p_intSalvoconductoID, string strNumeroVital)
        {
            SqlDatabase objDataBase = null;
            DbCommand objCommand = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("SPSUN_ACTUALIZAR_NUMERO_VITAL_SOLICITUD");
                objDataBase.AddInParameter(objCommand, "@P_SALVOCONDUCTO_ID", DbType.Int32, p_intSalvoconductoID);
                objDataBase.AddInParameter(objCommand, "@P_NUMERO_VITAL_TRAMITE", DbType.String, strNumeroVital);

                //Actualizar
                objDataBase.ExecuteNonQuery(objCommand);
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: ActulaizarNumeroVitalRegistro -> Error bd: " + sqle.Message + " " + sqle.StackTrace);
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: ActulaizarNumeroVitalRegistro -> Error inesperado: " + exc.Message + " " + exc.StackTrace);
            }
        }
        public List<SalvoconductoAnterior> ListaSalvoconductoAnterior(int intSalvoconductoID)
        {
            try
            {
                List<SalvoconductoAnterior> lstSalvoconductoAnt = new List<SalvoconductoAnterior>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_CONSULTA_SALVOCONDUCTO_ANTERIOR");
                db.AddInParameter(cmd, "P_SALVOCONDUCTO_ID", DbType.Int32, intSalvoconductoID);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        lstSalvoconductoAnt.Add(new SalvoconductoAnterior
                        {
                            SalvoconductoID = Convert.ToInt32(reader["SALVOCONDUCTO_ANTERIOR_ID"]),
                            Detalle = reader["DETALLE"].ToString(),
                            Numero = reader["NUMERO_SUNL_ANTERIOR"].ToString()
                        });
                    }
                }
                return lstSalvoconductoAnt;
            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: ListaSalvoconductoAnterior -> Error bd: " + sqle.Message + " " + sqle.StackTrace);
                throw sqle;
            }
            catch (Exception exc)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: ListaSalvoconductoAnterior -> Error bd: " + exc.Message + " " + exc.StackTrace);
                throw exc;
            }
        }
        public List<AprovechamientoOrigen> ListaAprovechamientoOrigen(int intSalvoconductoID)
        {
            try
            {
                List<AprovechamientoOrigen> lstAprovechamientoOrigen = new List<AprovechamientoOrigen>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_CONSULTA_SALVOCONDUCTO_ORIGEN");
                db.AddInParameter(cmd, "P_SALVOCONDUCTO_ID", DbType.Int32, intSalvoconductoID);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        lstAprovechamientoOrigen.Add(new AprovechamientoOrigen
                        {
                            AprovechamientoID = Convert.ToInt32(reader["APROVECHAMIENTO_ID"]),
                            Detalle = reader["DETALLE"].ToString(),
                            numeroAprovechamiento = reader["NUMERO_APROVECHAMIENTO"].ToString()
                        });
                    }
                }
                return lstAprovechamientoOrigen;
            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: ListaAprovechamientoOrigen -> Error bd: " + sqle.Message + " " + sqle.StackTrace);
                throw sqle;
            }
            catch (Exception exc)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: ListaAprovechamientoOrigen -> Error bd: " + exc.Message + " " + exc.StackTrace);
                throw exc;
            }
        }

        public List<NumeracionSalvoconducto> ConsecutivoSalvoconductoDalc(int ID_AUT_AMBIENTAL, int SALVOCONDUCTO_ID)
        {
            try
            {
                List<NumeracionSalvoconducto> AsignarConsecutivo = new List<NumeracionSalvoconducto>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_ASIGNAR_SERIES_SALVOCONDUCTO");
                db.AddInParameter(cmd, "P_ID_AUT_AMBIENTAL", DbType.Int32, ID_AUT_AMBIENTAL);
                db.AddInParameter(cmd, "P_SALVOCONDUCTO_ID", DbType.Int32, SALVOCONDUCTO_ID);

                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        AsignarConsecutivo.Add(new NumeracionSalvoconducto
                        {
                            CONSECUTIVO = Convert.ToInt32(reader["SERIE_NUEVO"]),
                            MENSAJE = reader["RESULTADO"].ToString()

                        });
                    }
                }
                return AsignarConsecutivo;
            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: ConsecutivoSalvoconductoDalc -> Error bd: " + sqle.Message + " " + sqle.StackTrace);
                throw sqle;
            }
            catch (Exception exc)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: ConsecutivoSalvoconductoDalc -> Error bd: " + exc.Message + " " + exc.StackTrace);
                throw exc;
            }
        }

        public void EmitirSalvoconductoDalc(int CONSECUTIVO, int SALVOCONDUCTO_ID, DateTime FEC_EXPEDICION, DateTime FEC_INI_VIGENCIA, DateTime FEC_FIN_VIGENCIA, string USUARIO, String CODIGO_SEGURIDAD)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_EMITIR_SALVOCONDUCTO");
                db.AddInParameter(cmd, "p_CONSECUTIVO", DbType.Int32, CONSECUTIVO);
                db.AddInParameter(cmd, "p_SALVOCONDUCTO_ID", DbType.Int32, SALVOCONDUCTO_ID);
                db.AddInParameter(cmd, "p_FECHA_EXPEDICION", DbType.DateTime, FEC_EXPEDICION);
                db.AddInParameter(cmd, "p_FECHA_INI_VIGENCIA", DbType.DateTime, FEC_INI_VIGENCIA);
                db.AddInParameter(cmd, "p_FECHA_FIN_VIGENCIA", DbType.DateTime, FEC_FIN_VIGENCIA);
                db.AddInParameter(cmd, "p_USUARIO_CAMBIO_ESTADO", DbType.String, USUARIO);
                db.AddInParameter(cmd, "p_CODIGO_SEGURIDAD", DbType.String, CODIGO_SEGURIDAD);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: EmitirSalvoconductoDalc -> Error bd: " + sqle.Message + " " + sqle.StackTrace);
                throw sqle;
            }
            catch (Exception exc)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: EmitirSalvoconductoDalc -> Error bd: " + exc.Message + " " + exc.StackTrace);
                throw exc;
            }
        }

        public void RechazarSalvoconductoDalc(int SALVOCONDUCTO_ID, string MOTIVO_RECHAZO, string USUARIO)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_RECHAZAR_SALVOCONDUCTO");
                db.AddInParameter(cmd, "p_SALVOCONDUCTO_ID", DbType.Int32, SALVOCONDUCTO_ID);
                db.AddInParameter(cmd, "p_MOTIVO_RECHAZO", DbType.String, MOTIVO_RECHAZO);
                db.AddInParameter(cmd, "p_USUARIO_CAMBIO_ESTADO", DbType.String, USUARIO);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: RechazarSalvoconductoDalc -> Error bd: " + sqle.Message + " " + sqle.StackTrace);
                throw sqle;
            }
            catch (Exception exc)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: RechazarSalvoconductoDalc -> Error bd: " + exc.Message + " " + exc.StackTrace);
                throw exc;
            }
        }

        public bool GrabarBloqueoSalvoconducto(int SALVOCONDUCTO_ID, int TIPO_BLOQUEO_ID, string USUARIO, int autoridadCambiaEstado)
        {
            bool respuesta = false;
            try
            {
                List<SeguimientoRutaSalvoconductoIdentity> LstValidacion = new List<SeguimientoRutaSalvoconductoIdentity>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_BLOQUEAR_SALVOCONDUCTO");
                db.AddInParameter(cmd, "P_SALVOCONDUCTO_ID", DbType.Int32, SALVOCONDUCTO_ID);
                db.AddInParameter(cmd, "P_ID_TIPO_BLOQUEO", DbType.Int32, TIPO_BLOQUEO_ID);
                db.AddInParameter(cmd, "P_USUARIO", DbType.String, USUARIO);
                db.AddInParameter(cmd, "P_AUTORIDAD_CAMBIA_ESTADO", DbType.Int32, autoridadCambiaEstado);
                db.ExecuteNonQuery(cmd);
                respuesta = true;
            }
            catch (SqlException sqle)
            {
                respuesta = false;
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: GrabarBloqueoSalvoconducto -> Error bd: " + sqle.Message + " " + sqle.StackTrace);
                throw sqle;
            }
            catch (Exception exc)
            {
                respuesta = false;
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: GrabarBloqueoSalvoconducto -> Error bd: " + exc.Message + " " + exc.StackTrace);
                throw exc;
            }
            return respuesta;
        }

        private DataTable ConsultarSalvoconductosAsociados(int vSalvoconductoID)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("SSP_CONSULTAR_SALVOCONDUCTOS_ASOCIADOS");
            db.AddInParameter(cmd, "P_SALVOCONDUCTO_ID", DbType.Int32, vSalvoconductoID);
            return db.ExecuteDataSet(cmd).Tables[0];
        }

        public void GrabarDesbloqueoSalvoconducto(int salvoconductoID, string motivoDesbloqueo, DateTime fechaDesbloqueo, string usuarioDesbloquea, int autoridadDesbloquea, byte[] soporteDesbloqueo)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SSP_INSERTA_DESBLOQUEO_SALVOCONDUCTO");
                db.AddInParameter(cmd, "P_SALVOCONDUCTO_ID", DbType.Int32, salvoconductoID);
                db.AddInParameter(cmd, "P_MOTIVO_DESBLOQUEO", DbType.String, motivoDesbloqueo);
                db.AddInParameter(cmd, "P_FECHA_DESBLOQUEO", DbType.DateTime, fechaDesbloqueo);
                db.AddInParameter(cmd, "P_USUARIO_DESBLOQUEA", DbType.String, usuarioDesbloquea);
                db.AddInParameter(cmd, "P_AUTORIDAD_DESBLOQUEA", DbType.Int32, autoridadDesbloquea);
                db.AddInParameter(cmd, "P_SOPORTE_DESBLOQUEO", DbType.Binary, soporteDesbloqueo);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: GrabarDesbloqueoSalvoconducto -> Error bd: " + sqle.Message + " " + sqle.StackTrace);
                throw sqle;
            }
            catch (Exception exc)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: GrabarDesbloqueoSalvoconducto -> Error bd: " + exc.Message + " " + exc.StackTrace);
                throw exc;
            }

        }

        public bool VerificarNumeroSalvoconducto(string NumeroSalvoconducto)
        {
            bool SnValida = false;
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_VALIDAR_NUMERO_SUNL");
                db.AddInParameter(cmd, "P_NUMERO_SALVOCONDUCTO", DbType.String, NumeroSalvoconducto);
                db.AddOutParameter(cmd, "P_VALIDA_EXISTE", DbType.Boolean, 0);
                db.ExecuteNonQuery(cmd);
                SnValida = Convert.ToBoolean(db.GetParameterValue(cmd, "@P_VALIDA_EXISTE"));
            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: VerificarNumeroSalvoconducto -> Error bd: " + sqle.Message + " " + sqle.StackTrace);
                throw sqle;
            }
            catch (Exception exc)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: VerificarNumeroSalvoconducto -> Error bd: " + exc.Message + " " + exc.StackTrace);
                throw exc;
            }

            return SnValida;
        }
        
        /// <summary>
        /// Metodo Para Validar El salvocnducto que verifica saldos, integridad referencial en la base de datos
        /// </summary>
        /// <param name="SalvoconductoID"></param>
        /// <returns></returns>
        public DataSet ValidarSalvoconductoDalc(int SalvoconductoID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SPSUN_VERIFICAR_SALVOCONDUCTO");
                db.AddInParameter(cmd, "P_SALVOCONDUCTO_ID", DbType.Int32, SalvoconductoID);
                ds = db.ExecuteDataSet(cmd);
                return ds;
            }
            catch (SqlException sqle)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: ValidarSalvoconductoDalc -> Error bd: " + sqle.Message + " " + sqle.StackTrace);
                throw sqle;
            }
            catch (Exception exc)
            {
                SMLog.Escribir(Severidad.Critico, "SalvoconductoNewDalc :: ValidarSalvoconductoDalc -> Error bd: " + exc.Message + " " + exc.StackTrace);
                throw exc;
            }
        }

    }
}
