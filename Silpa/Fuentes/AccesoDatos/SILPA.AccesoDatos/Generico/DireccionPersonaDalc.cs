using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using System.Configuration;
using SILPA.Comun;
using System.Data.SqlClient;


namespace SILPA.AccesoDatos.Generico
{
    [Serializable]
    public class DireccionPersonaDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public DireccionPersonaDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Constructor de la clase que a su vez carga los valores para una identidad de la dirección
        /// de una persona cuyo valor del identificador corresponda con la BD. Utilizando el Id de la direccion o el Id de la persona
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador de la dirección a cargar, en la propiedad ID del objetoIdentity</param>
        /// <param name="objIdentity.IdPersona ">Valor del identificador de la persona asociada a la dirección a cargar, en la propiedad ID del objetoIdentity</param>
        public DireccionPersonaDalc(ref DireccionPersonaIdentity objIdentity)
        {
            objConfiguracion = new Configuracion();
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.Id, objIdentity.IdPersona };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_DIR_PERSONA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                objIdentity.Id = Convert.ToInt64(dsResultado.Tables[0].Rows[0]["DIP_ID"]);
                objIdentity.IdPersona = Convert.ToInt64(dsResultado.Tables[0].Rows[0]["PER_ID"]);
                objIdentity.MunicipioId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["MUN_ID"]);
                objIdentity.NombreMunicipio = Convert.ToString(dsResultado.Tables[0].Rows[0]["MUN_NOMBRE"]);
                objIdentity.CorregimientoId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["COR_ID"]);
                objIdentity.NombreCorregimiento = Convert.ToString(dsResultado.Tables[0].Rows[0]["COR_NOMBRE"]);
                objIdentity.VeredaId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["VER_ID"]);
                objIdentity.NombreVereda = Convert.ToString(dsResultado.Tables[0].Rows[0]["VER_NOMBRE"]);
                objIdentity.DireccionPersona = Convert.ToString(dsResultado.Tables[0].Rows[0]["DIP_DIRECCION"]);
                objIdentity.PaisId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PAI_ID"]);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Carga los valores para una identidad de la dirección cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador de la dirección a cargar, en la propiedad ID del objetoIdentity</param>
        /// <param name="objIdentity.IdPersona ">Valor del identificador de la persona asociada a la dirección a cargar, en la propiedad ID del objetoIdentity</param>
        public void ObtenerDireccionPersona(ref DireccionPersonaIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.Id, objIdentity.IdPersona };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_DIR_PERSONA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    if (dsResultado.Tables[0].Rows[0]["DIP_ID"] != DBNull.Value) { objIdentity.Id = Convert.ToInt64(dsResultado.Tables[0].Rows[0]["DIP_ID"]); }
                    if (dsResultado.Tables[0].Rows[0]["PER_ID"] != DBNull.Value) { objIdentity.IdPersona = Convert.ToInt64(dsResultado.Tables[0].Rows[0]["PER_ID"]); }
                    if (dsResultado.Tables[0].Rows[0]["MUN_ID"] != DBNull.Value) { objIdentity.MunicipioId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["MUN_ID"]); }
                    if (dsResultado.Tables[0].Rows[0]["MUN_NOMBRE"] != DBNull.Value) { objIdentity.NombreMunicipio = Convert.ToString(dsResultado.Tables[0].Rows[0]["MUN_NOMBRE"]); }
                    if (dsResultado.Tables[0].Rows[0]["DIP_DIRECCION"] != DBNull.Value) { objIdentity.DireccionPersona = Convert.ToString(dsResultado.Tables[0].Rows[0]["DIP_DIRECCION"]);}
                    if (dsResultado.Tables[0].Rows[0]["DEP_NOMBRE"] != DBNull.Value) { objIdentity.NombreDepartamento = Convert.ToString(dsResultado.Tables[0].Rows[0]["DEP_NOMBRE"]); }
                    if (dsResultado.Tables[0].Rows[0]["DEP_ID"] != DBNull.Value) { objIdentity.DepartamentoId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["DEP_ID"]); }
                    if (dsResultado.Tables[0].Rows[0]["PAI_ID"] != DBNull.Value) { objIdentity.PaisId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["PAI_ID"]); }
                    if (dsResultado.Tables[0].Rows[0]["PAI_NOMBRE"] != DBNull.Value) { objIdentity.NombrePais = Convert.ToString(dsResultado.Tables[0].Rows[0]["PAI_NOMBRE"]); }
                    if (dsResultado.Tables[0].Rows[0]["VER_ID"] != DBNull.Value) { objIdentity.VeredaId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["VER_ID"]); }
                    if (dsResultado.Tables[0].Rows[0]["VER_NOMBRE"] != DBNull.Value) { objIdentity.NombreVereda = Convert.ToString(dsResultado.Tables[0].Rows[0]["VER_NOMBRE"]); }
                    if (dsResultado.Tables[0].Rows[0]["COR_ID"] != DBNull.Value) { objIdentity.CorregimientoId = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["COR_ID"]); }
                    if (dsResultado.Tables[0].Rows[0]["COR_NOMBRE"] != DBNull.Value) { objIdentity.NombreCorregimiento = Convert.ToString(dsResultado.Tables[0].Rows[0]["COR_NOMBRE"]); }
                }              
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al cargar los valores para una identidad de la dirección.";
                throw new Exception(strException, ex);
            }
        }


        /// <summary>
        /// hava:
        /// Método que obtiene las direcciones de relacionadas al solicitante
        /// </summary>
        /// <param name="IdPersona">identificador de la persona</param>
        /// <returns>Lista de direcciones relacionadas a la persona</returns>
        public List<DireccionPersonaIdentity> ObtenerDirecciones(long IdPersona) 
        {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { IdPersona };
                
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_DIRECCIONES", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                List<DireccionPersonaIdentity> listaDirecciones =  null;

                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    listaDirecciones = new List<DireccionPersonaIdentity>();

                    foreach(DataRow dr in dsResultado.Tables[0].Rows)
                    {
                        DireccionPersonaIdentity objDirPersona = new DireccionPersonaIdentity();

                        if (dr["DIP_ID"] != DBNull.Value) { objDirPersona.Id = Convert.ToInt64(dr["DIP_ID"]); }
                        if (dr["PER_ID"] != DBNull.Value) { objDirPersona.IdPersona = Convert.ToInt64(dr["PER_ID"]); }
                        if (dr["MUN_ID"] != DBNull.Value) { objDirPersona.MunicipioId = Convert.ToInt32(dr["MUN_ID"]); }
                        if (dr["MUN_NOMBRE"] != DBNull.Value) { objDirPersona.NombreMunicipio = Convert.ToString(dr["MUN_NOMBRE"]); }
                        if (dr["DIP_DIRECCION"] != DBNull.Value) { objDirPersona.DireccionPersona = Convert.ToString(dr["DIP_DIRECCION"]); }
                        if (dr["DEP_NOMBRE"] != DBNull.Value) { objDirPersona.NombreDepartamento = Convert.ToString(dr["DEP_NOMBRE"]); }
                        if (dr["DEP_ID"] != DBNull.Value) { objDirPersona.DepartamentoId = Convert.ToInt32(dr["DEP_ID"]); }
                        if (dr["PAI_ID"] != DBNull.Value) { objDirPersona.PaisId = Convert.ToInt32(dr["PAI_ID"]); }
                        if (dr["PAI_NOMBRE"] != DBNull.Value) { objDirPersona.NombrePais = Convert.ToString(dr["PAI_NOMBRE"]); }
                        if (dr["VER_ID"] != DBNull.Value) { objDirPersona.VeredaId = Convert.ToInt32(dr["VER_ID"]); }
                        if (dr["VER_NOMBRE"] != DBNull.Value) { objDirPersona.NombreVereda = Convert.ToString(dr["VER_NOMBRE"]); }
                        if (dr["COR_ID"] != DBNull.Value) { objDirPersona.CorregimientoId = Convert.ToInt32(dr["COR_ID"]); }
                        if (dr["COR_NOMBRE"] != DBNull.Value) { objDirPersona.NombreCorregimiento = Convert.ToString(dr["COR_NOMBRE"]); }

                        if (dr["ID_TIPO_DIRECCION"] != DBNull.Value) { objDirPersona.TipoDireccion= int.Parse(dr["ID_TIPO_DIRECCION"].ToString()); }
                        if (dr["TIPO_DIRECCION"] != DBNull.Value) { objDirPersona.NombreTipoDireccion = Convert.ToString(dr["TIPO_DIRECCION"]); }
                        listaDirecciones.Add(objDirPersona);
                    }
               }
               
            return listaDirecciones;
        }


        /// <summary>
        /// Inserta los valores para una identidad de la dirección cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity">Valores a insertar</param>
        public void InsertarDireccionPersona(ref DireccionPersonaIdentity objIdentity)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { objIdentity.PaisId, objIdentity.MunicipioId, objIdentity.VeredaId, objIdentity.CorregimientoId, objIdentity.DireccionPersona, objIdentity.IdPersona, objIdentity.TipoDireccion };
            DbCommand cmd = db.GetStoredProcCommand("BAS_INSERT_DIRECCION_PERSONA", parametros);
            try
            {
                //cmd.ExecuteNonQuery();
                db.ExecuteNonQuery(cmd);
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }


        public void InsertarTipoDocumentoAcreditacion(Int64 idApoderado, Int64 tipIdAcreditacion)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { idApoderado, tipIdAcreditacion};
            DbCommand cmd = db.GetStoredProcCommand("BAS_INSERT_TIPO_ACREDITACION_APODERADO", parametros);
            try
            {
                //cmd.ExecuteNonQuery();
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }    
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }
        public void ActualizarTipoDocumentoAcreditacion(Int64 idSolicitante,string idApoderado, Int64 tipIdAcreditacion)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { idSolicitante, idApoderado, tipIdAcreditacion };
            DbCommand cmd = db.GetStoredProcCommand("BAS_ACTUALIZA_TIPO_ACREDITACION_APODERADO", parametros);
            try
            {
                //cmd.ExecuteNonQuery();
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
        }
        /// <summary>
        /// Actualiza los valores para una identidad de la dirección cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity">Valores a insertar</param>
        public void ActualizarDireccionPersona(ref DireccionPersonaIdentity objIdentity)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { objIdentity.MunicipioId, objIdentity.VeredaId, objIdentity.CorregimientoId, objIdentity.DireccionPersona, objIdentity.IdPersona };
            DbCommand cmd = db.GetStoredProcCommand("BAS_UPDATE_DIRECCION_PERSONA", parametros);
            cmd.ExecuteNonQuery();
        }

        public void ActualizarDireccionPersonaSol(ref DireccionPersonaIdentity objIdentity)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { objIdentity.MunicipioId, objIdentity.CorregimientoId, objIdentity.VeredaId, objIdentity.DireccionPersona, objIdentity.IdPersona, objIdentity.TipoDireccion, objIdentity.PaisId};
            DbCommand cmd = db.GetStoredProcCommand("BAS_UPDATE_DIRECCION_PERSONA_SOL", parametros);
            //cmd.ExecuteNonQuery();
            DataSet dsResultado = db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// Borrar los valores para una identidad de la dirección cuyo valor del identificador corresponda con la BD
        /// </summary>
        /// <param name="objIdentity">Valores a insertar</param>
        public void BorraDireccionPersona(ref DireccionPersonaIdentity objIdentity)
        {
            SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
            object[] parametros = new object[] { objIdentity.IdPersona };
            DbCommand cmd = db.GetStoredProcCommand("BAS_DELETE_DIRECCION_PERSONA", parametros);
            cmd.ExecuteNonQuery();
        }


        /// <summary>
        /// Lista las direcciónes en la BD.
        /// </summary>
        /// <param name="objIdentity.Id">Valor del identificador de la dirección a cargar, en la propiedad ID del objetoIdentity</param>
        /// <param name="objIdentity.IdPersona ">Valor del identificador de la persona asociada a la dirección a cargar, en la propiedad ID del objetoIdentity</param>
        /// <returns>DataSet con los registros y las siguientes columnas:  DIP_ID, PER_ID, MUN_ID, MUN_NOMBRE, COR_ID, COR_NOMBRE, VER_ID, VER_NOMBRE, DIP_DIRECCION</returns>
        public DataSet ListarDireccionPersona(Nullable<Int64> lngIdDireccion, Nullable<Int64> lngIdPersona)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { lngIdDireccion, lngIdPersona };
                DbCommand cmd = db.GetStoredProcCommand("BAS_LISTA_DIR_PERSONA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);

            }
            catch (SqlException sqle)
            {
                return null;
            }
        }

    }
}
