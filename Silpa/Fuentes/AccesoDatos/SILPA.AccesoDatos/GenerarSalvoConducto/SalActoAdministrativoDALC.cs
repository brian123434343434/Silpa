using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace SILPA.AccesoDatos.GenerarSalvoConducto
{
    public class SalActoAdministrativoDALC
    {
         /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        public SalActoAdministrativoDALC()
        {
            objConfiguracion = new Configuracion();
        }


        /// <summary>
        /// Metodo que permite insertar el IdentificacionRecurso
        /// a partir de una entidad de tipo IdentificacionRecurso
        /// que implementa la interfaz IEntity
        /// </summary>
        /// <param name="obj"></param>
        public void InsertarSalActoAdministrativo(ref SalActoAdministrativo obj, SqlConnection db, SqlTransaction transaction)
        {
            try
            {
                
                
             
                object var1 = DBNull.Value, var2 = DBNull.Value, var3 = DBNull.Value, var4 = DBNull.Value, var5 = DBNull.Value,
                       var6 = DBNull.Value, var7 = DBNull.Value, var8 = DBNull.Value, var9 = DBNull.Value, var10 = DBNull.Value, var11 = DBNull.Value,
                       var12 = DBNull.Value;

                if (obj.GetType() == typeof(SalActoAdministrativo))
                {

                    var1 = 0;

                    if (((SalActoAdministrativo)obj).NumeroActo != null)
                    {
                        var2 = ((SalActoAdministrativo)obj).NumeroActo;
                    }

                    if (((SalActoAdministrativo)obj).NumeroVital != null)
                    {
                        var3 = ((SalActoAdministrativo)obj).NumeroVital;
                    }

                    if (((SalActoAdministrativo)obj).AutoridadAmbiental != null)
                    {
                        var4 = ((SalActoAdministrativo)obj).AutoridadAmbiental;
                    }

                    if (((SalActoAdministrativo)obj).FormaOtorgamiento != null)
                    {
                        var5 = ((SalActoAdministrativo)obj).FormaOtorgamiento.IDFormaOtorgamiento;
                    }

                    if (((SalActoAdministrativo)obj).Fecha != null)
                    {
                        var6 = ((SalActoAdministrativo)obj).Fecha;
                    }

                    if (((SalActoAdministrativo)obj).FechaInicioVigencia != null)
                    {
                        var7 = ((SalActoAdministrativo)obj).FechaInicioVigencia;
                    }

                    if (((SalActoAdministrativo)obj).FechaFinalVigencia != null)
                    {
                        var8 = ((SalActoAdministrativo)obj).FechaFinalVigencia;
                    }

                    if (((SalActoAdministrativo)obj).Tarea != null)
                    {
                        var9 = ((SalActoAdministrativo)obj).Tarea;
                    }


                    if (((SalActoAdministrativo)obj).ResolucionSancionatoria != null)
                    {
                        var10 = ((SalActoAdministrativo)obj).ResolucionSancionatoria;
                    }


                    if (((SalActoAdministrativo)obj).EsTramitadoSila != null)
                    {
                        var11 = ((SalActoAdministrativo)obj).EsTramitadoSila;
                    }

                    if (((SalActoAdministrativo)obj).Expediente != null)
                    {
                        var12 = ((SalActoAdministrativo)obj).Expediente;
                    }

                                   
                    
                }

             
                SqlCommand cmd = new SqlCommand("SALP_INSERTAR_ACTO_ADMINISTRATIVO", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SAL_NUMACTO", var2));
                cmd.Parameters.Add(new SqlParameter("@SAL_NUMEROVITAL", var3));
                cmd.Parameters.Add(new SqlParameter("@SAL_AUTORIDADAMBIENTAL", var4));
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_FORMAOTORGAMIENTO", var5));
                cmd.Parameters.Add(new SqlParameter("@SAL_FECHA", var6));
                cmd.Parameters.Add(new SqlParameter("@SAL_FECHAINIVIGENCIA", var7));
                cmd.Parameters.Add(new SqlParameter("@SAL_FECHAFINVIGENCIA ", var8));
                cmd.Parameters.Add(new SqlParameter("@SAL_TAREA ", var9));
                cmd.Parameters.Add(new SqlParameter("@SAL_DIRD_ID ", var10));
                cmd.Parameters.Add(new SqlParameter("@SAL_TRAMITADOSILA ", var11));
                cmd.Parameters.Add(new SqlParameter("@SAL_EPX", var12));
         
                cmd.Transaction = transaction;
                var1= cmd.ExecuteScalar();
             
                ((SalActoAdministrativo)obj).IDSalActoAdministrativo = int.Parse(var1.ToString());

                

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
        public void ActualizarSalActoAdministrativo(ref SalActoAdministrativo obj, SqlConnection db, SqlTransaction transaction)
        {
            try
            {



                object var1 = DBNull.Value, var2 = DBNull.Value, var3 = DBNull.Value, var4 = DBNull.Value, var5 = DBNull.Value,
                       var6 = DBNull.Value, var7 = DBNull.Value, var8 = DBNull.Value, var9 = DBNull.Value, var10 = DBNull.Value, var11 = DBNull.Value,
                       var12 = DBNull.Value;


                if (obj.GetType() == typeof(SalActoAdministrativo))
                {

                    if (((SalActoAdministrativo)obj).IDSalActoAdministrativo != null)
                    {
                        var1 = ((SalActoAdministrativo)obj).IDSalActoAdministrativo;
                    }
                    if (((SalActoAdministrativo)obj).NumeroActo != null)
                    {
                        var2 = ((SalActoAdministrativo)obj).NumeroActo;
                    }

                    if (((SalActoAdministrativo)obj).NumeroVital != null)
                    {
                        var3 = ((SalActoAdministrativo)obj).NumeroVital;
                    }

                    if (((SalActoAdministrativo)obj).AutoridadAmbiental != null)
                    {
                        var4 = ((SalActoAdministrativo)obj).AutoridadAmbiental;
                    }

                    if (((SalActoAdministrativo)obj).FormaOtorgamiento != null)
                    {
                        var5 = ((SalActoAdministrativo)obj).FormaOtorgamiento.IDFormaOtorgamiento;
                    }

                    if (((SalActoAdministrativo)obj).Fecha != null)
                    {
                        var6 = ((SalActoAdministrativo)obj).Fecha;
                    }

                    if (((SalActoAdministrativo)obj).FechaInicioVigencia != null)
                    {
                        var7 = ((SalActoAdministrativo)obj).FechaInicioVigencia;
                    }

                    if (((SalActoAdministrativo)obj).FechaFinalVigencia != null)
                    {
                        var8 = ((SalActoAdministrativo)obj).FechaFinalVigencia;
                    }

                    if (((SalActoAdministrativo)obj).Tarea != null)
                    {
                        var9 = ((SalActoAdministrativo)obj).Tarea;
                    }

                    if (((SalActoAdministrativo)obj).ResolucionSancionatoria != null)
                    {
                        var10 = ((SalActoAdministrativo)obj).ResolucionSancionatoria;
                    }


                    if (((SalActoAdministrativo)obj).EsTramitadoSila != null)
                    {
                        var11 = ((SalActoAdministrativo)obj).EsTramitadoSila;
                    }

                    if (((SalActoAdministrativo)obj).Expediente != null)
                    {
                        var12 = ((SalActoAdministrativo)obj).Expediente;
                    }



                }


                SqlCommand cmd = new SqlCommand("SALP_ACTUALIZAR_ACTO_ADMINISTRATIVO", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_ACTOADMINISTRATIVO", var1));
                cmd.Parameters.Add(new SqlParameter("@SAL_NUMACTO", var2));
                cmd.Parameters.Add(new SqlParameter("@SAL_NUMEROVITAL", var3));
                cmd.Parameters.Add(new SqlParameter("@SAL_AUTORIDADAMBIENTAL", var4));
                cmd.Parameters.Add(new SqlParameter("@SAL_ID_FORMAOTORGAMIENTO", var5));
                cmd.Parameters.Add(new SqlParameter("@SAL_FECHA", var6));
                cmd.Parameters.Add(new SqlParameter("@SAL_FECHAINIVIGENCIA", var7));
                cmd.Parameters.Add(new SqlParameter("@SAL_FECHAFINVIGENCIA ", var8));
                cmd.Parameters.Add(new SqlParameter("@SAL_TAREA ", var9));
                cmd.Parameters.Add(new SqlParameter("@SAL_DIRD_ID ", var10));
                cmd.Parameters.Add(new SqlParameter("@SAL_TRAMITADOSILA ", var11));
                cmd.Parameters.Add(new SqlParameter("@SAL_EXP ", var12));

                cmd.Transaction = transaction;
                cmd.ExecuteScalar();

         
                

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        /// <summary>
        /// Metodo que permite actualizar el IdentificacionRecurso
        /// a partir de una entidad de tipo IdentificacionRecurso
        /// que implementa la interfaz IEntity
        /// </summary>
        /// <param name="obj"></param>
        public SILPA.AccesoDatos.GenerarSalvoConducto.SalActoAdministrativo ConsultarSalActoAdministrativo(string _numeroSilpa, string _autoridadAmbiental, int tar_id, SqlConnection db, SqlTransaction transaction)
        {
            try
            {
               SILPA.AccesoDatos.GenerarSalvoConducto.SalActoAdministrativo _respuesta = null;

                object var1 = DBNull.Value, var2 = DBNull.Value, var3 = DBNull.Value, var4 = DBNull.Value, var5 = DBNull.Value,
                      var6 = DBNull.Value, var7 = DBNull.Value, var8 = DBNull.Value;

                if (_numeroSilpa != null)
                {
                    var1 = _numeroSilpa;
                }

                if (_autoridadAmbiental != null)
                {
                    var2 = _autoridadAmbiental;
                }

               
                if (tar_id != null)
                {
                    var3 = tar_id;
                }

                
                 SqlCommand cmd = new SqlCommand("SALP_CONSULTAR_ACTO_ADMINISTRATIVO", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@P_NUMEROSILPA", var1));
                cmd.Parameters.Add(new SqlParameter("@P_AAID", var2));
                cmd.Parameters.Add(new SqlParameter("@P_TARID", var3));
                
               cmd.Transaction = transaction;

               using (IDataReader reader = cmd.ExecuteReader())
               {
                   while (reader.Read())
                   {
                       _respuesta = new SalActoAdministrativo();

                       if (reader["SAL_ID_ACTOADMINISTRATIVO"].ToString() != "")
                       {
                           _respuesta.IDSalActoAdministrativo = Convert.ToInt32(reader["SAL_ID_ACTOADMINISTRATIVO"]);
                       }

                       _respuesta.NumeroActo = reader["SAL_NUMACTO"].ToString();
                       _respuesta.NumeroVital = reader["SAL_NUMEROVITAL"].ToString();

                       if (reader["SAL_AUTORIDADAMBIENTAL"].ToString() != "")
                       {
                           _respuesta.AutoridadAmbiental = Convert.ToInt32(reader["SAL_AUTORIDADAMBIENTAL"]);
                       }


                       if (reader["SAL_ID_FORMAOTORGAMIENTO"].ToString() != "")
                       {
                           _respuesta.FormaOtorgamiento = new FormaOtorgamiento();
                           _respuesta.FormaOtorgamiento.IDFormaOtorgamiento = Convert.ToInt32(reader["SAL_ID_FORMAOTORGAMIENTO"]);
                       }
                       _respuesta.FormaOtorgamiento.DesFormaOtorgamiento = reader["SAL_DESCRIPCION"].ToString();

                       if (reader["SAL_FECHA"].ToString() != "")
                       {
                           _respuesta.Fecha = Convert.ToDateTime(reader["SAL_FECHA"]);
                       }

                       if (reader["SAL_FECHAINIVIGENCIA"].ToString() != "")
                       {
                           //_respuesta.FechaInicioVigencia = Convert.ToDateTime(reader["SAL_FECHAINIVIGENCIA"]);
                       }

                       if (reader["SAL_FECHAFINVIGENCIA"].ToString() != "")
                       {
                           //_respuesta.FechaFinalVigencia = Convert.ToDateTime(reader["SAL_FECHAFINVIGENCIA"]);
                       }

                       if (reader["SAL_FECHACREACION"].ToString() != "")
                       {
                           //_respuesta.FechaCreacion = Convert.ToDateTime(reader["SAL_FECHACREACION"]);
                       }

                       if (reader["SAL_TRAMITADOSILA"].ToString() != "")
                       {

                           _respuesta.EsTramitadoSila = Convert.ToBoolean(reader["SAL_TRAMITADOSILA"]);
                       }

                       if (reader["SAL_DIRD_ID"].ToString() != "")
                       {
                           _respuesta.ResolucionSancionatoria = Convert.ToInt32(reader["SAL_DIRD_ID"]);
                       }

                       if (reader["SAL_EXPEDIENTE_ID"].ToString() != "")
                       {
                           _respuesta.Expediente = reader["SAL_EXPEDIENTE_ID"].ToString();
                       }
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
