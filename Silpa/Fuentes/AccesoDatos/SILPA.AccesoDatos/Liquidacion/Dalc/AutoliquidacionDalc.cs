using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using SoftManagement.Log;
using SILPA.Comun;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.Liquidacion.Entity;

namespace SILPA.AccesoDatos.Liquidacion.Dalc
{
    public class AutoliquidacionDalc
    {
        #region  Objetos

            private Configuracion _objConfiguracion;

        #endregion


        #region  Creadora

            /// <summary>
            /// Creadora
            /// </summary>
            public AutoliquidacionDalc()
            {
                //Creary cargar configuración
                this._objConfiguracion = new Configuracion();
            }

        #endregion


        #region Metodos Privados

            /// <summary>
            /// Guarda la información basica de la solicitud
            /// </summary>
            /// <param name="p_objCommand">SqlCommand con el command que ejecutara las diferentes transacciones</param>
            /// <param name="p_objSolicitud">SolicitudLiquidacionEntity con la información de la solicitud</param>
            private int GuardarSolicitudLiquidacion(SqlCommand p_objCommand, SolicitudLiquidacionEntity p_objSolicitud)
            {
                int intSolicitudLiquidacionID = 0;

                try
                {
                    p_objCommand.CommandText = "AUTOLIQ_INSERTAR_SOLICITUD_LIQUIDACION";
                    p_objCommand.Parameters.Clear();

                    //Cargar parametros
                    p_objCommand.Parameters.Add("@P_AUTOLIQTIPOSOLICITUD_ID", SqlDbType.Int).Value = p_objSolicitud.TipoSolicitud.TipoSolicitudID;
                    p_objCommand.Parameters.Add("@P_AUTOLIQSOLICITUD_ID", SqlDbType.Int).Value = p_objSolicitud.ClaseSolicitud.ClaseSolicitudID;
                    if (p_objSolicitud.Tramite != null && p_objSolicitud.Tramite.TramiteID > 0)
                        p_objCommand.Parameters.Add("@P_AUTOLIQTRAMITE_ID", SqlDbType.Int).Value = p_objSolicitud.Tramite.TramiteID;
                    else
                        p_objCommand.Parameters.Add("@P_AUTOLIQTRAMITE_ID", SqlDbType.Int).Value = DBNull.Value;
                    if (p_objSolicitud.Sector != null && p_objSolicitud.Sector.SectorID > 0)
                        p_objCommand.Parameters.Add("@P_AUTOLIQSECTOR_ID", SqlDbType.Int).Value = p_objSolicitud.Sector.SectorID;
                    else
                        p_objCommand.Parameters.Add("@P_AUTOLIQSECTOR_ID", SqlDbType.Int).Value = DBNull.Value;
                    p_objCommand.Parameters.Add("@P_AUT_ID", SqlDbType.Int).Value = p_objSolicitud.AutoridadAmbiental.IdAutoridad;
                    if (p_objSolicitud.Proyecto != null && p_objSolicitud.Proyecto.ProyectoID > 0)
                        p_objCommand.Parameters.Add("@P_AUTOLIQPROYECTO_ID", SqlDbType.Int).Value = p_objSolicitud.Proyecto.ProyectoID;
                    else
                        p_objCommand.Parameters.Add("@P_AUTOLIQPROYECTO_ID", SqlDbType.Int).Value = DBNull.Value;
                    if (p_objSolicitud.Actividad != null && p_objSolicitud.Actividad.ActividadID > 0)
                        p_objCommand.Parameters.Add("@P_AUTOLIQACTIVIDAD_ID", SqlDbType.Int).Value = p_objSolicitud.Actividad.ActividadID;
                    else
                        p_objCommand.Parameters.Add("@P_AUTOLIQACTIVIDAD_ID", SqlDbType.Int).Value = DBNull.Value;
                    p_objCommand.Parameters.Add("@P_NOMBRE_PROYECTO", SqlDbType.VarChar).Value = p_objSolicitud.NombreProyecto;
                    p_objCommand.Parameters.Add("@P_DESCRIPCION_PROYECTO", SqlDbType.VarChar).Value = p_objSolicitud.DescripcionProyecto;
                    p_objCommand.Parameters.Add("@P_VALOR_PROYECTO", SqlDbType.Decimal).Value = p_objSolicitud.ValorProyecto;
                    p_objCommand.Parameters.Add("@P_VALOR_PROYECTO_LETRAS", SqlDbType.VarChar).Value = p_objSolicitud.ValorProyectoLetras;
                    if (p_objSolicitud.ValorModificacion >= 0)
                        p_objCommand.Parameters.Add("@P_VALOR_MODIFICACION", SqlDbType.Decimal).Value = p_objSolicitud.ValorModificacion;
                    if (!string.IsNullOrEmpty(p_objSolicitud.ValorModificacionLetras))
                        p_objCommand.Parameters.Add("@P_VALOR_MODIFICACION_LETRAS", SqlDbType.VarChar).Value = p_objSolicitud.ValorModificacionLetras;
                    if (p_objSolicitud.ProyectoPINE != null)
                        p_objCommand.Parameters.Add("@P_ES_PROYECTO_PINE", SqlDbType.Bit).Value = p_objSolicitud.ProyectoPINE.Value;
                    else
                        p_objCommand.Parameters.Add("@P_ES_PROYECTO_PINE", SqlDbType.Bit).Value = DBNull.Value;
                    if (p_objSolicitud.ProyectoAguasMaritimas != null)
                        p_objCommand.Parameters.Add("@P_PROYECTO_EN_AGUAS_MARITIMAS", SqlDbType.Bit).Value = p_objSolicitud.ProyectoAguasMaritimas.Value;
                    else
                        p_objCommand.Parameters.Add("@P_PROYECTO_EN_AGUAS_MARITIMAS", SqlDbType.Bit).Value = DBNull.Value;
                    if (p_objSolicitud.Oceano != null && p_objSolicitud.Oceano.OceanoID > 0)
                        p_objCommand.Parameters.Add("@P_AUTOLIQOCEANO_ID", SqlDbType.Int).Value = p_objSolicitud.Oceano.OceanoID;
                    else
                        p_objCommand.Parameters.Add("@P_AUTOLIQOCEANO_ID", SqlDbType.Int).Value = DBNull.Value;
                    p_objCommand.Parameters.Add("@P_SOLICITANTE_ID", SqlDbType.Int).Value = p_objSolicitud.SolicitanteID;
                    if (!string.IsNullOrEmpty(p_objSolicitud.NumeroVITAL))
                        p_objCommand.Parameters.Add("@P_NUMERO_VITAL", SqlDbType.VarChar).Value = p_objSolicitud.NumeroVITAL;
                    else
                        p_objCommand.Parameters.Add("@P_NUMERO_VITAL", SqlDbType.VarChar).Value = DBNull.Value;
                    p_objCommand.Parameters.Add("@P_AUTOLIQESTADOSOLICITUD_ID", SqlDbType.Int).Value = p_objSolicitud.EstadoSolicitud.EstadoSolicitudID;

                    //Ejecuta sentencia
                    using (IDataReader reader = p_objCommand.ExecuteReader())
                    {
                        //Cargar id del certificado
                        if (reader.Read())
                        {
                            intSolicitudLiquidacionID = Convert.ToInt32(reader["AUTOLIQSOLICITUDLIQUIDACION_ID"]);
                        }
                    }
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: GuardarSolicitudLiquidacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: GuardarSolicitudLiquidacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }

                return intSolicitudLiquidacionID;
            }


            /// <summary>
            /// Guarda la información del permiso asociado a una solicitud
            /// </summary>
            /// <param name="p_objCommand">SqlCommand con el command que ejecutara las diferentes transacciones</param>
            /// <param name="p_objPermiso">PermisoSolicitudLiquidacionEntity con la información del permiso</param>
            private void GuardarPermisoSolicitudLiquidacion(SqlCommand p_objCommand, PermisoSolicitudLiquidacionEntity p_objPermiso)
            {
                try
                {
                    p_objCommand.CommandText = "AUTOLIQ_INSERTAR_PERMISO_SOLICITUD_LIQUIDACION";
                    p_objCommand.Parameters.Clear();

                    //Cargar parametros
                    p_objCommand.Parameters.Add("@P_AUTOLIQSOLICITUDLIQUIDACION_ID", SqlDbType.Int).Value = p_objPermiso.SolicitudLiquidacionID;
                    p_objCommand.Parameters.Add("@P_AUTOLIQPERMISO_ID", SqlDbType.Int).Value = p_objPermiso.Permiso.PermisoID;
                    p_objCommand.Parameters.Add("@P_AUT_ID", SqlDbType.Int).Value = p_objPermiso.AutoridadID;
                    p_objCommand.Parameters.Add("@P_NUMERO_PERMISOS", SqlDbType.Int).Value = p_objPermiso.NumeroPermisos;
                    p_objCommand.ExecuteNonQuery();
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: GuardarPermisoLiquidacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: GuardarPermisoLiquidacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }
            }


            /// <summary>
            /// Guarda la información de una region asociada a una solicitud
            /// </summary>
            /// <param name="p_objCommand">SqlCommand con el command que ejecutara las diferentes transacciones</param>
            /// <param name="p_objRegion">RegionSolicitudLiquidacionEntity con la información de la región</param>
            private void GuardarRegionSolicitudLiquidacion(SqlCommand p_objCommand, RegionSolicitudLiquidacionEntity p_objRegion)
            {
                try
                {
                    p_objCommand.CommandText = "AUTOLIQ_INSERTAR_REGION_SOLICITUD_LIQUIDACION";
                    p_objCommand.Parameters.Clear();

                    //Cargar parametros
                    p_objCommand.Parameters.Add("@P_AUTOLIQSOLICITUDLIQUIDACION_ID", SqlDbType.Int).Value = p_objRegion.SolicitudLiquidacionID;
                    p_objCommand.Parameters.Add("@P_AUTOLIQREGION_ID", SqlDbType.Int).Value = p_objRegion.Region.RegionID;
                    p_objCommand.ExecuteNonQuery();
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: GuardarRegionSolicitudLiquidacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: GuardarRegionSolicitudLiquidacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }
            }


            /// <summary>
            /// Guarda la información de una coordenada asociada a una ubicación
            /// </summary>
            /// <param name="p_objCommand">SqlCommand con el command que ejecutara las diferentes transacciones</param>
            /// <param name="p_objCoordenada">CoordenadaUbicacionLiquidacionEntity con la información de la coordenada</param>
            private void GuardarCoordenadaUbicacionSolicitudLiquidacion(SqlCommand p_objCommand, CoordenadaUbicacionLiquidacionEntity p_objCoordenada)
            {
                try
                {
                    p_objCommand.CommandText = "AUTOLIQ_INSERTAR_COORDENADA_SOLICITUD_LIQUIDACION";
                    p_objCommand.Parameters.Clear();

                    //Cargar parametros
                    p_objCommand.Parameters.Add("@P_AUTOLIQUBICACIONSOLICITUDLIQUIDACION_ID", SqlDbType.Int).Value = p_objCoordenada.UbicacionLiquidacionID;
                    p_objCommand.Parameters.Add("@P_AUTOLIQSOLICITUDLIQUIDACION_ID", SqlDbType.Int).Value = p_objCoordenada.SolicitudLiquidacionID;
                    p_objCommand.Parameters.Add("@P_LOCALIZACION", SqlDbType.VarChar).Value = p_objCoordenada.Localizacion;
                    if (p_objCoordenada.TipoGeometria != null && p_objCoordenada.TipoGeometria.TipoGeometriaID > 0)
                        p_objCommand.Parameters.Add("@P_AUTOLIQTIPOGEOMETRIA_ID", SqlDbType.Int).Value = p_objCoordenada.TipoGeometria.TipoGeometriaID;
                    else
                        p_objCommand.Parameters.Add("@P_AUTOLIQTIPOGEOMETRIA_ID", SqlDbType.Int).Value = DBNull.Value;
                    if (p_objCoordenada.TipoCoordenada != null && p_objCoordenada.TipoCoordenada.TipoCoordenadaID > 0)
                        p_objCommand.Parameters.Add("@P_AUTOLIQTIPOCOORDENADA_ID", SqlDbType.Int).Value = p_objCoordenada.TipoCoordenada.TipoCoordenadaID;
                    else
                        p_objCommand.Parameters.Add("@P_AUTOLIQTIPOCOORDENADA_ID", SqlDbType.Int).Value = DBNull.Value;
                    if (p_objCoordenada.OrigenMagna != null && p_objCoordenada.OrigenMagna.OrigenMagnaID > 0)
                        p_objCommand.Parameters.Add("@P_AUTOLIQORIGENMAGNA_ID", SqlDbType.Int).Value = p_objCoordenada.OrigenMagna.OrigenMagnaID;
                    else
                        p_objCommand.Parameters.Add("@P_AUTOLIQORIGENMAGNA_ID", SqlDbType.Int).Value = DBNull.Value;
                    p_objCommand.Parameters.Add("@P_COORDENADA_NORTE", SqlDbType.VarChar).Value = p_objCoordenada.Norte;
                    p_objCommand.Parameters.Add("@P_COORDENADA_ESTE", SqlDbType.VarChar).Value = p_objCoordenada.Este;
                    p_objCommand.ExecuteNonQuery();
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: GuardarCoordenadaUbicacionSolicitudLiquidacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: GuardarCoordenadaUbicacionSolicitudLiquidacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }
            }


            /// <summary>
            /// Guarda la información de la ubicación asociada a una solicitud
            /// </summary>
            /// <param name="p_objCommand">SqlCommand con el command que ejecutara las diferentes transacciones</param>
            /// <param name="p_objRegion">UbicacionLiquidacionEntity con la información de la ubicación</param>
            private void GuardarUbicacionSolicitudLiquidacion(SqlCommand p_objCommand, UbicacionSolicitudLiquidacionEntity p_objUbicacion)
            {
                int intUbicacionID = 0;

                try
                {
                    p_objCommand.CommandText = "AUTOLIQ_INSERTAR_UBICACION_SOLICITUD_LIQUIDACION";
                    p_objCommand.Parameters.Clear();

                    //Cargar parametros
                    p_objCommand.Parameters.Add("@P_AUTOLIQSOLICITUDLIQUIDACION_ID", SqlDbType.Int).Value = p_objUbicacion.SolicitudLiquidacionID;
                    p_objCommand.Parameters.Add("@P_DEP_ID", SqlDbType.Int).Value = p_objUbicacion.Departamento.Id;
                    p_objCommand.Parameters.Add("@P_MUN_ID", SqlDbType.Int).Value = p_objUbicacion.Municipio.Id;
                    if (!string.IsNullOrEmpty(p_objUbicacion.Corregimiento))
                        p_objCommand.Parameters.Add("@P_CORREGIMIENTO", SqlDbType.VarChar).Value = p_objUbicacion.Corregimiento;
                    else
                        p_objCommand.Parameters.Add("@P_CORREGIMIENTO", SqlDbType.VarChar).Value = DBNull.Value;
                    if (!string.IsNullOrEmpty(p_objUbicacion.Vereda))
                        p_objCommand.Parameters.Add("@P_VEREDA", SqlDbType.VarChar).Value = p_objUbicacion.Vereda;
                    else
                        p_objCommand.Parameters.Add("@P_VEREDA", SqlDbType.VarChar).Value = DBNull.Value;                    

                    //Ejecuta sentencia
                    using (IDataReader reader = p_objCommand.ExecuteReader())
                    {
                        //Cargar id del certificado
                        if (reader.Read())
                        {
                            intUbicacionID = Convert.ToInt32(reader["AUTOLIQUBICACIONSOLICITUDLIQUIDACION_ID"]);
                        }
                    }

                    //Verificar si se obtuvo el identificador de la ubicación
                    if (intUbicacionID > 0)
                    {
                        //Verificar si se tiene coordenadas para insertar
                        if (p_objUbicacion.Coordenadas != null && p_objUbicacion.Coordenadas.Count > 0)
                        {
                            //Ciclo que inserta las coordenadas
                            foreach (CoordenadaUbicacionLiquidacionEntity objCoordenada in p_objUbicacion.Coordenadas)
                            {
                                //Cargar el id
                                objCoordenada.SolicitudLiquidacionID = p_objUbicacion.SolicitudLiquidacionID;
                                objCoordenada.UbicacionLiquidacionID = intUbicacionID;

                                //Insertar coordenada
                                this.GuardarCoordenadaUbicacionSolicitudLiquidacion(p_objCommand, objCoordenada);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("No se obtuvo el identificador de la ubicación");
                    }

                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: GuardarUbicacionSolicitudLiquidacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: GuardarUbicacionSolicitudLiquidacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }
            }


            /// <summary>
            /// Guarda la información de la ruta asociada a una liquidación
            /// </summary>
            /// <param name="p_objCommand">SqlCommand con el command que ejecutara las diferentes transacciones</param>
            /// <param name="p_objRuta">RutaLogisticaSolicitudLiquidacionEntity con la información de la ruta</param>
            private void GuardarRutaSolicitudLiquidacion(SqlCommand p_objCommand, RutaLogisticaSolicitudLiquidacionEntity p_objRuta)
            {
                try
                {
                    p_objCommand.CommandText = "AUTOLIQ_INSERTAR_RUTA_SOLICITUD_LIQUIDACION";
                    p_objCommand.Parameters.Clear();

                    //Cargar parametros
                    p_objCommand.Parameters.Add("@P_AUTOLIQSOLICITUDLIQUIDACION_ID", SqlDbType.Int).Value = p_objRuta.SolicitudLiquidacionID;
                    p_objCommand.Parameters.Add("@P_AUTOLIQMEDIOTRANSPORTE_ID", SqlDbType.Int).Value = p_objRuta.MedioTransporte.MedioTransporteID;
                    p_objCommand.Parameters.Add("@P_DEP_ORIGEN_ID", SqlDbType.Int).Value = p_objRuta.DepartamentoOrigen.Id;
                    p_objCommand.Parameters.Add("@P_MUN_ORIGEN_ID", SqlDbType.Int).Value = p_objRuta.MunicipioOrigen.Id;
                    p_objCommand.Parameters.Add("@P_DEP_DESTINO_ID", SqlDbType.Int).Value = p_objRuta.DepartamentoDestino.Id;
                    p_objCommand.Parameters.Add("@P_MUN_DESTINO_ID", SqlDbType.Int).Value = p_objRuta.MunicipioDestino.Id;
                    p_objCommand.Parameters.Add("@P_TIEMPO_APROXIMADO_TRAYECTO", SqlDbType.VarChar).Value = p_objRuta.TiempoAproximadoTrayecto;
                    p_objCommand.ExecuteNonQuery();
                }
                catch (SqlException sqle)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: GuardarRutaSolicitudLiquidacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                    //Escalar error
                    throw sqle;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: GuardarRutaSolicitudLiquidacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }
            }

            
            /// <summary>
            /// Cargar la información contenida en el datatable en el objeto de solicitud de liquidación
            /// </summary>
            /// <param name="p_objDatosSolicitud">DataTable con la información de la solicitud</param>
            /// <param name="p_objSolicitudLiquidacion">SolicitudLiquidacionEntity objeto en el cual se cargara los datos de la solicitud</param>
            private void CargarDatosSolicitudLiquidacion(DataTable p_objDatosSolicitud, ref SolicitudLiquidacionEntity p_objSolicitudLiquidacion)
            {
                try
                {
                    p_objSolicitudLiquidacion.SolicitudLiquidacionID = Convert.ToInt32(p_objDatosSolicitud.Rows[0]["AUTOLIQSOLICITUDLIQUIDACION_ID"]);
                    p_objSolicitudLiquidacion.SolicitanteID = Convert.ToInt32(p_objDatosSolicitud.Rows[0]["SOLICITANTE_ID"]);
                    p_objSolicitudLiquidacion.TipoSolicitud = new TipoSolicitudLiquidacionEntity{ TipoSolicitudID = Convert.ToInt32(p_objDatosSolicitud.Rows[0]["AUTOLIQTIPOSOLICITUD_ID"]), TipoSolicitud = p_objDatosSolicitud.Rows[0]["TIPO_SOLICITUD"].ToString() };
                    p_objSolicitudLiquidacion.ClaseSolicitud = new ClaseSolicitudLiquidacionEntity{ ClaseSolicitudID = Convert.ToInt32(p_objDatosSolicitud.Rows[0]["AUTOLIQSOLICITUD_ID"]), ClaseSolicitud = p_objDatosSolicitud.Rows[0]["SOLICITUD"].ToString() };
                    if(p_objDatosSolicitud.Rows[0]["AUTOLIQTRAMITE_ID"] != System.DBNull.Value && Convert.ToInt32(p_objDatosSolicitud.Rows[0]["AUTOLIQTRAMITE_ID"]) > 0)
                        p_objSolicitudLiquidacion.Tramite = new TramiteLiquidacionEntity{ TramiteID = Convert.ToInt32(p_objDatosSolicitud.Rows[0]["AUTOLIQTRAMITE_ID"]), Tramite = p_objDatosSolicitud.Rows[0]["TRAMITE"].ToString() };
                    else
                        p_objSolicitudLiquidacion.Tramite = null;
                    if(p_objDatosSolicitud.Rows[0]["AUTOLIQSECTOR_ID"] != System.DBNull.Value && Convert.ToInt32(p_objDatosSolicitud.Rows[0]["AUTOLIQSECTOR_ID"]) > 0)
                        p_objSolicitudLiquidacion.Sector = new SectorLiquidacionEntity{ SectorID = Convert.ToInt32(p_objDatosSolicitud.Rows[0]["AUTOLIQSECTOR_ID"]), Sector = p_objDatosSolicitud.Rows[0]["SECTOR"].ToString() };
                    else
                        p_objSolicitudLiquidacion.Sector = null;
                    if(p_objDatosSolicitud.Rows[0]["AUT_ID"] != System.DBNull.Value && Convert.ToInt32(p_objDatosSolicitud.Rows[0]["AUT_ID"]) > 0)
                        p_objSolicitudLiquidacion.AutoridadAmbiental = new AutoridadAmbientalIdentity{ IdAutoridad = Convert.ToInt32(p_objDatosSolicitud.Rows[0]["AUT_ID"]), Nombre = p_objDatosSolicitud.Rows[0]["AUTORIDAD"].ToString() };
                    else
                        p_objSolicitudLiquidacion.AutoridadAmbiental = null;
                    if(p_objDatosSolicitud.Rows[0]["AUTOLIQPROYECTO_ID"] != System.DBNull.Value && Convert.ToInt32(p_objDatosSolicitud.Rows[0]["AUTOLIQPROYECTO_ID"]) > 0)
                        p_objSolicitudLiquidacion.Proyecto = new ProyectoLiquidacionEntity{ ProyectoID = Convert.ToInt32(p_objDatosSolicitud.Rows[0]["AUTOLIQPROYECTO_ID"]), Proyecto = p_objDatosSolicitud.Rows[0]["PROYECTO"].ToString() };
                    else
                        p_objSolicitudLiquidacion.Proyecto = null;
                    if(p_objDatosSolicitud.Rows[0]["AUTOLIQACTIVIDAD_ID"] != System.DBNull.Value && Convert.ToInt32(p_objDatosSolicitud.Rows[0]["AUTOLIQACTIVIDAD_ID"]) > 0)
                        p_objSolicitudLiquidacion.Actividad = new ActividadLiquidacionEntity{ ActividadID = Convert.ToInt32(p_objDatosSolicitud.Rows[0]["AUTOLIQACTIVIDAD_ID"]), Actividad = p_objDatosSolicitud.Rows[0]["ACTIVIDAD"].ToString() };
                    else
                        p_objSolicitudLiquidacion.Actividad = null;
                    p_objSolicitudLiquidacion.NombreProyecto = p_objDatosSolicitud.Rows[0]["NOMBRE_PROYECTO"].ToString();
                    p_objSolicitudLiquidacion.DescripcionProyecto = p_objDatosSolicitud.Rows[0]["DESCRIPCION_PROYECTO"].ToString();
                    p_objSolicitudLiquidacion.ValorProyecto = Convert.ToDecimal(p_objDatosSolicitud.Rows[0]["VALOR_PROYECTO"]);
                    p_objSolicitudLiquidacion.ValorProyectoLetras = p_objDatosSolicitud.Rows[0]["VALOR_PROYECTO_LETRAS"].ToString();
                    p_objSolicitudLiquidacion.ValorModificacion = (p_objDatosSolicitud.Rows[0]["VALOR_MODIFICACION"] != System.DBNull.Value ? Convert.ToDecimal(p_objDatosSolicitud.Rows[0]["VALOR_MODIFICACION"]) : -1);
                    p_objSolicitudLiquidacion.ValorModificacionLetras = (p_objDatosSolicitud.Rows[0]["VALOR_MODIFICACION_LETRAS"] != System.DBNull.Value ? p_objDatosSolicitud.Rows[0]["VALOR_MODIFICACION_LETRAS"].ToString() : "");
                    if(p_objDatosSolicitud.Rows[0]["ES_PROYECTO_PINE"] != System.DBNull.Value)
                        p_objSolicitudLiquidacion.ProyectoPINE = Convert.ToBoolean(p_objDatosSolicitud.Rows[0]["ES_PROYECTO_PINE"]);
                    else
                        p_objSolicitudLiquidacion.ProyectoPINE = null;
                    if(p_objDatosSolicitud.Rows[0]["PROYECTO_EN_AGUAS_MARITIMAS"] != System.DBNull.Value)
                        p_objSolicitudLiquidacion.ProyectoAguasMaritimas = Convert.ToBoolean(p_objDatosSolicitud.Rows[0]["PROYECTO_EN_AGUAS_MARITIMAS"]);
                    else
                        p_objSolicitudLiquidacion.ProyectoAguasMaritimas = null;
                    
                    if(p_objDatosSolicitud.Rows[0]["AUTOLIQOCEANO_ID"] != System.DBNull.Value && Convert.ToInt32(p_objDatosSolicitud.Rows[0]["AUTOLIQOCEANO_ID"]) > 0)
                        p_objSolicitudLiquidacion.Oceano = new OceanoLiquidacionEntity{ OceanoID = Convert.ToInt32(p_objDatosSolicitud.Rows[0]["AUTOLIQOCEANO_ID"]), Oceano = p_objDatosSolicitud.Rows[0]["OCEANO"].ToString() };
                    else
                        p_objSolicitudLiquidacion.Oceano = null;
                    if(p_objDatosSolicitud.Rows[0]["NUMERO_VITAL"] != System.DBNull.Value)
                        p_objSolicitudLiquidacion.NumeroVITAL = p_objDatosSolicitud.Rows[0]["NUMERO_VITAL"].ToString();
                    else
                        p_objSolicitudLiquidacion.NumeroVITAL = null;
                    p_objSolicitudLiquidacion.FechaRadicacionVITAL = (p_objDatosSolicitud.Rows[0]["FECHA_REGISTRO_VITAL"] != System.DBNull.Value ? Convert.ToDateTime(p_objDatosSolicitud.Rows[0]["FECHA_REGISTRO_VITAL"]) : default(DateTime));
                    p_objSolicitudLiquidacion.EstadoSolicitud = new EstadoSolicitudLiquidacionEntity{ EstadoSolicitudID = Convert.ToInt32(p_objDatosSolicitud.Rows[0]["AUTOLIQESTADOSOLICITUD_ID"]), EstadoSolicitud = p_objDatosSolicitud.Rows[0]["ESTADO"].ToString()};
                    p_objSolicitudLiquidacion.FechaCreacion = Convert.ToDateTime(p_objDatosSolicitud.Rows[0]["FECHA_CREACION"]);
                    p_objSolicitudLiquidacion.FechaModificacion = Convert.ToDateTime(p_objDatosSolicitud.Rows[0]["FECHA_ULTIMA_MODIFICACION"]);
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: CargarDatosSolicitudLiquidacion -> Error cargando información al objeto: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }
            }


            /// <summary>
            /// Cargar la información contenida en el datarow en el objeto de solicitud de liquidación
            /// </summary>
            /// <param name="p_objDatosSolicitud">DataRow con la información de la solicitud</param>
            /// <param name="p_objSolicitudLiquidacion">SolicitudLiquidacionEntity objeto en el cual se cargara los datos de la solicitud</param>
            private void CargarDatosSolicitudLiquidacion(DataRow p_objDatosSolicitud, ref SolicitudLiquidacionEntity p_objSolicitudLiquidacion)
            {
                try
                {
                    p_objSolicitudLiquidacion.SolicitudLiquidacionID = Convert.ToInt32(p_objDatosSolicitud["AUTOLIQSOLICITUDLIQUIDACION_ID"]);
                    p_objSolicitudLiquidacion.SolicitanteID = Convert.ToInt32(p_objDatosSolicitud["SOLICITANTE_ID"]);
                    p_objSolicitudLiquidacion.TipoSolicitud = new TipoSolicitudLiquidacionEntity { TipoSolicitudID = Convert.ToInt32(p_objDatosSolicitud["AUTOLIQTIPOSOLICITUD_ID"]), TipoSolicitud = p_objDatosSolicitud["TIPO_SOLICITUD"].ToString() };
                    p_objSolicitudLiquidacion.ClaseSolicitud = new ClaseSolicitudLiquidacionEntity { ClaseSolicitudID = Convert.ToInt32(p_objDatosSolicitud["AUTOLIQSOLICITUD_ID"]), ClaseSolicitud = p_objDatosSolicitud["SOLICITUD"].ToString() };
                    if (p_objDatosSolicitud["AUTOLIQTRAMITE_ID"] != System.DBNull.Value && Convert.ToInt32(p_objDatosSolicitud["AUTOLIQTRAMITE_ID"]) > 0)
                        p_objSolicitudLiquidacion.Tramite = new TramiteLiquidacionEntity { TramiteID = Convert.ToInt32(p_objDatosSolicitud["AUTOLIQTRAMITE_ID"]), Tramite = p_objDatosSolicitud["TRAMITE"].ToString() };
                    else
                        p_objSolicitudLiquidacion.Tramite = null;
                    if (p_objDatosSolicitud["AUTOLIQSECTOR_ID"] != System.DBNull.Value && Convert.ToInt32(p_objDatosSolicitud["AUTOLIQSECTOR_ID"]) > 0)
                        p_objSolicitudLiquidacion.Sector = new SectorLiquidacionEntity { SectorID = Convert.ToInt32(p_objDatosSolicitud["AUTOLIQSECTOR_ID"]), Sector = p_objDatosSolicitud["SECTOR"].ToString() };
                    else
                        p_objSolicitudLiquidacion.Sector = null;
                    if (p_objDatosSolicitud["AUT_ID"] != System.DBNull.Value && Convert.ToInt32(p_objDatosSolicitud["AUT_ID"]) > 0)
                        p_objSolicitudLiquidacion.AutoridadAmbiental = new AutoridadAmbientalIdentity { IdAutoridad = Convert.ToInt32(p_objDatosSolicitud["AUT_ID"]), Nombre = p_objDatosSolicitud["AUTORIDAD"].ToString() };
                    else
                        p_objSolicitudLiquidacion.AutoridadAmbiental = null;
                    if (p_objDatosSolicitud["AUTOLIQPROYECTO_ID"] != System.DBNull.Value && Convert.ToInt32(p_objDatosSolicitud["AUTOLIQPROYECTO_ID"]) > 0)
                        p_objSolicitudLiquidacion.Proyecto = new ProyectoLiquidacionEntity { ProyectoID = Convert.ToInt32(p_objDatosSolicitud["AUTOLIQPROYECTO_ID"]), Proyecto = p_objDatosSolicitud["PROYECTO"].ToString() };
                    else
                        p_objSolicitudLiquidacion.Proyecto = null;
                    if (p_objDatosSolicitud["AUTOLIQACTIVIDAD_ID"] != System.DBNull.Value && Convert.ToInt32(p_objDatosSolicitud["AUTOLIQACTIVIDAD_ID"]) > 0)
                        p_objSolicitudLiquidacion.Actividad = new ActividadLiquidacionEntity { ActividadID = Convert.ToInt32(p_objDatosSolicitud["AUTOLIQACTIVIDAD_ID"]), Actividad = p_objDatosSolicitud["ACTIVIDAD"].ToString() };
                    else
                        p_objSolicitudLiquidacion.Actividad = null;
                    p_objSolicitudLiquidacion.NombreProyecto = p_objDatosSolicitud["NOMBRE_PROYECTO"].ToString();
                    p_objSolicitudLiquidacion.DescripcionProyecto = p_objDatosSolicitud["DESCRIPCION_PROYECTO"].ToString();
                    p_objSolicitudLiquidacion.ValorProyecto = Convert.ToDecimal(p_objDatosSolicitud["VALOR_PROYECTO"]);
                    p_objSolicitudLiquidacion.ValorProyectoLetras = p_objDatosSolicitud["VALOR_PROYECTO_LETRAS"].ToString();
                    p_objSolicitudLiquidacion.ValorModificacion = (p_objDatosSolicitud["VALOR_MODIFICACION"] != System.DBNull.Value ? Convert.ToDecimal(p_objDatosSolicitud["VALOR_MODIFICACION"]) : -1);
                    p_objSolicitudLiquidacion.ValorModificacionLetras = (p_objDatosSolicitud["VALOR_MODIFICACION_LETRAS"] != System.DBNull.Value ? p_objDatosSolicitud["VALOR_MODIFICACION_LETRAS"].ToString() : "");
                    if (p_objDatosSolicitud["ES_PROYECTO_PINE"] != System.DBNull.Value)
                        p_objSolicitudLiquidacion.ProyectoPINE = Convert.ToBoolean(p_objDatosSolicitud["ES_PROYECTO_PINE"]);
                    else
                        p_objSolicitudLiquidacion.ProyectoPINE = null;
                    if (p_objDatosSolicitud["PROYECTO_EN_AGUAS_MARITIMAS"] != System.DBNull.Value)
                        p_objSolicitudLiquidacion.ProyectoAguasMaritimas = Convert.ToBoolean(p_objDatosSolicitud["PROYECTO_EN_AGUAS_MARITIMAS"]);
                    else
                        p_objSolicitudLiquidacion.ProyectoAguasMaritimas = null;

                    if (p_objDatosSolicitud["AUTOLIQOCEANO_ID"] != System.DBNull.Value && Convert.ToInt32(p_objDatosSolicitud["AUTOLIQOCEANO_ID"]) > 0)
                        p_objSolicitudLiquidacion.Oceano = new OceanoLiquidacionEntity { OceanoID = Convert.ToInt32(p_objDatosSolicitud["AUTOLIQOCEANO_ID"]), Oceano = p_objDatosSolicitud["OCEANO"].ToString() };
                    else
                        p_objSolicitudLiquidacion.Oceano = null;
                    if (p_objDatosSolicitud["NUMERO_VITAL"] != System.DBNull.Value)
                        p_objSolicitudLiquidacion.NumeroVITAL = p_objDatosSolicitud["NUMERO_VITAL"].ToString();
                    else
                        p_objSolicitudLiquidacion.NumeroVITAL = null;
                    p_objSolicitudLiquidacion.FechaRadicacionVITAL = (p_objDatosSolicitud["FECHA_REGISTRO_VITAL"] != System.DBNull.Value ? Convert.ToDateTime(p_objDatosSolicitud["FECHA_REGISTRO_VITAL"]) : default(DateTime));
                    p_objSolicitudLiquidacion.EstadoSolicitud = new EstadoSolicitudLiquidacionEntity { EstadoSolicitudID = Convert.ToInt32(p_objDatosSolicitud["AUTOLIQESTADOSOLICITUD_ID"]), EstadoSolicitud = p_objDatosSolicitud["ESTADO"].ToString() };
                    p_objSolicitudLiquidacion.FechaCreacion = Convert.ToDateTime(p_objDatosSolicitud["FECHA_CREACION"]);
                    p_objSolicitudLiquidacion.FechaModificacion = Convert.ToDateTime(p_objDatosSolicitud["FECHA_ULTIMA_MODIFICACION"]);
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: CargarDatosSolicitudLiquidacion -> Error cargando información al objeto: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }
            }


            /// <summary>
            /// Cargar la información contenida en el datatable en el el listado de permisos del objeto de solicitud
            /// </summary>
            /// <param name="p_objDatosPermisos">DataTable con la información de los permisos</param>
            /// <param name="p_objSolicitudLiquidacion">SolicitudLiquidacionEntity objeto en el cual se cargara los datos de los permisos</param>
            private void CargarDatosPermisosSolicitudLiquidacion(DataTable p_objDatosPermisos, ref SolicitudLiquidacionEntity p_objSolicitudLiquidacion)
            {
                PermisoSolicitudLiquidacionEntity objPermisoSolicitud = null;

                try
                {
                    //Verificar si se obtuvo datos de permisos
                    if (p_objDatosPermisos != null && p_objDatosPermisos.Rows.Count > 0)
                    {
                        //Crear arreglo
                        p_objSolicitudLiquidacion.Permisos = new List<PermisoSolicitudLiquidacionEntity>();

                        //Ciclo que carga los datos de los permisos
                        foreach (DataRow objPermiso in p_objDatosPermisos.Rows)
                        {
                            //Crear objeto
                            objPermisoSolicitud = new PermisoSolicitudLiquidacionEntity
                            {
                                PermisoID = Convert.ToInt32(objPermiso["AUTOLIQPERMISOSOLICITUDLIQUIDACION_ID"]),
                                SolicitudLiquidacionID = Convert.ToInt32(objPermiso["AUTOLIQSOLICITUDLIQUIDACION_ID"]),
                                Permiso = new PermisoLiquidacionEntity { PermisoID = Convert.ToInt32(objPermiso["AUTOLIQPERMISO_ID"]), Permiso = objPermiso["PERMISO"].ToString()},
                                AutoridadID = Convert.ToInt32(objPermiso["AUT_ID"]),
                                Autoridad = objPermiso["AUTORIDAD"].ToString(),
                                NumeroPermisos = Convert.ToInt32(objPermiso["NUMERO_PERMISOS"]),
                            };

                            //Cargar al listado
                            p_objSolicitudLiquidacion.Permisos.Add(objPermisoSolicitud);
                        }
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: CargarDatosPermisosSolicitudLiquidacion -> Error cargando información de permisos: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }
            }


            /// <summary>
            /// Cargar la información contenida en el datatable en el el listado de regiones del objeto de solicitud
            /// </summary>
            /// <param name="p_objDatosPermisos">DataTable con la información de las regiones</param>
            /// <param name="p_objSolicitudLiquidacion">SolicitudLiquidacionEntity objeto en el cual se cargara los datos de las regiones</param>
            private void CargarDatosRegionesSolicitudLiquidacion(DataTable p_objDatosRegiones, ref SolicitudLiquidacionEntity p_objSolicitudLiquidacion)
            {
                RegionSolicitudLiquidacionEntity objRegionSolicitud = null;

                try
                {
                    //Verificar si se obtuvo datos de regiones
                    if (p_objDatosRegiones != null && p_objDatosRegiones.Rows.Count > 0)
                    {
                        //Crear arreglo
                        p_objSolicitudLiquidacion.Regiones = new List<RegionSolicitudLiquidacionEntity>();

                        //Ciclo que carga los datos de las regiones
                        foreach (DataRow objRegion in p_objDatosRegiones.Rows)
                        {
                            //Crear objeto
                            objRegionSolicitud = new RegionSolicitudLiquidacionEntity
                            {
                                RegionSolicitudID = Convert.ToInt32(objRegion["AUTOLIQREGIONSOLICITUDLIQUIDACION_ID"]),
                                SolicitudLiquidacionID = Convert.ToInt32(objRegion["AUTOLIQSOLICITUDLIQUIDACION_ID"]),
                                Region = new RegionLiquidacionEntity { RegionID = Convert.ToInt32(objRegion["AUTOLIQREGION_ID"]), Region = objRegion["REGION"].ToString() },
                            };

                            //Cargar al listado
                            p_objSolicitudLiquidacion.Regiones.Add(objRegionSolicitud);
                        }
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: CargarDatosRegionesSolicitudLiquidacion -> Error cargando información de regiones: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }
            }

            
            /// <summary>
            /// Cargar la información contenida en los datatable en el el listado de ubicaciones del objeto de solicitud
            /// </summary>
            /// <param name="p_objDatosUbicaciones">DataTable con la información de las ubicaciones</param>
            /// <param name="p_objDatosCoordenadas">DataTable con la información de las coordenadas de las ubicaciones</param>
            /// <param name="p_objSolicitudLiquidacion">SolicitudLiquidacionEntity objeto en el cual se cargara los datos de las ubicaciones</param>
            private void CargarDatosUbicacionesSolicitudLiquidacion(DataTable p_objDatosUbicaciones, DataTable p_objDatosCoordenadas, ref SolicitudLiquidacionEntity p_objSolicitudLiquidacion)
            {
                UbicacionSolicitudLiquidacionEntity objUbicacionSolicitud = null;
                CoordenadaUbicacionLiquidacionEntity objCoordenadaUbicacion = null;
                DataRow[] objCoordenadasUbicaciones = null;

                try
                {
                    //Verificar si se obtuvo datos de ubicaciones
                    if (p_objDatosUbicaciones != null && p_objDatosUbicaciones.Rows.Count > 0)
                    {
                        //Crear arreglo
                        p_objSolicitudLiquidacion.Ubicaciones = new List<UbicacionSolicitudLiquidacionEntity>();

                        //Ciclo que carga los datos de las ubicaciones
                        foreach (DataRow objUbicacion in p_objDatosUbicaciones.Rows)
                        {
                            //Crear objeto
                            objUbicacionSolicitud = new UbicacionSolicitudLiquidacionEntity
                            {
                                UbicacionSolicitudID = Convert.ToInt32(objUbicacion["AUTOLIQUBICACIONSOLICITUDLIQUIDACION_ID"]),
                                SolicitudLiquidacionID = Convert.ToInt32(objUbicacion["AUTOLIQSOLICITUDLIQUIDACION_ID"]),
                                Departamento = new DepartamentoIdentity { Id = Convert.ToInt32(objUbicacion["DEP_ID"]), Nombre = objUbicacion["DEPARTAMENTO"].ToString() },
                                Municipio = new MunicipioIdentity { Id = Convert.ToInt32(objUbicacion["MUN_ID"]), NombreMunicipio = objUbicacion["MUNICIPIO"].ToString() },                                
                            };

                            //Cargar objetos opcionales                            
                            objUbicacionSolicitud.Corregimiento = (objUbicacion["CORREGIMIENTO"] != System.DBNull.Value ? objUbicacion["CORREGIMIENTO"].ToString() : "");
                            objUbicacionSolicitud.Vereda = (objUbicacion["VEREDA"] != System.DBNull.Value ? objUbicacion["VEREDA"] .ToString(): "");

                            //Cargar el listado de coordenadas
                            if (p_objDatosCoordenadas != null && p_objDatosCoordenadas.Rows.Count > 0)
                            {
                                //Obtener el listado de coordenadas de la ubicación
                                objCoordenadasUbicaciones = p_objDatosCoordenadas.Select("AUTOLIQUBICACIONSOLICITUDLIQUIDACION_ID = " + objUbicacionSolicitud.UbicacionSolicitudID.ToString());

                                //Verificar que se obtenga datos
                                if (objCoordenadasUbicaciones != null && objCoordenadasUbicaciones.Length > 0)
                                {
                                    //Crear listado de coordenadas
                                    objUbicacionSolicitud.Coordenadas = new List<CoordenadaUbicacionLiquidacionEntity>();

                                    //Ciclo que carga la información de las coordenadas
                                    foreach (DataRow objCoordenada in objCoordenadasUbicaciones)
                                    {
                                        //Crear objeto
                                        objCoordenadaUbicacion = new CoordenadaUbicacionLiquidacionEntity
                                        {
                                            CoordenadaUbicacionLiquidacionID = Convert.ToInt32(objCoordenada["AUTOLIQCOORDENADASOLICITUDLIQUIDACION_ID"]),
                                            UbicacionLiquidacionID = Convert.ToInt32(objCoordenada["AUTOLIQUBICACIONSOLICITUDLIQUIDACION_ID"]),
                                            SolicitudLiquidacionID = Convert.ToInt32(objCoordenada["AUTOLIQSOLICITUDLIQUIDACION_ID"]),
                                            Localizacion = objCoordenada["LOCALIZACION"].ToString(),
                                            Norte = objCoordenada["COORDENADA_NORTE"].ToString(),
                                            Este = objCoordenada["COORDENADA_ESTE"].ToString()
                                        };

                                        //Cargar datos opcionales
                                        if (objCoordenada["AUTOLIQTIPOGEOMETRIA_ID"] != System.DBNull.Value && Convert.ToInt32(objCoordenada["AUTOLIQTIPOGEOMETRIA_ID"]) > 0)
                                            objCoordenadaUbicacion.TipoGeometria = new TipoGeometriaCoordenadaLiquidacionEntity { TipoGeometriaID = Convert.ToInt32(objCoordenada["AUTOLIQTIPOGEOMETRIA_ID"]), TipoGeometria = objCoordenada["TIPO_GEOMETRIA"].ToString() };
                                        else
                                            objCoordenadaUbicacion.TipoGeometria = null;

                                        if (objCoordenada["AUTOLIQTIPOCOORDENADA_ID"] != System.DBNull.Value && Convert.ToInt32(objCoordenada["AUTOLIQTIPOCOORDENADA_ID"]) > 0)
                                            objCoordenadaUbicacion.TipoCoordenada = new TipoCoordenadaUbicacionLiquidacionEntity {  TipoCoordenadaID = Convert.ToInt32(objCoordenada["AUTOLIQTIPOCOORDENADA_ID"]), TipoCoordenada = objCoordenada["TIPO_COORDENADA"].ToString() };
                                        else
                                            objCoordenadaUbicacion.TipoCoordenada = null;

                                        if (objCoordenada["AUTOLIQORIGENMAGNA_ID"] != System.DBNull.Value && Convert.ToInt32(objCoordenada["AUTOLIQORIGENMAGNA_ID"]) > 0)
                                            objCoordenadaUbicacion.OrigenMagna = new OrigenMagnaCoordenadaLiquidacionEntity { OrigenMagnaID = Convert.ToInt32(objCoordenada["AUTOLIQORIGENMAGNA_ID"]), OrigenMagna = objCoordenada["ORIGEN_MAGNA"].ToString() };
                                        else
                                            objCoordenadaUbicacion.OrigenMagna = null;

                                        //Agregar coordenada al listado
                                        objUbicacionSolicitud.Coordenadas.Add(objCoordenadaUbicacion);
                                    }
                                }
                                else
                                {
                                    objUbicacionSolicitud.Coordenadas = null;
                                }
                            }
                            else
                            {
                                objUbicacionSolicitud.Coordenadas = null;
                            }

                            //Cargar al listado
                            p_objSolicitudLiquidacion.Ubicaciones.Add(objUbicacionSolicitud);
                        }
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: CargarDatosUbicacionesSolicitudLiquidacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }
            }


            /// <summary>
            /// Cargar la información contenida en el datatable en el el listado de rutas del objeto de solicitud
            /// </summary>
            /// <param name="p_objDatosRutas">DataTable con la información de las rutas</param>
            /// <param name="p_objSolicitudLiquidacion">SolicitudLiquidacionEntity objeto en el cual se cargara los datos de las rutas</param>
            private void CargarDatosRutasSolicitudLiquidacion(DataTable p_objDatosRutas, ref SolicitudLiquidacionEntity p_objSolicitudLiquidacion)
            {
                RutaLogisticaSolicitudLiquidacionEntity objRutaSolicitud = null;

                try
                {
                    //Verificar si se obtuvo datos de rutas
                    if (p_objDatosRutas != null && p_objDatosRutas.Rows.Count > 0)
                    {
                        //Crear arreglo
                        p_objSolicitudLiquidacion.Rutas = new List<RutaLogisticaSolicitudLiquidacionEntity>();

                        //Ciclo que carga los datos de las rutas
                        foreach (DataRow objRuta in p_objDatosRutas.Rows)
                        {
                            //Crear objeto
                            objRutaSolicitud = new RutaLogisticaSolicitudLiquidacionEntity
                            {
                                RutaLogisticaSolicitudID = Convert.ToInt32(objRuta["AUTOLIQRUTASOLICITUDLIQUIDACION_ID"]),
                                SolicitudLiquidacionID = Convert.ToInt32(objRuta["AUTOLIQSOLICITUDLIQUIDACION_ID"]),
                                MedioTransporte = new MedioTransporteLiquidacionEntity { MedioTransporteID = Convert.ToInt32(objRuta["AUTOLIQMEDIOTRANSPORTE_ID"]), MedioTransporte = objRuta["MEDIO_TRANSPORTE"].ToString() },
                                DepartamentoOrigen = new DepartamentoIdentity { Id = Convert.ToInt32(objRuta["DEP_ORIGEN_ID"]), Nombre = objRuta["DEPARTAMENTO_ORIGEN"].ToString() },
                                MunicipioOrigen = new MunicipioIdentity { Id = Convert.ToInt32(objRuta["MUN_ORIGEN_ID"]), NombreMunicipio = objRuta["MUNICIPIO_ORIGEN"].ToString() },
                                DepartamentoDestino = new DepartamentoIdentity { Id = Convert.ToInt32(objRuta["DEP_DESTINO_ID"]), Nombre = objRuta["DEPARTAMENTO_DESTINO"].ToString() },
                                MunicipioDestino = new MunicipioIdentity { Id = Convert.ToInt32(objRuta["MUN_DESTINO_ID"]), NombreMunicipio = objRuta["MUNICIPIO_DESTINO"].ToString() },
                                TiempoAproximadoTrayecto = objRuta["TIEMPO_APROXIMADO_TRAYECTO"].ToString()
                            };

                            //Cargar al listado
                            p_objSolicitudLiquidacion.Rutas.Add(objRutaSolicitud);
                        }
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: CargarDatosRutasSolicitudLiquidacion -> Error cargando información de rutas: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }
            }

    
            /// <summary>
            /// Cargar la información contenida en el datatable en el listado de cobros relacionados
            /// </summary>
            /// <param name="p_objDatosCobros">DataTable con la información de los cobros</param>
            /// <param name="p_objSolicitudLiquidacion">SolicitudLiquidacionEntity objeto en el cual se cargara los datos de los cobros</param>
            private void CargarDatosCobrosRelacionados(DataTable p_objDatosCobros, ref SolicitudLiquidacionEntity p_objSolicitudLiquidacion)
            {
                CobroSolicitudLiquidacionEntity objCobroSolicitud = null;

                try
                {
                    //Verificar si se obtuvo datos de campos
                    if (p_objDatosCobros != null && p_objDatosCobros.Rows.Count > 0)
                    {
                        //Crear arreglo
                        p_objSolicitudLiquidacion.CobrosSolicitud = new List<CobroSolicitudLiquidacionEntity>();

                        //Ciclo que carga los datos de los campos
                        foreach (DataRow objCobro in p_objDatosCobros.Rows)
                        {
                            //Crear objeto
                            objCobroSolicitud = new CobroSolicitudLiquidacionEntity
                            {
                                CobroSolicitudLiquidacionEntityID = Convert.ToInt32(objCobro["AUTOLIQCOBROSOLICITUDLIQUIDACION_ID"]),
                                SolicitudLiquidacionID = Convert.ToInt32(objCobro["AUTOLIQSOLICITUDLIQUIDACION_ID"]),
                                AutoridadAmbiental = new AutoridadAmbientalIdentity { IdAutoridad = Convert.ToInt32(objCobro["AUT_ID"]), Nombre = objCobro["AUTORIDAD"].ToString() },
                                Concepto = objCobro["CONCEPTO_COBRO"].ToString(),
                                LiquidacionID = (objCobro["AUTLIQLIQUIDACION_ID"] != System.DBNull.Value ? Convert.ToInt32(objCobro["AUTLIQLIQUIDACION_ID"]) : 0),
                                CobroLiquidacionID = (objCobro["COBROAUTOLIQ_ID"] != System.DBNull.Value ? Convert.ToInt32(objCobro["COBROAUTOLIQ_ID"]) : 0),
                                DetalleCobroLiquidacionID = (objCobro["COBRODETALLEAUTOLIQ_ID"] != System.DBNull.Value ? Convert.ToInt32(objCobro["COBRODETALLEAUTOLIQ_ID"]) : 0),
                                CobroID = Convert.ToInt32(objCobro["COB_ID"]),
                                ValorCobro = Convert.ToDecimal(objCobro["CCO_VALOR"])                                
                            };
                            if (objCobro["AUTOLIQESTADOCOBRO_ID"] != System.DBNull.Value)
                                objCobroSolicitud.EstadoCobro = new EstadoCobroLiquidacionEntity { EstadoCobroID = Convert.ToInt32(objCobro["AUTOLIQESTADOCOBRO_ID"]), EstadoCobro = objCobro["ESTADO_COBRO"].ToString() };                            

                            //Cargar al listado
                            p_objSolicitudLiquidacion.CobrosSolicitud.Add(objCobroSolicitud);
                        }
                    }
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: CargarDatosCobrosRelacionados -> Error cargando información de cobros: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw exc;
                }
            }

        #endregion


        #region  Metodos Publicos


            #region Consultas

                /// <summary>
                /// Consultar el identificador correspondiente a la descripcion
                /// </summary>
                /// <param name="p_intDesplegable">int con el identificador del deplegable</param>
                /// <param name="p_strDescripcion">string con la descripción</param>
                /// <param name="p_strDescripcionPadre">string con la descripcion padre a buscar</param>
                /// <returns>int con el identificador</returns>
                public int ConsultarIdentificadoDesplegable(int p_intDesplegable, string p_strDescripcion, string p_strDescripcionPadre = "")
                {
                    SqlDatabase objDataBase = null;
                    DbCommand objCommand = null;
                    DataSet objInformacion = null;
                    int intIdentificador = 0;

                    try
                    {
                        //Cargar conexion
                        objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                        //Cargar comando
                        objCommand = objDataBase.GetStoredProcCommand("AUTOLIQ_OBTENER_IDENTIFICADOR_TABLA");
                        objDataBase.AddInParameter(objCommand, "@P_DESPLEGABLE", DbType.Int32, p_intDesplegable);
                        if (!string.IsNullOrEmpty(p_strDescripcionPadre))
                            objDataBase.AddInParameter(objCommand, "@P_DESCRIPCION_PADRE", DbType.String, p_strDescripcionPadre);
                        objDataBase.AddInParameter(objCommand, "@P_DESCRIPCION", DbType.String, p_strDescripcion);

                        //Consultar
                        objInformacion = objDataBase.ExecuteDataSet(objCommand);
                        if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                        {
                            intIdentificador = Convert.ToInt32(objInformacion.Tables[0].Rows[0]["IDENTIFICADOR"]);
                        }
                    }
                    catch (SqlException sqle)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: ConsultarIdentificadoDesplegable -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                        //Escalar error
                        throw sqle;
                    }
                    catch (Exception exc)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: ConsultarIdentificadoDesplegable -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                        //Escalar error
                        throw exc;
                    }

                    return intIdentificador;
                }


                /// <summary>
                /// Obtener la información de la solicitud identificada por el número VITAL que ingresa
                /// </summary>
                /// <param name="p_strNumeroVital">string con el número vital</param>
                /// <returns>
                /// DataSet con la información de la radicación en las siguientes tablas:
                /// - Radicacion
                /// - Solicitante
                /// - Representantes
                /// </returns>
                public DataSet ConsultarInformacionRadicacionNumeroVital(string p_strNumeroVital)
                {
                    SqlDatabase objDataBase = null;
                    DbCommand objCommand = null;
                    DataSet objInformacion = null;

                    try
                    {
                        //Cargar conexion
                        objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                        //Cargar comando
                        objCommand = objDataBase.GetStoredProcCommand("AUTOLIQ_OBTENER_INFORMACION_RADICACION");
                        objDataBase.AddInParameter(objCommand, "@P_NUMERO_VITAL", DbType.String, p_strNumeroVital);
                    
                        //Consultar
                        objInformacion = objDataBase.ExecuteDataSet(objCommand);
                        if (objInformacion != null && objInformacion.Tables.Count > 0)
                        {
                            objInformacion.Tables[0].TableName = "Radicacion";
                            if (objInformacion.Tables.Count > 1)
                                objInformacion.Tables[1].TableName = "Solicitante";
                            if (objInformacion.Tables.Count > 2)
                                objInformacion.Tables[2].TableName = "Representantes";
                        }
                    }
                    catch (SqlException sqle)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: ConsultarIdentificadoDesplegable -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                        //Escalar error
                        throw sqle;
                    }
                    catch (Exception exc)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: ConsultarIdentificadoDesplegable -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                        //Escalar error
                        throw exc;
                    }

                    return objInformacion;
                }


                /// <summary>
                /// Obtener la autoridad(es) ambiental(es) correspondiente a la solicitud de liquidación realizada
                /// </summary>
                /// <param name="p_intTipoSolicitudID">int con el identificador del tipo de solicitud que se esta realizando</param>
                /// <param name="p_intTramiteID">int con el identificador del tramite que se encuentra realizando</param>
                /// <param name="p_intTipoProyectoID">int con el identificador el tipo de proyecto o actividad</param>
                /// <param name="p_lstTipoProyectoID">List con el listado de identificadores de municipios donde se llevara a capo los proyectos</param>
                /// <returns>List con la(s) autoridad(es) ambiental(es) a las cuales se direccione la solicitud</returns>
                public List<AutoridadAmbientalIdentity> ConsultarAutoridadAmbientalSolicitud(int p_intTipoSolicitudID, int p_intTramiteID, int p_intTipoProyectoID, List<int> p_lstTipoProyectoID)
                {
                    SqlDatabase objDataBase = null;
                    DbCommand objCommand = null;
                    DataSet objInformacion = null;
                    List<AutoridadAmbientalIdentity> objLstAutoridades = null;
                    AutoridadAmbientalIdentity objAutoridad = null;
                    string strListaMunicipios = "";

                    try
                    {
                        //Cargar el listado de municipios
                        if (p_lstTipoProyectoID != null && p_lstTipoProyectoID.Count > 0)
                        {
                            //Ciclo que carga lista de municipios
                            foreach (int intMunicipio in p_lstTipoProyectoID)
                            {
                                if (string.IsNullOrEmpty(strListaMunicipios))
                                    strListaMunicipios = intMunicipio.ToString();
                                else
                                    strListaMunicipios = strListaMunicipios + "," + intMunicipio.ToString();
                            }
                        }

                        //Cargar conexion
                        objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                        //Cargar comando
                        objCommand = objDataBase.GetStoredProcCommand("AUTOLIQ_CONSULTAR_ENTIDAD_SECTOR_PROYECTO");
                        objDataBase.AddInParameter(objCommand, "@P_ID_TIPOSOLICITUD", DbType.Int32, p_intTipoSolicitudID);
                        if (p_intTramiteID > 0)
                            objDataBase.AddInParameter(objCommand, "@P_ID_TRAMITE", DbType.Int32, p_intTramiteID);
                        if (p_intTipoProyectoID > 0)
                            objDataBase.AddInParameter(objCommand, "@P_TIPO_PROYECTO_ID", DbType.Int32, p_intTipoProyectoID);
                        if (!string.IsNullOrEmpty(strListaMunicipios))
                            objDataBase.AddInParameter(objCommand, "@P_LST_MUNICIPIO_ID", DbType.String, strListaMunicipios);

                        //Consultar
                        objInformacion = objDataBase.ExecuteDataSet(objCommand);
                        if (objInformacion != null && objInformacion.Tables[0].Rows.Count > 0)
                        {
                            //Crear listado
                            objLstAutoridades = new List<AutoridadAmbientalIdentity>();

                            //Ciclo que carga el lisatdo de autoridades
                            foreach (DataRow objAutoridadAmbiental in objInformacion.Tables[0].Rows)
                            {
                                //Cargar datos de autoridad
                                objAutoridad = new AutoridadAmbientalIdentity
                                {
                                    IdAutoridad = Convert.ToInt32(objAutoridadAmbiental["AUT_ID"]),
                                    Nombre = objAutoridadAmbiental["AUT_NOMBRE"].ToString()
                                };

                                //Adicionar al listado
                                objLstAutoridades.Add(objAutoridad);
                            }
                        }
                    }
                    catch (SqlException sqle)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: ConsultarAutoridadAmbientalSolicitud -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                        //Escalar error
                        throw sqle;
                    }
                    catch (Exception exc)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: ConsultarAutoridadAmbientalSolicitud -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                        //Escalar error
                        throw exc;
                    }

                    return objLstAutoridades;
                }
        

                /// <summary>
                /// Consultar la información de la solicitud de la liquidación
                /// </summary>
                /// <param name="p_SolicitudLiquidacionID">int con el identificador de la solicitud</param>
                /// <returns>SolicitudLiquidacionEntity con la información de la solicitud. En caso de no encontrar información retorna null</returns>
                public SolicitudLiquidacionEntity ConsultarSolicitudLiquidacion(int p_SolicitudLiquidacionID)
                {
                    SqlDatabase objDataBase = null;
                    DbCommand objCommand = null;
                    DataSet objInformacionSolicitud = null;
                    SolicitudLiquidacionEntity objSolicitudLiquidacion = null;

                    try
                    {
                        //Cargar conexion
                        objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                        //Cargar comando
                        objCommand = objDataBase.GetStoredProcCommand("AUTOLIQ_CONSULTAR_SOLICITUD_LIQUIDACION");
                        objDataBase.AddInParameter(objCommand, "@P_AUTOLIQSOLICITUDLIQUIDACION_ID", DbType.Int32, p_SolicitudLiquidacionID);

                        //Consultar
                        objInformacionSolicitud = objDataBase.ExecuteDataSet(objCommand);

                        //Cargar información
                        if (objInformacionSolicitud != null && objInformacionSolicitud.Tables.Count > 0)
                        {
                            //Crear objeto
                            objSolicitudLiquidacion = new SolicitudLiquidacionEntity();

                            //Cargar la información de la solicitud
                            this.CargarDatosSolicitudLiquidacion(objInformacionSolicitud.Tables[0], ref objSolicitudLiquidacion);
                            this.CargarDatosPermisosSolicitudLiquidacion(objInformacionSolicitud.Tables[1], ref objSolicitudLiquidacion);
                            this.CargarDatosRegionesSolicitudLiquidacion(objInformacionSolicitud.Tables[2], ref objSolicitudLiquidacion);
                            this.CargarDatosUbicacionesSolicitudLiquidacion(objInformacionSolicitud.Tables[3], objInformacionSolicitud.Tables[4], ref objSolicitudLiquidacion);
                            this.CargarDatosRutasSolicitudLiquidacion(objInformacionSolicitud.Tables[5], ref objSolicitudLiquidacion);
                            this.CargarDatosCobrosRelacionados(objInformacionSolicitud.Tables[6], ref objSolicitudLiquidacion);
                        }
                    }
                    catch (SqlException sqle)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: ConsultarSolicitudLiquidacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                        //Escalar error
                        throw sqle;
                    }
                    catch (Exception exc)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: ConsultarSolicitudLiquidacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                        //Escalar error
                        throw exc;
                    }

                    return objSolicitudLiquidacion;
                }


                /// <summary>
                /// Consultar la información de la solicitud de la liquidación para generación del documento de radicación
                /// </summary>
                /// <param name="p_SolicitudLiquidacionID">int con el identificador de la solicitud</param>
                /// <returns>
                /// DataSet con la información de la solicitud en las siguientes tablas:
                /// - Solicitud
                /// - Permisos
                /// - Regiones
                /// - Ubicaciones
                /// - Coordenadas
                /// - Rutas
                /// - Campos_Complementarios
                /// </returns>
                public DataSet ConsultarSolicitudLiquidacionDocumentoRadicacion(int p_SolicitudLiquidacionID)
                {
                    SqlDatabase objDataBase = null;
                    DbCommand objCommand = null;
                    DataSet objInformacionSolicitud = null;

                    try
                    {
                        //Cargar conexion
                        objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                        //Cargar comando
                        objCommand = objDataBase.GetStoredProcCommand("AUTOLIQ_CONSULTAR_SOLICITUD_LIQUIDACION_DOCUMENTO_PDF");
                        objDataBase.AddInParameter(objCommand, "@P_AUTOLIQSOLICITUDLIQUIDACION_ID", DbType.Int32, p_SolicitudLiquidacionID);

                        //Consultar
                        objInformacionSolicitud = objDataBase.ExecuteDataSet(objCommand);

                        //Cargar información
                        if (objInformacionSolicitud != null && objInformacionSolicitud.Tables.Count > 0)
                        {
                            objInformacionSolicitud.Tables[0].TableName = "Solicitud";
                            objInformacionSolicitud.Tables[1].TableName = "Permisos";
                            objInformacionSolicitud.Tables[2].TableName = "Regiones";
                            objInformacionSolicitud.Tables[3].TableName = "Ubicaciones";
                            objInformacionSolicitud.Tables[4].TableName = "Coordenadas";
                            objInformacionSolicitud.Tables[5].TableName = "Rutas";
                        }
                    }
                    catch (SqlException sqle)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: ConsultarSolicitudLiquidacionDocumentoRadicacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                        //Escalar error
                        throw sqle;
                    }
                    catch (Exception exc)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: ConsultarSolicitudLiquidacionDocumentoRadicacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                        //Escalar error
                        throw exc;
                    }

                    return objInformacionSolicitud;
                }


                /// <summary>
                /// Consultar el listado de solicitudes que cumplen con los parametros de busqueda especificados
                /// </summary>
                /// <param name="p_intSolicitanteID">int con el identificador del solicitante</param>
                /// <param name="p_intAutoridadID">int con el identificador de la autoridad ambiental. Opcional.</param>
                /// <param name="p_intTipoSolicitudID">int con el identificador del tipo de solicitud. Opcional.</param>
                /// <param name="p_intClaseSolicitudID">int con la clase de solicitud. Opcional</param>
                /// <param name="p_strNombreProyecto">string con el nombre del proyecto. Opcional.</param>
                /// <param name="p_strNumeroVital">string con el numero vital. Opcional.</param>
                /// <param name="p_intEstadoSolicitudID">int con el identificador del estado de la solicitud. Opcional.</param>
                /// <param name="p_objFechaCreacionInicio">DataTime con la fecha de creación. Rango Inicial. Opcional.</param>
                /// <param name="p_objFechaCreacionFin">DataTime con la fecha de creación. Rango Final. Opcional.</param>
                /// <returns></returns>
                public List<SolicitudLiquidacionEntity> ConsultarListadoSolicitudesLiquidacion(int p_intSolicitanteID, int p_intAutoridadID = -1, int p_intTipoSolicitudID = -1,
                                                                                               int p_intClaseSolicitudID = -1, string p_strNombreProyecto = "", string p_strNumeroVital = "",
                                                                                               int p_intEstadoSolicitudID = -1, DateTime p_objFechaCreacionInicio = default(DateTime), DateTime p_objFechaCreacionFin = default(DateTime))
                {
                    SqlDatabase objDataBase = null;
                    DbCommand objCommand = null;
                    DataSet objInformacionSolicitud = null;
                    List<SolicitudLiquidacionEntity> objLstSolicitudesLiquidacion = null;
                    SolicitudLiquidacionEntity objSolicitudLiquidacion = null;

                    try
                    {
                        //Cargar conexion
                        objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                        //Cargar comando
                        objCommand = objDataBase.GetStoredProcCommand("AUTOLIQ_LISTAR_SOLICITUD_LIQUIDACION");
                        objDataBase.AddInParameter(objCommand, "@P_SOLICITANTE_ID", DbType.Int32, p_intSolicitanteID);
                        if(p_intAutoridadID > 0)
                            objDataBase.AddInParameter(objCommand, "@P_AUT_ID", DbType.Int32, p_intAutoridadID);
                        if(p_intTipoSolicitudID > 0)
                            objDataBase.AddInParameter(objCommand, "@P_AUTOLIQTIPOSOLICITUD_ID", DbType.Int32, p_intTipoSolicitudID);
                        if(p_intClaseSolicitudID > 0)
                            objDataBase.AddInParameter(objCommand, "@P_AUTOLIQSOLICITUD_ID", DbType.Int32, p_intClaseSolicitudID);
                        if(!string.IsNullOrEmpty(p_strNombreProyecto))
                            objDataBase.AddInParameter(objCommand, "@P_NOMBRE_PROYECTO", DbType.String, p_strNombreProyecto);
                        if(!string.IsNullOrEmpty(p_strNumeroVital))
                            objDataBase.AddInParameter(objCommand, "@P_NUMERO_VITAL", DbType.String, p_strNumeroVital);
                        if(p_intEstadoSolicitudID > 0)
                            objDataBase.AddInParameter(objCommand, "@P_AUTOLIQESTADOSOLICITUD_ID", DbType.Int32, p_intEstadoSolicitudID);
                        if(p_objFechaCreacionInicio != default(DateTime))
                            objDataBase.AddInParameter(objCommand, "@P_FECHA_CREACION_INICIAL", DbType.DateTime, p_objFechaCreacionInicio);
                        if(p_objFechaCreacionFin != default(DateTime))
                            objDataBase.AddInParameter(objCommand, "@P_FECHA_CREACION_FINAL", DbType.DateTime, p_objFechaCreacionFin);    

                        //Consultar
                        objInformacionSolicitud = objDataBase.ExecuteDataSet(objCommand);

                        //Cargar los datos de las solicitudes
                        if (objInformacionSolicitud != null && objInformacionSolicitud.Tables.Count > 0 && objInformacionSolicitud.Tables[0].Rows.Count > 0)
                        {
                            //Crear listado
                            objLstSolicitudesLiquidacion = new List<SolicitudLiquidacionEntity>();

                            //Ciclo que carga datos de la solicitud
                            foreach(DataRow objSolicitud in objInformacionSolicitud.Tables[0].Rows)
                            {
                                //Crear objeto
                                objSolicitudLiquidacion = new SolicitudLiquidacionEntity();

                                //Cargar datos
                                this.CargarDatosSolicitudLiquidacion(objSolicitud, ref objSolicitudLiquidacion);

                                //Cargar al listado
                                objLstSolicitudesLiquidacion.Add(objSolicitudLiquidacion);
                            }
                        }
                    }
                    catch (SqlException sqle)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: ConsultarListadoSolicitudesLiquidacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                        //Escalar error
                        throw sqle;
                    }
                    catch (Exception exc)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: ConsultarListadoSolicitudesLiquidacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                        //Escalar error
                        throw exc;
                    }

                    return objLstSolicitudesLiquidacion;
                }


                /// <summary>
                /// Cargar la información contenida en el datatable en el listado de cobros relacionados
                /// </summary>
                /// <param name="p_objDatosCobros">DataTable con la información de los cobros</param>
                /// <param name="p_objSolicitudLiquidacion">SolicitudLiquidacionEntity objeto en el cual se cargara los datos de los cobros</param>
                public List<CobroSolicitudLiquidacionEntity> ObtenerCobrosSolicitudLiquidacion(int p_intSolicitudLiquidacionID)
                {
                    SqlDatabase objDataBase = null;
                    DbCommand objCommand = null;
                    DataSet objInformacion = null;
                    CobroSolicitudLiquidacionEntity objCobroSolicitud = null;
                    List<CobroSolicitudLiquidacionEntity> objLstCobros = null;

                    try
                    {
                        //Cargar conexion
                        objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                        //Cargar comando
                        objCommand = objDataBase.GetStoredProcCommand("AUTOLIQ_CONSULTAR_COBRO_SOLICITUD_LIQUIDACION");
                        objDataBase.AddInParameter(objCommand, "@P_AUTOLIQSOLICITUDLIQUIDACION_ID", DbType.Int32, p_intSolicitudLiquidacionID);

                        //Consultar
                        objInformacion = objDataBase.ExecuteDataSet(objCommand);

                        if (objInformacion != null && objInformacion.Tables.Count > 0 && objInformacion.Tables[0].Rows.Count > 0)
                        {
                            //Crear listado
                            objLstCobros = new List<CobroSolicitudLiquidacionEntity>();

                            //Ciclo que carga los cobros
                            foreach (DataRow objCobro in objInformacion.Tables[0].Rows)
                            {
                                //Crear objeto
                                objCobroSolicitud = new CobroSolicitudLiquidacionEntity
                                {
                                    CobroSolicitudLiquidacionEntityID = Convert.ToInt32(objCobro["AUTOLIQCOBROSOLICITUDLIQUIDACION_ID"]),
                                    SolicitudLiquidacionID = Convert.ToInt32(objCobro["AUTOLIQSOLICITUDLIQUIDACION_ID"]),
                                    AutoridadAmbiental = new AutoridadAmbientalIdentity { IdAutoridad = Convert.ToInt32(objCobro["AUT_ID"]), Nombre = objCobro["AUTORIDAD"].ToString() },
                                    Concepto = objCobro["CONCEPTO_COBRO"].ToString(),
                                    LiquidacionID = (objCobro["AUTLIQLIQUIDACION_ID"] != System.DBNull.Value ? Convert.ToInt32(objCobro["AUTLIQLIQUIDACION_ID"]) : 0),
                                    CobroLiquidacionID = (objCobro["COBROAUTOLIQ_ID"] != System.DBNull.Value ? Convert.ToInt32(objCobro["COBROAUTOLIQ_ID"]) : 0),
                                    DetalleCobroLiquidacionID = (objCobro["COBRODETALLEAUTOLIQ_ID"] != System.DBNull.Value ? Convert.ToInt32(objCobro["COBRODETALLEAUTOLIQ_ID"]) : 0),
                                    CobroID = Convert.ToInt32(objCobro["COB_ID"]),
                                    ValorCobro = Convert.ToDecimal(objCobro["CCO_VALOR"]),
                                    FechaVencimiento = (objCobro["COB_FECHA_VENCIMIENTO"] != System.DBNull.Value ? Convert.ToDateTime(objCobro["COB_FECHA_VENCIMIENTO"]) : default(DateTime))
                                };
                                if (objCobro["AUTOLIQESTADOCOBRO_ID"] != System.DBNull.Value)
                                    objCobroSolicitud.EstadoCobro = new EstadoCobroLiquidacionEntity { EstadoCobroID = Convert.ToInt32(objCobro["AUTOLIQESTADOCOBRO_ID"]), EstadoCobro = objCobro["ESTADO_COBRO"].ToString() };


                                //Cargar al listado
                                objLstCobros.Add(objCobroSolicitud);
                            }
                        }
                    }
                    catch (SqlException sqle)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: ObtenerCobrosSolicitudLiquidacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                        //Escalar error
                        throw sqle;
                    }
                    catch (Exception exc)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: ObtenerCobrosSolicitudLiquidacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                        //Escalar error
                        throw exc;
                    }

                    return objLstCobros;
                }

            #endregion


            #region Insertar

                /// <summary>
                /// Insertar la solicitud de liquidación
                /// </summary>
                /// <param name="p_objSolicitud">SolicitudLiquidacionEntity con la información de la solicitud de liquidación</param>
                /// <returns>int con el identificador de la solicitud</returns>
                public int InsertarSolicitudAutoliquidacion(SolicitudLiquidacionEntity p_objSolicitud)
                {
                    SqlConnection objConnection = null;
                    SqlTransaction objTransaccion = null;
                    SqlCommand objCommand = null;

                    try
                    {
                        //Cargar conexion
                        objConnection = new SqlConnection(this._objConfiguracion.SilpaCnx.ToString());

                        using (objConnection)
                        {
                            //Abrir conexion
                            objConnection.Open();

                            try
                            {
                                //Comenzar transaccion
                                objTransaccion = objConnection.BeginTransaction("InsertarSolicitudLiquidacion");

                                //Crear comando
                                objCommand = objConnection.CreateCommand();
                                objCommand.CommandType = CommandType.StoredProcedure;
                                objCommand.Connection = objConnection;
                                objCommand.Transaction = objTransaccion;

                                //Guardar solicitud
                                p_objSolicitud.SolicitudLiquidacionID = this.GuardarSolicitudLiquidacion(objCommand, p_objSolicitud);

                                //Verificar que se obtenga el identificador de la solicitud
                                if (p_objSolicitud.SolicitudLiquidacionID > 0)
                                {
                                    //Guardar el listado de permisos
                                    if (p_objSolicitud.Permisos != null && p_objSolicitud.Permisos.Count > 0)
                                    {
                                        //Ciclo que almacena información
                                        foreach (PermisoSolicitudLiquidacionEntity objPermiso in p_objSolicitud.Permisos)
                                        {
                                            //Asignar identificador
                                            objPermiso.SolicitudLiquidacionID = p_objSolicitud.SolicitudLiquidacionID;

                                            //Guardar registro
                                            this.GuardarPermisoSolicitudLiquidacion(objCommand, objPermiso);
                                        }
                                    }

                                    //Guardar las regiones
                                    if (p_objSolicitud.Regiones != null && p_objSolicitud.Regiones.Count > 0)
                                    {
                                        //Ciclo que almacena información
                                        foreach (RegionSolicitudLiquidacionEntity objRegion in p_objSolicitud.Regiones)
                                        {
                                            //Asignar identificador
                                            objRegion.SolicitudLiquidacionID = p_objSolicitud.SolicitudLiquidacionID;

                                            //Guardar registro
                                            this.GuardarRegionSolicitudLiquidacion(objCommand, objRegion);
                                        }
                                    }

                                    //Guardar las ubicaciones
                                    if (p_objSolicitud.Ubicaciones != null && p_objSolicitud.Ubicaciones.Count > 0)
                                    {
                                        //Ciclo que almacena información
                                        foreach (UbicacionSolicitudLiquidacionEntity objUbicacion in p_objSolicitud.Ubicaciones)
                                        {
                                            //Asignar identificador
                                            objUbicacion.SolicitudLiquidacionID = p_objSolicitud.SolicitudLiquidacionID;

                                            //Guardar registro
                                            this.GuardarUbicacionSolicitudLiquidacion(objCommand, objUbicacion);
                                        }
                                    }

                                    //Guardar las rutas
                                    if (p_objSolicitud.Rutas != null && p_objSolicitud.Rutas.Count > 0)
                                    {
                                        //Ciclo que almacena información
                                        foreach (RutaLogisticaSolicitudLiquidacionEntity objRuta in p_objSolicitud.Rutas)
                                        {
                                            //Asignar identificador
                                            objRuta.SolicitudLiquidacionID = p_objSolicitud.SolicitudLiquidacionID;

                                            //Guardar registro
                                            this.GuardarRutaSolicitudLiquidacion(objCommand, objRuta);
                                        }
                                    }

                                }
                                else
                                {
                                    throw new Exception("No se obtuvo el identificador de la solicitud");
                                }

                                //Realizar Commit de la transaccion
                                objTransaccion.Commit();
                            }
                            catch (SqlException sqle)
                            {
                                //Realizar rollback
                                objTransaccion.Rollback();

                                //Escalar exc
                                throw sqle;
                            }                        
                            catch (Exception exc)
                            {
                                //Realizar rollback
                                objTransaccion.Rollback();

                                //Escalar exc
                                throw exc;
                            }
                        }
                    }
                    catch (SqlException sqle)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: InsertarSolicitudAutoliquidacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                        //Escalar error
                        throw sqle;
                    }
                    catch (Exception exc)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: InsertarSolicitudAutoliquidacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                        //Escalar error
                        throw exc;
                    }
                    finally
                    {
                        try
                        {
                            objConnection.Close();
                        }
                        catch (Exception exc)
                        {
                            //Escribir error
                            SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: InsertarSolicitudAutoliquidacion -> Error bd cerrando conexión: " + exc.Message + " " + exc.StackTrace);

                            //Escalar error
                            throw exc;
                        }
                    }

                    return p_objSolicitud.SolicitudLiquidacionID;
                }


                /// <summary>
                /// Insertar datos de cobros relacionados a la solicitud de liquidacion
                /// </summary>
                /// <param name="p_intSolicitudLiquidacionID">int con el identificador de la solicitud de liquidación</param>
                /// <param name="p_intLiquidacionID">int con el identificadro de liquidación</param>
                /// <param name="p_intCobroLiquidacionID">int con el identificador de cobro (SILA)</param>
                /// <param name="p_intDetalleCobroLiquidacionID">int con el identificador del detalle de cobro (SILA)</param>
                /// <param name="p_intCobroVITALID">int con el identificador de cobro VITAL</param>
                public void InsertarDatosCobroSolicitudLiquidacion(CobroSolicitudLiquidacionEntity p_objCobroSolicitud)
                {
                    SqlDatabase objDataBase = null;
                    DbCommand objCommand = null;

                    try
                    {
                        //Cargar conexion
                        objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                        //Cargar comando
                        objCommand = objDataBase.GetStoredProcCommand("AUTOLIQ_INSERTAR_COBRO_SOLICITUD_LIQUIDACION");
                        objDataBase.AddInParameter(objCommand, "@P_AUTOLIQSOLICITUDLIQUIDACION_ID", DbType.Int32, p_objCobroSolicitud.SolicitudLiquidacionID);
                        objDataBase.AddInParameter(objCommand, "@P_AUT_ID", DbType.Int32, p_objCobroSolicitud.AutoridadAmbiental.IdAutoridad);
                        objDataBase.AddInParameter(objCommand, "@P_CONCEPTO_COBRO", DbType.String, p_objCobroSolicitud.Concepto);
                        if(p_objCobroSolicitud.LiquidacionID > 0)
                            objDataBase.AddInParameter(objCommand, "@P_AUTLIQLIQUIDACION_ID", DbType.Int32, p_objCobroSolicitud.LiquidacionID);
                        if(p_objCobroSolicitud.CobroLiquidacionID > 0)
                            objDataBase.AddInParameter(objCommand, "@P_COBROAUTOLIQ_ID", DbType.Int32, p_objCobroSolicitud.CobroLiquidacionID);
                        if (p_objCobroSolicitud.DetalleCobroLiquidacionID > 0)
                            objDataBase.AddInParameter(objCommand, "@P_COBRODETALLEAUTOLIQ_ID", DbType.Int32, p_objCobroSolicitud.DetalleCobroLiquidacionID);
                        objDataBase.AddInParameter(objCommand, "@P_COB_ID", DbType.Int64, p_objCobroSolicitud.CobroID);
                        objDataBase.AddInParameter(objCommand, "@P_AUTOLIQESTADOCOBRO_ID", DbType.Int32, p_objCobroSolicitud.EstadoCobro.EstadoCobroID);

                        //Actualizar
                        objDataBase.ExecuteNonQuery(objCommand);
                    }
                    catch (SqlException sqle)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: InsertarDatosCobroSolicitudLiquidacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                        //Escalar error
                        throw sqle;
                    }
                    catch (Exception exc)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: InsertarDatosCobroSolicitudLiquidacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                        //Escalar error
                        throw exc;
                    }
                }

            #endregion


            #region Modificar


                /// <summary>
                /// Modificar el estado de la solicitud de liquidación indicada
                /// </summary>
                /// <param name="p_intSolicitudLiquidacionID">int con el identificador de la solicitud</param>
                /// <param name="p_intEstadoSolicitudID">int con el identificador del nuevo estado</param>
                public void ModificarEstadoSolicitudLiquidacion(int p_intSolicitudLiquidacionID, int p_intEstadoSolicitudID)
                {
                    SqlDatabase objDataBase = null;
                    DbCommand objCommand = null;

                    try
                    {
                        //Cargar conexion
                        objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                        //Cargar comando
                        objCommand = objDataBase.GetStoredProcCommand("AUTOLIQ_ACTUALIZAR_ESTADO_SOLICITUD_LIQUIDACION");
                        objDataBase.AddInParameter(objCommand, "@P_AUTOLIQSOLICITUDLIQUIDACION_ID", DbType.Int32, p_intSolicitudLiquidacionID);
                        objDataBase.AddInParameter(objCommand, "@P_AUTOLIQESTADOSOLICITUD_ID", DbType.Int32, p_intEstadoSolicitudID);

                        //Actualizar
                        objDataBase.ExecuteNonQuery(objCommand);
                    }
                    catch (SqlException sqle)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: ModificarEstadoSolicitudLiquidacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                        //Escalar error
                        throw sqle;
                    }
                    catch (Exception exc)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: ModificarEstadoSolicitudLiquidacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                        //Escalar error
                        throw exc;
                    }
                }


                /// <summary>
                /// Modificar el número vital de la solicitud de liquidación indicada
                /// </summary>
                /// <param name="p_intSolicitudLiquidacionID">int con el identificador de la solicitud</param>
                /// <param name="p_strNumeroVital">string con el número vital</param>
                public void ModificarNumeroVitalSolicitudLiquidacion(int p_intSolicitudLiquidacionID, string p_strNumeroVital)
                {
                    SqlDatabase objDataBase = null;
                    DbCommand objCommand = null;

                    try
                    {
                        //Cargar conexion
                        objDataBase = new SqlDatabase(this._objConfiguracion.SilpaCnx.ToString());

                        //Cargar comando
                        objCommand = objDataBase.GetStoredProcCommand("AUTOLIQ_ACTUALIZAR_NUMERO_VITAL_SOLICITUD_LIQUIDACION");
                        objDataBase.AddInParameter(objCommand, "@P_AUTOLIQSOLICITUDLIQUIDACION_ID", DbType.Int32, p_intSolicitudLiquidacionID);
                        objDataBase.AddInParameter(objCommand, "@P_NUMERO_VITAL", DbType.String, p_strNumeroVital);

                        //Actualizar
                        objDataBase.ExecuteNonQuery(objCommand);
                    }
                    catch (SqlException sqle)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: ModificarNumeroVitalSolicitudLiquidacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                        //Escalar error
                        throw sqle;
                    }
                    catch (Exception exc)
                    {
                        //Escribir error
                        SMLog.Escribir(Severidad.Critico, "AutoliquidacionDalc :: ModificarNumeroVitalSolicitudLiquidacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                        //Escalar error
                        throw exc;
                    }
                }

            #endregion


        #endregion

    }
}
