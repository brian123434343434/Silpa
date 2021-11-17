using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using SILPA.AccesoDatos.PINES;
using System.Data.SqlClient;
using System.Linq;
using Silpa.Workflow.AccesoDatos;
using System.Data;

namespace SILPA.LogicaNegocio.PINES
{
    public class PINES
    {
        public PINESDALC vPINESDALC;
        public PINES()
        {
            vPINESDALC = new PINESDALC();
        }

        public List<int> ListaActividadesVariosIntervinientes()
        {
            try
            {
                return vPINESDALC.ListaActividadesVariosIntervinientes();

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public List<int> ListaAutoridadesSolicitudId(int IdProcessInstace, int? idField)
        {
            try
            {
                return vPINESDALC.ListaAutoridadesSolicitudID(IdProcessInstace,idField);

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public List<string> ListaAutoridadesSolicitudNombre(int IdProcessInstace, int? idField)
        {
            try
            {
                return vPINESDALC.ListaAutoridadesSolicitudNombre(IdProcessInstace,idField);

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void FinalizarActivityInstance(int IdActivity, int IdProcessInstace, int IdActivityInstance, int? idField, string usuario)
        {
            bpmServices.GattacaBPMServices9000 wsGattaca = new bpmServices.GattacaBPMServices9000();
            wsGattaca.Url = SILPA.Comun.DireccionamientoWS.UrlWS("bpmServices");
            wsGattaca.Credentials = Credenciales();
            // consulta la lista de las actividades que necesitan la intervencion de varias personas para poder finalizar
            List<int> LstActividades = ListaActividadesVariosIntervinientes();
            // validamos si la actividad que se intentar finalizar se encuentra en la lista
            if (LstActividades.Contains(IdActivity))
            {
                int nroActividadesRealizadas = 0;
                // consultamos el numero de intervinientes que tiene la actividad
                var LstAutoridadesSolicitud = ListaAutoridadesSolicitudId(IdProcessInstace, idField);
                int NroAutoridadesSolicitud = LstAutoridadesSolicitud.Count;
                //consultamos la acciones que son obligatorias para la actividad
                var listaAccions = ConsultaAccionActividad(IdActivity).AsEnumerable();
                var accionesObligatorias = listaAccions.Where(x => x.Field<Boolean>("OBLIGATORIA") == true && x.Field<Boolean>("ACTIVO") == true).ToList();

                foreach (int idAutoridad in LstAutoridadesSolicitud)
                {
                    foreach (var item in accionesObligatorias)
                    {
                        // validamos si el usuario ya creo un comentario para la actividad
                        ComentarioActividadIdentity vComentarioActividadIdentity = new ComentarioActividadIdentity();
                        vComentarioActividadIdentity.IDActivityInstance = IdActivityInstance;
                        vComentarioActividadIdentity.IDActivity = IdActivity;
                        vComentarioActividadIdentity.IDProcessInstance = IdProcessInstace;
                        vComentarioActividadIdentity.EsGerente = false;
                        vComentarioActividadIdentity.IDAccion = Convert.ToInt32(item.Field<int>("IDACCION"));
                        vComentarioActividadIdentity.AutoridadId = idAutoridad;
                        ComentariosActividad vComentariosActividad = new ComentariosActividad();
                        List<ComentarioActividadIdentity> LstComentarios = vPINESDALC.ListaComentarioActividad(vComentarioActividadIdentity);
                        // filtramos los comentarios que dieron aprobado la finalizacion de la tarea
                        var LstActividadesRealizadas = LstComentarios.Where(x => x.ActividadRealizada == true).ToList();
                        if (LstActividadesRealizadas.Count > 0)
                        {
                            nroActividadesRealizadas++;
                        }
                    }
                    
                }

                if (nroActividadesRealizadas >= (NroAutoridadesSolicitud * accionesObligatorias.Count))
                {
                    // finalizar actividad
                    Int32 UserID = ApplicationUserDao.ObtenerAplicationUser(usuario);
                    bpmServices.ListItem[] condiciones = wsGattaca.GetConditionsByActivityInstance("SoftManagement", UserID, IdActivityInstance);

                    wsGattaca.EndActivityInstance("SoftManagement", UserID, IdActivityInstance, IdProcessInstace, condiciones[0].Value, "", "", "", "", "");
                    Alarmas clsAlarmas = new Alarmas();
                    clsAlarmas.NotificarCrearcionActivityInstance(UserID, IdProcessInstace);
                }
                else
                {
                    throw new Exception("La actividad se encuentra en proceso de Finalizacion por otra autoridad Ambiental");
                }

            }
            else
            {
                Int32 UserID = ApplicationUserDao.ObtenerAplicationUser(usuario);
                bpmServices.ListItem[] condiciones = wsGattaca.GetConditionsByActivityInstance("SoftManagement", UserID, IdActivityInstance);

                wsGattaca.EndActivityInstance("SoftManagement", UserID, IdActivityInstance, IdProcessInstace, condiciones[0].Value, "", "", "", "", "");
                Alarmas clsAlarmas = new Alarmas();
                clsAlarmas.NotificarCrearcionActivityInstance(UserID, IdProcessInstace);
            }
        }

        public static System.Net.NetworkCredential Credenciales()
        {
            string user = System.Configuration.ConfigurationManager.AppSettings["usuario_servicio"].ToString();
            string pass = System.Configuration.ConfigurationManager.AppSettings["clave_servicio"].ToString();
            System.Net.NetworkCredential credencial = new System.Net.NetworkCredential(user, pass);
            return credencial;
        }
        public DataTable ConsultarVisorProceso(string nroVital, DateTime? fechaIni, DateTime? fechaFin, string departamento, int? autoridadId, string nombreProyecto, string solicitante)
        {
            try
            {
                return vPINESDALC.ConsultarVisorProceso(nroVital, fechaIni, fechaFin, departamento, autoridadId, nombreProyecto, solicitante);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public DataTable ConsultaAvanceEsperadoHoyProyecto(string nroVital, DateTime? fechaIni, DateTime? fechaFin, string departamento, int? autoridadId, string nombreProyecto)
        {
            try
            {
                return vPINESDALC.ConsultaAvanceEsperadoHoyProyecto(nroVital, fechaIni, fechaFin, departamento, autoridadId, nombreProyecto);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataTable ConsultaAccionActividad(Int32 IDActivity)
        {
            try
            {
                return vPINESDALC.ConsultaAccionActividad(IDActivity);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataTable ConsultaActividadesWorkFlowPINES()
        {
            try
            {
                return vPINESDALC.ConsultaActividadesWorkFlowPINES();
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public void InsertarActividadProcesoUrl(ActividadProcesoURL pActividadProcesoURL)
        {
            try
            {
                vPINESDALC.InsertarActividadProcesoUrl(pActividadProcesoURL);

            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public void ConsultarActividadProcesoUrl(ref ActividadProcesoURL pActividadProcesoURL)
        {
            try
            {
                vPINESDALC.ConsultarActividadProcesoUrl(ref pActividadProcesoURL);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public List<ActividadProcesoURL> ListaActividadProcesoUrl(ActividadProcesoURL pActividadProcesoURL)
        {
            try
            {
                return vPINESDALC.ListaActividadProcesoUrl(pActividadProcesoURL);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
        public DataTable AccionesNoUsadas(Int32 idActivity)
        {
            try
            {
                return vPINESDALC.AccionesNoUsadas(idActivity);
            }
            catch (SqlException sqle)
            {
                throw new Exception(sqle.Message);
            }
        }
    }
}

