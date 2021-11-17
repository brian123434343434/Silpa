using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using System.Data;

namespace SILPA.AccesoDatos.GenerarSalvoConducto
{
    public class SolicitudProductoDALC
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        public SolicitudProductoDALC()
        {
            objConfiguracion = new Configuracion();
        }


        /// <summary>
        /// Metodo que permite insertar el SolicitudProducto
        /// a partir de una entidad de tipo SolicitudProducto
        /// que implementa la interfaz IEntity
        /// </summary>
        /// <param name="obj"></param>
        public void InsertarSolicitudProducto(SolicitudProducto obj, SqlConnection db, SqlTransaction transaction)
        {
            try
            {

                object var1 = DBNull.Value, var2 = DBNull.Value, var3 = DBNull.Value, var4 = DBNull.Value, var5 = DBNull.Value,
                       var6 = DBNull.Value, var7 = DBNull.Value, var8 = DBNull.Value, var9 = DBNull.Value, var10 = DBNull.Value;


                if (obj.GetType() == typeof(SolicitudProducto))
                {

                    var1 = 0;

                    if (((SolicitudProducto)obj).IdentificacionRecurso != null)
                    {
                        var2 = ((SolicitudProducto)obj).IdentificacionRecurso;
                    }

                    if (((SolicitudProducto)obj).NombreCientifico != null)
                    {
                        var3 = ((SolicitudProducto)obj).NombreCientifico.IDNombreCientifico;
                    }

                    if (((SolicitudProducto)obj).NombreComun != null)
                    {
                        var4 = ((SolicitudProducto)obj).NombreComun.IDNombreComun;
                    }

                    if (((SolicitudProducto)obj).Producto != null)
                    {
                        var5 = ((SolicitudProducto)obj).Producto.IDSalProducto;
                    }

                    if (((SolicitudProducto)obj).OtroProducto != null)
                    {
                        var6 = ((SolicitudProducto)obj).OtroProducto;
                    }

                    if (((SolicitudProducto)obj).CantidadBruto != null)
                    {
                        var7 = ((SolicitudProducto)obj).CantidadBruto;
                    }

                    if (((SolicitudProducto)obj).CantidadTransformado != null)
                    {
                        var8 = ((SolicitudProducto)obj).CantidadTransformado;
                    }

                    if (((SolicitudProducto)obj).UnidadMetrica != null)
                    {
                        var9 = ((SolicitudProducto)obj).UnidadMetrica.IDUnidadMetrica;
                    }


                }

                SqlCommand cmd = new SqlCommand("SALP_INSERTAR_SOLICITUD_PRODUCTO", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_IDENTIFICACIONRECURSO", var2));
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_NOMBRECIENTIFICO", var3));
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_NOMBRECOMUN", var4));
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_PRODUCTO", var5));
                cmd.Parameters.Add(new SqlParameter("@SAL_OTROPRODUCTO", var6));
                cmd.Parameters.Add(new SqlParameter("@CANTIDADBRUTO", var7));
                cmd.Parameters.Add(new SqlParameter("@CANTIDADTRANSFORMADO ", var8));
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_UNIDADMEDIDA", var9));

                cmd.Transaction = transaction;
                var1 = cmd.ExecuteScalar();

                ((SolicitudProducto)obj).IDSolicitudProducto = int.Parse(var1.ToString());


                return;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Metodo que permite insertar el SolicitudProducto
        /// a partir de una entidad de tipo SolicitudProducto
        /// que implementa la interfaz IEntity
        /// </summary>
        /// <param name="obj"></param>
        public void ActualizarSolicitudProducto(SolicitudProducto obj, SqlConnection db, SqlTransaction transaction)
        {
            try
            {

                object var1 = DBNull.Value, var2 = DBNull.Value, var3 = DBNull.Value, var4 = DBNull.Value, var5 = DBNull.Value,
                       var6 = DBNull.Value, var7 = DBNull.Value, var8 = DBNull.Value, var9 = DBNull.Value, var10 = DBNull.Value;


                if (obj.GetType() == typeof(SolicitudProducto))
                {

                    if (((SolicitudProducto)obj).IDSolicitudProducto != null)
                    {
                        var1 = ((SolicitudProducto)obj).IDSolicitudProducto;
                    }

                    if (((SolicitudProducto)obj).IdentificacionRecurso != null)
                    {
                        var2 = ((SolicitudProducto)obj).IdentificacionRecurso;
                    }

              
                    if (((SolicitudProducto)obj).NombreCientifico != null)
                    {
                        var3 = ((SolicitudProducto)obj).NombreCientifico.IDNombreCientifico;
                    }

                    if (((SolicitudProducto)obj).NombreComun != null)
                    {
                        var4 = ((SolicitudProducto)obj).NombreComun.IDNombreComun;
                    }

                    if (((SolicitudProducto)obj).Producto != null)
                    {
                        var5 = ((SolicitudProducto)obj).Producto.IDSalProducto;
                    }

                    if (((SolicitudProducto)obj).OtroProducto != null)
                    {
                        var6 = ((SolicitudProducto)obj).OtroProducto;
                    }

                    if (((SolicitudProducto)obj).CantidadBruto != null)
                    {
                        var7 = ((SolicitudProducto)obj).CantidadBruto;
                    }

                    if (((SolicitudProducto)obj).CantidadTransformado != null)
                    {
                        var8 = ((SolicitudProducto)obj).CantidadTransformado;
                    }

                    if (((SolicitudProducto)obj).UnidadMetrica != null)
                    {
                        var9 = ((SolicitudProducto)obj).UnidadMetrica.IDUnidadMetrica;
                    }


                }

                SqlCommand cmd = new SqlCommand("SALP_ACTUALIZAR_SOLICITUD_PRODUCTO", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_SOLICITUDPRODUCTO", var1));
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_IDENTIFICACIONRECURSO", var2));
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_NOMBRECIENTIFICO", var3));
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_NOMBRECOMUN", var4));
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_PRODUCTO", var5));
                cmd.Parameters.Add(new SqlParameter("@SAL_OTROPRODUCTO", var6));
                cmd.Parameters.Add(new SqlParameter("@CANTIDADBRUTO", var7));
                cmd.Parameters.Add(new SqlParameter("@CANTIDADTRANSFORMADO ", var8));
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_UNIDADMEDIDA", var9));

                cmd.Transaction = transaction;
                cmd.ExecuteScalar();

               
                return;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// Metodo que permite insertar el IdentificacionRecurso
        /// a partir de una entidad de tipo IdentificacionRecurso
        /// que implementa la interfaz IEntity
        /// </summary>
        /// <param name="obj"></param>
        public List<SolicitudProducto> ConsultarSolicitudProducto(Int32 _identificacionRecurso, SqlConnection db, SqlTransaction transaction)
        {
            try
            {
                List<SolicitudProducto> _respuesta = new List<SolicitudProducto>(); ;
                object var1 = DBNull.Value, var2 = DBNull.Value, var3 = DBNull.Value, var4 = DBNull.Value, var5 = DBNull.Value;


                if (_identificacionRecurso != null)
                {
                    var1 = _identificacionRecurso;
                }


                SqlCommand cmd = new SqlCommand("SALP_CONSULTAR_SOLICITUD_PRODUCTO", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_IDENTIFICACIONRECURSO", var1));

                cmd.Transaction = transaction;

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SolicitudProducto _objRespuesta = new SolicitudProducto();
                     
                        if (reader["SAL_ID_SOLICITUDPRODUCTO"].ToString() != "")
                        {
                            _objRespuesta.IDSolicitudProducto = Convert.ToInt32(reader["SAL_ID_SOLICITUDPRODUCTO"]);
                        }
                        if (reader["SAL_ID_IDENTIFICACIONRECURSO"].ToString() != "")
                        {
                            _objRespuesta.IdentificacionRecurso = Convert.ToInt32(reader["SAL_ID_IDENTIFICACIONRECURSO"]);
                        }
                        if (reader["SAL_ID_NOMBRECIENTIFICO"].ToString() != "")
                        {
                            _objRespuesta.NombreCientifico = new NombreCientifico();
                            _objRespuesta.NombreCientifico.IDNombreCientifico = Convert.ToInt32(reader["SAL_ID_NOMBRECIENTIFICO"]);
                            _objRespuesta.NombreCientifico.DesNombreCientifico = reader["DESNCI"].ToString();
                        }
                        if (reader["SAL_ID_NOMBRECOMUN"].ToString() != "")
                        {
                            _objRespuesta.NombreComun = new NombreComun();
                            _objRespuesta.NombreComun.IDNombreComun = Convert.ToInt32(reader["SAL_ID_NOMBRECOMUN"]);
                            _objRespuesta.NombreComun.DesNombreComun = reader["DESCNCU"].ToString();
                        }
                        if (reader["SAL_ID_PRODUCTO"].ToString() != "")
                        {
                            _objRespuesta.Producto = new SalProducto();
                            _objRespuesta.Producto.IDSalProducto = Convert.ToInt32(reader["SAL_ID_PRODUCTO"]);
                            _objRespuesta.Producto.DescSalProducto = reader["DESCP"].ToString();
                            _objRespuesta.Producto.ClaseRecursosTrans = new ClaseRecursosTrans();
                            _objRespuesta.Producto.ClaseRecursosTrans.IDClaseRecursosTrans = Convert.ToInt32(reader["SAL_ID_CLASERECURSOTRANS"]);
                            _objRespuesta.Producto.ClaseRecursosTrans.DesClaseRecursosTrans = reader["DESCCA"].ToString();
                        }
                        if (reader["SAL_OTROPRODUCTO"].ToString() != "")
                        {
                            _objRespuesta.OtroProducto = reader["SAL_OTROPRODUCTO"].ToString();
                        }
                        if (reader["CANTIDADBRUTO"].ToString() != "")
                        {
                            _objRespuesta.CantidadBruto = Convert.ToInt64(reader["CANTIDADBRUTO"]);
                        }
                        if (reader["CANTIDADTRANSFORMADO"].ToString() != "")
                        {
                            _objRespuesta.CantidadTransformado = Convert.ToInt64(reader["CANTIDADTRANSFORMADO"]);
                        }
                        if (reader["SAL_ID_UNIDADMEDIDA"].ToString() != "")
                        {
                            _objRespuesta.UnidadMetrica = new UnidadMetrica();
                            _objRespuesta.UnidadMetrica.IDUnidadMetrica = Convert.ToInt32(reader["SAL_ID_UNIDADMEDIDA"]);
                            _objRespuesta.UnidadMetrica.DescUnidadMetrica = reader["DESCU"].ToString();
                        }



                        if (reader["SAL_FECHACREACION"].ToString() != "")
                        {
                            _objRespuesta.FechaCreacion = Convert.ToDateTime(reader["SAL_FECHACREACION"]);
                        }

                        _respuesta.Add(_objRespuesta);
                    }

                }

                return _respuesta;


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }





    }
}
