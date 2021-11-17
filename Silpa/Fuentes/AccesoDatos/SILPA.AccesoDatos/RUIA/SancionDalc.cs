using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace SILPA.AccesoDatos.RUIA
{
    public class SancionDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        public SancionDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Método que inserta una sanción en la base de datos
        /// </summary>
        /// <param name="objIdentity">Onjeto con los datos de la sanción</param>
        public void InsertarSancion(ref SancionIdentity objIdentity)
        {
            try
            {
	            // manejamos los nulos en el sitio de concurrencia
	            object objMunId = objIdentity.MunId;
	            object objCorId = objIdentity.CorId;
	            object objVerId = objIdentity.VerId;

	            if (objIdentity.MunId == -1)
	            {
	                objMunId = System.DBNull.Value;
	            }

	            if (objIdentity.CorId == -1)
	            {
	                objCorId = System.DBNull.Value;
	            }

	            if (objIdentity.VerId == -1)
	            {
	                objVerId = System.DBNull.Value;
	            }
	          
	                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
	
	                // mardila 05/04/2010 guardamos la ocurrencia
	                object[] parametros = new object[]
	                       {                            
	                            objIdentity.IdSancion, objIdentity.TipoPersona, objIdentity.IdFalta,
	                            objIdentity.DescripcionNorma, 
	                            objIdentity.NumeroExpediente, objIdentity.NumeroActo, 
	                            objIdentity.FechaExpedicion,objIdentity.FechaEjecutoria,
	                            objIdentity.FechaEjecucion,objIdentity.RazonSocial,objIdentity.Nit,
	                            objIdentity.PrimerNombre,objIdentity.SegundoNombre,objIdentity.PrimerApellido,
	                            objIdentity.SegundoApellido,objIdentity.IdTipoIdentificacion,
	                            objIdentity.NumeroIdentificacion,objIdentity.IdMunicipio,
	                            objIdentity.IdAutoridad,objIdentity.DescripcionDesfijacion,                        
	                            objMunId,
	                            objCorId,
	                            objVerId,
	                            objIdentity.IdSancionPrincipal,
	                            objIdentity.TramiteModificacion,
	                            objIdentity.TipoDocumento,
	                            objIdentity.Observaciones
	                        };
	
	                DbCommand cmd = db.GetStoredProcCommand("RUH_CREAR_SANCION", parametros);
	                db.ExecuteNonQuery(cmd);
	                string _idSancion = cmd.Parameters["@P_SAN_ID_SANCION"].Value.ToString();
	                objIdentity.IdSancion = Int64.Parse(_idSancion);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al insertar sanción en la base de datos.";
                throw new Exception(strException, ex);
            }
        }
        
        /// <summary>
        /// aegb
        /// Método que modifica una sanción en la base de datos
        /// </summary>
        /// <param name="objIdentity">Onjeto con los datos de la sanción</param>
        public void ActualizarSancion(SancionIdentity objIdentity)
        {
            // manejamos los nulos en el sitio de concurrencia
            object objMunId = objIdentity.MunId;
            object objCorId = objIdentity.CorId;
            object objVerId = objIdentity.VerId;

            if (objIdentity.MunId == -1)
            {
                objMunId = System.DBNull.Value;
            }

            if (objIdentity.CorId == -1)
            {
                objCorId = System.DBNull.Value;
            }

            if (objIdentity.VerId == -1)
            {
                objVerId = System.DBNull.Value;
            }
                     
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());              
                object[] parametros = new object[]
                       {                            
                            objIdentity.IdSancion, objIdentity.IdFalta,
                            objIdentity.DescripcionNorma, 
                            objIdentity.NumeroExpediente, objIdentity.NumeroActo, 
                            objIdentity.FechaExpedicion,objIdentity.FechaEjecutoria,
                            objIdentity.FechaEjecucion,     
                            objIdentity.IdAutoridad,objIdentity.DescripcionDesfijacion,                        
                           objMunId,
                           objCorId,
                           objVerId,
                           objIdentity.MotivoModificacion,
                           objIdentity.IdSancionPrincipal,
                           objIdentity.TramiteModificacion //30-sep-2010 - aegb : incidencia 2284
                           , objIdentity.Observaciones
                           ,objIdentity.UsuarioModifica
                        };

                DbCommand cmd = db.GetStoredProcCommand("RUH_ACTUALIZAR_SANCION", parametros);
                db.ExecuteNonQuery(cmd);
                     
        }

        /// <summary>
        /// aegb
        /// Método que actualiza la fecha de cumplimiento de una sanción en la base de datos
        /// </summary>
        /// <param name="objIdentity">Onjeto con los datos de la sanción</param>
        public void ActualizarSancion(long _idSancion, string _fechaEjecucion, int _idSancionPrincipal)
        {           
            
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[]
                       {                            
                            _idSancion, _fechaEjecucion, _idSancionPrincipal
                        };

            DbCommand cmd = db.GetStoredProcCommand("RUH_ACTUALIZAR_SANCION_FECHA", parametros);
            db.ExecuteNonQuery(cmd);

        }


        /// <summary>
        /// Metodo de consulta de sanciones con opciones de busqueda
        /// </summary>
        /// <param name="_idSancion">Identificador de la sancion</param>
        /// <param name="_idAutoridad">Identificador de la Autoridad Ambiental</param>
        /// <param name="_idFalta">IIdentificador del tipo de falta</param>
        /// <param name="_numeroExpediente">Número del expediente</param>
        /// <param name="_numeroActo">Número del Acto administrativo</param>
        /// <param name="_numeroIdentificacion">Número de identificación</param>
        /// <param name="_fechaDesde">Fecha de consulta incial</param>
        /// <param name="_fechaHasta">Fecha de consulta final</param>
        /// <returns>Conjunto de Datos: [CODIGO] - [AUTORIDAD] - [FALTA] - [CONCURRENCIA] - [DESCRIPCION_NORMA]
        /// [EXPEDIENTE] - [ACTO] - [FECHA_EXP_ACTO] - [FECHA_EJECUT_ACTO] - [FECHA_EJECU_ACTO] - [PRIMER_NOMBRE]
        /// - [SEGUNDO_NOMBRE] - [PRIMER_APELLIDO] - [SEGUNDO_APELLIDO] - [RAZON_SOCIAL] - [NIT]
        /// - [IDENTIFICACION] - [TIPO_ID] - [DEPARTAMENTO] - [MUNICIPIO] - [FECHA_DESF] - [DESCRIPCION_DESF]
        /// </returns>
        public DataSet ListaSancion(long? _idSancion, int? _idAutoridad,
            int? _idFalta, int? _idTipoSancion, string _numeroExpediente, string _numeroActo, string _nombreResponsable,
            string _fechaDesde, string _fechaHasta, int? _idDepartamento, int? _idMunicipio, int? _idCorregimiento, int? _idVereda,
            string _fechaEjecucion, string _numeroIdentificacion, string estadoSancion)
        {
         
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { _idSancion, _idAutoridad, _idFalta, _idTipoSancion, _numeroExpediente,
                    _numeroActo,_nombreResponsable, _fechaDesde, _fechaHasta, _idDepartamento, _idMunicipio, _idCorregimiento, _idVereda,
                    _fechaEjecucion, _numeroIdentificacion,estadoSancion};
                DbCommand cmd = db.GetStoredProcCommand("RUH_LISTA_SANCION", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);                      
        }

       

        /// <summary>
        /// Metodo de consulta de sanciones con opciones de busqueda
        /// </summary>
        /// <param name="_idSancion">Identificador de la sancion</param>
        /// <param name="_idAutoridad">Identificador de la Autoridad Ambiental</param>
        /// <param name="_idFalta">IIdentificador del tipo de falta</param>
        /// <param name="_numeroExpediente">Número del expediente</param>
        /// <param name="_numeroActo">Número del Acto administrativo</param>
        /// <param name="_numeroIdentificacion">Número de identificación</param>
        /// <param name="_fechaDesde">Fecha de consulta incial</param>
        /// <param name="_fechaHasta">Fecha de consulta final</param>
        /// <returns>Conjunto de Datos: [CODIGO] - [AUTORIDAD] - [FALTA] - [CONCURRENCIA] - [DESCRIPCION_NORMA]
        /// [EXPEDIENTE] - [ACTO] - [FECHA_EXP_ACTO] - [FECHA_EJECUT_ACTO] - [FECHA_EJECU_ACTO] - [PRIMER_NOMBRE]
        /// - [SEGUNDO_NOMBRE] - [PRIMER_APELLIDO] - [SEGUNDO_APELLIDO] - [RAZON_SOCIAL] - [NIT]
        /// - [IDENTIFICACION] - [TIPO_ID] - [DEPARTAMENTO] - [MUNICIPIO] - [FECHA_DESF] - [DESCRIPCION_DESF]
        /// </returns>
        public DataSet ListaSancionDetalle(long? _idSancion, int? _idAutoridad, string _fechaEjecucion)
        {
          
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { _idSancion, _idAutoridad, _fechaEjecucion };
                DbCommand cmd = db.GetStoredProcCommand("RUH_LISTA_SANCION_DETALLE", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            
        }


        /// <summary>
        /// Metodo de consulta de sanciones con opciones de busqueda
        /// </summary>
        /// <param name="_idSancion">Identificador de la sancion</param>
        /// <param name="_idAutoridad">Identificador de la Autoridad Ambiental</param>
        /// <param name="_idFalta">IIdentificador del tipo de falta</param>
        /// <param name="_numeroExpediente">Número del expediente</param>
        /// <param name="_numeroActo">Número del Acto administrativo</param>
        /// <param name="_numeroIdentificacion">Número de identificación</param>
        /// <param name="_fechaDesde">Fecha de consulta incial</param>
        /// <param name="_fechaHasta">Fecha de consulta final</param>
        /// <returns>Conjunto de Datos: [CODIGO] - [AUTORIDAD] 
        /// [FECHA CUMPLIMIENTO]
        /// </returns>
        public SancionIdentity ListaSancionDetalles(long _idSancion, int? _idAutoridad, string _fechaEjecucion)
        {
           
                SancionIdentity sancion = new SancionIdentity();
                sancion.IdSancion = _idSancion;    
                DataSet dsResultado = ListaSancionDetalle(_idSancion, _idAutoridad, _fechaEjecucion);

                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    DataRow row = dsResultado.Tables[0].Rows[0];
                    sancion.CorId = row["COR_ID"].ToString() == "" ? -1 : int.Parse(row["COR_ID"].ToString());
                    sancion.DepId = row["DEP_ID"].ToString() == "" ? -1 : int.Parse(row["DEP_ID"].ToString());
                    sancion.DescripcionDesfijacion = row["SAN_DESCRIPCION_DESF"].ToString();
                    sancion.DescripcionNorma = row["SAN_DESCRIPCION_NORMA"].ToString();
                    //sancion.FechaDesde = DateTime.Parse(row["SAN_FECHA_DESDE"].ToString()).ToShortDateString();
                    sancion.FechaEjecucion = row["SAN_FECHA_EJECUCION_ACTO"].ToString() == "" ? "" : DateTime.Parse(row["SAN_FECHA_EJECUCION_ACTO"].ToString()).ToShortDateString();
                    sancion.FechaEjecutoria = row["SAN_FECHA_EJECUTORIA_ACTO"].ToString() == "" ? "" : DateTime.Parse(row["SAN_FECHA_EJECUTORIA_ACTO"].ToString()).ToShortDateString();
                    sancion.FechaExpedicion = DateTime.Parse(row["SAN_FECHA_EXPEDICION_ACTO"].ToString()).ToShortDateString();
                    //sancion.FechaHasta = DateTime.Parse(row["SAN_FECHA_HASTA"].ToString()).ToShortDateString();
                    sancion.IdAutoridad = int.Parse(row["SAN_AUT_ID"].ToString());
                    sancion.IdFalta = row["SAN_TIF_ID_FALTA"].ToString() == "" ? -1 : int.Parse(row["SAN_TIF_ID_FALTA"].ToString());
                    sancion.IdMunicipio = row["SAN_MUN_ID"].ToString() == "" ? -1 : int.Parse(row["SAN_MUN_ID"].ToString());
                    sancion.IdDpto = row["SAN_DEP_ID"].ToString() == "" ? -1 : int.Parse(row["SAN_DEP_ID"].ToString());
                    sancion.IdTipoIdentificacion = int.Parse(row["SAN_TID_ID"].ToString());
                    sancion.MotivoModificacion = row["SAN_MOTIVO_MODIFICACION"].ToString();
                    sancion.MunId = int.Parse(row["MUN_ID"].ToString());
                    sancion.Nit = row["SAN_NIT"].ToString();
                    sancion.NumeroActo = row["SAN_NUMERO_ACTO"].ToString();
                    sancion.NumeroExpediente = row["SAN_NUMERO_EXPE"].ToString();
                    sancion.NumeroIdentificacion = row["SAN_NUMERO_IDENTIFICACION"].ToString();
                    sancion.PrimerApellido = row["SAN_PRIMER_APELLIDO"].ToString();
                    sancion.PrimerNombre = row["SAN_PRIMER_NOMBRE"].ToString();
                    sancion.RazonSocial = row["SAN_RAZON_SOCIAL"].ToString();
                    sancion.SegundoApellido = row["SAN_SEGUNDO_APELLIDO"].ToString();
                    sancion.SegundoNombre = row["SAN_SEGUNDO_NOMBRE"].ToString();
                    sancion.TipoPersona = int.Parse(row["SAN_TIPO_PERSONA"].ToString());
                    sancion.VerId = row["VER_ID"].ToString() == "" ? -1 : int.Parse(row["VER_ID"].ToString());
                    sancion.IdSancionPrincipal = int.Parse(row["RSA_ID_OPCION"].ToString());
                    sancion.SancionPrincipal = row["RSA_SANCION_APLICADA"].ToString();
                    //30-sep-2010 - aegb : incidencia 2284
                    sancion.IdFalta = row["SAN_TIF_ID_FALTA"].ToString() == "" ? -1 : int.Parse(row["SAN_TIF_ID_FALTA"].ToString());
                    sancion.TramiteModificacion=row["SAN_TRAMITE_MODIFICACION"].ToString() == "" ? -1 : int.Parse(row["SAN_TRAMITE_MODIFICACION"].ToString());
                    sancion.Observaciones = row["SAN_OBSERVACIONES"].ToString();
                  
                }
                return sancion;
          
        }

        /// <summary>
        /// Elimina una sancion, la inactiva en la base de datos
        /// </summary>
        /// <param name="idPublicacion">Identificador de la sancion</param>
        public void EliminarSancion(long idSancion, string motivoEliminacion, string Usuario)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[]
                       {
                           idSancion, motivoEliminacion,Usuario
                        };

            DbCommand cmd = db.GetStoredProcCommand("RUH_ELIMINAR_SANCION", parametros);
            db.ExecuteNonQuery(cmd);
        }

        

        /// <summary>
        /// aegb
        /// Método que actualiza la fecha ejecutoria de una sanción en la base de datos
        /// </summary>
        /// <param name="objIdentity">Onjeto con los datos de la sanción</param>
        public void ActualizarSancionEjecutoria(int _idAutoridad, string _numeroExpediente, string _numeroActo, DateTime _fechaEjecucion,string tipoDocumento)
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[]
                       {                            
                          _idAutoridad, _numeroExpediente, _numeroActo, _fechaEjecucion, tipoDocumento
                        };

            DbCommand cmd = db.GetStoredProcCommand("RUH_ACTUALIZAR_SANCION_EJECUTORIA", parametros);
            db.ExecuteNonQuery(cmd);

        }

        /// <summary>
        /// aegb
        /// Método que actualiza la fecha ejecutoria de una sanción en la base de datos
        /// </summary>
        /// <param name="objIdentity">Onjeto con los datos de la sanción</param>
        public void ActualizarSancion(int _idAutoridad, string _numeroExpediente, string _numeroActo, DateTime _fechaEjecucion,string tipoDocumento)
        {

            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[]
                       {                            
                          _idAutoridad, _numeroExpediente, _numeroActo, _fechaEjecucion, tipoDocumento
                        };

            DbCommand cmd = db.GetStoredProcCommand("RUH_ACTUALIZAR_SANCION_FECHA_CUMPLIMIENTO", parametros);
            db.ExecuteNonQuery(cmd);

        }
              
    }
}
