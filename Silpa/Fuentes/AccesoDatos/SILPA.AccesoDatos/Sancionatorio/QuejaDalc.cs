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

namespace SILPA.AccesoDatos.Sancionatorio
{
    public class QuejaDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        public QuejaDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Método que almacena los datos de una queja en la base de datos
        /// </summary>
        /// <param name="objIdentity">Objeto con los datos de la queja</param>        
        public void InsertarQueja(ref QuejaIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                    {
                        objIdentity.IdQueja,
                        objIdentity.NumeroSilpa,
                        objIdentity.DescripcionQueja,
                        objIdentity.IdMunicipio,
                        objIdentity.IdUbicacion,
                        objIdentity.IdCorregimiento,
                        objIdentity.IdVereda,
                        objIdentity.IdAreaHidrografica,
                        objIdentity.IdZonaHidrografica,
                        objIdentity.IdSubZonaHidrografica,
                        objIdentity.IdAutoridadAmbiental, 0
                        //objIdentity.IdSector
                    };
                DbCommand cmd = db.GetStoredProcCommand("SAN_CREAR_QUEJA", parametros);
                db.ExecuteNonQuery(cmd);
                string _codigoQueja = cmd.Parameters["@P_QUE_ID_QUEJA"].Value.ToString();
                objIdentity.NumeroSilpa = cmd.Parameters["@P_NUMERO_SILPA"].Value.ToString();
                objIdentity.NumeroVital = cmd.Parameters["@P_NUMERO_VITAL"].Value.ToString();
                objIdentity.IdQueja = Int64.Parse(_codigoQueja);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }


        /// <summary>
        /// Registra una queja generada
        /// </summary>
        /// <param name="numero_silpa"></param>
        /// <returns>long: identificador de la queja generada</returns>
        public void InsertarQueja(string numero_silpa,string primerNombre, 
            string segundoNombre, string primerApellido, string segundoApellido,string mailQuejoso) 
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { numero_silpa,  primerNombre, segundoNombre, primerApellido,  segundoApellido ,mailQuejoso };
            DbCommand cmd = db.GetStoredProcCommand("SAN_REGISTRAR_QUEJA", parametros);
            db.ExecuteNonQuery(cmd);

        }


        /// <summary>
        /// Método que almacena los datos de la relación entre una queja y un recurso
        /// </summary>
        /// <param name="objIdentity">Objeto con los datos del recurso y la queja</param>
        public void InsertarQuejaRecurso(ref QuejaRecursoIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                    {
                        objIdentity.IdQueja,
                        objIdentity.IdRecurso,
                        objIdentity.OtroRecurso
                    };
                DbCommand cmd = db.GetStoredProcCommand("SAN_CREAR_QUEJA_RECURSO", parametros);
                db.ExecuteNonQuery(cmd);                
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Método que almacena los datos de la coordenada de una queja
        /// </summary>
        /// <param name="objIdentity">Objeto con los datos de la coordenada</param>
        public void InsertarCoordenada(ref CoordenadaIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[]
                    {
                        objIdentity.IdQueja,
                        objIdentity.CoordenadaX,
                        objIdentity.CoordenadaY
                    };
                DbCommand cmd = db.GetStoredProcCommand("SAN_CREAR_COORDENADA", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }


        /// <summary>
        /// hava:
        /// Obtiene el nombre y el mail del quejoso 
        /// </summary>
        /// <param name="idRadicacion">identificador de la radicaición</param>
        /// <param name="mailQuejoso">string: amil del quejoso</param>
        /// <param name="nombreQuejoso">string: nombre del quejoso</param>
        public void ObtenerUsuarioQuejoso(long idRadicacion, ref string mailQuejoso, ref string nombreQuejoso) 
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { idRadicacion, string.Empty, string.Empty };
            DbCommand cmd = db.GetStoredProcCommand("SAN_OBTENER_NOMBRE_PERSONA_QUEJA", parametros);
            db.ExecuteNonQuery(cmd);
            nombreQuejoso = (string)db.GetParameterValue(cmd, "@NOMBRE_QUEJOSO");
            mailQuejoso = (string)db.GetParameterValue(cmd, "@MAIL_QUEJOSO");
        }


        public DataSet obtenerUsuarioQueja()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                DbCommand cmd = db.GetStoredProcCommand("SAN_OBTENER_USUARIO_QUEJA");
                DataSet ds_usuario = new DataSet();
                ds_usuario = db.ExecuteDataSet(cmd);

                return ds_usuario;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }


        public DataSet obtenerUsuarioQueja(string idUsuario)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { idUsuario };

                DbCommand cmd = db.GetStoredProcCommand("SAN_OBTENER_USUARIO_QUEJA", parametros);
                DataSet ds_usuario = new DataSet();
                ds_usuario = db.ExecuteDataSet(cmd);

                return ds_usuario;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        public DataSet obtenerUsuarioAudiencia()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                

                DbCommand cmd = db.GetStoredProcCommand("SAN_OBTENER_USUARIO_AUDIENCIA");
                DataSet ds_usuario = new DataSet();
                ds_usuario = db.ExecuteDataSet(cmd);

                return ds_usuario;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }


        /// <summary>
        /// Lista las quejas asociadas al un numero SILPA
        /// </summary>
        /// <param name="strNumSILPA">Numero silpa</param>
        /// <returns>DataSet con los registros y las siguientes columnas: [QUE_ID_QUEJA], [NUMERO_SILPA],[QUE_DESCRIPCION],
        /// [QUE_MUN_ID],[QUE_UBI_ID],[QUE_COR_ID],[QUE_VER_ID],[QUE_AHI_ID],[QUE_ZHI_ID],[QUE_SHI_ID],[QUE_AUT_ID]
        /// ,[QUE_SEC_ID],[QUE_ACTIVO] FROM [SILPA_PRE].[dbo].[SAN_QUEJA]</returns>
        public DataSet ListarQuejasXNumSILPA(string strNumSILPA)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { strNumSILPA };
            DbCommand cmd = db.GetStoredProcCommand("SAN_CONSULTAR_QUEJAS_NUM_SILPA", parametros);
            DataSet dsResultado = db.ExecuteDataSet(cmd);
            return (dsResultado);
        }


        /// <summary>
        /// Actualiza el  número vital de una queja
        /// </summary>
        /// <param name="IdQueja">Identificador de la queja</param>
        /// <param name="numeroVital">Número Vital Asociado</param>
        public void ActualizarNumeroVital(Int64 IdQueja, string numeroVital) 
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { numeroVital , IdQueja};
            DbCommand cmd = db.GetStoredProcCommand("SAN_ACTUALIZAR_NUMERO_VITAL_QUEJA", parametros);
            int i = db.ExecuteNonQuery(cmd);
        }


        public void ActualizarRemitente(string numero_silpa, string remitente)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { numero_silpa, remitente };
            DbCommand cmd = db.GetStoredProcCommand("SAN_ACTUALIZAR_REMITENTE", parametros);
            int i = db.ExecuteNonQuery(cmd);
        }

        public DataSet obtenerUsuarioRecurso(string idUsuario)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { idUsuario };
                DbCommand cmd = db.GetStoredProcCommand("SAN_OBTENER_USUARIO_RECURSO",parametros);
                DataSet ds_usuario = new DataSet();
                ds_usuario = db.ExecuteDataSet(cmd);

                return ds_usuario;
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
    }
}
