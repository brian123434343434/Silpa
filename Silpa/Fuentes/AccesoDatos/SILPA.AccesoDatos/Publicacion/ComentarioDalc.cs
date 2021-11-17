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

namespace SILPA.AccesoDatos.Publicacion
{
    public class ComentarioDalc
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        /// <summary>
        /// Constructor vacio inicia el objeto de configuración
        /// </summary>
        public ComentarioDalc()
        {
            objConfiguracion = new Configuracion();
        }


        /// <summary>
        /// Lista los comentarios existententes en la base de datos segun los registros que apliquen.
        /// </summary>
        /// <param name="intIdComentario">Identificador del comentario</param>
        /// <param name="intIdPublicacion">Identificador de la publicacion</param>
        /// <returns>DataSet con los registros y las siguientes columnas: 
        /// ID_COMENTARIO - TEXTO_COMENTARIO - FECHA_COMENTARIO
        /// </returns>
        public DataSet ListarComentario(Nullable<int> intIdComentario, Nullable<Int64> intIdPublicacion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { intIdComentario, intIdPublicacion };
                DbCommand cmd = db.GetStoredProcCommand("SIH_LISTA_COMENTARIO", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return (dsResultado);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

        /// <summary>
        /// Inserta un comentario en la base de datos, ligado a una publicacion.
        /// </summary>
        /// <param name="intIdPublicacion">Identificador de la Publicacion</param>
        /// <param name="strComentario">Texto del comentario</param>
        public void InsertarComentario(ref ComentarioIdentity objIdentity)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { objIdentity.IdPublicacion, objIdentity.TexComentario };
                DbCommand cmd = db.GetStoredProcCommand("SIH_ADICIONAR_COMENTARIO_PUB", parametros);
                db.ExecuteNonQuery(cmd);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }

    }
}
