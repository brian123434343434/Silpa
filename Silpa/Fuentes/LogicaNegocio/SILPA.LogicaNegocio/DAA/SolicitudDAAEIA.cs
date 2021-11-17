using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.DAA;
using SILPA.AccesoDatos.Utilidades;
using System.Data;
using SILPA.Comun;
using SILPA.AccesoDatos.Generico;
using System.Xml;
using SILPA.LogicaNegocio.Generico;
using System.IO;
using SILPA.AccesoDatos.Parametrizacion;
using SoftManagement.Log;
using SILPA.AccesoDatos.Notificacion;



#region jmartinez importo libreria para pasar archivos por ftp
using System.Net; 
using System.Configuration;

#endregion

namespace SILPA.LogicaNegocio.DAA
{
    public class SolicitudDAAEIA : EntidadSerializable
    {
        public SolicitudDAAEIAIdentity Identity;
        private SolicitudDAAEIADalc Dalc;
        private Formulario _objFormulario;
        private FormularioDalc _objFormularioDalc;
        private List<String> _lstEtiquetasAA;
        private List<String> _lstEtiquetasAANumVital;
        private List<String> _lstEtiquetasAAxSector;
        private List<String> _lstEtiquetasAAxTipoProy;
        private List<String> _lstEtiquetaTramiteDefineAUxSector;
        private Configuracion _objConfiguracion;
        
        /// <summary>
        /// Objeto que tiene los datos del formulario de la solcitud
        /// </summary>

        public const string columnaIdAutoridadAmbiental = "ID_AUTORIDAD_AMBIENTAL";
        public const string columnaAutId = "AUT_ID";
        public const string tablaSectores = "Sectores";
        public const string tablaTipoProyecto = "TiposProyecto";
        public const string tablaAutAmbiental = "AutoridadesAmbientales";

        /// <summary>
        /// Constructor
        /// </summary>
        public SolicitudDAAEIA()
        {
            Dalc = new SolicitudDAAEIADalc();
            Identity = new SolicitudDAAEIAIdentity();
            this.CargarListaEtiquetas();
        }



        public SolicitudDAAEIA(Int64 int64ProcessInstance)
        {
            Dalc = new SolicitudDAAEIADalc();
            Identity = new SolicitudDAAEIAIdentity();
            this.CargarListaEtiquetas();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strClient"></param>
        /// <param name="int64UserId"></param>
        /// <param name="int64IdProcessCase"></param>
        public SolicitudDAAEIA(string strClient, Int64 int64UserId, Int64 int64IdProcessInstanceID)
        {
            this.Dalc = new SolicitudDAAEIADalc();
            this.Identity = new SolicitudDAAEIAIdentity();
            Identity.IdSolicitante = int64UserId;
            Identity.IdProcessInstance = int64IdProcessInstanceID;
            //Trámite DAA -> ID = 1
            Identity.IdTipoTramite = 1;
            _objFormulario = new Formulario();
            _objFormulario.ObtenerDatosFormularioSolcitudDaa(ref Identity);
            //this.ConsultarDatosFormulario(int.Parse(strIdEntryData), int64ProcessInstanceID);
            //this.ConsultarDatosFormulario(Convert.ToInt64(strIdEntryData), int64ProcessInstanceID);
            this.DefinirTipoEstadoProceso();
        }

        /// <summary>
        /// Instancia los datos para una solicitud Estándar (diferente a AA)
        /// </summary>
        /// <param name="strClient">Cliente de BPM - Softmanagement</param>
        /// <param name="int64UserId">ID del Usuario de eSecurity - BPM</param>
        /// <param name="int64IdProcessInstanceID">ID de la instancia del proceso de BPM</param>
        public void SolicitudEstandar(string strClient, Int64 int64UserId, Int64 int64IdProcessInstanceID)
        {
            try
            {
                this.Dalc = new SolicitudDAAEIADalc();
                this.Identity = new SolicitudDAAEIAIdentity();
                Identity.IdSolicitante = int64UserId;
                Identity.IdProcessInstance = int64IdProcessInstanceID;
                //Trámite DAA -> ID = 1
                ProcesoDalc _procesoDalc = new ProcesoDalc();
                ProcesoIdentity _proceso = _procesoDalc.ObtenerObjProceso(int64IdProcessInstanceID);
                SILPA.AccesoDatos.Parametrizacion.TipoTramiteDalc _tipoTramiteDalc = new SILPA.AccesoDatos.Parametrizacion.TipoTramiteDalc();
                TipoTramite _tipoTramite = _tipoTramiteDalc.BuscarTramitePorCaso(Convert.ToInt32(_proceso.IdProcessCase));
                Identity.IdTipoTramite = _tipoTramite.Id;
                // ojo dato quemado.
                //SMLog.Escribir(Severidad.Informativo, "++++Identity.IdTipoEstadoSolicitud = 10");     
                //Identity.IdTipoEstadoSolicitud = 10;
                _objFormulario = new Formulario();
                //JMM - OCT 08/10 - Se comenta la validacion del if para obteber la AA de los procesos de EE
                //if (_proceso.TipoEntidad)
                //{
                    DefinirAutoridadesAmbientales();
                    //Se deshabilida para agregar funcionalidad de búsqueda de autoridad ambiental por ubicacion - JDGB 07102010 14.30
                    //SMLog.Escribir(Severidad.Informativo, "++++Tipo de Tramite:" + _tipoTramite.Id.ToString());
                    //SMLog.Escribir(Severidad.Informativo, "++++int64IdProcessInstanceID:" + int64IdProcessInstanceID.ToString());
                    //_objFormulario.ObtenerAAFormularioSolcitudEstandar(ref Identity);
                    //SMLog.Escribir(Severidad.Informativo, "++++Finalizó _objFormulario:");
                //}
                //else
                //{
                //    Identity.IdAutoridadAmbiental = null;
                //}
                Identity.IdTipoEstadoSolicitud = Dalc.EstadoSolicitud(Identity.Conflicto);
            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Informativo, "++++Error:" + ex.Message);
                throw new ApplicationException(ex.Message, ex);  
            }
        }



        /// <summary>
        /// Constructor con los parámetros provenientes del BpmServices
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="UserID"></param>
        /// <param name="activityInstanceID"></param>
        /// <param name="processInstanceID"></param>
        /// <param name="entryDataType"></param>
        /// <param name="entryData"></param>
        /// <param name="idEntryData"></param>
        public SolicitudDAAEIA(string Client, Int64 int64UserID, Int64 int64ActivityInstanceID,
                                             Int64 int64ProcessInstanceID, string strEntryDataType,
                                             string strEntryData, string strIdEntryData) 
        {
            Dalc = new SolicitudDAAEIADalc();
            Identity = new SolicitudDAAEIAIdentity();
            Identity.IdSolicitante = int64UserID;
            Identity.IdProcessInstance = int64ProcessInstanceID;
            Identity.IdTipoTramite = 1;
            _objFormulario = new Formulario();
            //_objFormulario.ObtenerDatosFormularioSolcitudDaa(ref Identity);
            //this.ConsultarDatosFormulario(int.Parse(strIdEntryData), int64ProcessInstanceID);
            this.ConsultarDatosFormulario(Convert.ToInt64(strIdEntryData), int64ProcessInstanceID); 
            this.DefinirTipoEstadoProceso();
        }


        /// <summary>
        /// Obtiene los datos del formulario de la solicitud
        /// </summary>
        /// <param name="int64dEntryData"></param>
        private void ConsultarDatosFormulario(Int64 int64dEntryData, Int64 int64ProcessInstance)
        {
            //this._formularioDalc = new FormularioDalc();
            //this._xmlForm.LoadXml(this._formularioDalc.ConsultarDatosFormulario(int64dEntryData).GetXml());
            /// Se cargan os datos del formulario:
          //  this.Identity.IdSector = 14;
           // this.Identity.IdTipoProyecto = 138;
        }

        /// <summary>
        /// define el tipo de stado de proceso
        /// </summary>
        private void DefinirTipoEstadoProceso()
        {
            SectorIdentity sector = new SectorIdentity();
            sector.Id = Convert.ToInt32(this.Identity.IdTipoProyecto);
            sector.IdPadre = Convert.ToInt32(this.Identity.IdSector); //!!! siempre va!
            SectorDalc sectorDalc = new SectorDalc(ref sector);
            /// Se determina el estado de conflicto
            //this.Dalc.DeterminarConflicto(ref this.Identity);
            AutoridadAmbiental aa = new AutoridadAmbiental();
            DataSet ds = aa.ListarAutoridadesAmbientalesByUbicacion(int.Parse(this.Identity.IdProcessInstance.ToString()));
            if (ds.Tables[0].Rows.Count > 1)
            {
                //hay conflicto
                this.Identity.Conflicto = true;
            }
            else if (ds.Tables[0].Rows.Count == 1)
            {
                this.Identity.IdAutoridadAmbiental = Convert.ToInt32(ds.Tables[0].Rows[0]["AUT_ID"].ToString());
            }
            if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "-1")
                        throw new ApplicationException("Las ciudades incluidas en las ubicaciones de la solicitud no tienen una jurisdicción asignada, se ha terminado el proceso");
                }
            

            /// se obtiene el tipo de estado del proceso
            this.Dalc.ObtenerEstadoProceso(sector, ref this.Identity);
            //Se fija el MAVDT en caso de pertenecer a Hidrocarburos
            if (sector.PerteneceMAVDT)
            {
                //this.Identity.IdAutoridadAmbiental =(int)AutoridadesAmbientales.MAVDT;
                AutoridadAmbientalDalc aDalc = new AutoridadAmbientalDalc();
                this.Identity.IdAutoridadAmbiental = aDalc.ObtenerIdAutoridadMAVDT((int)GenParametro.AutoridadAmbientalMAVDT);
            }
            
        }

        /// <summary>
        /// Obtiene el objeto solcitud mediante el identificador de la instancia del proceso
        /// </summary>
        /// <param name="int64IdFormInstance">int64: identificador de la instancia del proceso</param>
        public void ConsultarSolicitudByProcessInstance(Int64 int64IdProcessInstance)
        {
            this.Identity = Dalc.ObtenerSolicitud(null, int64IdProcessInstance,null);
        }


        public void ConsultarSolicitudNumeroSILPA(string numeroSILPA)
        {
            this.Identity = Dalc.ObtenerSolicitud(null, null, numeroSILPA);
        }

        /// <summary>
        /// Modifica en la tabla Solicitud
        /// </summary>
        /// <param name="objIdentity">Objeto que contiene los valores de los campos que se van a modificiar</param>
        public void ActualizarSolicitud()
        {
                Dalc.ActualizarSolicitud(ref Identity);
        }

        /// <summary>
        /// Actualiza el estado del proceso de la solcitud
        /// </summary>
        public void ActualizarTipoEstadoProceso()
        {
            Dalc.ActualizarTipoEstadoProceso(Identity);
        }


        /// <summary>
        /// Insertar en la tabla Expediente_Ubicacion
        /// </summary>
        /// <param name="objIdentity">Objeto que contiene los valores de los campos que se van a insertar</param>
        public void InsertarSolicitud()
        {

            SolicitudDAAEIAIdentity objIdentity = new SolicitudDAAEIAIdentity();
            objIdentity = Dalc.InsertarSolicitud(ref Identity);

        }
        
        //public List<string> PasarArchivosRepositorioSigpro(string ruta, string carpetaremota, string login, string pass, int ContSaltoDirectorios)
        //{
        //    UploadFtp ftp = new UploadFtp();
        //    List<string> ListaArchivos = new List<string>();

        //    try
        //    {
        //        SMLog.Escribir(Severidad.Informativo, "SigproServicios :: RadicarDocumentoVitalSigpro -> PasarArchivosRepositorioSigpro -> comienza proceso copiado repositorio sigpro por ftp con usuario: " + login + "password:" + pass + " a la carpeta remota:" + carpetaremota + "con la ruta origen: " + ruta);
        //        carpetaremota = ftp.CrearCarpetaFtp(carpetaremota, ruta, login, pass, ContSaltoDirectorios);
        //        ListaArchivos = ftp.SubirArchivosFtp(ruta, carpetaremota, login, pass);
        //    }
        //    catch (Exception exc)
        //    {
        //        SMLog.Escribir(Severidad.Critico, "SigproServicios :: RadicarDocumentoVitalSigpro -> PasarArchivosRepositorioSigpro -> Error Inesperado copiando archivos ftp:" + exc.Message);
        //    }
        //    return ListaArchivos;

        //}

        public void InsertarSolicitudEE()
        {
            Dalc.InsertarSolicitudEE(ref Identity);
        }

        /// <summary>
        /// Retorna un DataSet con los datos de una ubicacion especifica
        /// </summary>
        /// <param name="intIdAA">int: indentificador de la ubicacion</param>
        /// <returns>DataSet: Conjunto de resultados de ubicacion</returns>
        public void ObtenerSolicitud(Nullable<Int64> lngIdSolicitud, Nullable<Int64> lngIdProccessInstance)
        {
            this.Identity = Dalc.ObtenerSolicitud(lngIdSolicitud, lngIdProccessInstance,null);
        }

        /// <summary>
        /// Obtiene los datos de la solicitud a partir del identificador de la radicacion.
        /// </summary>
        /// <param name="intIdRadicacion">Id de la radicacion</param>
        public void ObtenerSolicitudxRadicacion(Int32 intIdRadicacion)
        {
            this.Identity = Dalc.ObtenerSolicitud(intIdRadicacion);
        }

        public DataSet ObtenerSolicitud(Nullable<Int64> lngIdSolicitud, Nullable<int> intIdTipoEstadoSol, Nullable<Int64> lngIdProccessInstance, Nullable<Int32> lngTipoTramite, Nullable<Int32>lngNumeroSilpa )
        {
            return Dalc.ObtenerSolicitud(lngIdSolicitud, intIdTipoEstadoSol, lngIdProccessInstance, lngTipoTramite, lngNumeroSilpa);
        }

        public void RadicarSolicitud(Int64 int64IdProcessInstance, Int64 int64IdFromInstance)
        {
            string NumRadicacion =  string.Empty;
            DateTime _fechaRadicacion = DateTime.Now;

            RadicacionDocumento v = new RadicacionDocumento();
            FormularioDalc objFormularioDalc = new FormularioDalc();
            RadicacionDocumentoDalc objRadicarDacl = new RadicacionDocumentoDalc();

            this.Identity = Dalc.ObtenerSolicitud(null, int64IdProcessInstance,null);

            //NumRadicacion = objFormularioDalc.ConsultarListadoCamposForm(int64IdFromInstance).Tables[0].Rows[0]["VALOR"].ToString();

            DataSet dsRadica = objFormularioDalc.ConsultarListadoCamposForm(int64IdFromInstance);

            if (dsRadica!=null)
            {
            
            if (dsRadica.Tables.Count>0)
            {
                if (dsRadica.Tables[0].Rows.Count>0)
                {

                    if (dsRadica.Tables[0].Rows[0]["VALOR"] != DBNull.Value) 
                    {
                        NumRadicacion = dsRadica.Tables[0].Rows[0]["VALOR"].ToString();
                    }

                    if (dsRadica.Tables[0].Rows[1]["VALOR"] != DBNull.Value) 
                    {
                        _fechaRadicacion = Convert.ToDateTime(dsRadica.Tables[0].Rows[1]["VALOR"].ToString());
                    }

                    objRadicarDacl.ActualizarRadicacionDocumento(this.Identity.IdRadicacion, NumRadicacion, _fechaRadicacion);
                    //objRadicarDacl.ActualizarRadicacionDocumento(this.Identity.IdRadicacion, NumRadicacion, null);
                }
            }
        }
        
        }

        /// <summary>
        /// Lista los tramites que tengan valores que coincidan con los parametros de entrada
        /// </summary>
        /// <param name="blNombreVacio"></param>
        /// <param name="dtFechaIni"></param>
        /// <param name="dtFechaFin"></param>
        /// <param name="intIdTpTramite"></param>
        /// <param name="strNumeroSilpa"></param>
        /// <param name="intIdAA"></param>
        /// <param name="strNombre"></param>
        /// <param name="intIdDepartamento"></param>
        /// <param name="IdMunicipio"></param>
        /// <returns></returns>
        public DataTable ListarTramitesRPT( bool blNombreVacio, DateTime dtFechaIni, DateTime dtFechaFin, int intIdTpTramite,
            string strNumeroSilpa, int intIdAA, string strNombre, int intIdDepartamento, int IdMunicipio, string hidrografia) {

            return Dalc.ListarTramitesRPT( blNombreVacio, dtFechaIni, dtFechaFin, intIdTpTramite,
                strNumeroSilpa, intIdAA, strNombre, intIdDepartamento, IdMunicipio, "", hidrografia ).Tables[ 0 ];
        }
        public DataTable ListarTramitesRPT( bool blNombreVacio, DateTime? dtFechaIni, DateTime? dtFechaFin, int intIdTpTramite,
            string strNumeroSilpa, int intIdAA, string strNombre, int intIdDepartamento, int IdMunicipio, string hidrografia, int UsuarioId ) {

            return Dalc.ListarTramitesRPT( blNombreVacio, dtFechaIni, dtFechaFin, intIdTpTramite,
                strNumeroSilpa, intIdAA, strNombre, intIdDepartamento, IdMunicipio, "", hidrografia, UsuarioId ).Tables[ 0 ];
        }

        public DataTable ListarTramitesRPT( bool blNombreVacio, DateTime dtFechaIni, DateTime dtFechaFin, int intIdTpTramite,
            string strNumeroSilpa, int intIdAA, string strNombre, int intIdDepartamento, int IdMunicipio, string numeroExpediente, string sectoresSeleccionados, string hidrografia ) {
            return Dalc.ListarTramitesRPT( blNombreVacio, dtFechaIni, dtFechaFin, intIdTpTramite,
                strNumeroSilpa, intIdAA, strNombre, intIdDepartamento, IdMunicipio, numeroExpediente, sectoresSeleccionados, hidrografia, 0 ).Tables[ 0 ];
        }

        public DataTable ListarTramitesRPT( bool blNombreVacio, DateTime dtFechaIni, DateTime dtFechaFin, int intIdTpTramite,
            string strNumeroSilpa, int intIdAA, string strNombre, int intIdDepartamento, int IdMunicipio, string numeroExpediente,
            string sectoresSeleccionados, string hidrografia, int usuarioId ) {
            return Dalc.ListarTramitesRPT( blNombreVacio, dtFechaIni, dtFechaFin, intIdTpTramite,
                strNumeroSilpa, intIdAA, strNombre, intIdDepartamento, IdMunicipio, numeroExpediente, sectoresSeleccionados, hidrografia, usuarioId ).Tables[ 0 ];
        }

        public DataTable ListarTramitesRPT(bool blNombreVacio, DateTime? dtFechaIni, DateTime? dtFechaFin, int intIdTpTramite,
           string strNumeroSilpa, int intIdAA, string strNombre, int intIdDepartamento, int IdMunicipio, string numeroExpediente,
           string sectoresSeleccionados, string hidrografia, int usuarioId,string estadoResolucion,string estadoTramite,string idSolicitante)
        {
            return Dalc.ListarTramitesRPT(blNombreVacio, dtFechaIni, dtFechaFin, intIdTpTramite,
                strNumeroSilpa, intIdAA, strNombre, intIdDepartamento, IdMunicipio, numeroExpediente, sectoresSeleccionados, hidrografia, usuarioId,estadoResolucion,estadoTramite,idSolicitante).Tables[0];
        }

        /// <summary>
        /// Define la autoridad ambiental a la cuál se le debe enviar la solicitud
        /// </summary>
        /// <remarks>Este proceso solo aplica para Solicitudes diferentes a DAA y que hayan sido parametrizadas
        /// como solicitudes en las que aplica autoridad ambiental (ver tabla WSB_CASO_PROCESO_PERMITIDO)
        /// También se debe tener parametrizado el formulario de ubicaciones en la tabla GEN_PARAMETRO
        /// para el PARAMETRO formulario_ubicaciones
        /// </remarks>
        private void DefinirAutoridadesAmbientales()
        {
            try
            {
                AutoridadAmbiental aa = new AutoridadAmbiental();
                SMLog.Escribir(Severidad.Informativo, "++++this.Identity.IdProcessInstance: " + this.Identity.IdProcessInstance.ToString());   
                DataSet ds = aa.ListarAutoridadesAmbientalesOtros(int.Parse(this.Identity.IdProcessInstance.ToString()));
                SMLog.Escribir(Severidad.Informativo, "++++ds: " + (ds != null ? ds.GetXml() : "null"));
                //JMM 2010/10/27 - codigo para definir la AA por medio del sector y tipo de proyecto
                _objFormularioDalc = new FormularioDalc();
                DataSet _dsResultado = _objFormularioDalc.ConsultarDatosFormulario(int.Parse(this.Identity.IdProcessInstance.ToString()));
                SMLog.Escribir(Severidad.Informativo, "++++_dsResultado: " + (_dsResultado != null ? _dsResultado.GetXml() : "null"));   

                if (_dsResultado != null)
                {
                    if (_dsResultado.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drTemp in _dsResultado.Tables[0].Rows)
                        {
                            if (EtiquetaDefAA(_lstEtiquetasAAxSector, drTemp["VALORCAMPO"].ToString()))
                            {
                                Identity.IdSector = Convert.ToInt32(drTemp["VALOR"].ToString());
                            }
                            if (EtiquetaDefAA(_lstEtiquetasAAxTipoProy, drTemp["VALORCAMPO"].ToString()))
                            {
                                Identity.IdTipoProyecto = Convert.ToInt32(drTemp["VALOR"].ToString());
                            }
                        }
                    }
                }

                bool tieneEtiquetaAU = false;
                // validamos si el formulario tiene etiqueta radicable atoridad
                if (_dsResultado.Tables[0].Rows.Count > 0)
                {
                    
                    foreach (DataRow drTemp in _dsResultado.Tables[0].Rows)
                    {
                        if (EtiquetaDefAA(_lstEtiquetasAA, drTemp["VALORCAMPO"].ToString()))
                        {
                            if (drTemp["VALOR"].ToString() != string.Empty && !drTemp["VALOR"].ToString().Contains("-"))
                            {
                                Identity.IdAutoridadAmbiental = Convert.ToInt32(drTemp["VALOR"].ToString());
                                tieneEtiquetaAU = true;
                            }
                        }
                        else if (EtiquetaDefAA(_lstEtiquetasAANumVital, drTemp["VALORCAMPO"].ToString()))
                        {
                            if (drTemp["VALOR"].ToString() != string.Empty && !drTemp["VALOR"].ToString().Contains("-"))
                            {
                                string strNumeroVITAL = drTemp["VALOR"].ToString();
                                SolicitudDAAEIAIdentity objSolicitud = Dalc.ObtenerSolicitud(null, null, strNumeroVITAL);
                                if (objSolicitud != null && objSolicitud.IdAutoridadAmbiental > 0)
                                {
                                    Identity.IdAutoridadAmbiental = objSolicitud.IdAutoridadAmbiental;
                                    tieneEtiquetaAU = true;
                                }
                            }
                        }
                    }
                }
                if (!tieneEtiquetaAU)
                {

                    if (ds.Tables[0].Rows.Count > 1)
                    {

                        //SMLog.Escribir(Severidad.Informativo, "++++ds.Tables[0].Rows[0][0].ToString(): " + ds.Tables[0].Rows[0][0].ToString());
                        if (ds.Tables[0].Rows[0][0].ToString() == "-1")
                        {
                            throw new ApplicationException("Las ciudades incluidas en las ubicaciones de la solicitud no tienen una jurisdicción asignada, se ha terminado el proceso");
                        }
                        else
                        {
                            //Se registra en la solicitud que hubo conflicto
                            this.Identity.Conflicto = true;
                        }
                    }
                    else
                    {
                        this.Identity.IdAutoridadAmbiental = Convert.ToInt32(ds.Tables[0].Rows[0]["AUT_ID"].ToString());
                    }
                }
                //JMM - OCT 25 - 2010 - SI EL PROCESO ES UNA LICENCIA AMBIENTAL O UNA LIQUIDACION AMBIENTAL DEFINE LA AA CON EL SECTOR SI APLICA
                Proceso proceso = new Proceso(int.Parse(this.Identity.IdProcessInstance.ToString()));
                if (EtiquetaDefAA(_lstEtiquetaTramiteDefineAUxSector, proceso.PIdentity.Clave))
                {
                    if (Identity.IdSector != null)
                    {
                        SectorIdentity sector = new SectorIdentity();
                        sector.Id = Convert.ToInt32(this.Identity.IdTipoProyecto);
                        sector.IdPadre = Convert.ToInt32(this.Identity.IdSector);
                        SectorDalc sectorDalc = new SectorDalc(ref sector);

                        if (sector.PerteneceMAVDT)
                        {
                            AutoridadAmbientalDalc aDalc = new AutoridadAmbientalDalc();
                            this.Identity.IdAutoridadAmbiental = aDalc.ObtenerIdAutoridadMAVDT((int)GenParametro.AutoridadAmbientalMAVDT);
                            this.Identity.Conflicto = false;
                        }
                    }
                }
                //JMM

                #region jmartinez 14/12/2017 realizo la reasignacion de la autoridad ambiental de la solicitud en caso de que el usuario pertenezca a una corporacion o no
                if (this.Identity.IdAutoridadAmbiental > 0 && this.Identity.IdTipoTramite > 0 && this.Identity.IdSolicitante > 0)
                {
                    SMLog.Escribir(Severidad.Informativo, "inicio Resignacion Autoridad Ambiental " + this.Identity.IdTipoTramite.ToString() + "," + this.Identity.IdSolicitante.ToString() + "," + this.Identity.IdAutoridadAmbiental.ToString());
                    AutoridadAmbientalDalc aDalc2 = new AutoridadAmbientalDalc();
                    this.Identity.IdAutoridadAmbiental = aDalc2.ValidarUsuarioCorporacion(this.Identity.IdAutoridadAmbiental, this.Identity.IdSolicitante, this.Identity.IdTipoTramite);
                    SMLog.Escribir(Severidad.Informativo, "Fin Resignacion Autoridad Ambiental " + this.Identity.IdTipoTramite.ToString() + "," + this.Identity.IdSolicitante.ToString() + "," + this.Identity.IdAutoridadAmbiental.ToString());
                }
                #endregion
            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Critico, "++++" + ex.Message + " - " + ex.StackTrace);     
                throw new ApplicationException(ex.Message, ex); 
            }
        }


        /// <summary>
        /// Verifica si el expediente relacionado corresponde.
        /// </summary>
        /// <returns>true/false</returns>
        public bool VerificarExpedienteRelacionado(string idExpediente, string numeroVital)
        {
            SMLog.Escribir(Severidad.Informativo,"aud 1");
            bool result = false;
            int i = this.Dalc.VerificarExpedienteRelacionado(idExpediente, numeroVital);
            if (i == 1) { result = true; }
            SMLog.Escribir(Severidad.Informativo, "aud 2");
            return result;
        }


        public void CargarListaEtiquetas()
        {
            _objConfiguracion = new Configuracion();
            _lstEtiquetasAA = new List<String>();
            _lstEtiquetasAAxSector = new List<String>();
            _lstEtiquetasAAxTipoProy = new List<String>();
            _lstEtiquetaTramiteDefineAUxSector = new List<String>();
            _lstEtiquetasAANumVital = new List<string>();

            if (!String.IsNullOrEmpty(_objConfiguracion.ArchivoEtiquetaRadicable) && System.IO.File.Exists(_objConfiguracion.ArchivoEtiquetaRadicable))
            {
                DataSet ds = new DataSet();
                ds.ReadXml(_objConfiguracion.ArchivoEtiquetaRadicable);

                // Carga las etiquetas en memoria. 
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow drxml in dt.Rows)
                    {
                        switch (dt.TableName)
                        {
                            case "NumeroVitalDefAA":
                                this._lstEtiquetasAA.Add(drxml["ID"].ToString());
                                break;
                            case "SectorSolEstandarDefAASector":
                                this._lstEtiquetasAAxSector.Add(drxml["ID"].ToString());
                                break;
                            case "SectorSolEstandarDefAATipoProy":
                                this._lstEtiquetasAAxTipoProy.Add(drxml["ID"].ToString());
                                break;
                            case "TramiteDefineAutoridadPorSector":
                                this._lstEtiquetaTramiteDefineAUxSector.Add(drxml["ID"].ToString());
                                break;
                            case "NumeroVitalDefAANumVital":
                                this._lstEtiquetasAANumVital.Add(drxml["ID"].ToString());
                                break;
                        }
                    }
                }
            }
        }

        public bool EtiquetaDefAA(List<string> lst, string label)
        {
            if (lst == null)
            {
                CargarListaEtiquetas();
            }

            int i = lst.IndexOf(label);
            if (i == -1)
            {
                return false;
            }
            else { return true; }
        }


        /// <summary>
        /// hava:
        /// obtiene el tipo de tramite asociado a un numero silpa
        /// </summary>
        /// <param name="numerosilpa"></param>
        /// <param name="tipoTramite"></param>
        /// <param name="nombreTramite"></param>
        public void ObtenerTipoTramite(string numerosilpa, out string tipoTramite, out string nombreTramite, out string nombreSolicitante)
        {
            this.Dalc.ObtenerNombreTipoTramite(numerosilpa,  out tipoTramite, out nombreTramite, out nombreSolicitante);
        }



    }
}
