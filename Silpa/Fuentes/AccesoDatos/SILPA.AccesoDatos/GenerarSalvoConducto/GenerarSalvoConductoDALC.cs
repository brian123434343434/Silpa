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
    public class GenerarSalvoConductoDALC
    {
         /// <summary>
        /// Objeto de configuracion de la aplicación
        /// </summary>
        private Configuracion objConfiguracion;

        public GenerarSalvoConductoDALC()
        {
            objConfiguracion = new Configuracion();
        }

        public bool InsertarAprovechamientoForesta(ref SalActoAdministrativo _actoAdministrativo)
        {
            bool _respuesta = false;
          

            SqlConnection db = new SqlConnection(objConfiguracion.SilpaCnx.ToString());
           
            db.Open();
            SqlTransaction transaction = db.BeginTransaction();

                try
                {
                    SalActoAdministrativoDALC _aa = new SalActoAdministrativoDALC();
                    _aa.InsertarSalActoAdministrativo(ref _actoAdministrativo, db, transaction);

                    if (_actoAdministrativo.ListaIdentificacionRecurso != null)
                    {
                        foreach (IdentificacionRecurso item in _actoAdministrativo.ListaIdentificacionRecurso)
                        {
                            IdentificacionRecursoDALC _i = new IdentificacionRecursoDALC();
                            item.ActoAdministrativo = _actoAdministrativo.IDSalActoAdministrativo;
                            _i.InsertarIdentificacionRecurso(item, db, transaction);
                        }
                    }
                    if (_actoAdministrativo.ListaLocalizacionPredial != null)
                    {
                        foreach (LocalizacionPredial item in _actoAdministrativo.ListaLocalizacionPredial)
                        {
                            LocalizacionPredialDALC _l = new LocalizacionPredialDALC();
                            item.ActoAdministrativo = _actoAdministrativo.IDSalActoAdministrativo;
                            _l.InsertarLocalizacionPredial(item, db, transaction);
                        }
                    }

                       
                    transaction.Commit();
                    db.Close();
                    _respuesta = true;
                    
                }
                catch
                {
                    transaction.Rollback();
                    db.Close();
                    throw;
                }

                return _respuesta;
         
        }

        public SILPA.AccesoDatos.GenerarSalvoConducto.SalActoAdministrativo ConsultarAprovechamientoForesta(string _numeroSilpa, string _autoridadAmbiental, int tar_id)
        {
            SILPA.AccesoDatos.GenerarSalvoConducto.SalActoAdministrativo _respuesta = null;

            SqlConnection db = new SqlConnection(objConfiguracion.SilpaCnx.ToString());

            db.Open();
            SqlTransaction transaction = db.BeginTransaction();

            try
            {
                SalActoAdministrativoDALC _aa = new SalActoAdministrativoDALC();
                _respuesta= _aa.ConsultarSalActoAdministrativo(_numeroSilpa, _autoridadAmbiental, tar_id, db, transaction);

                if (_respuesta != null)
                {

                    IdentificacionRecursoDALC _ir = new IdentificacionRecursoDALC();
                    _respuesta.ListaIdentificacionRecurso = _ir.ConsultarIdentificacionRecurso(Convert.ToInt32(_respuesta.IDSalActoAdministrativo), db, transaction);

                    LocalizacionPredialDALC _l = new LocalizacionPredialDALC();
                    _respuesta.ListaLocalizacionPredial = _l.ConsultarLocalizacionPredial(Convert.ToInt32(_respuesta.IDSalActoAdministrativo), db, transaction);
                }

                transaction.Commit();
                db.Close();
              

            }
            catch
            {
                transaction.Rollback();
                db.Close();
                throw;
            }

            return _respuesta;
        }

        public bool ActualizarAprovechamientoForesta(Int32 _actoAdministrativo,ref SalActoAdministrativo _objActoAdministrativo)
        {
            bool _respuesta = false;


            SqlConnection db = new SqlConnection(objConfiguracion.SilpaCnx.ToString());

            db.Open();
            SqlTransaction transaction = db.BeginTransaction();

            try
            {

                object var1 = DBNull.Value;
                var1 = _actoAdministrativo;
               


                SqlCommand cmd = new SqlCommand("SAL_ELIMINAR_PERMISO_FORESTAL", db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SAL_ACTO_ADMINISTRATIVO", var1));

                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();


                SalActoAdministrativoDALC _aa = new SalActoAdministrativoDALC();
                _aa.ActualizarSalActoAdministrativo(ref _objActoAdministrativo, db, transaction);

                if (_objActoAdministrativo.ListaIdentificacionRecurso != null)
                {
                    foreach (IdentificacionRecurso item in _objActoAdministrativo.ListaIdentificacionRecurso)
                    {
                        IdentificacionRecursoDALC _i = new IdentificacionRecursoDALC();
                        item.ActoAdministrativo = _objActoAdministrativo.IDSalActoAdministrativo;
                        _i.InsertarIdentificacionRecurso(item, db, transaction);
                    }
                }
                if (_objActoAdministrativo.ListaLocalizacionPredial != null)
                {
                    foreach (LocalizacionPredial item in _objActoAdministrativo.ListaLocalizacionPredial)
                    {
                        LocalizacionPredialDALC _l = new LocalizacionPredialDALC();
                        item.ActoAdministrativo = _objActoAdministrativo.IDSalActoAdministrativo;
                        _l.InsertarLocalizacionPredial(item, db, transaction);
                    }
                }


                transaction.Commit();
                db.Close();
                _respuesta = true;

            }
            catch
            {
                transaction.Rollback();
                db.Close();
                throw;
            }

            return _respuesta;

        }

       

    }
}
