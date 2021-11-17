using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.Generico;
using System.Data;
using SILPA.AccesoDatos.DAA;
using SILPA.Comun;
using SoftManagement.Log;
using SILPA.LogicaNegocio.Formularios;
using System.IO;

namespace SILPA.LogicaNegocio.Generico
{
    /// <summary>
    /// Clase que contiene la lógica de negocio para utilizar actividades radicables y generar registros
    /// de radicación con ellas
    /// </summary>
    public class ActividadRadicable
    {

        ActividadRadicableIdentity _actividadRadicable;


        /// <summary>
        /// Verifica si la actividad entregada por el BPM se encuentra en la lista de actividades Radicables
        /// En caso de existir, asigna el primer registro encontrado a la variable global de ActividadRadicable
        /// </summary>
        /// <param name="activityInstanceID">Instancia de la Actividad</param>
        /// <param name="processInstanceID">Instancia del Proceso</param>
        /// <returns>Verdadero si la actividad existe</returns>
        public bool ObtenerActividad(long activityInstanceID, long processInstanceID)
        {
            int _intIDActividad = 0;
            ActividadRadicableDalc _actividadDalc = new ActividadRadicableDalc();
            _intIDActividad = _actividadDalc.ObtenerActividadPorInstancia((Int32.Parse(activityInstanceID.ToString())));
            List<ActividadRadicableIdentity> _listaActividades = _actividadDalc.ListaActividadesRadicables(_intIDActividad);
            if (_listaActividades.Count > 0)
            {
                this._actividadRadicable = _listaActividades[0];
                return true;
            }
            else
                return false;

        }


        /// <summary>
        /// HAVA: 09-oct-2010
        /// Genera el fus en rtf para las actividades radicables 
        /// que usan formularios de informacion adicional
        /// </summary>
        /// <param name="idProcessInstance">int: identificador del processinstance</param>
        /// <param name="idformInstance">int: identificador del forminstance</param>
        /// <param name="ruta">string: ruta</param>
        public void generarFusActividadRadicable(int idProcessInstance, int idformInstance, string ruta,string usuario,int radId)
        {
            if (string.IsNullOrEmpty(ruta))
            {
                SILPA.Comun.TraficoDocumento objTrafico = new SILPA.Comun.TraficoDocumento();
                ruta = objTrafico.CrearDirectorio(objTrafico.FileTraffic, idProcessInstance.ToString(), usuario);
                RadicacionDocumento objRadicacion = new RadicacionDocumento();
                objRadicacion.ActualizarRutaRadicacion(radId, ruta);
            }
            CrearFormularios generador = new CrearFormularios();
            DataSet dsInfo= generarFusActividadRadicableRtf(idProcessInstance, ruta, idformInstance);
            string nombreArchivo = ValidarExisteArchivo(ruta, "InfoAdicional_" + idProcessInstance.ToString() + "_" + idformInstance.ToString(), ".pdf");
            generador.GenerarFormularioPdfActividadRadicable(ruta, nombreArchivo, dsInfo);

            #region Generarpdf acuse recibido
            SILPA.LogicaNegocio.Generico.NumeroSilpa numero;
            numero = new SILPA.LogicaNegocio.Generico.NumeroSilpa();


            string mensajeSolicitudRecibida = string.Empty, numeroVITAL = string.Empty;
            numero = new SILPA.LogicaNegocio.Generico.NumeroSilpa();
            mensajeSolicitudRecibida = numero.mensajeSolicitudRecibida(idProcessInstance, out numeroVITAL);
            if (!string.IsNullOrEmpty(mensajeSolicitudRecibida))
            {
                (new CrearFormularios()).GenerarRecepcionSolicitudTramite(numeroVITAL, mensajeSolicitudRecibida, ruta);
            }

            #endregion

            //string direccion = System.Configuration.ConfigurationSettings.AppSettings.Get("DireccionFus").ToString();
            //string direccion = System.Configuration.ConfigurationManager.AppSettings.Get("DireccionFus").ToString();


        }

        private static string ValidarExisteArchivo(string file, string archivo, string extension)
        {
            int i = 0;
            if (File.Exists(file + archivo + extension))
            {
                try
                {
                    File.Delete(file + archivo + extension);
                }
                catch (Exception ex)
                {
                    SMLog.Escribir(Severidad.Critico, "ImpresionFUSFachada + ValidarExisteArchivo: No se pudo Eliminar el archivo. " + file + archivo + extension + "  -- " + ex.ToString());
                }
            }
            return archivo + extension;
        }

        public DataSet generarFusActividadRadicableRtf(int idProcessInstance, string ruta, int idformInstance)
        {
            string linea;
            SILPA.LogicaNegocio.ImpresionesFus.GenerarArchivoFus _fus = new SILPA.LogicaNegocio.ImpresionesFus.GenerarArchivoFus();
            SILPA.LogicaNegocio.Generico.Persona _per = new SILPA.LogicaNegocio.Generico.Persona();
            SILPA.AccesoDatos.Generico.PersonaIdentity _persona = new PersonaIdentity();
            SILPA.AccesoDatos.Generico.DireccionPersonaIdentity _direccion = new DireccionPersonaIdentity();

            SILPA.AccesoDatos.Generico.NumeroSilpaDalc silpa = new NumeroSilpaDalc();
            string numeroVital = silpa.NumeroSilpa(Convert.ToInt32(idProcessInstance));

            List<SILPA.AccesoDatos.ImpresionesFus.ImpresionArchivoFus> listFusEnyity;
            listFusEnyity = _fus.CrearArchivoInfoAdicional((int)idProcessInstance, idformInstance);

            DataSet dsInfoAdd = new DataSet();

            DataTable dtInfoAdd = new DataTable();
            dtInfoAdd.Columns.Add("NOMBRE_FORMULARIO");
            dtInfoAdd.Columns.Add("NUMERO_VITAL");


            DataTable dtDetalle = new DataTable();           
            dtDetalle.Columns.Add("CAMPO");
            dtDetalle.Columns.Add("VALOR");

           

            DataRow row = dtInfoAdd.NewRow();
            string nomArch = ruta + "InfoAdicional_" + idProcessInstance.ToString() + "_" + idformInstance.ToString() + ".rtf";

            nomArch = ruta+ValidarExisteArchivo(ruta, "InfoAdicional_" + idProcessInstance.ToString() + "_" + idformInstance.ToString(), ".rtf");
            if (!String.IsNullOrEmpty(ruta) && ruta != "")
            {
                using (System.IO.StreamWriter arc = new System.IO.StreamWriter(nomArch, true, Encoding.UTF8))
                {
                    //Acá deben ir la cosas de encabezado del archivo. Nombre del Documento del Fus.
                    linea = "----------------------------------------------------------------------";
                    arc.WriteLine(linea);
                    linea = "Nombre del formulario: " + _fus.NombreFormulario((int)idProcessInstance);
                    row["NOMBRE_FORMULARIO"] = _fus.NombreFormulario((int)idProcessInstance);
                    arc.WriteLine(linea);
                    linea = "El Numero Vital de la Operacion es : " + numeroVital.Replace("<br />",@" - ");
                    row["NUMERO_VITAL"] = numeroVital.Replace("<br />", @" - ");
                    arc.WriteLine(linea);
                    linea = "----------------------------------------------------------------------";
                    arc.WriteLine(linea);

                    //Ahora deben aparecer los datos personales de quien diligencio el FUS.
                    //_per.ObternerPersonaByProcessInstace((int)idProcessInstance);
                    //_persona = _per.Identity;
                    //_direccion = _per.IdentityDir;

                    //arc.Write(CadenaUsuario(_persona, _direccion));

                    foreach (SILPA.AccesoDatos.ImpresionesFus.ImpresionArchivoFus fus in listFusEnyity)
                    {
                        DataRow row2 = dtDetalle.NewRow();
                        row2["CAMPO"] = fus.strCampo;
                        row2["VALOR"] = fus.strValor;
                        linea = fus.strCampo.ToUpper().ToString() + ":    " + fus.strValor;
                        arc.WriteLine(linea);
                        linea = "";
                        dtDetalle.Rows.Add(row2);
                    }

                }                
            }
            dtInfoAdd.Rows.Add(row);
            dtInfoAdd.TableName="FORMULARIO";
            dsInfoAdd.Tables.Add(dtInfoAdd);
            dtDetalle.TableName = "DETALLES";
            dsInfoAdd.Tables.Add(dtDetalle);
            return dsInfoAdd;
        }


        /// <summary>
        /// Utiliza los parámetros asignados para buscar la solicitud y el formulario de la actividad
        /// </summary>
        /// <param name="Client">Cliente - SoftManagement</param>
        /// <param name="UserID">ID del Solicitante en eSecurity</param>
        /// <param name="activityInstanceID">ID de la Instancia de la Actividad</param>
        /// <param name="processInstanceID"></param>
        /// <param name="entryDataType"></param>
        /// <param name="entryData"></param>
        /// <param name="idEntryData"></param>
        /// <remarks>El objeto ActividadRadicableIdentity de la Instancia de Esta Clase debe tener datos para utilizar este método</remarks>
        public void GenerarRadicacion(string Client, long UserID, long activityInstanceID, long processInstanceID, string entryDataType, string entryData, string idEntryData)
        {
            Formulario _formulario = new Formulario();
            PersonaIdentity _persona = new PersonaIdentity();
            PersonaDalc _personaDalc = new PersonaDalc();
            RadicacionDocumento _objRadicacionDocumento = new RadicacionDocumento();
            SolicitudDAAEIADalc _solicitudDalc = new SolicitudDAAEIADalc();
            SolicitudDAAEIAIdentity _solicitud = new SolicitudDAAEIAIdentity();

            // byte[] bytesArchivoRtf =  null;
            //string archivoRtf = string.Empty;

            SMLog.Escribir(Severidad.Informativo, "Inicio Metodo GenerarRadicacion " + "PARAMETROS: Client = " + Client + ", UserID = " + UserID + ", activityInstanceID = " + activityInstanceID + ", processInstanceID = " + processInstanceID + ", entryDataType = " + entryDataType + ", entryData = " + entryData + " ,idEntryData = " + idEntryData);
            

            DataSet dsPadre = _formulario.ConsultarListadoFormularios(processInstanceID.ToString(), activityInstanceID.ToString());
            if (dsPadre != null)
            {                
                if (dsPadre.Tables[0].Rows.Count>0)
                {
                    if (dsPadre.Tables[0].Rows[0]["ID"] != DBNull.Value)
                    {                        
                        DataSet dsListaHijos = _formulario.ConsultarListadoFormularios("childForm", Convert.ToString(dsPadre.Tables[0].Rows[0]["ID"]));
                        if (dsListaHijos != null)
                        {
                            if (dsListaHijos.Tables[0].Rows.Count > 0)
                            {
                                //Inicio los datos de RAdicación                                
                                _solicitud = _solicitudDalc.ObtenerSolicitud(null, processInstanceID, null);

                                //Este codigo estaba comentado.                                
                                _persona = _personaDalc.BuscarPersonaByUserId(UserID.ToString());
                                //Recorro los hijos y agrego sus datos a una lista de Informacion Adicional
                                List<InformacionAdicionalIdentity> listaInfoAdicional = new List<InformacionAdicionalIdentity>();
                                //La listaInfoAdicionalRadicar genera un registro de radicado por todos los items
                                List<InformacionAdicionalIdentity> listaInfoAdicionalRadicar = new List<InformacionAdicionalIdentity>();
                                //La listaInfoAdicionalRadicar genera un registro de radicado por cada item
                                List<InformacionAdicionalIdentity> listaInfoAdicionalRadicada = new List<InformacionAdicionalIdentity>();
                                List<String> listaNombresArchivos = new List<string>();
                                //InformacionAdicionalIdentity _infoAdicional = new InformacionAdicionalIdentity();
                                List<Byte[]> listaBytes;
                                foreach (DataRow dr in dsListaHijos.Tables[0].Rows)
                                {
                                    //_infoAdicional = new InformacionAdicionalIdentity();
                                    listaInfoAdicional = _formulario.ObtenerDatosFormularioInfoAdicional(Convert.ToInt64(dr["ID"]));
                                    //MODIFICACION: JACOSTA. Se recorre la lista de la informacion adicional (Archivos adjuntos)
                                    foreach (InformacionAdicionalIdentity info in listaInfoAdicional)
                                    {//Se verifica si el registro tiene algún dato en Número de RAdicado
                                        //Se agrupan los registro en listas diferentes
                                        if (!String.IsNullOrEmpty(info.NumeroRadicado))
                                        {
                                            listaInfoAdicionalRadicada.Add(info);
                                        }
                                        else
                                        {
                                            listaInfoAdicionalRadicar.Add(info);
                                        }
                                    }

                                }

                                if (listaInfoAdicionalRadicar.Count > 0)
                                {
                                    listaBytes = new List<byte[]>();
                                    foreach (InformacionAdicionalIdentity info1 in listaInfoAdicionalRadicar)
                                    {
                                        if (!string.IsNullOrEmpty(info1.RutaDocumento))
                                        {
                                            listaNombresArchivos.Add(info1.RutaDocumento);
                                        }
                                    }



                                    //jmartinez 22-05-2018 se realiza comentario a esta linea debido la lectura de bytes no se esta utilizando en el metodo de radicacion 
                                    //ya que los archivos se encentran en el mismo servidor y lo que realiza al final es mover los archivos                                  
                                    //listaBytes = _objRadicacionDocumento.ObtenerListaDeBytesBPM(listaNombresArchivos);
                                    listaBytes = null;

                                    _objRadicacionDocumento._objRadDocIdentity.NumeroSilpa = processInstanceID.ToString();
                                    _objRadicacionDocumento._objRadDocIdentity.IdSolicitante = UserID.ToString();
                                    _objRadicacionDocumento._objRadDocIdentity.LstBteDocumentoAdjunto = listaBytes;
                                    _objRadicacionDocumento._objRadDocIdentity.LstNombreDocumentoAdjunto = listaNombresArchivos;
                                    _objRadicacionDocumento._objRadDocIdentity.IdAA = _solicitud.IdAutoridadAmbiental;
                                    _objRadicacionDocumento._objRadDocIdentity.NumeroVITALCompleto = _solicitud.NumeroSilpa;
                                    _objRadicacionDocumento._objRadDocIdentity.IdentificacionSolicitante = _persona.NumeroIdentificacion;
                                    _objRadicacionDocumento._objRadDocIdentity.FechaSolicitud = _solicitud.FechaCreacion;                                    
                                    _objRadicacionDocumento.RadicarDocumento();

                                    //hava:09-oct-10
                                    this.generarFusActividadRadicable((int)processInstanceID, (int)activityInstanceID, _objRadicacionDocumento._objRadDocIdentity.UbicacionDocumento, _objRadicacionDocumento._objRadDocIdentity.IdSolicitante.ToString(), _objRadicacionDocumento._objRadDocIdentity.IdRadicacion);

                                }
                                else
                                {
                                    // Se agrega el ítem rtf:
                                    // listaBytes = new List<byte[]>();

                                    listaBytes = new List<byte[]>();
                                    foreach (InformacionAdicionalIdentity info1 in listaInfoAdicionalRadicada)
                                    {
                                        listaNombresArchivos.Add(info1.RutaDocumento);
                                    }
                                    //SMLog.Escribir(Severidad.Informativo, "Borrar ObtenerListaDeBytesBPM(2): listaNombresArchivos" + listaNombresArchivos.ToArray().Length);
                                    
                                    
                                    //jmartinez 22-05-2018 se realiza comentario a esta linea debido la lectura de bytes no se esta utilizando en el metodo de radicacion 
                                    //ya que los archivos se encentran en el mismo servidor y lo que realiza al final es mover los archivos                                  
                                    //listaBytes = _objRadicacionDocumento.ObtenerListaDeBytesBPM(listaNombresArchivos);
                                    listaBytes = null;

                                    //listaNombresArchivos.Add(archivoRtf);
                                    // listaBytes.Add(bytesArchivoRtf);

                                    _objRadicacionDocumento._objRadDocIdentity.NumeroSilpa = processInstanceID.ToString();
                                    _objRadicacionDocumento._objRadDocIdentity.IdSolicitante = UserID.ToString();
                                    _objRadicacionDocumento._objRadDocIdentity.LstBteDocumentoAdjunto = listaBytes;
                                    _objRadicacionDocumento._objRadDocIdentity.LstNombreDocumentoAdjunto = listaNombresArchivos;
                                    _objRadicacionDocumento._objRadDocIdentity.IdAA = _solicitud.IdAutoridadAmbiental;
                                    _objRadicacionDocumento._objRadDocIdentity.NumeroVITALCompleto = _solicitud.NumeroSilpa;
                                    _objRadicacionDocumento._objRadDocIdentity.IdentificacionSolicitante = _persona.NumeroIdentificacion;
                                    _objRadicacionDocumento._objRadDocIdentity.FechaSolicitud = _solicitud.FechaCreacion;                                    
                                    _objRadicacionDocumento.RadicarDocumento();

                                    //hava:09-oct-10
                                    this.generarFusActividadRadicable((int)processInstanceID, (int)activityInstanceID, _objRadicacionDocumento._objRadDocIdentity.UbicacionDocumento.ToString(), _objRadicacionDocumento._objRadDocIdentity.IdSolicitante, (int)_objRadicacionDocumento._objRadDocIdentity.Id);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
