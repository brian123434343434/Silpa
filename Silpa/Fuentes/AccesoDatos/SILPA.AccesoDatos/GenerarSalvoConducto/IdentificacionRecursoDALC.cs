using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using System.Data;

namespace SILPA.AccesoDatos.GenerarSalvoConducto
{
    public class IdentificacionRecursoDALC
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        public IdentificacionRecursoDALC()
        {
            objConfiguracion = new Configuracion();
        }


        /// <summary>
        /// Metodo que permite insertar el IdentificacionRecurso
        /// a partir de una entidad de tipo IdentificacionRecurso
        /// que implementa la interfaz IEntity
        /// </summary>
        /// <param name="obj"></param>
        public void InsertarIdentificacionRecurso(IdentificacionRecurso obj, SqlConnection db, SqlTransaction transaction)
        {
            try
            {

                object var1 = DBNull.Value, var2 = DBNull.Value, var3 = DBNull.Value, var4 = DBNull.Value, var5 = DBNull.Value;


                if (obj.GetType() == typeof(IdentificacionRecurso))
                {

                    var1 = 0;

                    if (((IdentificacionRecurso)obj).ActoAdministrativo != null)
                    {
                        var2 = ((IdentificacionRecurso)obj).ActoAdministrativo;
                    }

                    if (((IdentificacionRecurso)obj).ClaseAprovechamiento != null)
                    {
                        var3 = ((IdentificacionRecurso)obj).ClaseAprovechamiento.IDClaseAprovechamiento;
                    }

                    if (((IdentificacionRecurso)obj).ClaseRecursosTrans != null)
                    {
                        var4 = ((IdentificacionRecurso)obj).ClaseRecursosTrans.IDClaseRecursosTrans;
                    }

                    if (((IdentificacionRecurso)obj).FinalidadAprovechamiento != null)
                    {
                        var5 = ((IdentificacionRecurso)obj).FinalidadAprovechamiento.IDFinalidadAprovechamiento;
                    }



                }


                SqlCommand cmd = new SqlCommand("SALP_INSERTAR_IDENTIFICACION_RECURSO", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_ACTOADMINISTRATIVO", var2));
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_CLASEAPROVECHAMIENTO", var3));
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_CLASERECURSOTRANS", var4));
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_FINALIDADAPROVECHAMIENTO", var5));

                cmd.Transaction = transaction;
                var1 = cmd.ExecuteScalar();

                ((IdentificacionRecurso)obj).IDIdentificacionRecurso = int.Parse(var1.ToString());


                if (((IdentificacionRecurso)obj).ListaSolicitudProducto != null)
                {
                    foreach (SolicitudProducto item in ((IdentificacionRecurso)obj).ListaSolicitudProducto)
                    {
                        item.IdentificacionRecurso = ((IdentificacionRecurso)obj).IDIdentificacionRecurso;
                        SolicitudProductoDALC sol = new SolicitudProductoDALC();
                        sol.InsertarSolicitudProducto(item, db, transaction);
                    }
                }

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
        public void ActualizarIdentificacionRecurso(IdentificacionRecurso obj, SqlConnection db, SqlTransaction transaction)
        {
            try
            {

                object var1 = DBNull.Value, var2 = DBNull.Value, var3 = DBNull.Value, var4 = DBNull.Value, var5 = DBNull.Value;


                if (obj.GetType() == typeof(IdentificacionRecurso))
                {

                    if (((IdentificacionRecurso)obj).IDIdentificacionRecurso != null)
                    {
                        var1 = ((IdentificacionRecurso)obj).IDIdentificacionRecurso;
                    }

                    if (((IdentificacionRecurso)obj).ActoAdministrativo != null)
                    {
                        var2 = ((IdentificacionRecurso)obj).ActoAdministrativo;
                    }

                    if (((IdentificacionRecurso)obj).ClaseAprovechamiento != null)
                    {
                        var3 = ((IdentificacionRecurso)obj).ClaseAprovechamiento.IDClaseAprovechamiento;
                    }

                    if (((IdentificacionRecurso)obj).ClaseRecursosTrans != null)
                    {
                        var4 = ((IdentificacionRecurso)obj).ClaseRecursosTrans.IDClaseRecursosTrans;
                    }

                    if (((IdentificacionRecurso)obj).FinalidadAprovechamiento != null)
                    {
                        var5 = ((IdentificacionRecurso)obj).FinalidadAprovechamiento.IDFinalidadAprovechamiento;
                    }



                }


                SqlCommand cmd = new SqlCommand("SALP_ACTUALIZAR_IDENTIFICACION_RECURSO", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_IDENTIFICACIONRECURSO", var2));
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_ACTOADMINISTRATIVO", var2));
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_CLASEAPROVECHAMIENTO", var3));
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_CLASERECURSOTRANS", var4));
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_FINALIDADAPROVECHAMIENTO", var5));

                cmd.Transaction = transaction;
               cmd.ExecuteScalar();

               

                if (((IdentificacionRecurso)obj).ListaSolicitudProducto != null)
                {
                    foreach (SolicitudProducto item in ((IdentificacionRecurso)obj).ListaSolicitudProducto)
                    {
                        item.IdentificacionRecurso = ((IdentificacionRecurso)obj).IDIdentificacionRecurso;
                        SolicitudProductoDALC sol = new SolicitudProductoDALC();
                        sol.ActualizarSolicitudProducto(item, db, transaction);
                    }
                }

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
        public List<IdentificacionRecurso> ConsultarIdentificacionRecurso(Int32 _actoAdministrativo, SqlConnection db, SqlTransaction transaction)
        {
            try
            {
                List<IdentificacionRecurso> _respuesta = new List<IdentificacionRecurso>(); ;
                object var1 = DBNull.Value, var2 = DBNull.Value, var3 = DBNull.Value, var4 = DBNull.Value, var5 = DBNull.Value;


                if (_actoAdministrativo != null)
                {
                    var1 = _actoAdministrativo;
                }


                SqlCommand cmd = new SqlCommand("SALP_CONSULTAR_IDENTIFICACION_RECURSO", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_ACTOADMINISTRATIVO", var1));

                cmd.Transaction = transaction;

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        IdentificacionRecurso _objRespuesta = new IdentificacionRecurso();
                        if (reader["SAL_ID_IDENTIFICACIONRECURSO"].ToString() != "")
                        {
                            _objRespuesta.IDIdentificacionRecurso = Convert.ToInt32(reader["SAL_ID_IDENTIFICACIONRECURSO"]);
                        }
                        if (reader["SAL_ID_ACTOADMINISTRATIVO"].ToString() != "")
                        {
                            _objRespuesta.ActoAdministrativo = Convert.ToInt32(reader["SAL_ID_ACTOADMINISTRATIVO"]);
                        }
                        if (reader["SAL_ID_CLASEAPROVECHAMIENTO"].ToString() != "")
                        {
                            _objRespuesta.ClaseAprovechamiento = new ClaseAprovechamiento();
                            _objRespuesta.ClaseAprovechamiento.IDClaseAprovechamiento = Convert.ToInt32(reader["SAL_ID_CLASEAPROVECHAMIENTO"]);
                            _objRespuesta.ClaseAprovechamiento.DesClaseAprovechamiento = reader["DESCLASE"].ToString();
                        }
                        if (reader["SAL_ID_CLASERECURSOTRANS"].ToString() != "")
                        {
                            _objRespuesta.ClaseRecursosTrans = new ClaseRecursosTrans();
                            _objRespuesta.ClaseRecursosTrans.IDClaseRecursosTrans = Convert.ToInt32(reader["SAL_ID_CLASERECURSOTRANS"]);
                            _objRespuesta.ClaseRecursosTrans.DesClaseRecursosTrans = reader["DESCRT"].ToString();
                        }
                        if (reader["SAL_ID_FINALIDADAPROVECHAMIENTO"].ToString() != "")
                        {
                            _objRespuesta.FinalidadAprovechamiento = new FinalidadAprovechamiento();
                            _objRespuesta.FinalidadAprovechamiento.IDFinalidadAprovechamiento = Convert.ToInt32(reader["SAL_ID_FINALIDADAPROVECHAMIENTO"]);
                            _objRespuesta.FinalidadAprovechamiento.DesFinalidadAprovechamiento = reader["DESCF"].ToString();
                        }

                        if (reader["SAL_FECHACREACION"].ToString() != "")
                        {
                            _objRespuesta.FechaCreacion = Convert.ToDateTime(reader["SAL_FECHACREACION"]);
                        }

                      


                        _respuesta.Add(_objRespuesta);
                    }

                }


                foreach (IdentificacionRecurso item in _respuesta)
                {
                    SolicitudProductoDALC sol = new SolicitudProductoDALC();
                    item.ListaSolicitudProducto = sol.ConsultarSolicitudProducto(Convert.ToInt32(item.IDIdentificacionRecurso), db, transaction);
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
