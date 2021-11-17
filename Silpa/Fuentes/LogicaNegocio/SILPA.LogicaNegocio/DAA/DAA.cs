using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.Generico;
using System.IO;
using System.Xml;
using SILPA.Comun;
using SILPA.AccesoDatos.DAA;
using Silpa.Workflow;
using System.Data;
using SILPA.LogicaNegocio.Generico;
using SoftManagement.Log;

namespace SILPA.LogicaNegocio.DAA
{
    public class DAA
    {

        /// <summary>
        /// Hava:14-mayo-10
        /// Ejecuta el reenvío de la solicitud DAA
        /// </summary>
        /// <param name="xmlDatosReenvio">string: xml de datos para reenvío</param>
        /// <returns>bool</returns>
        public bool ReenviarSolicitud(string xmlDatosReenvio)
        {
            try
            {
	            Configuracion objConfiguracion = new Configuracion();
	            
	            SILPA.AccesoDatos.DAA.ReenviarSolicitud objReenviar = new ReenviarSolicitud();
	            objReenviar = (ReenviarSolicitud)objReenviar.Deserializar(xmlDatosReenvio);
	
	            SILPA.AccesoDatos.DAA.DAADalc DaaDalc = new DAADalc();
	
	            string ruta = string.Empty;
	            long result = DaaDalc.RecibirSolicitudReenvio(objReenviar, ref ruta, objConfiguracion.FileTraffic);

	            if (string.IsNullOrEmpty(ruta)) 
	            {
	                throw new InvalidOperationException("El procedimiento COR_REASIGNAR_AUTORIDAD_RADICACION entregó ruta vacía");
	            }

	            TraficoDocumento tf = new TraficoDocumento();
	            //string usuario =  "Reenvio_"+objReenviar.autIdAsignada.ToString();
	            tf.RecibirDocumentoReenvio(objReenviar.documentoAdjunto,ruta);
	            //tf.RecibirDocumento(objReenviar.NumeroSilpa, usuario, lstBytes, lstNombres);
	            //
	
	            string condicion = string.Empty;
	
	            if (objReenviar.IdTipoDocumentoVital == -1)
	            {
	                throw new InvalidOperationException("No se recibió el Tipo de Documento Vital.  El valor de IdTipoDocumentoVital es:" + objReenviar.IdTipoDocumentoVital);
	            }
	
	            // Se busca la condición dependiendo del tipo de documento asociado
	            TipoDocumentoDalc tipoDocDalc = new TipoDocumentoDalc();
	            condicion = tipoDocDalc.ObtenerCondicionTipoDocumento(objReenviar.IdTipoDocumentoVital);
	            /// si hay condición avanza la tarea
	            if (condicion != string.Empty)
	            {
	                SILPA.LogicaNegocio.AdmTablasBasicas.TipoTramite objTablasBasicas = new SILPA.LogicaNegocio.AdmTablasBasicas.TipoTramite();
	                System.Data.DataTable dtCondicion = objTablasBasicas.ListarCondicionesEspeciales(null, condicion, null);
	                if (dtCondicion.Rows.Count == 0)
	                {
	                    NumeroSilpaDalc numvital = new NumeroSilpaDalc();
	                    string strProcessinstance = numvital.ProcessInstance(objReenviar.NumeroSilpa);
	                    // si existe el processInstance  se finaliza
	                    if (!String.IsNullOrEmpty(strProcessinstance))
	                    {
                            //JNS 2019/08/22 Se realiza ajuste para que en caso de que no se cumpla condición se reporte en log
	                        ServicioWorkflow servicioWorkflow = new ServicioWorkflow();
	                        string strRes = servicioWorkflow.ValidarActividadActual(Convert.ToInt64(strProcessinstance), objConfiguracion.UserFinaliza, (long)ActividadSilpa.RegistrarInformacionDocumento);
	                        if (strRes == "")
	                            servicioWorkflow.FinalizarTarea(Convert.ToInt64(strProcessinstance), ActividadSilpa.RegistrarInformacionDocumento, objConfiguracion.UserFinaliza, condicion);
	                        else
                                SMLog.Escribir(Severidad.Informativo, "BPM - Validacion :: DAA::ReenviarSolicitud - xmlDatosReenvio: \n" + (!string.IsNullOrEmpty(xmlDatosReenvio) ? xmlDatosReenvio : "null") + "\n\n Error: " + strRes, "BPM_VAL_CON");
	                    }
	                }
	                else
	                {
	                    return true;
	                }
	            }
	            else
	            {
	                throw new InvalidOperationException(String.Format("No se encontró condición asociada al Tipo de Documento Vital {0} para avanzar tarea", objReenviar.IdTipoDocumentoVital));
	            }
	
	            return true;
            }
            catch (Exception ex)
            {
                string strException = "Validar los pasos efectuados al realizar el reenvío de la solicitud a la Autoridad Ambiental Competente.";
                throw new Exception(strException, ex);
            }
        }


        public DataSet ConsultarFormularioJurisdiccion()
        {         
            SILPA.AccesoDatos.DAA.DAADalc obj = new SILPA.AccesoDatos.DAA.DAADalc();
            return obj.ConsultarFormularioJurisdiccion();
        }

        public DataSet ConsultarListaFormularios()
        {
            SILPA.AccesoDatos.DAA.DAADalc obj = new SILPA.AccesoDatos.DAA.DAADalc();
            return obj.ConsultarListaFormularios();
        }

        public string InsertarFormularioJurisdiccion(int idPadre, int idJur)
        {
            SILPA.AccesoDatos.DAA.DAADalc obj = new SILPA.AccesoDatos.DAA.DAADalc();
            return obj.InsertarFormularioJurisdiccion(idPadre,idJur);
        }

        public string EliminarFormularioJurisdiccion(int idReg)
        {
            SILPA.AccesoDatos.DAA.DAADalc obj = new SILPA.AccesoDatos.DAA.DAADalc();
            return obj.EliminarFormularioJurisdiccion(idReg);
        }

        private DataTable pivot(DataTable dt)
        {
            
            DataTable dtRes = new DataTable();
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    dtRes.Columns.Add(EliminarCaracteresNoPermitidosXml(row["TEXT"].ToString()));
                }
                catch
                { }
            }
            DataRow rowNew = dtRes.NewRow();
            foreach (DataRow row in dt.Rows)
            {
                rowNew[EliminarCaracteresNoPermitidosXml(row["TEXT"].ToString())] = row["VALUE"].ToString();
            }
            dtRes.Rows.Add(rowNew);
            return dtRes;
        }

        private string EliminarCaracteresNoPermitidosXml(string texto)
        {
            
            texto = texto.Replace(" ", "_");//esto es ' '
            texto = texto.Replace(",", "");//esto es ','
            texto = texto.Replace(":", "");//esto es ':'
            texto = texto.Replace("?", "");//esto es '?'
            texto = texto.Replace("¿", "");//esto es '¿'
            texto = texto.Replace("º", "o");//esto es 'º'
            texto = texto.Replace("(", "");//esto es '('
            texto = texto.Replace(")", "");//esto es ')'
            texto = texto.Replace("/", "");//esto es '/'
            texto = texto.Replace("\"" , "");//esto es '\'
            texto = texto.Replace("$", "");//esto es '$'
            return texto;
        }

        private DataTable pivotHija(DataSet ds)
        {
            DataTable dtRes = new DataTable();
            DataRow dr = ds.Tables[0].Rows[0];
            DataRow[] drs = ds.Tables[1].Select("id=" + dr["ID"].ToString());
            int i = 0;
            for (i = 0; i < drs.Length; i++)
            {
                string nombreColumna = EliminarCaracteresNoPermitidosXml(drs[i]["TEXT"].ToString());
                if (!dtRes.Columns.Contains(nombreColumna))
                    dtRes.Columns.Add(nombreColumna);
            }
            
            foreach (DataRow dr2 in ds.Tables[0].Rows)
            {
                DataRow rowNew = dtRes.NewRow();
                DataRow[] drs2 = ds.Tables[1].Select("id=" + dr2["ID"].ToString());                
                for (i = 0; i < drs2.Length; i++)
                {
                    rowNew[EliminarCaracteresNoPermitidosXml(drs2[i]["TEXT"].ToString())] = drs2[i]["VALUE"].ToString();                    
                }
                dtRes.Rows.Add(rowNew);
            }       
            
            return dtRes;
        }

        public string ConsultarDatosFormulario(string numeroSilpa)
        {

            DataSet ds = new DataSet();
            SILPA.AccesoDatos.DAA.DAADalc obj = new SILPA.AccesoDatos.DAA.DAADalc();            
            DataTable dtFormulario = pivot(obj.InfoFormularioPrincipal(numeroSilpa,"V"));
            dtFormulario.TableName = "FORMULARIO";
            ds.Tables.Add(dtFormulario);
            DataTable dtTablasHijas= obj.InfoTablasHijas(numeroSilpa);
            foreach (DataRow row in dtTablasHijas.Rows)
            {
                DataTable dtTablaHija = pivotHija(obj.InfoTablaHija(int.Parse(row["IDFORM"].ToString()),int.Parse(row["IDENTRYDATA"].ToString()),"V"));
                dtTablaHija.TableName = EliminarCaracteresNoPermitidosXml(row["descriptionText"].ToString());
                ds.Tables.Add(dtTablaHija);
                ds.Relations.Add("FK" + EliminarCaracteresNoPermitidosXml(row["descriptionText"].ToString()), ds.Tables["FORMULARIO"].Columns["IDFORMULARIO"], ds.Tables[EliminarCaracteresNoPermitidosXml(row["descriptionText"].ToString())].Columns["IDFORMULARIO"], true);
                ds.Relations["FK" + EliminarCaracteresNoPermitidosXml(row["descriptionText"].ToString())].Nested = true;
            }
            ds.Prefix = "";
            SILPA.LogicaNegocio.Generico.Persona _per = new SILPA.LogicaNegocio.Generico.Persona();
            SILPA.AccesoDatos.Generico.PersonaIdentity _persona = new PersonaIdentity();
            SILPA.AccesoDatos.Generico.DireccionPersonaIdentity _direccion = new DireccionPersonaIdentity();
            _per.ObternerPersonaByNumeroSilpa(numeroSilpa);
            _persona = _per.Identity;
            _direccion = _per.IdentityDir;
            DataTable InfoAdd = CrearDataSetInfoBasica(numeroSilpa, _persona, _direccion);
            InfoAdd.Rows[0]["IDFORMULARIO"]=ds.Tables["FORMULARIO"].Rows[0]["IDFORMULARIO"].ToString();
            ds.Tables.Add(InfoAdd);            
            ds.Relations.Add("FK_InfoBas", ds.Tables["FORMULARIO"].Columns["IDFORMULARIO"], ds.Tables["InfoBas"].Columns["IDFORMULARIO"], true);
            ds.Relations["FK_InfoBas"].Nested = true;
            string crearXml = ds.GetXml();  
            return crearXml;         

        }
     


        public string ConsultarDatosFormulario(string numeroSilpa,string tipo)
        {
            DataSet ds = new DataSet();
            SILPA.AccesoDatos.DAA.DAADalc obj = new SILPA.AccesoDatos.DAA.DAADalc();
            DataTable dtFormulario = pivot(obj.InfoFormularioPrincipal(numeroSilpa, tipo));
            dtFormulario.TableName = "FORMULARIO";
            ds.Tables.Add(dtFormulario);
            DataTable dtTablasHijas = obj.InfoTablasHijas(numeroSilpa);
            foreach (DataRow row in dtTablasHijas.Rows)
            {
                DataTable dtTablaHija = pivotHija(obj.InfoTablaHija(int.Parse(row["IDFORM"].ToString()), int.Parse(row["IDENTRYDATA"].ToString()), tipo));
                dtTablaHija.TableName = EliminarCaracteresNoPermitidosXml(row["descriptionText"].ToString());

                foreach (DataRow RowHija in dtTablaHija.Rows)
                {
                    if (!ds.Tables.Contains(dtTablaHija.TableName))
                    {
                        ds.Tables.Add(dtTablaHija);
                        ds.Relations.Add("FK" + EliminarCaracteresNoPermitidosXml(row["descriptionText"].ToString()), ds.Tables["FORMULARIO"].Columns["IDFORMULARIO"], ds.Tables[EliminarCaracteresNoPermitidosXml(row["descriptionText"].ToString())].Columns["IDFORMULARIO"], true);
                        ds.Relations["FK" + EliminarCaracteresNoPermitidosXml(row["descriptionText"].ToString())].Nested = true;
                        DataTable dtTablasHijas2 = obj.InfoTablasHijasHijas(RowHija["IDSUBFORMULARIO"].ToString());
                        foreach (DataRow row2 in dtTablasHijas2.Rows)
                        {
                            DataTable dtTablaHija2 = pivotHija(obj.InfoTablaHija(int.Parse(row2["IDFORM"].ToString()), int.Parse(row2["IDENTRYDATA"].ToString()), tipo));
                            string nombreTabla = EliminarCaracteresNoPermitidosXml(row2["descriptionText"].ToString() + row["IDFORM"].ToString());
                            dtTablaHija2.TableName = nombreTabla;
                            ds.Tables.Add(dtTablaHija2);
                            ds.Relations.Add("FK" + nombreTabla, ds.Tables[EliminarCaracteresNoPermitidosXml(row["descriptionText"].ToString())].Columns["IDSUBFORMULARIO"], ds.Tables[nombreTabla].Columns["IDFORMULARIO"], true);
                            ds.Relations["FK" + nombreTabla].Nested = true;
                        }
                    }
                    else
                    {

                        #region jmartinez 22-06-2018 se adiciona validacion para los formularios hijos que poseen mas de un registro en el idsubformualrio garantizando que lo registros de los formularios nietos salgan bien
                        DataTable dtTablasHijas2;
                        if (!string.IsNullOrEmpty(RowHija["IDSUBFORMULARIO"].ToString()))
                        {
                            dtTablasHijas2 = obj.InfoTablasHijasHijas(RowHija["IDSUBFORMULARIO"].ToString());
                        }
                        else
                        {
                            dtTablasHijas2 = obj.InfoTablasHijasHijas(row["ID"].ToString());
                        }

                        #endregion

                        foreach (DataRow row2 in dtTablasHijas2.Rows)
                        {
                            DataTable dtTablaHija2 = pivotHija(obj.InfoTablaHija(int.Parse(row2["IDFORM"].ToString()), int.Parse(row2["IDENTRYDATA"].ToString()), tipo));
                            string nombreTabla = EliminarCaracteresNoPermitidosXml(row2["descriptionText"].ToString() + row["IDFORM"].ToString());
                            dtTablaHija2.TableName = nombreTabla;
                            if (!ds.Tables.Contains(dtTablaHija.TableName))
                            {
                                ds.Tables.Add(dtTablaHija2);
                                ds.Relations.Add("FK" + nombreTabla, ds.Tables[EliminarCaracteresNoPermitidosXml(row["descriptionText"].ToString())].Columns["IDSUBFORMULARIO"], ds.Tables[nombreTabla].Columns["IDFORMULARIO"], true);
                                ds.Relations["FK" + nombreTabla].Nested = true;
                            }
                            else
                            {
                                ds.Merge(dtTablaHija2);
                            }
                        }
                    }
                }
                //if (!ds.Tables.Contains(dtTablaHija.TableName))
                //{
                //    ds.Tables.Add(dtTablaHija);
                //    ds.Relations.Add("FK" + EliminarCaracteresNoPermitidosXml(row["descriptionText"].ToString()), ds.Tables["FORMULARIO"].Columns["IDFORMULARIO"], ds.Tables[EliminarCaracteresNoPermitidosXml(row["descriptionText"].ToString())].Columns["IDFORMULARIO"], true);
                //    ds.Relations["FK" + EliminarCaracteresNoPermitidosXml(row["descriptionText"].ToString())].Nested = true;
                //    DataTable dtTablasHijas2 = obj.InfoTablasHijasHijas(row["ID"].ToString());
                //    foreach (DataRow row2 in dtTablasHijas2.Rows)
                //    {
                //        DataTable dtTablaHija2 = pivotHija(obj.InfoTablaHija(int.Parse(row2["IDFORM"].ToString()), int.Parse(row2["IDENTRYDATA"].ToString()), tipo));
                //        string nombreTabla = EliminarCaracteresNoPermitidosXml(row2["descriptionText"].ToString() + row["IDFORM"].ToString());
                //        dtTablaHija2.TableName = nombreTabla;
                //        ds.Tables.Add(dtTablaHija2);

                //        ds.Relations.Add("FK" + nombreTabla, ds.Tables[EliminarCaracteresNoPermitidosXml(row["descriptionText"].ToString())].Columns["IDSUBFORMULARIO"], ds.Tables[nombreTabla].Columns["IDFORMULARIO"], true);
                //        ds.Relations["FK" + nombreTabla].Nested = true;
                //    }

            }
            ds.Prefix = "";
            SILPA.LogicaNegocio.Generico.Persona _per = new SILPA.LogicaNegocio.Generico.Persona();
            SILPA.AccesoDatos.Generico.PersonaIdentity _persona = new PersonaIdentity();
            SILPA.AccesoDatos.Generico.DireccionPersonaIdentity _direccion = new DireccionPersonaIdentity();
            _per.ObternerPersonaByNumeroSilpa(numeroSilpa);
            _persona = _per.Identity;
            _direccion = _per.IdentityDir;
            DataTable InfoAdd = CrearDataSetInfoBasica(numeroSilpa, _persona, _direccion);
            InfoAdd.Rows[0]["IDFORMULARIO"] = ds.Tables["FORMULARIO"].Rows[0]["IDFORMULARIO"].ToString();
            ds.Tables.Add(InfoAdd);
            ds.Relations.Add("FK_InfoBas", ds.Tables["FORMULARIO"].Columns["IDFORMULARIO"], ds.Tables["InfoBas"].Columns["IDFORMULARIO"], true);
            ds.Relations["FK_InfoBas"].Nested = true;
            string crearXml = ds.GetXml();
            return crearXml;

        }

        public static DataTable CrearDataSetInfoBasica(string numeroVital, PersonaIdentity persona, DireccionPersonaIdentity direccion)
        {            
            DataTable dtTable = new DataTable();
            dtTable.Columns.Add("IDFORMULARIO");
            dtTable.Columns.Add("NUMERO_VITAL");
            dtTable.Columns.Add("NOMBRE_COMPLETO");
            dtTable.Columns.Add("PRIMER_NOMBRE");
            dtTable.Columns.Add("SEGUNDO_NOMBRE");
            dtTable.Columns.Add("PRIMER_APELLIDO");
            dtTable.Columns.Add("SEGUNDO_APELLIDO");
            dtTable.Columns.Add("TIPO_IDENTIFICACION");
            dtTable.Columns.Add("NUMERO_IDENTIFICACION");
            dtTable.Columns.Add("ORIGEN_DOCUMENTO");
            dtTable.Columns.Add("NUMERO_TELEFONO");
            dtTable.Columns.Add("NUMERO_CELULAR");
            dtTable.Columns.Add("NUMERO_FAX");
            dtTable.Columns.Add("CORREO_ELECTRONICO");
            dtTable.Columns.Add("PAIS");
            dtTable.Columns.Add("CIUDAD");
            dtTable.Columns.Add("DIRECCION");
            dtTable.Columns.Add("PAIS_COR");
            dtTable.Columns.Add("CIUDAD_COR");
            dtTable.Columns.Add("DIRECCION_COR");
            dtTable.Columns.Add("RAZON_SOCIAL");
            dtTable.Columns.Add("SOLICITANTE_ID"); 

            DataRow row = dtTable.NewRow();
            row["NUMERO_VITAL"] = numeroVital;
            ParametroEntity _parametro = new ParametroEntity();
            ParametroDalc _parametroDalc = new ParametroDalc();
            _parametro.IdParametro = -1;
            _parametro.NombreParametro = "IDE_PERSONA_JURIDICA";
            _parametroDalc.obtenerParametros(ref _parametro);

            if (persona.PrimerNombre != null && !persona.PrimerNombre.Contains("Ciudadano"))
            {
                if (!persona.TipoDocumentoIdentificacion.Nombre.Equals(_parametro.Parametro))
                {
                    if (!string.IsNullOrEmpty(persona.RazonSocial))
                    {
                        row["NOMBRE_COMPLETO"] = persona.RazonSocial;
                    }
                    else
                    {
                        row["NOMBRE_COMPLETO"] = persona.PrimerNombre + " " + persona.SegundoNombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido;
                        row["PRIMER_NOMBRE"] = persona.PrimerNombre;
                        row["SEGUNDO_NOMBRE"] = persona.SegundoNombre;
                        row["PRIMER_APELLIDO"] = persona.PrimerApellido;
                        row["SEGUNDO_APELLIDO"] = persona.SegundoApellido;
                    }
                                       
                    row["TIPO_IDENTIFICACION"] = persona.TipoDocumentoIdentificacion.Nombre;
                    row["NUMERO_IDENTIFICACION"] = persona.NumeroIdentificacion;
                    row["SOLICITANTE_ID"] = persona.IdApplicationUser;

                    if (string.IsNullOrEmpty(persona.LugarExpediciónDocumento))
                        row["ORIGEN_DOCUMENTO"] = "";
                    else
                    {
                        if (persona.LugarExpediciónDocumento != "-1")
                            row["ORIGEN_DOCUMENTO"] = Municipio.obtenerNomDepMunByMunId(int.Parse(persona.LugarExpediciónDocumento));
                        else
                            row["ORIGEN_DOCUMENTO"] = "";
                    }

                    row["NUMERO_TELEFONO"] = persona.Telefono;
                    row["NUMERO_CELULAR"] = persona.Celular;
                    row["NUMERO_FAX"] = persona.Fax;
                    row["CORREO_ELECTRONICO"] = persona.CorreoElectronico;
                    row["PAIS"] = Pais.getNombrePaisById(persona._direccionPersona.PaisId);
                    row["CIUDAD"] = persona._direccionPersona.NombreDepartamento + "-" + persona._direccionPersona.NombreMunicipio;
                    row["DIRECCION"] = persona._direccionPersona.DireccionPersona;
                    row["PAIS_COR"] = Pais.getNombrePaisById(direccion.PaisId);
                    row["CIUDAD_COR"] = direccion.NombreDepartamento + "-" + direccion.NombreMunicipio;
                    row["DIRECCION_COR"] = direccion.DireccionPersona;
                }
                else
                {
                    row["RAZON_SOCIAL"] = persona.RazonSocial;
                    row["NUMERO_IDENTIFICACION"] = persona.NumeroIdentificacion;
                    row["ORIGEN_DOCUMENTO"] = persona.LugarExpediciónDocumento;
                    row["NUMERO_TELEFONO"] = persona.Telefono;
                    row["NUMERO_CELULAR"] = persona.Celular;
                    row["NUMERO_FAX"] = persona.Fax;
                    row["CORREO_ELECTRONICO"] = persona.CorreoElectronico;
                    row["PAIS"] = Pais.getNombrePaisById(persona._direccionPersona.PaisId);
                    row["CIUDAD"] = persona._direccionPersona.NombreDepartamento + "-" + persona._direccionPersona.NombreMunicipio;
                    row["DIRECCION"] = persona._direccionPersona.DireccionPersona;
                    row["PAIS_COR"] = Pais.getNombrePaisById(direccion.PaisId);
                    row["CIUDAD_COR"] = direccion.NombreDepartamento + "-" + direccion.NombreMunicipio;
                    row["DIRECCION_COR"] = direccion.DireccionPersona;
                }
            }
            else
            {
                row["RAZON_SOCIAL"] = "";
                row["NOMBRE_COMPLETO"] = "";
                row["PRIMER_NOMBRE"] = "";
                row["SEGUNDO_NOMBRE"] = "";
                row["PRIMER_APELLIDO"] = "";
                row["SEGUNDO_APELLIDO"] = "";
                row["NUMERO_IDENTIFICACION"] = "";
                row["ORIGEN_DOCUMENTO"] = "";
                row["NUMERO_TELEFONO"] = "";
                row["NUMERO_CELULAR"] = "";
                row["NUMERO_FAX"] = "";
                row["CORREO_ELECTRONICO"] = "";
                row["PAIS"] = "";
                row["CIUDAD"] = "";
                row["DIRECCION"] = "";
                row["PAIS_COR"] = "";
                row["CIUDAD_COR"] = "";
                row["DIRECCION_COR"] = "";
            }
            dtTable.Rows.Add(row);
            dtTable.TableName = "InfoBas";            
            return dtTable;
        }

        public string ConsultarEstructuraFormulario(string numeroSilpa)
        {
            DataSet ds = new DataSet();
            SILPA.AccesoDatos.DAA.DAADalc obj = new SILPA.AccesoDatos.DAA.DAADalc();
            DataTable dtFormulario = obj.InfoFormularioPrincipal(numeroSilpa,"D");
            DataTable dtRes = new DataTable();
            dtRes.Columns.Add("ID");
            dtRes.Columns.Add("PADRE");
            dtRes.Columns.Add("TEXT");
            dtRes.Columns.Add("OBSERVACION");
            int id = 1;
            DataRow newRow2 = dtRes.NewRow();
            newRow2["ID"] = id;
            newRow2["PADRE"] = 0;
            newRow2["TEXT"] = "FORMULARIO";
            newRow2["OBSERVACION"] = "";
            dtRes.Rows.Add(newRow2);
            id += 1;
            foreach (DataRow row in dtFormulario.Rows)
            {
                DataRow newRow = dtRes.NewRow();
                newRow["ID"] = id;
                newRow["PADRE"]=1;
                newRow["TEXT"] = EliminarCaracteresNoPermitidosXml(row["TEXT"].ToString());
                newRow["OBSERVACION"] = row["TYPE"].ToString();
                dtRes.Rows.Add(newRow);
                id += 1;
            }
            DataTable dtTablasHijas = obj.InfoTablasHijas(numeroSilpa);
            foreach (DataRow row in dtTablasHijas.Rows)
            {
                int padre = id;
                DataRow newRow = dtRes.NewRow();
                newRow["ID"] = id;
                newRow["PADRE"] = 1;
                newRow["TEXT"] = EliminarCaracteresNoPermitidosXml(row["descriptionText"].ToString());
                newRow["OBSERVACION"] = "";
                dtRes.Rows.Add(newRow);
                id += 1;
                DataSet dsHija = obj.InfoTablaHija(int.Parse(row["IDFORM"].ToString()), int.Parse(row["IDENTRYDATA"].ToString()), "V");
                DataRow[] drs = dsHija.Tables[1].Select("id=" + dsHija.Tables[0].Rows[0]["ID"].ToString());
                int i = 0;
                for (i = 0; i < drs.Length; i++)
                {
                    DataRow newRow3 = dtRes.NewRow();
                    newRow3["ID"] = id;
                    newRow3["PADRE"] = padre;
                    newRow3["TEXT"] =  EliminarCaracteresNoPermitidosXml(drs[i]["TEXT"].ToString());
                    newRow3["OBSERVACION"] = drs[i]["TYPE"].ToString();
                    dtRes.Rows.Add(newRow3);
                    id += 1;
                }
            }
            dtRes.TableName = "Detalles";
            ds.Tables.Add(dtRes);
           
            string crearXml = ds.GetXml();           
            return crearXml;
        }


        /// <summary>
        /// Obtiene el listado de autoridades de una solicitud
        /// </summary>
        /// <param name="numeroVital">string con el numero vital</param>
        /// <returns>string con la informacion de las autoridades</returns>
        public string ObtenerAutoridadesSolicitud(string numeroVital)
        {
            XmlSerializador objXmlSerializador = null;
            DataSet objDatos = null;
            string strAutoridades = "";

            try
            {
                DAADalc dalc = new DAADalc();
                objDatos = dalc.ObtenerAutoridadesSolicitud(numeroVital);

                //Verificar si se obtuvo datos
                if (objDatos != null && objDatos.Tables.Count > 0 && objDatos.Tables[0].Rows.Count > 0)
                {
                    //Serializar datos
                    objXmlSerializador = new XmlSerializador();
                    strAutoridades = objXmlSerializador.serializar(objDatos);
                }

                return strAutoridades;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en laconsulta de autoridades ambientales. " + ex.Message + " - " + ex.StackTrace.ToString(), ex);
            }
        }


        /// <summary>
        /// Obtiene el listado de comunidades de una solicitud
        /// </summary>
        /// <param name="numeroVital">string con el numero vital</param>
        /// <returns>string con la informacion de las comunidades</returns>
        public string ObtenerComunidadesSolicitud(string numeroVital)
        {
            XmlSerializador objXmlSerializador = null;
            DataSet objDatos = null;
            string strComunidades = "";

            try
            {
                DAADalc dalc = new DAADalc();
                objDatos = dalc.ObtenerComunidadesSolicitud(numeroVital);

                //Verificar si se obtuvo datos
                if (objDatos != null && objDatos.Tables.Count > 0 && objDatos.Tables[0].Rows.Count > 0)
                {
                    //Serializar datos
                    objXmlSerializador = new XmlSerializador();
                    strComunidades = objXmlSerializador.serializar(objDatos);
                }

                return strComunidades;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la consulta de comunidades. " + ex.Message + " - " + ex.StackTrace.ToString(), ex);
            }
        }


        /// <summary>
        /// Obtiene el listado de los registros que se encuentran pendientes de radicación en SIGPRO - (12082020 - FRAMIREZ)
        /// </summary>
        /// <param name="identificadorAutoridadAmbiental">int con el identificador de la autoridad ambiental</param>
        /// <param name="fechaInicial">string con la fecha inicial de busqueda</param>
        /// <param name="fechaFinal">string con la fecha final de busqueda</param>
        /// <returns>dataset con la informacion de los registros que se encuentran pendientes de radicar en SIGPRO</returns>

        public List<RegistroRadicarSigproEntity> ObtenerRegistrosPendientesRadicacionSigpro(int identificadorAutoridadAmbiental, string fechaInicial, string fechaFinal)
        {
            var listaRegistros = new List<RegistroRadicarSigproEntity>();
            DataSet objDatos = null;

            try
            {
                DAADalc dalc = new DAADalc();
                objDatos = dalc.ObtenerRegistrosPendientesRadicacionSigpro(identificadorAutoridadAmbiental, fechaInicial, fechaFinal);

                // Verificar si se obtuvo datos
                if (objDatos != null && objDatos.Tables.Count > 0 && objDatos.Tables[0].Rows.Count > 0)
                {
                    // Recorrer dataset y almacenar en entity

                    foreach (DataRow registro in objDatos.Tables[0].Rows)
                    {
                        var registroVital = new RegistroRadicarSigproEntity();
                        var objPersona = new PersonaDalc();

                        registroVital.NumeroVital = registro["NUMERO_VITAL"].ToString(); 
                        registroVital.FechaRadicacionVital = DateTime.Parse(registro["FECHA_GEN_RADICACION"].ToString());
                        registroVital.IdRadicacionVital = registro["ID_RADICACION"] != DBNull.Value ? int.Parse(registro["ID_RADICACION"].ToString()) : -1;
                        registroVital.NumeroVitalPadre = registro["NUMERO_VITAL_PADRE"].ToString();
                        registroVital.IdAutoridadAmbiental = registro["ID_AA"] != DBNull.Value ? int.Parse(registro["ID_AA"].ToString()) : 0;
                        registroVital.SectorPadre = registro["SECTOR_PADRE"].ToString();
                        registroVital.SectorHijo = registro["SECTOR_HIJO"].ToString();
                        registroVital.NumeroSilpa = registro["NUMERO_SILPA"] != DBNull.Value ? int.Parse(registro["NUMERO_SILPA"].ToString()) : -1;
                        registroVital.Expediente = registro["EXPEDIENTE"].ToString();
                        registroVital.PathDocumento = registro["PATH_DOCUMENTO"].ToString();
                        registroVital.IdSolicitante = registro["ID_SOLICITANTE"] != DBNull.Value ? int.Parse(registro["ID_SOLICITANTE"].ToString()) : -1;
                        registroVital.Solicitante = registro["SOLICITANTE"].ToString();
                        registroVital.Direccion = registro["DIRECCION"].ToString();
                        registroVital.Telefono = registro["TELEFONO"].ToString();
                        registroVital.CorreoElectronico = registro["CORREO_ELECTRONICO"].ToString();
                        registroVital.TipoIdentificacion = registro["TIPO_IDENTIFICACION"].ToString();
                        registroVital.Solicitud = registro["SOLICITUD"].ToString();
                        registroVital.CodigoTipoTramiteSigpro = registro["COD_TIPO_TRAMITE_SIGPRO"].ToString();
                        registroVital.NombreTipoTramiteSigpro = registro["NOMBRE_TIPO_TRAMITE_SIGPRO"].ToString();
                        registroVital.CodigoTipoDocumentalSigpro = registro["COD_TIPO_DOCUMENTAL_SIGPRO"].ToString();
                        registroVital.NombreTipoDocumentalSigpro = registro["NOMBRE_TIPO_DOCUMENTAL_SIGPRO"].ToString();
                        registroVital.MedioEnvio = registro["MEDIO_ENVIO"].ToString();
                        registroVital.CodigoDependencia = registro["COD_DEP"].ToString();
                        registroVital.NombreProyecto = registro["NOMBRE_PROYECTO"].ToString();
                        registroVital.EnviarTodosLosArchivos = registro["ENVIAR_TODOS_ARCHIVOS"] != DBNull.Value ?  Convert.ToInt32(registro["ENVIAR_TODOS_ARCHIVOS"]) : 0;
                        registroVital.TramiteVitalId = registro["TRAMITE_VITAL_ID"] != DBNull.Value ? int.Parse(registro["TRAMITE_VITAL_ID"].ToString()) : -1;
                        registroVital.SolicitudVitalId = registro["SOLICITUD_VITAL_ID"] != DBNull.Value ? int.Parse(registro["SOLICITUD_VITAL_ID"].ToString()) : -1;
                        registroVital.DescripcionRadicacion = registro["DESCRIPCION_RADICACION"].ToString();

                        registroVital.NumeroIdentificacion = objPersona.BuscarPersonaByUserId(registroVital.IdSolicitante.ToString()).NumeroIdentificacion ?? "";


                        listaRegistros.Add(registroVital);                        
                    }

                }
                return listaRegistros;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la consulta de registros pendientes por radicar en SIGPRO. " + ex.Message + " - " + ex.StackTrace.ToString(), ex);
            }
        }

        public List<List<string>> RetornarListadoArchivosRadicacionSigpro(string pathDocumentos, int idTramite, int numeroSilpa ) {
            try
            {
                DAADalc dalc = new DAADalc();
                var listaArchivosRadicar = new List<List<string>>();

                var listaArchivosTodos = new List<string>();
                var listaArchivosBase = new List<string>();
                var listaArchivosAdicionales = new List<string>();
                
                if (System.IO.Directory.Exists(pathDocumentos))
                {
                    var listaArchivosPathDocumentos = new List<string>(Directory.GetFiles(pathDocumentos));
                    var rutaDocumentos = RetornarPathDocumentos(pathDocumentos);

                    // Lista todos los archivos del directorio pathDocumentos
                    foreach (var archivo in listaArchivosPathDocumentos)
                    {
                        listaArchivosTodos.Add(rutaDocumentos + Path.GetFileName(archivo));
                    }

                    // Listo los archivos que son base para la radicación 
                    listaArchivosBase = RetornarListaArchivosBaseRadicacionSigpro(pathDocumentos, numeroSilpa, rutaDocumentos);
                    
                    //Los archivos adicionales, son los que no coinciden con la ruta y nombre del archivo almacenado en el arreglo de archivos base.
                    if (listaArchivosBase.Count > 0)
                    {
                        foreach (var archivo in listaArchivosTodos)
                        {
                            if (!listaArchivosBase.Contains(archivo))
                            {
                                listaArchivosAdicionales.Add(archivo);
                            }
                        }                        
                    }
                    else
                    {
                        listaArchivosAdicionales = listaArchivosTodos;
                    }
                }

                listaArchivosRadicar.Add(listaArchivosTodos);
                listaArchivosRadicar.Add(listaArchivosBase);
                listaArchivosRadicar.Add(listaArchivosAdicionales);

                return listaArchivosRadicar;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la consulta de los archivos para radicar en SIGPRO. " + ex.Message + " - " + ex.StackTrace.ToString(), ex);
            }
        }


        private List<string> RetornarListaArchivosBaseRadicacionSigpro(string pathDocumentos, int numeroSilpa, string rutaDocumentos)
        {
            Parametrizacion.Parametrizacion objParametrizacion = null;
            objParametrizacion = new Parametrizacion.Parametrizacion();
            var listaArchivosBase = new List<string>();
            try
            {
                var rutaPdfPrincipal = pathDocumentos + numeroSilpa.ToString() + ".pdf";

                if (System.IO.File.Exists(rutaPdfPrincipal))
                {
                    var rutaPdfBase = rutaDocumentos + numeroSilpa.ToString() + ".pdf";
                    listaArchivosBase.Add(rutaPdfBase);
                }

                return listaArchivosBase;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private string RetornarPathDocumentos(string pathDocumentos) {

            string pathToCreate = pathDocumentos;
            string[] Ruta = pathToCreate.Split('\\');
            pathToCreate = string.Empty;
            for (int i = 2; i <= Ruta.Length - 1; i++)
            {
                if (Ruta[i].ToString().Length > 0 && !string.IsNullOrEmpty(Ruta[i]))
                {
                    pathToCreate = pathToCreate + Ruta[i].ToString() + '\\';
                }
            }
            return pathToCreate;
        }


        /// <summary>
        /// Obtener la informacion de una solicitud relacionada al numero VITAL indicado
        /// </summary>
        /// <param name="p_strNumeroVITAL">string con el numero VITAL</param>
        /// <param name="p_intAutoridadID">int con el identificador de la autoridad ambiental. Opcional</param>
        /// <returns>DAASolicitudEntity con la informacion de la solicitud</returns>
        public DAASolicitudEntity ObtenerSolicitudNumeroVITAL(string p_strNumeroVITAL, int p_intAutoridadID = -1)
        {
            DAADalc objDAADalc;
            DAASolicitudEntity objDAASolicitudEntity = null;

            try
            {
                //Consultar informacion
                objDAADalc = new DAADalc();
                objDAASolicitudEntity = objDAADalc.ObtenerSolicitudNumeroVITAL(p_strNumeroVITAL, p_intAutoridadID);
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DAA :: ObtenerSolicitudNumeroVITAL -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }

            return objDAASolicitudEntity;

        }


        /// <summary>
        /// Obtener la informacion de una solicitud relacionada al numero VITAL indicado
        /// </summary>
        /// <param name="p_strNumeroVITAL">string con el numero VITAL</param>
        /// <param name="p_intAutoridadID">int con el identificador de la autoridad ambiental. Opcional</param>
        /// <returns>DAASolicitudEntity con la informacion de la solicitud</returns>
        public void ReasignarSolicitud(string p_strNumeroVITAL, int p_intAutoridadReasignarID, int p_intSolicitudReasignacionID = -1, int p_SolicitanteID = -1)
        {
            DAADalc objDAADalc;

            try
            {
                //Consultar informacion
                objDAADalc = new DAADalc();
                objDAADalc.ReasignarSolicitud(p_strNumeroVITAL, p_intAutoridadReasignarID, p_intSolicitudReasignacionID, p_SolicitanteID);
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DAA :: ReasignarSolicitud -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }
        }


        /// <summary>
        /// Obtiene el listado de estados que puede tener una solicitud de reasignacion
        /// </summary>
        /// <returns>List con la información de los estados</returns>
        public List<DAASolicitudEstadoReasignacionEntity> ObtenerEstadosSolicitudReasignacion()
        {
            DAADalc objDAADalc;
            List<DAASolicitudEstadoReasignacionEntity> objLstEstados = null;

            try
            {
                //Consultar informacion
                objDAADalc = new DAADalc();
                objLstEstados = objDAADalc.ObtenerEstadosSolicitudReasignacion();
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DAA :: ObtenerEstadosSolicitudReasignacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }

            return objLstEstados;

        }

        /// <summary>
        /// Crea una solicitud de reasignacion
        /// </summary>
        /// <param name="p_objDAASolicitudReasignacionEntity">DAASolicitudReasignacionEntity con la información de la solicitud de reasignación</param>
        /// <returns>int con el identificador de la solicitud</returns>
        public int InsertarSolicitudReasignacion(DAASolicitudReasignacionEntity p_objDAASolicitudReasignacionEntity)
        {
            DAADalc objDAADalc;
            int intSolicitudReasignacionID;

            try
            {
                //Consultar informacion
                objDAADalc = new DAADalc();
                intSolicitudReasignacionID = objDAADalc.InsertarSolicitudReasignacion(p_objDAASolicitudReasignacionEntity);
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DAA :: InsertarSolicitudReasignacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }

            return intSolicitudReasignacionID;
        }


        /// <summary>
        /// Realiza la actualizacion de la solicitud de reasignacion
        /// </summary>
        /// <param name="p_objDAASolicitudReasignacionEntity">DAASolicitudReasignacionEntity con la informacion que se actualizará</param>
        public void ActualizarSolicitudReasignacion(DAASolicitudReasignacionEntity p_objDAASolicitudReasignacionEntity)
        {
            DAADalc objDAADalc;

            try
            {
                //Consultar informacion
                objDAADalc = new DAADalc();
                objDAADalc.ActualizarSolicitudReasignacion(p_objDAASolicitudReasignacionEntity);
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DAA :: ActualizarSolicitudReasignacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }
        }


        /// <summary>
        /// Obtener solicitud de reasignacion
        /// </summary>
        /// <param name="p_intSolicitudReasignacionID">int con el identificador de solicitud de reasignacion</param>
        /// <returns>DAASolicitudReasignacionEntity con la información de las solicitud de reasignación</returns>
        public DAASolicitudReasignacionEntity ObtenerSolicitudReasignacion(int p_intSolicitudReasignacionID)
        {
            DAADalc objDAADalc;
            DAASolicitudReasignacionEntity objDAASolicitudReasignacionEntity;

            try
            {
                //Consultar informacion
                objDAADalc = new DAADalc();
                objDAASolicitudReasignacionEntity = objDAADalc.ObtenerSolicitudReasignacion(p_intSolicitudReasignacionID);
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DAA :: ObtenerSolicitudReasignacion -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }

            return objDAASolicitudReasignacionEntity;
        }


        /// <summary>
        /// Obtener el listado de solicitudes de reasignacion realizadas por una autoridad.
        /// </summary>
        /// <param name="p_intAutoridadID">int con el identificador de la autoridad que realizo la solicitud</param>
        /// <param name="p_strNumeroVITAL">string con el número VITAL</param>
        /// <param name="p_intAutoridadReasignar">int con el identificador a la cual se reasigno</param>
        /// <param name="p_intEstadoSolicitudID">int con el identificador del estado de la solicitud. Opcional enviar -1</param>
        /// <param name="p_objFechaSolicitudInicial">Fecha inicial desde la cual se realiza la busqueda</param>
        /// <param name="p_objFechaSolicitudFinal">Fecha Final desde la cual se realiza la busqueda</param>
        /// <returns>List con la información de las solicitudes de reasignación</returns>
        public List<DAASolicitudReasignacionEntity> ObtenerSolicitudesReasignacionRealizadasAutoridad(int p_intAutoridadID, string p_strNumeroVITAL, int p_intAutoridadReasignar, int p_intEstadoSolicitudID, DateTime p_objFechaSolicitudInicial, DateTime p_objFechaSolicitudFinal)
        {
            DAADalc objDAADalc;
            List<DAASolicitudReasignacionEntity> objLstSolicitudes;

            try
            {
                //Consultar informacion
                objDAADalc = new DAADalc();
                objLstSolicitudes = objDAADalc.ObtenerSolicitudesReasignacionRealizadasAutoridad(p_intAutoridadID, p_strNumeroVITAL, p_intAutoridadReasignar, p_intEstadoSolicitudID, p_objFechaSolicitudInicial, p_objFechaSolicitudFinal);
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DAA :: ObtenerSolicitudesReasignacionRealizadasAutoridad -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }

            return objLstSolicitudes;
        }


        /// <summary>
        /// Obtener el listado de solicitudes de reasignacion asignadas.
        /// </summary>
        /// <param name="p_intAutoridadID">int con el identificador de la autoridad que recibe solicitud</param>
        /// <param name="p_strNumeroVITAL">string con el número VITAL</param>
        /// <param name="p_intAutoridadSolicitante">int con el identificador de la autoridad que realizo la solicitud</param>
        /// <param name="p_intEstadoSolicitudID">int con el identificador del estado de la solicitud. Opcional enviar -1</param>
        /// <param name="p_objFechaSolicitudInicial">Fecha inicial desde la cual se realiza la busqueda</param>
        /// <param name="p_objFechaSolicitudFinal">Fecha Final desde la cual se realiza la busqueda</param>
        /// <returns>List con la información de las solicitudes de reasignación</returns>
        public List<DAASolicitudReasignacionEntity> ObtenerSolicitudesReasignacionAsignadasAutoridad(int p_intAutoridadID, string p_strNumeroVITAL, int p_intAutoridadSolicitante, int p_intEstadoSolicitudID, DateTime p_objFechaSolicitudInicial, DateTime p_objFechaSolicitudFinal)
        {
            DAADalc objDAADalc;
            List<DAASolicitudReasignacionEntity> objLstSolicitudes;

            try
            {
                //Consultar informacion
                objDAADalc = new DAADalc();
                objLstSolicitudes = objDAADalc.ObtenerSolicitudesReasignacionAsignadasAutoridad(p_intAutoridadID, p_strNumeroVITAL, p_intAutoridadSolicitante, p_intEstadoSolicitudID, p_objFechaSolicitudInicial, p_objFechaSolicitudFinal);
            }
            catch (Exception exc)
            {
                //Escribir error
                SMLog.Escribir(Severidad.Critico, "DAA :: ObtenerSolicitudesReasignacionAsignadasAutoridad -> Error inesperado: " + exc.Message + " " + exc.StackTrace);

                //Escalar error
                throw exc;
            }

            return objLstSolicitudes;
        }

    }
}
