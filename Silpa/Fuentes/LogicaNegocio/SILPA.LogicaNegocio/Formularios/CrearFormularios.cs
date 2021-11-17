using System;
using System.Collections.Generic;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using CrystalDecisions.Shared;
using SoftManagement.Log;
using System.IO;
using System.Configuration;
using SILPA.LogicaNegocio.Generico;
using SILPA.LogicaNegocio.Liquidacion;
using SILPA.Comun;
using SILPA.AccesoDatos.Salvoconducto;
using System.Linq;
using SILPA.AccesoDatos.Tramites.BeneficioTributario.CertificadoUPME;
using SILPA.LogicaNegocio.EvaluacionREA;
using SILPA.LogicaNegocio.Encuestas.Contingencias;

namespace SILPA.LogicaNegocio.Formularios
{
    public class CrearFormularios
    {

        private void GenerarReporte(ReportDocument reportDocument, string pathSalida, string nombre, PaperSize paperSize = PaperSize.PaperLetter)
        {
            ExportOptions exportOptions;
            DiskFileDestinationOptions diskFileDestinationOptions;
            string nombreReporteCompleto = null;
            string nombreArchivoRep = "";
            try
            {
                reportDocument.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
                reportDocument.PrintOptions.PaperSize = paperSize;
                Random rnd = new Random();
                nombreReporteCompleto = nombre;
                nombreArchivoRep = pathSalida + nombre;
                //---------------------------------------------------------------------- 
                // DEFINIR ARCHIVO DESTINO PARA EL REPORTE A GENERARSE 
                //---------------------------------------------------------------------- 
                diskFileDestinationOptions = new DiskFileDestinationOptions();
                diskFileDestinationOptions.DiskFileName = nombreArchivoRep;
                //---------------------------------------------------------------------- 
                // DEFINIR OPCIONES PARA EL ARCHIVO A GENERARSE 
                //---------------------------------------------------------------------- 
                exportOptions = reportDocument.ExportOptions;
                exportOptions.DestinationOptions = diskFileDestinationOptions;
                exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                //Obtiene o establece el tipo de formato de exportación. 
                exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                //---------------------------------------------------------------------- 
                // EXPORTAR EL ARCHIVO 
                //---------------------------------------------------------------------- 
                reportDocument.Export();
            }
            catch (Exception ex)
            {
                //string error = ex.ToString();
                throw new ApplicationException(String.Format("No fue posible generar el reporte {0} en el path {1}", nombreArchivoRep, pathSalida), ex);
            }
        }
        private void GenerarReporteSalvoconducto(ReportDocument reportDocument, string pathSalida, string nombre, PaperSize paperSize = PaperSize.PaperLegal)
        {
            ExportOptions exportOptions;
            DiskFileDestinationOptions diskFileDestinationOptions;
            string nombreReporteCompleto = null;
            string nombreArchivoRep = "";
            try
            {
                reportDocument.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
                reportDocument.PrintOptions.PaperSize = paperSize;
                Random rnd = new Random();
                nombreReporteCompleto = nombre;
                nombreArchivoRep = pathSalida + nombre;
                //---------------------------------------------------------------------- 
                // DEFINIR ARCHIVO DESTINO PARA EL REPORTE A GENERARSE 
                //---------------------------------------------------------------------- 
                diskFileDestinationOptions = new DiskFileDestinationOptions();
                diskFileDestinationOptions.DiskFileName = nombreArchivoRep;
                //---------------------------------------------------------------------- 
                // DEFINIR OPCIONES PARA EL ARCHIVO A GENERARSE 
                //---------------------------------------------------------------------- 
                exportOptions = reportDocument.ExportOptions;
                exportOptions.DestinationOptions = diskFileDestinationOptions;
                exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                //Obtiene o establece el tipo de formato de exportación. 
                exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                //---------------------------------------------------------------------- 
                // EXPORTAR EL ARCHIVO 
                //---------------------------------------------------------------------- 
                reportDocument.Export();
            }
            catch (Exception ex)
            {
                //string error = ex.ToString();
                throw new ApplicationException(String.Format("No fue posible generar el reporte {0} en el path {1}", nombreArchivoRep, pathSalida), ex);
            }
        }


        /// <summary>
        /// Enviar correo informativo de acuerdo al proceso ejecutado
        /// </summary>
        /// <param name="p_strNumeroVital">string con el  número vital</param>
        /// <param name="p_strPathSalida">string con el path donde se ubica el documento</param>
        /// <param name="p_strNombreArchivo">string con el nombre del archivo</param>
        /// <param name="p_objDatosXml">DataSet con la información con la cuals e gener el archivo</param>
        private void EnviarCorreos(string p_strNumeroVital, string p_strPathSalida, string p_strNombreArchivo, DataSet p_objDatosXml)
        {
            Parametrizacion.Parametrizacion objParametrizacion = null;
            ICorreo.Correo objCorreo = null;
            string strCorreos = "";

            switch (p_strNumeroVital.Substring(0, 2))
            {
                case "41":
                    //Obtener correos
                    objParametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();
                    strCorreos = objParametrizacion.ObtenerValorParametroGeneral(-1, "CORREOS_ALERTA_CONTINGENCIA");

                    //Validar si se eocntraron correos
                    if (!string.IsNullOrEmpty(strCorreos))
                    {
                        string strNivelEmergencia = string.Empty;
                        strNivelEmergencia = p_objDatosXml.Tables["FORMULARIO"].Rows[0]["nivelEmergencia_9991"].ToString();
                        //Enviar correo de alerta
                        objCorreo = new ICorreo.Correo();
                        objCorreo.EnviarCorreoAlertaContingencia(strCorreos, p_strNumeroVital, p_strPathSalida + p_strNombreArchivo, strNivelEmergencia);

                        if (p_objDatosXml.Tables["FORMULARIO"].Rows[0]["autoridad_ambiental_9988"].ToString() != string.Empty)
                        {
                            objCorreo.EnviarCorreoReporteContigenciasAutoridadAmbiental(p_objDatosXml.Tables["FORMULARIO"].Rows[0]["autoridad_ambiental_9988"].ToString(), p_strNumeroVital, p_strPathSalida + p_strNombreArchivo, strNivelEmergencia);
                        }
                        if (p_objDatosXml.Tables["FORMULARIO"].Rows[0]["municipio"].ToString() != string.Empty)
                        {
                            objCorreo.EnviarCorreoReporteContigenciasPorJurisdiccionAutoridadAmbiental(p_objDatosXml.Tables["FORMULARIO"].Rows[0]["municipio"].ToString(), p_strNumeroVital, p_strPathSalida + p_strNombreArchivo, strNivelEmergencia);
                            // se realiza el envio de la alerta dependiendo del nivel de alerta
                            //SILPA.LogicaNegocio.DAA.DAA objDaa = new SILPA.LogicaNegocio.DAA.DAA();
                            //string xmlInformacion = objDaa.ConsultarDatosFormulario(p_strNumeroVital, "V");
                            //System.IO.StringReader sr = new System.IO.StringReader(xmlInformacion);
                            //DataSet dsXML = new DataSet();
                            //dsXML.ReadXml(sr);
                            objCorreo.EnviarCorreoAlertaContingenciaPorNiveldeEmergencia(p_objDatosXml.Tables["FORMULARIO"].Rows[0]["municipio"].ToString(), strNivelEmergencia, p_strNumeroVital, p_strPathSalida + p_strNombreArchivo);
                        }
                        /*if (p_objDatosXml.Tables["FORMULARIO"].Rows[0]["Área_marina_p.e_Plataforma_OFF_-_SHORE_boya_de_cargue_buque_etc_4972"].ToString() != string.Empty)
                        {
                            objCorreo.EnviarCorreoReporteContigenciasDIMAR("DIMAR", p_strNumeroVital, p_strPathSalida + p_strNombreArchivo, strNivelEmergencia);
                        }*/
                        
                    }
                    else
                    {
                        throw new Exception("Direcciones de correo no configurados para envío alerta contingencia");
                    }
                    break;
                case "73":
                    //Obtener correos
                    objParametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();
                    strCorreos = objParametrizacion.ObtenerValorParametroGeneral(-1, "CORREOS_ALERTA_CONTINGENCIA");

                    //Validar si se eocntraron correos
                    if (!string.IsNullOrEmpty(strCorreos))
                    {
                        //Enviar correo de alerta
                        objCorreo = new ICorreo.Correo();
                        objCorreo.EnviarCorreoAlertaContingencia(strCorreos, p_strNumeroVital, p_strPathSalida + p_strNombreArchivo, "ASOCIADO AL REPORTE INICIAL");
                    }
                    else
                    {
                        throw new Exception("Direcciones de correo no configurados para envío alerta contingencia 2");
                    }
                    break;
                case "74":
                    //Obtener correos
                    objParametrizacion = new SILPA.LogicaNegocio.Parametrizacion.Parametrizacion();
                    strCorreos = objParametrizacion.ObtenerValorParametroGeneral(-1, "CORREOS_ALERTA_CONTINGENCIA");

                    //Validar si se eocntraron correos
                    if (!string.IsNullOrEmpty(strCorreos))
                    {
                        //Enviar correo de alerta
                        objCorreo = new ICorreo.Correo();
                        objCorreo.EnviarCorreoAlertaContingencia(strCorreos, p_strNumeroVital, p_strPathSalida + p_strNombreArchivo, "ASOCIADO AL REPORTE INICIAL");
                    }
                    else
                    {
                        throw new Exception("Direcciones de correo no configurados para envío alerta contingencia 3");
                    }
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objXML"></param>
        private void ProcesarXMLModificacionLicencia(ref DataSet p_objXML)
        {
            string IDFORMULARIO = string.Empty;

            var TablaPadre = from ResultPadre in p_objXML.Tables["Información_para_la_ubicación_del_proyecto_con_seleccion_de_AA_91"].AsEnumerable()
                             select new
                             {
                                 IdSubFormulario = ResultPadre.Field<string>("IDSUBFORMULARIO")//,
                             };

            if (TablaPadre != null && TablaPadre.Count() > 0)
            {
                foreach (var ResultPadre in TablaPadre)
                {
                    var TablaHija = from ResultHija in p_objXML.Tables["Multiregistro_para_coordenadas_premiso_de_captación_15091"].AsEnumerable()
                                    where ResultHija.Field<string>("IDFORMULARIO") == ResultPadre.IdSubFormulario
                                    select new
                                    {
                                        IdFormulario = ResultHija.Field<string>("IDFORMULARIO")
                                    };

                    if (TablaHija.Count() == 0 && TablaHija != null)
                    {
                        DataRow row = p_objXML.Tables["Multiregistro_para_coordenadas_premiso_de_captación_15091"].NewRow();
                        IDFORMULARIO = ResultPadre.IdSubFormulario;
                        row["IDFORMULARIO"] = IDFORMULARIO;
                        row["IDSUBFORMULARIO"] = "0";
                        row["Localización_3272"] = string.Empty;
                        row["Tipo_de_geometría_3273"] = string.Empty;
                        row["Tipo_de_Coordenada_4848"] = string.Empty;
                        row["Origen_-_Magna_Sirgas_3277"] = string.Empty;
                        row["Norte_3278"] = "0";
                        row["Este_4585"] = "0";
                        p_objXML.Tables["Multiregistro_para_coordenadas_premiso_de_captación_15091"].Rows.Add(row);
                    }
                }
            }
        }

        private void ProcesarXMLEmiAtmFtesFijas(ref DataSet p_objXML)
        {
            string IDFORMULARIO = string.Empty;

            var TablaPadre = from ResultPadre in p_objXML.Tables["Información_para_la_ubicación_del_proyecto_con_seleccion_de_AA_156"].AsEnumerable()
                             select new
                             {
                                 IdSubFormulario = ResultPadre.Field<string>("IDSUBFORMULARIO")//,
                                                                                               // IdSeleccion = ResultPadre.Field<int>("Información_para_la_ubicación_del_proyecto_con_seleccion_de_AA_91_Id")
                             };

            if (TablaPadre != null && TablaPadre.Count() > 0)
            {
                foreach (var ResultPadre in TablaPadre)
                {
                    var TablaHija = from ResultHija in p_objXML.Tables["Multiregistro_para_coordenadas_premiso_de_captación_150156"].AsEnumerable()
                                    where ResultHija.Field<string>("IDFORMULARIO") == ResultPadre.IdSubFormulario
                                    select new
                                    {
                                        IdFormulario = ResultHija.Field<string>("IDFORMULARIO")
                                    };
                    if (TablaHija.Count() == 0 && TablaHija != null)
                    {
                        DataRow row = p_objXML.Tables["Multiregistro_para_coordenadas_premiso_de_captación_150156"].NewRow();
                        IDFORMULARIO = ResultPadre.IdSubFormulario;
                        row["IDFORMULARIO"] = IDFORMULARIO;
                        row["IDSUBFORMULARIO"] = "0";
                        row["Localización_3272"] = string.Empty;
                        row["Tipo_de_geometría_3273"] = string.Empty;
                        row["Tipo_de_Coordenada_4848"] = string.Empty;
                        row["Origen_-_Magna_Sirgas_3277"] = string.Empty;
                        row["Norte_3278"] = "0";
                        row["Este_4585"] = "0";
                        p_objXML.Tables["Multiregistro_para_coordenadas_premiso_de_captación_150156"].Rows.Add(row);
                    }
                }
            }
        }


        private void ProcesarXMLSolicitudDAA(ref DataSet p_objXML)
        {
            string IDFORMULARIO = string.Empty;

            var TablaPadre = from ResultPadre in p_objXML.Tables["Información_para_la_ubicación_del_proyecto_con_seleccion_de_AA_91"].AsEnumerable()
                             select new
                             {
                                 IdSubFormulario = ResultPadre.Field<string>("IDSUBFORMULARIO")//,
                                                                                               // IdSeleccion = ResultPadre.Field<int>("Información_para_la_ubicación_del_proyecto_con_seleccion_de_AA_91_Id")
                             };

            if (TablaPadre != null && TablaPadre.Count() > 0)
            {
                foreach (var ResultPadre in TablaPadre)
                {
                    var TablaHija = from ResultHija in p_objXML.Tables["Multiregistro_para_coordenadas_premiso_de_captación_15091"].AsEnumerable()
                                    where ResultHija.Field<string>("IDFORMULARIO") == ResultPadre.IdSubFormulario
                                    select new
                                    {
                                        IdFormulario = ResultHija.Field<string>("IDFORMULARIO")
                                    };
                    if (TablaHija.Count() == 0 && TablaHija != null)
                    {
                        DataRow row = p_objXML.Tables["Multiregistro_para_coordenadas_premiso_de_captación_15091"].NewRow();
                        IDFORMULARIO = ResultPadre.IdSubFormulario;
                        row["IDFORMULARIO"] = IDFORMULARIO;
                        row["IDSUBFORMULARIO"] = "0";
                        row["Localización_3272"] = string.Empty;
                        row["Tipo_de_geometría_3273"] = string.Empty;
                        row["Tipo_de_Coordenada_4848"] = string.Empty;
                        row["Origen_-_Magna_Sirgas_3277"] = string.Empty;
                        row["Norte_3278"] = "0";
                        row["Este_4585"] = "0";
                        p_objXML.Tables["Multiregistro_para_coordenadas_premiso_de_captación_15091"].Rows.Add(row);
                    }
                }
            }
        }

        public void GenerarFormularioPdf(string pathSalida, string nombreArchivo, string numeroVital)
        {
            ReportDocument reporte = null;
            DataSet DatosXml = new DataSet();
            switch(numeroVital.Substring(0,2))
            {
                case "01":
                    //DatosXml = new OrigenDatos.DSDaaTdrEia();
                    //reporte = new Formularios.FormularioDAATDREIA();                    
                     DatosXml = new OrigenDatos.DSSolicitudDAA();
                    reporte = new Formularios.FormularioSolicitudDAA();        
                    break;
                case "02":
                    DatosXml = new OrigenDatos.DSLicenciaAmbiental2041();
                    reporte = new Formularios.FormularioLicenciaAmbiental2041();
                    break;
                case "06":
                    DatosXml = new OrigenDatos.DSQuejas();
                    reporte = new Formularios.FormularioQuejas();
                    break;
                case "07":
                    DatosXml = new OrigenDatos.DSLiquidacionSolicitud();
                    reporte = new Formularios.FormularioSolicitudLiquidacion();
                    break;
                //case "09":
                //    DatosXml = new OrigenDatos.DSSalvoconductoMovilizacion();
                //    reporte = new Formularios.FormularioSalvoconductoMovilizacion();
                //    break;
                case "11":
                    DatosXml = new OrigenDatos.DSLiquidacionEvaluacion();
                    reporte = new Formularios.FormularioLiquidacionEvaluacion();
                    break;
                case "12":
                    DatosXml = new OrigenDatos.DSCesionDerechosyTramites();
                    reporte = new Formularios.FormularioCesionDerechosTramite();
                    break;
                case "14":
                    DatosXml = new OrigenDatos.DSSolicitudEE();
                    reporte = new Formularios.FormularioSolicitudEE();
                    break;
                case "15":
                    DatosXml = new OrigenDatos.DSPresentarRecurso();
                    reporte = new Formularios.FormularioRecursoRepo();
                    break;
                //case "17":
                //    DatosXml = new OrigenDatos.DSSalvoconductoRenovacion();
                //    reporte = new Formularios.FormularioSalvoconductoRenovacion();
                //    break;
                //case "18":
                //    DatosXml = new OrigenDatos.SalvoconductoRemovilizacion();
                //    reporte = new Formularios.FormularioSalvoconductoRemovilizacion();
                //    break;
                case "23":
                    DatosXml = new OrigenDatos.DSAprobechamientoForestal();
                    reporte = new Formularios.FormularioAprovechamientoForestal();
                    break;
                case "27":
                    DatosXml = new OrigenDatos.DSSancionatorio();
                    reporte = new Formularios.FormularioSancionatorio();
                    break;
                case "29":
                    DatosXml = new OrigenDatos.DSSolicitudCertificacionPresenciaMIJ();
                    reporte = new Formularios.FormularioSolicitudCertificacionPresenciaMIJ();
                    break;
                case "30":
                    DatosXml = new OrigenDatos.DSConsecionAguasSubterraneas();
                    reporte = new Formularios.FormularioConsecionAguasSubterraneas();
                    break;
                case "31":
                    DatosXml = new OrigenDatos.DSConsecionAguasSuperficiales();
                    reporte = new Formularios.FormularioConsecionAguasSuperficiales();
                    break;
                case "32":
                    DatosXml = new OrigenDatos.DSEmisionesAtmosfericas();
                    reporte = new Formularios.FormularioEmisionesAtmosfericas();
                    break;
                case "33":
                    DatosXml = new OrigenDatos.DSExploracionyProspeccion();
                    reporte = new Formularios.FormularioExploracionProspeccion();
                    break;
                case "34":
                    DatosXml = new OrigenDatos.DSPermiVertimientos();
                    reporte = new Formularios.FormularioPermvertimientos();
                    break;
                case "35":
                    DatosXml = new OrigenDatos.DSEnviarInfoAdicional();
                    reporte = new Formularios.FormularioEnviarInfoAdicional();
                    break;
                case "36":
                    DatosXml = new OrigenDatos.DSSolInformcionAdicional();
                    reporte = new Formularios.FormularioSolInfoAdicional();
                    break;
                case "37":
                    DatosXml = new OrigenDatos.DSCobro();
                    reporte = new Formularios.FormularioCobro();
                    break;
                case "38":
                    DatosXml = new OrigenDatos.DSModificacionLAM();
                    reporte = new Formularios.FormularioModificacionLAM();
                    break;
                case "41":
                    //DatosXml = new OrigenDatos.DSReporteContingencias();
                    //reporte = new Formularios.FormularioReporteContingencias();
                    // JACOSTA  20200125 NUEVO PROCESO
                    DatosXml = new OrigenDatos.DSContingenciasInfoInicial();
                    reporte = new Formularios.FormularioContingenciasInfoInicial();
                    break;
                case "42":
                    DatosXml = new OrigenDatos.DSBeneficiosTributarios();
                    reporte = new Formularios.FormularioBeneficiosTributariosIVA();
                    break;
                case "43":
                    DatosXml =  new OrigenDatos.DSSolicitudNotElec1();
                    reporte = new Formularios.FormularioNotElec();
                    break;
                case "44":
                    DatosXml = new OrigenDatos.DSBeneficiosTributariosRenta();
                    reporte = new Formularios.FormularioBeneficiosTributariosRenta();
                    break;
                case "45":
                    DatosXml = new OrigenDatos.DSFuentesNoConvencionalesDeEnergia();
                    reporte = new Formularios.FormularioFuentesNoConvencionalesDeEnergia();
                    break;
                case "46":
                    DatosXml = new OrigenDatos.DSPruebaDinamica();
                    reporte = new Formularios.FormularioPruebaDinamica();
                    break;
                //jmartinez 24-05-2018 nuevo reporte
                //FORMULARIO UNICO DE SOLICITUD DE SUSTRACCIÓN DE RESERVAS FORESTALES NACIONALES
                case "48":
                    DatosXml = new OrigenDatos.DSSolicitudSustraccionReservasForestalesNal();
                    reporte = new Formularios.FormularioSolicitudSustraccionReservasForestalesNal();
                    break;

                case "49":
                    DatosXml = new OrigenDatos.DSOcupacionCauce();
                    reporte = new Formularios.FormularioOcupacionCauce();
                    break;
                case "56":
                    DatosXml = new OrigenDatos.DSREA();
                    reporte = new Formularios.FormularioREA();
                    break;
                case "57":
                    DatosXml = new OrigenDatos.DSPermisoIndividualRecoleccion();
                    reporte = new Formularios.FormularioPermisoIndividualRecoleccion();
                    break;
                case "58":
                    DatosXml = new OrigenDatos.DSSistemasRecoleccionSelectiva();
                    reporte = new Formularios.FormularioSistemasRecoleccionSelectiva();
                    break;
                case "59":
                    DatosXml = new OrigenDatos.DSPlanesDevolucionPosconsumo();
                    reporte = new Formularios.FormularioPlanesDevolucionPosconsumo();
                    break;
                //jmartinez Creo la funcionalidad para generar el reporte
                //reporte SOLICITUD DE PERMISO DE TOMA DE FOTOGRAFÍAS
                case "62": //SE CAMBIA A 62 DEBIDO QUE LA ASIGANCION DE CODIGO DE PRODUCCION ASIGNO ESTE IDENTIFICADOR
                    DatosXml = new OrigenDatos.DSSolicitudTomaFotografias();
                    reporte = new Formularios.FormularioSolicitudDePermisoTomaFotografias();
                    break;
                //jmartinez Creo la funcionalidad para generar el reporte
                //reporte REGISTRO DE RESERVA NATURAL
                case "63":
                    DatosXml = new OrigenDatos.DSRegistroDeReservaNatural();
                    reporte = new Formularios.FormularioRegistroDeReservaNatural();
                    break;
                case "64":
                    DatosXml = new OrigenDatos.DSPermisoMarcoRecoleccion();
                    reporte = new Formularios.FormularioPermisoMarcoRecoleccion();
                    break;
                case "65":
                    DatosXml = new OrigenDatos.DSModificacionPMA();
                    reporte = new Formularios.FormularioModificacionPMA();
                    break;
                case "67":
                    DatosXml = new OrigenDatos.DSRecursosGeneticos_ProductosDerivados();
                    reporte = new Formularios.FormularioAccesoRecursosGeneticos();
                    break;
                //jmartinez Creo la funcionalidad para generar el reporte
                //reporte TRAMITE DE ESTRUCTURAS DE COMUNICACION
                case "69":
                    DatosXml = new OrigenDatos.DSTramiteDeEstructurasDeComunicacion();
                    reporte = new Formularios.FormularioTramiteEstructurasdeComunicacion();
                    break;
                case "70":
                    DatosXml = new OrigenDatos.DSBolsas();
                    reporte = new Formularios.FormularioBolsas();
                    break;
                case "73":
                    DatosXml = new OrigenDatos.DSReporteContingenciaParcial();
                    reporte = new Formularios.FormularioContingenciasParcial();
                    break;
                case "74":
                    DatosXml = new OrigenDatos.DSReporteContingenciasFinal();
                    reporte = new Formularios.FormularioReporteContingenciasFinal();
                    break;
                case "75":
                    DatosXml = new OrigenDatos.DSLiquidacionSolicitud();
                    reporte = new Formularios.FormularioSolicitudLiquidacion();
                    break;
                case "76":
                    DatosXml = new OrigenDatos.DSLiquidacionSolicitud();
                    reporte = new Formularios.FormularioSolicitudLiquidacion();
                    break;

                //jmartinez Creo la funcionalidad para generar el reporte
                //reporte Tramite Obras Preexistentes PNNCRSB
                case "78":
                    DatosXml = new OrigenDatos.DSTramiteObrasPreexistentesPNNCRSB();
                    reporte = new Formularios.FormularioSolicitudDePermisoObrasPreexistentesPNNCRSB();
                    break;

                //jmartinez Creo la funcionalidad para generar el reporte
                //Permiso Consulta previa grupos etnicos certificados en pruebas es el 92 en produccion es el 79
                case "79":
                    DatosXml = new OrigenDatos.DSSolicInicioConsulPrevGrupEtnicosCertif();
                    reporte = new Formularios.FormularioSolicitudInicioConsulPrevGrupEtnicosCertif();
                    break;

                ///SOLICITUD PERMISO DE PROVEEDOR DE ELEMENTOS DE MARCAJE DEL SISTEMA NACIONAL DE IDENTIFICACIÓN Y REGISTRO PARA ESPECÍMENES DE LA FAUNA SILVESTRE EN CONDICIONES "EX SITU"
                case "83":
                    DatosXml = new OrigenDatos.DSSolPerElemEspecFaunaSilvEXSITU();
                    reporte = new Formularios.FormularioSolPerElemEspecFaunaSilvEXSITU();
                    break;
                ///SOLICITUD AUTORIZACIÓN PARA EXPORTACIÓN Y/O IMPORTACIÓN DE ESPECÍMENES DE LA DIVERSIDAD BIOLÓGICA NO LISTADO EN LOS APÉNDICES DE LA CONVENCIÓN CITES
                case "85":
                    DatosXml = new OrigenDatos.DSSolAutImpExpEspecieCITES();
                    reporte = new Formularios.FormularioSolAutImpExpEspecieCITES();
                    break;
                ///SOLICITUD DE AUTORIZACIÓN PARA EL MOVIMIENTO TRANSFRONTERIZO DE RESIDUOS PELIGROSOS Y SU ELIMINACIÓN (CONVENIO DE BASILEA)
                case "86":
                    DatosXml = new OrigenDatos.DSSolAutMovTransfResiduoConvBASILEA();
                    reporte = new Formularios.FormularioSolAutMovTransfResiduoConvBASILEA();
                    break;

                case "88":
                    DatosXml = new OrigenDatos.DSGestionEnvasesEmpaques();
                    reporte = new Formularios.FormularioGestionEnvasesEmpaques();
                    break;

                case "91":
                    DatosXml = new OrigenDatos.DSArqueologiaPreventiva();
                    reporte = new Formularios.FormularioArqueologiaPreventiva();
                    break;

                case "92":
                    DatosXml = new OrigenDatos.DSArqueologiaPreventivaCambioMayor();
                    reporte = new Formularios.FormularioArqueologiaPreventivaCambioMayor();
                    break;

                case "93":
                    DatosXml = new OrigenDatos.DSArqueologiaPreventivaCambioMenor();
                    reporte = new Formularios.FormularioArqueologiaPreventivaCambioMenor();
                    break;

                case "97":
                    DatosXml = new OrigenDatos.DSArqueologiaPreventivaInformeFinal();
                    reporte = new Formularios.FormularioArqueologiaPreventivaInformeFinal();
                    break;

                case "98":
                    DatosXml = new OrigenDatos.DSArqueologiaPreventivaPlanManejo();
                    reporte = new Formularios.FormularioArqueologiaPreventivaPlanManejo();
                    break;

                case "99":
                    DatosXml = new OrigenDatos.DSArqueologiaPreventivaResSubsanacion();
                    reporte = new Formularios.FormularioArqueologiaPreventivaResSubsanacion();
                    break;

                default:
                    SMLog.Escribir(Severidad.Informativo, "No existe Logica para crear el pdf de " + numeroVital);
                    return;                
            }
            try
            {         
                SILPA.LogicaNegocio.DAA.DAA objDaa = new SILPA.LogicaNegocio.DAA.DAA();
                string  xmlInformacion = objDaa.ConsultarDatosFormulario(numeroVital,"D");
                System.IO.StringReader sr = new System.IO.StringReader(xmlInformacion);
                DataSet dsXML = new DataSet();
                dsXML.ReadXml(sr);
                foreach (DataTable tabla in dsXML.Tables)
                {
                    foreach (DataColumn column in tabla.Columns)
                    {
                        column.ColumnName = nombreColumna(column.ColumnName).Replace("\t", "_x0009_");
                        if (numeroVital.Substring(0, 2) == "35")
                            column.ColumnName = nombreColumna(column.ColumnName).Replace("%", "_x0025_");
                    }
                }
                foreach (DataTable dt in DatosXml.Tables)
                {
                    if (dt.TableName == "Imagenes")
                        DatosXml.Merge(CargarTablaImagenes(numeroVital, DatosXml.Tables["Imagenes"]));
                    else
                        if (dsXML.Tables[dt.TableName] != null)
                        {
                            
                                DatosXml.Merge(dsXML.Tables[dt.TableName]);
                            
                        }
                    
                }
                #region jmartinez creo procedimiento para crear los registros en blanco para que aparezcan los registros en el subreporte
                if(numeroVital.Substring(0, 2) == "38")
                {
                        this.ProcesarXMLModificacionLicencia(ref DatosXml);
                }

                if (numeroVital.Substring(0, 2) == "32")
                {
                    this.ProcesarXMLEmiAtmFtesFijas(ref DatosXml);
                }

                if (numeroVital.Substring(0, 2) == "01")
                {
                    this.ProcesarXMLSolicitudDAA(ref DatosXml);
                }
                #endregion
                




                //JNEVA Si es autoliquidación cargar datos complementarios al DataSet desde tablas de solicitud
                if (numeroVital.Substring(0, 2) == ConfigurationManager.AppSettings["LiquidacionProcesoLicencia"].ToString() ||
                    numeroVital.Substring(0, 2) == ConfigurationManager.AppSettings["LiquidacionProcesoPermiso"].ToString() ||
                    numeroVital.Substring(0, 2) == ConfigurationManager.AppSettings["LiquidacionProcesoInstrumentos"].ToString())
                {
                    this.CargarDatosSolicitudAutoliquidacion(ref DatosXml);
                }
                else if (numeroVital.Substring(0, 2) == ConfigurationManager.AppSettings["REAProceso"].ToString())
                {
                    this.CargarDatosPDFREA(ref DatosXml);
                }
                else if (numeroVital.Substring(0, 2) == ConfigurationManager.AppSettings["ContingenciasProceso"].ToString())
                {
                    this.CargarDatosPDFContingenciasInfoPrincipal(ref DatosXml);
                }
                else if (numeroVital.Substring(0, 2) == ConfigurationManager.AppSettings["ContingenciasParcialProceso"].ToString())
                {
                    this.CargarDatosPDFContingenciasParcial(ref DatosXml);
                }

                reporte.SetDataSource(DatosXml);
                GenerarReporte(reporte, pathSalida, nombreArchivo);

                //Enviar correo electronico según proceso
                this.EnviarCorreos(numeroVital, pathSalida, nombreArchivo, DatosXml);
            }
            catch (Exception ex)
            { 
                SMLog.Escribir(Severidad.Critico,"GenerarFormularioPdf -- "+ numeroVital+" ---Error:  "+ex.ToString());
            }
            finally
            {
                if (reporte != null)
                    reporte.Close();
            }
        }

        private void CargarDatosPDFContingenciasParcial(ref DataSet DatosXml)
        {
            GeneracionPDFContingencias objGeneracionPDF = null;

            try
            {
                //Cargar datos XML PDF
                objGeneracionPDF = new GeneracionPDFContingencias();
                objGeneracionPDF.GenerarDocumentoPDFInfoParcial(ref DatosXml, DatosXml.Tables["FORMULARIO"].Rows[0]["Número_Vital_Solicitud._9927"].ToString());
            }
            catch (Exception exc)
            {
                //EScribir excepción y realizar escalamiento
                SMLog.Escribir(Severidad.Critico, "GenerarFormularioPdf -- CargarDatosPDFCambiosMenores. Error: " + exc.Message + " - " + exc.StackTrace.ToString());
                throw exc;
            }
        }
        /// <summary>
        /// Genera el documento pdf de la solicitud de informacion principal de contingencias
        /// </summary>
        /// <param name="DatosXml"></param>
        private void CargarDatosPDFContingenciasInfoPrincipal(ref DataSet objDatosXml)
        {
            GeneracionPDFContingencias objGeneracionPDF = null;

            try
            {
                //Cargar datos XML PDF
                objGeneracionPDF = new GeneracionPDFContingencias();
                objGeneracionPDF.GenerarDocumentoPDFInfoInicial(ref objDatosXml, Convert.ToInt32(objDatosXml.Tables["FORMULARIO"].Rows[0][2]));
            }
            catch (Exception exc)
            {
                //EScribir excepción y realizar escalamiento
                SMLog.Escribir(Severidad.Critico, "GenerarFormularioPdf -- CargarDatosPDFCambiosMenores. Error: " + exc.Message + " - " + exc.StackTrace.ToString());
                throw exc;
            }
        }

        /// <summary>
        /// Complementar la información del DataSet de generación de documento xml con la información de solicitud de liquidacion
        /// </summary>
        /// <param name="objDatosXml">DataSet que contiene datos para generación de documento</param>
        private void CargarDatosSolicitudAutoliquidacion(ref DataSet objDatosXml)
        {
            Autoliquidacion objAutoliquidacion = null;
            DataSet objDatosSolicitud = null;
            int p_intSolicitudLiquidacionID = 0;

            try
            {
                //Cargar el identificador de la solicitud
                p_intSolicitudLiquidacionID = Convert.ToInt32(objDatosXml.Tables["FORMULARIO"].Rows[0][1]);

                //Consultar informacion de la solicitud
                objAutoliquidacion = new Autoliquidacion();
                objDatosSolicitud = objAutoliquidacion.ConsultarSolicitudLiquidacionDocumentoRadicacion(p_intSolicitudLiquidacionID);

                //Realizar merge de datos
                objDatosXml.Tables["Solicitud"].Merge(objDatosSolicitud.Tables["Solicitud"]);
                objDatosXml.Tables["Permisos"].Merge(objDatosSolicitud.Tables["Permisos"]);
                objDatosXml.Tables["Regiones"].Merge(objDatosSolicitud.Tables["Regiones"]);
                objDatosXml.Tables["Ubicaciones"].Merge(objDatosSolicitud.Tables["Ubicaciones"]);
                objDatosXml.Tables["Coordenadas"].Merge(objDatosSolicitud.Tables["Coordenadas"]);
                objDatosXml.Tables["Rutas"].Merge(objDatosSolicitud.Tables["Rutas"]);

                //Formatear valor del proyecto y modificación
                if (objDatosXml.Tables["Solicitud"].Rows[0]["VALOR_PROYECTO"] != System.DBNull.Value)
                {
                    objDatosXml.Tables["Solicitud"].Rows[0]["VALOR_PROYECTO"] = string.Format("{0:C}", Convert.ToDecimal(objDatosXml.Tables["Solicitud"].Rows[0]["VALOR_PROYECTO_NUMEROS"]));
                }
                if (objDatosXml.Tables["Solicitud"].Rows[0]["VALOR_MODIFICACION"] != System.DBNull.Value)
                {
                    objDatosXml.Tables["Solicitud"].Rows[0]["VALOR_MODIFICACION"] = string.Format("{0:C}", Convert.ToDecimal(objDatosXml.Tables["Solicitud"].Rows[0]["VALOR_MODIFICACION_NUMEROS"]));
                }

                //Cargar datos adicionales
                objDatosXml.Tables["Solicitud"].Rows[0]["NUMERO_PERMISOS"] = objDatosXml.Tables["Permisos"].Rows.Count;
                objDatosXml.Tables["Solicitud"].Rows[0]["NUMERO_REGIONES"] = objDatosXml.Tables["Regiones"].Rows.Count;
                objDatosXml.Tables["Solicitud"].Rows[0]["NUMERO_UBICACIONES"] = objDatosXml.Tables["Ubicaciones"].Rows.Count;
                objDatosXml.Tables["Solicitud"].Rows[0]["NUMERO_COORDENADAS"] = objDatosXml.Tables["Coordenadas"].Rows.Count;
                objDatosXml.Tables["Solicitud"].Rows[0]["NUMERO_LOCALIZACIONES"] = objDatosXml.Tables["Rutas"].Rows.Count;
                objDatosXml.Tables["Solicitud"].Rows[0]["NUMERO_CAMPOS_COMPLEMENTARIOS"] = 0;

                //Cargar el lsitado de regiones
                if (objDatosXml.Tables["Regiones"].Rows.Count > 0)
                {
                    foreach(DataRow objRegion in objDatosXml.Tables["Regiones"].Rows)
                    {
                        if (!string.IsNullOrEmpty(objDatosXml.Tables["Solicitud"].Rows[0]["REGIONES"].ToString()))
                            objDatosXml.Tables["Solicitud"].Rows[0]["REGIONES"] = objDatosXml.Tables["Solicitud"].Rows[0]["REGIONES"].ToString() + " / " + objRegion["REGION"].ToString();
                        else
                            objDatosXml.Tables["Solicitud"].Rows[0]["REGIONES"] = objRegion["REGION"].ToString();
                    }
                }
                
            }
            catch(Exception exc)
            {
                //EScribir excepción y realizar escalamiento
                SMLog.Escribir(Severidad.Critico, "GenerarFormularioPdf -- CargarDatosSolicitudAutoliquidacion. Error: " + exc.Message + " - " + exc.StackTrace.ToString());
                throw exc;
            }
        }



        /// <summary>
        /// Complementar la información del DataSet de generación de documento xml con la información de solicitud de REA
        /// </summary>
        /// <param name="objDatosXml">DataSet que contiene datos para generación de documento</param>
        private void CargarDatosPDFREA(ref DataSet objDatosXml)
        {
            PDFSolicitudREA objSolicitudREA = null;
            DataSet objInformacionPDF = null;
            int intRegistroID = 0;

            try
            {
                //Cargar el identificador de la solicitud
                intRegistroID = Convert.ToInt32(objDatosXml.Tables["FORMULARIO"].Rows[0][2]);

                //Consultar informacion de la solicitud
                objSolicitudREA = new PDFSolicitudREA();
                objInformacionPDF = objSolicitudREA.ConsultarRegistroPDF(intRegistroID);

                if (objInformacionPDF != null)
                {
                    //Si tabla se encuentra vacia adicionar una
                    if (objInformacionPDF.Tables["REPRESENTANTE"].Rows.Count == 0)
                    {
                        objInformacionPDF.Tables["REPRESENTANTE"].Rows.Add(objInformacionPDF.Tables["REPRESENTANTE"].NewRow());
                        objInformacionPDF.Tables["REPRESENTANTE"].Rows[0]["SOLICITANTE_ID"] = objInformacionPDF.Tables["SOLICITANTE"].Rows[0]["SOLICITANTE_ID"];
                    }
                    if (objInformacionPDF.Tables["APODERADO"].Rows.Count == 0)
                    {
                        objInformacionPDF.Tables["APODERADO"].Rows.Add(objInformacionPDF.Tables["APODERADO"].NewRow());
                        objInformacionPDF.Tables["APODERADO"].Rows[0]["SOLICITANTE_ID"] = objInformacionPDF.Tables["SOLICITANTE"].Rows[0]["SOLICITANTE_ID"];
                    }

                    //Realizar cargue de los datos
                    objDatosXml.Merge(objInformacionPDF.Tables["SOLICITUD"]);
                    objDatosXml.Merge(objInformacionPDF.Tables["INSUMO_PRESERVACION"]);
                    objDatosXml.Merge(objInformacionPDF.Tables["INSUMO_RECOLECCION"]);
                    objDatosXml.Merge(objInformacionPDF.Tables["INSUMO_PROFESIONALES"]);
                    objDatosXml.Merge(objInformacionPDF.Tables["INSUMO_COBERTURA"]);
                    objDatosXml.Merge(objInformacionPDF.Tables["SOLICITANTE"]);
                    objDatosXml.Merge(objInformacionPDF.Tables["REPRESENTANTE"]);
                    objDatosXml.Merge(objInformacionPDF.Tables["APODERADO"]);
                }
                else
                {
                    throw new Exception("No se obtuvo informacion de la solicitud");
                }
            }
            catch (Exception exc)
            {
                //EScribir excepción y realizar escalamiento
                SMLog.Escribir(Severidad.Critico, "GenerarFormularioPdf -- CargarDatosPDFREA. Error: " + exc.Message + " - " + exc.StackTrace.ToString());
                throw exc;
            }
        }



        public string nombreColumna(string columna)
        {
            string nombreColumna = string.Empty;
            string[] numeracion = columna.Split('.');
            int numeral = 0, subnumeral = 0;
            if (int.TryParse(numeracion[0], out numeral) && int.TryParse(numeracion[1], out subnumeral))
            {
                nombreColumna = columna.Replace(string.Format("{0}.{1}.", numeral, subnumeral), string.Format("_x003{0}_.{1}.", numeral, subnumeral));
            }
            else if (int.TryParse(numeracion[0], out numeral) && subnumeral == 0)
            {
                nombreColumna = columna.Replace(string.Format("{0}.", numeral), string.Format("_x003{0}_.", numeral));
            }
            else
            {
                nombreColumna = columna;
            }
            nombreColumna = nombreColumna.Replace("–", "_x2013_");
            return nombreColumna;
        }

        public string GenerarSalvoconductoPDF(int pSalvoconductoID)
        {
            string nombreArchivo = string.Empty;
            ReportDocument reporte = null;
            DataSet DatosXml = new DataSet();
            SalvoconductoNewDalc vSalvoconductoNewDalc = new SalvoconductoNewDalc();
            var objSalvoconducto = vSalvoconductoNewDalc.ConsultaSalvoconductoXSalvoconductoID(pSalvoconductoID);
            nombreArchivo = objSalvoconducto.Numero + ".pdf";
            reporte = new Formularios.PDFSalvoconducto();
            DatosXml = new OrigenDatos.DSSalvoconducto();
            DataRow dtrSalvoconducto = DatosXml.Tables["SALVOCONDUCTO"].NewRow();
            dtrSalvoconducto["SalvoconductoID"] = objSalvoconducto.SalvoconductoID;
            dtrSalvoconducto["TipoSalvoconducto"] = objSalvoconducto.TipoSalvoconducto;
            dtrSalvoconducto["TipoSalvoconductoID"] = objSalvoconducto.TipoSalvoconductoID.Value;
            dtrSalvoconducto["ClaseRecurso"] = objSalvoconducto.ClaseRecurso;
            dtrSalvoconducto["Vigencia"] = objSalvoconducto.Vigencia.ToString();
            dtrSalvoconducto["Numero"] = objSalvoconducto.Numero;
            dtrSalvoconducto["CodigoSeguridad"] = objSalvoconducto.CodigoSeguridad;
            dtrSalvoconducto["NumeroVitalTramite"] = objSalvoconducto.NumeroVitalTramite;
            if (objSalvoconducto.AprovechamientoID != null)
                dtrSalvoconducto["AprovechamientoID"] = objSalvoconducto.AprovechamientoID.Value.ToString();
            else
                dtrSalvoconducto["AprovechamientoID"] = "-1";
            dtrSalvoconducto["DepartamentoProcedencia"] = objSalvoconducto.DepartamentoProcedencia;
            dtrSalvoconducto["MunicipioProcedencia"] = objSalvoconducto.MunicipioProcedencia;
            dtrSalvoconducto["CorregimientoProcedencia"] = objSalvoconducto.CorregimientoProcedencia;
            dtrSalvoconducto["VeredaProcedencia"] = objSalvoconducto.VeredaProcedencia;
            dtrSalvoconducto["Estado"] = objSalvoconducto.Estado;
            dtrSalvoconducto["FechaExpedicion"] = objSalvoconducto.FechaExpedicion.ToShortDateString();
            dtrSalvoconducto["FechaInicioVigencia"] = objSalvoconducto.FechaInicioVigencia.Value.ToShortDateString();
            dtrSalvoconducto["FechaFinalVigencia"] = objSalvoconducto.FechaFinalVigencia.Value.ToShortDateString();
            dtrSalvoconducto["FormatOtorgamiento"] = objSalvoconducto.FormatOtorgamiento;
            dtrSalvoconducto["FechaSolicitud"] = objSalvoconducto.FechaSolicitud.ToShortDateString();
            dtrSalvoconducto["Finalidad"] = objSalvoconducto.Finalidad;
            dtrSalvoconducto["SolicitanteID"] = objSalvoconducto.SolicitanteID.ToString();
            dtrSalvoconducto["AutoridadEmisora"] = objSalvoconducto.AutoridadEmisora;
            dtrSalvoconducto["imgAutoridadEmisora"] = ObtenerImagenbyteAutoridadAmbiental(objSalvoconducto.NombreImagenAutoridad);
            DatosXml.Tables["SALVOCONDUCTO"].Rows.Add(dtrSalvoconducto);
            if (objSalvoconducto.Aprovechamiento != null)
            {
                DataRow dtrAprovechamiento = DatosXml.Tables["APROVECHAMIENTO"].NewRow();
                dtrAprovechamiento["AprovechamientoID"] = objSalvoconducto.Aprovechamiento.AprovechamientoID.ToString();
                dtrAprovechamiento["TipoAprovechamiento"] = objSalvoconducto.Aprovechamiento.TipoAprovechamiento;
                dtrAprovechamiento["TipoAprovechamientoID"] = objSalvoconducto.Aprovechamiento.TipoAprovechamientoID;
                dtrAprovechamiento["Numero"] = objSalvoconducto.Aprovechamiento.Numero;
                dtrAprovechamiento["FechaExpedicion"] = objSalvoconducto.Aprovechamiento.FechaExpedicion.Value.ToShortDateString();
                if (objSalvoconducto.Aprovechamiento.FechaDesde != null)
                    dtrAprovechamiento["FechaDesde"] = objSalvoconducto.Aprovechamiento.FechaDesde.Value.ToShortDateString();
                if (objSalvoconducto.Aprovechamiento.FechaHasta != null)
                    dtrAprovechamiento["FechaHasta"] = objSalvoconducto.Aprovechamiento.FechaHasta.Value.ToShortDateString();
                dtrAprovechamiento["ClaseRecurso"] = objSalvoconducto.Aprovechamiento.ClaseRecurso;
                dtrAprovechamiento["ModoAdquisicionRecurso"] = objSalvoconducto.Aprovechamiento.ModoAdquisicionRecurso;
                dtrAprovechamiento["DepartamentoProcedencia"] = objSalvoconducto.Aprovechamiento.DepartamentoProcedencia;
                dtrAprovechamiento["MunicipioProcedencia"] = objSalvoconducto.Aprovechamiento.MunicipioProcedencia;
                dtrAprovechamiento["CorregimientoProcedencia"] = objSalvoconducto.Aprovechamiento.CorregimientoProcedencia;
                dtrAprovechamiento["VeredaProcedencia"] = objSalvoconducto.Aprovechamiento.VeredaProcedencia;
                dtrAprovechamiento["AutoridadEmisora"] = objSalvoconducto.Aprovechamiento.AutoridadEmisora;
                dtrAprovechamiento["AutoridadOtorga"] = objSalvoconducto.Aprovechamiento.AutoridadOtorga;
                dtrAprovechamiento["FormaOtorgamiento"] = objSalvoconducto.Aprovechamiento.FormaOtorgamiento;
                dtrAprovechamiento["NumeroDocOtorga"] = objSalvoconducto.Aprovechamiento.NumeroDocOtorga;
                if (objSalvoconducto.Aprovechamiento.FechaDocOtorga != null)
                    dtrAprovechamiento["FechaDocOtorga"] = objSalvoconducto.Aprovechamiento.FechaDocOtorga.Value.ToShortDateString();
                dtrAprovechamiento["PaisProcedencia"] = objSalvoconducto.Aprovechamiento.PaisProcedencia;
                dtrAprovechamiento["SolicitanteID"] = objSalvoconducto.Aprovechamiento.SolicitanteID.Value.ToString();
                DatosXml.Tables["APROVECHAMIENTO"].Rows.Add(dtrAprovechamiento);
            }
            else
            {
                DataRow dtrAprovechamiento = DatosXml.Tables["APROVECHAMIENTO"].NewRow();
                dtrAprovechamiento["AprovechamientoID"] = "-1";
                dtrAprovechamiento["SolicitanteID"] = "-1";
                DatosXml.Tables["APROVECHAMIENTO"].Rows.Add(dtrAprovechamiento);
            }

            if (objSalvoconducto.Aprovechamiento != null)
            {
                DataRow dtrTitularAprovechamiento = DatosXml.Tables["TITULAR_APROVECHAMIENTO"].NewRow();
                dtrTitularAprovechamiento["AprovechamientoID"] = objSalvoconducto.Aprovechamiento.AprovechamientoID.ToString();
                if (objSalvoconducto.Aprovechamiento.TipoAprovechamientoID == 1)
                {
                    dtrTitularAprovechamiento["PrimerNombre"] = objSalvoconducto.Aprovechamiento.Solicitante.PrimerNombre;
                    dtrTitularAprovechamiento["SegundoNombre"] = objSalvoconducto.Aprovechamiento.Solicitante.SegundoNombre;
                    dtrTitularAprovechamiento["PrimerApellido"] = objSalvoconducto.Aprovechamiento.Solicitante.PrimerApellido;
                    dtrTitularAprovechamiento["SegundoApellido"] = objSalvoconducto.Aprovechamiento.Solicitante.SegundoApellido;
                    dtrTitularAprovechamiento["CorreoElectronico"] = objSalvoconducto.Aprovechamiento.Solicitante.CorreoElectronico;
                    dtrTitularAprovechamiento["Telefono"] = objSalvoconducto.Aprovechamiento.Solicitante.Telefono;
                    dtrTitularAprovechamiento["NumeroIdentificacion"] = objSalvoconducto.Aprovechamiento.Solicitante.NumeroIdentificacion;
                    dtrTitularAprovechamiento["TipoDocumentoIdentificacion"] = objSalvoconducto.Aprovechamiento.Solicitante.TipoDocumentoIdentificacion.Nombre;
                    dtrTitularAprovechamiento["TipoPersona"] = objSalvoconducto.Aprovechamiento.Solicitante.TipoPersona.NombreTipoPersona;
                    if (!string.IsNullOrEmpty(objSalvoconducto.Aprovechamiento.Solicitante.DireccionPersona.DireccionPersona))
                        dtrTitularAprovechamiento["Direccion"] = objSalvoconducto.Aprovechamiento.Solicitante.DireccionPersona.DireccionPersona.ToString();
                    else
                        dtrTitularAprovechamiento["Direccion"] = "Direccion no registrada";
                    if (!string.IsNullOrEmpty(objSalvoconducto.Aprovechamiento.Solicitante.DireccionPersona.NombreMunicipio))
                        dtrTitularAprovechamiento["DireccionMunicipio"] = objSalvoconducto.Aprovechamiento.Solicitante.DireccionPersona.NombreMunicipio.ToString();
                    else
                        dtrTitularAprovechamiento["DireccionMunicipio"] = "Direccion no registrada";
                    dtrTitularAprovechamiento["RazonSocial"] = objSalvoconducto.Aprovechamiento.Solicitante.RazonSocial;
                    dtrTitularAprovechamiento["SolicitanteID"] = objSalvoconducto.Aprovechamiento.Solicitante.IdApplicationUser.ToString();
                }
                else
                {
                    if (objSalvoconducto.Aprovechamiento.SolicitanteOtorga != null)
                    {
                        dtrTitularAprovechamiento["PrimerNombre"] = objSalvoconducto.Aprovechamiento.SolicitanteOtorga.PrimerNombre;
                        dtrTitularAprovechamiento["SegundoNombre"] = objSalvoconducto.Aprovechamiento.SolicitanteOtorga.SegundoNombre;
                        dtrTitularAprovechamiento["PrimerApellido"] = objSalvoconducto.Aprovechamiento.SolicitanteOtorga.PrimerApellido;
                        dtrTitularAprovechamiento["SegundoApellido"] = objSalvoconducto.Aprovechamiento.SolicitanteOtorga.SegundoApellido;
                        dtrTitularAprovechamiento["CorreoElectronico"] = objSalvoconducto.Aprovechamiento.SolicitanteOtorga.CorreoElectronico;
                        dtrTitularAprovechamiento["Telefono"] = objSalvoconducto.Aprovechamiento.SolicitanteOtorga.Telefono;
                        dtrTitularAprovechamiento["NumeroIdentificacion"] = objSalvoconducto.Aprovechamiento.SolicitanteOtorga.NumeroIdentificacion;
                        dtrTitularAprovechamiento["TipoDocumentoIdentificacion"] = objSalvoconducto.Aprovechamiento.SolicitanteOtorga.TipoDocumentoIdentificacion;
                        dtrTitularAprovechamiento["TipoPersona"] = objSalvoconducto.Aprovechamiento.SolicitanteOtorga.TipoPersona.NombreTipoPersona;
                        if (!string.IsNullOrEmpty(objSalvoconducto.Aprovechamiento.SolicitanteOtorga.DireccionPersona.DireccionPersona))
                            dtrTitularAprovechamiento["Direccion"] = objSalvoconducto.Aprovechamiento.SolicitanteOtorga.DireccionPersona.DireccionPersona.ToString();
                        else
                            dtrTitularAprovechamiento["Direccion"] = "Direccion no registrada";
                        if (!string.IsNullOrEmpty(objSalvoconducto.Aprovechamiento.SolicitanteOtorga.DireccionPersona.NombreMunicipio))
                            dtrTitularAprovechamiento["DireccionMunicipio"] = objSalvoconducto.Aprovechamiento.SolicitanteOtorga.DireccionPersona.NombreMunicipio;
                        else
                            dtrTitularAprovechamiento["DireccionMunicipio"] = "Direccion no registrada";
                        dtrTitularAprovechamiento["RazonSocial"] = objSalvoconducto.Aprovechamiento.SolicitanteOtorga.RazonSocial;
                        dtrTitularAprovechamiento["SolicitanteID"] = objSalvoconducto.Aprovechamiento.SolicitanteOtorga.IdApplicationUser.ToString();
                    }
                    else if (objSalvoconducto.Aprovechamiento.Solicitante != null)
                    {
                        dtrTitularAprovechamiento["PrimerNombre"] = objSalvoconducto.Aprovechamiento.Solicitante.PrimerNombre;
                        dtrTitularAprovechamiento["SegundoNombre"] = objSalvoconducto.Aprovechamiento.Solicitante.SegundoNombre;
                        dtrTitularAprovechamiento["PrimerApellido"] = objSalvoconducto.Aprovechamiento.Solicitante.PrimerApellido;
                        dtrTitularAprovechamiento["SegundoApellido"] = objSalvoconducto.Aprovechamiento.Solicitante.SegundoApellido;
                        dtrTitularAprovechamiento["CorreoElectronico"] = objSalvoconducto.Aprovechamiento.Solicitante.CorreoElectronico;
                        dtrTitularAprovechamiento["Telefono"] = objSalvoconducto.Aprovechamiento.Solicitante.Telefono;
                        dtrTitularAprovechamiento["NumeroIdentificacion"] = objSalvoconducto.Aprovechamiento.Solicitante.NumeroIdentificacion;
                        dtrTitularAprovechamiento["TipoDocumentoIdentificacion"] = objSalvoconducto.Aprovechamiento.Solicitante.TipoDocumentoIdentificacion.Nombre;
                        dtrTitularAprovechamiento["TipoPersona"] = objSalvoconducto.Aprovechamiento.Solicitante.TipoPersona.NombreTipoPersona;
                        if (!string.IsNullOrEmpty(objSalvoconducto.Aprovechamiento.Solicitante.DireccionPersona.DireccionPersona))
                            dtrTitularAprovechamiento["Direccion"] = objSalvoconducto.Aprovechamiento.Solicitante.DireccionPersona.DireccionPersona.ToString();
                        else
                            dtrTitularAprovechamiento["Direccion"] = "Direccion no registrada";
                        if (!string.IsNullOrEmpty(objSalvoconducto.Aprovechamiento.Solicitante.DireccionPersona.NombreMunicipio))
                            dtrTitularAprovechamiento["DireccionMunicipio"] = objSalvoconducto.Aprovechamiento.Solicitante.DireccionPersona.NombreMunicipio.ToString();
                        else
                            dtrTitularAprovechamiento["DireccionMunicipio"] = "Direccion no registrada";
                        dtrTitularAprovechamiento["RazonSocial"] = objSalvoconducto.Aprovechamiento.Solicitante.RazonSocial;
                        dtrTitularAprovechamiento["SolicitanteID"] = objSalvoconducto.Aprovechamiento.Solicitante.IdApplicationUser.ToString();
                    }
                }

                DatosXml.Tables["TITULAR_APROVECHAMIENTO"].Rows.Add(dtrTitularAprovechamiento);
            }
            else
            {
                DataRow dtrTitularAprovechamiento = DatosXml.Tables["TITULAR_APROVECHAMIENTO"].NewRow();
                dtrTitularAprovechamiento["AprovechamientoID"] = "-1";
                DatosXml.Tables["TITULAR_APROVECHAMIENTO"].Rows.Add(dtrTitularAprovechamiento);
            }

            foreach (EspecimenNewIdentity iEspecimen in objSalvoconducto.LstEspecimen)
            {
                DataRow dtrEspecimen = DatosXml.Tables["ESPECIMENES"].NewRow();
                dtrEspecimen["EspecieSalvoconductoID"] = iEspecimen.EspecieSalvoconductoID;
                dtrEspecimen["SalvocoductoID"] = iEspecimen.SalvocoductoID;
                dtrEspecimen["EspecieTaxonomiaID"] = iEspecimen.EspecieTaxonomiaID;
                dtrEspecimen["NombreEspecie"] = iEspecimen.NombreEspecie;
                dtrEspecimen["ClaseProducto"] = iEspecimen.ClaseProducto;
                dtrEspecimen["TipoProducto"] = iEspecimen.TipoProducto;
                dtrEspecimen["UnidadMedida"] = iEspecimen.UnidadMedida;
                dtrEspecimen["Descripcion"] = iEspecimen.Descripcion;
                dtrEspecimen["Identificacion"] = iEspecimen.Identificacion;
                dtrEspecimen["Cantidad"] = iEspecimen.Cantidad.ToString("N4");
                dtrEspecimen["Volumen"] = iEspecimen.Volumen.ToString("N4");
                dtrEspecimen["VolumenBruto"] = iEspecimen.VolumenBruto;
                dtrEspecimen["Dimensiones"] = iEspecimen.Dimensiones;
                //jmartinez Salvoconducto Fase 2
                dtrEspecimen["NombreComunEspecie"] = iEspecimen.NombreComunEspecie;
                //dtrEspecimen["CantidadDisponible"] = iEspecimen.CantidadDisponible;
                DatosXml.Tables["ESPECIMENES"].Rows.Add(dtrEspecimen);
                

            }
            foreach (RutaEntity iRuta in objSalvoconducto.LstRuta)
            {
                DataRow dtrRuta = DatosXml.Tables["RUTA"].NewRow();
                dtrRuta["RutaID"] = iRuta.RutaID;
                dtrRuta["Departamento"] = iRuta.Departamento;
                dtrRuta["Municipio"] = iRuta.Municipio;
                dtrRuta["Corregimiento"] = iRuta.Corregimiento;
                dtrRuta["Vereda"] = iRuta.Vereda;
                dtrRuta["Barrio"] = iRuta.Barrio;
                dtrRuta["Orden"] = iRuta.Orden;
                dtrRuta["TipoRuta"] = iRuta.TipoRuta;
                dtrRuta["SalvoconductoID"] = objSalvoconducto.SalvoconductoID;
                DatosXml.Tables["RUTA"].Rows.Add(dtrRuta);
            }
            foreach (TransporteNewIdentity iTransporte in objSalvoconducto.LstTransporte)
            {
                DataRow dtrTransporte = DatosXml.Tables["TRANSPORTE"].NewRow();
                dtrTransporte["SalvoconductoID"] = iTransporte.SalvoconductoID;
                dtrTransporte["TransporteSalvoconductoID"] = iTransporte.TransporteSalvoconductoID;
                dtrTransporte["TipoTransporte"] = iTransporte.TipoTransporte;
                dtrTransporte["ModoTransporte"] = iTransporte.ModoTransporte;
                dtrTransporte["Empresa"] = iTransporte.Empresa;
                dtrTransporte["NumeroIdentificacionMedioTransporte"] = iTransporte.NumeroIdentificacionMedioTransporte;
                dtrTransporte["NombreTransportador"] = iTransporte.NombreTransportador;
                dtrTransporte["NumeroIdentificacionTransportador"] = iTransporte.NumeroIdentificacionTransportador;
                dtrTransporte["TipoIdentificacionTransportador"] = iTransporte.TipoIdentificacionTransportador;
                DatosXml.Tables["TRANSPORTE"].Rows.Add(dtrTransporte);
            }

            DataRow dtrTitularSalvoconducto = DatosXml.Tables["TITULAR_SALVOCONDUCTO"].NewRow();
            dtrTitularSalvoconducto["PrimerNombre"] = objSalvoconducto.SolicitanteTitularPersonaIdentity.PrimerNombre;
            dtrTitularSalvoconducto["SegundoNombre"] = objSalvoconducto.SolicitanteTitularPersonaIdentity.SegundoNombre;
            dtrTitularSalvoconducto["PrimerApellido"] = objSalvoconducto.SolicitanteTitularPersonaIdentity.PrimerApellido;
            dtrTitularSalvoconducto["SegundoApellido"] = objSalvoconducto.SolicitanteTitularPersonaIdentity.SegundoApellido;
            dtrTitularSalvoconducto["CorreoElectronico"] = objSalvoconducto.SolicitanteTitularPersonaIdentity.CorreoElectronico;
            dtrTitularSalvoconducto["Telefono"] = objSalvoconducto.SolicitanteTitularPersonaIdentity.Telefono;
            dtrTitularSalvoconducto["NumeroIdentificacion"] = objSalvoconducto.SolicitanteTitularPersonaIdentity.NumeroIdentificacion;
            dtrTitularSalvoconducto["TipoDocumentoIdentificacion"] = objSalvoconducto.SolicitanteTitularPersonaIdentity.TipoDocumentoIdentificacion.Nombre;
            dtrTitularSalvoconducto["TipoPersona"] = objSalvoconducto.SolicitanteTitularPersonaIdentity.TipoPersona.NombreTipoPersona;
            if (!string.IsNullOrEmpty(objSalvoconducto.SolicitanteTitularPersonaIdentity.DireccionPersona.DireccionPersona))
                dtrTitularSalvoconducto["Direccion"] = objSalvoconducto.SolicitanteTitularPersonaIdentity.DireccionPersona.DireccionPersona.ToString();
            else
                dtrTitularSalvoconducto["Direccion"] = "Direccion no registrada";
            if (!string.IsNullOrEmpty(objSalvoconducto.SolicitanteTitularPersonaIdentity.DireccionPersona.NombreDepartamento))
                dtrTitularSalvoconducto["DireccionDepartamento"] = objSalvoconducto.SolicitanteTitularPersonaIdentity.DireccionPersona.NombreDepartamento.ToString();
            else
                dtrTitularSalvoconducto["DireccionDepartamento"] = "Direccion no registrada";
            if (!string.IsNullOrEmpty(objSalvoconducto.SolicitanteTitularPersonaIdentity.DireccionPersona.NombreMunicipio))
                dtrTitularSalvoconducto["DireccionMunicipio"] = objSalvoconducto.SolicitanteTitularPersonaIdentity.DireccionPersona.NombreMunicipio.ToString();
            else
                dtrTitularSalvoconducto["DireccionMunicipio"] = "Direccion no registrada";
            dtrTitularSalvoconducto["RazonSocial"] = objSalvoconducto.SolicitanteTitularPersonaIdentity.RazonSocial;
            dtrTitularSalvoconducto["SolicitanteID"] = objSalvoconducto.SolicitanteID.ToString();
            DatosXml.Tables["TITULAR_SALVOCONDUCTO"].Rows.Add(dtrTitularSalvoconducto);

            foreach (var item in objSalvoconducto.LstSalvoconductoAnterior)
            {
                DataRow dtrSalvoconductoAnterior = DatosXml.Tables["SALVOCONDUCTO_ANTERIOR"].NewRow();
                dtrSalvoconductoAnterior["Detalle"] = item.Detalle;
                dtrSalvoconductoAnterior["SalvoconductoID"] = objSalvoconducto.SalvoconductoID;
                DatosXml.Tables["SALVOCONDUCTO_ANTERIOR"].Rows.Add(dtrSalvoconductoAnterior);
            }
            foreach (var aprovechamiento in objSalvoconducto.LstAprovechamientoOrigen)
            {
                DataRow dtrAprovechamientoOrigen = DatosXml.Tables["APROVECHAMIENTO_ORIGEN"].NewRow();
                dtrAprovechamientoOrigen["Detalle"] = aprovechamiento.Detalle;
                dtrAprovechamientoOrigen["SalvoconductoID"] = objSalvoconducto.SalvoconductoID;
                dtrAprovechamientoOrigen["AprovechamientoID"] = aprovechamiento.AprovechamientoID.ToString();
                DatosXml.Tables["APROVECHAMIENTO_ORIGEN"].Rows.Add(dtrAprovechamientoOrigen);
            }

            //jmartinez salvoconducto Fase 2
            if (objSalvoconducto.LstEspecimen.Count > 0)
            {
                var Resultado = from x in (from y in objSalvoconducto.LstEspecimen
                                           select new
                                           {
                                               y.NombreComunEspecie,
                                               y.NombreEspecie,
                                               y.TipoProducto,
                                               y.UnidadMedida,
                                               y.Cantidad,
                                               y.Volumen
                                           })
                                group x by new {x.NombreComunEspecie, x.NombreEspecie, x.TipoProducto, x.UnidadMedida } into t
                                select new {t.Key.NombreComunEspecie, t.Key.NombreEspecie, t.Key.TipoProducto, t.Key.UnidadMedida, Cantidad = t.Sum(y => y.Cantidad), VolumenTotal = t.Sum(y => y.Volumen) };

                foreach (var ResultadoEspecimenes in Resultado)
                {
                    DataRow dtTotalesEspecimenes = DatosXml.Tables["DET_TIPO_PROD_UNID_MEDIDA_ESPEC"].NewRow();
                    dtTotalesEspecimenes["SalvoconductoID"] = objSalvoconducto.SalvoconductoID;
                    dtTotalesEspecimenes["NombreCientifico"] = ResultadoEspecimenes.NombreEspecie;
                    dtTotalesEspecimenes["NombreComun"] = ResultadoEspecimenes.NombreComunEspecie;
                    dtTotalesEspecimenes["TipoProducto"] = ResultadoEspecimenes.TipoProducto;
                    dtTotalesEspecimenes["UnidadMedida"] = ResultadoEspecimenes.UnidadMedida;
                    dtTotalesEspecimenes["Cantidad"] = ResultadoEspecimenes.Cantidad;
                    dtTotalesEspecimenes["VolumenTotal"] = ResultadoEspecimenes.VolumenTotal;
                    DatosXml.Tables["DET_TIPO_PROD_UNID_MEDIDA_ESPEC"].Rows.Add(dtTotalesEspecimenes);
                }
            }

                DatosXml.AcceptChanges();
            try
            {
                reporte.SetDataSource(DatosXml);
                GenerarReporteSalvoconducto(reporte, ConfigurationManager.AppSettings["FILE_TRAFFIC"].ToString() + "SUNL\\", nombreArchivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reporte != null)
                    reporte.Close();
            }

            return ConfigurationManager.AppSettings["FILE_TRAFFIC"].ToString() + "SUNL\\" + nombreArchivo;
        }

        public string GenerarSolicitudCertificadoFNCE(int certificadoID, string path)
        {
            string nombreArchivo = string.Empty;
            ReportDocument reporte = null;
            DataSet DatosXml = new DataSet();
            CertificadoUPMEDalc objCertificadoUPMEDalc = new CertificadoUPMEDalc();
            var objCertificado = objCertificadoUPMEDalc.ConsultaCertificadoUPMEFNCE(certificadoID);
            nombreArchivo = objCertificado.certificadoID + ".pdf";
            reporte = new Formularios.SolicitudCertificadoFNCE();
            DatosXml = new OrigenDatos.DSSolicitudFNCE();
            DataRow dtrSolicitudCertificado = DatosXml.Tables["SolicitudCertificadoFNCE"].NewRow();
            dtrSolicitudCertificado["CertificadoID"] = objCertificado.certificadoID;
            dtrSolicitudCertificado["NumeroCertificado"] = objCertificado.numeroCertificado;
            dtrSolicitudCertificado["TipoCertificado"] = objCertificado.tipoCertificacion;
            dtrSolicitudCertificado["NombreProyecto"] = objCertificado.nombreProyecto;
            dtrSolicitudCertificado["Municipio"] = objCertificado.municipio;
            dtrSolicitudCertificado["Etapa"] = objCertificado.etapa;
            dtrSolicitudCertificado["Departamento"] = objCertificado.departamento;
            dtrSolicitudCertificado["Latitud"] = objCertificado.latitud.ToString();
            dtrSolicitudCertificado["Longitud"] = objCertificado.longitud.ToString();
            dtrSolicitudCertificado["ValorTotalInversion"] = objCertificado.valorTotalInversion.ToString("C");
            dtrSolicitudCertificado["EnergiaAnualGenerada"] = objCertificado.energiaAnualGenerada.ToString("N");
            dtrSolicitudCertificado["FuenteNoConvencialUtilizar"] = objCertificado.fuenteNoConvencional;
            dtrSolicitudCertificado["FuenteConvencionalSustituir"] = objCertificado.fuenteConvencionalSustituir;
            dtrSolicitudCertificado["EmisionesCO2DejanEmitir"] = objCertificado.emisionesCO2.ToString("N");
            dtrSolicitudCertificado["ValorIVA"] = objCertificado.valorIVA.ToString("C");
            dtrSolicitudCertificado["NumeroReferenciaPago"] = objCertificado.numeroReferenciaPago;
            dtrSolicitudCertificado["ArchivoObjetoFinalidadProyecto"] = objCertificado.rutaDescripcionProyecto.Split('\\').Last();
            dtrSolicitudCertificado["ArchivoSoportePago"] = objCertificado.rutaSoportePago.Split('\\').Last();
            dtrSolicitudCertificado["SubFuenteConvencionalSustituir"] = objCertificado.subFuenteConvencionalSustituir;
            DatosXml.Tables["SolicitudCertificadoFNCE"].Rows.Add(dtrSolicitudCertificado);


            DataRow dtrSolicitudPrincipal = DatosXml.Tables["SolicitantePrincipal"].NewRow();
            dtrSolicitudPrincipal["CertificadoID"] = objCertificado.solicitantePrincial.certificadoID;
            dtrSolicitudPrincipal["NombreRazonSocial"] = objCertificado.solicitantePrincial.nombre;
            dtrSolicitudPrincipal["SectorProductivo"] = objCertificado.solicitantePrincial.sectorProductivo;
            dtrSolicitudPrincipal["CodigoCIIU"] = objCertificado.solicitantePrincial.codigoCIIU;
            dtrSolicitudPrincipal["TipoIdentificacion"] = objCertificado.solicitantePrincial.tipoIdentificacion;
            dtrSolicitudPrincipal["NumeroIdentificacion"] = objCertificado.solicitantePrincial.identificacion;
            dtrSolicitudPrincipal["Domicilio"] = objCertificado.solicitantePrincial.domicilio;
            dtrSolicitudPrincipal["Direccion"] = objCertificado.solicitantePrincial.direccion;
            dtrSolicitudPrincipal["Telefono"] = objCertificado.solicitantePrincial.telefono;
            dtrSolicitudPrincipal["Fax"] = string.Empty;
            dtrSolicitudPrincipal["CorreoElectronico"] = objCertificado.solicitantePrincial.emailContacto;
            dtrSolicitudPrincipal["PersonaContacto"] = objCertificado.solicitantePrincial.nombreContacto;
            DatosXml.Tables["SolicitantePrincipal"].Rows.Add(dtrSolicitudPrincipal);

            foreach (var solicitanteSecundario in objCertificado.lstSolicitanteSecundario)
            {
                DataRow dtrSolicitanteSecundario = DatosXml.Tables["SolicitanteSecundario"].NewRow();
                dtrSolicitanteSecundario["CertificadoID"] = solicitanteSecundario.certificadoID;
                dtrSolicitanteSecundario["NombreRazonSocial"] = solicitanteSecundario.nombre;
                dtrSolicitanteSecundario["SectorProductivo"] = solicitanteSecundario.sectorProductivo;
                dtrSolicitanteSecundario["CodigoCIIU"] = solicitanteSecundario.codigoCIIU;
                dtrSolicitanteSecundario["TipoIdentificacion"] = solicitanteSecundario.tipoIdentificacion;
                dtrSolicitanteSecundario["NumeroIdentificacion"] = solicitanteSecundario.identificacion;
                dtrSolicitanteSecundario["Domicilio"] = solicitanteSecundario.domicilio;
                dtrSolicitanteSecundario["Direccion"] = solicitanteSecundario.direccion;
                dtrSolicitanteSecundario["Telefono"] = solicitanteSecundario.telefono;
                dtrSolicitanteSecundario["Fax"] = string.Empty;
                dtrSolicitanteSecundario["CorreoElectronico"] = solicitanteSecundario.emailContacto;
                dtrSolicitanteSecundario["PersonaContacto"] = solicitanteSecundario.nombreContacto;
                DatosXml.Tables["SolicitanteSecundario"].Rows.Add(dtrSolicitanteSecundario);
            }

            foreach (var bien in objCertificado.lstBienes)
            {
                DataRow dtrBien = DatosXml.Tables["Bienes"].NewRow();
                dtrBien["CertificadoID"] = bien.certificadoID;
                dtrBien["Elemento"] = bien.elemento;
                dtrBien["SubpartidaArancelaria"] = bien.subpartida_arancelaria;
                dtrBien["Cantidad"] = bien.cantidad;
                dtrBien["Marca"] = bien.marca;
                dtrBien["ModeloReferencia"] = bien.modelo;
                dtrBien["FabricanteProveedor"] = bien.fabricante;
                dtrBien["ProveedorVendedor"] = bien.proveedor;
                dtrBien["Funcion"] = bien.funcion;
                dtrBien["IVA"] = objCertificado.tipoCertificacion== "IVA"? "X":"";
                dtrBien["Renta"] = objCertificado.tipoCertificacion == "RENTA" ? "X" : ""; 
                dtrBien["ValorTotalPesos"] = bien.valor_total.ToString("C");
                dtrBien["ValorIVAPesos"] = bien.iva.ToString("C");
                DatosXml.Tables["Bienes"].Rows.Add(dtrBien);
            }
            foreach (var servicio in objCertificado.lstServicios)
            {
                DataRow dtrServicio = DatosXml.Tables["Servicios"].NewRow();
                dtrServicio["CertificadoID"] = servicio.certificadoID;
                dtrServicio["Servicio"] = servicio.servicio;
                dtrServicio["Proveedor"] = servicio.proveedor;
                dtrServicio["IVA"] = objCertificado.tipoCertificacion == "IVA" ? "X" : ""; ;
                dtrServicio["Renta"] = objCertificado.tipoCertificacion == "RENTA" ? "X" : ""; ;
                dtrServicio["ValorTotalPesos"] = servicio.valor_total.ToString("C");
                dtrServicio["ValorIVAPesos"] = servicio.iva.ToString("C");
                dtrServicio["Funcion"] = servicio.alcance;
                DatosXml.Tables["Servicios"].Rows.Add(dtrServicio);
            }

            DatosXml.AcceptChanges();
            try
            {
                reporte.SetDataSource(DatosXml);
                GenerarReporte(reporte, path, nombreArchivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reporte != null)
                    reporte.Close();
            }
            return path + nombreArchivo;
        }

        private byte[] ObtenerImagenbyteAutoridadAmbiental(string nombreImagenAutoridad)
        {
            FileStream fs;
            BinaryReader br;
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["RutaImagenesAutoridades"].ToString() + nombreImagenAutoridad))
            {
                fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["RutaImagenesAutoridades"].ToString() + nombreImagenAutoridad, FileMode.Open);
            }
            else
            {
                fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["RutaImagenesAutoridades"].ToString() + "sinimagen.jpg", FileMode.Open);
            }
            br = new BinaryReader(fs);
            // define the byte array of filelength 
            byte[] imgbyte = new byte[fs.Length + 1];
            // read the bytes from the binary reader 
            imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));
            fs.Close();
            fs.Dispose();
            fs = null;
            return imgbyte;
        }


        public void GenerarFormularioPdfActividadRadicable(string pathSalida, string nombreArchivo, DataSet datos)
        {
            ReportDocument reporte = null;
            DataSet DatosXml = new DataSet();            
            DatosXml = new OrigenDatos.DSActividadRadicable();
            reporte = new Formularios.FormularioActividadRadicable();
                    
            try
            {                
                
                DatosXml.Merge(datos.Tables["FORMULARIO"]);
                DatosXml.Merge(datos.Tables["DETALLES"]);
                reporte.SetDataSource(DatosXml);
                GenerarReporte(reporte, pathSalida, nombreArchivo);
            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Critico, "GenerarActividadRadicablePdf -- " + nombreArchivo + " ---Error:  " + ex.ToString());
            }
            finally
            {
                if (reporte != null)
                    reporte.Close();
            }
        }


        /// <summary>
        /// Genera el pdf de la plantilla de notificación indicada
        /// </summary>
        /// <param name="p_strPathSalida">string con el path de salida</param>
        /// <param name="p_strNombreArchivo">string con el nombre del archivo</param>
        /// <param name="p_objDatos">DataSet con los datos para la generación de la plantilla</param>
        /// <param name="p_objPlantilla">Plantilla a generar</param>
        public void GenerarFormularioPdfNotificaciones(string p_strPathSalida, string p_strNombreArchivo, DataSet p_objDatos, NOTPlantilla p_objPlantilla)
        {
            ReportDocument objReporte = null;
            
            try
            {
                
                //Crear reporte
                if (p_objPlantilla.Equals(NOTPlantilla.FormularioNotificacion))
                {
                    objReporte = new Formularios.FormularioNotificacion();
                }
                else if (p_objPlantilla.Equals(NOTPlantilla.FormularioNotificacionNoFirma))
                {
                    objReporte = new Formularios.FormularioNotificacionNoFirma();
                }
                else if (p_objPlantilla.Equals(NOTPlantilla.FormularioNotificacionANLA))
                {
                    objReporte = new Formularios.FormularioNotificacionANLA();
                }
                else if (p_objPlantilla.Equals(NOTPlantilla.FormularioNotificacionNoFirmaANLA))
                {
                    objReporte = new Formularios.FormularioNotificacionNoFirmaANLA();
                }
                else if (p_objPlantilla.Equals(NOTPlantilla.FormularioNotificacionANLAFirmaIzquierda))
                {
                    objReporte = new Formularios.FormularioNotificacionANLAFirmaIzquierda();
                }

                //Si se cargo reporte generarlo
                if (objReporte != null)
                {
                    //Generar PDF
                    objReporte.SetDataSource(p_objDatos);
                    GenerarReporte(objReporte, p_strPathSalida, p_strNombreArchivo, PaperSize.PaperLegal);
                }
            }
            catch (Exception ex)
            {
                SMLog.Escribir(Severidad.Critico, "GenerarFormularioPdfNotificaciones -- " + p_strNombreArchivo + " ---Error:  " + ex.ToString());
            }
            finally
            {
                if (objReporte != null)
                    objReporte.Close();
            }
        }


        private DataTable CargarTablaImagenes(string numeroVital,DataTable dtImagenes)
        {

            DataRow row = dtImagenes.NewRow(); 
            row["Formulario_Id"] = 0;
            string ruta = ConfigurationManager.AppSettings["FILE_TRAFFIC"].ToString() + "Imagenes_Autoridades/Escudo.bmp";
            if (File.Exists(ConfigurationManager.AppSettings["FILE_TRAFFIC"].ToString() + "Imagenes_Autoridades/Escudo.bmp"))
                row["ImagenGeneral"] = File.ReadAllBytes(ConfigurationManager.AppSettings["FILE_TRAFFIC"].ToString() + "Imagenes_Autoridades/Escudo.bmp");
            else
                row["ImagenGeneral"]=new Byte[1];
            AutoridadAmbiental objAutoridadAmbiental = new AutoridadAmbiental();
            DataSet dsAutoridad = objAutoridadAmbiental.ListarAutoridadAmbientalXNumeroVital(numeroVital);
            if (File.Exists(ConfigurationManager.AppSettings["FILE_TRAFFIC"].ToString() + "Imagenes_Autoridades/"+dsAutoridad.Tables[0].Rows[0]["AUT_NOMBRE"].ToString()+".bmp"))
                row["ImagenAutoridad"] = File.ReadAllBytes(ConfigurationManager.AppSettings["FILE_TRAFFIC"].ToString() + "Imagenes_Autoridades/"+dsAutoridad.Tables[0].Rows[0]["AUT_NOMBRE"].ToString()+".bmp");
            else
                row["ImagenAutoridad"] = new Byte[1];
            dtImagenes.Rows.Add(row);
            //dtImagenes.TableName = "Imagenes";
            return dtImagenes;

        }
        /// <summary>
        /// crea el acuse de recepcion del la solicitud de vital creada
        /// </summary>
        /// <param name="numeroVITAL"></param>
        /// <param name="mensajeRecepcionSolicitud"></param>
        /// <returns></returns>
        public string GenerarRecepcionSolicitudTramite(string numeroVITAL, string mensajeRecepcionSolicitud)
        {
            string nombreArchivo = string.Empty;
            string path = string.Empty;
            ReportDocument reporte = null;
            DataSet DatosXml = new DataSet();
            reporte = new Formularios.RecepcionSolicitudTramite();
            DatosXml = new OrigenDatos.DSRecepcionSolicitudTramite();
            nombreArchivo = "AcuseRecibido_" + numeroVITAL + ".pdf";
            try
            {
                DataRow dtrRecepcionSolicitudTramite = DatosXml.Tables["DtRecepcionSolicitudTramite"].NewRow();
                dtrRecepcionSolicitudTramite["Mensaje"] = mensajeRecepcionSolicitud;
                DatosXml.Tables["DtRecepcionSolicitudTramite"].Rows.Add(dtrRecepcionSolicitudTramite);
                DatosXml.AcceptChanges();
                path = (new RadicacionDocumento()).ObtenerPathDocumentosNumeroVital(numeroVITAL);
                reporte.SetDataSource(DatosXml);
                GenerarReporte(reporte, path, nombreArchivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reporte != null)
                    reporte.Close();
            }
            return path + nombreArchivo;
        }

        public string GenerarRecepcionSolicitudTramite(string numeroVITAL, string mensajeRecepcionSolicitud, string path)
        {
            string nombreArchivo = string.Empty;
            //string path = string.Empty;
            ReportDocument reporte = null;
            DataSet DatosXml = new DataSet();
            reporte = new Formularios.RecepcionSolicitudTramite();
            DatosXml = new OrigenDatos.DSRecepcionSolicitudTramite();
            nombreArchivo = "AcuseRecibido_" + numeroVITAL + ".pdf";
            try
            {
                DataRow dtrRecepcionSolicitudTramite = DatosXml.Tables["DtRecepcionSolicitudTramite"].NewRow();
                dtrRecepcionSolicitudTramite["Mensaje"] = mensajeRecepcionSolicitud;
                DatosXml.Tables["DtRecepcionSolicitudTramite"].Rows.Add(dtrRecepcionSolicitudTramite);
                DatosXml.AcceptChanges();
                //path = (new RadicacionDocumento()).ObtenerPathDocumentosNumeroVital(numeroVITAL);
                reporte.SetDataSource(DatosXml);
                GenerarReporte(reporte, path, nombreArchivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reporte != null)
                    reporte.Close();
            }
            return path + nombreArchivo;
        }

    }


}
