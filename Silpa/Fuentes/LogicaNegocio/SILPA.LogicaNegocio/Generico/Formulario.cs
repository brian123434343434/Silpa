using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.DAA;
using System.Data;
using SILPA.LogicaNegocio.DAA;
using SILPA.LogicaNegocio;
using SILPA.LogicaNegocio.Generico;
using System.IO;
using SoftManagement.Log;
using SILPA.Comun;

namespace SILPA.LogicaNegocio.Generico
{
    public class Formulario
    {
        private FormularioDalc _objDalc;
        private DataSet _dsDatos;

        private string DatosSolicitante = "Datos del Solicitante";
        private string Representante = "Datos de Representante Legal";
        private string Apoderado = "Datos del Apoderado";
        private string Sector = "Sector";
        private string TipoProyecto = "Tipo de Proyecto";
        private string Multiregistro = "Multiregistro";
        private string adjuntarDocumento = "Adjuntar Documento";
        private string noRadicado = "Nº de Radicado";

        /// <summary>
        ///  contiene el listado de etiquetas que indican carga de archivos.
        /// </summary>
        private List<String> _lstEtiquetasRadicables;
        private List<String> _lstValoresRadicables;
        private List<String> _lstEtiquetaCorreoForma;
        private List<String> _lstEtiquetaCorreoArchivo;

        /// <summary>
        /// Contiene la configuracion desde el web.congfig de la webApp
        /// </summary>
        private Configuracion _objConfiguracion;
        
        public Formulario() 
        {
            
            _lstEtiquetasRadicables=  new List<string>();
            _lstValoresRadicables =  new List<string>();
            _lstEtiquetaCorreoForma=  new List<string>();
            _lstEtiquetaCorreoArchivo=  new List<string>();

            this._objDalc = new FormularioDalc();
            _objConfiguracion = new Configuracion();
            this.CargarListaEtiquetas();
        }

        /// <summary>
        /// hava:08-oct-10
        /// Determina si la etiqueta actual es de un formbuilder de radicación
        /// </summary>
        /// <param name="label">string: nombre de la etiqeuta de radicación</param>
        /// <returns>bool:  true/false</returns>
        public bool ExisteItem(List<string> lst, string label)
        {
            int i = lst.IndexOf(label);
            if (i == -1)
            {
                return false;
            }
            else { return true; }
        }


        /// <summary>
        /// hava:08-oct-10
        /// Determina si la etiqueta actual es de un formbuilder de radicación
        /// </summary>
        /// <param name="label">string: nombre de la etiqeuta de radicación</param>
        /// <returns>bool:  true/false</returns>
        public bool EsEtiquetaRadicable(string label)
        {
            bool esRadicable = false;
            foreach (var etiqueta in this._lstEtiquetasRadicables)
	        {
                if (label.ToUpper().Contains(etiqueta.ToUpper()))
                    esRadicable = true;
	        }
            return esRadicable;
        }

        /// <summary>
        /// hava:08-oct-10
        /// Determina si la etiqueta actual es de un formbuilder de radicación
        /// </summary>
        /// <param name="label">string: nombre de la etiqeuta de radicación</param>
        /// <returns>bool:  true/false</returns>
        public bool EsValorRadicado(string radicado)
        {
            int i = this._lstValoresRadicables.IndexOf(radicado);
            if (i == -1)
            {
                return false;
            }
            else { return true; }
        }

        /// <summary>
        /// hava:08-oct-10
        /// Carga las etiquetas que indican radicaición
        /// </summary>
        public void CargarListaEtiquetas()
        {
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
                              case "labelArchivo":
                                  this._lstEtiquetasRadicables.Add(drxml["ID"].ToString());
                                  break;
                              case "labelRadicado":
                                  this._lstValoresRadicables.Add(drxml["ID"].ToString());
                                  break;
                              case "labelCorreoArchivo":
                                  this._lstEtiquetaCorreoArchivo.Add(drxml["ID"].ToString());
                                  break;
                              case "labelCorreoForma":
                                  this._lstEtiquetaCorreoForma.Add(drxml["ID"].ToString());
                                  break;
                          } 
                      }                
                    //if (dt.TableName == "labelArchivo")
                    //{
                    //    this._lstEtiquetasRadicables.Add(dt.Rows[0]["ID"].ToString());
                    //}

                    //if (dt.TableName == "labelRadicado")
                    //{
                    //    this._lstValoresRadicables.Add(dt.Rows[0]["ID"].ToString());
                    //}

                    //if (dt.TableName == "labelCorreoArchivo")
                    //{
                    //    this._lstEtiquetasRadicables.Add(dt.Rows[0]["ID"].ToString());
                    //}

                    //if (dt.TableName == "labelCorreoForma")
                    //{
                    //    this._lstEtiquetasRadicables.Add(dt.Rows[0]["ID"].ToString());
                    //}

                }
            }
        }

        /// <summary>
        /// Método que llena los datos del objeto SolicitudDAAEIA
        /// </summary>
        /// <param name="objSolcitudDAA">Objeto como referencia </param>
        /// <param name="int64FormInstance">int:_ forminstanceque contiene los datos</param>
        public void ObtenerDatosFormularioSolcitudDaa(ref SolicitudDAAEIAIdentity objSolcitudDAA) 
        {
            //this._dsDatos = this._objDalc.ConsultarDatosFormulario((Int64)objSolcitudDAA.IdProcessInstance);
            //this._dsDatos = this._objDalc.ConsultarDatosFormulario(IdProcessInstance);
            this._dsDatos = this._objDalc.ConsultarDatosFormulario(objSolcitudDAA.IdProcessInstance);
            
            ///Recorrido de los datos
            foreach (DataRow dr in this._dsDatos.Tables[0].Rows)
            {
                if (dr["VALORCAMPO"].ToString() == "Sector") { objSolcitudDAA.IdSector = int.Parse(dr["VALOR"].ToString()); }
                if (dr["VALORCAMPO"].ToString() == "Tipo de Proyecto") { objSolcitudDAA.IdTipoProyecto = int.Parse(dr["VALOR"].ToString()); }

            }

        }

        /// <summary>
        /// Obtiene los Datos del formulario de información Adicional
        /// </summary>
        /// <param name="formInstance">Instancia de Formulario</param>
        /// <returns>Objeto con Datos para Información Adicional</returns>
        /// MODIFICACION: JACOSTA. Se ajusta el metodo para retorne mas de un archivo adjunto esto se hace ya que pueden anexarce mas de una archvio en el formulario 92
        public List<InformacionAdicionalIdentity> ObtenerDatosFormularioInfoAdicional(long formInstance)
        {
            SMLog.Escribir(Severidad.Informativo,"ObtenerDatosFormularioInfoAdicional");
            List<InformacionAdicionalIdentity> LstInfoAdicional = new List<InformacionAdicionalIdentity>();
            this._dsDatos = this._objDalc.ConsultarListadoCamposForm(formInstance);
           

            ///Recorrido de los datos
            foreach (DataRow dr in this._dsDatos.Tables[0].Rows)
            {
                InformacionAdicionalIdentity _objInfoAdicional = new InformacionAdicionalIdentity();
                // Etiquetas de la forma  115
                //SMLog.Escribir(Severidad.Informativo, "ObtenerDatosFormularioInfoAdicional entrando");
                if (dr["TEXTO"].ToString().Trim() != string.Empty && dr["TEXTO"] != DBNull.Value)
                {
                    if (this.EsEtiquetaRadicable(dr["TEXTO"].ToString().Trim())==true) 
                    {
                        _objInfoAdicional.RutaDocumento = Convert.ToString(dr["VALOR"].ToString());
                        //SMLog.Escribir(Severidad.Informativo, "_objInfoAdicional.RutaDocumento" + _objInfoAdicional.RutaDocumento.ToString());
                    }
                }
                
                // el valor de radicado...
                if (dr["TEXTO"].ToString().Trim() != string.Empty && dr["TEXTO"] != DBNull.Value)
                {
                    if (this.EsValorRadicado(dr["TEXTO"].ToString().Trim()) == true)
                    {
                        _objInfoAdicional.NumeroRadicado = Convert.ToString(dr["VALOR"].ToString());
                        SMLog.Escribir(Severidad.Informativo, "_objInfoAdicional.NumeroRadicado" + _objInfoAdicional.NumeroRadicado.ToString());
                    }
                }
                LstInfoAdicional.Add(_objInfoAdicional);

                //if (dr["TEXTO"].ToString().Trim().Equals(adjuntarDocumento))
                //{ 
                //    _objInfoAdicional.RutaDocumento=Convert.ToString(dr["VALOR"].ToString()); 
                //}

                //if (dr["TEXTO"].ToString().Trim().Equals(noRadicado)) 
                //{ 
                //    _objInfoAdicional.NumeroRadicado=Convert.ToString(dr["VALOR"].ToString()); 
                //}
            }

            return LstInfoAdicional;
        }

        /// <summary>
        /// Obtiene los datos de Autoridad Ambiental
        /// </summary>
        /// <param name="objSolcitudDAA">Objeto de la Solicitud Estándar</param>
        public void ObtenerAAFormularioSolcitudEstandar(ref SolicitudDAAEIAIdentity objSolcitudDAA)
        {
            //this._dsDatos = this._objDalc.ConsultarDatosFormulario((Int64)objSolcitudDAA.IdProcessInstance);
            //this._dsDatos = this._objDalc.ConsultarDatosFormulario(IdProcessInstance);
            SMLog.Escribir(Severidad.Informativo, "++++Inicio de ObtenerAAFormularioSolcitudEstandar");    
            this._dsDatos = this._objDalc.ConsultarDatosFormulario(objSolcitudDAA.IdProcessInstance);

            ///Recorrido de los datos
            foreach (DataRow dr in this._dsDatos.Tables[0].Rows)
            {
                SMLog.Escribir(Severidad.Informativo, "++++Hay Datos");    
                SMLog.Escribir(Severidad.Informativo, "++++dr[VALORCAMPO].ToString()" + dr["VALORCAMPO"].ToString());    
                if (dr["VALORCAMPO"].ToString() == "Autoridad Ambiental") 
                {
                    if (dr["VALOR"] !=DBNull.Value)
                    {
                        SMLog.Escribir(Severidad.Informativo, "++++No es Nulo");    
                        if (!dr["VALOR"].ToString().Equals(""))
                        {
                            SMLog.Escribir(Severidad.Informativo, "++++No es vacio");    
                            objSolcitudDAA.IdAutoridadAmbiental = int.Parse(dr["VALOR"].ToString());
                        }
                    }
                    
                }

            }

        }

        /// <summary>
        /// Lista los campos multi-registro por identificador del instancia del proceso
        /// </summary>
        /// <param name="int64IdProcessInstance">Identificador de la instacia de proceso</param>
        /// <returns>
        /// DataSet -> Campos [idFormInstance - TEXTO - FORMULARIO - DATO ]
        /// </returns>
        public DataSet ListarCamposMultiRegistrosByProcessIns(int int64IdProcessInstance)
        {
            FormularioDalc obj = new FormularioDalc();
            return obj.ConsultarListadoCamposMultiRegistros(int64IdProcessInstance);
        }

        /// <summary>
        /// Lista los campos por identificador del instancia del proceso
        /// </summary>
        /// <param name="int64IdProcessInstance">Identificador de la instacia de formulario</param>
        /// <returns>
        /// DataSet -> Campos [TEXTO - VALOR]
        /// </returns>
        public DataSet ConsultarListadoCamposForm(int idFormInstance)
        {
            FormularioDalc obj = new FormularioDalc();
            return obj.ConsultarListadoCamposForm(idFormInstance);
        }

        /// <summary>
        /// Consulta la lista de Formularios para el Proceso, y/o la Actividad, o para el Formulario Padre dado
        /// </summary>
        /// <param name="idProcessInstance">Instancia de Proceso</param>
        /// <param name="idActivityInstance">Instancia de Actividad o Formulario Padre</param>
        /// <returns>Dataset con la lista de ID de formularios</returns>
        /// <remarks>idProcessInstance = "childForm" si se buscan formularios hijos</remarks>
        public DataSet ConsultarListadoFormularios(string idProcessInstance, string idActivityInstance)
        {
            this._dsDatos = null;
            return this._objDalc.ConsultarListadoFormulario(idProcessInstance, idActivityInstance);
        }


        /// <summary>
        /// Hava: 04-oct-10
        /// actualiza el identificador del participante para el proceso 
        /// Cesion de derechos
        /// </summary>
        public void ActualizarParticipanteFormulario(long IdProcessInstance) 
        {
            this._objDalc.ActualizarParticipanteCesionDerechos(IdProcessInstance);
        }

        public void ConsultaDatosEnvioCorreoEE(string IDENTRYDATA, Int64 IDAPPUSER, string IDFORM, string str_rutaArchivo, string str_AA)
        {
            DataSet _dsDatos;
            String str_NombreSolicitante = "";
            String str_CorreoSolicitante = "";
            String str_Mensaje = "";
            String str_NombreArchivo = "";
            string str_archivos = "";

            _dsDatos = _objDalc.ObtenerDatosCorreoRespuestaEE(IDENTRYDATA, IDAPPUSER, IDFORM);

            if (_dsDatos.Tables.Count > 0)
            {
                foreach (DataRow dr in _dsDatos.Tables[0].Rows)
                {
                    if (dr["text"].ToString().Trim() != string.Empty && dr["text"] != DBNull.Value)
                    {
                        if (this.ExisteItem(_lstEtiquetaCorreoForma, dr["text"].ToString().Trim()))
                        {
                            str_Mensaje = dr["value"].ToString();
                            str_NombreSolicitante = dr["Name"].ToString();
                            str_CorreoSolicitante = dr["EMail"].ToString();
                        }

                        if (this.ExisteItem(_lstEtiquetaCorreoArchivo, dr["text"].ToString().Trim()))
                        {
                            str_NombreArchivo = dr["value"].ToString();
                            string rutaGataca = _objConfiguracion.DirectorioGattaca;
                            if (File.Exists(rutaGataca+str_NombreArchivo))
                            {
                                File.Copy(rutaGataca + str_NombreArchivo, str_rutaArchivo + str_NombreArchivo);
                                str_archivos += str_rutaArchivo + str_NombreArchivo + ";";
                            }
                            else if (File.Exists(str_rutaArchivo + str_NombreArchivo))
                            {
                                str_archivos += str_rutaArchivo + str_NombreArchivo + ";";
                            }
                        }
                    }
                }
                ICorreo.Correo.EnviarCorreoRstaEE(str_CorreoSolicitante, str_NombreSolicitante, str_Mensaje, str_archivos.Trim(), str_AA);
            }
        }



    }
}
