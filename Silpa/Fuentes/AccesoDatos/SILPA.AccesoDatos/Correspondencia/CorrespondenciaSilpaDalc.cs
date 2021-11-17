using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos;
using SILPA.Comun;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using SILPA.AccesoDatos.Generico;
using System.Configuration;

namespace SILPA.AccesoDatos.Correspondencia
{
    public class CorrespondenciaSilpaDalc
    {
        private Configuracion objConfiguracion;

        public CorrespondenciaSilpaDalc()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Listado de los estados de correspondencia -- ok
        /// </summary>
        /// <param name="str_visible"></param>
        /// <returns></returns>
        public DataSet listarEstados(string str_visible)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { str_visible };

                DbCommand cmd = db.GetStoredProcCommand("LISTAR_ESTADOS_CORRESPONDENCIA", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                return dsResultado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ok
        /// </summary>
        /// <param name="str_cod_dependencia"></param>
        /// <param name="str_nur"></param>
        /// <param name="str_remitente"></param>
        /// <param name="dte_fecha_desde"></param>
        /// <param name="dte_fecha_hasta"></param>
        /// <param name="str_ciclo_id"></param>
        /// <param name="str_estado_id"></param>
        /// <param name="str_asunto_resumen"></param>
        /// <param name="intIdAA"></param>
        /// <param name="intIdAA"></param>
        /// <returns></returns>
        public DataSet consultarMovimientos
            (
                string str_cod_dependencia, string str_nur, 
                string str_remitente, DateTime dte_fecha_desde, 
                DateTime dte_fecha_hasta, string str_ciclo_id, 
                string str_estado_id, string str_asunto_resumen,
                string str_numero_silpa, string strIdAA
            )
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] 
                { 
                    str_cod_dependencia,    str_nur,    str_remitente, 
                    dte_fecha_desde, dte_fecha_hasta, 
                    str_ciclo_id,   str_estado_id,    str_asunto_resumen, str_numero_silpa, strIdAA
                };

                
                DbCommand cmd = db.GetStoredProcCommand("COR_XS_SILA_LST_DOCUMENTOS", parametros);
                cmd.CommandTimeout = 0;
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                return dsResultado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ok
        /// </summary>
        /// <param name="int_mov_id"></param>
        /// <returns></returns>
        public SILPA.AccesoDatos.Generico.Movimiento consultarMovimientoxID(int int_mov_id)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { int_mov_id };

                DbCommand cmd = db.GetStoredProcCommand("COR_XS_SILA_CON_DOCUMENTO_MOV", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);
                return this.generarMovimiento(dsResultado);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ok
        /// </summary>
        /// <param name="str_nur"></param>
        /// <returns></returns>
        public Movimiento consultarMovimientoxNUR(string str_nur)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { str_nur };

                DbCommand cmd = db.GetStoredProcCommand("COR_XS_SILA_CON_DOCUMENTO_NUR", parametros);

                DataSet dsResultado = db.ExecuteDataSet(cmd);

                return this.generarMovimiento(dsResultado);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ok
        /// </summary>
        /// <param name="ds_data"></param>
        /// <returns></returns>
        private SILPA.AccesoDatos.Generico.Movimiento generarMovimiento(DataSet ds_data)
        {
            SILPA.AccesoDatos.Generico.Movimiento stMov = new SILPA.AccesoDatos.Generico.Movimiento();

          
            if (ds_data.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds_data.Tables[0].Rows[0];

                stMov.Anno = -1;
                if (!dr.IsNull("Doc_Anno")) 
                {
                    stMov.Anno = Int32.Parse(dr["Doc_Anno"].ToString());
                }

                stMov.Asunto = string.Empty;
                if (!dr.IsNull("Mov_Asunto"))
                {
                    stMov.Asunto = dr["Mov_Asunto"].ToString();
                }

                stMov.DocumentoID = -1;
                if (!dr.IsNull("Doc_Id"))
                {
                    if (dr["Doc_Id"].ToString()!=string.Empty)
                    {
                        stMov.DocumentoID = Int32.Parse(dr["Doc_Id"].ToString());
                    }
                }

                //stMov.FechaDocumento = null;
                if (!dr.IsNull("Doc_FechaCreacion"))
                {
                    stMov.FechaDocumento = (DateTime)dr["Doc_FechaCreacion"];
                }

               ///stMov.FechaMovimiento = null;
                if (!dr.IsNull("Mov_FechaCreacion"))
                {
                    stMov.FechaMovimiento = (DateTime)dr["Mov_FechaCreacion"];
                }

                //stMov.FechaVencimiento = null;
                if (!dr.IsNull("Mov_FechaVenc"))
                {
                    stMov.FechaVencimiento = (DateTime)dr["Mov_FechaVenc"];
                }


                stMov.MovimientoID = - 1;
                if (!dr.IsNull("Mov_Id"))
                {
                    if (dr["Mov_Id"].ToString()!=string.Empty)
                    {
                        stMov.MovimientoID = Int32.Parse(dr["Mov_Id"].ToString());
                    }
                }

                stMov.NUR = string.Empty;
                if (!dr.IsNull("Mov_NUR"))
                {
                    stMov.NUR = dr["Mov_NUR"].ToString();
                }

                stMov.Remitente = string.Empty;
                if (!dr.IsNull("Mov_Remitente"))
                {
                    stMov.Remitente = dr["Mov_Remitente"].ToString();
                }


                stMov.Resumen = dr["Mov_Resumen"].ToString();
                if (!dr.IsNull("Mov_Resumen"))
                {
                    stMov.Resumen = dr["Mov_Resumen"].ToString();
                }

                stMov.Numero_Silpa = -1;
                if (!dr.IsNull("NUMERO_SILPA"))
                {
                    stMov.Numero_Silpa = Int64.Parse(dr["NUMERO_SILPA"].ToString());
                }

                // LACH - 03/03/2010 Numero vital compuesto
                stMov.Str_numero_vital = string.Empty;
                if (!dr.IsNull("SOL_NUMERO_SILPA"))
                {
                    stMov.Str_numero_vital = dr["SOL_NUMERO_SILPA"].ToString();
                }

                
                stMov.Representante_Legal = -1;
                if (!dr.IsNull("REPRESENTANTE_LEGAL") && dr["REPRESENTANTE_LEGAL"].ToString() != string.Empty)
                {
                    stMov.Representante_Legal = int.Parse(dr["REPRESENTANTE_LEGAL"].ToString());
                }
                //09-jun-2010 - aegb: incidencia 1827
                stMov.Apoderado = -1;
                if (!dr.IsNull("APODERADO") && dr["APODERADO"].ToString() != string.Empty)
                {
                    stMov.Apoderado = int.Parse(dr["APODERADO"].ToString());
                }

                stMov.Solicitante = -1;
                if (!dr.IsNull("ID_SOLICITANTE"))
                {
                    stMov.Solicitante = int.Parse(dr["ID_SOLICITANTE"].ToString());
                }

                stMov.Dir_Remitente = string.Empty;
                if (!dr.IsNull("Mov_DirRemitente"))
                {
                    stMov.Dir_Remitente =  dr["Mov_DirRemitente"].ToString();
                }

                stMov.Nombre_Representante_Legal = string.Empty;
                stMov.Nur_Asociado = string.Empty;
                stMov.Observaciones = string.Empty;

                stMov.Sector = -1;
                if (!dr.IsNull("SECTOR"))
                {
                    stMov.Sector = int.Parse(dr["SECTOR"].ToString());
                }

                stMov.Sector_Hijo = -1;
                if (!dr.IsNull("SECTOR_HIJO"))
                {
                    stMov.Sector_Hijo = int.Parse(dr["SECTOR_HIJO"].ToString());
                }

                stMov.Tipo_Tramite = -1;
                if (!dr.IsNull("TIPO_TRAMITE"))
                {
                    stMov.Tipo_Tramite = int.Parse(dr["TIPO_TRAMITE"].ToString());
                }


                stMov.Id_Remitente = -1;
                if (!dr.IsNull("ID_REMITENTE"))
                {
                    stMov.Id_Remitente = int.Parse(dr["ID_REMITENTE"].ToString());
                }

                // Si tiene datos en la ubicación:
                if (ds_data.Tables.Count>1) 
                {
                    if (ds_data.Tables[1].Rows.Count > 0)
                    {
                        stMov.Ubicaciones = new List<UbicacionProyecto>();

                        foreach (DataRow rowU in ds_data.Tables[2].Rows)
                        {
                            UbicacionProyecto ubicacion = new UbicacionProyecto();
                            string filtro = "idFormInstance = " + rowU["idFormInstance"].ToString();
                            DataRow[] rows = ds_data.Tables[1].Select(filtro);
                            if (rows.Length > 0)
                                foreach (DataRow row in rows)
                                {
                                    int valor = -1;
                                    int result = 0;
                                    if (int.TryParse(row["DATO"].ToString(), out result))
                                    {
                                        if (result > 0)
                                            valor = result;
                                    }

                                    switch (row["TEXTO"].ToString())
                                    {
                                        case EnumUbicacion.departamento:
                                            ubicacion.DepId = valor;
                                            ubicacion.DepNombre = row["NOMBRE"].ToString();
                                            break;
                                        case EnumUbicacion.municipio:
                                            ubicacion.MunId = valor;
                                            ubicacion.MunNombre = row["NOMBRE"].ToString();
                                            break;
                                        case EnumUbicacion.corregimiento:
                                            ubicacion.CorId = valor;
                                            ubicacion.CorNombre = row["NOMBRE"].ToString();
                                            break;
                                        case EnumUbicacion.vereda:
                                            ubicacion.VerId = valor;
                                            ubicacion.VerNombre = row["NOMBRE"].ToString();
                                            break;
                                        case EnumUbicacion.area:
                                            ubicacion.AreId = valor;
                                            ubicacion.AreNombre = row["NOMBRE"].ToString();
                                            break;
                                        case EnumUbicacion.zona:
                                            ubicacion.ZonId = valor;
                                            ubicacion.ZonNombre = row["NOMBRE"].ToString();
                                            break;
                                        case EnumUbicacion.subzona:
                                            ubicacion.SubId = valor;
                                            ubicacion.SubNombre = row["NOMBRE"].ToString();
                                            break;
                                    }
                                }
                            stMov.Ubicaciones.Add(ubicacion);
                        }
                    }
                }
            }
            else
            {
                stMov.MovimientoID = -1;
            }

            return stMov;
        }

        /// <summary>
        /// ok
        /// </summary>
        /// <param name="str_movimientos"></param>
        /// <returns></returns>
        public DataSet listarGrupoMovimientos(string str_movimientos)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { str_movimientos };

                DbCommand cmd = db.GetStoredProcCommand("COR_XS_SILA_LST_DOCUMENTO_IDS", parametros);

                DataSet dsResultado = db.ExecuteDataSet(cmd);

                return dsResultado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            //try
            //{
            //    //ConfigurationManager.ConnectionStrings["DBCorrespondencia"].ConnectionString;
                
            //    /// Se construye el query:
            //    string qry = "SELECT DISTINCT M.*, D.*, DD.* ";
            //    qry = qry + " FROM DBO.MOVIMIENTO_DOCUMENTO M ";
            //    qry = qry + " INNER JOIN DBO.DOCUMENTO D ON D.DOC_ID = M.DOC_ID AND D.DOC_ANNO = M.DOC_ANNO ";
            //    qry = qry + " INNER JOIN DBO.DEP_DESTINATARIA_DOCUMENTO DD ON DD.MOV_ID = M.MOV_ID ";
            //    qry = qry + " WHERE 1=1 ";

            //    if (str_movimientos.Length > 0)
            //    {
            //        qry = qry + " AND M.MOV_ID IN ( " + str_movimientos + " ) ";
            //    }
            //    else 
            //    {
            //        qry = qry + " AND M.MOV_ID IN (-1) ";
            //    }

            //    qry = qry + "ORDER BY M.MOV_FECHACREACION DESC ";

            //    SqlDatabase db = new SqlDatabase(ConfigurationManager.ConnectionStrings["DBCorrespondencia"].ConnectionString);
            //    DbCommand cmd = db.GetSqlStringCommand(qry);
            //    DataSet dsResultado = db.ExecuteDataSet(cmd);
            //    return dsResultado;
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
            
            /*
             * string _strConexion = ConfigurationManager.ConnectionStrings["eFormBuilderConnectionString"].ConnectionString;
        string _strQuery = "select fieldbyforminstance.idForminstance, Field.Type, Field.Text,  "
                           +"fieldbyforminstance.Value, Field.sourcesql from fieldbyforminstance "
                           +"inner join field on fieldbyforminstance.IdField = Field.Id "
                           +"where fieldbyforminstance.value = @VALOR";
        SqlConnection _con = new SqlConnection(_strConexion);
        _con.Open();
        SqlCommand _com = new SqlCommand(_strQuery, _con);
        _com.Parameters.AddWithValue("VALOR", txt_numero.Text);
             */


        }

        
        /// <summary>
        /// Obtiene los datos de la radicación para crear la carpeta
        /// </summary>
        /// <param name="str_movimientos"></param>
        /// <returns></returns>
        public DataSet ObtenerDatosCrearCarpeta(int intIdRadicacion)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { intIdRadicacion };

                DbCommand cmd = db.GetStoredProcCommand("COR_OBTENER_DATOS_CREAR_CARPETA", parametros);

                DataSet dsResultado = db.ExecuteDataSet(cmd);

                return dsResultado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Método  que permite la consulta de los datos de radicación  y numero silpa
        /// desde gen_radicacion
        /// </summary>
        /// <param name="intIdRadicacion"></param>
        /// <returns></returns>
        public DataSet ObtenerDatosActividadNueva(Int64 intIdRadicacion) 
        { 
             try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { intIdRadicacion };

                DbCommand cmd = db.GetStoredProcCommand("COR_CREAR_ACTIVIDAD_NUEVA", parametros);

                DataSet dsResultado = db.ExecuteDataSet(cmd);

                return dsResultado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Método que actualiza la nueva autoridad ambiental
        /// </summary>
        /// <param name="intIdRadicacion"></param>
        /// <param name="intAutIdAsignada">identificador de la autoridad ambiental asignada</param>
        /// <param name="intAutIdEntrega">identificador de la autoridad ambiental anterior</param>
        /// <returns>exito / fracaso de la operación </returns>
        public bool ReasignarAutoridadRadicacion(Int64 intIdRadicacion, int intAutIdAsignada, int intAutIdEntrega)
        { 
             try
            {
             
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { intAutIdAsignada, intAutIdEntrega, intIdRadicacion };

                DbCommand cmd = db.GetStoredProcCommand("COR_REASIGNAR_AUTORIDAD_RADICACION", parametros);

                int i = db.ExecuteNonQuery(cmd);

                if (i > 0)
                {
                    return true;
                }
                else 
                { 
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Método para obtener el listado de correspondencia adjunto por un expediente
        /// SOFT - NETCO - HAVA
        /// 11 -FEB -10
        /// </summary>
        /// <param name="intIdExpediente">identificador del expediente en SilaMc</param>
        /// <param name="intAutId">Identificador de la autoridad ambiental en SilaMc</param>
        /// <returns>string: XML: con los datos</returns>
        public DataSet CorresPondenciaPorExpediente(string numeroSilpa, int autId)
        { 
            try
            {
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());

                object[] parametros = new object[] { numeroSilpa, autId };

                DbCommand cmd = db.GetStoredProcCommand("COR_OBTENER_CORRESPONDENCIA_RADICACION", parametros);

                DataSet ds = db.ExecuteDataSet(cmd);

                return ds;
                //55
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Metodo que permite insertar el IdentificacionRecurso
        /// a partir de una entidad de tipo IdentificacionRecurso
        /// que implementa la interfaz IEntity
        /// </summary>
        /// <param name="obj"></param>
        public Int32 VerificarAutoridadAmbiental(string nombreAutoridadAmbiental)
        {
            try
            {
                Int32 codigo = 0;
                SqlDatabase db = new SqlDatabase(objConfiguracion.SilpaCnx.ToString());
                object[] parametros = new object[] { nombreAutoridadAmbiental};
                DbCommand cmd = db.GetStoredProcCommand("NOT_VERIFICA_CODIGO_AUTORIDAD_AMBIENTAL", parametros);
                DataSet dsResultado = db.ExecuteDataSet(cmd);

                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    codigo = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["CODIGO_AA"]);
                }
                else
                {
                    codigo = -1;
                }

                return codigo;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    

    }
}