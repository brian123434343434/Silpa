using System;
using System.Collections.Generic;
using System.Text;
using SILPA.LogicaNegocio;
using SILPA.LogicaNegocio.DAA;
using SILPA.LogicaNegocio.Generico;
using SILPA.Servicios.Generico.RadicarDocumento;
using SILPA.Comun;
using SILPA.AccesoDatos.Generico;
using System.Data;

namespace SILPA.Servicios.SolicitudDAA
{
    public class SolicitudFachada
    {
        public SolicitudDAAEIA _objSolicitudFachada;
        public int idRadicacion;

        public SolicitudFachada() { }

        /// <summary>
        /// Constructor de la fachada de solicitud DAA
        /// </summary>
        /// <param name="strClient"></param>
        /// <param name="int64UserId"></param>
        /// <param name="int64ActivityInstanceId"></param>
        /// <param name="int64IdProcessCase"></param>
        /// <param name="strEntryDataType"></param>
        /// <param name="strEntryData"></param>
        /// <param name="strIDEntryData"></param>
        public SolicitudFachada(string strClient, Int64 int64UserId, Int64 int64ActivityInstanceId,
                                Int64 int64IdProcessCase, string strEntryDataType, string strEntryData, 
                                string strIDEntryData)
        {
                this._objSolicitudFachada= new SolicitudDAAEIA(strClient,int64UserId,int64ActivityInstanceId,  
                                    int64IdProcessCase, strEntryDataType, strEntryData, strIDEntryData);
                /// se inserta la solicitud
                this._objSolicitudFachada.InsertarSolicitud();

        }

        public void SolicitudFachadaCrearSolicitud(string strClient, Int64 int64UserId, Int64 int64IdProcessInstance, int idRadicacion, int autoridadAmbiental)
        {
            this._objSolicitudFachada = new SolicitudDAAEIA();
            this._objSolicitudFachada.SolicitudEstandar(strClient, int64UserId, int64IdProcessInstance);
            _objSolicitudFachada.Identity.IdAutoridadAmbiental = autoridadAmbiental;

            #region jmartinez 14/12/2017 realizo la reasignacion de la autoridad ambiental de la solicitud en caso de que el usuario pertenezca a una corporacion o no
            if (this._objSolicitudFachada.Identity.IdAutoridadAmbiental > 0 && this._objSolicitudFachada.Identity.IdTipoTramite > 0 && this._objSolicitudFachada.Identity.IdSolicitante > 0)
            {
                //SMLog.Escribir(Severidad.Informativo, "inicio Resignacion Autoridad Ambiental " + this.Identity.IdTipoTramite.ToString() + "," + this.Identity.IdSolicitante.ToString() + "," + this.Identity.IdAutoridadAmbiental.ToString());
                AutoridadAmbientalDalc aDalc2 = new AutoridadAmbientalDalc();
                this._objSolicitudFachada.Identity.IdAutoridadAmbiental = aDalc2.ValidarUsuarioCorporacion(this._objSolicitudFachada.Identity.IdAutoridadAmbiental, this._objSolicitudFachada.Identity.IdSolicitante, this._objSolicitudFachada.Identity.IdTipoTramite);
                //SMLog.Escribir(Severidad.Informativo, "Fin Resignacion Autoridad Ambiental " + this.Identity.IdTipoTramite.ToString() + "," + this.Identity.IdSolicitante.ToString() + "," + this.Identity.IdAutoridadAmbiental.ToString());
            }
            #endregion


            
            /// se inserta la solicitud
            this._objSolicitudFachada.InsertarSolicitud();

            //Se actualiza la solicitud con la AA escogida
            this.ActualizarAASolicitud(this._objSolicitudFachada.Identity.IdProcessInstance, this._objSolicitudFachada.Identity.IdAutoridadAmbiental, idRadicacion);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strClient"></param>
        /// <param name="int64UserId"></param>
        /// <param name="int64IdProcessCase"></param>
        public SolicitudFachada(string strClient, Int64 int64UserId, Int64 int64IdProcessInstance) 
        {
            this._objSolicitudFachada = new SolicitudDAAEIA(strClient, int64UserId, int64IdProcessInstance);

            /// se inserta la solicitud
            this._objSolicitudFachada.InsertarSolicitud();
            int _idRadicacion = 0;
            if (this._objSolicitudFachada.Identity.IdTipoEstadoSolicitud == (int)EstadoProcesoDAA.Radicar_Solicitud_Manualmente)
            {
                _idRadicacion = this.RadicarSolicitud(this._objSolicitudFachada.Identity.IdProcessInstance, this._objSolicitudFachada.Identity.IdAutoridadAmbiental, this._objSolicitudFachada.Identity.IdSolicitante);
                this.idRadicacion = _idRadicacion;

                //Se actualiza la solicitud con la AA escogida
                this.ActualizarAASolicitud(this._objSolicitudFachada.Identity.IdProcessInstance, this._objSolicitudFachada.Identity.IdAutoridadAmbiental, _idRadicacion);
            }
        }

        public void SolicitudFachadaEstandar(string strClient, Int64 int64UserId, Int64 int64IdProcessInstance)
        {
            this._objSolicitudFachada = new SolicitudDAAEIA();
            this._objSolicitudFachada.SolicitudEstandar(strClient, int64UserId, int64IdProcessInstance);
            /// se inserta la solicitud
            this._objSolicitudFachada.InsertarSolicitud();
            int _idRadicacion = 0;
            _idRadicacion = this.RadicarSolicitud(this._objSolicitudFachada.Identity.IdProcessInstance, this._objSolicitudFachada.Identity.IdAutoridadAmbiental, this._objSolicitudFachada.Identity.IdSolicitante);
            this.idRadicacion = _idRadicacion;
                //Se actualiza la solicitud con la AA escogida
                this.ActualizarAASolicitud(this._objSolicitudFachada.Identity.IdProcessInstance, this._objSolicitudFachada.Identity.IdAutoridadAmbiental, _idRadicacion);
        }


        public void SolicitudFachadaEstandarAA(string strClient, Int64 int64UserId, Int64 int64IdProcessInstance, int AA)
        {
            this._objSolicitudFachada = new SolicitudDAAEIA();
            this._objSolicitudFachada.SolicitudEstandar(strClient, int64UserId, int64IdProcessInstance);
            this._objSolicitudFachada.Identity.IdAutoridadAmbiental = AA;

            #region jmartinez 14/12/2017 realizo la reasignacion de la autoridad ambiental de la solicitud en caso de que el usuario pertenezca a una corporacion o no
            if (this._objSolicitudFachada.Identity.IdAutoridadAmbiental > 0 && this._objSolicitudFachada.Identity.IdTipoTramite > 0 && this._objSolicitudFachada.Identity.IdSolicitante > 0)
            {
                //SMLog.Escribir(Severidad.Informativo, "inicio Resignacion Autoridad Ambiental " + this.Identity.IdTipoTramite.ToString() + "," + this.Identity.IdSolicitante.ToString() + "," + this.Identity.IdAutoridadAmbiental.ToString());
                AutoridadAmbientalDalc aDalc2 = new AutoridadAmbientalDalc();
                this._objSolicitudFachada.Identity.IdAutoridadAmbiental = aDalc2.ValidarUsuarioCorporacion(this._objSolicitudFachada.Identity.IdAutoridadAmbiental, this._objSolicitudFachada.Identity.IdSolicitante, this._objSolicitudFachada.Identity.IdTipoTramite);
                //SMLog.Escribir(Severidad.Informativo, "Fin Resignacion Autoridad Ambiental " + this.Identity.IdTipoTramite.ToString() + "," + this.Identity.IdSolicitante.ToString() + "," + this.Identity.IdAutoridadAmbiental.ToString());
            }
            #endregion

            /// se inserta la solicitud
            this._objSolicitudFachada.InsertarSolicitud();
            int _idRadicacion = 0;
            _idRadicacion = this.RadicarSolicitud(this._objSolicitudFachada.Identity.IdProcessInstance, this._objSolicitudFachada.Identity.IdAutoridadAmbiental, this._objSolicitudFachada.Identity.IdSolicitante);
            this.idRadicacion = _idRadicacion;
            //Se actualiza la solicitud con la AA escogida
            this.ActualizarAASolicitud(this._objSolicitudFachada.Identity.IdProcessInstance, this._objSolicitudFachada.Identity.IdAutoridadAmbiental, _idRadicacion);
        }

        public void SolicitarDDA(string strClient, long lngUserId, long lngIdProcessCase, long lngSequence,
                                 string strEntryDataType, string strIDEntryData, string strEntryData,
                                 ref SolicitudDAAEIA objSolicitud, List<string> lstStrDocumentoAdjunto,
                                 List<byte[]> lstBteDocumentoAdjunto, string strUserSolicitante)
        {

           /// BPMServices.GattacaBPMServices9000 objBPMServices = new SILPA.Servicios.BPMServices.GattacaBPMServices9000();
           //objSolicitud.objIdentity.NumeroSilpa = Convert.ToInt32(objBPMServices.WMCreateProcessInstance(strClient, lngUserId, lngIdProcessCase, lngSequence, strEntryDataType, strIDEntryData, strIDEntryData));
           // objSolicitud.InsertarSolicitud();

            ///RadicacionDocumentoFachada objRadica = new RadicacionDocumentoFachada(Convert.ToString(objSolicitud.Identity.NumeroSilpa), "", "", 0, objSolicitud.jIdentity.IdSolicitante.ToString(), 0, lstStrDocumentoAdjunto, lstBteDocumentoAdjunto, objSolicitud.Identity.IdAutoridadAmbiental, (int)TipoDocumento.Oficio, strUserSolicitante);
            //objBPMServices.WMStartProcessInstance("SoftManagement", lngUserId, objSolicitud.objIdentity.NumeroSilpa);

         


        }

        /// <summary>
        /// Consulta el Estado en el que se encuentra el proceso
        /// </summary>
        /// <param name="idProcessInstance">Número de Instancia de Proceso en BPM</param>
        /// <returns>Número de Estado de Proceso</returns>
        public int ConsultarProceso(long idProcessInstance)
        {
            SolicitudDAAEIA _daaeia = new SolicitudDAAEIA();
            _daaeia.ConsultarSolicitudByProcessInstance(idProcessInstance);
            return _daaeia.Identity.IdTipoEstadoSolicitud;
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idProcessInstance"></param>
        /// <param name="autoridadAmbiental"></param>

        public int RadicarSolicitud(long idProcessInstance, int? AutoridadAmbiental, long userID)
        {
            SolicitudDAAEIA _daaeia = new SolicitudDAAEIA();
            _daaeia.ConsultarSolicitudByProcessInstance(idProcessInstance);

            RadicacionDocumentoFachada _objRadicar = new RadicacionDocumentoFachada(_daaeia.Identity.IdProcessInstance.ToString(), "", "", _daaeia.Identity.FormularioID, _daaeia.Identity.IdSolicitante.ToString(), 0, Convert.ToInt32(AutoridadAmbiental), 1, userID.ToString(), _daaeia.Identity.FechaCreacion, _daaeia.Identity.NumeroSilpa);

            return _objRadicar.RadicarDocumento();
        }

        /// <summary>
        /// JMM
        /// Metodo para la actualización del registro de radicación.
        /// </summary>
        /// <param name="idProcessInstance">Id del process intance para buscar el </param>
        /// <param name="AutoridadAmbiental">Autoridad ambiental que se va a actualizar.</param>
        /// <returns></returns>
        public int ActualizarRadicacion(long idProcessInstance, int AutoridadAmbiental)
        {
            RadicacionDocumentoFachada _objRadicar = new RadicacionDocumentoFachada(idProcessInstance, AutoridadAmbiental);

            return _objRadicar.ActualizarRadicacion();
            
        }

        /// <summary>
        /// Consulta las autoridades ambientales que intervienen en el conflicto de competencias
        /// </summary>
        /// <param name="idProcessInstance">Número de proceso de BPM de la solicitud de DAA</param>
        /// <returns>Dataset con la lista de autoridades ambientales en conflicto</returns>
        public DataSet ConsultarAutoridadesAmbientales(long idProcessInstance)
        {
            AutoridadAmbiental _objAA = new AutoridadAmbiental();
            return _objAA.ListarAutoridadesAmbientalesByUbicacion((int)idProcessInstance);
        }


        public void ReenviarDAA(string NumeroSILPA, string documento)
        {
            DocumentoIdentity _documento = new DocumentoIdentity();
        //    //_documento = _documento.
        }

        /// <summary>
        /// Obtiene el URL encontrado en el sector para la solicitud de DAA que tiene TDR para DAA Fijos
        /// </summary>
        /// <param name="idProcessInstance"></param>
        public string ObtenerURL_TDRDAA(long idProcessInstance)
        {
            SolicitudDAAEIA _daaeia = new SolicitudDAAEIA();
            _daaeia.ConsultarSolicitudByProcessInstance(idProcessInstance);
            int? _intSector = _daaeia.Identity.IdSector;
            int? _intTipoProyecto = _daaeia.Identity.IdTipoProyecto;
            SectorIdentity _sector = new SectorIdentity();
            _sector.IdPadre = Convert.ToInt32(_intSector);
            _sector.Id = Convert.ToInt32(_intTipoProyecto);
            SectorDalc _sectorDalc = new SectorDalc(ref _sector);
            return _sector.UrlDAATDR;
        }

        /// <summary>
        /// Actualiza la Autoridad Ambiental para la solicitud, para los casos en los que hay conflicto de de competencias
        /// </summary>
        /// <param name="idProcessInstance">Número de Instancia del proceso</param>
        public void ActualizarAASolicitud(long idProcessInstance, int? idAutoridadAmbiental, Int64 int64IdRadicacion)
        {
            SolicitudDAAEIA _daaeia = new SolicitudDAAEIA();
            _daaeia.ConsultarSolicitudByProcessInstance(idProcessInstance);
            _daaeia.Identity.IdAutoridadAmbiental = idAutoridadAmbiental;
            if(!Convert.IsDBNull(int64IdRadicacion))
            {
                 _daaeia.Identity.IdRadicacion = int64IdRadicacion;
            }
            _daaeia.ActualizarSolicitud();
        }

        /// <summary>
        /// Obtiene la autoridad ambiental asignada a una solicitud especifica.
        /// </summary>
        /// <param name="idProcessInstance">Número de Instancia del proceso</param>
        public int ObtenerAASolicitud(long idProcessInstance)
        {
            SolicitudDAAEIA _daaeia = new SolicitudDAAEIA();
            _daaeia.ConsultarSolicitudByProcessInstance(idProcessInstance);
            return Convert.ToInt32(_daaeia.Identity.IdAutoridadAmbiental);
        }


        /// <summary>
        /// Se actualiza el estado de la solicitud
        /// </summary>
        /// <param name="idProcessInstance"></param>
        /// <param name="idEstado"></param>
        public void ActualizarEstadoSolicitud(long idProcessInstance, int idEstado)
        {
            SolicitudDAAEIA _daaeia = new SolicitudDAAEIA();
            _daaeia.ConsultarSolicitudByProcessInstance(idProcessInstance);
            _daaeia.Identity.IdTipoEstadoSolicitud = idEstado;
            _daaeia.ActualizarSolicitud();
        }

        /// <summary>
        /// Se actualiza el tipo de trámite
        /// </summary>
        /// <param name="idProcessInstance"></param>
        /// <param name="idEstado"></param>
        public void ActualizarTipoTramite(long idProcessInstance, int tipoTramite)
        {
            SolicitudDAAEIA _daaeia = new SolicitudDAAEIA();
            _daaeia.ConsultarSolicitudByProcessInstance(idProcessInstance);
            _daaeia.Identity.IdTipoTramite = tipoTramite;
            _daaeia.ActualizarSolicitud();
        }



        //public int ObtenerAASolicitud(long processInstance)
        //{
        //    SolicitudDAAEIA _daaeia = new SolicitudDAAEIA();
        //    _daaeia.ConsultarSolicitudByProcessInstance(processInstance);
        //    return _daaeia.Identity.IdAutoridadAmbiental;
        //}

        public bool EsConflicto(long idProcessInstance)
        {
           
            AutoridadAmbientalDalc aad = new AutoridadAmbientalDalc();
            DataSet ds = new DataSet();
            ds = aad.ListarAAXUbicacion(idProcessInstance);
            if (ds.Tables[0].Rows.Count > 1)
                return true;
            else
                return false;


            
        }

        public int ObtenerTramiteEIA()
        {
            TipoTramiteDalc _tipoTramite = new TipoTramiteDalc();
            return _tipoTramite.ObtenerIDTramiteXNombre("Solicitud TDR para EIA");
        }


    }
}
