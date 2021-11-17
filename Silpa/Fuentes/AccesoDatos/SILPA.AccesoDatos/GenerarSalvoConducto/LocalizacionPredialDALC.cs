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
    public class LocalizacionPredialDALC
    {
        /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        public LocalizacionPredialDALC()
        {
            objConfiguracion = new Configuracion();
        }


        /// <summary>
        /// Metodo que permite insertar el IdentificacionRecurso
        /// a partir de una entidad de tipo IdentificacionRecurso
        /// que implementa la interfaz IEntity
        /// </summary>
        /// <param name="obj"></param>
        public void InsertarLocalizacionPredial(LocalizacionPredial obj, SqlConnection db, SqlTransaction transaction)
        {
            try
            {
               
                object var1 = DBNull.Value, var2 = DBNull.Value, var3 = DBNull.Value, var4 = DBNull.Value, var5 = DBNull.Value,
                       var6 = DBNull.Value, var7 = DBNull.Value, var8 = DBNull.Value, var9 = DBNull.Value;


                if (obj.GetType() == typeof(LocalizacionPredial))
                {

                    var1 = 0;

                    if (((LocalizacionPredial)obj).NombrePredio != null)
                    {
                        var2 = ((LocalizacionPredial)obj).NombrePredio;
                    }

                    if (((LocalizacionPredial)obj).ActoAdministrativo != null)
                    {
                        var3 = ((LocalizacionPredial)obj).ActoAdministrativo;
                    }

                    if (((LocalizacionPredial)obj).TipoPropiedadPredio != null)
                    {
                        var4 = ((LocalizacionPredial)obj).TipoPropiedadPredio.IDTipoPropiedadPredio;
                    }

                    if (((LocalizacionPredial)obj).OtroRegimenPropietario != null)
                    {
                        var5 = ((LocalizacionPredial)obj).OtroRegimenPropietario;
                    }

                    if (((LocalizacionPredial)obj).NumeroMatricula != null)
                    {
                        var6 = ((LocalizacionPredial)obj).NumeroMatricula;
                    }

                    if (((LocalizacionPredial)obj).Vereda != null)
                    {
                        var7 = ((LocalizacionPredial)obj).Vereda.IDVereda;
                    }

                    if (((LocalizacionPredial)obj).Municipio != null)
                    {
                        var8 = ((LocalizacionPredial)obj).Municipio;
                    }

                    if (((LocalizacionPredial)obj).Departamento != null)
                    {
                        var9 = ((LocalizacionPredial)obj).Departamento;
                    }

                   
                    
                }

                SqlCommand cmd = new SqlCommand("SALP_INSERTAR_LOCALIZACION_PREDIAL", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SAL_NOMBRE_PREDIO", var2));
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_ACTOADMINISTRATIVO", var3));
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_TIPOPROPIEDADPREDIO ", var4));
                cmd.Parameters.Add(new SqlParameter("@SAL_OTROREGIMENPROPIETARIO", var5));
                cmd.Parameters.Add(new SqlParameter("@SAL_NUMEROMATRICULA ", var6));
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_VEREDA", var7));
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_MUNICIPIO", var8));
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_DEPARTAMENTO", var9));

                cmd.Transaction = transaction;
                var1 = cmd.ExecuteScalar();

                ((LocalizacionPredial)obj).IDLocalizacionPredial = int.Parse(var1.ToString());


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
        public void ActualizarLocalizacionPredial(LocalizacionPredial obj, SqlConnection db, SqlTransaction transaction)
        {
            try
            {

                object var1 = DBNull.Value, var2 = DBNull.Value, var3 = DBNull.Value, var4 = DBNull.Value, var5 = DBNull.Value,
                       var6 = DBNull.Value, var7 = DBNull.Value, var8 = DBNull.Value, var9 = DBNull.Value;


                if (obj.GetType() == typeof(LocalizacionPredial))
                {

                    if (((LocalizacionPredial)obj).IDLocalizacionPredial != null)
                    {
                        var1 = ((LocalizacionPredial)obj).IDLocalizacionPredial;
                    }

                    if (((LocalizacionPredial)obj).NombrePredio != null)
                    {
                        var2 = ((LocalizacionPredial)obj).NombrePredio;
                    }

                    if (((LocalizacionPredial)obj).ActoAdministrativo != null)
                    {
                        var3 = ((LocalizacionPredial)obj).ActoAdministrativo;
                    }

                    if (((LocalizacionPredial)obj).TipoPropiedadPredio != null)
                    {
                        var4 = ((LocalizacionPredial)obj).TipoPropiedadPredio.IDTipoPropiedadPredio;
                    }

                    if (((LocalizacionPredial)obj).OtroRegimenPropietario != null)
                    {
                        var5 = ((LocalizacionPredial)obj).OtroRegimenPropietario;
                    }

                    if (((LocalizacionPredial)obj).NumeroMatricula != null)
                    {
                        var6 = ((LocalizacionPredial)obj).NumeroMatricula;
                    }

                    if (((LocalizacionPredial)obj).Vereda != null)
                    {
                        var7 = ((LocalizacionPredial)obj).Vereda.IDVereda;
                    }

                    if (((LocalizacionPredial)obj).Municipio != null)
                    {
                        var8 = ((LocalizacionPredial)obj).Municipio;
                    }

                    if (((LocalizacionPredial)obj).Departamento != null)
                    {
                        var9 = ((LocalizacionPredial)obj).Departamento;
                    }



                }

                SqlCommand cmd = new SqlCommand("SALP_ACTUALIZAR_LOCALIZACION_PREDIAL", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_ACTOADMINISTRATIVO", var1));
                cmd.Parameters.Add(new SqlParameter("@SAL_NOMBRE_PREDIO", var2));
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_ACTOADMINISTRATIVO", var3));
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_TIPOPROPIEDADPREDIO ", var4));
                cmd.Parameters.Add(new SqlParameter("@SAL_OTROREGIMENPROPIETARIO", var5));
                cmd.Parameters.Add(new SqlParameter("@SAL_NUMEROMATRICULA ", var6));
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_VEREDA", var7));
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_MUNICIPIO", var8));
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_DEPARTAMENTO", var9));

                cmd.Transaction = transaction;
                cmd.ExecuteScalar();

            
               

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
        public List<LocalizacionPredial> ConsultarLocalizacionPredial(Int32 _actoAdministrativo, SqlConnection db, SqlTransaction transaction)
        {
            try
            {
                List<LocalizacionPredial> _respuesta = new List<LocalizacionPredial>(); ;
                object var1 = DBNull.Value, var2 = DBNull.Value, var3 = DBNull.Value, var4 = DBNull.Value, var5 = DBNull.Value;


                if (_actoAdministrativo != null)
                {
                    var1 = _actoAdministrativo;
                }


                SqlCommand cmd = new SqlCommand("SALP_CONSULTAR_LOCALIZACION_PREDIAL", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_ACTOADMINISTRATIVO", var1));

                cmd.Transaction = transaction;

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        LocalizacionPredial _objRespuesta = new LocalizacionPredial();
                        if (reader["SAL_ID_LOCALIZACIONPREDIAL"].ToString() != "")
                        {
                            _objRespuesta.IDLocalizacionPredial = Convert.ToInt32(reader["SAL_ID_LOCALIZACIONPREDIAL"]);
                        }
                        if (reader["SAL_NOMBRE_PREDIO"].ToString() != "")
                        {
                            _objRespuesta.NombrePredio = reader["SAL_NOMBRE_PREDIO"].ToString();
                        }
                        if (reader["SAL_ID_ACTOADMINISTRATIVO"].ToString() != "")
                        {
                            _objRespuesta.ActoAdministrativo = Convert.ToInt32(reader["SAL_ID_ACTOADMINISTRATIVO"]);
                        }
                        if (reader["SAL_ID_TIPOPROPIEDADPREDIO"].ToString() != "")
                        {
                            _objRespuesta.TipoPropiedadPredio = new TipoPropiedadPredio();
                            _objRespuesta.TipoPropiedadPredio.IDTipoPropiedadPredio = Convert.ToInt32(reader["SAL_ID_TIPOPROPIEDADPREDIO"]);
                            _objRespuesta.TipoPropiedadPredio.DescTipoPropiedadPredio = reader["DESCTP"].ToString();
                        }
                        if (reader["SAL_OTROREGIMENPROPIETARIO"].ToString() != "")
                        {
                            _objRespuesta.OtroRegimenPropietario = reader["SAL_OTROREGIMENPROPIETARIO"].ToString();
                        }
                        if (reader["SAL_NUMEROMATRICULA"].ToString() != "")
                        {
                            _objRespuesta.NumeroMatricula = reader["SAL_NUMEROMATRICULA"].ToString();
                        }
                        if (reader["SAL_ID_VEREDA"].ToString() != "")
                        {
                            _objRespuesta.Vereda = new Vereda();
                            _objRespuesta.Vereda.IDVereda = Convert.ToInt32(reader["SAL_ID_VEREDA"]);
                            _objRespuesta.Vereda.DescVereda = reader["DESCv"].ToString();
                        }
                        if (reader["SAL_ID_MUNICIPIO"].ToString() != "")
                        {
                            _objRespuesta.Municipio = Convert.ToInt32(reader["SAL_ID_MUNICIPIO"]);
                        }
                        if (reader["SAL_ID_DEPARTAMENTO"].ToString() != "")
                        {
                            _objRespuesta.Departamento = Convert.ToInt32(reader["SAL_ID_DEPARTAMENTO"]);
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
