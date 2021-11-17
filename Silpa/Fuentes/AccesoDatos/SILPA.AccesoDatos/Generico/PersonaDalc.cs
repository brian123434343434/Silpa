using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.Comun;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace SILPA.AccesoDatos.Generico
{
    /// <summary>
    /// clase que obtiene los datos de la persona
    /// </summary>
    [Serializable]
    public class PersonaDalc
    {
        private Configuracion objConfiguracion;

        public PersonaDalc() { objConfiguracion = new Configuracion(); }
        /// <summary>
        /// Busca la persona que tiene asignado el Número VITAL de tramite otorgado
        /// </summary>
        /// <param name="numeroVITAL">Número VITAL para Buscar la Persona</param>
        /// <returns></returns>
        public PersonaIdentity BuscarPersonaXNumeroVITAL(string numeroVITAL)
        {
            //Persona de Prueba mientras se implementa el método
            PersonaIdentity persona = new PersonaIdentity();
            persona.PrimerNombre = "María";
            persona.SegundoNombre = "Cristina";
            persona.PrimerApellido = "Flórez";
            persona.SegundoApellido = "Babativa";
            persona.Telefono = "280808997";

            persona.CorreoElectronico = "jgarces@softmanagement.com.co";
            persona.PersonaId = 10203040;
            return persona;
        }

        /// <summary>
        /// Busca las personas que tienen asignado el Número VITAL de tramite otorgado
        /// </summary>
        /// <param name="numeroVITAL">Número VITAL para Buscar las Personas</param>
        /// <returns></returns>
        public List<PersonaIdentity> BuscarPersonaNumeroVITAL(string numeroVITAL)
        {
            //Persona de Prueba mientras se implementa el método
            PersonaIdentity objIdentity = new PersonaIdentity();
            List<PersonaIdentity> _listaPersona = new List<PersonaIdentity>();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { numeroVITAL };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_PERSONA_NUMERO_VITAL", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            TipoPersonaIdentity tmpTipoPerI = new TipoPersonaIdentity();
            SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity tmpTipoIdI = new SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity();
            TipoIdentificacionDalc tmpTipoIdD;
            TipoPersonaDalc tmpTipoPerD;

            foreach (DataRow drResultado in dsResultado.Tables[0].Rows)
            {
                objIdentity.PersonaId = Convert.ToInt32(drResultado["PER_ID"]);
                objIdentity.PrimerNombre = Convert.ToString(drResultado["PER_PRIMER_NOMBRE"]);
                objIdentity.SegundoNombre = Convert.ToString(drResultado["PER_SEGUNDO_NOMBRE"]);
                objIdentity.PrimerApellido = Convert.ToString(drResultado["PER_PRIMER_APELLIDO"]);
                objIdentity.SegundoApellido = Convert.ToString(drResultado["PER_SEGUNDO_APELLIDO"]);
                objIdentity.NumeroIdentificacion = Convert.ToString(drResultado["PER_NUMERO_IDENTIFICACION"]);

                tmpTipoIdI.Id = Convert.ToInt32(drResultado["TID_ID"]);
                tmpTipoIdD = new TipoIdentificacionDalc();
                tmpTipoIdD.ObtenerTipoIdentificacion(ref tmpTipoIdI);

                objIdentity.TipoDocumentoIdentificacion = tmpTipoIdI;

                objIdentity.LugarExpediciónDocumento = Convert.ToString(drResultado["PER_LUGAR_EXPEDICION_DOC"]);
                //objIdentity.Pais = Convert.ToInt32(drResultado["PAI_ID"]);
                objIdentity.Telefono = Convert.ToString(drResultado["PER_TELEFONO"]);
                objIdentity.Celular = Convert.ToString(drResultado["PER_CELULAR"]);
                objIdentity.Fax = Convert.ToString(drResultado["PER_FAX"]);
                objIdentity.CorreoElectronico = Convert.ToString(drResultado["PER_CORREO_ELECTRONICO"]);
                objIdentity.IdApplicationUser = Convert.ToInt32(drResultado["ID_APPLICATION_USER"]);
                objIdentity.RazonSocial = Convert.ToString(drResultado["PER_RAZON_SOCIAL"]);

                tmpTipoPerI.CodigoTipoPersona = Convert.ToInt32(drResultado["PER_TIPO_PERSONA"]);
                tmpTipoPerD = new TipoPersonaDalc(ref tmpTipoPerI);
                objIdentity.TipoPersona = tmpTipoPerI;

                //objIdentity.Otro = Convert.ToString(drResultado["PER_OTRO"]);
                objIdentity.TarjetaProfesional = Convert.ToString(drResultado["PER_TARJETA_PROFESIONAL"]);
                objIdentity.Pregunta = drResultado["PER_PREGUNTA"].ToString();
                objIdentity.Respuesta = drResultado["PER_PREGUNTA"].ToString();
                objIdentity.Activo = Convert.ToBoolean(drResultado["PER_ACTIVO"]);
                //objIdentity. = Convert.ToString(drResultado["PER_ID_SOLICITANTE"]);
                objIdentity.IdAutoridadAmbiental = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["AUT_ID"]);
                //tmpTipoPerI.CodigoTipoPersona = Convert.ToInt32(drResultado["PER_CALIDAD_ACTUA"]);
                /*tmpTipoPerD = new TipoPersonaDalc(ref tmpTipoPerI);
                objIdentity.CalidadActua = tmpTipoPerI;*/

                _listaPersona.Add(objIdentity);
            }

            return _listaPersona;
        }

        /// <summary>
        /// Método que obtiene el Identity de persona mediante su número de identificación.
        /// </summary>
        /// <param name="strIdentificacionPersona">string: con el número de identificación de la persona</param>
        /// <returns>PersonaIndentity: objeto que contiene los datos de persona</returns>
        public PersonaIdentity BuscarPersonaByIdentification(string strIdentificacionPersona)
        {
            //Persona de Prueba mientras se implementa el método
            PersonaIdentity persona = new PersonaIdentity();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

            object[] parametros = new object[] { strIdentificacionPersona};

            DbCommand cmd = db.GetStoredProcCommand("BPM_OBTENER_USUARIO_X_IDENTIFICACION", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            PersonaIdentity objIdentity = new PersonaIdentity();

            if (dsResultado.Tables[0].Rows.Count > 0)
            {
                if (dsResultado.Tables[0].Rows[0]["ID"] != null) { objIdentity.IdApplicationUser = Int64.Parse(dsResultado.Tables[0].Rows[0]["ID"].ToString()); }
                if (dsResultado.Tables[0].Rows[0]["CODE"] != null) { objIdentity.IdUsuario = dsResultado.Tables[0].Rows[0]["CODE"].ToString(); }
                if (dsResultado.Tables[0].Rows[0]["CODE"] != null) { objIdentity.Username = dsResultado.Tables[0].Rows[0]["CODE"].ToString(); }
                if (dsResultado.Tables[0].Rows[0]["FirstName"] != null) { objIdentity.PrimerNombre = dsResultado.Tables[0].Rows[0]["FirstName"].ToString(); }
                if (dsResultado.Tables[0].Rows[0]["MiddleName"] != null) { objIdentity.SegundoNombre = dsResultado.Tables[0].Rows[0]["MiddleName"].ToString(); }
                if (dsResultado.Tables[0].Rows[0]["LastName"] != null) { objIdentity.PrimerApellido = dsResultado.Tables[0].Rows[0]["LastName"].ToString(); }
                if (dsResultado.Tables[0].Rows[0]["Identification"] != null) { objIdentity.NumeroIdentificacion = dsResultado.Tables[0].Rows[0]["Identification"].ToString(); }
                if (dsResultado.Tables[0].Rows[0]["Email"] != null) { objIdentity.CorreoElectronico = dsResultado.Tables[0].Rows[0]["Email"].ToString(); }
                if (dsResultado.Tables[0].Rows[0]["Password"] != null) { objIdentity.Password = dsResultado.Tables[0].Rows[0]["Password"].ToString(); }
                return objIdentity;
            }
            else {
                return null;
            }

        }

        /// <summary>
        /// Método que obtiene el Identity de persona mediante su IdUsuario.
        /// </summary>
        /// <param name="intIdPersona">int: identificador de la persona registrada</param>
        /// <returns>PersonaIndentity: objeto que contiene los datos de persona</returns>
        public PersonaIdentity BuscarPersonaByUserId(string strIdPersona)
        {
            //Persona de Prueba mientras se implementa el método
            PersonaIdentity persona = new PersonaIdentity();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

            int intIdPersona = int.Parse(strIdPersona);

            object[] parametros = new object[] { intIdPersona, null, null };

            DbCommand cmd = db.GetStoredProcCommand("BPM_OBTENER_USUARIO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            PersonaIdentity objIdentity = new PersonaIdentity();

            if (dsResultado.Tables[0].Rows.Count > 0)
            {
                if (dsResultado.Tables[0].Rows[0]["ID"] != null) { objIdentity.IdApplicationUser = Int64.Parse(dsResultado.Tables[0].Rows[0]["ID"].ToString()); }
                if (dsResultado.Tables[0].Rows[0]["CODE"] != null) { objIdentity.IdUsuario = dsResultado.Tables[0].Rows[0]["CODE"].ToString(); }
                if (dsResultado.Tables[0].Rows[0]["CODE"] != null) { objIdentity.Username = dsResultado.Tables[0].Rows[0]["CODE"].ToString(); }
                if (dsResultado.Tables[0].Rows[0]["FirstName"] != null) { objIdentity.PrimerNombre = dsResultado.Tables[0].Rows[0]["FirstName"].ToString(); }
                if (dsResultado.Tables[0].Rows[0]["MiddleName"] != null) { objIdentity.SegundoNombre = dsResultado.Tables[0].Rows[0]["MiddleName"].ToString(); }
                if (dsResultado.Tables[0].Rows[0]["LastName"] != null) { objIdentity.PrimerApellido = dsResultado.Tables[0].Rows[0]["LastName"].ToString(); }
                if (dsResultado.Tables[0].Rows[0]["Identification"] != null) { objIdentity.NumeroIdentificacion = dsResultado.Tables[0].Rows[0]["Identification"].ToString(); }
                if (dsResultado.Tables[0].Rows[0]["Email"] != null) { objIdentity.CorreoElectronico = dsResultado.Tables[0].Rows[0]["Email"].ToString(); }
                if (dsResultado.Tables[0].Rows[0]["Password"] != null) { objIdentity.Password = dsResultado.Tables[0].Rows[0]["Password"].ToString(); }
                return objIdentity;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Método que obtiene el Identity de persona mediante su IdUsuario.
        /// </summary>
        /// <param name="intIdPersona">int: identificador de la persona registrada</param>
        /// <returns>PersonaIndentity: objeto que contiene los datos de persona</returns>
        public PersonaIdentity BuscarPersonaByUsername(string strUsername)
        {
            //Persona de Prueba mientras se implementa el método
            PersonaIdentity persona = new PersonaIdentity();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

            object[] parametros = new object[] { null, null, strUsername };

            DbCommand cmd = db.GetStoredProcCommand("BPM_OBTENER_USUARIO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            PersonaIdentity objIdentity = new PersonaIdentity();
            if (dsResultado.Tables[0].Rows.Count > 0)
            {
                if (dsResultado.Tables[0].Rows[0]["ID"] != null) { objIdentity.IdApplicationUser = Int64.Parse(dsResultado.Tables[0].Rows[0]["ID"].ToString()); }
                if (dsResultado.Tables[0].Rows[0]["CODE"] != null) { objIdentity.IdUsuario = dsResultado.Tables[0].Rows[0]["CODE"].ToString(); }
                if (dsResultado.Tables[0].Rows[0]["CODE"] != null) { objIdentity.Username = dsResultado.Tables[0].Rows[0]["CODE"].ToString(); }
                if (dsResultado.Tables[0].Rows[0]["FirstName"] != null) { objIdentity.PrimerNombre = dsResultado.Tables[0].Rows[0]["FirstName"].ToString(); }
                if (dsResultado.Tables[0].Rows[0]["MiddleName"] != null) { objIdentity.SegundoNombre = dsResultado.Tables[0].Rows[0]["MiddleName"].ToString(); }
                if (dsResultado.Tables[0].Rows[0]["LastName"] != null) { objIdentity.PrimerApellido = dsResultado.Tables[0].Rows[0]["LastName"].ToString(); }
                if (dsResultado.Tables[0].Rows[0]["Identification"] != null) { objIdentity.NumeroIdentificacion = dsResultado.Tables[0].Rows[0]["Identification"].ToString(); }
                if (dsResultado.Tables[0].Rows[0]["Email"] != null) { objIdentity.CorreoElectronico = dsResultado.Tables[0].Rows[0]["Email"].ToString(); }
                if (dsResultado.Tables[0].Rows[0]["Password"] != null) { objIdentity.Password = dsResultado.Tables[0].Rows[0]["Password"].ToString(); }
            }
            return objIdentity;
        }
        
        /// <summary>
        /// Constructor de la clase que a su vez carga los valores para una identidad de las personas
        /// cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador del tipos de ubicación a cargar, en la propiedad ID del objetoIdentity</param>
        public PersonaDalc(ref PersonaIdentity objIdentity)
        {
            objConfiguracion = new Configuracion();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { objIdentity.PersonaId, objIdentity.IdApplicationUser };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_PERSONA", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            TipoPersonaIdentity tmpTipoPerI = new TipoPersonaIdentity();
            SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity tmpTipoIdI = new SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity();
            TipoIdentificacionDalc tmpTipoIdD;
            TipoPersonaDalc tmpTipoPerD;
            TipoPersonaIdentity tmpTipoSolI = new TipoPersonaIdentity();
          
            objIdentity.PersonaId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PER_ID"]);
            objIdentity.PrimerNombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_PRIMER_NOMBRE"]);
            objIdentity.SegundoNombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_SEGUNDO_NOMBRE"]);
            objIdentity.PrimerApellido = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_PRIMER_APELLIDO"]);
            objIdentity.SegundoApellido = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_SEGUNDO_APELLIDO"]);
            objIdentity.NumeroIdentificacion = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_NUMERO_IDENTIFICACION"]);
            tmpTipoIdI.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TID_ID"]);
            tmpTipoIdD = new TipoIdentificacionDalc();
            tmpTipoIdD.ObtenerTipoIdentificacion(ref tmpTipoIdI);
            objIdentity.TipoDocumentoIdentificacion = tmpTipoIdI;
            objIdentity.LugarExpediciónDocumento = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_LUGAR_EXPEDICION_DOC"]);
            objIdentity.Pais = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PAI_ID"]);
            objIdentity.Telefono = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_TELEFONO"]);
            objIdentity.Celular = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_CELULAR"]);
            objIdentity.Fax = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_FAX"]);
            objIdentity.CorreoElectronico = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_CORREO_ELECTRONICO"]);
            objIdentity.IdApplicationUser = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ID_APPLICATION_USER"]);
            objIdentity.RazonSocial = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_RAZON_SOCIAL"]);

            tmpTipoPerI.CodigoTipoPersona = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PER_TIPO_PERSONA"]);
            tmpTipoPerD = new TipoPersonaDalc(ref tmpTipoPerI);
            objIdentity.TipoPersona = tmpTipoPerI;

            //10-jun-2010 - aegb : se ingresa tipo solicitante
            if (dsResultado.Tables[0].Rows[0]["TSO_ID"].ToString() != string.Empty)
                tmpTipoSolI.CodigoTipoPersona = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TSO_ID"]);
            else
                tmpTipoSolI.CodigoTipoPersona = (int)TipoSolicitante.Solicitante;
            objIdentity.TipoSolicitante = tmpTipoSolI;

            objIdentity.Pregunta = dsResultado.Tables[0].Rows[0]["PER_PREGUNTA"].ToString();
            objIdentity.Respuesta = dsResultado.Tables[0].Rows[0]["PER_PREGUNTA"].ToString();
            objIdentity.Activo = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["PER_ACTIVO"]);
            //objIdentity.Otro = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_OTRO"]);
            objIdentity.TarjetaProfesional = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_TARJETA_PROFESIONAL"]);
            //objIdentity. = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_ID_SOLICITANTE"]);
            objIdentity.IdAutoridadAmbiental = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["AUT_ID"]);
            //tmpTipoPerI.CodigoTipoPersona = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PER_CALIDAD_ACTUA"]);
            /*tmpTipoPerD = new TipoPersonaDalc(ref tmpTipoPerI);
            objIdentity.CalidadActua = tmpTipoPerI;*/

        }
        
        /// <summary>
        /// Modifica un registro de la tabla persona
        /// </summary>
        public void ActualizarPersona(ref PersonaIdentity objIdentity)
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

            object[] parametros = new object[] { 
                                                    objIdentity.PersonaId,        
                                                    objIdentity.PrimerNombre, 
                                                    objIdentity.SegundoNombre,
                                                    objIdentity.PrimerApellido,
                                                    objIdentity.SegundoApellido,
                                                    objIdentity.NumeroIdentificacion,
                                                    objIdentity.TipoDocumentoIdentificacion.Id,
                                                    objIdentity.LugarExpediciónDocumento,
                                                    //objIdentity.Pais,
                                                    objIdentity.Telefono,
                                                    objIdentity.Celular,
                                                    objIdentity.Fax,
                                                    objIdentity.CorreoElectronico,
                                                    objIdentity.IdApplicationUser,
                                                    objIdentity.RazonSocial,
                                                    objIdentity.TipoPersona.CodigoTipoPersona,
                                                    //objIdentity.Otro,
                                                    objIdentity.TarjetaProfesional,
                                                    objIdentity.IdSolicitante,
                                                    objIdentity.Activo,
                                                    objIdentity.Respuesta,
                                                    objIdentity.Pregunta,
                                                    };

            DbCommand cmd = db.GetStoredProcCommand("BAS_UPDATE_PERSONA", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

        }

        /// <summary>
        /// JMM 09-07-2010
        /// Modifica el registro de un solicitnate o un apoderado
        /// </summary>
        /// <param name="objIdentity"></param>
        public void ActualizarPersonaSolicitante(ref PersonaIdentity objIdentity)
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

            object[] parametros = new object[] {
                                                    objIdentity.PersonaId,
                                                    objIdentity.PrimerNombre,
                                                    objIdentity.SegundoNombre,
                                                    objIdentity.PrimerApellido,
                                                    objIdentity.SegundoApellido,
                                                    objIdentity.Telefono,
                                                    objIdentity.Celular,
                                                    objIdentity.Fax,
                                                    objIdentity.CorreoElectronico,
                                                    objIdentity.RazonSocial,
                                                    objIdentity.TarjetaProfesional,
                                                    objIdentity.IdSolicitante,
                                                    objIdentity.AutorizaCorreo //jmarttinez Adicion campo autoriza correo
                                                    };

            DbCommand cmd = db.GetStoredProcCommand("BAS_UPDATE_PERSONA_SOL", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

        }

        /// <summary>
        /// Inserta un registro de la tabla Persona
        /// </summary>
        public void InsertarPersona(ref PersonaIdentity objIdentity)
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

            object[] parametros = new object[] { objIdentity.PersonaId,
                                                     objIdentity.PrimerNombre, 
                                                     objIdentity.SegundoNombre,
                                                     objIdentity.PrimerApellido,
                                                     objIdentity.SegundoApellido,
                                                     objIdentity.NumeroIdentificacion,
                                                     objIdentity.TipoDocumentoIdentificacion.Id,
                                                     objIdentity.LugarExpediciónDocumento,
                                                     objIdentity.Pais,
                                                     objIdentity.Telefono,
                                                     objIdentity.Celular,
                                                     objIdentity.Fax,
                                                     objIdentity.CorreoElectronico,
                                                     objIdentity.IdApplicationUser,
                                                     objIdentity.RazonSocial,
                                                     objIdentity.TipoPersona.CodigoTipoPersona,
                                                     //objIdentity.Otro,
                                                     objIdentity.TarjetaProfesional,
                                                     objIdentity.IdSolicitante,
                                                     objIdentity.Activo,
                                                     objIdentity.Pregunta,
                                                     objIdentity.Respuesta,
                                                     objIdentity.IdAutoridadAmbiental,
                                                     objIdentity.TipoSolicitante.CodigoTipoPersona,
                                                     objIdentity.AutorizaCorreo //jmartinez Adicion campo autoriza correo
                };
            DbCommand cmd = db.GetStoredProcCommand("BAS_CREAR_PERSONA_TODO", parametros);
            db.ExecuteDataSet(cmd);
            objIdentity.PersonaId = Int64.Parse(cmd.Parameters["@PER_ID"].Value.ToString());

        }

        /// <summary>
        /// Actualiza un registro de la tabla Persona y direcciones
        /// </summary>
        public void ActualizaApoderado(ref PersonaIdentity objIdentity, ref DireccionPersonaIdentity objIdentityDir)
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

            object[] parametros = new object[] { 
                                                     objIdentity.PrimerNombre, 
                                                     objIdentity.SegundoNombre,
                                                     objIdentity.PrimerApellido,
                                                     objIdentity.SegundoApellido,
                                                     objIdentity.NumeroIdentificacion,
                                                     objIdentity.TipoDocumentoIdentificacion.Id,
                                                     objIdentity.LugarExpediciónDocumento,
                                                     //objIdentity.Pais,
                                                     objIdentity.Telefono,
                                                     objIdentity.Celular,
                                                     objIdentity.Fax,
                                                     objIdentity.CorreoElectronico,
                                                     objIdentity.IdApplicationUser,
                                                     objIdentity.TipoPersona.CodigoTipoPersona,
                                                     objIdentity.TarjetaProfesional,
                                                     objIdentity.IdSolicitante,
                                                     objIdentity.IdAutoridadAmbiental,
                                                     objIdentity.TipoSolicitante.CodigoTipoPersona,
                                                     objIdentity.Activo, 
                                                     objIdentityDir.MunicipioId,
                                                     objIdentityDir.VeredaId,
                                                     objIdentityDir.CorregimientoId,
                                                     objIdentityDir.DireccionPersona,
                                                     objIdentityDir.PaisId,
                                                     objIdentityDir.TipoDireccion
                                                     
                };

            DbCommand cmd = db.GetStoredProcCommand("BAS_ACTUALIZAR_APODERADOS", parametros);
            db.ExecuteDataSet(cmd);
            //objIdentity.PersonaId = Int64.Parse(cmd.Parameters["@PER_ID"].Value.ToString());

        }
        
        /// <summary>
        /// determina la existencia de una persona mediante el numero de identificación
        /// -1:No existe : puede inscribirse | 0: Existe Por Aprobar  - En proceso - Esperar mail | 1:Aprobado No se puede reinscribir | 2:Existe Rechazado puede Reinscribirse		DECLARE @ESTADO INT
        /// </summary>
        /// <param name="NumeroIdentificacion">String: Numero de identificación</param>
        /// <returns>int: >1 Existe, 0: No existe </returns>
        public int ExistePersona(string NumeroIdentificacion) 
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { NumeroIdentificacion, 0 };

            DbCommand cmd = db.GetStoredProcCommand("GEN_EXISTE_CREDENCIAL_PERSONA", parametros);

            db.ExecuteDataSet(cmd);

            int resultado = -1;

            if (db.GetParameterValue(cmd, "EXISTE") != DBNull.Value)
            {
                resultado = int.Parse(db.GetParameterValue(cmd, "EXISTE").ToString());
            }

            return resultado;
        }

        /// <summary>
        /// Elimina un registro de la tabla Persona
        /// </summary>
        /// <param name="objIdentity.Id">Identificador del registro de la persona a eliminar</param>
        public void EliminarPersona(ref PersonaIdentity objIdentity)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

            object[] parametros = new object[] { objIdentity.PersonaId };

            DbCommand cmd = db.GetStoredProcCommand("BAS_DELETE_PERSONA", parametros);
            db.ExecuteDataSet(cmd);

        }
        
        public DataSet ObtenerPersonasAsociadasSolicitante(ref PersonaIdentity objIdentity)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { objIdentity.PersonaId, objIdentity.TipoPersona.CodigoTipoPersona };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_PERSONAS_ASOCIADAS_SOLICITANTE", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return dsResultado;
        }

        public DataSet ObtenerPersonasAsociadasSolicitanteLeft(ref PersonaIdentity objIdentity)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { objIdentity.PersonaId, objIdentity.TipoPersona.CodigoTipoPersona };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_PERSONAS_ASOCIADAS_SOLICITANTE_LEFT", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return dsResultado;
        }

        public DataSet ObtenerPersonasFiltro(string idNumero,string nombre)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { nombre, idNumero };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_PERSONAS_FILTRO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return dsResultado;
        }

        /// <summary>
        /// Obtener el listado de personas asociadas a un solicitante del tipo indicado
        /// </summary>
        /// <param name="objIdentity">objIdentity con la informacion de la persona</param>
        /// <returns>List con la informacion del solicitante asociado</returns>
        public DataSet ObtenerPersonasAsociadasSolicitanteApoderado(PersonaIdentity objIdentity)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { objIdentity.PersonaId, objIdentity.TipoPersona.CodigoTipoPersona };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_PERSONAS_ASOCIADAS_SOLICITANTE_APOD", parametros);
            return db.ExecuteDataSet(cmd);
        }


        /// <summary>
        /// Obtenerel listado de personas activas que pertenecen a un role especifico
        /// </summary>
        /// <param name="p_intRoleID">int con el identifiacdor del role</param>
        /// <param name="p_blnInformacionDetallada">bool que indica si extrae la informacion detallada</param>
        /// <returns>List con la informacion de las personas</returns>
        public List<PersonaIdentity> ObtenerPersonasPorRoles(int p_intRoleID, bool p_blnInformacionDetallada)
        {
            List<PersonaIdentity> objLstPersona = null;
            PersonaIdentity objPersona;
            SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity objTipoIdentificacion;
            TipoPersonaIdentity objTipoPersona;

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { p_intRoleID };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_PERSONAS_X_ROLE", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            //Verificar si obtuvo informacion
            if (dsResultado != null && dsResultado.Tables.Count > 0 && dsResultado.Tables[0].Rows.Count > 0)
            {
                //Crear listado
                objLstPersona = new List<PersonaIdentity>();

                //Ciclo que carga las personas
                foreach (DataRow objInformacion in dsResultado.Tables[0].Rows)
                {
                    //Cargar datos de persona
                    objPersona = new PersonaIdentity
                    {
                        PersonaId = Convert.ToInt32(objInformacion["PER_ID"]),
                        PrimerNombre = (objInformacion["PER_PRIMER_NOMBRE"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_PRIMER_NOMBRE"]).ToUpper() : ""),
                        SegundoNombre = (objInformacion["PER_SEGUNDO_NOMBRE"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_SEGUNDO_NOMBRE"]).ToUpper() : ""),
                        PrimerApellido = (objInformacion["PER_PRIMER_APELLIDO"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_PRIMER_APELLIDO"]).ToUpper() : ""),
                        SegundoApellido = (objInformacion["PER_SEGUNDO_APELLIDO"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_SEGUNDO_APELLIDO"]).ToUpper() : ""),
                        RazonSocial = (objInformacion["PER_RAZON_SOCIAL"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_RAZON_SOCIAL"]).ToUpper() : ""),
                        NumeroIdentificacion = (objInformacion["PER_NUMERO_IDENTIFICACION"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_NUMERO_IDENTIFICACION"]) : ""),
                        Pais = (objInformacion["PAI_ID"] != System.DBNull.Value ? Convert.ToInt32(objInformacion["PAI_ID"]) : -1),
                        NombrePais = (objInformacion["PAI_NOMBRE"] != System.DBNull.Value ? Convert.ToString(objInformacion["PAI_NOMBRE"]) : ""),
                        Telefono = (objInformacion["PER_TELEFONO"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_TELEFONO"]) : ""),
                        Celular = (objInformacion["PER_CELULAR"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_CELULAR"]) : ""),
                        Fax = (objInformacion["PER_FAX"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_FAX"]) : ""),
                        CorreoElectronico = (objInformacion["PER_CORREO_ELECTRONICO"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_CORREO_ELECTRONICO"]) : ""),
                        IdApplicationUser = (objInformacion["ID_APPLICATION_USER"] != System.DBNull.Value ? Convert.ToInt32(objInformacion["ID_APPLICATION_USER"]) : -1)
                    };

                    //Verificar si se extrae informacion detallada
                    if (p_blnInformacionDetallada)
                    {
                        //Cargar el lugar de expedicion del documento
                        if (objInformacion["PER_IDDEPTO_EXP_DOC"] != System.DBNull.Value && objInformacion["PER_IDMUN_EXP_DOC"] != System.DBNull.Value && Convert.ToInt32(objInformacion["PER_IDDEPTO_EXP_DOC"]) == 11)
                            objPersona.LugarExpediciónDocumento = (objInformacion["PER_MUN_EXP_DOC"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_MUN_EXP_DOC"]) : "");
                        else if (objInformacion["PER_IDDEPTO_EXP_DOC"] != System.DBNull.Value && objInformacion["PER_IDMUN_EXP_DOC"] != System.DBNull.Value && Convert.ToInt32(objInformacion["PER_IDDEPTO_EXP_DOC"]) > 0)
                            objPersona.LugarExpediciónDocumento = (objInformacion["PER_MUN_EXP_DOC"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_MUN_EXP_DOC"]) : "") + " - " +
                                                                  (objInformacion["PER_DEPTO_EXP_DOC"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_DEPTO_EXP_DOC"]) : "");
                        else
                            objPersona.LugarExpediciónDocumento = "";

                        //Obtener el tipo de identificacion
                        objTipoIdentificacion = new Notificacion.TipoIdentificacionEntity { Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TID_ID"]) };
                        (new TipoIdentificacionDalc()).ObtenerTipoIdentificacion(ref objTipoIdentificacion);
                        objPersona.TipoDocumentoIdentificacion = objTipoIdentificacion;

                        //Obtener el tipo de persona
                        objTipoPersona = new TipoPersonaIdentity { CodigoTipoPersona = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PER_TIPO_PERSONA"]) };
                        new TipoPersonaDalc(ref objTipoPersona);
                        objPersona.TipoPersona = objTipoPersona;
                    }

                    //Adicionar persona al listado
                    objLstPersona.Add(objPersona);
                }
            }

            return objLstPersona;
        }


        /// <summary>
        /// Obtener el listado de personas asociadas a un solicitante del tipo indicado
        /// </summary>
        /// <param name="objIdentity">objIdentity con la informacion de la persona</param>
        /// <returns>List con la informacion del solicitante asociado</returns>
        public List<PersonaIdentity> ListaObtenerPersonasAsociadasSolicitanteApoderado(PersonaIdentity objIdentity)
        {
            List<PersonaIdentity> objLstPersona = null;
            PersonaIdentity objPersona;
            SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity objTipoIdentificacion;
            TipoPersonaIdentity objTipoPersona;

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { objIdentity.PersonaId, objIdentity.TipoPersona.CodigoTipoPersona };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_PERSONAS_ASOCIADAS_SOLICITANTE_APOD", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            //Verificar si obtuvo informacion
            if (dsResultado != null && dsResultado.Tables.Count > 0 && dsResultado.Tables[0].Rows.Count > 0)
            {
                //Crear listado
                objLstPersona = new List<PersonaIdentity>();

                //Ciclo que carga las personas
                foreach (DataRow objInformacion in dsResultado.Tables[0].Rows)
                {
                    //Cargar datos de persona
                    objPersona = new PersonaIdentity
                    {
                        PersonaId = Convert.ToInt32(objInformacion["PER_ID"]),
                        PrimerNombre = (objInformacion["PER_PRIMER_NOMBRE"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_PRIMER_NOMBRE"]).ToUpper() : ""),
                        SegundoNombre = (objInformacion["PER_SEGUNDO_NOMBRE"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_SEGUNDO_NOMBRE"]).ToUpper() : ""),
                        PrimerApellido = (objInformacion["PER_PRIMER_APELLIDO"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_PRIMER_APELLIDO"]).ToUpper() : ""),
                        SegundoApellido = (objInformacion["PER_SEGUNDO_APELLIDO"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_SEGUNDO_APELLIDO"]).ToUpper() : ""),
                        RazonSocial = (objInformacion["PER_RAZON_SOCIAL"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_RAZON_SOCIAL"]).ToUpper() : ""),
                        NumeroIdentificacion = (objInformacion["PER_NUMERO_IDENTIFICACION"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_NUMERO_IDENTIFICACION"]).ToUpper() : ""),
                        Pais = (objInformacion["PAI_ID"] != System.DBNull.Value ? Convert.ToInt32(objInformacion["PAI_ID"]) : -1),
                        NombrePais = (objInformacion["PAI_NOMBRE"] != System.DBNull.Value ? Convert.ToString(objInformacion["PAI_NOMBRE"]) : ""),
                        Telefono = (objInformacion["PER_TELEFONO"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_TELEFONO"]) : ""),
                        Celular = (objInformacion["PER_CELULAR"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_CELULAR"]) : ""),
                        Fax = (objInformacion["PER_FAX"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_FAX"]) : ""),
                        CorreoElectronico = (objInformacion["PER_CORREO_ELECTRONICO"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_CORREO_ELECTRONICO"]) : ""),
                        IdApplicationUser = (objInformacion["ID_APPLICATION_USER"] != System.DBNull.Value ? Convert.ToInt32(objInformacion["ID_APPLICATION_USER"]) : -1)
                    };

                    //Cargar el lugar de expedicion del documento
                    if (objInformacion["PER_IDDEPTO_EXP_DOC"] != System.DBNull.Value && objInformacion["PER_IDMUN_EXP_DOC"] != System.DBNull.Value && Convert.ToInt32(objInformacion["PER_IDDEPTO_EXP_DOC"]) == 11)
                        objPersona.LugarExpediciónDocumento = (objInformacion["PER_MUN_EXP_DOC"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_MUN_EXP_DOC"]) : "");
                    else if (objInformacion["PER_IDDEPTO_EXP_DOC"] != System.DBNull.Value && objInformacion["PER_IDMUN_EXP_DOC"] != System.DBNull.Value && Convert.ToInt32(objInformacion["PER_IDDEPTO_EXP_DOC"]) > 0)
                        objPersona.LugarExpediciónDocumento = (objInformacion["PER_MUN_EXP_DOC"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_MUN_EXP_DOC"]) : "") + " - " +
                                                              (objInformacion["PER_DEPTO_EXP_DOC"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_DEPTO_EXP_DOC"]) : "");
                    else
                        objPersona.LugarExpediciónDocumento = "";

                    //Obtener el tipo de identificacion
                    objTipoIdentificacion = new Notificacion.TipoIdentificacionEntity { Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TID_ID"]) };
                    (new TipoIdentificacionDalc()).ObtenerTipoIdentificacion(ref objTipoIdentificacion);
                    objPersona.TipoDocumentoIdentificacion = objTipoIdentificacion;

                    //Obtener el tipo de persona
                    objTipoPersona = new TipoPersonaIdentity { CodigoTipoPersona = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PER_TIPO_PERSONA"]) };
                    new TipoPersonaDalc(ref objTipoPersona);
                    objPersona.TipoPersona = objTipoPersona;

                    //Adicionar persona al listado
                    objLstPersona.Add(objPersona);
                }
            }

            return objLstPersona;
        }


        /// <summary>
        /// Obtener el listado de personas que representan a un solicitante
        /// </summary>
        /// <param name="p_lngSolicitanteID">long con el identifiacdor de la persona</param>
        /// <param name="p_blnInformacionCompleta">bool que indica si se extrae la informacion completa</param>
        /// <returns>List con la informacion del solicitante asociado</returns>
        public List<PersonaIdentity> ListaPersonasRepresentanUsuario(long p_lngSolicitanteID, bool p_blnInformacionCompleta)
        {
            List<PersonaIdentity> objLstPersona = null;
            PersonaIdentity objPersona;
            SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity objTipoIdentificacion;
            TipoPersonaIdentity objTipoPersona;

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { p_lngSolicitanteID };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_PERSONAS_REPRESENTANTES_SOLICITANTE", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            //Verificar si obtuvo informacion
            if (dsResultado != null && dsResultado.Tables.Count > 0 && dsResultado.Tables[0].Rows.Count > 0)
            {
                //Crear listado
                objLstPersona = new List<PersonaIdentity>();

                //Ciclo que carga las personas
                foreach (DataRow objInformacion in dsResultado.Tables[0].Rows)
                {
                    //Cargar datos de persona
                    objPersona = new PersonaIdentity
                    {
                        PersonaId = Convert.ToInt32(objInformacion["PER_ID"]),
                        PrimerNombre = (objInformacion["PER_PRIMER_NOMBRE"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_PRIMER_NOMBRE"]).ToUpper() : ""),
                        SegundoNombre = (objInformacion["PER_SEGUNDO_NOMBRE"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_SEGUNDO_NOMBRE"]).ToUpper() : ""),
                        PrimerApellido = (objInformacion["PER_PRIMER_APELLIDO"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_PRIMER_APELLIDO"]).ToUpper() : ""),
                        SegundoApellido = (objInformacion["PER_SEGUNDO_APELLIDO"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_SEGUNDO_APELLIDO"]).ToUpper() : ""),
                        RazonSocial = (objInformacion["PER_RAZON_SOCIAL"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_RAZON_SOCIAL"]).ToUpper() : ""),
                        NumeroIdentificacion = (objInformacion["PER_NUMERO_IDENTIFICACION"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_NUMERO_IDENTIFICACION"]).ToUpper() : ""),
                        Pais = (objInformacion["PAI_ID"] != System.DBNull.Value ? Convert.ToInt32(objInformacion["PAI_ID"]) : -1),
                        NombrePais = (objInformacion["PAI_NOMBRE"] != System.DBNull.Value ? Convert.ToString(objInformacion["PAI_NOMBRE"]) : ""),
                        Telefono = (objInformacion["PER_TELEFONO"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_TELEFONO"]) : ""),
                        Celular = (objInformacion["PER_CELULAR"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_CELULAR"]) : ""),
                        Fax = (objInformacion["PER_FAX"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_FAX"]) : ""),
                        CorreoElectronico = (objInformacion["PER_CORREO_ELECTRONICO"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_CORREO_ELECTRONICO"]) : ""),
                        IdApplicationUser = (objInformacion["ID_APPLICATION_USER"] != System.DBNull.Value ? Convert.ToInt32(objInformacion["ID_APPLICATION_USER"]) : -1),
                        Descripcion = (objInformacion["DESCRIPCION"] != System.DBNull.Value ? Convert.ToString(objInformacion["DESCRIPCION"]) : "")
                    };

                    //Verifca si se obtiene la informacion detallada
                    if (p_blnInformacionCompleta)
                    {

                        //Cargar el lugar de expedicion del documento
                        if (objInformacion["PER_IDDEPTO_EXP_DOC"] != System.DBNull.Value && objInformacion["PER_IDMUN_EXP_DOC"] != System.DBNull.Value && Convert.ToInt32(objInformacion["PER_IDDEPTO_EXP_DOC"]) == 11)
                            objPersona.LugarExpediciónDocumento = (objInformacion["PER_MUN_EXP_DOC"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_MUN_EXP_DOC"]) : "");
                        else if (objInformacion["PER_IDDEPTO_EXP_DOC"] != System.DBNull.Value && objInformacion["PER_IDMUN_EXP_DOC"] != System.DBNull.Value && Convert.ToInt32(objInformacion["PER_IDDEPTO_EXP_DOC"]) > 0)
                            objPersona.LugarExpediciónDocumento = (objInformacion["PER_MUN_EXP_DOC"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_MUN_EXP_DOC"]) : "") + " - " +
                                                                  (objInformacion["PER_DEPTO_EXP_DOC"] != System.DBNull.Value ? Convert.ToString(objInformacion["PER_DEPTO_EXP_DOC"]) : "");
                        else
                            objPersona.LugarExpediciónDocumento = "";

                        //Obtener el tipo de identificacion
                        objTipoIdentificacion = new Notificacion.TipoIdentificacionEntity { Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TID_ID"]) };
                        (new TipoIdentificacionDalc()).ObtenerTipoIdentificacion(ref objTipoIdentificacion);
                        objPersona.TipoDocumentoIdentificacion = objTipoIdentificacion;

                        //Obtener el tipo de persona
                        objTipoPersona = new TipoPersonaIdentity { CodigoTipoPersona = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PER_TIPO_PERSONA"]) };
                        new TipoPersonaDalc(ref objTipoPersona);
                        objPersona.TipoPersona = objTipoPersona;
                    }

                    //Adicionar persona al listado
                    objLstPersona.Add(objPersona);
                }
            }

            return objLstPersona;
        }

        public void ObtenerPersonaPorNumeroIdentificacion(ref PersonaIdentity objIdentity)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { objIdentity.NumeroIdentificacion, objIdentity.TipoDocumentoIdentificacion.Id };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_PERSONA_IDENTIFICACION", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            TipoPersonaIdentity tmpTipoPerI = new TipoPersonaIdentity();
            SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity tmpTipoIdI = new SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity();
            TipoIdentificacionDalc tmpTipoIdD;
            TipoPersonaDalc tmpTipoPerD;
            DireccionPersonaDalc objDireccionPersona = new DireccionPersonaDalc();
            if (dsResultado.Tables[0].Rows.Count > 0)
            {
                objIdentity.PersonaId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PER_ID"]);
                objIdentity.PrimerNombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_PRIMER_NOMBRE"]);
                objIdentity.SegundoNombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_SEGUNDO_NOMBRE"]);
                objIdentity.PrimerApellido = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_PRIMER_APELLIDO"]);
                objIdentity.SegundoApellido = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_SEGUNDO_APELLIDO"]);
                objIdentity.NumeroIdentificacion = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_NUMERO_IDENTIFICACION"]);
                tmpTipoIdI.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TID_ID"]);
                tmpTipoIdD = new TipoIdentificacionDalc();
                tmpTipoIdD.ObtenerTipoIdentificacion(ref tmpTipoIdI);
                objIdentity.TipoDocumentoIdentificacion = tmpTipoIdI;
                objIdentity.LugarExpediciónDocumento = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_LUGAR_EXPEDICION_DOC"]);
                objIdentity.Telefono = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_TELEFONO"]);
                objIdentity.Celular = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_CELULAR"]);
                objIdentity.Fax = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_FAX"]);
                objIdentity.CorreoElectronico = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_CORREO_ELECTRONICO"]);
                objIdentity.IdApplicationUser = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ID_APPLICATION_USER"]);
                objIdentity.RazonSocial = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_RAZON_SOCIAL"]);
                tmpTipoPerI.CodigoTipoPersona = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PER_TIPO_PERSONA"]);
                tmpTipoPerD = new TipoPersonaDalc(ref tmpTipoPerI);
                objIdentity.TipoPersona = tmpTipoPerI;
                objIdentity.TarjetaProfesional = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_TARJETA_PROFESIONAL"]);
                objIdentity.Pregunta = dsResultado.Tables[0].Rows[0]["PER_PREGUNTA"].ToString();
                objIdentity.Respuesta = dsResultado.Tables[0].Rows[0]["PER_PREGUNTA"].ToString();
                objIdentity.Activo = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["PER_ACTIVO"]);
                objIdentity.IdAutoridadAmbiental = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["AUT_ID"]);
                objIdentity.Direcciones = objDireccionPersona.ObtenerDirecciones(objIdentity.PersonaId);
                objIdentity.IdSolicitante = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ID_APPLICATION_USER"]);
                objIdentity.AutorizaCorreo = (dsResultado.Tables[0].Rows[0]["AUTORIZA_NOTICACION"] != System.DBNull.Value ? Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["AUTORIZA_NOTICACION"]) : false);
            }   
        }


        //TODO
        public void ObtenerPersonaByNumeroIdentificacion(ref PersonaIdentity objIdentity)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { objIdentity.NumeroIdentificacion };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_PERSONA", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            TipoPersonaIdentity tmpTipoPerI = new TipoPersonaIdentity();
            SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity tmpTipoIdI = new SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity();
            TipoIdentificacionDalc tmpTipoIdD;
            TipoPersonaDalc tmpTipoPerD;

            objIdentity.PersonaId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PER_ID"]);
            objIdentity.PrimerNombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_PRIMER_NOMBRE"]);
            objIdentity.SegundoNombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_SEGUNDO_NOMBRE"]);
            objIdentity.PrimerApellido = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_PRIMER_APELLIDO"]);
            objIdentity.SegundoApellido = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_SEGUNDO_APELLIDO"]);
            objIdentity.NumeroIdentificacion = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_NUMERO_IDENTIFICACION"]);

            tmpTipoIdI.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TID_ID"]);
            tmpTipoIdD = new TipoIdentificacionDalc();
            tmpTipoIdD.ObtenerTipoIdentificacion(ref tmpTipoIdI);
            objIdentity.TipoDocumentoIdentificacion = tmpTipoIdI;

            objIdentity.LugarExpediciónDocumento = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_LUGAR_EXPEDICION_DOC"]);
            //objIdentity.Pais = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PAI_ID"]);
            objIdentity.Telefono = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_TELEFONO"]);
            objIdentity.Celular = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_CELULAR"]);
            objIdentity.Fax = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_FAX"]);
            objIdentity.CorreoElectronico = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_CORREO_ELECTRONICO"]);
            objIdentity.IdApplicationUser = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ID_APPLICATION_USER"]);
            objIdentity.RazonSocial = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_RAZON_SOCIAL"]);

            tmpTipoPerI.CodigoTipoPersona = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PER_TIPO_PERSONA"]);
            tmpTipoPerD = new TipoPersonaDalc(ref tmpTipoPerI);
            objIdentity.TipoPersona = tmpTipoPerI;

            //objIdentity.Otro = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_OTRO"]);
            objIdentity.TarjetaProfesional = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_TARJETA_PROFESIONAL"]);
            objIdentity.Pregunta = dsResultado.Tables[0].Rows[0]["PER_PREGUNTA"].ToString();
            objIdentity.Respuesta = dsResultado.Tables[0].Rows[0]["PER_PREGUNTA"].ToString();
            objIdentity.Activo = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["PER_ACTIVO"]);
            objIdentity.IdAutoridadAmbiental = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["AUT_ID"]);
            //objIdentity. = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_ID_SOLICITANTE"]);

            //tmpTipoPerI.CodigoTipoPersona = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PER_CALIDAD_ACTUA"]);
            /*tmpTipoPerD = new TipoPersonaDalc(ref tmpTipoPerI);
            objIdentity.CalidadActua = tmpTipoPerI;*/
        }

        //V 1.0.0.42
        /// <summary>
        /// Obtiene un array de personas pertenecientes a una autoridad ambiental
        /// </summary>
        /// <param name="intAutoridadAmbientalID"></param>
        /// <param name="personaID"></param>
        /// <param name="blnEnProceso"></param>
        /// <returns></returns>
        public List<PersonaIdentity> ObtenerPersonasByAutoridadAmbiental(int intAutoridadAmbientalID, int personaID, int intEnProceso, string ruta)
        {

            List<PersonaIdentity> _listaPersona = new List<PersonaIdentity>();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intAutoridadAmbientalID, personaID, intEnProceso };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_PERSONA_X_AUTORIDAD_AMBIENTAL", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            TipoPersonaIdentity tmpTipoPerI = new TipoPersonaIdentity();
            SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity tmpTipoIdI = new SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity();
            TipoIdentificacionDalc tmpTipoIdD;
            TipoPersonaDalc tmpTipoPerD;

            foreach (DataRow drResultado in dsResultado.Tables[0].Rows)
            {
                PersonaIdentity objEntity = new PersonaIdentity();
                DireccionPersonaDalc objDireccionPersona = new DireccionPersonaDalc();

                objEntity.PersonaId = Convert.ToInt32(drResultado["PER_ID"]);
                objEntity.PrimerNombre = Convert.ToString(drResultado["PER_PRIMER_NOMBRE"]);
                objEntity.SegundoNombre = Convert.ToString(drResultado["PER_SEGUNDO_NOMBRE"]);
                objEntity.PrimerApellido = Convert.ToString(drResultado["PER_PRIMER_APELLIDO"]);
                objEntity.SegundoApellido = Convert.ToString(drResultado["PER_SEGUNDO_APELLIDO"]);
                objEntity.NumeroIdentificacion = Convert.ToString(drResultado["PER_NUMERO_IDENTIFICACION"]);
                tmpTipoIdI = new SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity();
                tmpTipoIdI.Id = Convert.ToInt32(drResultado["TID_ID"]);
                tmpTipoIdD = new TipoIdentificacionDalc();
                tmpTipoIdD.ObtenerTipoIdentificacion(ref tmpTipoIdI);

                objEntity.TipoDocumentoIdentificacion = tmpTipoIdI;
                objEntity.LugarExpediciónDocumento = Convert.ToString(drResultado["PER_LUGAR_EXPEDICION_DOC"]);
                //objIdentity.Pais = Convert.ToInt32(drResultado["PAI_ID"]);
                objEntity.Telefono = Convert.ToString(drResultado["PER_TELEFONO"]);
                objEntity.Celular = Convert.ToString(drResultado["PER_CELULAR"]);
                objEntity.Fax = Convert.ToString(drResultado["PER_FAX"]);
                objEntity.CorreoElectronico = Convert.ToString(drResultado["PER_CORREO_ELECTRONICO"]);
                objEntity.IdApplicationUser = Convert.ToInt32(drResultado["ID_APPLICATION_USER"]);
                objEntity.RazonSocial = Convert.ToString(drResultado["PER_RAZON_SOCIAL"]);

                tmpTipoPerI.CodigoTipoPersona = Convert.ToInt32(drResultado["PER_TIPO_PERSONA"]);
                tmpTipoPerD = new TipoPersonaDalc(ref tmpTipoPerI);
                objEntity.TipoPersona = tmpTipoPerI;

                objEntity.TarjetaProfesional = Convert.ToString(drResultado["PER_TARJETA_PROFESIONAL"]);
                objEntity.Pregunta = drResultado["PER_PREGUNTA"].ToString();
                objEntity.Respuesta = drResultado["PER_PREGUNTA"].ToString();
                objEntity.Activo = Convert.ToBoolean(drResultado["PER_ACTIVO"]);
                objEntity.EnProceso = Convert.ToInt32(drResultado["SOL_EN_PROCESO"]);
                objEntity.RutaRtf = ruta + objEntity.NumeroIdentificacion + "/" + objEntity.NumeroIdentificacion + ".rtf";

                objEntity.Direcciones = objDireccionPersona.ObtenerDirecciones(objEntity.PersonaId);
                _listaPersona.Add(objEntity);

            }

            return _listaPersona;
        }

        /// <summary>
        /// JMM - 2010-10-01
        /// Obtiene un array de personas pertenecientes a una autoridad ambiental con estado para aprobar y rechazado
        /// </summary>
        /// <param name="intAutoridadAmbientalID"></param>
        /// <param name="personaID"></param>
        /// <param name="blnEnProceso"></param>
        /// <returns></returns>
        public List<PersonaIdentity> ObtenerPersonasByAutoridadAmbientalAprobRec(int intAutoridadAmbientalID, int personaID, string ruta)
        {

            List<PersonaIdentity> _listaPersona = new List<PersonaIdentity>();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intAutoridadAmbientalID, personaID };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_PERSONA_X_AUTORIDAD_AMBIENTAL_APROB_RECHAZADO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            TipoPersonaIdentity tmpTipoPerI = new TipoPersonaIdentity();
            SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity tmpTipoIdI = new SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity();
            TipoIdentificacionDalc tmpTipoIdD;
            TipoPersonaDalc tmpTipoPerD;

            foreach (DataRow drResultado in dsResultado.Tables[0].Rows)
            {
                PersonaIdentity objEntity = new PersonaIdentity();
                objEntity.PersonaId = Convert.ToInt32(drResultado["PER_ID"]);
                objEntity.PrimerNombre = Convert.ToString(drResultado["PER_PRIMER_NOMBRE"]);
                objEntity.SegundoNombre = Convert.ToString(drResultado["PER_SEGUNDO_NOMBRE"]);
                objEntity.PrimerApellido = Convert.ToString(drResultado["PER_PRIMER_APELLIDO"]);
                objEntity.SegundoApellido = Convert.ToString(drResultado["PER_SEGUNDO_APELLIDO"]);
                objEntity.NumeroIdentificacion = Convert.ToString(drResultado["PER_NUMERO_IDENTIFICACION"]);
                tmpTipoIdI = new SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity();
                tmpTipoIdI.Id = Convert.ToInt32(drResultado["TID_ID"]);
                tmpTipoIdD = new TipoIdentificacionDalc();
                tmpTipoIdD.ObtenerTipoIdentificacion(ref tmpTipoIdI);

                objEntity.TipoDocumentoIdentificacion = tmpTipoIdI;
                objEntity.LugarExpediciónDocumento = Convert.ToString(drResultado["PER_LUGAR_EXPEDICION_DOC"]);
                //objIdentity.Pais = Convert.ToInt32(drResultado["PAI_ID"]);
                objEntity.Telefono = Convert.ToString(drResultado["PER_TELEFONO"]);
                objEntity.Celular = Convert.ToString(drResultado["PER_CELULAR"]);
                objEntity.Fax = Convert.ToString(drResultado["PER_FAX"]);
                objEntity.CorreoElectronico = Convert.ToString(drResultado["PER_CORREO_ELECTRONICO"]);
                objEntity.IdApplicationUser = Convert.ToInt32(drResultado["ID_APPLICATION_USER"]);
                objEntity.RazonSocial = Convert.ToString(drResultado["PER_RAZON_SOCIAL"]);

                tmpTipoPerI.CodigoTipoPersona = Convert.ToInt32(drResultado["PER_TIPO_PERSONA"]);
                tmpTipoPerD = new TipoPersonaDalc(ref tmpTipoPerI);
                objEntity.TipoPersona = tmpTipoPerI;

                objEntity.TarjetaProfesional = Convert.ToString(drResultado["PER_TARJETA_PROFESIONAL"]);
                objEntity.Pregunta = drResultado["PER_PREGUNTA"].ToString();
                objEntity.Respuesta = drResultado["PER_PREGUNTA"].ToString();
                objEntity.Activo = Convert.ToBoolean(drResultado["PER_ACTIVO"]);
                objEntity.EnProceso = Convert.ToInt32(drResultado["SOL_EN_PROCESO"]);
                objEntity.RutaRtf = ruta + objEntity.NumeroIdentificacion + "/" + objEntity.NumeroIdentificacion + ".rtf";
                DireccionPersonaDalc direcciones = new DireccionPersonaDalc();
                objEntity.Direcciones = direcciones.ObtenerDirecciones(objEntity.PersonaId);
                _listaPersona.Add(objEntity);
            }

            return _listaPersona;
        }

        /// <summary>
        /// 01-jul-2010 - aegb
        /// Obtiene un array de personas pertenecientes a una autoridad ambiental relavionadas con el numero vital
        /// </summary>
        /// <param name="intAutoridadAmbientalID"></param>
        /// <param name="personaID"></param>     
        /// <returns></returns>
        public List<PersonaIdentity> ObtenerPersonasByAutoridadAmbiental(int intAutoridadAmbientalID, int personaID)
        {
            List<PersonaIdentity> _listaPersona = new List<PersonaIdentity>();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intAutoridadAmbientalID, personaID };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_NUMERO_VITAL_X_PERSONA", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            TipoPersonaIdentity tmpTipoPerI = new TipoPersonaIdentity();
            SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity tmpTipoIdI = new SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity();
            TipoIdentificacionDalc tmpTipoIdD;
            TipoPersonaDalc tmpTipoPerD;

            foreach (DataRow drResultado in dsResultado.Tables[0].Rows)
            {
                PersonaIdentity objEntity = new PersonaIdentity();
                objEntity.PersonaId = Convert.ToInt32(drResultado["PER_ID"]);
                objEntity.PrimerNombre = Convert.ToString(drResultado["PER_PRIMER_NOMBRE"]);
                objEntity.SegundoNombre = Convert.ToString(drResultado["PER_SEGUNDO_NOMBRE"]);
                objEntity.PrimerApellido = Convert.ToString(drResultado["PER_PRIMER_APELLIDO"]);
                objEntity.SegundoApellido = Convert.ToString(drResultado["PER_SEGUNDO_APELLIDO"]);
                objEntity.NumeroIdentificacion = Convert.ToString(drResultado["PER_NUMERO_IDENTIFICACION"]);
                tmpTipoIdI = new SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity();
                tmpTipoIdI.Id = Convert.ToInt32(drResultado["TID_ID"]);
                tmpTipoIdD = new TipoIdentificacionDalc();
                tmpTipoIdD.ObtenerTipoIdentificacion(ref tmpTipoIdI);

                objEntity.TipoDocumentoIdentificacion = tmpTipoIdI;
                objEntity.LugarExpediciónDocumento = Convert.ToString(drResultado["PER_LUGAR_EXPEDICION_DOC"]);
                //objIdentity.Pais = Convert.ToInt32(drResultado["PAI_ID"]);
                objEntity.Telefono = Convert.ToString(drResultado["PER_TELEFONO"]);
                objEntity.Celular = Convert.ToString(drResultado["PER_CELULAR"]);
                objEntity.Fax = Convert.ToString(drResultado["PER_FAX"]);
                objEntity.CorreoElectronico = Convert.ToString(drResultado["PER_CORREO_ELECTRONICO"]);
                objEntity.IdApplicationUser = Convert.ToInt32(drResultado["ID_APPLICATION_USER"]);
                objEntity.RazonSocial = Convert.ToString(drResultado["PER_RAZON_SOCIAL"]);

                tmpTipoPerI.CodigoTipoPersona = Convert.ToInt32(drResultado["PER_TIPO_PERSONA"]);
                tmpTipoPerD = new TipoPersonaDalc(ref tmpTipoPerI);
                objEntity.TipoPersona = tmpTipoPerI;

                objEntity.TarjetaProfesional = Convert.ToString(drResultado["PER_TARJETA_PROFESIONAL"]);
                objEntity.Pregunta = drResultado["PER_PREGUNTA"].ToString();
                objEntity.Respuesta = drResultado["PER_RESPUESTA"].ToString();
                objEntity.Activo = Convert.ToBoolean(drResultado["PER_ACTIVO"]);
                objEntity.NumeroVital = drResultado["SOL_NUMERO_SILPA"].ToString();
                objEntity.RutaRtf = drResultado["numero_vital_tramite"].ToString();
                _listaPersona.Add(objEntity);
            }

            return _listaPersona;
        }


        /// <summary>
        /// HAVA: 22-sep-2010
        /// Obtiene lista de objetos PersonavitalCesion pertenecientes a una autoridad ambiental relacionadas con el numero vital
        /// </summary>
        /// <param name="intAutoridadAmbientalID">int: identificador del numero vital</param>
        /// <param name="personaID">Int: Indentificador de la persona en silamc_mavdt.dbo.Persona.per_id </param>     
        /// <returns>List<PersonaCesionIdentity></returns>
        public List<PersonaIdentity> ObtenerNumeroVitalCesionPersona(int intAutoridadAmbientalID, int personaID)
        {
            List<PersonaIdentity> _listaPersona = new List<PersonaIdentity>();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intAutoridadAmbientalID, personaID };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_NUMERO_VITAL_X_PERSONA", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            TipoPersonaIdentity tmpTipoPerI = new TipoPersonaIdentity();
            SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity tmpTipoIdI = new SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity();
            TipoIdentificacionDalc tmpTipoIdD;
            TipoPersonaDalc tmpTipoPerD;

            foreach (DataRow drResultado in dsResultado.Tables[0].Rows)
            {
                PersonaIdentity objEntity = new PersonaIdentity();
                objEntity.PersonaId = Convert.ToInt32(drResultado["PER_ID"]);
                objEntity.PrimerNombre = Convert.ToString(drResultado["PER_PRIMER_NOMBRE"]);
                objEntity.SegundoNombre = Convert.ToString(drResultado["PER_SEGUNDO_NOMBRE"]);
                objEntity.PrimerApellido = Convert.ToString(drResultado["PER_PRIMER_APELLIDO"]);
                objEntity.SegundoApellido = Convert.ToString(drResultado["PER_SEGUNDO_APELLIDO"]);
                objEntity.NumeroIdentificacion = Convert.ToString(drResultado["PER_NUMERO_IDENTIFICACION"]);
                tmpTipoIdI = new SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity();
                tmpTipoIdI.Id = Convert.ToInt32(drResultado["TID_ID"]);
                tmpTipoIdD = new TipoIdentificacionDalc();
                tmpTipoIdD.ObtenerTipoIdentificacion(ref tmpTipoIdI);

                objEntity.TipoDocumentoIdentificacion = tmpTipoIdI;
                objEntity.LugarExpediciónDocumento = Convert.ToString(drResultado["PER_LUGAR_EXPEDICION_DOC"]);
                //objIdentity.Pais = Convert.ToInt32(drResultado["PAI_ID"]);
                objEntity.Telefono = Convert.ToString(drResultado["PER_TELEFONO"]);
                objEntity.Celular = Convert.ToString(drResultado["PER_CELULAR"]);
                objEntity.Fax = Convert.ToString(drResultado["PER_FAX"]);
                objEntity.CorreoElectronico = Convert.ToString(drResultado["PER_CORREO_ELECTRONICO"]);
                objEntity.IdApplicationUser = Convert.ToInt32(drResultado["ID_APPLICATION_USER"]);
                objEntity.RazonSocial = Convert.ToString(drResultado["PER_RAZON_SOCIAL"]);

                tmpTipoPerI.CodigoTipoPersona = Convert.ToInt32(drResultado["PER_TIPO_PERSONA"]);
                tmpTipoPerD = new TipoPersonaDalc(ref tmpTipoPerI);
                objEntity.TipoPersona = tmpTipoPerI;

                objEntity.TarjetaProfesional = Convert.ToString(drResultado["PER_TARJETA_PROFESIONAL"]);
                objEntity.Pregunta = drResultado["PER_PREGUNTA"].ToString();
                objEntity.Respuesta = drResultado["PER_PREGUNTA"].ToString();
                objEntity.Activo = Convert.ToBoolean(drResultado["PER_ACTIVO"]);
                objEntity.NumeroVital = drResultado["SOL_NUMERO_SILPA"].ToString();
                _listaPersona.Add(objEntity);
            }

            return _listaPersona;
        }


        
        /// <summary>
        /// Constructor de la clase que a su vez carga los valores para una identidad de una persona
        /// cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador de las personas cargar, en la propiedad ID del objetoIdentity</param>
        public void ObtenerPersona(ref PersonaIdentity objIdentity, bool p_blnDetalleInformacion = true)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { objIdentity.PersonaId, objIdentity.IdApplicationUser, objIdentity.TipoPersona.CodigoTipoPersona };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_PERSONA", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            //hava:20-abr-10
            // corrección 
            if (dsResultado != null) 
            {
                if ( dsResultado.Tables.Count>0 )
                {
                    if (dsResultado.Tables[0].Rows.Count > 0)
                    {

                        TipoPersonaIdentity tmpTipoPerI = new TipoPersonaIdentity();
                        SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity tmpTipoIdI = new SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity();
                        TipoIdentificacionDalc tmpTipoIdD;
                        TipoPersonaDalc tmpTipoPerD;

                        objIdentity.PersonaId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PER_ID"]);
                        objIdentity.PrimerNombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_PRIMER_NOMBRE"]);
                        objIdentity.SegundoNombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_SEGUNDO_NOMBRE"]);
                        objIdentity.PrimerApellido = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_PRIMER_APELLIDO"]);
                        objIdentity.SegundoApellido = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_SEGUNDO_APELLIDO"]);
                        objIdentity.NumeroIdentificacion = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_NUMERO_IDENTIFICACION"]);
                        objIdentity.RazonSocial = (dsResultado.Tables[0].Rows[0]["PER_RAZON_SOCIAL"] != System.DBNull.Value ? dsResultado.Tables[0].Rows[0]["PER_RAZON_SOCIAL"].ToString() : "");

                        objIdentity.LugarExpediciónDocumento = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_LUGAR_EXPEDICION_DOC"]);
                        objIdentity.Telefono = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_TELEFONO"]);
                        objIdentity.Celular = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_CELULAR"]);
                        objIdentity.Fax = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_FAX"]);
                        objIdentity.CorreoElectronico = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_CORREO_ELECTRONICO"]);
                        objIdentity.IdApplicationUser = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ID_APPLICATION_USER"]);
                        objIdentity.IdSolicitante = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ID_APPLICATION_USER"]);

                        objIdentity.TarjetaProfesional = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_TARJETA_PROFESIONAL"]);
                        objIdentity.Pregunta = dsResultado.Tables[0].Rows[0]["PER_PREGUNTA"].ToString();
                        objIdentity.Respuesta = dsResultado.Tables[0].Rows[0]["PER_PREGUNTA"].ToString();
                        objIdentity.Activo = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["PER_ACTIVO"]);
                        objIdentity.IdAutoridadAmbiental = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["AUT_ID"]);
                        objIdentity.AutorizaCorreo = (dsResultado.Tables[0].Rows[0]["AUTORIZA_NOTICACION"] != System.DBNull.Value ? Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["AUTORIZA_NOTICACION"]) : false);

                        if (p_blnDetalleInformacion)
                        {
                            tmpTipoPerI.CodigoTipoPersona = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PER_TIPO_PERSONA"]);
                            tmpTipoPerD = new TipoPersonaDalc(ref tmpTipoPerI);
                            objIdentity.TipoPersona = tmpTipoPerI;

                            tmpTipoIdI.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TID_ID"]);
                            tmpTipoIdD = new TipoIdentificacionDalc();
                            tmpTipoIdD.ObtenerTipoIdentificacion(ref tmpTipoIdI);
                            objIdentity.TipoDocumentoIdentificacion = tmpTipoIdI;
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Lista todos los tipo de ubicación en la BD o uno en particular.
        /// </summary>
        /// <param name="objIdentity.Id" >Con este valor se lista las personas con un identificador determinado, si es null no existen restricciones</param>
        /// <returns>DataSet con los registros y las siguientes columnas: PER_ID, PER_PRIMER_NOMBRE, PER_SEGUNDO_NOMBRE, PER_PRIMER_APELLIDO,
        ///         PER_SEGUNDO_APELLIDO, PER_NUMERO_IDENTIFICACION, TID_ID, PER_LUGAR_EXPEDICION_DOC, 
        ///         PAI_ID, PER_TELEFONO, PER_CELULAR, PER_FAX, PER_CORREO_ELECTRONICO,
        ///         ID_APPLICATION_USER, PER_RAZON_SOCIAL, PER_TIPO_PERSONA, PER_OTRO,
        ///         PER_TARJETA_PROFESIONAL, PER_ID_SOLICITANTE, PER_CALIDAD_ACTUA,  PER_PREGUNTA,  PER_RESPUESTA,PER_ACTIVO</returns>
        public DataSet ListarPersona(PersonaIdentity objIdentity)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { objIdentity.PersonaId, objIdentity.IdApplicationUser };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_PERSONA", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return (dsResultado);
        }

        /// <summary>
        /// Lista todas las personas en la BD o uno en particular.
        /// </summary>
        /// <param name="int64IdPersona" >valor del id de la persona en SILPA, si es null no existen restricciones</param>
        /// <param name="Int64IdAppUser" >valor del id de la persona en Gattaca, si es null no existen restricciones</param>
        /// <returns>DataSet con los registros y las siguientes columnas: PER_ID, PER_PRIMER_NOMBRE, PER_SEGUNDO_NOMBRE, PER_PRIMER_APELLIDO,
        ///         PER_SEGUNDO_APELLIDO, PER_NUMERO_IDENTIFICACION, TID_ID, PER_LUGAR_EXPEDICION_DOC, 
        ///         PAI_ID, PER_TELEFONO, PER_CELULAR, PER_FAX, PER_CORREO_ELECTRONICO,
        ///         ID_APPLICATION_USER, PER_RAZON_SOCIAL, PER_TIPO_PERSONA, PER_OTRO,
        ///         PER_TARJETA_PROFESIONAL, PER_ID_SOLICITANTE, PER_CALIDAD_ACTUA, PER_PREGUNTA,  PER_RESPUESTA, PER_ACTIVO</returns>
        public DataSet ListarPersona(Nullable<Int64> int64IdPersona, Nullable<Int64> Int64IdAppUser)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { int64IdPersona, Int64IdAppUser };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_PERSONA", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return (dsResultado);


        }

        /// <summary>
        /// Inserta un registro en la BD del BMP para el usuario
        /// </summary>
        /// <param name="Code">Cedula</param>
        /// <param name="Name">UserName</param>
        /// <param name="Firstname">Pirmer nombre</param>
        /// <param name="Middlename">Segundo nombre</param>
        /// <param name="Lastname">Apellido</param>
        /// <param name="Idlocality">1</param>
        /// <param name="Positionname">Tipo persona</param>
        /// <param name="Identification">Identificaciòn</param>
        /// <param name="Email">eMail</param>
        /// <param name="Expiration">Dias en los que expira la cuenta</param>
        /// <param name="Enabled">T ò F </param>
        /// <param name="Active">T ò F</param>
        /// <param name="Imageuser">NoImage.gif</param>
        /// <param name="Changepassword">T ò F</param>
        /// <returns>Identificador en la tabla applicationuser</returns>
        public int InsertarPersonaBPM(string Code, string Name, string Password, string Firstname, string Middlename, string Lastname, int Idlocality, string Positionname,
                            string Identification, string Email, int Expiration, string Enabled, string Active, string Imageuser, string Changepassword)
        {

            int _intId = 0;
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

            object[] parametros = new object[] { _intId,
                                                    Code,
                                                    Name,
                                                    Password,
                                                    Firstname,	
                                                    Middlename,
                                                    Lastname, 
                                                    Idlocality, 
                                                    Positionname,
                                                    Identification, 
                                                    Email, 
                                                    Expiration,
                                                    Enabled, 
                                                    Active, 
                                                    Imageuser,
                                                    Changepassword
                };

            DbCommand cmd = db.GetStoredProcCommand("BPM_CREAR_USUARIO", parametros);
            db.ExecuteDataSet(cmd);
            return int.Parse(cmd.Parameters["@ID"].Value.ToString());

        }
        
        /// <summary>
        /// Modifica un registro en particular en la BD del BMP para el usuario
        /// </summary>
        /// <param name="Id">Identificador</param>
        /// <param name="Code">Cedula</param>
        /// <param name="Name">UserName</param>
        /// <param name="Firstname">Pirmer nombre</param>
        /// <param name="Middlename">Segundo nombre</param>
        /// <param name="Lastname">Apellido</param>
        /// <param name="Idlocality">1</param>
        /// <param name="Positionname">Tipo persona</param>
        /// <param name="Identification">Identificaciòn</param>
        /// <param name="Email">eMail</param>
        /// <param name="Expiration">Dias en los que expira la cuenta</param>
        /// <param name="Enabled">T ò F </param>
        /// <param name="Active">T ò F</param>
        /// <param name="Imageuser">NoImage.gif</param>
        /// <param name="Changepassword">T ò F</param>
        public void ActualizarPersonaBPM(int Id, string Code, string Name, string Password, string Firstname, string Middlename, string Lastname, int Idlocality, string Positionname,
                            string Identification, string Email, int Expiration, string Enabled, string Active, string Imageuser, string Changepassword)
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

            object[] parametros = new object[] { 
                                                    Id,
                                                    Code,
                                                    Name,
                                                    Password,
                                                    Firstname,	
                                                    Middlename, 
                                                    Lastname,
                                                    Idlocality, 
                                                    Positionname,
                                                    Identification,
                                                    Email, 
                                                    Expiration, 
                                                    Enabled,
                                                    Active, 
                                                    Imageuser, 
                                                    Changepassword
                                                    };

            DbCommand cmd = db.GetStoredProcCommand("BPM_UPDATE_USUARIO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

        }

        /// <summary>
        /// asigna valores a una instacia del identity de persona con los valores de la persona que tiene asociado un nuemro silpa
        /// </summary>
        /// <param name="strNumSilpa">Numero silpa</param>
        /// <param name="objIdentity">referencia a la instacia del identity de persona que se va a llenar</param>
        public void ObternerPersonaByNumeroSilpa(string strNumSilpa, ref PersonaIdentity objIdentity)
        {
            objConfiguracion = new Configuracion();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { strNumSilpa };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_PERSONA_NUMERO_VITAL", parametros);
            
            TipoPersonaIdentity tmpTipoPerI = new TipoPersonaIdentity();
            SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity tmpTipoIdI = new SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity();
            TipoIdentificacionDalc tmpTipoIdD;
            TipoPersonaDalc tmpTipoPerD;
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    objIdentity.PersonaId = Convert.ToInt32(reader["PER_ID"]);
                    objIdentity.PrimerNombre = Convert.ToString(reader["PER_PRIMER_NOMBRE"]);
                    objIdentity.SegundoNombre = Convert.ToString(reader["PER_SEGUNDO_NOMBRE"]);
                    objIdentity.PrimerApellido = Convert.ToString(reader["PER_PRIMER_APELLIDO"]);
                    objIdentity.SegundoApellido = Convert.ToString(reader["PER_SEGUNDO_APELLIDO"]);
                    objIdentity.NumeroIdentificacion = Convert.ToString(reader["PER_NUMERO_IDENTIFICACION"]);

                    tmpTipoIdI.Id = Convert.ToInt32(reader["TID_ID"]);
                    tmpTipoIdD = new TipoIdentificacionDalc(ref tmpTipoIdI);
                    objIdentity.TipoDocumentoIdentificacion = tmpTipoIdI;

                    objIdentity.LugarExpediciónDocumento = Convert.ToString(reader["PER_LUGAR_EXPEDICION_DOC"]);
                    if (reader["PAI_ID"].ToString() != string.Empty)
                        objIdentity.Pais = Convert.ToInt32(reader["PAI_ID"]);
                    objIdentity.Telefono = Convert.ToString(reader["PER_TELEFONO"]);
                    objIdentity.Celular = Convert.ToString(reader["PER_CELULAR"]);
                    objIdentity.Fax = Convert.ToString(reader["PER_FAX"]);
                    objIdentity.CorreoElectronico = Convert.ToString(reader["PER_CORREO_ELECTRONICO"]);
                    objIdentity.IdApplicationUser = Convert.ToInt32(reader["ID_APPLICATION_USER"]);
                    objIdentity.RazonSocial = Convert.ToString(reader["PER_RAZON_SOCIAL"]);

                    tmpTipoPerI.CodigoTipoPersona = Convert.ToInt32(reader["PER_TIPO_PERSONA"]);
                    tmpTipoPerD = new TipoPersonaDalc(ref tmpTipoPerI);
                    objIdentity.TipoPersona = tmpTipoPerI;

                    objIdentity.Pregunta = reader["PER_PREGUNTA"].ToString();
                    objIdentity.Respuesta = reader["PER_PREGUNTA"].ToString();
                    objIdentity.Activo = Convert.ToBoolean(reader["PER_ACTIVO"]);
                    //objIdentity.Otro = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_OTRO"]);
                    objIdentity.TarjetaProfesional = Convert.ToString(reader["PER_TARJETA_PROFESIONAL"]);
                    //objIdentity. = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_ID_SOLICITANTE"]);
                    objIdentity.IdAutoridadAmbiental = Convert.ToInt32(reader["AUT_ID"]);
                    //tmpTipoPerI.CodigoTipoPersona = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PER_CALIDAD_ACTUA"]);
                    /*tmpTipoPerD = new TipoPersonaDalc(ref tmpTipoPerI);
                    objIdentity.CalidadActua = tmpTipoPerI;*/
                }
            }
        }

        public void ObtenerPersonaNotificacion(long idpersona, ref PersonaIdentity objIdentity)
        {
            objConfiguracion = new Configuracion();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { idpersona };
            DbCommand cmd = db.GetStoredProcCommand("BAS_CONSULTA_PERSONA_NOTIFICAR", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            TipoPersonaIdentity tmpTipoPerI = new TipoPersonaIdentity();
            SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity tmpTipoIdI = new SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity();
            TipoIdentificacionDalc tmpTipoIdD;
            TipoPersonaDalc tmpTipoPerD;

            objIdentity.PersonaId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PER_ID"]);
            objIdentity.PrimerNombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_PRIMER_NOMBRE"]);
            objIdentity.SegundoNombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_SEGUNDO_NOMBRE"]);
            objIdentity.PrimerApellido = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_PRIMER_APELLIDO"]);
            objIdentity.SegundoApellido = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_SEGUNDO_APELLIDO"]);
            objIdentity.NumeroIdentificacion = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_NUMERO_IDENTIFICACION"]);

            tmpTipoIdI.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TID_ID"]);
            tmpTipoIdD = new TipoIdentificacionDalc(ref tmpTipoIdI);
            objIdentity.TipoDocumentoIdentificacion = tmpTipoIdI;

            objIdentity.LugarExpediciónDocumento = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_LUGAR_EXPEDICION_DOC"]);
            objIdentity.Pais = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PAI_ID"]);
            objIdentity.Telefono = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_TELEFONO"]);
            objIdentity.Celular = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_CELULAR"]);
            objIdentity.Fax = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_FAX"]);
            objIdentity.CorreoElectronico = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_CORREO_ELECTRONICO"]);
            objIdentity.IdApplicationUser = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ID_APPLICATION_USER"]);
            objIdentity.RazonSocial = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_RAZON_SOCIAL"]);

            tmpTipoPerI.CodigoTipoPersona = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PER_TIPO_PERSONA"]);
            tmpTipoPerD = new TipoPersonaDalc(ref tmpTipoPerI);
            objIdentity.TipoPersona = tmpTipoPerI;

            objIdentity.Pregunta = dsResultado.Tables[0].Rows[0]["PER_PREGUNTA"].ToString();
            objIdentity.Respuesta = dsResultado.Tables[0].Rows[0]["PER_PREGUNTA"].ToString();
            objIdentity.Activo = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["PER_ACTIVO"]);
            //objIdentity.Otro = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_OTRO"]);
            objIdentity.TarjetaProfesional = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_TARJETA_PROFESIONAL"]);
            //objIdentity. = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_ID_SOLICITANTE"]);
            objIdentity.IdAutoridadAmbiental = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["AUT_ID"]);
            //tmpTipoPerI.CodigoTipoPersona = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PER_CALIDAD_ACTUA"]);
            /*tmpTipoPerD = new TipoPersonaDalc(ref tmpTipoPerI);
            objIdentity.CalidadActua = tmpTipoPerI;*/
        }

        /// <summary>
        /// Crea una instacia del identity de persona con los valores de la persona que tiene asociado un nuemro silpa
        /// </summary>
        /// <param name="intProcessInstance">Indentificador del proceso</param>
        /// <param name="objIdentity">referencia a la instancia del indentity persona</param>
        public void ObternerPersonaByProcessInstace(int intProcessInstance, ref PersonaIdentity objIdentity)
        {
            objConfiguracion = new Configuracion();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intProcessInstance };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_PERSONA_PROCESS_INSTANCE", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            TipoPersonaIdentity tmpTipoPerI = new TipoPersonaIdentity();
            SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity tmpTipoIdI = new SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity();
            TipoIdentificacionDalc tmpTipoIdD;
            TipoPersonaDalc tmpTipoPerD;

            objIdentity.PersonaId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PER_ID"]);
            objIdentity.PrimerNombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_PRIMER_NOMBRE"]);
            objIdentity.SegundoNombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_SEGUNDO_NOMBRE"]);
            objIdentity.PrimerApellido = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_PRIMER_APELLIDO"]);
            objIdentity.SegundoApellido = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_SEGUNDO_APELLIDO"]);
            objIdentity.NumeroIdentificacion = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_NUMERO_IDENTIFICACION"]);

            tmpTipoIdI.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TID_ID"]);
            tmpTipoIdD = new Generico.TipoIdentificacionDalc(ref tmpTipoIdI);
            objIdentity.TipoDocumentoIdentificacion = tmpTipoIdI;
            
            SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity tipoIdentificacion= new SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity();
            tipoIdentificacion.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TID_ID"]);
            tipoIdentificacion.Nombre = dsResultado.Tables[0].Rows[0]["TID_NOMBRE"].ToString();

            objIdentity.TipoDocumentoIdentificacion = tipoIdentificacion;

            objIdentity.LugarExpediciónDocumento = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_LUGAR_EXPEDICION_DOC"]);
            //objIdentity.Pais = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PAI_ID"]);
            objIdentity.Telefono = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_TELEFONO"]);
            objIdentity.Celular = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_CELULAR"]);
            objIdentity.Fax = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_FAX"]);
            objIdentity.CorreoElectronico = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_CORREO_ELECTRONICO"]);
            objIdentity.IdApplicationUser = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ID_APPLICATION_USER"]);
            objIdentity.RazonSocial = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_RAZON_SOCIAL"]);

            tmpTipoPerI.CodigoTipoPersona = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PER_TIPO_PERSONA"]);
            tmpTipoPerD = new TipoPersonaDalc(ref tmpTipoPerI);
            objIdentity.TipoPersona = tmpTipoPerI;

            objIdentity.Pregunta = dsResultado.Tables[0].Rows[0]["PER_PREGUNTA"].ToString();
            objIdentity.Respuesta = dsResultado.Tables[0].Rows[0]["PER_PREGUNTA"].ToString();
            objIdentity.Activo = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["PER_ACTIVO"]);
            //objIdentity.Otro = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_OTRO"]);
            objIdentity.TarjetaProfesional = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_TARJETA_PROFESIONAL"]);
            objIdentity.IdAutoridadAmbiental = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["AUT_ID"]);
            //objIdentity. = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_ID_SOLICITANTE"]);

            //tmpTipoPerI.CodigoTipoPersona = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PER_CALIDAD_ACTUA"]);
            /*tmpTipoPerD = new TipoPersonaDalc(ref tmpTipoPerI);
            objIdentity.CalidadActua = tmpTipoPerI;*/

        }
        
        public int CambiarClave(int intIdAppUser, string strClave, string strCambioClave)
        {
            objConfiguracion = new Configuracion();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intIdAppUser, strClave, strCambioClave };
            DbCommand cmd = db.GetStoredProcCommand("BPM_CAMBIAR_CLAVE_USUARIO", parametros);
            return db.ExecuteNonQuery(cmd);
        }


        /// <summary>
        /// Realizar el cambio de la segunda clave del objeto especificado
        /// </summary>
        /// <param name="intUsuarioID">int con el id del usuario al cual se le realiza la modificación de la clave</param>
        /// <param name="strClaveAnterior">string que contiene la nueva contraseña</param>
        /// <param name="strClaveNueva">string con la Nueva clave</param>
        /// <returns>int con el resultado de la actualizacion</returns>
        public int CambiarSegundaClave(int intUsuarioID, string strClaveAnterior, string strClaveNueva)
        {
            objConfiguracion = new Configuracion();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intUsuarioID, strClaveAnterior, strClaveNueva, 0 };
            DbCommand cmd = db.GetStoredProcCommand("BPM_CAMBIAR_SEGUNDA_CLAVE_USUARIO", parametros);
            db.ExecuteNonQuery(cmd);

            return (int)db.GetParameterValue(cmd, "@P_Resultado");
        }


        /// <summary>
        /// Verificar si el usuario tiene segunda clave
        /// </summary>
        /// <param name="intUsuarioID">int con el id del usuario</param>
        /// <returns>bool indicando si tiene o no segunda clave</returns>
        public bool TieneSegundaClave(int intUsuarioID)
        {
            objConfiguracion = new Configuracion();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intUsuarioID, 0 };
            DbCommand cmd = db.GetStoredProcCommand("BPM_USUARIO_TIENE_SEGUNDA_CLAVE", parametros);
            db.ExecuteNonQuery(cmd);

            return (bool)db.GetParameterValue(cmd, "@P_TieneClave");
        }


        /// <summary>
        /// Verificar si la segunda clave es valida
        /// </summary>
        /// <param name="intUsuarioID">int con el id del usuario</param>
        /// <param name="strClave">string con la clave a validar</param>
        /// <returns>bool indicando si la segunda clave es valida</returns>
        public bool ValidarSegundaClave(int intUsuarioID, string strClave)
        {
            objConfiguracion = new Configuracion();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intUsuarioID, strClave, 0 };
            DbCommand cmd = db.GetStoredProcCommand("BPM_VALIDA_SEGUNDA_CLAVE", parametros);
            db.ExecuteNonQuery(cmd);

            return (bool)db.GetParameterValue(cmd, "@P_ClaveValida");
        }


        /// <summary>
        /// consultar la información de la firma de un usuario
        /// </summary>
        /// <param name="intUsuarioID">int con el id del usuario</param>
        /// <returns>PersonaFirmaIdentity con la informacion de la firma del usuario</returns>
        public PersonaFirmaIdentity ConsultarFirma(int intUsuarioID)
        {
            PersonaFirmaIdentity objFirma = null;
            objConfiguracion = new Configuracion();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intUsuarioID };
            DbCommand cmd = db.GetStoredProcCommand("BPM_OBTENER_FIRMA_USUARIO", parametros);

            using(IDataReader objReader = db.ExecuteReader(cmd))
            {
                //verificar que se hallan encontrado resultados
                if (objReader != null && objReader.Read())
                {
                    objFirma = new PersonaFirmaIdentity();
                    objFirma.UsuarioID = intUsuarioID;
                    objFirma.Nombre = objReader["SignatureName"].ToString();
                    objFirma.Cargo = objReader["SignaturePositionName"].ToString();
                    objFirma.Imagen = (byte[])objReader["SignatureImage"];
                    objFirma.NombreImagen = objReader["SignatureImageName"].ToString();
                    objFirma.TipoImagen = objReader["SignatureImageType"].ToString();
                    objFirma.LongitudImagen = Convert.ToInt32( objReader["SignatureImageLength"] );
                    objFirma.Nit = objReader["Identification"].ToString();
                }
            }

            return objFirma;
        }


        /// <summary>
        /// Realizar el cambio de la información de la firma del usuario
        /// </summary>
        /// <param name="objFirmaPersona">PersonaFirmaIdentity con la información de la firma del usuario</param>
        public void CambiarFirma(PersonaFirmaIdentity objFirmaPersona)
        {
            objConfiguracion = new Configuracion();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("BPM_CAMBIAR_FIRMA_USUARIO");
            db.AddInParameter(cmd, "@P_IdAplicationUser", DbType.Int32, objFirmaPersona.UsuarioID);
            if (!string.IsNullOrEmpty(objFirmaPersona.Nombre))
                db.AddInParameter(cmd, "@P_SignatureName", DbType.String, objFirmaPersona.Nombre);
            db.AddInParameter(cmd, "@P_SignaturePositionName", DbType.String, objFirmaPersona.Cargo);
            if (objFirmaPersona != null)
            {
                db.AddInParameter(cmd, "@P_SignatureImage", DbType.Binary, objFirmaPersona.Imagen);
                db.AddInParameter(cmd, "@P_SignatureImageName", DbType.String, objFirmaPersona.NombreImagen);
                db.AddInParameter(cmd, "@P_SignatureImageType", DbType.String, objFirmaPersona.TipoImagen);
                db.AddInParameter(cmd, "@P_SignatureImageLength", DbType.Int32, objFirmaPersona.LongitudImagen);
            }
            else
            {
                db.AddInParameter(cmd, "@P_SignatureImage", DbType.Binary, DBNull.Value);
                db.AddInParameter(cmd, "@P_SignatureImageName", DbType.String, DBNull.Value);
                db.AddInParameter(cmd, "@P_SignatureImageType", DbType.String, DBNull.Value);
                db.AddInParameter(cmd, "@P_SignatureImageLength", DbType.Int32, DBNull.Value);
            }

            db.ExecuteNonQuery(cmd);
        }


        /// <summary>
        /// Consultar el logo de un usuario
        /// </summary>
        /// <param name="intUsuarioID">int con el id del usuario</param>
        /// <returns>PersonaLogoIdentity con la informacion del logo del usuario</returns>
        public PersonaLogoIdentity ConsultarLogo(int intUsuarioID)
        {
            PersonaLogoIdentity objLogo = null;
            objConfiguracion = new Configuracion();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intUsuarioID };
            DbCommand cmd = db.GetStoredProcCommand("BPM_OBTENER_LOGO_USUARIO", parametros);

            using (IDataReader objReader = db.ExecuteReader(cmd))
            {
                //verificar que se hallan encontrado resultados
                if (objReader != null && objReader.Read())
                {
                    objLogo = new PersonaLogoIdentity();
                    objLogo.UsuarioID = intUsuarioID;
                    objLogo.Logo = (byte[])objReader["LogoImage"];
                    objLogo.NombreLogo = objReader["LogoImageName"].ToString();
                    objLogo.TipoLogo = objReader["LogoImageType"].ToString();
                    objLogo.LongitudLogo = Convert.ToInt32(objReader["LogoImageLength"]);
                }
            }

            return objLogo;
        }


        /// <summary>
        /// Realizar el cambio de la información del logo de un usuario
        /// </summary>
        /// <param name="objFirmaPersona">PersonaLogoIdentity con la información del logo del usuario</param>
        public void CambiarLogo(PersonaLogoIdentity objLogoPersona)
        {
            objConfiguracion = new Configuracion();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            DbCommand cmd = db.GetStoredProcCommand("BPM_CAMBIAR_LOGO_USUARIO");
            db.AddInParameter(cmd, "@P_IdAplicationUser", DbType.Int32, objLogoPersona.UsuarioID);
            db.AddInParameter(cmd, "@P_LogoImage", DbType.Binary, objLogoPersona.Logo);
            db.AddInParameter(cmd, "@P_LogoImageName", DbType.String, objLogoPersona.NombreLogo);
            db.AddInParameter(cmd, "@P_LogoImageType", DbType.String, objLogoPersona.TipoLogo);
            db.AddInParameter(cmd, "@P_LogoImageLength", DbType.Int32, objLogoPersona.LongitudLogo);

            db.ExecuteNonQuery(cmd);
        }


        /// <summary>
        /// Verificar si el usuario tiene firma registrada
        /// </summary>
        /// <param name="intUsuarioID">int con el id del usuario</param>
        /// <returns>bool indicando si tiene o no firma registrada</returns>
        public bool TieneFirma(int intUsuarioID)
        {
            objConfiguracion = new Configuracion();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { intUsuarioID, 0 };
            DbCommand cmd = db.GetStoredProcCommand("BPM_USUARIO_TIENE_FIRMA", parametros);
            db.ExecuteNonQuery(cmd);

            return (bool)db.GetParameterValue(cmd, "@P_TieneFirma");
        }

        /// <summary>
        /// HAVA:21-jun-10
        /// Método que determina si el correo para solicitud de credenciales de solicitante ya existe.
        /// </summary>
        /// <returns>bool: true/ false</returns>
        public bool ExisteCorreo(string strEmail) 
        {
            objConfiguracion = new Configuracion();

            bool result = false;

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { strEmail, 0 };
            DbCommand cmd = db.GetStoredProcCommand("GEN_EXISTE_CORREO_PERSONA", parametros);
            db.ExecuteNonQuery(cmd);

            result = (bool)db.GetParameterValue(cmd, "Existe");
            
            return result;
        }

        /// <summary>
        /// JMM - 16/07/2010
        /// Método que determina si el correo para solicitud de credenciales de solicitante ya existe teniendo en cuenta el el unico q debe
        /// existir es el correo de la persona a la cual se le esta realizando la actualizacion.
        /// </summary>
        /// <returns>bool: true/ false</returns>
        public bool ExisteCorreoSOL(Int64 iIdPersona, string strEmail)
        {
            objConfiguracion = new Configuracion();

            bool result = false;

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { iIdPersona, strEmail, 0 };
            DbCommand cmd = db.GetStoredProcCommand("GEN_EXISTE_CORREO_PERSONA_SOL", parametros);
            db.ExecuteNonQuery(cmd);

            result = (bool)db.GetParameterValue(cmd, "Existe");

            return result;
        }


        /// <summary>
        /// 11-08-2010 - aegb: CU Emitir Documento Manual
        /// Constructor de la clase que a su vez carga los valores para una identidad de las personas
        /// </summary>
        /// <param name="usuario">username de la persona</param>
        public PersonaIdentity ConsultarPersona(string usuario)
        {
            SILPA.AccesoDatos.Usuario.UsuarioDalc usuarioDalc = new SILPA.AccesoDatos.Usuario.UsuarioDalc();
            DataSet dsResultado = usuarioDalc.ConsultarUsuarioCompania(usuario);
            PersonaIdentity objIdentity = new PersonaIdentity();
            TipoPersonaIdentity tmpTipoPerI = new TipoPersonaIdentity();
            SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity tmpTipoIdI = new SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity();
            TipoIdentificacionDalc tmpTipoIdD;
            TipoPersonaDalc tmpTipoPerD;
            TipoPersonaIdentity tmpTipoSolI = new TipoPersonaIdentity();
            objIdentity.PersonaId = 0;
            if (dsResultado.Tables[0].Rows.Count > 0)
            {
                objIdentity.PersonaId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PER_ID"]);
                objIdentity.PrimerNombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_PRIMER_NOMBRE"]);
                objIdentity.SegundoNombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_SEGUNDO_NOMBRE"]);
                objIdentity.PrimerApellido = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_PRIMER_APELLIDO"]);
                objIdentity.SegundoApellido = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_SEGUNDO_APELLIDO"]);
                objIdentity.NumeroIdentificacion = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_NUMERO_IDENTIFICACION"]);
                tmpTipoIdI.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TID_ID"]);
                tmpTipoIdD = new TipoIdentificacionDalc();
                tmpTipoIdD.ObtenerTipoIdentificacion(ref tmpTipoIdI);
                objIdentity.TipoDocumentoIdentificacion = tmpTipoIdI;
                objIdentity.LugarExpediciónDocumento = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_LUGAR_EXPEDICION_DOC"]);
                //objIdentity.Pais = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PAI_ID"]);
                objIdentity.Telefono = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_TELEFONO"]);
                objIdentity.Celular = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_CELULAR"]);
                objIdentity.Fax = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_FAX"]);
                objIdentity.CorreoElectronico = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_CORREO_ELECTRONICO"]);
                objIdentity.IdApplicationUser = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ID_APPLICATION_USER"]);
                objIdentity.RazonSocial = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_RAZON_SOCIAL"]);

                tmpTipoPerI.CodigoTipoPersona = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PER_TIPO_PERSONA"]);
                tmpTipoPerD = new TipoPersonaDalc(ref tmpTipoPerI);
                objIdentity.TipoPersona = tmpTipoPerI;

                if (dsResultado.Tables[0].Rows[0]["TSO_ID"].ToString() != string.Empty)
                    tmpTipoSolI.CodigoTipoPersona = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TSO_ID"]);
                else
                    tmpTipoSolI.CodigoTipoPersona = (int)TipoSolicitante.Solicitante;
                objIdentity.TipoSolicitante = tmpTipoSolI;

                objIdentity.Pregunta = dsResultado.Tables[0].Rows[0]["PER_PREGUNTA"].ToString();
                objIdentity.Respuesta = dsResultado.Tables[0].Rows[0]["PER_PREGUNTA"].ToString();
                objIdentity.Activo = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["PER_ACTIVO"]);
                objIdentity.TarjetaProfesional = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_TARJETA_PROFESIONAL"]);
                objIdentity.IdAutoridadAmbiental = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["AUT_ID"]);
            }
            return objIdentity;
        }



        /// <summary>
        /// 27-10-2010 - HAVA: Manual
        /// </summary>
        /// <param name="usuario">IdApplicationUser</param>
        public PersonaIdentity ConsultarPersonaPorIdAppUser(Int64 idAppUser)
        {
            SILPA.AccesoDatos.Usuario.UsuarioDalc usuarioDalc = new SILPA.AccesoDatos.Usuario.UsuarioDalc();

            DataSet dsResultado = usuarioDalc.ConsultarUsuarioByIdUserApp(idAppUser);
            PersonaIdentity objIdentity = new PersonaIdentity();
            TipoPersonaIdentity tmpTipoPerI = new TipoPersonaIdentity();
            SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity tmpTipoIdI = new SILPA.AccesoDatos.Notificacion.TipoIdentificacionEntity();
            TipoIdentificacionDalc tmpTipoIdD;
            TipoPersonaDalc tmpTipoPerD;
            TipoPersonaIdentity tmpTipoSolI = new TipoPersonaIdentity();
            objIdentity.PersonaId = 0;
            if (dsResultado.Tables[0].Rows.Count > 0)
            {
                objIdentity.PersonaId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PER_ID"]);
                objIdentity.PrimerNombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_PRIMER_NOMBRE"]);
                objIdentity.SegundoNombre = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_SEGUNDO_NOMBRE"]);
                objIdentity.PrimerApellido = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_PRIMER_APELLIDO"]);
                objIdentity.SegundoApellido = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_SEGUNDO_APELLIDO"]);
                objIdentity.NumeroIdentificacion = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_NUMERO_IDENTIFICACION"]);
                tmpTipoIdI.Id = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TID_ID"]);
                tmpTipoIdD = new TipoIdentificacionDalc();
                tmpTipoIdD.ObtenerTipoIdentificacion(ref tmpTipoIdI);
                objIdentity.TipoDocumentoIdentificacion = tmpTipoIdI;
                objIdentity.LugarExpediciónDocumento = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_LUGAR_EXPEDICION_DOC"]);
                //objIdentity.Pais = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PAI_ID"]);
                objIdentity.Telefono = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_TELEFONO"]);
                objIdentity.Celular = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_CELULAR"]);
                objIdentity.Fax = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_FAX"]);
                objIdentity.CorreoElectronico = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_CORREO_ELECTRONICO"]);
                objIdentity.IdApplicationUser = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["ID_APPLICATION_USER"]);
                objIdentity.RazonSocial = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_RAZON_SOCIAL"]);

                tmpTipoPerI.CodigoTipoPersona = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PER_TIPO_PERSONA"]);
                tmpTipoPerD = new TipoPersonaDalc(ref tmpTipoPerI);
                objIdentity.TipoPersona = tmpTipoPerI;

                if (dsResultado.Tables[0].Rows[0]["TSO_ID"].ToString() != string.Empty)
                    tmpTipoSolI.CodigoTipoPersona = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TSO_ID"]);
                else
                    tmpTipoSolI.CodigoTipoPersona = (int)TipoSolicitante.Solicitante;
                objIdentity.TipoSolicitante = tmpTipoSolI;

                objIdentity.Pregunta = dsResultado.Tables[0].Rows[0]["PER_PREGUNTA"].ToString();
                objIdentity.Respuesta = dsResultado.Tables[0].Rows[0]["PER_PREGUNTA"].ToString();
                objIdentity.Activo = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["PER_ACTIVO"]);
                objIdentity.TarjetaProfesional = Convert.ToString(dsResultado.Tables[0].Rows[0]["PER_TARJETA_PROFESIONAL"]);
                objIdentity.IdAutoridadAmbiental = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["AUT_ID"]);
                //jmartinez salvoconducto fase 2
                objIdentity.TipoIdentificacionIDEAM = dsResultado.Tables[0].Rows[0]["TIPO_IDENTIFICACION_IDEAM"].ToString();
                objIdentity.TipoPersonaIDEAM = dsResultado.Tables[0].Rows[0]["TIPO_PERSONA_IDEAM"].ToString();




            }
            return objIdentity;
        }




        /// <summary>
        /// 11-08-2010 - aegb: CU Emitir Documento Manual
        /// Constructor de la clase que a su vez carga los valores en una tabla de las personas
        /// </summary>
        /// <param name="numeroSilpa">numero silpa</param>
        public DataTable ConsultarPersonasNumeroSilpa(string numeroSilpa)
        {
            objConfiguracion = new Configuracion();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { numeroSilpa };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_PERSONAS_NUMERO_VITAL", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            return dsResultado.Tables[0];
        }

        /// <summary>
        /// 04-10-2010 - aegb
        /// Constructor de la clase que a su vez carga los valores en una tabla de los numeros silpa de una autoridad ambiental para un tramite
        /// </summary>
        /// <param name="numeroSilpa">numero silpa</param>
        public DataSet ConsultarNumeroVitalAutoridad(int tramiteID, int autoridadID)
        {
            objConfiguracion = new Configuracion();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] {tramiteID, autoridadID };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_NUMERO_VITAL_AUTORIDAD", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            return dsResultado;
        }

        /// <summary>
        /// 04-10-2010 - aegb
        /// Constructor de la clase que a su vez carga los valores en una tabla de los numeros silpa de una autoridad ambiental para un tramite
        /// </summary>
        /// <param name="numeroSilpa">numero silpa</param>
        public DataSet ConsultarNumeroVitalAutoridad(int tramiteID, int autoridadID, long personaID)
        {
            objConfiguracion = new Configuracion();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { tramiteID, autoridadID, personaID };
            DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_NUMERO_VITAL_AUTORIDAD_PERSONA", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            return dsResultado;
        }

        public bool ConsultarPersonaPol(string user, string pass)
        {
            

            objConfiguracion = new Configuracion();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { user, pass};
            DbCommand cmd = db.GetStoredProcCommand("BAS_CONSULTAR_PERSONA_POL", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);


            if (dsResultado.Tables.Count > 0)
                if (dsResultado.Tables[0].Rows.Count > 0)
                    return true;
                else
                    return false;
            else
                return false;

            
        }
     
        public bool ConsultarPersonaAsociaRepre(string user)
        {


            objConfiguracion = new Configuracion();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { user };
            DbCommand cmd = db.GetStoredProcCommand("BAS_CONSULTAR_REPRESENTANTE_ASOCIADO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);


            if (dsResultado.Tables.Count > 0)
                if (dsResultado.Tables[0].Rows.Count > 0)
                    return true;
                else
                    return false;
            else
                return false;


        }
        public bool ConsultarPersonaAsociaApod(string user)
        {


            objConfiguracion = new Configuracion();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { user };
            DbCommand cmd = db.GetStoredProcCommand("BAS_CONSULTAR_RELACION_ASOCIADO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);


            if (dsResultado.Tables.Count > 0)
                if (dsResultado.Tables[0].Rows.Count > 0)
                    return true;
                else
                    return false;
            else
                return false;


        }

        
        public DataTable ConsultarPersona(int tipoIdentificacion, string numeroIdentificacion)
        {
            objConfiguracion = new Configuracion();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { tipoIdentificacion, numeroIdentificacion };
            DbCommand cmd = db.GetStoredProcCommand("BAS_CONSULTAR_PERSONA_INFO", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return dsResultado.Tables[0];
        }

        public DataTable ConsultaPersonaByNumeroIdentificacionSILAVITAL(string numeroIdentificacion, int tipoIdentificacion)
        {
            objConfiguracion = new Configuracion();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { numeroIdentificacion, tipoIdentificacion };
            DbCommand cmd = db.GetStoredProcCommand("NOT_CONSULTAR_PERSONA_SILA_VITAL", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return dsResultado.Tables[0];
        }


        public DataSet ConsultarPersonaSun(string user)
        {
            objConfiguracion = new Configuracion();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { user };
            DbCommand cmd = db.GetStoredProcCommand("BAS_CONSULTAR_PERSONA_SUN", parametros);
            return db.ExecuteDataSet(cmd);

        }


        /// <summary>
        /// Obtiene el nombre de la autoridad ambiental asociada el solcitante al registrarse
        /// </summary>
        /// <param name="idPersona">long: identificador de la persona en eSecurity</param>
        /// <returns>string: nombre de la AA</returns>
        public string ObtenerAutoridadPorPersona(long idPersona, out int idAA) 
        {
            //objConfiguracion = new Configuracion();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { idPersona, string.Empty, 0};
            DbCommand cmd = db.GetStoredProcCommand("OBTENER_AUTORIDAD_POR_PERSONA", parametros);
            db.ExecuteNonQuery(cmd);
            string result  = db.GetParameterValue(cmd,"@AUT_NOMBRE").ToString();
            idAA = int.Parse(db.GetParameterValue(cmd, "@IDCompany").ToString());
            return result;
        }



        /// <summary>
        /// hava:23-nov-10
        /// Obtiene el correo electronico del funcionario notificador
        /// </summary>
        /// <param name="identificacionFuncionario">string: número de identificación del funcionario</param>
        /// <returns>string: correo del funcionario</returns>
        public string ObtenerEmailFuncionario(string identificacionFuncionario) 
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { identificacionFuncionario, string.Empty };
            DbCommand cmd = db.GetStoredProcCommand("OBTENER_CORREO_FUNCIONARIO", parametros);
            db.ExecuteNonQuery(cmd);
            string result = db.GetParameterValue(cmd, "@CORREO").ToString();
            return result;
        }
        public string ObtenerEmailFuncionarioByApplicationUserID(string applicationUserID)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { applicationUserID, string.Empty };
            DbCommand cmd = db.GetStoredProcCommand("OBTENER_CORREO_FUNCIONARIO_BY_ID", parametros);
            db.ExecuteNonQuery(cmd);
            string result = db.GetParameterValue(cmd, "@CORREO").ToString();
            return result;
        }


        public DataTable ConsultarPersonasActivas(string strNombreUsuario = null)
        {
            objConfiguracion = new Configuracion();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { strNombreUsuario };
            DbCommand cmd = db.GetStoredProcCommand("BAS_CONSULTAR_PERSONAS_ACTIVAS", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return dsResultado.Tables[0];
        }


        /// <summary>
        /// Obtener el listado de representantes de una persona
        /// </summary>
        /// <param name="p_lngIdPersona">long con el id de los representantes de una persona</param>
        /// <returns>DataTable con la información de los representantes</returns>
        public DataTable ConsultarRepresentantesPersona(long p_lngIdPersona)
        {
            objConfiguracion = new Configuracion();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { p_lngIdPersona };
            DbCommand cmd = db.GetStoredProcCommand("BPM_CONSULTAR_REPRESENTANTE_LEGAL", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return dsResultado.Tables[0];
        }

        /// <summary>
        /// Crear un nuevo representante
        /// </summary>
        /// <param name="p_lngIdPersona">long con el id de la persona</param>
        /// <param name="p_lngIdRepresentante">long con el id del representante</param>
        /// <param name="p_strDescripcion">string con la descripción</param>
        public void CrearRepresentantesPersona(long p_lngIdPersona, long p_lngIdRepresentante, string p_strDescripcion)
        {
            objConfiguracion = new Configuracion();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { p_lngIdPersona, p_lngIdRepresentante, p_strDescripcion };
            DbCommand cmd = db.GetStoredProcCommand("BPM_INSERTAR_REPRESENTANTE_LEGAL", parametros);
            db.ExecuteNonQuery(cmd);
        }


        /// <summary>
        /// Editar los datos de unrepresentante
        /// </summary>
        /// <param name="p_lngIdRepresentante">long con el id del representante</param>
        /// <param name="p_strDescripcion">string con la descripción</param>
        public void ModificarRepresentantesPersona(long p_lngIdRepresentante, string p_strDescripcion)
        {
            objConfiguracion = new Configuracion();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { p_lngIdRepresentante, p_strDescripcion };
            DbCommand cmd = db.GetStoredProcCommand("BPM_ACTUALIZAR_REPRESENTANTE_LEGAL", parametros);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Elimina un representante
        /// </summary>
        /// <param name="p_lngIdRepresentante">long con el id del representante</param>
        public void EliminarRepresentantesPersona(long p_lngIdRepresentante)
        {
            objConfiguracion = new Configuracion();
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { p_lngIdRepresentante };
            DbCommand cmd = db.GetStoredProcCommand("BPM_ELIMINAR_REPRESENTANTE_LEGAL", parametros);
            db.ExecuteNonQuery(cmd);
        }

        public void ObtenerDirecciones(ref PersonaIdentity persona)
        {
         
        }


        /// <summary>
        /// Obtiene el correo de una persona
        /// </summary>
        /// <param name="p_intPersonaID">int con el identificador de la persona</param>
        /// <returns>string con el correo</returns>
        public string ObtenerCorreoPersona(int p_intPersonaID)
        {
            string strCorreo = "";
            objConfiguracion = new Configuracion();

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { p_intPersonaID };
            DbCommand cmd = db.GetStoredProcCommand("BAS_CONSULTA_CORREO_PERSONA", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);

            if (dsResultado != null && dsResultado.Tables.Count > 0 && dsResultado.Tables[0].Rows.Count > 0)
                strCorreo = (dsResultado.Tables[0].Rows[0]["CORREO"] != System.DBNull.Value ? dsResultado.Tables[0].Rows[0]["CORREO"].ToString() : "");

            return strCorreo;
        }
    }
}
