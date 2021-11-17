using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using SILPA.AccesoDatos.Generico;
using SoftManagement.Log;
using System.Data.SqlClient;

namespace SILPA.AccesoDatos.Notificacion
{
    public class PersonaNotificarDalc
    {
        private Configuracion objConfiguracion;

        public PersonaNotificarDalc()
        {
            objConfiguracion = new Configuracion();
        }

        public List<PersonaNotificarEntity> ObtenerPersonas(object[] parametros)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_PERSONA_NOTIFICAR", parametros);
            DataSet dsResultado = new DataSet();

            try
            {
                dsResultado = db.ExecuteDataSet(cmd);
	            List<PersonaNotificarEntity> lista;
	            PersonaNotificarEntity persona;
				
	            TipoIdentificacionDalc tipoIDDalc = new TipoIdentificacionDalc();
	            TipoIdentificacionEntity tipoIDId = new TipoIdentificacionEntity();
	            TipoPersonaNotificacionDalc tipoPerDalc = new TipoPersonaNotificacionDalc();
	            Notificacion.TipoPersonaEntity tipoPerId = new Notificacion.TipoPersonaEntity();
	            NotificacionDalc actoDalc = new NotificacionDalc();
	            EstadoNotificacionDalc estadoDalc = new EstadoNotificacionDalc();
	            EstadoNotificacionEntity estado;
	            object[] parametro;

                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    lista = new List<PersonaNotificarEntity>();
                    foreach (DataRow dt in dsResultado.Tables[0].Rows)
                    {
                        persona = new PersonaNotificarEntity();
                        persona.Id = Convert.ToInt32(dt["ID_PERSONA"]);
                        persona.IdActoNotificar = (Convert.ToDecimal(parametros[1]));
                        parametro = new object[] { Convert.ToInt32(dt["ID_ACTO_NOTIFICACION"]), null, null, null, null, null, null, null, null, null, null };
                        persona.NumeroIdentificacion = dt["NPE_NUMERO_IDENTIFICACION"].ToString();
                        tipoIDId = new TipoIdentificacionEntity();
                        tipoIDId.Id = Convert.ToInt32(dt["ID_TIPO_IDENTIFICACION"]);
                        tipoIDDalc.ObtenerTipoIdentificacion(ref tipoIDId);
                        persona.TipoIdentificacion = tipoIDId;
                        object[] pa = new object[] { Convert.ToInt32(dt["ID_TIPO_PERSONA"]) };
                        persona.TipoPersona = tipoPerDalc.ListarTipoPersona(pa);
                        if (!DBNull.Value.Equals(dt["NPE_NUMERO_NIT"]))
                            persona.NumeroNIT = Convert.ToInt32(dt["NPE_NUMERO_NIT"]);
                        if (!DBNull.Value.Equals(dt["NPE_DIGITO_VER_NIT"]))
                            persona.DigitoVerificacionNIT = Convert.ToInt32(dt["NPE_DIGITO_VER_NIT"]);
                        if (dt["NPE_PRIMER_APELLIDO"].ToString() != null)
                            persona.PrimerApellido = dt["NPE_PRIMER_APELLIDO"].ToString();
                        if (dt["NPE_SEGUNDO_APELLIDO"].ToString() != null)
                            persona.SegundoApellido = dt["NPE_SEGUNDO_APELLIDO"].ToString();
                        if (dt["NPE_PRIMER_NOMBRE"].ToString() != null)
                            persona.PrimerNombre = dt["NPE_PRIMER_NOMBRE"].ToString();
                        if (dt["NPE_SEGUNDO_NOMBRE"].ToString() != null)
                            persona.SegundoApellido = dt["NPE_SEGUNDO_NOMBRE"].ToString();
                        if (dt["NPE_RAZON_SOCIAL"].ToString() != null)
                            persona.RazonSocial = dt["NPE_RAZON_SOCIAL"].ToString();
                        if (!DBNull.Value.Equals(dt["NPE_FECHA_NOTIFICADO"]))
                            persona.FechaNotificado = Convert.ToDateTime(dt["NPE_FECHA_NOTIFICADO"]);
                        //22-sept-2010 - aegb
                        if (!DBNull.Value.Equals(dt["NPE_FECHA_ESTADO_NOTIFICADO"]))
                            persona.FechaEstadoNotificado = Convert.ToDateTime(dt["NPE_FECHA_ESTADO_NOTIFICADO"]);

                        estado = estadoDalc.ListarEstadoNotificacion(new object[] { Convert.ToInt32(dt["ESTADO_NOTIFICADO"]) });
                        persona.EstadoNotificado = estado;
                        if (dt["NPE_CONSECUTIVO"].ToString() != null)
                            persona.ConsecutivoNotificacion = dt["NPE_CONSECUTIVO"].ToString();
                        if (dt["NPE_ANO_NOTIFICACION"].ToString() != null)
                            persona.AnoNotificacion = dt["NPE_ANO_NOTIFICACION"].ToString();

                        if (dt["ESTADO_ACTUAL"].ToString() != null)
                            persona.EstadoActualNotificado = dt["ESTADO_ACTUAL"].ToString().Equals("0") ? false : true;
                        persona.TipoNotificacionId = (dt["ID_TIPO_NOTIFICACION"] != System.DBNull.Value ? Convert.ToInt32(dt["ID_TIPO_NOTIFICACION"]) : -1);
                        persona.EstadoPersonaID = Convert.ToInt32(dt["ID_ESTADO_PERSONA_NOTIFICAR"]);

                        lista.Add(persona);
                    }
                    return lista;
                }
                return null;
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Obtener Personas.";
                throw new Exception(strException, ex);
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                dsResultado.Dispose();
                dsResultado = null;
                db = null;
            }
        }

        /// <summary>
        /// Obtiene las personas asociadas a un acto que no tengan el estado entregado
        /// </summary>
        /// <param name="acto">acto al cual están asociadas las personas</param>
        /// <param name="estado">Estado que no deben poseer las personas</param>
        /// <returns>Lista de Personas</returns>
        /// <remarks>Se usa especialmetne para consultar personas que no tengan el estado EJECUTORIADO</remarks>
        public List<PersonaNotificarEntity> ObtenerPersonasEstado(NotificacionEntity acto, EstadoNotificacionEntity estadoNOT)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { acto.IdActoNotificacion, estadoNOT.ID };
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_PERSONA", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            List<PersonaNotificarEntity> lista;
            PersonaNotificarEntity persona;

            TipoIdentificacionDalc tipoIDDalc = new TipoIdentificacionDalc();
            TipoIdentificacionEntity tipoIDId = new TipoIdentificacionEntity();
            TipoPersonaNotificacionDalc tipoPerDalc = new TipoPersonaNotificacionDalc();
            Notificacion.TipoPersonaEntity tipoPerId = new Notificacion.TipoPersonaEntity();
            NotificacionDalc actoDalc = new NotificacionDalc();
            EstadoNotificacionDalc estadoDalc = new EstadoNotificacionDalc();
            EstadoNotificacionEntity estado;
            object[] parametro;
            try
            {
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    lista = new List<PersonaNotificarEntity>();
                    foreach (DataRow dt in dsResultado.Tables[0].Rows)
                    {
                        persona = new PersonaNotificarEntity();
                        persona.Id = Convert.ToInt32(dt["ID_PERSONA"]);
                        persona.IdActoNotificar = (Convert.ToDecimal(parametros[0].ToString()));
                        parametro = new object[] { Convert.ToInt32(dt["ID_ACTO_NOTIFICACION"]), null, null, null, null, null, null, null, null, null, null };
                        persona.NumeroIdentificacion = dt["NPE_NUMERO_IDENTIFICACION"].ToString();
                        tipoIDId.Id = Convert.ToInt32(dt["ID_TIPO_IDENTIFICACION"]);
                        tipoIDDalc.ObtenerTipoIdentificacion(ref tipoIDId);
                        persona.TipoIdentificacion = tipoIDId;
                        object[] pa = new object[] { Convert.ToInt32(dt["ID_TIPO_PERSONA"]) };
                        persona.TipoPersona = tipoPerDalc.ListarTipoPersona(pa);
                        if (dt["NPE_NUMERO_NIT"] != null)
                            persona.NumeroNIT = Convert.ToInt32(dt["NPE_NUMERO_NIT"]);
                        if (dt["NPE_DIGITO_VER_NIT"] != null)
                            persona.DigitoVerificacionNIT = Convert.ToInt32(dt["NPE_DIGITO_VER_NIT"]);
                        if (!DBNull.Value.Equals(dt["NPE_PRIMER_APELLIDO"]))
                            persona.PrimerApellido = dt["NPE_PRIMER_APELLIDO"].ToString();
                        if (!DBNull.Value.Equals(dt["NPE_SEGUNDO_APELLIDO"]))
                            persona.SegundoApellido = dt["NPE_SEGUNDO_APELLIDO"].ToString();
                        if (!DBNull.Value.Equals(dt["NPE_PRIMER_NOMBRE"]))
                            persona.PrimerNombre = dt["NPE_PRIMER_NOMBRE"].ToString();
                        if (!DBNull.Value.Equals(dt["NPE_SEGUNDO_NOMBRE"]))
                            persona.SegundoNombre = dt["NPE_SEGUNDO_NOMBRE"].ToString();
                        if (!DBNull.Value.Equals(dt["NPE_RAZON_SOCIAL"]))
                            persona.RazonSocial = dt["NPE_RAZON_SOCIAL"].ToString();
                        if (!DBNull.Value.Equals(dt["NPE_FECHA_NOTIFICADO"]))
                            persona.FechaNotificado = Convert.ToDateTime(dt["NPE_FECHA_NOTIFICADO"]);
                        estado = estadoDalc.ListarEstadoNotificacion(new object[] { Convert.ToInt32(dt["ESTADO_NOTIFICADO"]) });
                        persona.EstadoNotificado = estado;
                        if (dt["NPE_CONSECUTIVO"].ToString() != null)
                            persona.ConsecutivoNotificacion = dt["NPE_CONSECUTIVO"].ToString();
                        if (dt["NPE_ANO_NOTIFICACION"].ToString() != null)
                            persona.AnoNotificacion = dt["NPE_ANO_NOTIFICACION"].ToString();

                        lista.Add(persona);
                    }
                    return lista;
                }
                return null;
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                dsResultado.Dispose();
                dsResultado = null;
                db = null;
            }
        }


        /// <summary>
        /// Obtiene las personas asociadas a un acto que no tengan el estado entregado
        /// </summary>
        /// <param name="acto">acto al cual están asociadas las personas</param>
        /// <param name="estado">Estado que no deben poseer las personas</param>
        /// <returns>Lista de Personas</returns>
        /// <remarks>Se usa especialmetne para consultar personas que no tengan el estado EJECUTORIADO</remarks>
        public List<PersonaNotificarEntity> ObtenerPersonasEstado(NotificacionEntity acto, EstadoNotificacionEntity estadoNOT, long idPersona)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { acto.IdActoNotificacion, estadoNOT.ID, idPersona };
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_PERSONA", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            List<PersonaNotificarEntity> lista;
            PersonaNotificarEntity persona;

            TipoIdentificacionDalc tipoIDDalc = new TipoIdentificacionDalc();
            TipoIdentificacionEntity tipoIDId = new TipoIdentificacionEntity();
            TipoPersonaNotificacionDalc tipoPerDalc = new TipoPersonaNotificacionDalc();
            Notificacion.TipoPersonaEntity tipoPerId = new Notificacion.TipoPersonaEntity();
            NotificacionDalc actoDalc = new NotificacionDalc();
            EstadoNotificacionDalc estadoDalc = new EstadoNotificacionDalc();
            EstadoNotificacionEntity estado;
            object[] parametro;
            try
            {
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    lista = new List<PersonaNotificarEntity>();
                    foreach (DataRow dt in dsResultado.Tables[0].Rows)
                    {
                        persona = new PersonaNotificarEntity();
                        persona.Id = Convert.ToInt32(dt["ID_PERSONA"]);
                        persona.IdActoNotificar = (Convert.ToDecimal(parametros[0].ToString()));
                        parametro = new object[] { Convert.ToInt32(dt["ID_ACTO_NOTIFICACION"]), null, null, null, null, null, null, null, null, null, null };
                        persona.NumeroIdentificacion = dt["NPE_NUMERO_IDENTIFICACION"].ToString();
                        tipoIDId.Id = Convert.ToInt32(dt["ID_TIPO_IDENTIFICACION"]);
                        tipoIDDalc.ObtenerTipoIdentificacion(ref tipoIDId);
                        persona.TipoIdentificacion = tipoIDId;
                        object[] pa = new object[] { Convert.ToInt32(dt["ID_TIPO_PERSONA"]) };
                        persona.TipoPersona = tipoPerDalc.ListarTipoPersona(pa);
                        if (dt["NPE_NUMERO_NIT"] != null)
                            persona.NumeroNIT = Convert.ToInt32(dt["NPE_NUMERO_NIT"]);
                        if (dt["NPE_DIGITO_VER_NIT"] != null)
                            persona.DigitoVerificacionNIT = Convert.ToInt32(dt["NPE_DIGITO_VER_NIT"]);
                        if (!DBNull.Value.Equals(dt["NPE_PRIMER_APELLIDO"]))
                            persona.PrimerApellido = dt["NPE_PRIMER_APELLIDO"].ToString();
                        if (!DBNull.Value.Equals(dt["NPE_SEGUNDO_APELLIDO"]))
                            persona.SegundoApellido = dt["NPE_SEGUNDO_APELLIDO"].ToString();
                        if (!DBNull.Value.Equals(dt["NPE_PRIMER_NOMBRE"]))
                            persona.PrimerNombre = dt["NPE_PRIMER_NOMBRE"].ToString();
                        if (!DBNull.Value.Equals(dt["NPE_SEGUNDO_NOMBRE"]))
                            persona.SegundoNombre = dt["NPE_SEGUNDO_NOMBRE"].ToString();
                        if (!DBNull.Value.Equals(dt["NPE_RAZON_SOCIAL"]))
                            persona.RazonSocial = dt["NPE_RAZON_SOCIAL"].ToString();
                        if (!DBNull.Value.Equals(dt["NPE_FECHA_NOTIFICADO"]))
                            persona.FechaNotificado = Convert.ToDateTime(dt["NPE_FECHA_NOTIFICADO"]);
                        estado = estadoDalc.ListarEstadoNotificacion(new object[] { Convert.ToInt32(dt["ESTADO_NOTIFICADO"]) });
                        persona.EstadoNotificado = estado;
                        if (dt["NPE_CONSECUTIVO"].ToString() != null)
                            persona.ConsecutivoNotificacion = dt["NPE_CONSECUTIVO"].ToString();
                        if (dt["NPE_ANO_NOTIFICACION"].ToString() != null)
                            persona.AnoNotificacion = dt["NPE_ANO_NOTIFICACION"].ToString();
                        persona.TipoNotificacionId = (dt["ID_TIPO_NOTIFICACION"] != System.DBNull.Value ? Convert.ToInt32(dt["ID_TIPO_NOTIFICACION"]) : -1);
                        persona.FlujoNotificacionId = (dt["ID_FLUJO_NOT_ELEC"] != System.DBNull.Value ? Convert.ToInt32(dt["ID_FLUJO_NOT_ELEC"]) : -1);

                        lista.Add(persona);
                    }
                    return lista;
                }
                return null;
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                dsResultado.Dispose();
                dsResultado = null;
                db = null;
            }
        }


        public PersonaNotificarEntity ObtenerPersona(object[] parametros)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_PERSONA_NOTIFICAR", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            PersonaNotificarEntity persona;

            TipoIdentificacionDalc tipoIDDalc = new TipoIdentificacionDalc();
            TipoIdentificacionEntity tipoIDId = new TipoIdentificacionEntity();
            TipoPersonaNotificacionDalc tipoPerDalc = new TipoPersonaNotificacionDalc();
            Generico.TipoPersonaIdentity tipoPerId = new Generico.TipoPersonaIdentity();
            NotificacionDalc actoDalc = new NotificacionDalc();
            EstadoNotificacionDalc estadoDalc = new EstadoNotificacionDalc();
            EstadoNotificacionEntity estado;
            //object[] parametro;
            try
            {
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dt in dsResultado.Tables[0].Rows)
                    {
                        estado = new EstadoNotificacionEntity();
                        persona = new PersonaNotificarEntity();
                        persona.Id = Convert.ToInt32(dt["ID_PERSONA"]);
                        persona.IdActoNotificar = Convert.ToDecimal(dt["ID_ACTO_NOTIFICACION"]);
                        persona.NumeroIdentificacion = dt["NPE_NUMERO_IDENTIFICACION"].ToString();
                        tipoIDId.Id = Convert.ToInt32(dt["ID_TIPO_IDENTIFICACION"]);
                        tipoIDDalc.ObtenerTipoIdentificacion(ref tipoIDId);
                        persona.TipoIdentificacion = tipoIDId;
                        tipoPerId.CodigoTipoPersona = Convert.ToInt32(dt["ID_TIPO_PERSONA"]);
                        //tipoPerDalc ObtenerTipoPersona(ref tipoPerId);
                        Notificacion.TipoPersonaEntity o = new TipoPersonaEntity();
                        o.ID = tipoPerId.CodigoTipoPersona;
                        o.Nombre = tipoPerId.NombreTipoPersona;
                        persona.TipoPersona = o;
                        if (dt["NPE_NUMERO_NIT"] != null)
                            persona.NumeroNIT = Convert.ToInt32(dt["NPE_NUMERO_NIT"]);
                        if (dt["NPE_DIGITO_VER_NIT"] != null)
                            persona.DigitoVerificacionNIT = Convert.ToInt32(dt["NPE_DIGITO_VER_NIT"]);
                        if (dt["NPE_PRIMER_APELLIDO"].ToString() != null)
                            persona.PrimerApellido = dt["NPE_PRIMER_APELLIDO"].ToString();
                        if (dt["NPE_SEGUNDO_APELLIDO"].ToString() != null)
                            persona.SegundoApellido = dt["NPE_SEGUNDO_APELLIDO"].ToString();
                        if (dt["NPE_PRIMER_NOMBRE"].ToString() != null)
                            persona.PrimerNombre = dt["NPE_PRIMER_NOMBRE"].ToString();
                        if (dt["NPE_SEGUNDO_NOMBRE"].ToString() != null)
                            persona.SegundoApellido = dt["NPE_SEGUNDO_NOMBRE"].ToString();
                        if (dt["NPE_RAZON_SOCIAL"].ToString() != null)
                            persona.RazonSocial = dt["NPE_RAZON_SOCIAL"].ToString();
                        if (dt["NPE_FECHA_NOTIFICADO"].ToString() != null)
                            persona.FechaNotificado = Convert.ToDateTime(dt["NPE_FECHA_NOTIFICADO"]);
                        estado = estadoDalc.ListarEstadoNotificacion(new object[] { Convert.ToInt32(dt["ESTADO_NOTIFICADO"]) });
                        persona.EstadoNotificado = estado;
                        if (dt["NPE_CONSECUTIVO"].ToString() != null)
                            persona.ConsecutivoNotificacion = dt["NPE_CONSECUTIVO"].ToString();
                        if (dt["NPE_ANO_NOTIFICACION"].ToString() != null)
                            persona.AnoNotificacion = dt["NPE_ANO_NOTIFICACION"].ToString();
                        if (dt["ID_TIPO_NOTIFICACION"].ToString() != null)
                            persona.TipoNotificacionId = Convert.ToInt32(dt["ID_TIPO_NOTIFICACION"]);
                        if (dt["ID_FLUJO_NOT_ELEC"].ToString() != null)
                            persona.FlujoNotificacionId = Convert.ToInt32(dt["ID_FLUJO_NOT_ELEC"]);

                        return persona;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                dsResultado.Dispose();
                dsResultado = null;
                db = null;
            }
        }

        public void Insertar(ref PersonaNotificarEntity persona)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] {    persona.IdActoNotificar,
                                                    persona.Id,
                                                    persona.NumeroIdentificacion, 
                                                    persona.TipoIdentificacion.Id, 
                                                    persona.TipoPersona.ID, 
                                                    persona.NumeroNIT, 
                                                    persona.DigitoVerificacionNIT, 
                                                    persona.PrimerApellido, 
                                                    persona.SegundoApellido, 
                                                    persona.PrimerNombre, 
                                                    persona.SegundoNombre, 
                                                    persona.RazonSocial, 
                                                    persona.EstadoNotificado.ID, 
                                                    persona.FechaNotificado, 
                                                    persona.AnoNotificacion, 
                                                    persona.ConsecutivoNotificacion,
                                                    persona.EsNotificacionElectronica,
                                                    persona.TipoNotificacionId,
                                                    persona.FlujoNotificacionId,
                                                    persona.EstadoPersonaID
            };

            DbCommand cmd = db.GetStoredProcCommand("NOT_INSERT_PERSONA_NOTIFICAR", parametros);
            try
            {
                db.ExecuteNonQuery(cmd);
                persona.Id = Convert.ToDecimal(db.GetParameterValue(cmd, "P_ID_PERSONA"));
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Insertar Persona Notificar.";
                throw new Exception(strException, ex);
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }

        public void Actualizar(ref PersonaNotificarEntity persona)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] {    persona.IdActoNotificar, 
                                                    persona.Id,
                                                    persona.NumeroIdentificacion, 
                                                    persona.TipoIdentificacion.Id, 
                                                    persona.TipoPersona.ID, 
                                                    persona.NumeroNIT, 
                                                    persona.DigitoVerificacionNIT, 
                                                    persona.PrimerApellido, 
                                                    persona.SegundoApellido, 
                                                    persona.PrimerNombre, 
                                                    persona.SegundoNombre, 
                                                    persona.RazonSocial, 
                                                    persona.EstadoNotificado.ID, 
                                                    persona.FechaNotificado, 
                                                    persona.AnoNotificacion, 
                                                    persona.ConsecutivoNotificacion, 1,
                                                    persona.EsNotificacionElectronica,
                                                    persona.TipoNotificacionId,
                                                    persona.FlujoNotificacionId};



            DbCommand cmd = db.GetStoredProcCommand("NOT_UPDATE_PERSONA_NOTIFICAR", parametros);
            try
            {
                db.ExecuteNonQuery(cmd);
                //cmd.ExecuteNonQuery();
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }


        public void Eliminar(long p_lngActoID, long p_lngPersonaID, string p_strMotivoEliminacion, string p_strUsuario)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] {    p_lngActoID, 
                                                    p_lngPersonaID,
                                                    p_strMotivoEliminacion,
                                                    p_strUsuario
                                                };



            DbCommand cmd = db.GetStoredProcCommand("NOT_INSERTAR_ELIMINACION_PERSONA", parametros);
            try
            {
                db.ExecuteNonQuery(cmd);
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }


        /// <summary>
        /// HAVA: 
        /// Metodo que determina si el estado que se actulizará proviene desde PDI
        /// </summary>
        /// <param name="persona"></param>
        /// <param name="provienePDI">int: 0/1</param>
        public void Actualizar(ref PersonaNotificarEntity persona, int provienePDI)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] {    persona.IdActoNotificar, 
                                                    persona.Id,
                                                    persona.NumeroIdentificacion, 
                                                    persona.TipoIdentificacion.Id, 
                                                    persona.TipoPersona.ID, 
                                                    persona.NumeroNIT, 
                                                    persona.DigitoVerificacionNIT, 
                                                    persona.PrimerApellido, 
                                                    persona.SegundoApellido, 
                                                    persona.PrimerNombre, 
                                                    persona.SegundoNombre, 
                                                    persona.RazonSocial, 
                                                    persona.EstadoNotificado.ID, 
                                                    persona.FechaNotificado, 
                                                    persona.AnoNotificacion, 
                                                    persona.ConsecutivoNotificacion, provienePDI,
                                                    persona.EsNotificacionElectronica,
                                                    persona.TipoNotificacionId,
                                                    persona.FlujoNotificacionId};



            DbCommand cmd = db.GetStoredProcCommand("NOT_UPDATE_PERSONA_NOTIFICAR", parametros);
            try
            {
                db.ExecuteNonQuery(cmd);
                //cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al determinar si el estado que se actulizará proviene desde PDI.";
                throw new Exception(strException, ex);
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }

        /// <summary>
        /// HAVA:23-DIC-2010
        /// Obtiene las fechas de citación y Notificación de un acto admiknistratovo
        /// </summary>
        /// <returns></returns>
        public DataSet ObtenerFechaCitacionNotificacion(string numeroActo, int idAA, string codigoExpediente)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { numeroActo, idAA, codigoExpediente };
            DbCommand cmd = db.GetStoredProcCommand("NOT_OBTENER_ESTADOS_NOTIFICACION_ACTO_ADMINISTRATIVO", parametros);
            try
            {
                return db.ExecuteDataSet(cmd);
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }

        /// <summary>
        /// Retorna el número de días que ha permanecido la notificación en el estado actual
        /// </summary>
        /// <param name="p_strIdPersona">string con el id de la persona que se notifica</param>
        /// <param name="p_strIdActo">string con identificador del acto de notificación</param>        
        /// <returns>int con el número de días</returns>
        public int ObtenerNumeroDiasNotificacion(string p_strIdPersona, string p_strIdActoNot)
        {
            int intNumeroDias = -1;
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { p_strIdPersona, p_strIdActoNot, intNumeroDias };
            DbCommand cmd = db.GetStoredProcCommand("NOT_NUMERO_DIAS_NOTIFICACION");
            db.AddInParameter(cmd, "P_ID_PERSONA", DbType.Int32, p_strIdPersona);
            db.AddInParameter(cmd, "P_ID_ACTO_NOTIFICACION", DbType.Int32, p_strIdActoNot);
            db.AddOutParameter(cmd, "P_NUMERO_DIAS", DbType.Int32, 8);

            try
            {
                db.ExecuteNonQuery(cmd);
                intNumeroDias = Convert.ToInt32(db.GetParameterValue(cmd, "P_NUMERO_DIAS"));
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                db = null;
            }

            return intNumeroDias;
        }


        /// <summary>
        /// Obtener el listado de direcciones de una persona
        /// </summary>
        /// <param name="p_lngPersonaID">long con el identificador de la persona</param>
        /// <returns>DataSet con la información de las direcciones</returns>
        public DataSet ObtenerListadoDireccionesNotificar(long p_lngPersonaID)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            DataSet objDirecciones = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("NOT_CONSULTAR_DIRECCIONES_PERSONA");
                objDataBase.AddInParameter(objCommand, "@P_ID_PERSONA", DbType.Int64, p_lngPersonaID);

                //Crear registro
                objDirecciones = objDataBase.ExecuteDataSet(objCommand);
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PersonaNotificarDalc :: ObtenerListadoDireccionesNotificar -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PersonaNotificarDalc :: ObtenerListadoDireccionesNotificar -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }
            finally
            {
                if (objCommand != null)
                {
                    objCommand.Dispose();
                    objCommand = null;
                }
                if (objDataBase != null)
                    objDataBase = null;
            }

            return objDirecciones;
        }


        /// <summary>
        /// Obtener el listado de direcciones de una persona
        /// </summary>
        /// <param name="p_strNumeroIdentificacion">string con el numero de identificacion</param>
        /// <returns>List con la información de las direcciones</returns>
        public List<DireccionNotificacionEntity> ObtenerListadoDireccionesNotificarNumeroIdentificacion(string p_strNumeroIdentificacion = "")
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            DataSet objDirecciones = null;
            List<DireccionNotificacionEntity> objLstDirecciones = new List<DireccionNotificacionEntity>();

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("NOT_CONSULTAR_DIRECCIONES_PERSONA");
                objDataBase.AddInParameter(objCommand, "@P_NUMERO_IDENTIFICACION", DbType.String, p_strNumeroIdentificacion);

                //Crear registro
                objDirecciones = objDataBase.ExecuteDataSet(objCommand);

                //Verificar si se obtuvo direcciones
                if (objDirecciones != null && objDirecciones.Tables.Count > 0 && objDirecciones.Tables[0].Rows.Count > 0)
                {
                    //Ciclo que carga los datos
                    foreach (DataRow objDireccion in objDirecciones.Tables[0].Rows)
                    {
                        objLstDirecciones.Add(new DireccionNotificacionEntity
                        {
                            DireccionID = Convert.ToInt64(objDireccion["DIP_ID"]),
                            Pertenece = objDireccion["PERTENECE"].ToString(),
                            Departamento = objDireccion["DEP_NOMBRE"].ToString(),
                            Municipio = objDireccion["MUN_NOMBRE"].ToString(),
                            Direccion = objDireccion["DIP_DIRECCION"].ToString()
                        });
                    }
                }
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PersonaNotificarDalc :: ObtenerListadoDireccionesNotificarNumeroIdentificacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PersonaNotificarDalc :: ObtenerListadoDireccionesNotificarNumeroIdentificacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }
            finally
            {
                if (objCommand != null)
                {
                    objCommand.Dispose();
                    objCommand = null;
                }
                if (objDataBase != null)
                    objDataBase = null;
            }

            return objLstDirecciones;
        }


        /// <summary>
        /// Obtener la información de la dirección de la persona
        /// </summary>
        /// <param name="p_strTipoDireccionID">string con el tipo de dirección a buscar</param>
        /// <param name="p_lngPersonaID">long con el identificador de la persona</param>
        /// <param name="p_strNumeroIdentificacion">string con el número de identificación</param>
        /// <param name="p_lngDIreccionID">long con el identificador de la dirección.</param>
        /// <returns>DataRow con la información de la dirección</returns>
        public DataRow ObtenerInformacionDireccionPersona(long p_lngPersonaID, long p_lngDIreccionID)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            DataSet objDirecciones = null;
            DataRow objDireccion = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("NOT_CONSULTAR_DIRECCION_PERSONA");
                if (p_lngPersonaID > 0)
                    objDataBase.AddInParameter(objCommand, "@P_ID_PERSONA", DbType.Int64, p_lngPersonaID);
                objDataBase.AddInParameter(objCommand, "@P_DIP_ID", DbType.Int64, p_lngDIreccionID);

                //Crear registro
                objDirecciones = objDataBase.ExecuteDataSet(objCommand);

                if (objDirecciones != null && objDirecciones.Tables != null && objDirecciones.Tables[0].Rows.Count > 0)
                {
                    objDireccion = objDirecciones.Tables[0].Rows[0];
                }
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PersonaNotificarDalc :: ObtenerInformacionDireccionPersona -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PersonaNotificarDalc :: ObtenerListadoDireccionesNotificar -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }
            finally
            {
                if (objCommand != null)
                {
                    objCommand.Dispose();
                    objCommand = null;
                }
                if (objDataBase != null)
                    objDataBase = null;
            }

            return objDireccion;
        }


        /// <summary>
        /// Obtener la información de la dirección de la persona
        /// </summary>
        /// <param name="p_lngDIreccionID">long con el identificador de la direccion</param>
        /// <returns>DireccionNotificacionEntity con la información de la dirección</returns>
        public DireccionNotificacionEntity ObtenerInformacionDireccionPersona(long p_lngDIreccionID)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            DataSet objDirecciones = null;
            DireccionNotificacionEntity objDireccion = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("NOT_CONSULTAR_DIRECCION_PERSONA");
                objDataBase.AddInParameter(objCommand, "@P_DIP_ID", DbType.Int64, p_lngDIreccionID);

                //Crear registro
                objDirecciones = objDataBase.ExecuteDataSet(objCommand);

                if (objDirecciones != null && objDirecciones.Tables != null && objDirecciones.Tables[0].Rows.Count > 0)
                {
                    objDireccion = new DireccionNotificacionEntity
                    {
                        DireccionID = Convert.ToInt64(objDirecciones.Tables[0].Rows[0]["DIP_ID"]),
                        Pertenece = objDirecciones.Tables[0].Rows[0]["PERTENECE"].ToString(),
                        Departamento = objDirecciones.Tables[0].Rows[0]["DEP_NOMBRE"].ToString(),
                        Municipio = objDirecciones.Tables[0].Rows[0]["MUN_NOMBRE"].ToString(),
                        Direccion = objDirecciones.Tables[0].Rows[0]["DIP_DIRECCION"].ToString()
                    };
                }
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PersonaNotificarDalc :: ObtenerInformacionDireccionPersona -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PersonaNotificarDalc :: ObtenerListadoDireccionesNotificar -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }
            finally
            {
                if (objCommand != null)
                {
                    objCommand.Dispose();
                    objCommand = null;
                }
                if (objDataBase != null)
                    objDataBase = null;
            }

            return objDireccion;
        }


        /// <summary>
        /// Obtener el listado de correos de una persona
        /// </summary>
        /// <param name="p_lngPersonaID">long con el identificador de la persona</param>
        /// <param name="p_intTipoCorreo">int con el tipo de correo que se debe enviar</param>
        /// <param name="p_strExpediente">string con el expediente. Opcional</param>
        /// <returns>DataSet con la información de las direcciones</returns>
        public DataSet ObtenerListadoCorreosNotificar(long p_lngPersonaID)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            DataSet objDirecciones = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("NOT_CONSULTAR_CORREOS_PERSONA");
                objDataBase.AddInParameter(objCommand, "@P_ID_PERSONA", DbType.Int64, p_lngPersonaID);

                //Crear registro
                objDirecciones = objDataBase.ExecuteDataSet(objCommand);
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PersonaNotificarDalc :: ObtenerListadoCorreosNotificar -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PersonaNotificarDalc :: ObtenerListadoCorreosNotificar -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }
            finally
            {
                if (objCommand != null)
                {
                    objCommand.Dispose();
                    objCommand = null;
                }
                if (objDataBase != null)
                    objDataBase = null;
            }

            return objDirecciones;
        }


        /// <summary>
        /// Obtener el listado de correos de una persona
        /// </summary>
        /// <param name="p_lngPersonaID">long con el identificador de la persona</param>
        /// <param name="p_strNumeroVital">string con el numero Vital. Opcional</param>
        /// <returns>DataSet con la información de las direcciones</returns>
        public DataSet ObtenerListadoCorreosNotificar(long p_lngPersonaID, string p_strNumeroVital = "")
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            DataSet objDirecciones = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("NOT_CONSULTAR_CORREOS_PERSONA_SILA_VITAL");
                objDataBase.AddInParameter(objCommand, "@P_ID_PERSONA", DbType.Int64, p_lngPersonaID);
                if (!string.IsNullOrEmpty(p_strNumeroVital))
                    objDataBase.AddInParameter(objCommand, "@P_NUMERO_SILPA", DbType.String, p_strNumeroVital);

                //Crear registro
                objDirecciones = objDataBase.ExecuteDataSet(objCommand);
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PersonaNotificarDalc :: ObtenerListadoCorreosNotificar -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PersonaNotificarDalc :: ObtenerListadoCorreosNotificar -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }
            finally
            {
                if (objCommand != null)
                {
                    objCommand.Dispose();
                    objCommand = null;
                }
                if (objDataBase != null)
                    objDataBase = null;
            }

            return objDirecciones;
        }

        /// <summary>
        /// Obtener el listado de correos de una persona
        /// </summary>
        /// <param name="p_strNumeroIdentificacion">string con el numero de identificacion</param>
        /// <returns>DataSet con la información de las direcciones</returns>
        public List<string> ObtenerListadoCorreosNotificarNumeroIdentificacion(string p_strNumeroIdentificacion)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            DataSet objDirecciones = null;
            List<string> objLstCorreos = new List<string>();

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("NOT_CONSULTAR_CORREOS_PERSONA_SILA_VITAL");
                objDataBase.AddInParameter(objCommand, "@P_NUMERO_IDENTIFICACION", DbType.String, p_strNumeroIdentificacion);

                //Crear registro
                objDirecciones = objDataBase.ExecuteDataSet(objCommand);
                if (objDirecciones != null && objDirecciones.Tables != null && objDirecciones.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow objDireccion in objDirecciones.Tables[0].Rows)
                        objLstCorreos.Add(objDireccion["PER_CORREO_ELECTRONICO"].ToString());
                }
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PersonaNotificarDalc :: ObtenerListadoCorreosNotificarNumeroIdentificacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PersonaNotificarDalc :: ObtenerListadoCorreosNotificarNumeroIdentificacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }
            finally
            {
                if (objCommand != null)
                {
                    objCommand.Dispose();
                    objCommand = null;
                }
                if (objDataBase != null)
                    objDataBase = null;
            }

            return objLstCorreos;
        }


        /// <summary>
        /// Realizar el avance automatico de los estados que se encuentren pendientes de cambio
        /// </summary>
        /// <returns>DataTable con la información de los estados que se deben avanzar</returns>
        public DataTable ObtenerListadoEstadosPendientesAvanceAutomatico()
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            DataSet objInformacionEstados = null;
            DataTable objEstados = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("NOT_CONSULTAR_ESTADOS_PENDIENTES_AVANZAR");

                //Crear registro
                objInformacionEstados = objDataBase.ExecuteDataSet(objCommand);

                if (objInformacionEstados != null && objInformacionEstados.Tables.Count > 0)
                {
                    objEstados = objInformacionEstados.Tables[0];
                }

            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PersonaNotificarDalc :: ObtenerListadoEstadosPendientesAvanceAutomatico -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PersonaNotificarDalc :: ObtenerListadoEstadosPendientesAvanceAutomatico -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }
            finally
            {
                if (objCommand != null)
                {
                    objCommand.Dispose();
                    objCommand = null;
                }
                if (objDataBase != null)
                    objDataBase = null;
            }

            return objEstados;
        }


        /// <summary>
        /// Obtener la información de personas asociadas a un acto por tipo de notificación
        /// </summary>
        /// <param name="p_lngActoId">long con el identificador del acto administrativo</param>
        /// <param name="p_intTipoNotificacionID">long con el identificador del tipo de notificación</param>
        /// <param name="p_lngPersonaID">long con el identificador de la persona. Opcional</param>
        /// <returns>DataTable con la información de las personas</returns>
        public DataTable ObtenerListadoPersonasNotificarActoAdmin(long p_lngActoId, int p_intTipoNotificacionID, long p_lngPersonaID = 0)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            DataSet objPersonasInfo = null;
            DataTable objPersonas = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("NOT_ADM_LISTA_PERSONA_NOTIFICAR");
                objDataBase.AddInParameter(objCommand, "@P_ID_ACTO_NOTIFICACION", DbType.Int64, p_lngActoId);
                objDataBase.AddInParameter(objCommand, "@P_ID_TIPO_NOTIFICACION", DbType.Int32, p_intTipoNotificacionID);
                if (p_lngPersonaID > 0)
                    objDataBase.AddInParameter(objCommand, "@P_ID_PERSONA", DbType.Int64, p_lngPersonaID);

                //Obtener datos
                objPersonasInfo = objDataBase.ExecuteDataSet(objCommand);

                //Cargar datatable
                if (objPersonasInfo != null && objPersonasInfo.Tables.Count > 0 && objPersonasInfo.Tables[0].Rows.Count > 0)
                {
                    objPersonas = objPersonasInfo.Tables[0];
                    objPersonas.TableName = "PERSONAS";
                }
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PersonaNotificarDalc :: ObtenerListadoPersonasNotificarActoAdmin -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PersonaNotificarDalc :: ObtenerListadoPersonasNotificarActoAdmin -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }
            finally
            {
                if (objCommand != null)
                {
                    objCommand.Dispose();
                    objCommand = null;
                }
                if (objDataBase != null)
                    objDataBase = null;
            }

            return objPersonas;
        }


        /// <summary>
        /// Modificar el estado de una persona
        /// </summary>
        /// <param name="p_lngPersonaID">long con el identificador de la persona</param>
        /// <param name="p_intEstadoPersonaID">int con el estado de la persona</param>
        public void ModificarEstadoPersona(long p_lngPersonaID, int p_intEstadoPersonaID)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("NOT_ADM_MODIFICAR_ESTADO_PERSONA_NOTIFICAR");
                objDataBase.AddInParameter(objCommand, "@P_ID_PERSONA", DbType.Int64, p_lngPersonaID);
                objDataBase.AddInParameter(objCommand, "@P_ID_ESTADO_PERSONA_NOTIFICAR", DbType.Int32, p_intEstadoPersonaID);

                //Modificar
                objDataBase.ExecuteNonQuery(objCommand);
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PersonaNotificarDalc :: ModificarEstadoPersona -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PersonaNotificarDalc :: ModificarEstadoPersona -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }
            finally
            {
                if (objCommand != null)
                {
                    objCommand.Dispose();
                    objCommand = null;
                }
                if (objDataBase != null)
                    objDataBase = null;
            }
        }
    }
}
