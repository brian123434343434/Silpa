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
    public class NotificacionDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public NotificacionDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Constructor de la clase que a su vez carga los valores para una identidad de los tipos de identificación
        /// cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador del tipos de identificación a cargar, en la propiedad ID del objetoIdentity</param>
        public List<NotificacionEntity> ObtenerActosParaConsultarPDI(EstadoNotificacionEntity estado)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { estado.ID };
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_ACTOS_ESTADO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            NotificacionEntity _objActo;
            DependenciaEntidadEntity _objDependencia = new DependenciaEntidadEntity();

            TipoIdentificacionDalc _tipoIDDalc = new TipoIdentificacionDalc();
            TipoIdentificacionEntity _tipoID = new TipoIdentificacionEntity();
            List<NotificacionEntity> listaActos = new List<NotificacionEntity>();
            TipoDocumentoIdentity _tipoActo = new TipoDocumentoIdentity();
            TipoDocumentoDalc _tipoActoDalc = new TipoDocumentoDalc();
            EstadoNotificacionDalc _objEstadoDalc = new EstadoNotificacionDalc();
            if (dsResultado.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dt in dsResultado.Tables[0].Rows)
                {
                    _tipoActo = new TipoDocumentoIdentity();
                    _tipoID = new TipoIdentificacionEntity();
                    _objDependencia = new DependenciaEntidadEntity();
                    _objActo = new NotificacionEntity();

                    _objActo.IdActoNotificacion = Convert.ToDecimal(dt["ID_ACTO_NOTIFICACION"]);
                    _objActo.NumeroActoAdministrativo = Convert.ToString(dt["NOT_NUMERO_ACTO_ADMINISTRATIVO"]);
                    _objActo.ParteResolutiva = Convert.ToString(dt["NOT_PARTE_RESOLUTIVA"]);
                    _objActo.CodigoEntidadPublica = Convert.ToInt32(dt["NOT_CODIGO_ENTIDAD_PUBLICA"]);
                    _objActo.ProcesoAdministracion = Convert.ToString(dt["NOT_PROCESO_ADMINISTRACION"]);

                    DependenciaEntidadDalc depDalc = new DependenciaEntidadDalc();
                    _objActo.DependenciaEntidad = depDalc.ObtenerDependencia(Convert.ToInt32(dt["NOT_ID_DEPENDENCIA_ENTIDAD"]));

                    _objDependencia.ID = Convert.ToString(dt["NOT_ID_DEPENDENCIA_ENTIDAD"]);
                    _objActo.DependenciaEntidad = _objDependencia;
                    _objActo.SistemaEntidadPublica = Convert.ToInt32(dt["NOT_SISTEMA_ENTIDAD_PUBLICA"]);
                    _tipoID.Id = Convert.ToInt32(dt["NOT_TIPO_ID_FUNCIONARIO"]);
                    _tipoIDDalc.ObtenerTipoIdentificacion(ref _tipoID);
                    _objActo.TipoIdentificacionFuncionario = _tipoID;
                    _objActo.IdentificacionFuncionario = Convert.ToString(dt["NOT_ID_FUNCIONARIO_NOTIFICANTE"]);
                    _objActo.IdPlantilla = Convert.ToString(dt["NOT_ID_PLANTILLA"]);
                    if (dt["NOT_NOMBRE_PLANTILLA"] != DBNull.Value)
                        _objActo.NombrePlantilla = Convert.ToString(dt["NOT_NOMBRE_PLANTILLA"]);
                    if (dt["NOT_NUMERO_ACTO_ADMINISTRATIVO_ASOCIADO"] != DBNull.Value)
                        _objActo.NumeroActoAdministrativoAsociado = Convert.ToString(dt["NOT_NUMERO_ACTO_ADMINISTRATIVO_ASOCIADO"]);
                    if (dt["NOT_REQUIERE_PUBLICACION"] != DBNull.Value)
                        _objActo.RequierePublicacion = Convert.ToBoolean(dt["NOT_REQUIERE_PUBLICACION"]);
                    //if (dt["NOT_FECHA_ACTO"] != DBNull.Value)
                    //    _objActo.FechaActo = Convert.ToDateTime(dt["NOT_FECHA_ACTO"]);
                    if (dt["NOT_ID_TIPO_ACTO"] != DBNull.Value)
                    {
                        _tipoActo = new TipoDocumentoIdentity();
                        _tipoActo = _tipoActoDalc.ObtenerTipoDocumento(Convert.ToInt32(dt["NOT_ID_TIPO_ACTO"]), null);
                        _objActo.IdTipoActo = _tipoActo;
                    }
                    _objActo.NumeroSILPA = Convert.ToString(dt["NOT_NUMERO_SILPA"]);

                    listaActos.Add(_objActo);
                }
            }

            return listaActos;
        }

        public NotificacionEntity ConsultarEjecutoriaActo(string numActo,string codExpe,string numeroSilpa,int? tipoActoAdministrativo, out string respuesta,out bool Permite)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { numActo, codExpe, numeroSilpa, tipoActoAdministrativo };
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_ACTO_EJECUTORIAR", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            NotificacionEntity _objActo;
            DependenciaEntidadEntity _objDependencia = new DependenciaEntidadEntity();

            TipoIdentificacionDalc _tipoIDDalc = new TipoIdentificacionDalc();
            TipoIdentificacionEntity _tipoID = new TipoIdentificacionEntity();
            List<NotificacionEntity> listaActos = new List<NotificacionEntity>();
            TipoDocumentoIdentity _tipoActo = new TipoDocumentoIdentity();
            TipoDocumentoDalc _tipoActoDalc = new TipoDocumentoDalc();
            EstadoNotificacionDalc _objEstadoDalc = new EstadoNotificacionDalc();
            PersonaNotificarDalc _personaDalc = new PersonaNotificarDalc();
            List<PersonaNotificarEntity> listaPersonas;
            respuesta = "";
            Permite = false;
            try
            {
                if (dsResultado.Tables[0].Rows.Count > 0)
                {

                    DataRow dt = dsResultado.Tables[0].Rows[0];
                    
                    _tipoActo = new TipoDocumentoIdentity();
                    _tipoID = new TipoIdentificacionEntity();
                    _objDependencia = new DependenciaEntidadEntity();
                    _objActo = new NotificacionEntity();
                    listaPersonas = new List<PersonaNotificarEntity>();

                    _objActo.IdActoNotificacion = Convert.ToDecimal(dt["ID_ACTO_NOTIFICACION"]);
                    _objActo.NumeroActoAdministrativo = Convert.ToString(dt["NOT_NUMERO_ACTO_ADMINISTRATIVO"]);
                    _objActo.ParteResolutiva = Convert.ToString(dt["NOT_PARTE_RESOLUTIVA"]);
                    _objActo.CodigoEntidadPublica = Convert.ToInt32(dt["NOT_CODIGO_ENTIDAD_PUBLICA"]);
                    _objActo.ProcesoAdministracion = Convert.ToString(dt["NOT_PROCESO_ADMINISTRACION"]);

                    DependenciaEntidadDalc depDalc = new DependenciaEntidadDalc();
                    _objActo.DependenciaEntidad = depDalc.ObtenerDependencia(Convert.ToInt32(dt["NOT_ID_DEPENDENCIA_ENTIDAD"]));

                    _objDependencia.ID = Convert.ToString(dt["NOT_ID_DEPENDENCIA_ENTIDAD"]);
                    _objActo.DependenciaEntidad = _objDependencia;
                    _objActo.SistemaEntidadPublica = Convert.ToInt32(dt["NOT_SISTEMA_ENTIDAD_PUBLICA"]);
                    _tipoID.Id = Convert.ToInt32(dt["NOT_TIPO_ID_FUNCIONARIO"]);
                    _tipoIDDalc.ObtenerTipoIdentificacion(ref _tipoID);
                    _objActo.TipoIdentificacionFuncionario = _tipoID;
                    _objActo.IdentificacionFuncionario = Convert.ToString(dt["NOT_ID_FUNCIONARIO_NOTIFICANTE"]);
                    _objActo.IdPlantilla = Convert.ToString(dt["NOT_ID_PLANTILLA"]);
                    if (dt["NOT_NOMBRE_PLANTILLA"] != DBNull.Value)
                        _objActo.NombrePlantilla = Convert.ToString(dt["NOT_NOMBRE_PLANTILLA"]);
                    if (dt["NOT_NUMERO_ACTO_ADMINISTRATIVO_ASOCIADO"] != DBNull.Value)
                        _objActo.NumeroActoAdministrativoAsociado = Convert.ToString(dt["NOT_NUMERO_ACTO_ADMINISTRATIVO_ASOCIADO"]);
                    if (dt["NOT_REQUIERE_PUBLICACION"] != DBNull.Value)
                        _objActo.RequierePublicacion = Convert.ToBoolean(dt["NOT_REQUIERE_PUBLICACION"]);
                    if (dt["NOT_FECHA_ACTO"] != DBNull.Value)
                        _objActo.FechaActo = Convert.ToDateTime(dt["NOT_FECHA_ACTO"]);
                    if (dt["NOT_RUTA_DOCUMENTO"] != DBNull.Value)
                        _objActo.RutaDocumento = dt["NOT_RUTA_DOCUMENTO"].ToString();
                    if (dt["NOT_ID_TIPO_ACTO"] != DBNull.Value)
                    {
                        _tipoActo = new TipoDocumentoIdentity();
                        _tipoActo = _tipoActoDalc.ObtenerTipoDocumento(Convert.ToInt32(dt["NOT_ID_TIPO_ACTO"]), null);
                        _objActo.IdTipoActo = _tipoActo;
                    }
                    _objActo.NumeroSILPA = Convert.ToString(dt["NOT_NUMERO_SILPA"]);
                    _objActo.NombreProyecto = Convert.ToString(dt["NOMBRE_PROYECTO"]);
                    listaPersonas = _personaDalc.ObtenerPersonas(new object[] { null, _objActo.IdActoNotificacion, null, null, null, null });
                    if (dt["NOT_FECHA_ACTO"] != DBNull.Value)
                    {
                        _objActo.FechaActo = Convert.ToDateTime(dt["NOT_FECHA_ACTO"]);
                    }

                    if (listaPersonas != null)
                    {
                        if (listaPersonas.Count > 0)
                            _objActo.ListaPersonas = listaPersonas;

                        listaActos.Add(_objActo);
                    }
                    if (dsResultado.Tables[1].Rows.Count > 0)
                    {
                        respuesta = dsResultado.Tables[1].Rows[0]["RESPUESTA"].ToString();
                        if (dsResultado.Tables[1].Rows[0]["PERMITE"].ToString() == "S")
                            Permite = true;
                        else
                            Permite = false;

                    }

                    return _objActo;
                }               

                return null;
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Consultar Ejecutoria Acto.";
                throw new Exception(strException, ex);
            }
            finally
            {
                dsResultado.Dispose();
                dsResultado = null;
                db = null;
            }
        }


        /// <summary>
        /// hava: 18-nov-10
        /// </summary>
        /// <param name="estado"></param>
        /// <param name="idActoNot"></param>
        /// <returns></returns>
        public List<NotificacionEntity> ObtenerActosParaConsultarPDIPorActo(EstadoNotificacionEntity estado, long idActoNot)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { estado.ID, idActoNot };
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_ACTOS_ESTADO_POR_ID_ACTO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            NotificacionEntity _objActo;
            DependenciaEntidadEntity _objDependencia = new DependenciaEntidadEntity();

            TipoIdentificacionDalc _tipoIDDalc = new TipoIdentificacionDalc();
            TipoIdentificacionEntity _tipoID = new TipoIdentificacionEntity();
            List<NotificacionEntity> listaActos = new List<NotificacionEntity>();
            TipoDocumentoIdentity _tipoActo = new TipoDocumentoIdentity();
            TipoDocumentoDalc _tipoActoDalc = new TipoDocumentoDalc();
            EstadoNotificacionDalc _objEstadoDalc = new EstadoNotificacionDalc();
            if (dsResultado.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dt in dsResultado.Tables[0].Rows)
                {
                    _tipoActo = new TipoDocumentoIdentity();
                    _tipoID = new TipoIdentificacionEntity();
                    _objDependencia = new DependenciaEntidadEntity();
                    _objActo = new NotificacionEntity();

                    _objActo.IdActoNotificacion = Convert.ToDecimal(dt["ID_ACTO_NOTIFICACION"]);
                    _objActo.NumeroActoAdministrativo = Convert.ToString(dt["NOT_NUMERO_ACTO_ADMINISTRATIVO"]);
                    _objActo.ParteResolutiva = Convert.ToString(dt["NOT_PARTE_RESOLUTIVA"]);
                    _objActo.CodigoEntidadPublica = Convert.ToInt32(dt["NOT_CODIGO_ENTIDAD_PUBLICA"]);
                    _objActo.ProcesoAdministracion = Convert.ToString(dt["NOT_PROCESO_ADMINISTRACION"]);

                    DependenciaEntidadDalc depDalc = new DependenciaEntidadDalc();
                    _objActo.DependenciaEntidad = depDalc.ObtenerDependencia(Convert.ToInt32(dt["NOT_ID_DEPENDENCIA_ENTIDAD"]));

                    _objDependencia.ID = Convert.ToString(dt["NOT_ID_DEPENDENCIA_ENTIDAD"]);
                    _objActo.DependenciaEntidad = _objDependencia;
                    _objActo.SistemaEntidadPublica = Convert.ToInt32(dt["NOT_SISTEMA_ENTIDAD_PUBLICA"]);
                    _tipoID.Id = Convert.ToInt32(dt["NOT_TIPO_ID_FUNCIONARIO"]);
                    _tipoIDDalc.ObtenerTipoIdentificacion(ref _tipoID);
                    _objActo.TipoIdentificacionFuncionario = _tipoID;
                    _objActo.IdentificacionFuncionario = Convert.ToString(dt["NOT_ID_FUNCIONARIO_NOTIFICANTE"]);
                    _objActo.IdPlantilla = Convert.ToString(dt["NOT_ID_PLANTILLA"]);
                    if (dt["NOT_NOMBRE_PLANTILLA"] != DBNull.Value)
                        _objActo.NombrePlantilla = Convert.ToString(dt["NOT_NOMBRE_PLANTILLA"]);
                    if (dt["NOT_NUMERO_ACTO_ADMINISTRATIVO_ASOCIADO"] != DBNull.Value)
                        _objActo.NumeroActoAdministrativoAsociado = Convert.ToString(dt["NOT_NUMERO_ACTO_ADMINISTRATIVO_ASOCIADO"]);
                    if (dt["NOT_REQUIERE_PUBLICACION"] != DBNull.Value)
                        _objActo.RequierePublicacion = Convert.ToBoolean(dt["NOT_REQUIERE_PUBLICACION"]);
                    //if (dt["NOT_FECHA_ACTO"] != DBNull.Value)
                    //    _objActo.FechaActo = Convert.ToDateTime(dt["NOT_FECHA_ACTO"]);
                    if (dt["NOT_ID_TIPO_ACTO"] != DBNull.Value)
                    {
                        _tipoActo = new TipoDocumentoIdentity();
                        _tipoActo = _tipoActoDalc.ObtenerTipoDocumento(Convert.ToInt32(dt["NOT_ID_TIPO_ACTO"]), null);
                        _objActo.IdTipoActo = _tipoActo;
                    }
                    _objActo.NumeroSILPA = Convert.ToString(dt["NOT_NUMERO_SILPA"]);

                    listaActos.Add(_objActo);
                }
            }

            return listaActos;
        }

        public EstadoPersonaActoEntity ObtenerEstadoPersonaNotificacion(decimal idNotActo, int idEstado)
        {
            EstadoPersonaActoEntity estadoPersona = new EstadoPersonaActoEntity();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { idNotActo, idEstado };
            DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTA_ACTO_POR_ESTADO", parametros);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    estadoPersona.IdActo = Convert.ToDecimal(reader["ID_NOT_ACTO"]);
                    estadoPersona.IdEstado = Convert.ToInt32(reader["ID_ESTADO"]);
                    estadoPersona.IdPersona = Convert.ToDecimal(reader["ID_PERSONA"]);
                    estadoPersona.FechaEstado = Convert.ToDateTime(reader["FECHA_ESTADO"]);
                    estadoPersona.RutaDocumentos = reader["RUTA_DOCUMENTO"].ToString();
                    estadoPersona.ID = Convert.ToInt32(reader["ID"]);
                    estadoPersona.SistemaPDI = Convert.ToBoolean(reader["SISTEMA_PDI"]);
                    estadoPersona.EstadoActual = Convert.ToInt32(reader["ESTADO_ACTUAL"]);
                    estadoPersona.EnviaCorreo = Convert.ToInt32(reader["ENVIA_CORREO"]);
                }  
            }
            return estadoPersona;
            
        }


        /// <summary>
        /// Obtiene la información del actom que se encuentre en estado notificado
        /// </summary>
        /// <param name="idNotActo">int con el id del acto</param>
        /// <returns>EstadoPersonaActoEntity con la información del acto</returns>
        public EstadoPersonaActoEntity ObtenerEstadoPersonaNotificado(decimal idNotActo)
        {
            EstadoPersonaActoEntity estadoPersona = new EstadoPersonaActoEntity();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { idNotActo };
            DbCommand cmd = db.GetStoredProcCommand("SP_CONSULTA_ACTO_POR_ESTADOS_NOTIFICADOS", parametros);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    estadoPersona.IdActo = Convert.ToDecimal(reader["ID_NOT_ACTO"]);
                    estadoPersona.IdEstado = Convert.ToInt32(reader["ID_ESTADO"]);
                    estadoPersona.IdPersona = Convert.ToDecimal(reader["ID_PERSONA"]);
                    estadoPersona.FechaEstado = Convert.ToDateTime(reader["FECHA_ESTADO"]);
                    estadoPersona.RutaDocumentos = reader["RUTA_DOCUMENTO"].ToString();
                    estadoPersona.ID = Convert.ToInt32(reader["ID"]);
                    estadoPersona.SistemaPDI = Convert.ToBoolean(reader["SISTEMA_PDI"]);
                    estadoPersona.EstadoActual = Convert.ToInt32(reader["ESTADO_ACTUAL"]);
                    estadoPersona.EnviaCorreo = Convert.ToInt32(reader["ENVIA_CORREO"]);
                }
            }
            return estadoPersona;

        }


        /// <summary>
        /// hava: 18-nov-10
        /// Obtiene el acto para una persona
        /// </summary>
        /// <param name="estado"></param>
        /// <param name="idActoNot"></param>
        /// <returns></returns>
        public List<NotificacionEntity> ObtenerActosParaConsultarPDIPorActoPorPersona(EstadoNotificacionEntity estado, long idActoNot, long idPersona)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { estado.ID, idActoNot, idPersona };
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_ACTOS_ESTADO_POR_ID_ACTO_POR_PERSONA", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            NotificacionEntity _objActo;
            DependenciaEntidadEntity _objDependencia = new DependenciaEntidadEntity();

            TipoIdentificacionDalc _tipoIDDalc = new TipoIdentificacionDalc();
            TipoIdentificacionEntity _tipoID = new TipoIdentificacionEntity();
            List<NotificacionEntity> listaActos = new List<NotificacionEntity>();
            TipoDocumentoIdentity _tipoActo = new TipoDocumentoIdentity();
            TipoDocumentoDalc _tipoActoDalc = new TipoDocumentoDalc();
            EstadoNotificacionDalc _objEstadoDalc = new EstadoNotificacionDalc();
            if (dsResultado.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dt in dsResultado.Tables[0].Rows)
                {
                    _tipoActo = new TipoDocumentoIdentity();
                    _tipoID = new TipoIdentificacionEntity();
                    _objDependencia = new DependenciaEntidadEntity();
                    _objActo = new NotificacionEntity();

                    _objActo.IdActoNotificacion = Convert.ToDecimal(dt["ID_ACTO_NOTIFICACION"]);
                    _objActo.NumeroActoAdministrativo = Convert.ToString(dt["NOT_NUMERO_ACTO_ADMINISTRATIVO"]);
                    _objActo.ParteResolutiva = Convert.ToString(dt["NOT_PARTE_RESOLUTIVA"]);
                    _objActo.CodigoEntidadPublica = Convert.ToInt32(dt["NOT_CODIGO_ENTIDAD_PUBLICA"]);
                    _objActo.ProcesoAdministracion = Convert.ToString(dt["NOT_PROCESO_ADMINISTRACION"]);

                    DependenciaEntidadDalc depDalc = new DependenciaEntidadDalc();
                    _objActo.DependenciaEntidad = depDalc.ObtenerDependencia(Convert.ToInt32(dt["NOT_ID_DEPENDENCIA_ENTIDAD"]));

                    _objDependencia.ID = Convert.ToString(dt["NOT_ID_DEPENDENCIA_ENTIDAD"]);
                    _objActo.DependenciaEntidad = _objDependencia;
                    _objActo.SistemaEntidadPublica = Convert.ToInt32(dt["NOT_SISTEMA_ENTIDAD_PUBLICA"]);
                    _tipoID.Id = Convert.ToInt32(dt["NOT_TIPO_ID_FUNCIONARIO"]);
                    _tipoIDDalc.ObtenerTipoIdentificacion(ref _tipoID);
                    _objActo.TipoIdentificacionFuncionario = _tipoID;
                    _objActo.IdentificacionFuncionario = Convert.ToString(dt["NOT_ID_FUNCIONARIO_NOTIFICANTE"]);
                    _objActo.IdPlantilla = Convert.ToString(dt["NOT_ID_PLANTILLA"]);
                    if (dt["NOT_NOMBRE_PLANTILLA"] != DBNull.Value)
                        _objActo.NombrePlantilla = Convert.ToString(dt["NOT_NOMBRE_PLANTILLA"]);
                    if (dt["NOT_NUMERO_ACTO_ADMINISTRATIVO_ASOCIADO"] != DBNull.Value)
                        _objActo.NumeroActoAdministrativoAsociado = Convert.ToString(dt["NOT_NUMERO_ACTO_ADMINISTRATIVO_ASOCIADO"]);
                    if (dt["NOT_REQUIERE_PUBLICACION"] != DBNull.Value)
                        _objActo.RequierePublicacion = Convert.ToBoolean(dt["NOT_REQUIERE_PUBLICACION"]);
                    //if (dt["NOT_FECHA_ACTO"] != DBNull.Value)
                    //    _objActo.FechaActo = Convert.ToDateTime(dt["NOT_FECHA_ACTO"]);
                    if (dt["NOT_ID_TIPO_ACTO"] != DBNull.Value)
                    {
                        _tipoActo = new TipoDocumentoIdentity();
                        _tipoActo = _tipoActoDalc.ObtenerTipoDocumento(Convert.ToInt32(dt["NOT_ID_TIPO_ACTO"]), null);
                        _objActo.IdTipoActo = _tipoActo;
                    }
                    _objActo.NumeroSILPA = Convert.ToString(dt["NOT_NUMERO_SILPA"]);

                    listaActos.Add(_objActo);
                }
            }

            return listaActos;
        }

        
        public List<NotificacionEntity> ObtenerActoPorAA(string numeroActo, string numeroExpediente)
        {
            List<NotificacionEntity> listaActos = new List<NotificacionEntity>();
            try
            {
                SMLog.Escribir(Severidad.Informativo, "***Inicio de ObtenerActoPorAA");
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                SMLog.Escribir(Severidad.Informativo, "***numeroActo: " + numeroActo + " numeroExpediente: " + numeroExpediente);
                object[] parametros = new object[] { null, null, null, numeroActo, numeroExpediente, null, null, null, null, null, null };
                DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_ACTO", parametros);
                SMLog.Escribir(Severidad.Informativo, "***Se ejecutó NOT_LISTAR_ACTO");
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                NotificacionEntity _objActo;
                DependenciaEntidadEntity _objDependencia = new DependenciaEntidadEntity();
                TipoIdentificacionDalc _tipoIDDalc = new TipoIdentificacionDalc();
                TipoIdentificacionEntity _tipoID = new TipoIdentificacionEntity();
                TipoDocumentoIdentity _tipoActo = new TipoDocumentoIdentity();
                TipoDocumentoDalc _tipoActoDalc = new TipoDocumentoDalc();
                EstadoNotificacionDalc _objEstadoDalc = new EstadoNotificacionDalc();
                PersonaNotificarDalc _objPersonaDalc = new PersonaNotificarDalc();
                PersonaNotificarEntity _objPersonaEntity = new PersonaNotificarEntity();
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    SMLog.Escribir(Severidad.Informativo, "***SI hay datos en dsResultado.Tables[0]");
                    foreach (DataRow dt in dsResultado.Tables[0].Rows)
                    {
                        _tipoActo = new TipoDocumentoIdentity();
                        _tipoID = new TipoIdentificacionEntity();
                        _objDependencia = new DependenciaEntidadEntity();
                        _objActo = new NotificacionEntity();

                        SMLog.Escribir(Severidad.Informativo, "***dsResultado.Tables[0] = " + _objActo.IdActoNotificacion.ToString());

                        _objActo.IdActoNotificacion = Convert.ToDecimal(dt["ID_ACTO_NOTIFICACION"]);
                        _objActo.NumeroActoAdministrativo = Convert.ToString(dt["NOT_NUMERO_ACTO_ADMINISTRATIVO"]);
                        _objActo.ParteResolutiva = Convert.ToString(dt["NOT_PARTE_RESOLUTIVA"]);
                        _objActo.CodigoEntidadPublica = Convert.ToInt32(dt["NOT_CODIGO_ENTIDAD_PUBLICA"]);
                        _objActo.ProcesoAdministracion = Convert.ToString(dt["NOT_PROCESO_ADMINISTRACION"]);

                        DependenciaEntidadDalc depDalc = new DependenciaEntidadDalc();
                        _objActo.DependenciaEntidad = depDalc.ObtenerDependencia(Convert.ToInt32(dt["NOT_ID_DEPENDENCIA_ENTIDAD"]));

                        _objDependencia.ID = Convert.ToString(dt["NOT_ID_DEPENDENCIA_ENTIDAD"]);
                        _objActo.DependenciaEntidad = _objDependencia;
                        _objActo.SistemaEntidadPublica = Convert.ToInt32(dt["NOT_SISTEMA_ENTIDAD_PUBLICA"]);
                        _tipoID.Id = Convert.ToInt32(dt["NOT_TIPO_ID_FUNCIONARIO"]);
                        _tipoIDDalc.ObtenerTipoIdentificacion(ref _tipoID);
                        _objActo.TipoIdentificacionFuncionario = _tipoID;
                        _objActo.IdentificacionFuncionario = Convert.ToString(dt["NOT_ID_FUNCIONARIO_NOTIFICANTE"]);
                        _objActo.IdPlantilla = Convert.ToString(dt["NOT_ID_PLANTILLA"]);
                        if (dt["NOT_NOMBRE_PLANTILLA"] != DBNull.Value)
                            _objActo.NombrePlantilla = Convert.ToString(dt["NOT_NOMBRE_PLANTILLA"]);
                        if (dt["NOT_NUMERO_ACTO_ADMINISTRATIVO_ASOCIADO"] != DBNull.Value)
                            _objActo.NumeroActoAdministrativoAsociado = Convert.ToString(dt["NOT_NUMERO_ACTO_ADMINISTRATIVO_ASOCIADO"]);
                        if (dt["NOT_REQUIERE_PUBLICACION"] != DBNull.Value)
                            _objActo.RequierePublicacion = Convert.ToBoolean(dt["NOT_REQUIERE_PUBLICACION"]);
                        //if (dt["NOT_FECHA_ACTO"] != DBNull.Value)
                        //    _objActo.FechaActo = Convert.ToDateTime(dt["NOT_FECHA_ACTO"]);
                        if (dt["NOT_ID_TIPO_ACTO"] != DBNull.Value)
                        {
                            _tipoActo = new TipoDocumentoIdentity();
                            _tipoActo = _tipoActoDalc.ObtenerTipoDocumento(Convert.ToInt32(dt["NOT_ID_TIPO_ACTO"]), null);

                            _objActo.IdTipoActo = _tipoActo;
                        }
                        _objActo.NumeroSILPA = Convert.ToString(dt["NOT_NUMERO_SILPA"]);

                        SMLog.Escribir(Severidad.Informativo, "***Consultar personas: _objActo.IdActoNotificacion" + _objActo.IdActoNotificacion.ToString());
                        _objActo.ListaPersonas = _objPersonaDalc.ObtenerPersonas(new object[] { null, _objActo.IdActoNotificacion, null, null, null, null });
                        listaActos.Add(_objActo);
                    }
                }
                return listaActos;
            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Informativo, "***Error en ObtenerActoPorAA " + ex.ToString());
                return listaActos;
            }
        }


        /// <summary>
        /// hava: 05-ene-2011
        /// Lista los actos con sus estados de notificación
        /// </summary>
        /// <param name="numeroActo">NOT_NUMERO_ACTO_ADMINISTRATIVO</param>
        /// <param name="numeroExpediente">NOT_PROCESO_ADMINISTRACION</param>
        /// <returns></returns>
        public List<NotificacionEntity> ObtenerActoPorAA_VerificarEstado(string numeroActo, string numeroExpediente,string tipoDocumento, string idAA)
        {
            List<NotificacionEntity> listaActos = new List<NotificacionEntity>();
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { numeroActo, numeroExpediente, tipoDocumento, idAA };
                DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_ACTO_VERIFICAR_ESTADO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                NotificacionEntity _objActo;
                DependenciaEntidadEntity _objDependencia = new DependenciaEntidadEntity();
                TipoIdentificacionDalc _tipoIDDalc = new TipoIdentificacionDalc();
                TipoIdentificacionEntity _tipoID = new TipoIdentificacionEntity();
                TipoDocumentoIdentity _tipoActo = new TipoDocumentoIdentity();
                TipoDocumentoDalc _tipoActoDalc = new TipoDocumentoDalc();
                EstadoNotificacionDalc _objEstadoDalc = new EstadoNotificacionDalc();
                PersonaNotificarDalc _objPersonaDalc = new PersonaNotificarDalc();
                PersonaNotificarEntity _objPersonaEntity = new PersonaNotificarEntity();
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dt in dsResultado.Tables[0].Rows)
                    {
                        _tipoActo = new TipoDocumentoIdentity();
                        _tipoID = new TipoIdentificacionEntity();
                        _objDependencia = new DependenciaEntidadEntity();
                        _objActo = new NotificacionEntity();

                        _objActo.IdActoNotificacion = Convert.ToDecimal(dt["ID_ACTO_NOTIFICACION"]);
                        _objActo.NumeroActoAdministrativo = Convert.ToString(dt["NOT_NUMERO_ACTO_ADMINISTRATIVO"]);
                        _objActo.ParteResolutiva = Convert.ToString(dt["NOT_PARTE_RESOLUTIVA"]);
                        _objActo.CodigoEntidadPublica = Convert.ToInt32(dt["NOT_CODIGO_ENTIDAD_PUBLICA"]);
                        _objActo.ProcesoAdministracion = Convert.ToString(dt["NOT_PROCESO_ADMINISTRACION"]);

                        DependenciaEntidadDalc depDalc = new DependenciaEntidadDalc();
                        _objActo.DependenciaEntidad = depDalc.ObtenerDependencia(Convert.ToInt32(dt["NOT_ID_DEPENDENCIA_ENTIDAD"]));

                        _objDependencia.ID = Convert.ToString(dt["NOT_ID_DEPENDENCIA_ENTIDAD"]);
                        _objActo.DependenciaEntidad = _objDependencia;
                        _objActo.SistemaEntidadPublica = Convert.ToInt32(dt["NOT_SISTEMA_ENTIDAD_PUBLICA"]);
                        _tipoID.Id = Convert.ToInt32(dt["NOT_TIPO_ID_FUNCIONARIO"]);
                        _tipoIDDalc.ObtenerTipoIdentificacion(ref _tipoID);
                        _objActo.TipoIdentificacionFuncionario = _tipoID;
                        _objActo.IdentificacionFuncionario = Convert.ToString(dt["NOT_ID_FUNCIONARIO_NOTIFICANTE"]);
                        _objActo.IdPlantilla = Convert.ToString(dt["NOT_ID_PLANTILLA"]);
                        if (dt["NOT_NOMBRE_PLANTILLA"] != DBNull.Value)
                            _objActo.NombrePlantilla = Convert.ToString(dt["NOT_NOMBRE_PLANTILLA"]);
                        if (dt["NOT_NUMERO_ACTO_ADMINISTRATIVO_ASOCIADO"] != DBNull.Value)
                            _objActo.NumeroActoAdministrativoAsociado = Convert.ToString(dt["NOT_NUMERO_ACTO_ADMINISTRATIVO_ASOCIADO"]);
                        if (dt["NOT_REQUIERE_PUBLICACION"] != DBNull.Value)
                            _objActo.RequierePublicacion = Convert.ToBoolean(dt["NOT_REQUIERE_PUBLICACION"]);
                        if (dt["NOT_ID_TIPO_ACTO"] != DBNull.Value)
                        {
                            _tipoActo = new TipoDocumentoIdentity();
                            _tipoActo = _tipoActoDalc.ObtenerTipoDocumento(Convert.ToInt32(dt["NOT_ID_TIPO_ACTO"]), null);

                            _objActo.IdTipoActo = _tipoActo;
                        }
                        _objActo.NumeroSILPA = Convert.ToString(dt["NOT_NUMERO_SILPA"]);

                        _objActo.ListaPersonas = _objPersonaDalc.ObtenerPersonas(new object[] { null, _objActo.IdActoNotificacion, null, null, null, null });
                   
                        listaActos.Add(_objActo);
                    }
                }
                return listaActos;
            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Informativo, "***Error en ObtenerActoPorAA_verificar_estado " + ex.ToString());
                return listaActos;
            }
        }

        
        public DataTable ObtenerActosPorEstado( int estadoId, string numeroSilpa, int idUsuario ){

            SqlDatabase db = new SqlDatabase( objConfiguracion.SilpaCnx.ToString( ) );
            DbCommand cmd = db.GetStoredProcCommand( "EST_LISTAR_DOCUMENTOS_SOLICITUD", new object[] { estadoId, numeroSilpa, idUsuario } );

            return db.ExecuteDataSet( cmd ).Tables[ 0 ];
        }

        public DataTable ObtenerDocumentosEvaluacion(int estadoId, string numeroSilpa, int idUsuario)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("EST_LISTAR_DOCUMENTOS_EVALUACION", new object[] { estadoId, numeroSilpa, idUsuario });
            DataTable dtExpedientes = new DataTable();
            DataSet ds = new DataSet();
            ds = db.ExecuteDataSet(cmd);
            if (ds.Tables.Count > 0)
                dtExpedientes = ds.Tables[0];
            return dtExpedientes;
        }

        public DataTable ObtenerCertificados(string NumeroVITAL, string NumeroExpediente, string NumeroCertificado, int? SolicitanteID, int? año)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("EST_LISTAR_CERTIFICADOS_EVALUACION", new object[] { NumeroVITAL, NumeroExpediente, NumeroCertificado, SolicitanteID, año });

            return db.ExecuteDataSet(cmd).Tables[0];
        }

        /// <summary>
        /// obtiene los actos administrativos de acuerdo a los parámetros entregados
        /// </summary>
        /// <param name="parametros">objeto con los párametros</param>
        /// <returns>Lista con los Actos</returns>
        public List<NotificacionEntity> ObtenerActos(object[] parametros)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_ACTO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            NotificacionEntity _objActo;
            DependenciaEntidadEntity _objDependencia = new DependenciaEntidadEntity();

            TipoIdentificacionDalc _tipoIDDalc = new TipoIdentificacionDalc();
            TipoIdentificacionEntity _tipoID = new TipoIdentificacionEntity();
            List<NotificacionEntity> listaActos = new List<NotificacionEntity>();
            TipoDocumentoIdentity _tipoActo = new TipoDocumentoIdentity();
            TipoDocumentoDalc _tipoActoDalc = new TipoDocumentoDalc();
            EstadoNotificacionDalc _objEstadoDalc = new EstadoNotificacionDalc();
            PersonaNotificarDalc _personaDalc = new PersonaNotificarDalc();
            List<PersonaNotificarEntity> listaPersonas;
            try
            {
                if (dsResultado.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dt in dsResultado.Tables[0].Rows)
                    {
                        _tipoActo = new TipoDocumentoIdentity();
                        _tipoID = new TipoIdentificacionEntity();
                        _objDependencia = new DependenciaEntidadEntity();
                        _objActo = new NotificacionEntity();
                        listaPersonas = new List<PersonaNotificarEntity>();

                        _objActo.IdActoNotificacion = Convert.ToDecimal(dt["ID_ACTO_NOTIFICACION"]);
                        _objActo.NumeroActoAdministrativo = Convert.ToString(dt["NOT_NUMERO_ACTO_ADMINISTRATIVO"]);
                        _objActo.ParteResolutiva = Convert.ToString(dt["NOT_PARTE_RESOLUTIVA"]);
                        _objActo.CodigoEntidadPublica = Convert.ToInt32(dt["NOT_CODIGO_ENTIDAD_PUBLICA"]);
                        _objActo.ProcesoAdministracion = Convert.ToString(dt["NOT_PROCESO_ADMINISTRACION"]);

                        DependenciaEntidadDalc depDalc = new DependenciaEntidadDalc();
                        _objActo.DependenciaEntidad = depDalc.ObtenerDependencia(Convert.ToInt32(dt["NOT_ID_DEPENDENCIA_ENTIDAD"]));

                        _objDependencia.ID = Convert.ToString(dt["NOT_ID_DEPENDENCIA_ENTIDAD"]);
                        _objActo.DependenciaEntidad = _objDependencia;
                        _objActo.SistemaEntidadPublica = Convert.ToInt32(dt["NOT_SISTEMA_ENTIDAD_PUBLICA"]);
                        _tipoID.Id = Convert.ToInt32(dt["ID_TIPO_IDENTIFICACION"]);
                        _tipoIDDalc.ObtenerTipoIdentificacion(ref _tipoID);
                        _objActo.TipoIdentificacionFuncionario = _tipoID;
                        _objActo.IdentificacionFuncionario = Convert.ToString(dt["NOT_ID_FUNCIONARIO_NOTIFICANTE"]);
                        _objActo.IdPlantilla = Convert.ToString(dt["NOT_ID_PLANTILLA"]);
                        if (dt["NOT_NOMBRE_PLANTILLA"] != DBNull.Value)
                            _objActo.NombrePlantilla = Convert.ToString(dt["NOT_NOMBRE_PLANTILLA"]);
                        if (dt["NOT_NUMERO_ACTO_ADMINISTRATIVO_ASOCIADO"] != DBNull.Value)
                            _objActo.NumeroActoAdministrativoAsociado = Convert.ToString(dt["NOT_NUMERO_ACTO_ADMINISTRATIVO_ASOCIADO"]);
                        if (dt["NOT_REQUIERE_PUBLICACION"] != DBNull.Value)
                            _objActo.RequierePublicacion = Convert.ToBoolean(dt["NOT_REQUIERE_PUBLICACION"]);
                        if (dt["NOT_FECHA_ACTO"] != DBNull.Value)
                            _objActo.FechaActo = Convert.ToDateTime(dt["NOT_FECHA_ACTO"]);
                        if (dt["NOT_RUTA_DOCUMENTO"] != DBNull.Value)
                            _objActo.RutaDocumento = dt["NOT_RUTA_DOCUMENTO"].ToString();
                        if (dt["NOT_ID_TIPO_ACTO"] != DBNull.Value)
                        {
                            _tipoActo = new TipoDocumentoIdentity();
                            _tipoActo = _tipoActoDalc.ObtenerTipoDocumento(Convert.ToInt32(dt["NOT_ID_TIPO_ACTO"]), null);
                            _objActo.IdTipoActo = _tipoActo;
                        }
                        _objActo.NumeroSILPA = Convert.ToString(dt["NOT_NUMERO_SILPA"]);
                        _objActo.NumeroSILPAAsociado = Convert.ToString(dt["NOT_NUMERO_SILPA_ASOCIADO"]);
                        _objActo.IdentificacionUsuario = Convert.ToString(dt["NPE_NUMERO_IDENTIFICACION"]);
                        _objActo.NombreProyecto = Convert.ToString(dt["NOMBRE_PROYECTO"]);
                        listaPersonas = _personaDalc.ObtenerPersonas(new object[] { null, _objActo.IdActoNotificacion, null, null, null, null });
                        if (dt["NOT_FECHA_ACTO"] != DBNull.Value)
                        {
                            _objActo.FechaActo = Convert.ToDateTime(dt["NOT_FECHA_ACTO"]);
                        }

                        if (listaPersonas != null)
                        {
                            if (listaPersonas.Count > 0)
                                _objActo.ListaPersonas = listaPersonas;

                            listaActos.Add(_objActo);
                        }
                    }
                }
                return listaActos;
            }
            finally
            {
                dsResultado.Dispose();
                dsResultado = null;
                db = null;
            }
        }


        public List<NotificacionEntity> ObtenerActosRecurso(object[] parametros)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_ACTO_RECURSO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            NotificacionEntity _objActo;
            DependenciaEntidadEntity _objDependencia = new DependenciaEntidadEntity();

            TipoIdentificacionDalc _tipoIDDalc = new TipoIdentificacionDalc();
            TipoIdentificacionEntity _tipoID = new TipoIdentificacionEntity();
            List<NotificacionEntity> listaActos = new List<NotificacionEntity>();
            TipoDocumentoIdentity _tipoActo = new TipoDocumentoIdentity();
            TipoDocumentoDalc _tipoActoDalc = new TipoDocumentoDalc();
            EstadoNotificacionDalc _objEstadoDalc = new EstadoNotificacionDalc();
            PersonaNotificarDalc _personaDalc = new PersonaNotificarDalc();
            List<PersonaNotificarEntity> listaPersonas;
            try
            {
                if (dsResultado.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dt in dsResultado.Tables[0].Rows)
                    {
                        _tipoActo = new TipoDocumentoIdentity();
                        _tipoID = new TipoIdentificacionEntity();
                        _objDependencia = new DependenciaEntidadEntity();
                        _objActo = new NotificacionEntity();
                        listaPersonas = new List<PersonaNotificarEntity>();

                        _objActo.IdActoNotificacion = Convert.ToDecimal(dt["ID_ACTO_NOTIFICACION"]);
                        _objActo.NumeroActoAdministrativo = Convert.ToString(dt["NOT_NUMERO_ACTO_ADMINISTRATIVO"]);
                        _objActo.ParteResolutiva = Convert.ToString(dt["NOT_PARTE_RESOLUTIVA"]);
                        _objActo.CodigoEntidadPublica = Convert.ToInt32(dt["NOT_CODIGO_ENTIDAD_PUBLICA"]);
                        _objActo.ProcesoAdministracion = Convert.ToString(dt["NOT_PROCESO_ADMINISTRACION"]);

                        DependenciaEntidadDalc depDalc = new DependenciaEntidadDalc();
                        _objActo.DependenciaEntidad = depDalc.ObtenerDependencia(Convert.ToInt32(dt["NOT_ID_DEPENDENCIA_ENTIDAD"]));

                        _objDependencia.ID = Convert.ToString(dt["NOT_ID_DEPENDENCIA_ENTIDAD"]);
                        _objActo.DependenciaEntidad = _objDependencia;
                        _objActo.SistemaEntidadPublica = Convert.ToInt32(dt["NOT_SISTEMA_ENTIDAD_PUBLICA"]);
                        _tipoID.Id = Convert.ToInt32(dt["NOT_TIPO_ID_FUNCIONARIO"]);
                        _tipoIDDalc.ObtenerTipoIdentificacion(ref _tipoID);
                        _objActo.TipoIdentificacionFuncionario = _tipoID;
                        _objActo.IdentificacionFuncionario = Convert.ToString(dt["NOT_ID_FUNCIONARIO_NOTIFICANTE"]);
                        _objActo.IdPlantilla = Convert.ToString(dt["NOT_ID_PLANTILLA"]);
                        if (dt["NOT_NOMBRE_PLANTILLA"] != DBNull.Value)
                            _objActo.NombrePlantilla = Convert.ToString(dt["NOT_NOMBRE_PLANTILLA"]);
                        if (dt["NOT_NUMERO_ACTO_ADMINISTRATIVO_ASOCIADO"] != DBNull.Value)
                            _objActo.NumeroActoAdministrativoAsociado = Convert.ToString(dt["NOT_NUMERO_ACTO_ADMINISTRATIVO_ASOCIADO"]);
                        if (dt["NOT_REQUIERE_PUBLICACION"] != DBNull.Value)
                            _objActo.RequierePublicacion = Convert.ToBoolean(dt["NOT_REQUIERE_PUBLICACION"]);
                        if (dt["NOT_FECHA_ACTO"] != DBNull.Value)
                            _objActo.FechaActo = Convert.ToDateTime(dt["NOT_FECHA_ACTO"]);
                        if (dt["NOT_RUTA_DOCUMENTO"] != DBNull.Value)
                            _objActo.RutaDocumento = dt["NOT_RUTA_DOCUMENTO"].ToString();
                        if (dt["NOT_ID_TIPO_ACTO"] != DBNull.Value)
                        {
                            _tipoActo = new TipoDocumentoIdentity();
                            _tipoActo = _tipoActoDalc.ObtenerTipoDocumento(Convert.ToInt32(dt["NOT_ID_TIPO_ACTO"]), null);
                            _objActo.IdTipoActo = _tipoActo;
                        }
                        _objActo.NumeroSILPA = Convert.ToString(dt["NOT_NUMERO_SILPA"]);
                        _objActo.NombreProyecto = Convert.ToString(dt["NOMBRE_PROYECTO"]);
                        listaPersonas = _personaDalc.ObtenerPersonas(new object[] { null, _objActo.IdActoNotificacion, null, null, null, null });
                        if (dt["NOT_FECHA_ACTO"] != DBNull.Value)
                        {
                            _objActo.FechaActo = Convert.ToDateTime(dt["NOT_FECHA_ACTO"]);
                        }

                        if (listaPersonas != null)
                        {
                            if (listaPersonas.Count > 0)
                                _objActo.ListaPersonas = listaPersonas;

                            listaActos.Add(_objActo);
                        }
                    }
                }
                return listaActos;
            }
            finally
            {
                dsResultado.Dispose();
                dsResultado = null;
                db = null;
            }
        }


        public NotificacionEntity ObtenerActo(object[] parametros)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_ACTO", parametros);
            DataSet dsResultado = new DataSet();

            NotificacionEntity _objActo;
            DependenciaEntidadEntity _objDependencia = new DependenciaEntidadEntity();

            TipoIdentificacionDalc _tipoIDDalc = new TipoIdentificacionDalc();
            TipoIdentificacionEntity _tipoID = new TipoIdentificacionEntity();
            TipoDocumentoIdentity _tipoActo = new TipoDocumentoIdentity();
            TipoDocumentoDalc _tipoActoDalc = new TipoDocumentoDalc();
            EstadoNotificacionDalc _objEstadoDalc = new EstadoNotificacionDalc();
            PersonaNotificarDalc _personaDalc = new PersonaNotificarDalc();
            List<PersonaNotificarEntity> listaPersonas;

            try
            {
                dsResultado = db.ExecuteDataSet(cmd);
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dt in dsResultado.Tables[0].Rows)
                    {
                        _tipoActo = new TipoDocumentoIdentity();
                        _tipoID = new TipoIdentificacionEntity();
                        _objDependencia = new DependenciaEntidadEntity();
                        _objActo = new NotificacionEntity();
                        listaPersonas = new List<PersonaNotificarEntity>();
                        _objActo = new NotificacionEntity();
                        _objActo.IdActoNotificacion = Convert.ToDecimal(dt["ID_ACTO_NOTIFICACION"]);
                        _objActo.NumeroActoAdministrativo = Convert.ToString(dt["NOT_NUMERO_ACTO_ADMINISTRATIVO"]);
                        _objActo.ParteResolutiva = Convert.ToString(dt["NOT_PARTE_RESOLUTIVA"]);
                        _objActo.CodigoEntidadPublica = Convert.ToInt32(dt["NOT_CODIGO_ENTIDAD_PUBLICA"]);
                        _objActo.ProcesoAdministracion = Convert.ToString(dt["NOT_PROCESO_ADMINISTRACION"]);

                        DependenciaEntidadDalc depDalc = new DependenciaEntidadDalc();
                        _objActo.DependenciaEntidad = depDalc.ObtenerDependencia(Convert.ToInt32(dt["NOT_ID_DEPENDENCIA_ENTIDAD"]));

                        _objDependencia.ID = Convert.ToString(dt["NOT_ID_DEPENDENCIA_ENTIDAD"]);
                        _objActo.DependenciaEntidad = _objDependencia;
                        _objActo.SistemaEntidadPublica = Convert.ToInt32(dt["NOT_SISTEMA_ENTIDAD_PUBLICA"]);
                        _tipoID.Id = Convert.ToInt32(dt["NOT_TIPO_ID_FUNCIONARIO"]);
                        _tipoIDDalc.ObtenerTipoIdentificacion(ref _tipoID);
                        _objActo.TipoIdentificacionFuncionario = _tipoID;
                        _objActo.IdentificacionFuncionario = Convert.ToString(dt["NOT_ID_FUNCIONARIO_NOTIFICANTE"]);
                        _objActo.IdPlantilla = Convert.ToString(dt["NOT_ID_PLANTILLA"]);
                        if (dt["NOT_NOMBRE_PLANTILLA"] != DBNull.Value)
                            _objActo.NombrePlantilla = Convert.ToString(dt["NOT_NOMBRE_PLANTILLA"]);
                        if (dt["NOT_NUMERO_ACTO_ADMINISTRATIVO_ASOCIADO"] != DBNull.Value)
                            _objActo.NumeroActoAdministrativoAsociado = Convert.ToString(dt["NOT_NUMERO_ACTO_ADMINISTRATIVO_ASOCIADO"]);
                        if (dt["NOT_REQUIERE_PUBLICACION"] != DBNull.Value)
                            _objActo.RequierePublicacion = Convert.ToBoolean(dt["NOT_REQUIERE_PUBLICACION"]);
                        if (dt["NOT_FECHA_ACTO"] != DBNull.Value)
                            _objActo.FechaActo = Convert.ToDateTime(dt["NOT_FECHA_ACTO"]);
                        if (dt["NOT_RUTA_DOCUMENTO"] != DBNull.Value)
                            _objActo.RutaDocumento = dt["NOT_RUTA_DOCUMENTO"].ToString();
                        if (dt["NOT_ID_TIPO_ACTO"] != DBNull.Value)
                        {
                            _tipoActo = new TipoDocumentoIdentity();
                            _tipoActo = _tipoActoDalc.ObtenerTipoDocumento(Convert.ToInt32(dt["NOT_ID_TIPO_ACTO"]), null);
                            _objActo.IdTipoActo = _tipoActo;
                        }
                        if (dt["NOT_APLICA_RECURSO"] != DBNull.Value)
                            _objActo.AplicaRecursoReposicion = Convert.ToBoolean(dt["NOT_APLICA_RECURSO"]);
                        else
                            _objActo.AplicaRecursoReposicion = null;

                        if (dt["NOT_ES_NOTIFICACION"] != DBNull.Value)
                            _objActo.EsNotificacion = Convert.ToBoolean(dt["NOT_ES_NOTIFICACION"]);
                        else
                            _objActo.EsNotificacion = null;

                        if (dt["NOT_ES_COMUNICACION"] != DBNull.Value)
                            _objActo.EsComunicacion = Convert.ToBoolean(dt["NOT_ES_COMUNICACION"]);
                        else
                            _objActo.EsComunicacion = null;


                        if (dt["NOT_ES_CUMPLASE"] != DBNull.Value)
                            _objActo.EsCumplase = Convert.ToBoolean(dt["NOT_ES_CUMPLASE"]);
                        else
                            _objActo.EsCumplase = null;

                        _objActo.NumeroSILPA = (dt["NOT_NUMERO_SILPA"] != DBNull.Value) ? null : Convert.ToString(dt["NOT_NUMERO_SILPA"]);
                        listaPersonas = _personaDalc.ObtenerPersonas(new object[] { null, _objActo.IdActoNotificacion, null, null, null, null });
                        if (listaPersonas != null)
                        {
                            if (listaPersonas.Count > 0)
                                _objActo.ListaPersonas = listaPersonas;
                        }
                        return _objActo;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Obtener Acto.";
                throw new Exception(strException, ex);
            }
            finally
            {
                dsResultado.Dispose();
                dsResultado = null;
                db = null;
            }
        }


        /// <summary>
        /// Obtener información de acto administrativo por acto o por publicación
        /// </summary>
        /// <param name="p_lngActoAdministrativoID">long con el identificador del acto administrativo.</param>
        /// <param name="p_strOrigen">string con el origebn desde el cual se obtendar los datos</param>
        /// <param name="p_lngUsuarioID">long con el identificador del usuario que realiza la consulta</param>
        /// <returns>DataTable con la información de configuración del acto administrativo</returns>
        public DataTable ObtenerConfiguracionActoAdministrativo(long p_lngActoAdministrativoID, string p_strOrigen, long p_lngUsuarioID)
        {
            SqlDatabase db = null;
            object[] parametros = new object[] { p_lngActoAdministrativoID, p_strOrigen, p_lngUsuarioID };
            DbCommand cmd = null;
            DataSet dsResultado = null;
            DataTable dtConfiguracion = null;
            
            try
            {
                //Realizar consulta
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());                
                cmd = db.GetStoredProcCommand("NOT_CONSULTA_CONFIGURACION_ACTO_ADMINISTRATIVO", parametros);
                dsResultado = db.ExecuteDataSet(cmd);

                //Verificar si existe informacion
                if(dsResultado != null && dsResultado.Tables.Count > 0 && dsResultado.Tables[0].Rows.Count > 0)
                {
                    dtConfiguracion = dsResultado.Tables[0];
                }
            }
            finally
            {
                dsResultado.Dispose();
                dsResultado = null;
                db = null;
            }

            return dtConfiguracion;
        }


        /// <summary>
        /// Modificar la configuración del acto administrativo
        /// </summary>
        /// <param name="p_lngActoAdministrativoID">long con el identificador del acto administrativo</param>
        /// <param name="p_strOrigen">string con el origen de la información del acto administrativo actual</param>
        /// <param name="p_blnEsNotificacion">bool que indica si presenta notificaciones</param>
        /// <param name="p_blnEsComunicacion">bool que indica si presenta comunicaciones</param>
        /// <param name="p_blnEsCumplase">bool que indica si el acto debe ser cumplido</param>
        /// <param name="p_blnpublica">bool que indica si el acto debe ser publicado</param>
        /// <param name="p_blnAplicaRecurso">boo que indica si el acto aplica recurso de reposición</param>
        /// <param name="p_strRutaActoAdministrativo">string con la ruta del acto administrativo</param>
        /// <param name="p_lngUsuarioID">long con el identificador del usuario que realiza el cambio de configuración</param>
        public void ModificarConfiguracionActoAdministrativo(long p_lngActoAdministrativoID, string p_strOrigen, bool p_blnEsNotificacion, 
                                                             bool p_blnEsComunicacion, bool p_blnEsCumplase, bool p_blnPublica, 
                                                             bool p_blnAplicaRecurso, string p_strRutaActoAdministrativo, long p_lngUsuarioID)
        {
            SqlDatabase db = null;
            object[] parametros = new object[] { p_lngActoAdministrativoID, p_strOrigen, p_blnEsNotificacion, p_blnEsComunicacion, p_blnEsCumplase, p_blnAplicaRecurso, p_blnPublica, p_strRutaActoAdministrativo, p_lngUsuarioID };
            DbCommand cmd = null;

            try
            {
                //Realizar consulta
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                cmd = db.GetStoredProcCommand("NOT_MODIFICAR_CONFIGURACION_ACTO_ADMINISTRATIVO", parametros);
                db.ExecuteNonQuery(cmd);
            }
            finally
            {
                db = null;
            }
        }


        public void Insertar(ref NotificacionEntity acto)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { acto.NumeroActoAdministrativo, 
                acto.ProcesoAdministracion, acto.ParteResolutiva, acto.CodigoEntidadPublica, 
                acto.DependenciaEntidad.ID, acto.SistemaEntidadPublica, acto.TipoIdentificacionFuncionario.Id, 
                acto.IdentificacionFuncionario, acto.IdPlantilla, acto.NombrePlantilla, 
                acto.NumeroActoAdministrativoAsociado, acto.NumeroSILPA, acto.RequierePublicacion,
                acto.IdTipoActo.ID, acto.FechaActo, acto.RutaDocumento, acto.AplicaRecursoReposicion, acto.EsNotificacion, acto.EsComunicacion, acto.EsCumplase, (!string.IsNullOrEmpty(acto.CodigoAA) ? Convert.ToInt32(acto.CodigoAA) : 0), (!string.IsNullOrEmpty(acto.EstadoActo) ? Convert.ToInt32(acto.EstadoActo) : 1), acto.IdActoNotificacion};
            DbCommand cmd = db.GetStoredProcCommand("NOT_INSERT_ACTO", parametros);

            try
            {
                db.ExecuteNonQuery(cmd);
                acto.IdActoNotificacion = Convert.ToDecimal(db.GetParameterValue(cmd, "P_ID_ACTO"));
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Insertar Acto.";
                throw new Exception(strException, ex);
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }

        public void Actualizar(ref NotificacionEntity acto)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { acto.IdActoNotificacion, acto.NumeroActoAdministrativo, acto.ProcesoAdministracion, acto.ParteResolutiva, acto.CodigoEntidadPublica, acto.DependenciaEntidad.ID, acto.SistemaEntidadPublica, acto.TipoIdentificacionFuncionario.Id, acto.IdentificacionFuncionario, acto.IdPlantilla, acto.NombrePlantilla, acto.NumeroActoAdministrativoAsociado, acto.NumeroSILPA, acto.RequierePublicacion, acto.IdTipoActo.ID, acto.FechaActo, acto.RutaDocumento };
            DbCommand cmd = db.GetStoredProcCommand("NOT_UPDATE_ACTO", parametros);
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
        /// Modificar el estado del acto administrativo
        /// </summary>
        /// <param name="p_lngActoID">long con el identificador del acto</param>
        /// <param name="p_intEstadoActoID">int con el identificador del nuevo estado</param>
        /// <param name="p_UsuarioID">int con el identificador del usuario que realiza la modificación</param>
        public void ModificarEstadoActoAdministrativo(long p_lngActoID, int p_intEstadoActoID, int p_UsuarioID)
        {
            SqlDatabase db = null;
            DbCommand cmd = null;

            try
            {
                //Actualizar estado a modificar
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { p_lngActoID, p_intEstadoActoID, p_UsuarioID, "VITAL" };
                cmd = db.GetStoredProcCommand("NOT_ACTUALIZAR_ESTADO_ACTO_NOTIFICAR", parametros);
                db.ExecuteNonQuery(cmd);
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }
                if(db != null)
                    db = null;
            }
        }


        public bool ExistenPendientesNotificarNumeroSilpa(EstadoNotificacionEntity estado, string numeroSilpa)
        {
            // JALCALA 2010-07-26 Regresa verdadero o falso indicando si este número silpa tiene personas por notificar
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { estado.ID, numeroSilpa };
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_ACTOS_ESTADO_NUMERO_SILPA", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            if (dsResultado.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool ExistenPendientesActoAdministrativo(int idActoNotificacion)
        {
            // JALCALA 2010-07-26 Regresa verdadero o falso indicando si este número silpa tiene personas por notificar
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { idActoNotificacion };
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_ACTOS_ESTADO_X_ACTO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            if (dsResultado.Tables[0].Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Método que permite determinar si todas las personas se encuentran en 
        /// estado para permitir la publicación después de la notificación
        /// </summary>
        public int ConsultarTodosPublicacion(decimal _acto, int idTipoDocumento) 
        {
            /*@P_ACTO INT,  	@ID_TIPO_DOCUMENTO INT,  	@TodosPublicables INT OUTPUT*/
            int TodosPublicables = -1;
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { _acto, idTipoDocumento, 0};
            DbCommand cmd = db.GetStoredProcCommand("NOT_CONSULTAR_TODOS_PUBLICABLES", parametros);
            int i = db.ExecuteNonQuery(cmd);
            TodosPublicables = (int)db.GetParameterValue(cmd, "@TodosPublicables");
            return TodosPublicables;
        }


        /// <summary>
        /// HAVA: 14-sep-110
        /// Método  que obtiene el listado de los numero silpa relacionados a las personas notificadas.
        /// </summary>
        /// <param name="NumeroIdentificacionUsuario">string: identificador de </param>
        /// <returns>DataSet: listado de numero silpa</returns>
        public DataSet ListarNumeroSilpaNotificacion(string idusuario)
        {
                //listaNumeroVitalNotificacion
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { idusuario };
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_NUMERO_VITAL_ACTO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return dsResultado;
        }

        /// <summary>
        /// HAVA: 14-sep-110
        /// Método  que obtiene el listado de los numero silpa relacionados a las personas notificadas.
        /// </summary>
        /// <param name="NumeroIdentificacionUsuario">string: identificador de </param>
        /// <returns>DataSet: listado de numero silpa</returns>
        public DataSet ListarActosNotificacion(string idusuario,string numeroVital,string procesoAdministrativo,string actoAdministrativo)
        {
            //listaNumeroVitalNotificacion
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { idusuario,numeroVital,procesoAdministrativo,actoAdministrativo };
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAS_NOTIFICACION", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return dsResultado;
        }

        /// <summary>
        /// HAVA: 14-sep-110
        /// Método  que obtiene el listado de los numero de expediente relacionados a las personas notificadas.
        /// </summary>
        /// <param name="NumeroIdentificacionUsuario">string: numero de identificación de la persona </param>
        /// <returns>DataSet: listado de numero de expediente</returns>
        public DataSet ListarNumeroExpedienteNotificacion(string idusuario)
        {
            //listaNumeroVitalNotificacion
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { idusuario };
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_NUMERO_EXPEDIENTE_ACTO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return dsResultado;
        }
        
        /// <summary>
        /// HAVA: 16-sep-110
        /// Método  que obtiene el listado de los numero de acto administrativo por usuario registrado.
        /// </summary>
        /// <param name="NumeroIdentificacionUsuario">string: numero de identificación de la persona </param>
        /// <returns>DataSet: listado de numero de expediente</returns>
        public DataSet ListarNumeroActoAdministrativoNotificacion(string idusuario)
        {
            //listaNumeroVitalNotificacion
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { idusuario };
            DbCommand cmd = db.GetStoredProcCommand("NOT_LISTAR_NUMERO_ACTO_ADMINISTRATIVO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return dsResultado;
        }
        


//        --------------------------------------------------------------------------------
//CREATE PROCEDURE OBTENER_DATOS_PARA_EJECUTORIA
//(
//    @ID_ACTO BIGINT, -- identificador del acto a notificar
//    @NUMERO_ACTO_ADMINISTRATIVO VARCHAR(50) OUTPUT, ---  Número del documento
//    @NUMERO_PROCESO_ADMINISTRACION VARCHAR(50) OUTPUT, --- Expediente
//    @NUMERO_SILPA VARCHAR(20) OUTPUT, --- Número silpa
//    @PARTE_RESOLUTIVA VARCHAR(1000) OUTPUT


    /// <summary>
    /// HAVA: 19-NOV-110
    /// Método  que obtiene los datos necesarios para la ejecutoria de manera maual
    /// </summary>
    /// <param name="idActoNot"></param>
    public void ObtenerDatosParaEjecutoria(long idActoNot, out string numActoAdm, out string numProcesoAdm,
        out string numsilpa, out string parteResol, out string rutaDocumento, out string identificacionFuncionario)
    {
        //listaNumeroVitalNotificacion
        SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
        object[] parametros = new object[] { idActoNot, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty};
        DbCommand cmd = db.GetStoredProcCommand("OBTENER_DATOS_PARA_EJECUTORIA", parametros);
        int i = db.ExecuteNonQuery(cmd);

        numActoAdm      = db.GetParameterValue(cmd,"@NUMERO_ACTO_ADMINISTRATIVO").ToString();
        numProcesoAdm   = db.GetParameterValue(cmd,"@NUMERO_PROCESO_ADMINISTRACION").ToString();
        numsilpa        = db.GetParameterValue(cmd,"@NUMERO_SILPA").ToString();
        parteResol      = db.GetParameterValue(cmd,"@PARTE_RESOLUTIVA").ToString();
        rutaDocumento   = db.GetParameterValue(cmd,"@RUTA_DOCUMENTO").ToString();
        identificacionFuncionario = db.GetParameterValue(cmd, "@FUNCIONARIO_NOTIFICANTE").ToString();        
    }


        /// <summary>
        /// hava:
        /// Determina si el documento asociado a al ejecutoria procede o no recurso de reposición
        /// </summary>
        /// <param name="idTipoacto">int identificador del tipo del acto gen_tipo_dcoumento</param>
        /// <returns></returns>
        public bool ProcedeRecurso(int idTipoActo) 
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { idTipoActo, 0 };
            DbCommand cmd = db.GetStoredProcCommand("GEN_DOCUMENTO_PROCEDE_RECURSO", parametros);
            int i = db.ExecuteNonQuery(cmd);
            bool result = (bool)db.GetParameterValue(cmd, "@PROCEDE_RECURSO");
            return result;
        }



        public void notificarRecursos(int numeroActoNotificacion, string numeroIdentificacion, string archivosAdjuntos)
        {
            string resultado = String.Empty;
            List<NotificacionEntity> _lstObjNotificacion = new List<NotificacionEntity>();
            _lstObjNotificacion = ObtenerActos(new object[] { numeroActoNotificacion, null, null, null, 
                    null, null, null, null, null, null, null });

            ParametroEntity _parametro1 = new ParametroEntity();
            _parametro1.IdParametro = -1;
            _parametro1.NombreParametro = "CON_RECURSO_INTERPUESTO";
            ParametroDalc parametro = new ParametroDalc();
            parametro.obtenerParametros(ref _parametro1);            

            foreach (NotificacionEntity noty in _lstObjNotificacion)
            {
                NotificacionEntity _objNotificacion = new NotificacionEntity();
                _objNotificacion = noty;

                List<PersonaNotificarEntity> objPersonasNotificarEstadoActivoEject = _objNotificacion.ListaPersonas.FindAll(p => p.EstadoActualNotificado == true);
                foreach (PersonaNotificarEntity per in objPersonasNotificarEstadoActivoEject)
                {
                    //if (per.NumeroIdentificacion == noty.IdentificacionFuncionario)
                    if (per.NumeroIdentificacion == numeroIdentificacion)
                    {
                        SolicitanteEntity solicitante = new SolicitanteEntity();
                        SolicitanteDalc dalc = new SolicitanteDalc();

                        solicitante = dalc.ConsultaSolicitante(null, per.NumeroIdentificacion);

                        //Si es notificación electronica avanzar estado
                        if (solicitante.EsNotificacionElectronica || solicitante.EsNotificacionElectronica_AA || solicitante.EsNotificacionElectronica_EXP)
                        {                            
                            //Verificar si es notificación por expediente, que se encuentre el expediente
                            if (solicitante.EsNotificacionElectronica_EXP)
                            {
                                NotExpedientesEntityDalc dalcNot = new NotExpedientesEntityDalc();
                                List<NotExpedientesEntity> not = dalcNot.ConsultarNumeroSilpaPersonaNotificar(solicitante.NumeroIdentificacion, noty.NumeroSILPAAsociado);

                                if (not != null)
                                {
                                    if (not.Count != 0)
                                    {
                                        EstadoNotificacionDalc _estadoNotificacionDalc = new EstadoNotificacionDalc();
                                        Object[] parametrosIns = { per.IdActoNotificar, int.Parse(_parametro1.Parametro), per.Id, DateTime.Now, archivosAdjuntos, string.Empty, 0, 0 };
                                        resultado = _estadoNotificacionDalc.CrearEstadoPersonaActo(parametrosIns);
                                    }
                                }
                            }
                            else
                            {
                                EstadoNotificacionDalc _estadoNotificacionDalc = new EstadoNotificacionDalc();
                                Object[] parametrosIns = { per.IdActoNotificar, int.Parse(_parametro1.Parametro), per.Id, DateTime.Now, archivosAdjuntos, string.Empty, 0, 0 };
                                resultado = _estadoNotificacionDalc.CrearEstadoPersonaActo(parametrosIns);
                            }
                        }
                    }
                }
            }
        }

        public void notificarRecursosRegistraRecurso(int numeroActoNotificacion, string numeroIdentificacion, string archivosAdjuntos)
        {
            List<NotificacionEntity> _lstObjNotificacion = new List<NotificacionEntity>();
            _lstObjNotificacion = ObtenerActos(new object[] { numeroActoNotificacion, null, null, null, 
                    null, null, null, null, null, null, null });

            ParametroEntity _parametro1 = new ParametroEntity();
            _parametro1.IdParametro = -1;
            _parametro1.NombreParametro = "CON_RECURSO_INTERPUESTO";
            ParametroDalc parametro = new ParametroDalc();
            parametro.obtenerParametros(ref _parametro1);

            foreach (NotificacionEntity noty in _lstObjNotificacion)
            {
                NotificacionEntity _objNotificacion = new NotificacionEntity();
                _objNotificacion = noty;

                //List<PersonaNotificarEntity> objPersonasNotificarEstadoActivoEject = _objNotificacion.ListaPersonas.FindAll(p => p.EstadoActualNotificado == true);
                  //foreach (PersonaNotificarEntity per in objPersonasNotificarEstadoActivoEject)
                //{
                //if (per.NumeroIdentificacion == noty.IdentificacionUsuario)
                if (noty.IdentificacionUsuario == numeroIdentificacion)
                {

                    PersonaNotificarEntity objPersonasNotificarEstadoActivoEject = _objNotificacion.ListaPersonas.Find(p => p.NumeroIdentificacion == noty.IdentificacionUsuario);

                    EstadoNotificacionDalc _estadoNotificacionDalc = new EstadoNotificacionDalc();
                    Object[] parametrosIns = { objPersonasNotificarEstadoActivoEject.IdActoNotificar, int.Parse(_parametro1.Parametro), objPersonasNotificarEstadoActivoEject.Id, DateTime.Now, archivosAdjuntos, string.Empty, 0, 0 };
                    string resultado = _estadoNotificacionDalc.CrearEstadoPersonaActo(parametrosIns);
                    bool respuesta;
                    if (resultado != String.Empty)
                        respuesta = false;


                }
                //}
            }
        }
        public int DiasDiferencia(DateTime fechaEstado, DateTime fechaHoy)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { fechaEstado, fechaHoy, 0 };
            DbCommand cmd = db.GetStoredProcCommand("SP_DIAS_DIFERENCIA", parametros);
            db.ExecuteNonQuery(cmd);
            int dias = (int)db.GetParameterValue(cmd, "@P_DIAS");
            return dias;
        }

        // JM - 15/04/2013
        // Actualiza la Fecha_Fijacion de la Publicaciones cuando no existan notificaciones pendientes
        public void ActualizarFechaFijacionPublicacion(NotificacionEntity acto)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { null, acto.ProcesoAdministracion, acto.NumeroSILPA, acto.NumeroActoAdministrativo};
            DbCommand cmd = db.GetStoredProcCommand("NOT_ACTUALIZAR_FECHA_FIJACION_PUBLICACION", parametros);
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


        public bool ValidaNumeroSilpaNotificacionesPendiente(string numeroSilpa)
        {
            DataSet ds_data = new DataSet();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { numeroSilpa };

            DbCommand cmd = db.GetStoredProcCommand("NOT_PENDIENTES_X_NUM_SILPA", parametros);
            try
            {
                ds_data = db.ExecuteDataSet(cmd);

                if (ds_data.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds_data.Tables[0].Rows[0];

                    var resultado = (Int32)dr["CUENTA"];
                    if (resultado == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }


                }
                else
                {
                    return true;
                }
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }


        /// <summary>
        /// Verificar si existen pendientes de notificar
        /// </summary>
        /// <param name="p_lngIdActo">long con el identificador del acto que se comprobará</param>
        /// <returns>bool con true en caso de que existan pendientes, false en caso contrario.</returns>
        public bool ExistePendientesFinalizarNotificacionActo(long p_lngIdActo)
        {
            DataSet ds_data = new DataSet();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { p_lngIdActo };

            DbCommand cmd = db.GetStoredProcCommand("NOT_EXISTE_PENDIENTES_X_ACTO_NOTIFICACION", parametros);
            try
            {
                ds_data = db.ExecuteDataSet(cmd);

                if (ds_data.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds_data.Tables[0].Rows[0];

                    var resultado = (Int32)dr["PENDIENTE"];
                    if (resultado == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }


                }
                else
                {
                    return true;
                }
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }


        /// <summary>
        /// Verificar si existen pendientes de finalización por acto de notificación
        /// </summary>
        /// <param name="p_lngIdActo">long con el identificador del acto que se comprobará</param>
        /// <returns>bool con true en caso de que existan pendientes, false en caso contrario.</returns>
        public bool ExistePendientesFinalizarActo(long p_lngIdActo)
        {
            DataSet ds_data = new DataSet();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { p_lngIdActo };

            DbCommand cmd = db.GetStoredProcCommand("NOT_EXISTE_PENDIENTES_X_ACTO", parametros);
            try
            {
                ds_data = db.ExecuteDataSet(cmd);

                if (ds_data.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds_data.Tables[0].Rows[0];

                    var resultado = (Int32)dr["PENDIENTE"];
                    if (resultado == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }


                }
                else
                {
                    return true;
                }
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }

        /// <summary>
        /// Verifica si existen pendientes de finalziar por acto de notificacion teniendo prioridad la notificacion
        /// </summary>
        /// <param name="p_lngIdActo"></param>
        /// <returns></returns>
        public bool ExistePendientesFinalizarActoExcluyenteNotifComuni(long p_lngIdActo)
        {
            DataSet ds_data = new DataSet();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { p_lngIdActo };

            DbCommand cmd = db.GetStoredProcCommand("NOT_EXISTE_PENDIENTES_X_ACTO_PRIORIDAD_NOTIFICACION", parametros);
            try
            {
                ds_data = db.ExecuteDataSet(cmd);

                if (ds_data.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds_data.Tables[0].Rows[0];

                    var resultado = (Int32)dr["PENDIENTE"];
                    if (resultado == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }


                }
                else
                {
                    return true;
                }
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }

        /// <summary>
        /// Verificar si existen pendientes de finalización por acto de notificación
        /// </summary>
        /// <param name="p_lngIdActo">long con el identificador del acto que se comprobará</param>
        /// <returns>bool con true en caso de que existan pendientes, false en caso contrario.</returns>
        public bool ExistePendientesFinalizarActoCorporaciones(long p_lngIdActo)
        {
            DataSet ds_data = new DataSet();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { p_lngIdActo };

            DbCommand cmd = db.GetStoredProcCommand("NOT_EXISTE_PENDIENTES_X_ACTO_CORPORACION", parametros);
            try
            {
                ds_data = db.ExecuteDataSet(cmd);

                if (ds_data.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds_data.Tables[0].Rows[0];

                    var resultado = (Int32)dr["PENDIENTE"];
                    if (resultado == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }


                }
                else
                {
                    return true;
                }
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }

        /// <summary>
        /// Retorna el listado de usuarios que se encuentran relacionados a un acto de notificación
        /// </summary>
        /// <param name="p_lngIdActo">long con el identificador del acto que se comprobará</param>
        /// <returns>DataTable con la información de los usuarios.</returns>
        public DataTable ConsultarUsuariosActo(long p_lngIdActo)
        {
            DataSet ds_data = new DataSet();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { p_lngIdActo };

            DbCommand cmd = db.GetStoredProcCommand("NOT_USUARIOS_ESTADO_ACTO", parametros);
            try
            {
                ds_data = db.ExecuteDataSet(cmd);

                if (ds_data != null && ds_data.Tables != null && ds_data.Tables.Count > 0)
                {
                    return ds_data.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                db = null;
            }
        }


        /// <summary>
        /// Lista las autoridades ambientales que se encuentran inscritas a notificación ele
        /// </summary>
        /// <param name="p_blnIntegradaNotificacionElectronica">bool indicando si extrae las autoridades integradas a notificación. Opcional</param>
        /// <returns>DataSet con los registros y las siguientes columnas: AUT_ID, AUT_NOMBRE, AUT_DIRECCION, AUT_TELEFONO, AUT_FAX, 
        /// AUT_CORREO, AUT_NIT, AUT_CARGUE, APLICA_RADICACION, AUT_ACTIVO</returns>
        public DataSet ListarAutoridadAmbientalNotificacion(bool? p_blnIntegradaNotificacionElectronica = null)
        {
            SqlDatabase db = null;
            DbCommand cmd = null;

            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                cmd = db.GetStoredProcCommand("NOT_LISTA_AUT_AMB");
                if(p_blnIntegradaNotificacionElectronica != null)
                    db.AddInParameter(cmd, "@P_AUT_INTEGRADA_NOTIFICACION_ELECTRONICA", DbType.Boolean, p_blnIntegradaNotificacionElectronica);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (cmd != null)                
                    cmd.Dispose();                
                cmd = null;
                db = null;
            }
        }


        /// <summary>
        /// Indica si una autoridad ambiental se encuentra inscrita a notificación electronica
        /// </summary>
        /// <param name="p_intIdAutoridad">Id de la autoridad ambiental</param>
        /// <returns>bool indicando si la autoridad ambiental esta inscrita a notificaión electronica</returns>
        public bool AutoridadAmbientalInscritaNotificacion(int p_intIdAutoridad)
        {
            SqlDatabase db = null;
            DbCommand cmd = null;
            bool inscrita = false;

            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { p_intIdAutoridad };
                cmd = db.GetStoredProcCommand("NOT_AUT_AMB_INSCRITA_NOTIFICACION_ELECTRONICA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                
                //Cargar si se encuentra inscrita
                if (dsResultado != null && dsResultado.Tables != null && dsResultado.Tables.Count > 0 && dsResultado.Tables[0].Rows != null && dsResultado.Tables[0].Rows.Count > 0)
                {
                    inscrita = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["INSCRITA"]);
                }
                else
                {
                    inscrita = false;
                }
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al Autoridad Ambienta lInscrita Notificación.";
                throw new Exception(strException, ex);
                return false;
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
                db = null;
            }

            return inscrita;
        }


        /// <summary>
        /// Retornar el listado de actos administrativos con notificación que cumpla con los parametros de buqueda especificados
        /// </summary>
        /// <param name="p_strNumeroVital">string con el numero VITAL. Opcional</param>
        /// <param name="p_strExpediente">string con el codigo del expediente. Opcional</param>
        /// <param name="p_strIdentificacionUsuario">string con el número de identificación del usuario. Opcional</param>
        /// <param name="p_strUsuario">string con el nombre del usuario. Opcional</param>
        /// <param name="p_strNumeroActo">string con el numero de acto. Opcional</param>
        /// <param name="p_intTipoActo">int con el tipo de acto administrativo. Opcional</param>
        /// <param name="p_intDiasVencimientoDesde">int con el valor del rango inicial de números de vencimiento. Opcional</param>
        /// <param name="p_intDiasVencimientoHasta">int con el valor del rango final de números de vencimiento. Opcional</param>
        /// <param name="p_intProvienePDI">int indicando si proviene de sistema de notificación. Opcional</param>
        /// <param name="p_strProcesoPDI">string con el identificador del proceso. Opcional</param>
        /// <param name="p_intFlujo">int con el identificador del flujo</param>
        /// <param name="p_intEstadoActual">int con el id del estado actual que se desea buscar. Opcional</param>
        /// <param name="p_blEsEstadoActual">bool que indica si solo se consulta estado actual o actos que hayan pasado por este estado</param>
        /// <param name="p_objFechaActoDesde">DateTime con la fecha inicial del rango</param>
        /// <param name="p_objFechaActoHasta">DateTime con la fecha final del rango</param>
        /// <param name="p_lngIDApplicationUser">long con el identificador del usuario que realiza la consulta</param>
        /// <returns>DataSet con la información de actos administrativos</returns>
        public DataSet ObtenerListadoActosAdministrativosNotificacion(string p_strNumeroVital, string p_strExpediente, string p_strIdentificacionUsuario, string p_strUsuario,
                                                                      string p_strNumeroActo, int p_intTipoActo, int? p_intDiasVencimientoDesde, int? p_intDiasVencimientoHasta,
                                                                      int p_intProvienePDI, string p_strProcesoPDI, int p_intFlujo, int p_intEstadoActual, bool p_blEsEstadoActual, DateTime p_objFechaActoDesde, DateTime p_objFechaActoHasta,
                                                                      long p_lngIDApplicationUser)
        {
            SqlDatabase db = null;
            DbCommand cmd = null;

            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                cmd = db.GetStoredProcCommand("NOT_CONSULTAR_NOTIFICACIONES");
                if (!string.IsNullOrEmpty(p_strNumeroVital))
                    db.AddInParameter(cmd, "@P_NUMERO_VITAL", DbType.String, p_strNumeroVital);
                if (!string.IsNullOrEmpty(p_strExpediente))
                    db.AddInParameter(cmd, "@P_EXPEDIENTE", DbType.String, p_strExpediente);
                if (!string.IsNullOrEmpty(p_strIdentificacionUsuario))
                    db.AddInParameter(cmd, "@P_IDENTIFICACION_USUARIO", DbType.String, p_strIdentificacionUsuario);
                if (!string.IsNullOrEmpty(p_strUsuario))
                    db.AddInParameter(cmd, "@P_USUARIO_NOTIFICAR", DbType.String, p_strUsuario);
                if (!string.IsNullOrEmpty(p_strNumeroActo))
                    db.AddInParameter(cmd, "@P_NUMERO_ACTO_ADMINISTRATIVO", DbType.String, p_strNumeroActo);
                if (p_intTipoActo > 0)
                    db.AddInParameter(cmd, "@P_TIPO_ACTO_ADMINISTRATIVO", DbType.Int32, p_intTipoActo);
                if (p_intDiasVencimientoDesde != null)
                    db.AddInParameter(cmd, "@P_DIAS_VENCIMIENTO_DESDE", DbType.Int32, p_intDiasVencimientoDesde.Value);
                if (p_intDiasVencimientoHasta != null)
                    db.AddInParameter(cmd, "@P_DIAS_VENCIMIENTO_HASTA", DbType.Int32, p_intDiasVencimientoHasta.Value);
                if (p_intProvienePDI > 0)
                    db.AddInParameter(cmd, "@P_PROVIENE_PDI", DbType.Int32, p_intProvienePDI);
                if (!string.IsNullOrEmpty(p_strProcesoPDI))
                    db.AddInParameter(cmd, "@P_ID_PROCESO_NOTIFICACION", DbType.String, p_strProcesoPDI);
                if (p_intFlujo > 0)
                {
                    db.AddInParameter(cmd, "@P_ID_FLUJO_NOT_ELEC", DbType.Int32, p_intFlujo);                    
                }
                if (p_intEstadoActual > 0)
                {
                    db.AddInParameter(cmd, "@P_ESTADO_NOTIFICACION_ACTUAL", DbType.Int32, p_intEstadoActual);
                    db.AddInParameter(cmd, "@P_ES_ESTADO_ACTUAL", DbType.Boolean, p_blEsEstadoActual);
                }
                db.AddInParameter(cmd, "@P_FECHA_DESDE", DbType.DateTime, p_objFechaActoDesde);
                db.AddInParameter(cmd, "@P_FECHA_HASTA", DbType.DateTime, p_objFechaActoHasta);
                if (p_lngIDApplicationUser > 0)
                {
                    db.AddInParameter(cmd, "@P_ID_APPLICATION_USER", DbType.Int64, p_lngIDApplicationUser);
                }

                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
                db = null;
            }
        }


        /// <summary>
        /// Obtener la informacion de los estados de notificación de un acto administrativo para una persona especifica
        /// </summary>
        /// <param name="p_lngActoID">long con el identificador del acto administrativo</param>
        /// <param name="p_lngPersonaID">long con el identificador de la persona</param>
        /// <returns>DataSet con la información de los estados</returns>
        public DataSet ObtenerListadoEstadosActoPersona(long p_lngActoID, long p_lngPersonaID)
        {
            SqlDatabase db = null;
            DbCommand cmd = null;

            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                cmd = db.GetStoredProcCommand("NOT_CONSULTAR_NOTIFICACIONES_ACTO");
                db.AddInParameter(cmd, "@P_ID_NOT_ACTO", DbType.Int64, p_lngActoID);
                db.AddInParameter(cmd, "@P_ID_PERSONA", DbType.Int64, p_lngPersonaID);

                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
                db = null;
            }
        }


        /// <summary>
        /// Obtener la información de la notificación en el estado actual para una persona
        /// </summary>
        /// <param name="p_lngActoID">long con el identificador del acto administrativo</param>
        /// <param name="p_lngPersonaID">long con el identificador de la persona</param>
        /// <returns>DataSet con la información del estado</returns>
        public DataSet ObtenerEstadoActoPersona(long p_lngActoID, long p_lngPersonaID)
        {
            SqlDatabase db = null;
            DbCommand cmd = null;

            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                cmd = db.GetStoredProcCommand("NOT_CONSULTAR_NOTIFICACION_PERSONA");
                db.AddInParameter(cmd, "@P_ID_ACTO_NOTIFICACION", DbType.Int64, p_lngActoID);
                db.AddInParameter(cmd, "@P_ID_PERSONA", DbType.Int64, p_lngPersonaID);

                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
                db = null;
            }
        }

        /// <summary>
        /// Obtener la información del estado indicado
        /// </summary>
        /// <param name="p_lngEstadoPersonaActoID">long con el identificador del estado</param>
        /// <returns>DataSet con la información del estado</returns>
        public DataSet ObtenerInformacionEstadoPersonaActo(long p_lngEstadoPersonaActoID)
        {
            SqlDatabase db = null;
            DbCommand cmd = null;

            try
            {
                db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                cmd = db.GetStoredProcCommand("NOT_CONSULTAR_ESTADO_NOTIFICACION");
                db.AddInParameter(cmd, "@P_ID_ESTADO_PERSONA_ACTO", DbType.Int64, p_lngEstadoPersonaActoID);

                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
                db = null;
            }
        }


        /// <summary>
        /// Obtener la información necesaria para generar documento PDF
        /// </summary>
        /// <param name="p_lngIdActo">long con la identificación del acto administrativo</param>
        /// <param name="p_intPlantillaID">int con la identificación de la plantilla</param>
        /// <param name="p_intIdPersona">int con la identificación de la persona</param>
        /// <param name="p_intAutoridadID">int con la identificación de la autoridad ambiental</param>
        /// <param name="p_intFlujoID">int con el identificador del flujo</param>
        /// <param name="p_intIdEstado">int con el identificador del estado</param>
        /// <param name="p_intIdUsuario">int con el identificador del usuario que realiza avance de estado</param>
        /// <param name="p_intFirmaID">int con el identificador de la firma. Opcional</param>
        /// <param name="p_blConsultarConceptos">bool que indica si se consulta información de conceptos asociados. Opcional</param>
        /// <returns>DataSet con la información</returns>
        public DataSet ConsultarInformacionActoNotificacion(long p_lngIdActo, int p_intPlantillaID, long p_lngIdPersona, 
                                                            int p_intAutoridadID, int p_intFlujoID, int p_intIdEstado, int p_intIdUsuario, 
                                                            int p_intFirmaID = 0, bool p_blConsultarConceptos = false)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            DataSet objInformacion = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("NOT_CONSULTAR_INFORMACION_ACTO_PLANTILLA");
                objDataBase.AddInParameter(objCommand, "@P_AUT_ID", DbType.Int32, p_intAutoridadID);
                objDataBase.AddInParameter(objCommand, "@P_ID_PLANTILLA", DbType.Int32, p_intPlantillaID);
                objDataBase.AddInParameter(objCommand, "@P_ID_ACTO_NOTIFICACION", DbType.Int64, p_lngIdActo);
                objDataBase.AddInParameter(objCommand, "@P_ID_FLUJO", DbType.Int32, p_intFlujoID);
                objDataBase.AddInParameter(objCommand, "@P_ID_ESTADO", DbType.Int32, p_intIdEstado);
                objDataBase.AddInParameter(objCommand, "@P_ID_PERSONA", DbType.Int64, p_lngIdPersona);
                objDataBase.AddInParameter(objCommand, "@P_ID_USUARIO", DbType.Int32, p_intIdUsuario);
                if (p_intFirmaID > 0)
                    objDataBase.AddInParameter(objCommand, "@P_ID_FIRMA_AUTORIDAD", DbType.Int64, p_intFirmaID);
                objDataBase.AddInParameter(objCommand, "@P_CONSULTAR_CONCEPTOS", DbType.Boolean, p_blConsultarConceptos);

                //Obtener informacion
                objInformacion = objDataBase.ExecuteDataSet(objCommand);

                if (objInformacion != null && objInformacion.Tables.Count == 6)
                {
                    objInformacion.Tables[0].TableName = "FIRMA";
                    objInformacion.Tables[1].TableName = "ACTO";
                    objInformacion.Tables[2].TableName = "USUARIO";
                    objInformacion.Tables[3].TableName = "FUNCIONARIO";
                    objInformacion.Tables[4].TableName = "ESTADO_DEPENDIENTE";
                    objInformacion.Tables[5].TableName = "CONCEPTOS";
                }
                else if (objInformacion != null && objInformacion.Tables.Count == 5)
                {
                    objInformacion.Tables[0].TableName = "ACTO";
                    objInformacion.Tables[1].TableName = "USUARIO";
                    objInformacion.Tables[2].TableName = "FUNCIONARIO";
                    objInformacion.Tables[3].TableName = "ESTADO_DEPENDIENTE";
                    objInformacion.Tables[4].TableName = "CONCEPTOS";
                }

            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PlantillaNotificacionDalc :: ObtenerPlantilla -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PlantillaNotificacionDalc :: ObtenerPlantilla -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

            return objInformacion;
        }

        /// <summary>
        /// Obtener el identificador de la persona a notificar
        /// </summary>
        /// <param name="p_decPersonaID">decimal con el identificador de la persona</param>
        /// <returns>DataTable con la información del estado</returns>
        public DataTable ConsultarActividadRecursoReposicion(decimal p_decPersonaID)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            DataSet objInformacion = null;
            DataTable objDatosEstado = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("NOT_CONSULTAR_ESTADO_RECURSO_REPOSICION");
                objDataBase.AddInParameter(objCommand, "@P_ID_PERSONA", DbType.Decimal, p_decPersonaID);

                //Obtener informacion
                objInformacion = objDataBase.ExecuteDataSet(objCommand);

                if (objInformacion != null && objInformacion.Tables.Count > 0)
                {
                    objDatosEstado = objInformacion.Tables[0];
                }

            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PlantillaNotificacionDalc :: ConsultarActividadRecursoReposicion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PlantillaNotificacionDalc :: ConsultarActividadRecursoReposicion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

            return objDatosEstado;
        }


        /// <summary>
        /// Calcular el siguiente día habíl
        /// </summary>
        /// <param name="p_objFecha">Datetime con la fecha</param>
        /// <param name="p_intDiasHabiles">int días habiles a contar</param>
        /// <returns>DateTime con la fecha calculada</returns>
        public DateTime CalcularFechaHabil(DateTime p_objFecha, int p_intDiasHabiles)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                DbCommand cmd = db.GetStoredProcCommand("SP_CALCULA_FECHA_FIN_DIAS_HABILES");
                db.AddInParameter(cmd, "P_FECHA_INICIAL", DbType.DateTime, p_objFecha);
                db.AddInParameter(cmd, "P_DIAS_HABILES", DbType.Int32, p_intDiasHabiles);
                db.AddOutParameter(cmd, "P_FECHA_CALCULADA", DbType.DateTime, 15);
                db.ExecuteNonQuery(cmd);
                return Convert.ToDateTime(db.GetParameterValue(cmd, "@P_FECHA_CALCULADA"));
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }


        /// <summary>
        /// Obtener el listado de tipos de adjuntos
        /// </summary>
        /// <returns>DataTable con la información del listado de adjuntos</returns>
        public DataTable ConsultarListadoTiposAdjuntosCorreo()
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            DataSet objInformacion = null;
            DataTable objDatosAnexos = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("NOT_LISTAR_TIPO_ANEXO_CORREO");

                //Obtener informacion
                objInformacion = objDataBase.ExecuteDataSet(objCommand);

                if (objInformacion != null && objInformacion.Tables.Count > 0)
                {
                    objDatosAnexos = objInformacion.Tables[0];
                }

            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PlantillaNotificacionDalc :: ConsultarListadoTiposAdjuntosCorreo -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PlantillaNotificacionDalc :: ConsultarListadoTiposAdjuntosCorreo -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

            return objDatosAnexos;
        }


        /// <summary>
        /// Obtener el identificador de la plantilla que debe se debe implemnetar en el envío de correo
        /// </summary>
        /// <param name="p_intAutoridadID">int con el identificador de la autoridad</param>
        /// <param name="p_lngPersonaID">long con el identificador de la persona</param>
        /// <returns>int con el identificador de la plantilla</returns>
        public int ConsultarPlantillaCorreoAutoridadPersona(int p_intAutoridadID, long p_lngPersonaID)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            DataSet objInformacion = null;
            int intPlantillaID = 0;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("NOT_CONSULTAR_PLANTILLA_CORREO_AUTORIDAD_PERSONA");
                objDataBase.AddInParameter(objCommand, "P_AUT_ID", DbType.Int32, p_intAutoridadID);
                objDataBase.AddInParameter(objCommand, "P_ID_PERSONA", DbType.Int64, p_lngPersonaID);

                //Obtener informacion   
                objInformacion = objDataBase.ExecuteDataSet(objCommand);

                if (objInformacion != null && objInformacion.Tables.Count > 0 && objInformacion.Tables[0].Rows.Count > 0)
                {
                    intPlantillaID = Convert.ToInt32(objInformacion.Tables[0].Rows[0]["CORREO_PLANTILLA_ID"]);
                }
                else
                {
                    intPlantillaID = 0;
                }

            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PlantillaNotificacionDalc :: ConsultarPlantillaCorreoAutoridadTipoNotificacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PlantillaNotificacionDalc :: ConsultarPlantillaCorreoAutoridadTipoNotificacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

            return intPlantillaID;
        }



        /// <summary>
        /// Verificar si el acto administrativo notificacdo tiene conceptos asociados
        /// </summary>
        /// <param name="p_lngActoAdministrativoID">long con el identificador del acto administrativo</param>
        /// <returns>bool con true en caso de que tenga conceptos asociados, false en caso contrario</returns>
        public bool ActoTieneConceptosAsociados(long p_lngActoAdministrativoID)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            DataSet objInformacion = null;
            bool blnTieneConceptos = false;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("NOT_ACTO_TIENE_CONCEPTO_ASOCIADO");
                objDataBase.AddInParameter(objCommand, "P_ID_ACTO_NOTIFICACION", DbType.Int64, p_lngActoAdministrativoID);

                //Obtener informacion   
                objInformacion = objDataBase.ExecuteDataSet(objCommand);

                if (objInformacion != null && objInformacion.Tables.Count > 0 && objInformacion.Tables[0].Rows.Count > 0)
                {
                    blnTieneConceptos = Convert.ToBoolean(objInformacion.Tables[0].Rows[0]["TIENE_CONCEPTO"]);
                }
                else
                {
                    blnTieneConceptos = false;
                }

            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PlantillaNotificacionDalc :: ActoTieneConceptosAsociados -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "PlantillaNotificacionDalc :: ActoTieneConceptosAsociados -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

            return blnTieneConceptos;
        }


        /// <summary>
        /// Consultar el listado de conceptos asociados al acto administrativo
        /// </summary>
        /// <param name="p_lngActoAdministrativoID">long con el identificador del acto administrativo</param>
        /// <returns>DataTable con la información de conceptos del acto administrativo</returns>
        public DataTable ConsultarConceptosAsociadosActoAdministrativo(long p_lngActoAdministrativoID)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            DataSet objInformacion = null;
            DataTable objConceptos = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("NOT_CONSULTAR_ACTO_CONCEPTO_ASOCIADO");
                objDataBase.AddInParameter(objCommand, "P_ID_ACTO_NOTIFICACION", DbType.Int64, p_lngActoAdministrativoID);

                //Obtener informacion   
                objInformacion = objDataBase.ExecuteDataSet(objCommand);

                if (objInformacion != null && objInformacion.Tables.Count > 0 && objInformacion.Tables[0].Rows.Count > 0)
                {
                    objConceptos = objInformacion.Tables[0];
                }

            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionDalc :: ConceptosAsociadosActoAdministrativo -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionDalc :: ConceptosAsociadosActoAdministrativo -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

            return objConceptos;
        }


        /// <summary>
        /// Obtener la información de los actos administrativos existentes para notificación y publicación que cumplan con los parámetros de búsqueda
        /// </summary>
        /// <param name="p_strNumeroVital">string con el número vital</param>
        /// <param name="p_strExpediente">string con el código del expediente</param>
        /// <param name="p_strIdentificacionPersona">string con la identificación de la persona a notificar</param>
        /// <param name="p_strNombrePersona">string con el nombre de la persona a notificar</param>
        /// <param name="p_strNumeroActoAdministrativo">string con el número de acto administrativo</param>
        /// <param name="p_intTipoActoAdministrativoID">int con el identificador del tipo de acto administrativo</param>
        /// <param name="p_intEstadoActoId">int con el estado del acto administrativo</param>
        /// <param name="p_fechaActoInicial">DateTime con la fecha del acto administrativo inicial del rango de busqueda</param>
        /// <param name="p_fechaActoFinal">DateTime con la fecha del acto administrativo final del rango de busqueda</param>
        /// <param name="p_lngUsuarioID">long con el identificador del usuario que realiza la consulta</param>
        /// <returns>DataTable con la información de los actos y publicaciones</returns>
        public DataTable ConsultarInformacionActosAdministrativosPublicaciones(string p_strNumeroVital, string p_strExpediente, 
                                                                               string p_strIdentificacionPersona, string p_strNombrePersona,
                                                                               string p_strNumeroActoAdministrativo, int p_intTipoActoAdministrativoID,
                                                                               int p_intEstadoActoId, DateTime p_fechaActoInicial, DateTime p_fechaActoFinal,
                                                                               long p_lngUsuarioID)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            DataSet objInformacion = null;
            DataTable objActos = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("NOT_ADM_LISTA_ACTOS");
                if( !string.IsNullOrEmpty(p_strNumeroVital) )
                    objDataBase.AddInParameter(objCommand, "P_NUMERO_VITAL", DbType.String, p_strNumeroVital);
                if( !string.IsNullOrEmpty(p_strExpediente) )
                    objDataBase.AddInParameter(objCommand, "P_EXPEDIENTE", DbType.String, p_strExpediente);
                if( !string.IsNullOrEmpty(p_strIdentificacionPersona) )
                    objDataBase.AddInParameter(objCommand, "P_IDENTIFICACION_USUARIO", DbType.String, p_strIdentificacionPersona);
                if( !string.IsNullOrEmpty(p_strNombrePersona) )
                    objDataBase.AddInParameter(objCommand, "P_USUARIO_NOTIFICAR", DbType.String, p_strNombrePersona);
                if( !string.IsNullOrEmpty(p_strNumeroActoAdministrativo) )
                    objDataBase.AddInParameter(objCommand, "P_NUMERO_ACTO_ADMINISTRATIVO", DbType.String, p_strNumeroActoAdministrativo);
                if( p_intTipoActoAdministrativoID > 0 )
                    objDataBase.AddInParameter(objCommand, "P_TIPO_ACTO_ADMINISTRATIVO", DbType.Int32, p_intTipoActoAdministrativoID);
                if( p_intEstadoActoId > 0 )
                    objDataBase.AddInParameter(objCommand, "P_ID_ESTADO_ACTO", DbType.Int32, p_intEstadoActoId);
                if( p_fechaActoInicial != default(DateTime) )
                    objDataBase.AddInParameter(objCommand, "P_FECHA_DESDE", DbType.DateTime, p_fechaActoInicial);
                if( p_fechaActoFinal != default(DateTime) )
                    objDataBase.AddInParameter(objCommand, "P_FECHA_HASTA", DbType.DateTime, p_fechaActoFinal);
                objDataBase.AddInParameter(objCommand, "P_ID_APPLICATION_USER", DbType.Int64, p_lngUsuarioID);

                //Obtener informacion   
                objInformacion = objDataBase.ExecuteDataSet(objCommand);

                if (objInformacion != null && objInformacion.Tables.Count > 0 && objInformacion.Tables[0].Rows.Count > 0)
                {
                    objActos = objInformacion.Tables[0];
                }

            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionDalc :: ConsultarInformacionActosAdministrativosPublicaciones -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionDalc :: ConsultarInformacionActosAdministrativosPublicaciones -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

            return objActos;
        }



        /// <summary>
        /// Asociar un cobro a una notificacion
        /// </summary>
        /// <param name="p_decActoNotificacionID">decimal con el identificador del acto de notificacion</param>
        /// <param name="p_intCobroID">int con el identificador del cobro</param>
        public void AsociarCobroNotificacion(decimal p_decActoNotificacionID, int p_intCobroID)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("NOT_INSERTAR_ACTO_COBRO_AUTOLIQUIDACION");
                objDataBase.AddInParameter(objCommand, "P_ID_ACTO_NOTIFICACION", DbType.Int64, p_decActoNotificacionID);
                objDataBase.AddInParameter(objCommand, "P_COBROAUTOLIQ_ID", DbType.Int32, p_intCobroID);

                //Obtener informacion   
                objDataBase.ExecuteNonQuery(objCommand);
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionDalc :: AsociarCobroNotificacion -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionDalc :: AsociarCobroNotificacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

        /// <summary>
        /// Obtiene el identificador de cobro de relacionado al acto administrativo
        /// </summary>
        /// <param name="p_decActoNotificacionID">decimal con el identificador del acto administrativo</param>
        /// <returns>int con el identificador. En caso de no existir cobro relacionado retorna cero (0)</returns>
        public int ObtenerIdentificadorCobro(decimal p_decActoNotificacionID)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            DataSet objDatosCobro = null;
            int intCobroID = 0;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("NOT_CONSULTA_COBRO_AUTOLIQUIDACION_ACTO_NOTIFICACION");
                objDataBase.AddInParameter(objCommand, "P_ID_ACTO_NOTIFICACION", DbType.Int64, p_decActoNotificacionID);

                //Obtener informacion   
                objDatosCobro = objDataBase.ExecuteDataSet(objCommand);

                //Cargar identificador
                if (objDatosCobro != null && objDatosCobro.Tables.Count > 0 && objDatosCobro.Tables[0].Rows.Count > 0)
                {
                    intCobroID = Convert.ToInt32(objDatosCobro.Tables[0].Rows[0]["COBROAUTOLIQ_ID"]);
                }
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionDalc :: ObtenerIdentificadorCobro -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionDalc :: ObtenerIdentificadorCobro -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

            return intCobroID;
        }


        /// <summary>
        /// Retorna listado de procesos asociados al acto administrativo que no han sido notificados
        /// </summary>
        /// <param name="p_decActoNotificacionID">decimal con el identificador del acto administrativo </param>
        /// <returns>List con la información de procesos de notificación asociados</returns>
        public List<NotificacionEntity> ObtenerProcesosAsociadosNoNotificados(decimal p_decActoNotificacionID)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            DataSet objDatosProcesosAsociados = null;
            List<NotificacionEntity> objLstActosAdministrativos = null;
            NotificacionEntity objActoAdministrativo = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("NOT_LISTAR_PROCESOS_ASOCIADOS_NO_NOTIFICADOS");
                objDataBase.AddInParameter(objCommand, "P_ID_ACTO_NOTIFICACION", DbType.Decimal, p_decActoNotificacionID);

                //Obtener informacion   
                objDatosProcesosAsociados = objDataBase.ExecuteDataSet(objCommand);

                //Verificar que se obtenga datos
                if (objDatosProcesosAsociados != null && objDatosProcesosAsociados.Tables.Count > 0 && objDatosProcesosAsociados.Tables[0].Rows.Count > 0)
                {
                    //Crear listado a cargar
                    objLstActosAdministrativos = new List<NotificacionEntity>();

                    //Ciclo que carga información actosa administrativos
                    foreach (DataRow objActo in objDatosProcesosAsociados.Tables[0].Rows)
                    {
                        //Crear objeto y cargar datos
                        objActoAdministrativo = new NotificacionEntity
                        {
                            IdActoNotificacion = Convert.ToDecimal(objActo["ID_ACTO_NOTIFICACION"]),
                            NumeroSILPA = (objActo["NUMERO_VITAL"] != System.DBNull.Value ? objActo["NUMERO_VITAL"].ToString() : ""),
                            ProcesoAdministracion = (objActo["EXPEDIENTE"] != System.DBNull.Value ? objActo["EXPEDIENTE"].ToString() : ""),
                            NumeroActoAdministrativo = (objActo["NUMERO_DOCUMENTO"] != System.DBNull.Value ? objActo["NUMERO_DOCUMENTO"].ToString() : ""),
                            FechaActo = (objActo["FECHA_DOCUMENTO"] != System.DBNull.Value ? Convert.ToDateTime(objActo["FECHA_DOCUMENTO"]) : default(DateTime))
                        };

                        //Cargar al listado
                        objLstActosAdministrativos.Add(objActoAdministrativo);
                    }
                }
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionDalc :: ObtenerProcesosAsociadosNoNotificados -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionDalc :: ObtenerProcesosAsociadosNoNotificados -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

            return objLstActosAdministrativos;
        }


        /// <summary>
        /// Indica si tiene procesos asociados al acto administrativo que no han sido notificados
        /// </summary>
        /// <param name="p_decActoNotificacionID">decimal con el identificador del acto administrativo </param>
        /// <returns>bool en caso de que tenga procesos asociados, false en caso contrario</returns>
        public bool TieneProcesosAsociadosNoNotificados(decimal p_decActoNotificacionID)
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            DataSet objDatosProcesosAsociados = null;
            bool blTieneProcesosAsociados = false;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("NOT_CON_EXISTE_PROCESOS_ASOCIADOS_NO_NOTIFICADOS");
                objDataBase.AddInParameter(objCommand, "P_ID_ACTO_NOTIFICACION", DbType.Decimal, p_decActoNotificacionID);

                //Obtener informacion   
                objDatosProcesosAsociados = objDataBase.ExecuteDataSet(objCommand);

                //Verificar que se obtenga datos
                if (objDatosProcesosAsociados != null && objDatosProcesosAsociados.Tables.Count > 0 && objDatosProcesosAsociados.Tables[0].Rows.Count > 0)
                {
                    blTieneProcesosAsociados = Convert.ToBoolean(objDatosProcesosAsociados.Tables[0].Rows[0]["PROCESOS_SIN_NOTIFICAR"]);
                }
            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionDalc :: TieneProcesosAsociadosNoNotificados -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionDalc :: TieneProcesosAsociadosNoNotificados -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

            return blTieneProcesosAsociados;
        }



        public DataTable listaNotificacionesPorAdelantarBPM()
        {
            DbCommand objCommand = null;
            SqlDatabase objDataBase = null;
            DataSet objInformacion = null;
            DataTable objNotificaciones = null;

            try
            {
                //Cargar conexion
                objDataBase = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                //Cargar comando
                objCommand = objDataBase.GetStoredProcCommand("NOT_CONSULTA_PROCESOS_NOTIFICACION_SIN_AVANZAR_BPM");
                //Obtener informacion   
                objInformacion = objDataBase.ExecuteDataSet(objCommand);

                if (objInformacion != null && objInformacion.Tables.Count > 0 && objInformacion.Tables[0].Rows.Count > 0)
                {
                    objNotificaciones = objInformacion.Tables[0];
                }

            }
            catch (SqlException sqle)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionDalc :: ConsultarInformacionActosAdministrativosPublicaciones -> Error bd: " + sqle.Message + " " + sqle.StackTrace);

                //Escalar error
                throw sqle;
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "NotificacionDalc :: ConsultarInformacionActosAdministrativosPublicaciones -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

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

            return objNotificaciones;
        }
    }
}
