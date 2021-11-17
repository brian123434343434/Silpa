using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SILPA.AccesoDatos.REDDS
{
    public class ReddsDalc
    {
        private Configuracion objConfiguracion;

        public ReddsDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public List<ReddsEstadoAvanceIniciativa> ConsultaListaEstadosIniciativa()
        {
            try
            {
                List<ReddsEstadoAvanceIniciativa> LstEstados = new List<ReddsEstadoAvanceIniciativa>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("REDDSSP_LISTA_ESTADO_AVANCE_INICIATIVA");
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ReddsEstadoAvanceIniciativa vReddsEstadoAvanceIniciativa = new ReddsEstadoAvanceIniciativa
                        {
                            EstadoID = Convert.ToInt32(reader["ESTADO_ID"]),
                            NombreEstado = reader["NOMBRE_ESTADO"].ToString()
                        };
                        LstEstados.Add(vReddsEstadoAvanceIniciativa);
                    }
                }
                return LstEstados;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        public List<ReddsActividad> ConsultaListaActividades()
        {
            try
            {
                List<ReddsActividad> LstReddsActividad = new List<ReddsActividad>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("REDDSSP_LISTA_ACTIVIDADES");
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ReddsActividad vReddsActividad = new ReddsActividad
                        {
                            ActividadID = Convert.ToInt32(reader["ACTIVIDAD_ID"]),
                            NombreActividad = reader["NOMBRE_ACTIVIDAD"].ToString()
                        };
                        LstReddsActividad.Add(vReddsActividad);
                    }
                }
                return LstReddsActividad;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        public List<ReddsCompartimientoCarbono> ConsultaListaCompartimientoCarbono()
        {
            try
            {
                List<ReddsCompartimientoCarbono> LstReddsCompartimientoCarbono = new List<ReddsCompartimientoCarbono>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("REDDSSP_LISTA_COMPARTIMENTO");
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ReddsCompartimientoCarbono vReddsCompartimientoCarbono = new ReddsCompartimientoCarbono
                        {
                            CompartimientoID = Convert.ToInt32(reader["COMPARTIMENTO_ID"]),
                            NombreCompartimiento = reader["NOMBRE_COMPARTIMENTO"].ToString()
                        };
                        LstReddsCompartimientoCarbono.Add(vReddsCompartimientoCarbono);
                    }
                }
                return LstReddsCompartimientoCarbono;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        public int InsertarRedds(Redds pRedds)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("REDDSSP_INSERTAR_REDDS");
                db.AddOutParameter(cmd, "P_REDDS_ID", DbType.Int32, 0);
                db.AddInParameter(cmd, "P_NOMBRE_RAZON_SOCIAL_PROPONENTE", DbType.String, pRedds.NombreRazonSocial);
                db.AddInParameter(cmd, "P_NOMBRE_INICIATIVA", DbType.String, pRedds.NombreIniciativa);
                db.AddInParameter(cmd, "P_ESTADO_AVANCE_INICIATIVA_ID", DbType.Int32, pRedds.EstadoAvanceIniciativa);
                db.AddInParameter(cmd, "P_COSTO_ESTIMADO_FORMULACION", DbType.Double, pRedds.CostoEstimadoFormulacion);
                db.AddInParameter(cmd, "P_FECHA_INICIO_IMPLEMENTACION", DbType.DateTime, pRedds.FechaInicioImplementacion);
                db.AddInParameter(cmd, "P_FECHA_FIN_IMPLEMENTACION", DbType.DateTime, pRedds.FechaFinImplementacion);
                db.AddInParameter(cmd, "P_ESTANDAR_MERCADO_VOLUNTARIO", DbType.String, pRedds.EstandarMercadeo);
                db.AddInParameter(cmd, "P_METODOLOGIA_ESTANDAR_MERCADO_VOLUNTARIO", DbType.String, pRedds.MetodologiaEstandarMercadeo);
                db.AddInParameter(cmd, "P_AREA_INFLUENCIA", DbType.Double, pRedds.AreaInfluencia);
                db.AddInParameter(cmd, "P_DOCUMENTO_DISEÑO", DbType.String, pRedds.DocumentoDiseño);
                db.AddInParameter(cmd, "P_ARCHIVO_SHAPE", DbType.String, pRedds.ArchivosShape);
                db.AddInParameter(cmd, "P_NUMERO_VITAL", DbType.Int32, pRedds.NumeroVital);
                db.AddInParameter(cmd, "P_RELACION_JURIDICA_ID", DbType.Int32, pRedds.RelacionJuridicaID);
                db.AddInParameter(cmd, "P_CUAL_RELACION_JURIDICA", DbType.String, pRedds.DescRelacionJuridica);
                db.ExecuteNonQuery(cmd);
                pRedds.ReddsID = Convert.ToInt32(db.GetParameterValue(cmd, "@P_REDDS_ID"));
                InsertarParticipantes(pRedds.LstParticipante, pRedds.ReddsID);
                InsertarEstimadoReduccionEmisiones(pRedds.LstEstimadoReduccionEmisiones, pRedds.ReddsID);
                InsertarEstimadoReduccionDeforestacion(pRedds.LstEstimadoReduccionDeforestacion, pRedds.ReddsID);
                InsertarActividad(pRedds.LstActividad, pRedds.ReddsID);
                InsertarCompartimento(pRedds.LstCompartimentoCarbono, pRedds.ReddsID);
                InsertarAutoridadAmbiental(pRedds.LstAutoridades, pRedds.ReddsID);
                InsertarDepartamentoMunicipio(pRedds.LstDeptoMunicipio, pRedds.ReddsID);
                if(pRedds.LstArchivo != null)
                    InsertarArchivoAdicional(pRedds.LstArchivo, pRedds.ReddsID);
                InsertarLocalizacion(pRedds.LstLocalizacion, pRedds.ReddsID);
                if(pRedds.LstEstandarMercado != null)
                    InsertarEstandarMercado(pRedds.LstEstandarMercado, pRedds.ReddsID);
                return pRedds.ReddsID;
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        private void InsertarLocalizacion(List<ReddsLocalizacion> list, int pReddsID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                foreach (ReddsLocalizacion Localizacion in list)
                {
                    DbCommand cmd = db.GetStoredProcCommand("REDDSSP_INSERTAR_LOCALIZACION");
                    db.AddOutParameter(cmd, "P_LOC_ID", DbType.Int32, 0);
                    db.AddInParameter(cmd, "P_REDDS_ID", DbType.Int32, pReddsID);
                    db.AddInParameter(cmd, "P_LOC_NOMBRE", DbType.String, Localizacion.LocNombre);
                    db.AddInParameter(cmd, "P_GEO_ID", DbType.Int32, Localizacion.GeoID);
                    db.ExecuteNonQuery(cmd);
                    int LocID = Convert.ToInt32(db.GetParameterValue(cmd, "P_LOC_ID"));

                    foreach (ReddsCoordenadasLocalizacion coordenada in Localizacion.LstCoordenadas)
                    {
                        DbCommand cmdCoordenada = db.GetStoredProcCommand("REDDSSP_INSERTAR_COORDENADA_LOCALIZACION");
                        db.AddInParameter(cmdCoordenada, "P_LOC_ID", DbType.Int32, LocID);
                        db.AddInParameter(cmdCoordenada, "P_COOR_NORTE", DbType.Double, coordenada.CoorNorte);
                        db.AddInParameter(cmdCoordenada, "P_COOR_ESTE", DbType.Double, coordenada.CoorEste);
                        db.ExecuteNonQuery(cmdCoordenada);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void InsertarArchivoAdicional(List<ReddsArchivos> list, int pReddsID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                foreach (ReddsArchivos Archivo in list)
                {
                    DbCommand cmd = db.GetStoredProcCommand("REDDSSP_INSERTAR_ARCHIVOS");
                    db.AddInParameter(cmd, "P_REDDS_ID", DbType.Int32, pReddsID);
                    db.AddInParameter(cmd, "P_NOMBRE_ARCHIVO", DbType.String, Archivo.Archivo);
                    db.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void InsertarDepartamentoMunicipio(List<ReddsDeptoMunicipio> list, int pReddsID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                foreach (ReddsDeptoMunicipio DeptoMunicipio in list)
                {
                    DbCommand cmd = db.GetStoredProcCommand("REDDSSP_INSERTAR_DEPARTAMENTO_MUNICIPIO");
                    db.AddInParameter(cmd, "P_REDDS_ID", DbType.Int32, pReddsID);
                    db.AddInParameter(cmd, "P_DEPTO_ID", DbType.Int32, DeptoMunicipio.DeptoID);
                    db.AddInParameter(cmd, "P_MUNPIO_ID", DbType.Int32, DeptoMunicipio.MunpioID);
                    db.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void InsertarAutoridadAmbiental(List<ReddsAutoridadAmbiental> list, int pReddsID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                foreach (ReddsAutoridadAmbiental Autoridad in list)
                {
                    DbCommand cmd = db.GetStoredProcCommand("REDDSSP_INSERTAR_AUTORIDAD_AMBIENTAL");
                    db.AddInParameter(cmd, "P_REDDS_ID", DbType.Int32, pReddsID);
                    db.AddInParameter(cmd, "P_AUTORIDAD_ID", DbType.Int32, Autoridad.AutoridadID);
                    db.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void InsertarCompartimento(List<ReddsCompartimientoCarbono> list, int pReddsID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                foreach (ReddsCompartimientoCarbono Compartimento in list)
                {
                    DbCommand cmd = db.GetStoredProcCommand("REDDSSP_INSERTAR_COMPARTIMENTO");
                    db.AddInParameter(cmd, "P_REDDS_ID", DbType.Int32, pReddsID);
                    db.AddInParameter(cmd, "P_COMPARTIMENTO_ID", DbType.Int32, Compartimento.CompartimientoID);
                    db.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void InsertarActividad(List<ReddsActividad> list, int pReddsID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                foreach (ReddsActividad Actividad in list)
                {
                    DbCommand cmd = db.GetStoredProcCommand("REDDSSP_INSERTAR_ACTIVIDAD");
                    db.AddInParameter(cmd, "P_REDDS_ID", DbType.Int32, pReddsID);
                    db.AddInParameter(cmd, "P_ACTIVIDAD_ID", DbType.Int32, Actividad.ActividadID);
                    db.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void InsertarEstimadoReduccionEmisiones(List<ReddsEstimadoReduccionEmisiones> list, int pReddsID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                foreach (ReddsEstimadoReduccionEmisiones emisionReduccion in list)
                {
                    DbCommand cmd = db.GetStoredProcCommand("REDDSSP_INSERTAR_ESTIMADO_REDUCCION_EMISION");
                    db.AddInParameter(cmd, "P_REDDS_ID", DbType.Int32, pReddsID);
                    db.AddInParameter(cmd, "P_AÑO", DbType.Int32, emisionReduccion.Año);
                    db.AddInParameter(cmd, "P_VALOR", DbType.Int32, emisionReduccion.Valor);
                    db.AddInParameter(cmd, "P_VALOR_VERIFICACION", DbType.Int32, emisionReduccion.ValorVerificacion);
                    db.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void InsertarEstimadoReduccionDeforestacion(List<ReddsEstimadoReduccionDeforestacion> list, int pReddsID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                foreach (ReddsEstimadoReduccionDeforestacion emisionReduccion in list)
                {
                    DbCommand cmd = db.GetStoredProcCommand("REDDSSP_INSERTAR_ESTIMADO_REDUCCION_DEFORESTACION");
                    db.AddInParameter(cmd, "P_REDDS_ID", DbType.Int32, pReddsID);
                    db.AddInParameter(cmd, "P_AÑO", DbType.Int32, emisionReduccion.Año);
                    db.AddInParameter(cmd, "P_VALOR", DbType.Int32, emisionReduccion.Valor);
                    db.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void InsertarEstandarMercado(List<ReddsEstandarMercado> list, int pReddsID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                foreach (ReddsEstandarMercado estandarMercado in list)
                {
                    DbCommand cmd = db.GetStoredProcCommand("REDDSSP_INSERTAR_ESTANDAR_MERCADO");
                    db.AddInParameter(cmd, "P_REDDS_ID", DbType.Int32, pReddsID);
                    db.AddInParameter(cmd, "P_NOMBRE_ESTANDAR", DbType.String, estandarMercado.NombreEstandar);
                    db.AddOutParameter(cmd, "P_ESTANDAR_ID", DbType.Int32, 20);
                    db.ExecuteNonQuery(cmd);
                    estandarMercado.EstandarID = Convert.ToInt32(db.GetParameterValue(cmd, "P_ESTANDAR_ID"));
                    InsertarMetodologiaEstandarMercado(estandarMercado.LstMetodologiaEstandar, estandarMercado.EstandarID);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void InsertarMetodologiaEstandarMercado(List<ReddsMetodologiaEstandar> list, int estandarID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                foreach (ReddsMetodologiaEstandar metodologiaEstandar in list)
                {
                    DbCommand cmd = db.GetStoredProcCommand("REDDSSP_INSERTAR_METODOLOGIA_ESTANDAR_MERCADO");
                    db.AddInParameter(cmd, "P_ESTANDAR_ID", DbType.Int32, estandarID);
                    db.AddInParameter(cmd, "P_NOMBRE_METODOLOGIA", DbType.String, metodologiaEstandar.NombreMetodologia);
                    db.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void EliminarParticipantes(int pReddsID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("REDDSSP_ELIMINAR_PARTICIPANTES");
                db.AddInParameter(cmd, "P_REDDS_ID", DbType.Int32, pReddsID);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void InsertarParticipantes(List<ReddsParticipante> list, int pReddsID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                
                foreach (ReddsParticipante participante in list)
                {
                    DbCommand cmd = db.GetStoredProcCommand("REDDSSP_INSERTAR_PARTICIPANTE");
                    db.AddInParameter(cmd, "P_REDDS_ID", DbType.Int32, pReddsID);
                    db.AddInParameter(cmd, "P_NOMBRE_PARTICIPANTE", DbType.String, participante.NombreParticipante);
                    db.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void ActualizarNumeroVital(string numeroVital, int reddsID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("REDDSSP_ACTUALIZAR_NUMERO_VITAL_REDDS");
                db.AddInParameter(cmd, "P_REDDS_ID", DbType.Int32, reddsID);
                db.AddInParameter(cmd, "P_NUMERO_VITAL", DbType.String, numeroVital);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Redds ConsultaRegistroREDDNumeroVital(string numeroVital, bool consultartodo, int? reddsID)
        {
            try
            {
                Redds objRedds = new Redds();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("REDDSSP_CONSULTAR_REDDS");
                db.AddInParameter(cmd, "P_NUMERO_VITAL", DbType.String, numeroVital);
                db.AddInParameter(cmd, "P_REDDS_ID", DbType.Int32, reddsID);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        objRedds.ReddsID = Convert.ToInt32(reader["REDDS_ID"]);
                        objRedds.NombreRazonSocial = reader["NOMBRE_RAZON_SOCIAL_PROPONENTE"].ToString();
                        objRedds.NombreIniciativa = reader["NOMBRE_INICIATIVA"].ToString();
                        objRedds.EstadoAvanceIniciativa = Convert.ToInt32(reader["ESTADO_AVANCE_INICIATIVA_ID"]);
                        objRedds.CostoEstimadoFormulacion = Convert.ToDouble(reader["COSTO_ESTIMADO_FORMULACION"]);
                        objRedds.FechaInicioImplementacion = Convert.ToDateTime(reader["FECHA_INICIO_IMPLEMENTACION"]);
                        objRedds.FechaFinImplementacion = Convert.ToDateTime(reader["FECHA_FIN_IMPLEMENTACION"]);
                        objRedds.EstandarMercadeo = reader["ESTANDAR_MERCADO_VOLUNTARIO"].ToString();
                        objRedds.MetodologiaEstandarMercadeo = reader["METODOLOGIA_ESTANDAR_MERCADO_VOLUNTARIO"].ToString();
                        objRedds.AreaInfluencia = Convert.ToDouble(reader["AREA_INFLUENCIA"]);
                        objRedds.DocumentoDiseño = reader["DOCUMENTO_DISEÑO"].ToString();
                        objRedds.ArchivosShape = reader["ARCHIVO_SHAPE"].ToString();
                        objRedds.NumeroVital = reader["NUMERO_VITAL"].ToString();
                    }
                }
                if (consultartodo)
                {
                    objRedds.LstLocalizacion = ConsultaLocalizaciones(objRedds.ReddsID);
                }
                return objRedds;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        public List<ReddsLocalizacion> ConsultaLocalizaciones(int pReddsID)
        {
            try
            {
                List<ReddsLocalizacion> lstLocalicaciones = new List<ReddsLocalizacion>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("REDDSSP_CONSULTAR_LOCALIZACION");
                db.AddInParameter(cmd, "P_REDDS_ID", DbType.Int32, pReddsID);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while(reader.Read())
                        lstLocalicaciones.Add(new ReddsLocalizacion { GeoID = Convert.ToInt32(reader["GEO_ID"]), LocID = Convert.ToInt32(reader["LOC_ID"]), LocNombre = reader["LOC_NOMBRE"].ToString(), LstCoordenadas = ConsultaCoordenadasLocalizacion(Convert.ToInt32(reader["LOC_ID"])), ReddsID = pReddsID });
                }
                return lstLocalicaciones;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private List<ReddsCoordenadasLocalizacion> ConsultaCoordenadasLocalizacion(int LocID)
        {
            try
            {
                List<ReddsCoordenadasLocalizacion> lstCoordenadas = new List<ReddsCoordenadasLocalizacion>();
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmdCoordenada = db.GetStoredProcCommand("REDDSSP_CONSULTAR_COORDENADA_LOCALIZACION");
                db.AddInParameter(cmdCoordenada, "P_LOC_ID", DbType.Int32, LocID);
                using (IDataReader reader = db.ExecuteReader(cmdCoordenada))
                {
                    while (reader.Read())
                        lstCoordenadas.Add(new ReddsCoordenadasLocalizacion { LocID = LocID, CoorEste = Convert.ToDouble(reader["COOR_ESTE"]), CoorNorte = Convert.ToDouble(reader["COOR_NORTE"]), CoorID = Convert.ToInt32(reader["COOR_ID"]) });
                }
                return lstCoordenadas;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable ConsultaRelacionJuridica()
        {
            try
            {
                 SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                 DbCommand cmdCoordenada = db.GetStoredProcCommand("REDDSSP_LISTA_REALACION_JURIDICA");
                 return db.ExecuteDataSet(cmdCoordenada).Tables[0];
            }
            catch (Exception ex)
            {
                
                throw;
            }

        }
    }
}
